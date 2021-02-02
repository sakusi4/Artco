namespace Artco
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.txtbox_VideoPath = new System.Windows.Forms.TextBox();
            this.txtbox_SaveFolder = new System.Windows.Forms.TextBox();
            this.txtbox_AudioPath = new System.Windows.Forms.TextBox();
            this.btn_ChangeAudioPath = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_OpenSaveFolder = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_ChangeVideoPath = new Bunifu.Framework.UI.BunifuImageButton();
            this.cmbbox_Resolution = new System.Windows.Forms.ComboBox();
            this.cmbbox_Theme = new System.Windows.Forms.ComboBox();
            this.cmbbox_InputDevices = new System.Windows.Forms.ComboBox();
            this.cmbbox_AudioFormat = new System.Windows.Forms.ComboBox();
            this.cmbbox_SampleRate = new System.Windows.Forms.ComboBox();
            this.cmbbox_Channels = new System.Windows.Forms.ComboBox();
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.lbl_SerialCode = new System.Windows.Forms.Label();
            this.pnl_Setting = new Artco.DoubleBufferedPanel();
            this.btn_Close = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ChangeAudioPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_OpenSaveFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ChangeVideoPath)).BeginInit();
            this.pnl_Setting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbox_VideoPath
            // 
            this.txtbox_VideoPath.BackColor = System.Drawing.Color.White;
            this.txtbox_VideoPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_VideoPath.Location = new System.Drawing.Point(171, 372);
            this.txtbox_VideoPath.Name = "txtbox_VideoPath";
            this.txtbox_VideoPath.ReadOnly = true;
            this.txtbox_VideoPath.Size = new System.Drawing.Size(110, 13);
            this.txtbox_VideoPath.TabIndex = 19;
            // 
            // txtbox_SaveFolder
            // 
            this.txtbox_SaveFolder.BackColor = System.Drawing.Color.White;
            this.txtbox_SaveFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_SaveFolder.Location = new System.Drawing.Point(171, 324);
            this.txtbox_SaveFolder.Name = "txtbox_SaveFolder";
            this.txtbox_SaveFolder.ReadOnly = true;
            this.txtbox_SaveFolder.Size = new System.Drawing.Size(110, 13);
            this.txtbox_SaveFolder.TabIndex = 28;
            // 
            // txtbox_AudioPath
            // 
            this.txtbox_AudioPath.BackColor = System.Drawing.Color.White;
            this.txtbox_AudioPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_AudioPath.Location = new System.Drawing.Point(171, 420);
            this.txtbox_AudioPath.Name = "txtbox_AudioPath";
            this.txtbox_AudioPath.ReadOnly = true;
            this.txtbox_AudioPath.Size = new System.Drawing.Size(110, 13);
            this.txtbox_AudioPath.TabIndex = 29;
            // 
            // btn_ChangeAudioPath
            // 
            this.btn_ChangeAudioPath.BackColor = System.Drawing.Color.Transparent;
            this.btn_ChangeAudioPath.Image = global::Artco.Properties.Resources.OpenDirectory;
            this.btn_ChangeAudioPath.ImageActive = null;
            this.btn_ChangeAudioPath.Location = new System.Drawing.Point(295, 412);
            this.btn_ChangeAudioPath.Name = "btn_ChangeAudioPath";
            this.btn_ChangeAudioPath.Size = new System.Drawing.Size(28, 28);
            this.btn_ChangeAudioPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_ChangeAudioPath.TabIndex = 33;
            this.btn_ChangeAudioPath.TabStop = false;
            this.btn_ChangeAudioPath.Zoom = 10;
            this.btn_ChangeAudioPath.Click += new System.EventHandler(this.Btn_ChangeAudioPath_Click);
            // 
            // btn_OpenSaveFolder
            // 
            this.btn_OpenSaveFolder.BackColor = System.Drawing.Color.Transparent;
            this.btn_OpenSaveFolder.Image = global::Artco.Properties.Resources.OpenDirectory;
            this.btn_OpenSaveFolder.ImageActive = null;
            this.btn_OpenSaveFolder.Location = new System.Drawing.Point(295, 316);
            this.btn_OpenSaveFolder.Name = "btn_OpenSaveFolder";
            this.btn_OpenSaveFolder.Size = new System.Drawing.Size(28, 28);
            this.btn_OpenSaveFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_OpenSaveFolder.TabIndex = 33;
            this.btn_OpenSaveFolder.TabStop = false;
            this.btn_OpenSaveFolder.Zoom = 10;
            this.btn_OpenSaveFolder.Click += new System.EventHandler(this.Btn_OpenSaveFolder_Click);
            // 
            // btn_ChangeVideoPath
            // 
            this.btn_ChangeVideoPath.BackColor = System.Drawing.Color.Transparent;
            this.btn_ChangeVideoPath.Image = global::Artco.Properties.Resources.OpenDirectory;
            this.btn_ChangeVideoPath.ImageActive = null;
            this.btn_ChangeVideoPath.Location = new System.Drawing.Point(295, 365);
            this.btn_ChangeVideoPath.Name = "btn_ChangeVideoPath";
            this.btn_ChangeVideoPath.Size = new System.Drawing.Size(28, 28);
            this.btn_ChangeVideoPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_ChangeVideoPath.TabIndex = 33;
            this.btn_ChangeVideoPath.TabStop = false;
            this.btn_ChangeVideoPath.Zoom = 10;
            this.btn_ChangeVideoPath.Click += new System.EventHandler(this.Btn_ChangeVideoPath_Click);
            // 
            // cmbbox_Resolution
            // 
            this.cmbbox_Resolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_Resolution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbbox_Resolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbbox_Resolution.FormattingEnabled = true;
            this.cmbbox_Resolution.Location = new System.Drawing.Point(492, 156);
            this.cmbbox_Resolution.Name = "cmbbox_Resolution";
            this.cmbbox_Resolution.Size = new System.Drawing.Size(110, 21);
            this.cmbbox_Resolution.TabIndex = 10;
            this.cmbbox_Resolution.SelectedIndexChanged += new System.EventHandler(this.Cmbbox_Resolution_SelectedIndexChanged);
            // 
            // cmbbox_Theme
            // 
            this.cmbbox_Theme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_Theme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbbox_Theme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbbox_Theme.FormattingEnabled = true;
            this.cmbbox_Theme.Items.AddRange(new object[] {
            "Blue",
            "Yellow",
            "Black",
            "Red"});
            this.cmbbox_Theme.Location = new System.Drawing.Point(492, 197);
            this.cmbbox_Theme.Name = "cmbbox_Theme";
            this.cmbbox_Theme.Size = new System.Drawing.Size(110, 21);
            this.cmbbox_Theme.TabIndex = 13;
            this.cmbbox_Theme.SelectedIndexChanged += new System.EventHandler(this.Cmbbox_Theme_SelectedIndexChanged);
            // 
            // cmbbox_InputDevices
            // 
            this.cmbbox_InputDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_InputDevices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbbox_InputDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbbox_InputDevices.FormattingEnabled = true;
            this.cmbbox_InputDevices.Location = new System.Drawing.Point(492, 320);
            this.cmbbox_InputDevices.Name = "cmbbox_InputDevices";
            this.cmbbox_InputDevices.Size = new System.Drawing.Size(110, 21);
            this.cmbbox_InputDevices.TabIndex = 35;
            this.cmbbox_InputDevices.SelectedIndexChanged += new System.EventHandler(this.Cmbbox_InputDevices_SelectedIndexChanged);
            // 
            // cmbbox_AudioFormat
            // 
            this.cmbbox_AudioFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_AudioFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbbox_AudioFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbbox_AudioFormat.FormattingEnabled = true;
            this.cmbbox_AudioFormat.Items.AddRange(new object[] {
            "wav",
            "mp3"});
            this.cmbbox_AudioFormat.Location = new System.Drawing.Point(492, 357);
            this.cmbbox_AudioFormat.Name = "cmbbox_AudioFormat";
            this.cmbbox_AudioFormat.Size = new System.Drawing.Size(110, 21);
            this.cmbbox_AudioFormat.TabIndex = 39;
            // 
            // cmbbox_SampleRate
            // 
            this.cmbbox_SampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_SampleRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbbox_SampleRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbbox_SampleRate.FormattingEnabled = true;
            this.cmbbox_SampleRate.Items.AddRange(new object[] {
            "8000"});
            this.cmbbox_SampleRate.Location = new System.Drawing.Point(492, 393);
            this.cmbbox_SampleRate.Name = "cmbbox_SampleRate";
            this.cmbbox_SampleRate.Size = new System.Drawing.Size(110, 21);
            this.cmbbox_SampleRate.TabIndex = 41;
            // 
            // cmbbox_Channels
            // 
            this.cmbbox_Channels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_Channels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbbox_Channels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbbox_Channels.FormattingEnabled = true;
            this.cmbbox_Channels.Items.AddRange(new object[] {
            "Mono"});
            this.cmbbox_Channels.Location = new System.Drawing.Point(492, 430);
            this.cmbbox_Channels.Name = "cmbbox_Channels";
            this.cmbbox_Channels.Size = new System.Drawing.Size(110, 21);
            this.cmbbox_Channels.TabIndex = 43;
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_UserName.Location = new System.Drawing.Point(152, 151);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(60, 20);
            this.lbl_UserName.TabIndex = 1;
            this.lbl_UserName.Text = "label14";
            // 
            // lbl_SerialCode
            // 
            this.lbl_SerialCode.AutoSize = true;
            this.lbl_SerialCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_SerialCode.Location = new System.Drawing.Point(152, 194);
            this.lbl_SerialCode.Name = "lbl_SerialCode";
            this.lbl_SerialCode.Size = new System.Drawing.Size(60, 20);
            this.lbl_SerialCode.TabIndex = 1;
            this.lbl_SerialCode.Text = "label14";
            // 
            // pnl_Setting
            // 
            this.pnl_Setting.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Setting.BackgroundImage = global::Artco.Properties.Resources.setting_1Page;
            this.pnl_Setting.Controls.Add(this.btn_Close);
            this.pnl_Setting.Controls.Add(this.cmbbox_Theme);
            this.pnl_Setting.Controls.Add(this.cmbbox_Channels);
            this.pnl_Setting.Controls.Add(this.cmbbox_SampleRate);
            this.pnl_Setting.Controls.Add(this.lbl_UserName);
            this.pnl_Setting.Controls.Add(this.btn_ChangeVideoPath);
            this.pnl_Setting.Controls.Add(this.cmbbox_Resolution);
            this.pnl_Setting.Controls.Add(this.cmbbox_AudioFormat);
            this.pnl_Setting.Controls.Add(this.lbl_SerialCode);
            this.pnl_Setting.Controls.Add(this.cmbbox_InputDevices);
            this.pnl_Setting.Controls.Add(this.btn_OpenSaveFolder);
            this.pnl_Setting.Controls.Add(this.btn_ChangeAudioPath);
            this.pnl_Setting.Controls.Add(this.txtbox_AudioPath);
            this.pnl_Setting.Controls.Add(this.txtbox_VideoPath);
            this.pnl_Setting.Controls.Add(this.txtbox_SaveFolder);
            this.pnl_Setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.pnl_Setting.Location = new System.Drawing.Point(0, 0);
            this.pnl_Setting.Name = "pnl_Setting";
            this.pnl_Setting.Size = new System.Drawing.Size(710, 500);
            this.pnl_Setting.TabIndex = 44;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Image = global::Artco.Properties.Resources.Close;
            this.btn_Close.ImageActive = null;
            this.btn_Close.Location = new System.Drawing.Point(671, 8);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(29, 28);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Close.TabIndex = 44;
            this.btn_Close.TabStop = false;
            this.btn_Close.Zoom = 10;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::Artco.Properties.Resources.setting_BG;
            this.ClientSize = new System.Drawing.Size(710, 500);
            this.Controls.Add(this.pnl_Setting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_ChangeAudioPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_OpenSaveFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ChangeVideoPath)).EndInit();
            this.pnl_Setting.ResumeLayout(false);
            this.pnl_Setting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtbox_VideoPath;
        private System.Windows.Forms.TextBox txtbox_SaveFolder;
        private System.Windows.Forms.TextBox txtbox_AudioPath;
        private Bunifu.Framework.UI.BunifuImageButton btn_ChangeAudioPath;
        private Bunifu.Framework.UI.BunifuImageButton btn_OpenSaveFolder;
        private Bunifu.Framework.UI.BunifuImageButton btn_ChangeVideoPath;
        private System.Windows.Forms.ComboBox cmbbox_Resolution;
        private System.Windows.Forms.ComboBox cmbbox_Theme;
        private System.Windows.Forms.ComboBox cmbbox_InputDevices;
        private System.Windows.Forms.ComboBox cmbbox_AudioFormat;
        private System.Windows.Forms.ComboBox cmbbox_SampleRate;
        private System.Windows.Forms.ComboBox cmbbox_Channels;
        private System.Windows.Forms.Label lbl_UserName;
        private System.Windows.Forms.Label lbl_SerialCode;
        private DoubleBufferedPanel pnl_Setting;
        private Bunifu.Framework.UI.BunifuImageButton btn_Close;
    }
}