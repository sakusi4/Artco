
using System;
using System.Windows.Forms;

namespace Artco
{
    partial class SpriteStorageForm
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
            this.btn_Close = new Bunifu.Framework.UI.BunifuImageButton();
            this.txtbox_Search = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.pnl_Contents = new Artco.DoubleBufferedPanel();
            this.pnl_Tabs = new Artco.DoubleBufferedFlowPanel();
            this.btn_Tab_0 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_2 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_3 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_4 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_5 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_6 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_7 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_8 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_9 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Tab_10 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_OpenUserFile = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.pnl_Tabs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_OpenUserFile)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Image = global::Artco.Properties.Resources.Close;
            this.btn_Close.ImageActive = null;
            this.btn_Close.Location = new System.Drawing.Point(1378, 13);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(29, 28);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Close.TabIndex = 7;
            this.btn_Close.TabStop = false;
            this.btn_Close.Zoom = 10;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // txtbox_Search
            // 
            this.txtbox_Search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbox_Search.Font = new System.Drawing.Font("Gulim", 15F);
            this.txtbox_Search.Location = new System.Drawing.Point(32, 26);
            this.txtbox_Search.MaxLength = 20;
            this.txtbox_Search.Name = "txtbox_Search";
            this.txtbox_Search.Size = new System.Drawing.Size(118, 23);
            this.txtbox_Search.TabIndex = 14;
            this.txtbox_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txtbox_Search_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Artco.Properties.Resources.Storage_Search;
            this.pictureBox1.Location = new System.Drawing.Point(4, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 33);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 900);
            this.panel1.TabIndex = 63;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.bunifuImageButton1);
            this.panel2.Controls.Add(this.btn_Close);
            this.panel2.Controls.Add(this.txtbox_Search);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1420, 57);
            this.panel2.TabIndex = 64;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.Image = global::Artco.Properties.Resources.Close;
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(1378, 13);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(29, 28);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 7;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 1;
            this.bunifuImageButton1.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // pnl_Contents
            // 
            this.pnl_Contents.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Contents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Contents.Location = new System.Drawing.Point(20, 113);
            this.pnl_Contents.Name = "pnl_Contents";
            this.pnl_Contents.Size = new System.Drawing.Size(1420, 787);
            this.pnl_Contents.TabIndex = 65;
            // 
            // pnl_Tabs
            // 
            this.pnl_Tabs.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Tabs.Controls.Add(this.btn_Tab_0);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_1);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_2);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_3);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_4);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_5);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_6);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_7);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_8);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_9);
            this.pnl_Tabs.Controls.Add(this.btn_Tab_10);
            this.pnl_Tabs.Controls.Add(this.btn_OpenUserFile);
            this.pnl_Tabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Tabs.Location = new System.Drawing.Point(20, 57);
            this.pnl_Tabs.Name = "pnl_Tabs";
            this.pnl_Tabs.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pnl_Tabs.Size = new System.Drawing.Size(1420, 56);
            this.pnl_Tabs.TabIndex = 15;
            this.pnl_Tabs.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_Tabs_Paint);
            // 
            // btn_Tab_0
            // 
            this.btn_Tab_0.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_0.Image = global::Artco.Properties.Resources.User_tab;
            this.btn_Tab_0.ImageActive = null;
            this.btn_Tab_0.Location = new System.Drawing.Point(3, 8);
            this.btn_Tab_0.Name = "btn_Tab_0";
            this.btn_Tab_0.Size = new System.Drawing.Size(120, 40);
            this.btn_Tab_0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_Tab_0.TabIndex = 54;
            this.btn_Tab_0.TabStop = false;
            this.btn_Tab_0.Tag = "0";
            this.btn_Tab_0.Zoom = 0;
            this.btn_Tab_0.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_1
            // 
            this.btn_Tab_1.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_1.Image = global::Artco.Properties.Resources.Momochung_Tab;
            this.btn_Tab_1.ImageActive = null;
            this.btn_Tab_1.Location = new System.Drawing.Point(129, 8);
            this.btn_Tab_1.Name = "btn_Tab_1";
            this.btn_Tab_1.Size = new System.Drawing.Size(120, 40);
            this.btn_Tab_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_Tab_1.TabIndex = 55;
            this.btn_Tab_1.TabStop = false;
            this.btn_Tab_1.Tag = "1";
            this.btn_Tab_1.Zoom = 0;
            this.btn_Tab_1.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_2
            // 
            this.btn_Tab_2.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_2.Image = global::Artco.Properties.Resources.PS_sea_tab;
            this.btn_Tab_2.ImageActive = null;
            this.btn_Tab_2.Location = new System.Drawing.Point(255, 8);
            this.btn_Tab_2.Name = "btn_Tab_2";
            this.btn_Tab_2.Size = new System.Drawing.Size(120, 40);
            this.btn_Tab_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_Tab_2.TabIndex = 56;
            this.btn_Tab_2.TabStop = false;
            this.btn_Tab_2.Tag = "2";
            this.btn_Tab_2.Zoom = 0;
            this.btn_Tab_2.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_3
            // 
            this.btn_Tab_3.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_3.Image = global::Artco.Properties.Resources.PS_animal_tab;
            this.btn_Tab_3.ImageActive = null;
            this.btn_Tab_3.Location = new System.Drawing.Point(381, 8);
            this.btn_Tab_3.Name = "btn_Tab_3";
            this.btn_Tab_3.Size = new System.Drawing.Size(70, 40);
            this.btn_Tab_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Tab_3.TabIndex = 57;
            this.btn_Tab_3.TabStop = false;
            this.btn_Tab_3.Tag = "3";
            this.btn_Tab_3.Zoom = 0;
            this.btn_Tab_3.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_4
            // 
            this.btn_Tab_4.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_4.Image = global::Artco.Properties.Resources.PS_plants_tab;
            this.btn_Tab_4.ImageActive = null;
            this.btn_Tab_4.Location = new System.Drawing.Point(457, 8);
            this.btn_Tab_4.Name = "btn_Tab_4";
            this.btn_Tab_4.Size = new System.Drawing.Size(70, 40);
            this.btn_Tab_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Tab_4.TabIndex = 58;
            this.btn_Tab_4.TabStop = false;
            this.btn_Tab_4.Tag = "4";
            this.btn_Tab_4.Zoom = 0;
            this.btn_Tab_4.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_5
            // 
            this.btn_Tab_5.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_5.Image = global::Artco.Properties.Resources.PS_insect_tab;
            this.btn_Tab_5.ImageActive = null;
            this.btn_Tab_5.Location = new System.Drawing.Point(533, 8);
            this.btn_Tab_5.Name = "btn_Tab_5";
            this.btn_Tab_5.Size = new System.Drawing.Size(70, 40);
            this.btn_Tab_5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Tab_5.TabIndex = 59;
            this.btn_Tab_5.TabStop = false;
            this.btn_Tab_5.Tag = "5";
            this.btn_Tab_5.Zoom = 0;
            this.btn_Tab_5.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_6
            // 
            this.btn_Tab_6.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_6.Image = global::Artco.Properties.Resources.PS_character_tab;
            this.btn_Tab_6.ImageActive = null;
            this.btn_Tab_6.Location = new System.Drawing.Point(609, 8);
            this.btn_Tab_6.Name = "btn_Tab_6";
            this.btn_Tab_6.Size = new System.Drawing.Size(70, 40);
            this.btn_Tab_6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Tab_6.TabIndex = 60;
            this.btn_Tab_6.TabStop = false;
            this.btn_Tab_6.Tag = "6";
            this.btn_Tab_6.Zoom = 0;
            this.btn_Tab_6.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_7
            // 
            this.btn_Tab_7.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_7.Image = global::Artco.Properties.Resources.PS_food_tab;
            this.btn_Tab_7.ImageActive = null;
            this.btn_Tab_7.Location = new System.Drawing.Point(685, 8);
            this.btn_Tab_7.Name = "btn_Tab_7";
            this.btn_Tab_7.Size = new System.Drawing.Size(70, 40);
            this.btn_Tab_7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Tab_7.TabIndex = 61;
            this.btn_Tab_7.TabStop = false;
            this.btn_Tab_7.Tag = "7";
            this.btn_Tab_7.Zoom = 0;
            this.btn_Tab_7.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_8
            // 
            this.btn_Tab_8.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_8.Image = global::Artco.Properties.Resources.PS_traffic_tab;
            this.btn_Tab_8.ImageActive = null;
            this.btn_Tab_8.Location = new System.Drawing.Point(761, 8);
            this.btn_Tab_8.Name = "btn_Tab_8";
            this.btn_Tab_8.Size = new System.Drawing.Size(70, 40);
            this.btn_Tab_8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Tab_8.TabIndex = 62;
            this.btn_Tab_8.TabStop = false;
            this.btn_Tab_8.Tag = "8";
            this.btn_Tab_8.Zoom = 0;
            this.btn_Tab_8.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_9
            // 
            this.btn_Tab_9.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_9.Image = global::Artco.Properties.Resources.PS_object_tab;
            this.btn_Tab_9.ImageActive = null;
            this.btn_Tab_9.Location = new System.Drawing.Point(837, 8);
            this.btn_Tab_9.Name = "btn_Tab_9";
            this.btn_Tab_9.Size = new System.Drawing.Size(70, 40);
            this.btn_Tab_9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Tab_9.TabIndex = 64;
            this.btn_Tab_9.TabStop = false;
            this.btn_Tab_9.Tag = "9";
            this.btn_Tab_9.Zoom = 0;
            this.btn_Tab_9.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_Tab_10
            // 
            this.btn_Tab_10.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tab_10.Image = global::Artco.Properties.Resources.National_studies_tab;
            this.btn_Tab_10.ImageActive = null;
            this.btn_Tab_10.Location = new System.Drawing.Point(913, 8);
            this.btn_Tab_10.Name = "btn_Tab_10";
            this.btn_Tab_10.Size = new System.Drawing.Size(120, 40);
            this.btn_Tab_10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_Tab_10.TabIndex = 63;
            this.btn_Tab_10.TabStop = false;
            this.btn_Tab_10.Tag = "10";
            this.btn_Tab_10.Zoom = 0;
            this.btn_Tab_10.Click += new System.EventHandler(this.Btn_Tab_Click);
            // 
            // btn_OpenUserFile
            // 
            this.btn_OpenUserFile.BackColor = System.Drawing.Color.Transparent;
            this.btn_OpenUserFile.Image = global::Artco.Properties.Resources.Storage_FileButton;
            this.btn_OpenUserFile.ImageActive = null;
            this.btn_OpenUserFile.Location = new System.Drawing.Point(1039, 8);
            this.btn_OpenUserFile.Name = "btn_OpenUserFile";
            this.btn_OpenUserFile.Size = new System.Drawing.Size(40, 40);
            this.btn_OpenUserFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_OpenUserFile.TabIndex = 62;
            this.btn_OpenUserFile.TabStop = false;
            this.btn_OpenUserFile.Zoom = 5;
            this.btn_OpenUserFile.Click += new System.EventHandler(this.Btn_OpenUserFile_Click);
            // 
            // SpriteStorageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::Artco.Properties.Resources._2Picture_Storage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.pnl_Contents);
            this.Controls.Add(this.pnl_Tabs);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SpriteStorageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StorageForm";
            this.Load += new System.EventHandler(this.StorageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.pnl_Tabs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Tab_10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_OpenUserFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuImageButton btn_Close;
        private System.Windows.Forms.TextBox txtbox_Search;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DoubleBufferedFlowPanel pnl_Tabs;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_0;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_1;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_2;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_3;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_4;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_5;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_6;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_7;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_8;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_10;
        private Bunifu.Framework.UI.BunifuImageButton btn_Tab_9;
        private Bunifu.Framework.UI.BunifuImageButton btn_OpenUserFile;
        private Panel panel1;
        private Panel panel2;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private DoubleBufferedPanel pnl_Contents;
    }
}