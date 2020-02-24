namespace FaceRecognitionUI
{
    partial class FormHighlight
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
            this.pboxHighlight = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboxHighlight)).BeginInit();
            this.SuspendLayout();
            // 
            // pboxHighlight
            // 
            this.pboxHighlight.Location = new System.Drawing.Point(12, 12);
            this.pboxHighlight.Name = "pboxHighlight";
            this.pboxHighlight.Size = new System.Drawing.Size(231, 231);
            this.pboxHighlight.TabIndex = 0;
            this.pboxHighlight.TabStop = false;
            // 
            // FormHighlight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 355);
            this.Controls.Add(this.pboxHighlight);
            this.Name = "FormHighlight";
            this.Text = "FormHighlight";
            this.Load += new System.EventHandler(this.FormHighlight_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxHighlight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxHighlight;
    }
}