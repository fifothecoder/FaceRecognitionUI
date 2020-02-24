using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;

namespace FaceRecognitionUI
{

    public struct Coords {
        public int x1;
        public int x2;
        public int y1;
        public int y2;
        public int face;
    }

    public partial class FormMain : Form {
        private string FILE_PATH = string.Empty;
        private const string API_KEY = "749f8ce38b79476a91e899291bad7ea4";
        private const string API_URIBASE = "https://f-reco.cognitiveservices.azure.com/face/v1.0/detect";
        private Graphics __g__ = null;
        private Graphics G => __g__ ?? (__g__ = pboxMain.CreateGraphics());
        private List<Coords> Hovers = new List<Coords>();
        private Bitmap CurrentImage;


        public FormMain() {
            InitializeComponent();
            pboxMain.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void btnAddImage_Click(object sender, EventArgs e) {
            using (OpenFileDialog ofd = new OpenFileDialog()) {
                ofd.Multiselect = false;
                ofd.Filter = "JPG (*.jpg,*.jpeg)|*.jpg;*.jpeg|PNG files|*.png|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) { FILE_PATH = ofd.FileName; }
                else {
                    MessageBox.Show("CurrentImage did not load successfully!");
                    return;
                }
            }

            Bitmap image = new Bitmap(FILE_PATH);
            pboxMain.Image = image;

        }

        private async void btnDetect_Click(object sender, EventArgs e) {
            string response = null;
            if (File.Exists(FILE_PATH)) response = await MakeAnalysisRequest();
            if (response == null) {
                MessageBox.Show("Processing failed!");
                return;
            }

            //Validate JSON
            dynamic stuff = JsonConvert.DeserializeObject(response);
            try {
                if (stuff.error != null) {
                    MessageBox.Show("CurrentImage is too big to process!");
                    return;
                }
            }
            catch (RuntimeBinderException) { }

            int FaceCount = 0;
            for (int i = 0; i < int.MaxValue; i++, FaceCount++) {
                try {
                    if (stuff[i] == null) break;
                }
                catch (ArgumentOutOfRangeException) { break; }
            }


            Bitmap image = new Bitmap(FILE_PATH);
            tbMain.Text = "";

            Hovers.Clear();
            for (int faceIndex = 0; faceIndex < FaceCount; faceIndex++) {

                tbMain.Text += $"Face {faceIndex} :{Environment.NewLine}";
                //tbMain.Text += $"\tFaceId : {stuff[faceIndex].faceId}{Environment.NewLine}";
                tbMain.Text += $"\tGender : {stuff[faceIndex].faceAttributes.gender}{Environment.NewLine}";
                tbMain.Text += $"\tGlasses : {stuff[faceIndex].faceAttributes.glasses}{Environment.NewLine}";


                int HairCount = 0;
                for (int i = 0; i < int.MaxValue; i++, HairCount++) {
                    try {
                        if (stuff[faceIndex].faceAttributes.hair.hairColor[i] == null) break;
                    }
                    catch (ArgumentOutOfRangeException) { break; }
                }


                //Detect Hair
                string hairCol = null;
                float hairConfidence = 0;
                for (int i = 0; i < HairCount; i++) {
                    if (stuff[faceIndex].faceAttributes.hair.hairColor[i].confidence > hairConfidence) {
                        hairCol = stuff[faceIndex].faceAttributes.hair.hairColor[i].color;
                        hairConfidence = stuff[faceIndex].faceAttributes.hair.hairColor[i].confidence;
                    }
                }

                if (stuff[faceIndex].faceAttributes.hair.bald > hairConfidence) {
                    hairCol = "bald";
                    hairConfidence = stuff[faceIndex].faceAttributes.hair.bald;
                }

                tbMain.Text += $"\tHair : {hairCol} with confidence of {hairConfidence * 100}%{Environment.NewLine}";


                int t = stuff[faceIndex].faceRectangle.top;
                int l = stuff[faceIndex].faceRectangle.left;
                int w = stuff[faceIndex].faceRectangle.width;
                int h = stuff[faceIndex].faceRectangle.height;

                Hovers.Add(new Coords() {face = faceIndex, x1 = l, x2 = w + l, y1 = t, y2 = t + h});

                //Add Borders
                for (int i = t; i < t + h; i++) {

                    if (l - 2 >= 0) image.SetPixel(l - 2, i, Color.DeepPink);
                    if (l - 1 >= 0) image.SetPixel(l - 1, i, Color.DeepPink);
                    image.SetPixel(l, i, Color.DeepPink);
                    image.SetPixel(l + 1, i, Color.DeepPink);
                    image.SetPixel(l + 2, i, Color.DeepPink);

                    image.SetPixel(l + w - 2, i, Color.DeepPink);
                    image.SetPixel(l + w - 1, i, Color.DeepPink);
                    image.SetPixel(l + w, i, Color.DeepPink);
                    if (l + w + 1 < image.Width) image.SetPixel(l + w + 1, i, Color.DeepPink);
                    if (l + w + 2 < image.Width) image.SetPixel(l + w + 2, i, Color.DeepPink);
                }

                for (int i = l; i < l + w; i++) {

                    if (t - 2 >= 0) image.SetPixel(i, t - 2, Color.DeepPink);
                    if (t - 1 >= 0) image.SetPixel(i, t - 1, Color.DeepPink);
                    image.SetPixel(i, t, Color.DeepPink);
                    image.SetPixel(i, t + 1, Color.DeepPink);
                    image.SetPixel(i, t + 2, Color.DeepPink);

                    image.SetPixel(i, t + h - 2, Color.DeepPink);
                    image.SetPixel(i, t + h - 1, Color.DeepPink);
                    image.SetPixel(i, t + h, Color.DeepPink);
                    if (t + h + 1 < image.Height) image.SetPixel(i, t + h + 1, Color.DeepPink);
                    if (t + h + 2 < image.Height) image.SetPixel(i, t + h + 2, Color.DeepPink);
                }
            }

            CurrentImage = image;
            pboxMain.Image = image;
        }

        async Task<string> MakeAnalysisRequest() {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", API_KEY);

            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                                       "&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
                                       "emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            string uri = API_URIBASE + "?" + requestParameters;

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(FILE_PATH);

            using (ByteArrayContent content = new ByteArrayContent(byteData)) {
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(uri, content);

                string contentString = await response.Content.ReadAsStringAsync();
                return contentString;

            }
        }

        byte[] GetImageAsByteArray(string imageFilePath) {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read)) {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int) fileStream.Length);
            }
        }

        private void pboxMain_MouseClick(object sender, MouseEventArgs e) {
            return;

            Coords current = new Coords();
            current.x1 = -100000;
            
            foreach (var coord in Hovers) {

                Point mouse = new Point(e.X, e.Y);

                if (mouse.X > coord.x1 && mouse.X < coord.x2 && mouse.Y > coord.x1 && mouse.Y < coord.x2) {
                    current = coord;
                    break;
                }
            }

            if (current.x1 == -100000) return;

            //Cut
            Bitmap bm = new Bitmap(current.x2 - current.x1, current.y2 - current.y1);
            for (int i = 0; i < current.x2 - current.x1; i++) {
                for (int j = 0; j < current.y2 - current.y1; j++) {
                    bm.SetPixel(i, j, CurrentImage.GetPixel(i + current.x1, j + current.y1));
                }
            }

            FormHighlight fh = new FormHighlight(bm);
            fh.ShowDialog();
        }

        private Point TranslateStretchImageMousePosition(Point coordinates)
        {
            // test to make sure our image is not null
            if (pboxMain.Image == null) return coordinates;
            // Make sure our control width and height are not 0
            if (Width == 0 || Height == 0) return coordinates;
            // First, get the ratio (image to control) the height and width
            float ratioWidth = (float)pboxMain.Image.Width / Width;
            float ratioHeight = (float)pboxMain.Image.Height / Height;
            // Scale the points by our ratio
            float newX = coordinates.X;
            float newY = coordinates.Y;
            newX *= ratioWidth;
            newY *= ratioHeight;
            return new Point((int)newX, (int)newY);
        }
    }
}
