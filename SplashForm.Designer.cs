namespace Artco
{
    partial class SplashForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.richbox_Log = new System.Windows.Forms.RichTextBox();
            this.lbl_Details = new System.Windows.Forms.Label();
            this.pbx_Loading = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Loading)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.richbox_Log);
            this.panel1.Controls.Add(this.lbl_Details);
            this.panel1.Controls.Add(this.pbx_Loading);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1150, 680);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1282, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Log";
            // 
            // richbox_Log
            // 
            this.richbox_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richbox_Log.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richbox_Log.Location = new System.Drawing.Point(1284, 26);
            this.richbox_Log.Name = "richbox_Log";
            this.richbox_Log.Size = new System.Drawing.Size(204, 550);
            this.richbox_Log.TabIndex = 2;
            this.richbox_Log.Text = "";
            // 
            // lbl_Details
            // 
            this.lbl_Details.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Details.AutoSize = true;
            this.lbl_Details.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(176)))), ((int)(((byte)(72)))));
            this.lbl_Details.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Details.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Details.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbl_Details.Location = new System.Drawing.Point(1049, 9);
            this.lbl_Details.Name = "lbl_Details";
            this.lbl_Details.Size = new System.Drawing.Size(89, 16);
            this.lbl_Details.TabIndex = 1;
            this.lbl_Details.Text = "Show details";
            this.lbl_Details.Click += new System.EventHandler(this.Lbl_Details_Click);
            // 
            // pbx_Loading
            // 
            this.pbx_Loading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbx_Loading.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbx_Loading.Location = new System.Drawing.Point(0, 0);
            this.pbx_Loading.Name = "pbx_Loading";
            this.pbx_Loading.Size = new System.Drawing.Size(1280, 680);
            this.pbx_Loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbx_Loading.TabIndex = 0;
            this.pbx_Loading.TabStop = false;
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1150, 680);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoadingForm";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.Load += new System.EventHandler(this.SplashForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbx_Loading;
                private System.Windows.Forms.Label lbl_Details;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richbox_Log;
    }
}