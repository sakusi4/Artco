namespace Artco
{
    partial class UpdateForm
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
            if ( disposing && ( components != null ) )
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
                        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
                        this.richtxtbox_Update = new System.Windows.Forms.RichTextBox();
                        this.lbl_Status = new System.Windows.Forms.Label();
                        this.lbl_Details = new System.Windows.Forms.Label();
                        this.pictureBox1 = new System.Windows.Forms.PictureBox();
                        this.btn_Submit = new Bunifu.Framework.UI.BunifuImageButton();
                        this.prgbar_Update = new System.Windows.Forms.ProgressBar();
                        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                        ((System.ComponentModel.ISupportInitialize)(this.btn_Submit)).BeginInit();
                        this.SuspendLayout();
                        // 
                        // richtxtbox_Update
                        // 
                        this.richtxtbox_Update.BackColor = System.Drawing.Color.White;
                        this.richtxtbox_Update.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        this.richtxtbox_Update.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F);
                        this.richtxtbox_Update.Location = new System.Drawing.Point(12, 183);
                        this.richtxtbox_Update.Name = "richtxtbox_Update";
                        this.richtxtbox_Update.ReadOnly = true;
                        this.richtxtbox_Update.Size = new System.Drawing.Size(376, 193);
                        this.richtxtbox_Update.TabIndex = 2;
                        this.richtxtbox_Update.Text = "";
                        // 
                        // lbl_Status
                        // 
                        this.lbl_Status.AutoSize = true;
                        this.lbl_Status.BackColor = System.Drawing.Color.Transparent;
                        this.lbl_Status.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.lbl_Status.ForeColor = System.Drawing.SystemColors.Control;
                        this.lbl_Status.Location = new System.Drawing.Point(17, 58);
                        this.lbl_Status.Name = "lbl_Status";
                        this.lbl_Status.Size = new System.Drawing.Size(0, 16);
                        this.lbl_Status.TabIndex = 5;
                        // 
                        // lbl_Details
                        // 
                        this.lbl_Details.AutoSize = true;
                        this.lbl_Details.BackColor = System.Drawing.Color.Transparent;
                        this.lbl_Details.Cursor = System.Windows.Forms.Cursors.Hand;
                        this.lbl_Details.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.lbl_Details.ForeColor = System.Drawing.Color.RoyalBlue;
                        this.lbl_Details.Location = new System.Drawing.Point(9, 136);
                        this.lbl_Details.Name = "lbl_Details";
                        this.lbl_Details.Size = new System.Drawing.Size(89, 16);
                        this.lbl_Details.TabIndex = 6;
                        this.lbl_Details.Text = "Show details";
                        this.lbl_Details.Click += new System.EventHandler(this.Lbl_Details_Click);
                        // 
                        // pictureBox1
                        // 
                        this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
                        this.pictureBox1.Image = global::Artco.Properties.Resources.updatebar;
                        this.pictureBox1.Location = new System.Drawing.Point(15, 83);
                        this.pictureBox1.Name = "pictureBox1";
                        this.pictureBox1.Size = new System.Drawing.Size(376, 19);
                        this.pictureBox1.TabIndex = 7;
                        this.pictureBox1.TabStop = false;
                        // 
                        // btn_Submit
                        // 
                        this.btn_Submit.BackColor = System.Drawing.Color.Transparent;
                        this.btn_Submit.Image = global::Artco.Properties.Resources.Update_OKButton;
                        this.btn_Submit.ImageActive = null;
                        this.btn_Submit.Location = new System.Drawing.Point(309, 121);
                        this.btn_Submit.Name = "btn_Submit";
                        this.btn_Submit.Size = new System.Drawing.Size(80, 33);
                        this.btn_Submit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                        this.btn_Submit.TabIndex = 8;
                        this.btn_Submit.TabStop = false;
                        this.btn_Submit.Zoom = 10;
                        this.btn_Submit.Click += new System.EventHandler(this.Btn_Submit_Click);
                        // 
                        // prgbar_Update
                        // 
                        this.prgbar_Update.Location = new System.Drawing.Point(22, 86);
                        this.prgbar_Update.Name = "prgbar_Update";
                        this.prgbar_Update.Size = new System.Drawing.Size(362, 13);
                        this.prgbar_Update.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
                        this.prgbar_Update.TabIndex = 4;
                        this.prgbar_Update.Value = 50;
                        // 
                        // UpdateForm
                        // 
                        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
                        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                        this.BackColor = System.Drawing.Color.White;
                        this.BackgroundImage = global::Artco.Properties.Resources.UpdateFormBG;
                        this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                        this.ClientSize = new System.Drawing.Size(401, 161);
                        this.Controls.Add(this.btn_Submit);
                        this.Controls.Add(this.lbl_Details);
                        this.Controls.Add(this.lbl_Status);
                        this.Controls.Add(this.prgbar_Update);
                        this.Controls.Add(this.richtxtbox_Update);
                        this.Controls.Add(this.pictureBox1);
                        this.DoubleBuffered = true;
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                        this.Name = "UpdateForm";
                        this.ShowInTaskbar = false;
                        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        this.Text = "Artco Update";                        
                        this.Load += new System.EventHandler(this.UpdateForm_Load);
                        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                        ((System.ComponentModel.ISupportInitialize)(this.btn_Submit)).EndInit();
                        this.ResumeLayout(false);
                        this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richtxtbox_Update;
                private System.Windows.Forms.Label lbl_Status;
                private System.Windows.Forms.Label lbl_Details;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuImageButton btn_Submit;
        private System.Windows.Forms.ProgressBar prgbar_Update;
    }
}