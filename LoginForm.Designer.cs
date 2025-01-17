﻿namespace Artco
{
    partial class LoginForm
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
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.txtbox_userpass = new System.Windows.Forms.TextBox();
            this.txtbox_Username = new System.Windows.Forms.TextBox();
            this.pbx_RememberMe = new System.Windows.Forms.PictureBox();
            this.btn_Close = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Enter = new Bunifu.Framework.UI.BunifuImageButton();
            this.cmbbox_Resolution = new System.Windows.Forms.ComboBox();
            this.button_Signup = new Bunifu.Framework.UI.BunifuImageButton();
            this.button_Findidpw = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_RememberMe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Enter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.button_Signup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.button_Findidpw)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbox_userpass
            // 
            this.txtbox_userpass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_userpass.Font = new System.Drawing.Font("MS Gothic", 20F);
            this.txtbox_userpass.Location = new System.Drawing.Point(464, 410);
            this.txtbox_userpass.MaxLength = 15;
            this.txtbox_userpass.Name = "txtbox_userpass";
            this.txtbox_userpass.PasswordChar = '*';
            this.txtbox_userpass.Size = new System.Drawing.Size(192, 27);
            this.txtbox_userpass.TabIndex = 2;
            // 
            // txtbox_Username
            // 
            this.txtbox_Username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_Username.Font = new System.Drawing.Font("MS Gothic", 20F);
            this.txtbox_Username.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtbox_Username.Location = new System.Drawing.Point(464, 357);
            this.txtbox_Username.MaxLength = 15;
            this.txtbox_Username.Name = "txtbox_Username";
            this.txtbox_Username.Size = new System.Drawing.Size(192, 27);
            this.txtbox_Username.TabIndex = 1;
            // 
            // pbx_RememberMe
            // 
            this.pbx_RememberMe.BackColor = System.Drawing.Color.Transparent;
            this.pbx_RememberMe.Image = ((System.Drawing.Image)(resources.GetObject("pbx_RememberMe.Image")));
            this.pbx_RememberMe.Location = new System.Drawing.Point(420, 452);
            this.pbx_RememberMe.Name = "pbx_RememberMe";
            this.pbx_RememberMe.Size = new System.Drawing.Size(26, 25);
            this.pbx_RememberMe.TabIndex = 15;
            this.pbx_RememberMe.TabStop = false;
            this.pbx_RememberMe.Click += new System.EventHandler(this.Pbx_RememberMe_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Image = global::Artco.Properties.Resources.login_close;
            this.btn_Close.ImageActive = null;
            this.btn_Close.Location = new System.Drawing.Point(1115, 9);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(27, 27);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Close.TabIndex = 16;
            this.btn_Close.TabStop = false;
            this.btn_Close.Zoom = 10;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // btn_Enter
            // 
            this.btn_Enter.BackColor = System.Drawing.Color.Transparent;
            this.btn_Enter.Image = global::Artco.Properties.Resources.login_Button;
            this.btn_Enter.ImageActive = null;
            this.btn_Enter.Location = new System.Drawing.Point(420, 485);
            this.btn_Enter.Name = "btn_Enter";
            this.btn_Enter.Size = new System.Drawing.Size(311, 61);
            this.btn_Enter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Enter.TabIndex = 17;
            this.btn_Enter.TabStop = false;
            this.btn_Enter.Zoom = 5;
            this.btn_Enter.Click += new System.EventHandler(this.Btn_Enter_Click);
            // 
            // cmbbox_Resolution
            // 
            this.cmbbox_Resolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_Resolution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbbox_Resolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbbox_Resolution.FormattingEnabled = true;
            this.cmbbox_Resolution.Location = new System.Drawing.Point(1025, 12);
            this.cmbbox_Resolution.Name = "cmbbox_Resolution";
            this.cmbbox_Resolution.Size = new System.Drawing.Size(84, 21);
            this.cmbbox_Resolution.TabIndex = 18;
            // 
            // button_Signup
            // 
            this.button_Signup.BackColor = System.Drawing.Color.Transparent;
            this.button_Signup.Image = global::Artco.Properties.Resources.SignUpBtn;
            this.button_Signup.ImageActive = null;
            this.button_Signup.Location = new System.Drawing.Point(738, 517);
            this.button_Signup.Name = "button_Signup";
            this.button_Signup.Size = new System.Drawing.Size(147, 24);
            this.button_Signup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.button_Signup.TabIndex = 19;
            this.button_Signup.TabStop = false;
            this.button_Signup.Zoom = 0;
            this.button_Signup.Click += new System.EventHandler(this.Button_Signup_Click);
            // 
            // button_Findidpw
            // 
            this.button_Findidpw.BackColor = System.Drawing.Color.Transparent;
            this.button_Findidpw.Image = global::Artco.Properties.Resources.FindIDPWBtn;
            this.button_Findidpw.ImageActive = null;
            this.button_Findidpw.Location = new System.Drawing.Point(738, 490);
            this.button_Findidpw.Name = "button_Findidpw";
            this.button_Findidpw.Size = new System.Drawing.Size(147, 24);
            this.button_Findidpw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.button_Findidpw.TabIndex = 20;
            this.button_Findidpw.TabStop = false;
            this.button_Findidpw.Zoom = 0;
            this.button_Findidpw.Click += new System.EventHandler(this.Button_Findidpw_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::Artco.Properties.Resources.LoginForm;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1150, 680);
            this.Controls.Add(this.button_Findidpw);
            this.Controls.Add(this.button_Signup);
            this.Controls.Add(this.cmbbox_Resolution);
            this.Controls.Add(this.btn_Enter);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.pbx_RememberMe);
            this.Controls.Add(this.txtbox_Username);
            this.Controls.Add(this.txtbox_userpass);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Artco";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerSelectForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectServerForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LoginForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_RememberMe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Enter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.button_Signup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.button_Findidpw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtbox_userpass;
        private System.Windows.Forms.TextBox txtbox_Username;
        private System.Windows.Forms.PictureBox pbx_RememberMe;
        private Bunifu.Framework.UI.BunifuImageButton btn_Close;
        private Bunifu.Framework.UI.BunifuImageButton btn_Enter;
        private System.Windows.Forms.ComboBox cmbbox_Resolution;
        private Bunifu.Framework.UI.BunifuImageButton button_Signup;
        private Bunifu.Framework.UI.BunifuImageButton button_Findidpw;
    }
}