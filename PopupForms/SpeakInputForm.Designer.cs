namespace Artco
{
    partial class SpeakInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeakInputForm));
            this.txtbox_SpeakText = new System.Windows.Forms.TextBox();
            this.btn_Submit = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Close = new Bunifu.Framework.UI.BunifuImageButton();
            this.combobox_VarLiat = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Submit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbox_SpeakText
            // 
            this.txtbox_SpeakText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_SpeakText.Font = new System.Drawing.Font("Gulim", 20F);
            this.txtbox_SpeakText.Location = new System.Drawing.Point(16, 151);
            this.txtbox_SpeakText.MaxLength = 30;
            this.txtbox_SpeakText.Name = "txtbox_SpeakText";
            this.txtbox_SpeakText.Size = new System.Drawing.Size(266, 31);
            this.txtbox_SpeakText.TabIndex = 0;
            this.txtbox_SpeakText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txtbox_SpeakText_KeyDown);
            // 
            // btn_Submit
            // 
            this.btn_Submit.BackColor = System.Drawing.Color.Transparent;
            this.btn_Submit.Font = new System.Drawing.Font("Magic R", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Submit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Submit.Image")));
            this.btn_Submit.ImageActive = null;
            this.btn_Submit.Location = new System.Drawing.Point(288, 205);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(104, 44);
            this.btn_Submit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Submit.TabIndex = 10;
            this.btn_Submit.TabStop = false;
            this.btn_Submit.Zoom = 10;
            this.btn_Submit.Click += new System.EventHandler(this.Btn_Submit_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Font = new System.Drawing.Font("Magic R", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.ImageActive = null;
            this.btn_Close.Location = new System.Drawing.Point(178, 205);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(104, 44);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Close.TabIndex = 11;
            this.btn_Close.TabStop = false;
            this.btn_Close.Zoom = 10;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // combobox_VarLiat
            // 
            this.combobox_VarLiat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_VarLiat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combobox_VarLiat.Font = new System.Drawing.Font("Gulim", 18F);
            this.combobox_VarLiat.FormattingEnabled = true;
            this.combobox_VarLiat.Location = new System.Drawing.Point(288, 151);
            this.combobox_VarLiat.Margin = new System.Windows.Forms.Padding(4);
            this.combobox_VarLiat.Name = "combobox_VarLiat";
            this.combobox_VarLiat.Size = new System.Drawing.Size(104, 32);
            this.combobox_VarLiat.TabIndex = 12;
            this.combobox_VarLiat.DropDownClosed += new System.EventHandler(this.Combobox_VarLiat_DropDownClosed);
            // 
            // SpeakInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(406, 274);
            this.Controls.Add(this.combobox_VarLiat);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.txtbox_SpeakText);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SpeakInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "니나니뇨";
            this.Load += new System.EventHandler(this.SpeakInputForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Submit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbox_SpeakText;
        private Bunifu.Framework.UI.BunifuImageButton btn_Submit;
        private Bunifu.Framework.UI.BunifuImageButton btn_Close;
        private System.Windows.Forms.ComboBox combobox_VarLiat;
    }
}