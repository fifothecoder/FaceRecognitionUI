using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognitionUI
{
    public partial class FormHighlight : Form {
        private Bitmap Highlight;
        public FormHighlight(Bitmap image)
        {
            InitializeComponent();
            image = Highlight;
        }

        private void FormHighlight_Load(object sender, EventArgs e) { pboxHighlight.Image = Highlight; }
    }
}
