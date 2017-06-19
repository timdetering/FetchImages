namespace FetchImages
{
    partial class MainForm
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
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetImages = new System.Windows.Forms.Button();
            this.listImages = new System.Windows.Forms.ListBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(111, 18);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(296, 22);
            this.txtURL.TabIndex = 0;
            this.txtURL.Text = "http://www.vcskicks.com/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URL:";
            // 
            // btnGetImages
            // 
            this.btnGetImages.Location = new System.Drawing.Point(173, 44);
            this.btnGetImages.Name = "btnGetImages";
            this.btnGetImages.Size = new System.Drawing.Size(134, 23);
            this.btnGetImages.TabIndex = 2;
            this.btnGetImages.Text = "Get Images";
            this.btnGetImages.UseVisualStyleBackColor = true;
            this.btnGetImages.Click += new System.EventHandler(this.btnGetImages_Click);
            // 
            // listImages
            // 
            this.listImages.FormattingEnabled = true;
            this.listImages.Location = new System.Drawing.Point(16, 88);
            this.listImages.Name = "listImages";
            this.listImages.Size = new System.Drawing.Size(448, 82);
            this.listImages.TabIndex = 3;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(173, 176);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(134, 23);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(173, 199);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(134, 23);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Download All";
            this.btnDownload.UseVisualStyleBackColor = true;
            // 
            // picImage
            // 
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Location = new System.Drawing.Point(16, 233);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(448, 218);
            this.picImage.TabIndex = 6;
            this.picImage.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.CornflowerBlue;
            this.linkLabel1.Location = new System.Drawing.Point(382, 470);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(83, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Visual C# Kicks";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.CornflowerBlue;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(173, 457);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(134, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 500);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.listImages);
            this.Controls.Add(this.btnGetImages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtURL);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Fetch Images";
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetImages;
        private System.Windows.Forms.ListBox listImages;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnSave;
    }
}

