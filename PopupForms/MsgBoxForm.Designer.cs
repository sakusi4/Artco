namespace Artco
{
    partial class MsgBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBoxForm));
            this.pnl_Back = new System.Windows.Forms.Panel();
            this.txtbox_Text = new System.Windows.Forms.TextBox();
            this.btn_No = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Yes = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Ok = new Bunifu.Framework.UI.BunifuImageButton();
            this.pnl_Back.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_No)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Yes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Ok)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Back
            // 
            this.pnl_Back.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Back.Controls.Add(this.txtbox_Text);
            this.pnl_Back.Controls.Add(this.btn_Yes);
            this.pnl_Back.Controls.Add(this.btn_No);
            this.pnl_Back.Controls.Add(this.btn_Ok);
            this.pnl_Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Back.Location = new System.Drawing.Point(0, 0);
            this.pnl_Back.Name = "pnl_Back";
            this.pnl_Back.Size = new System.Drawing.Size(400, 202);
            this.pnl_Back.TabIndex = 5;
            // 
            // txtbox_Text
            // 
            this.txtbox_Text.BackColor = System.Drawing.Color.White;
            this.txtbox_Text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_Text.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtbox_Text.Font = new System.Drawing.Font("Magic R", 15F);
            this.txtbox_Text.Location = new System.Drawing.Point(57, 56);
            this.txtbox_Text.Multiline = true;
            this.txtbox_Text.Name = "txtbox_Text";
            this.txtbox_Text.ReadOnly = true;
            this.txtbox_Text.Size = new System.Drawing.Size(291, 51);
            this.txtbox_Text.TabIndex = 8;
            this.txtbox_Text.TabStop = false;
            this.txtbox_Text.Text = "Text";
            this.txtbox_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtbox_Text.WordWrap = false;
            // 
            // btn_No
            // 
            this.btn_No.BackColor = System.Drawing.Color.Transparent;
            this.btn_No.Image = ((System.Drawing.Image)(resources.GetObject("btn_No.Image")));
            this.btn_No.ImageActive = null;
            this.btn_No.Location = new System.Drawing.Point(149, 137);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(104, 44);
            this.btn_No.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_No.TabIndex = 5;
            this.btn_No.TabStop = false;
            this.btn_No.Visible = false;
            this.btn_No.Zoom = 10;
            this.btn_No.Click += new System.EventHandler(this.Btn_No_Click);
            // 
            // btn_Yes
            // 
            this.btn_Yes.BackColor = System.Drawing.Color.Transparent;
            this.btn_Yes.Image = ((System.Drawing.Image)(resources.GetObject("btn_Yes.Image")));
            this.btn_Yes.ImageActive = null;
            this.btn_Yes.Location = new System.Drawing.Point(268, 137);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(104, 44);
            this.btn_Yes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Yes.TabIndex = 6;
            this.btn_Yes.TabStop = false;
            this.btn_Yes.Visible = false;
            this.btn_Yes.Zoom = 10;
            this.btn_Yes.Click += new System.EventHandler(this.Btn_Yes_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.BackColor = System.Drawing.Color.Transparent;
            this.btn_Ok.Image = ((System.Drawing.Image)(resources.GetObject("btn_Ok.Image")));
            this.btn_Ok.ImageActive = null;
            this.btn_Ok.Location = new System.Drawing.Point(149, 137);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(104, 44);
            this.btn_Ok.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Ok.TabIndex = 7;
            this.btn_Ok.TabStop = false;
            this.btn_Ok.Visible = false;
            this.btn_Ok.Zoom = 10;
            this.btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // MsgBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 202);
            this.Controls.Add(this.pnl_Back);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MsgBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MsgBoxForm";
            this.Load += new System.EventHandler(this.MsgBoxForm_Load);
            this.pnl_Back.ResumeLayout(false);
            this.pnl_Back.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_No)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Yes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Ok)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Back;
        private System.Windows.Forms.TextBox txtbox_Text;
        private Bunifu.Framework.UI.BunifuImageButton btn_No;
        private Bunifu.Framework.UI.BunifuImageButton btn_Yes;
        private Bunifu.Framework.UI.BunifuImageButton btn_Ok;
    }
}