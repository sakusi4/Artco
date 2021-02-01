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
            this.btn_Close = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Submit = new Bunifu.Framework.UI.BunifuImageButton();
            this.listbox_VarList = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Submit)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbox_SpeakText
            // 
            this.txtbox_SpeakText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_SpeakText.Font = new System.Drawing.Font("Gulim", 20F);
            this.txtbox_SpeakText.Location = new System.Drawing.Point(30, 146);
            this.txtbox_SpeakText.MaxLength = 30;
            this.txtbox_SpeakText.Multiline = true;
            this.txtbox_SpeakText.Name = "txtbox_SpeakText";
            this.txtbox_SpeakText.Size = new System.Drawing.Size(381, 136);
            this.txtbox_SpeakText.TabIndex = 0;
            this.txtbox_SpeakText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txtbox_SpeakText_KeyDown);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Font = new System.Drawing.Font("Magic R", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Close.Image = global::Artco.Properties.Resources.MsgBoxcancelBtn;
            this.btn_Close.ImageActive = null;
            this.btn_Close.Location = new System.Drawing.Point(198, 317);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(104, 44);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Close.TabIndex = 11;
            this.btn_Close.TabStop = false;
            this.btn_Close.Zoom = 10;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // btn_Submit
            // 
            this.btn_Submit.BackColor = System.Drawing.Color.Transparent;
            this.btn_Submit.Font = new System.Drawing.Font("Magic R", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Submit.Image = global::Artco.Properties.Resources.MsgBoxOKBtn;
            this.btn_Submit.ImageActive = null;
            this.btn_Submit.Location = new System.Drawing.Point(308, 317);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(104, 44);
            this.btn_Submit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Submit.TabIndex = 10;
            this.btn_Submit.TabStop = false;
            this.btn_Submit.Zoom = 10;
            this.btn_Submit.Click += new System.EventHandler(this.Btn_Submit_Click);
            // 
            // listbox_VarList
            // 
            this.listbox_VarList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbox_VarList.Font = new System.Drawing.Font("Gulim", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listbox_VarList.FormattingEnabled = true;
            this.listbox_VarList.ItemHeight = 24;
            this.listbox_VarList.Location = new System.Drawing.Point(444, 51);
            this.listbox_VarList.Name = "listbox_VarList";
            this.listbox_VarList.Size = new System.Drawing.Size(171, 288);
            this.listbox_VarList.TabIndex = 13;
            this.listbox_VarList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Listbox_Varlist_Click);
            // 
            // SpeakInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::Artco.Properties.Resources.SpeakTextForm;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(636, 373);
            this.Controls.Add(this.listbox_VarList);
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
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Submit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbox_SpeakText;
        private System.Windows.Forms.ListBox listbox_VarList;
        private Bunifu.Framework.UI.BunifuImageButton btn_Submit;
        private Bunifu.Framework.UI.BunifuImageButton btn_Close;
    }
}