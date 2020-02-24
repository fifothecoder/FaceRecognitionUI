namespace FaceRecognitionUI
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pboxMain = new System.Windows.Forms.PictureBox();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.btnDetect = new System.Windows.Forms.Button();
            this.tbMain = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pboxMain
            // 
            this.pboxMain.Location = new System.Drawing.Point(12, 12);
            this.pboxMain.Name = "pboxMain";
            this.pboxMain.Size = new System.Drawing.Size(738, 502);
            this.pboxMain.TabIndex = 0;
            this.pboxMain.TabStop = false;
            this.pboxMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pboxMain_MouseClick);
            // 
            // btnAddImage
            // 
            this.btnAddImage.Location = new System.Drawing.Point(756, 12);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(187, 49);
            this.btnAddImage.TabIndex = 1;
            this.btnAddImage.Text = "Add CurrentImage";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(949, 12);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(192, 49);
            this.btnDetect.TabIndex = 2;
            this.btnDetect.Text = "Detect Faces";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // tbMain
            // 
            this.tbMain.Location = new System.Drawing.Point(756, 67);
            this.tbMain.Multiline = true;
            this.tbMain.Name = "tbMain";
            this.tbMain.ReadOnly = true;
            this.tbMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMain.Size = new System.Drawing.Size(385, 447);
            this.tbMain.TabIndex = 3;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 526);
            this.Controls.Add(this.tbMain);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.pboxMain);
            this.Name = "FormMain";
            this.Text = "Face Recognition";
            ((System.ComponentModel.ISupportInitialize)(this.pboxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxMain;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.TextBox tbMain;
    }
}

