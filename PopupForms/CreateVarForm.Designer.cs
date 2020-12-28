namespace Artco
{
        partial class CreateVarForm
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
            this.btn_Close = new Bunifu.Framework.UI.BunifuImageButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Cancel = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_OK = new Bunifu.Framework.UI.BunifuImageButton();
            this.txtbox_VarName = new System.Windows.Forms.TextBox();
            this.txtbox_VarInitVal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Cancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_OK)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Image = global::Artco.Properties.Resources.login_close;
            this.btn_Close.ImageActive = null;
            this.btn_Close.Location = new System.Drawing.Point(305, 4);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(25, 25);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Close.TabIndex = 17;
            this.btn_Close.TabStop = false;
            this.btn_Close.Zoom = 5;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Artco.Properties.Resources.VarFormInput;
            this.pictureBox1.Location = new System.Drawing.Point(128, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(169, 88);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_Cancel.Image = global::Artco.Properties.Resources.VarFormCancel;
            this.btn_Cancel.ImageActive = null;
            this.btn_Cancel.Location = new System.Drawing.Point(218, 154);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(79, 33);
            this.btn_Cancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Cancel.TabIndex = 0;
            this.btn_Cancel.TabStop = false;
            this.btn_Cancel.Zoom = 10;
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.Transparent;
            this.btn_OK.Image = global::Artco.Properties.Resources.VarFormOK;
            this.btn_OK.ImageActive = null;
            this.btn_OK.Location = new System.Drawing.Point(133, 154);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(79, 33);
            this.btn_OK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_OK.TabIndex = 0;
            this.btn_OK.TabStop = false;
            this.btn_OK.Zoom = 10;
            this.btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // txtbox_VarName
            // 
            this.txtbox_VarName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_VarName.ForeColor = System.Drawing.Color.DimGray;
            this.txtbox_VarName.Location = new System.Drawing.Point(135, 56);
            this.txtbox_VarName.Name = "txtbox_VarName";
            this.txtbox_VarName.Size = new System.Drawing.Size(154, 14);
            this.txtbox_VarName.TabIndex = 18;
            // 
            // txtbox_VarInitVal
            // 
            this.txtbox_VarInitVal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_VarInitVal.ForeColor = System.Drawing.Color.DimGray;
            this.txtbox_VarInitVal.Location = new System.Drawing.Point(135, 107);
            this.txtbox_VarInitVal.Name = "txtbox_VarInitVal";
            this.txtbox_VarInitVal.Size = new System.Drawing.Size(154, 14);
            this.txtbox_VarInitVal.TabIndex = 19;
            this.txtbox_VarInitVal.Text = "0";
            // 
            // CreateVarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Artco.Properties.Resources.CreateVarForm;
            this.ClientSize = new System.Drawing.Size(333, 199);
            this.Controls.Add(this.txtbox_VarInitVal);
            this.Controls.Add(this.txtbox_VarName);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateVarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateVarForm";
            this.Load += new System.EventHandler(this.CreateVarForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlTitleMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PnlTitleMouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Cancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_OK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

                }

        #endregion

        private Bunifu.Framework.UI.BunifuImageButton btn_OK;
        private Bunifu.Framework.UI.BunifuImageButton btn_Cancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuImageButton btn_Close;
        private System.Windows.Forms.TextBox txtbox_VarName;
        private System.Windows.Forms.TextBox txtbox_VarInitVal;
    }
}