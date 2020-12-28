namespace Artco
{
    partial class PracticeResultForm
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
            this.btn_NextStep = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Restart = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Finish = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.btn_NextStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Restart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Finish)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_NextStep
            // 
            this.btn_NextStep.BackColor = System.Drawing.Color.Transparent;
            this.btn_NextStep.Image = global::Artco.Properties.Resources.PracticeNext;
            this.btn_NextStep.ImageActive = null;
            this.btn_NextStep.Location = new System.Drawing.Point(177, 453);
            this.btn_NextStep.Name = "btn_NextStep";
            this.btn_NextStep.Size = new System.Drawing.Size(199, 67);
            this.btn_NextStep.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_NextStep.TabIndex = 3;
            this.btn_NextStep.TabStop = false;
            this.btn_NextStep.Zoom = 10;
            this.btn_NextStep.Click += new System.EventHandler(this.Btn_NextStep_Click);
            // 
            // btn_Restart
            // 
            this.btn_Restart.BackColor = System.Drawing.Color.Transparent;
            this.btn_Restart.Image = global::Artco.Properties.Resources.PracticeRepeat;
            this.btn_Restart.ImageActive = null;
            this.btn_Restart.Location = new System.Drawing.Point(403, 453);
            this.btn_Restart.Name = "btn_Restart";
            this.btn_Restart.Size = new System.Drawing.Size(199, 67);
            this.btn_Restart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Restart.TabIndex = 4;
            this.btn_Restart.TabStop = false;
            this.btn_Restart.Zoom = 10;
            this.btn_Restart.Click += new System.EventHandler(this.Btn_Restart_Click);
            // 
            // btn_Finish
            // 
            this.btn_Finish.BackColor = System.Drawing.Color.Transparent;
            this.btn_Finish.Image = global::Artco.Properties.Resources.PracticeFinish;
            this.btn_Finish.ImageActive = null;
            this.btn_Finish.Location = new System.Drawing.Point(628, 453);
            this.btn_Finish.Name = "btn_Finish";
            this.btn_Finish.Size = new System.Drawing.Size(199, 67);
            this.btn_Finish.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Finish.TabIndex = 5;
            this.btn_Finish.TabStop = false;
            this.btn_Finish.Zoom = 10;
            this.btn_Finish.Click += new System.EventHandler(this.Btn_Finish_Click);
            // 
            // PracticeResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::Artco.Properties.Resources.PracticeResultForm;
            this.ClientSize = new System.Drawing.Size(1000, 550);
            this.Controls.Add(this.btn_Finish);
            this.Controls.Add(this.btn_Restart);
            this.Controls.Add(this.btn_NextStep);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PracticeResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PracticeResultForm";
            ((System.ComponentModel.ISupportInitialize)(this.btn_NextStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Restart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Finish)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuImageButton btn_NextStep;
        private Bunifu.Framework.UI.BunifuImageButton btn_Restart;
        private Bunifu.Framework.UI.BunifuImageButton btn_Finish;
    }
}