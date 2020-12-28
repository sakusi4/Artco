namespace Artco
{
    partial class RenameSpriteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameSpriteForm));
            this.txt_NewName = new System.Windows.Forms.TextBox();
            this.btn_Submit = new Bunifu.Framework.UI.BunifuImageButton();
            this.pbx_Input = new System.Windows.Forms.PictureBox();
            this.btn_Close = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Submit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_NewName
            // 
            this.txt_NewName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_NewName.Font = new System.Drawing.Font("Gulim", 15F);
            this.txt_NewName.Location = new System.Drawing.Point(35, 138);
            this.txt_NewName.Name = "txt_NewName";
            this.txt_NewName.Size = new System.Drawing.Size(234, 23);
            this.txt_NewName.TabIndex = 1;
            this.txt_NewName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_NewName_KeyDown);
            // 
            // btn_Submit
            // 
            this.btn_Submit.BackColor = System.Drawing.Color.Transparent;
            this.btn_Submit.Font = new System.Drawing.Font("Magic R", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Submit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Submit.Image")));
            this.btn_Submit.ImageActive = null;
            this.btn_Submit.Location = new System.Drawing.Point(290, 126);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(104, 44);
            this.btn_Submit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Submit.TabIndex = 0;
            this.btn_Submit.TabStop = false;
            this.btn_Submit.Zoom = 10;
            this.btn_Submit.Click += new System.EventHandler(this.Btn_Submit_Click);
            // 
            // pbx_Input
            // 
            this.pbx_Input.BackColor = System.Drawing.Color.Transparent;
            this.pbx_Input.BackgroundImage = global::Artco.Properties.Resources.RenameInput;
            this.pbx_Input.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbx_Input.Location = new System.Drawing.Point(23, 126);
            this.pbx_Input.Name = "pbx_Input";
            this.pbx_Input.Size = new System.Drawing.Size(261, 44);
            this.pbx_Input.TabIndex = 2;
            this.pbx_Input.TabStop = false;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Image = global::Artco.Properties.Resources.Close;
            this.btn_Close.ImageActive = null;
            this.btn_Close.Location = new System.Drawing.Point(375, 5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(25, 25);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Close.TabIndex = 8;
            this.btn_Close.TabStop = false;
            this.btn_Close.Zoom = 10;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // RenameSpriteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(406, 202);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_NewName);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.pbx_Input);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RenameSpriteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RenameSpriteForm";
            this.Load += new System.EventHandler(this.RenameSpriteForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Submit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuImageButton btn_Submit;
        private System.Windows.Forms.TextBox txt_NewName;
        private System.Windows.Forms.PictureBox pbx_Input;
        private Bunifu.Framework.UI.BunifuImageButton btn_Close;
    }
}