namespace Artco
{
    partial class AudioRecordingForm
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
            this.btn_StartPlay = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_StartRecording = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Stop = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Close = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Pause = new Bunifu.Framework.UI.BunifuImageButton();
            this.lbl_Time = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btn_StartPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_StartRecording)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Stop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Pause)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_StartPlay
            // 
            this.btn_StartPlay.BackColor = System.Drawing.Color.Transparent;
            this.btn_StartPlay.Image = global::Artco.Properties.Resources.RecordingForm_play;
            this.btn_StartPlay.ImageActive = null;
            this.btn_StartPlay.Location = new System.Drawing.Point(100, 246);
            this.btn_StartPlay.Name = "btn_StartPlay";
            this.btn_StartPlay.Size = new System.Drawing.Size(51, 51);
            this.btn_StartPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_StartPlay.TabIndex = 16;
            this.btn_StartPlay.TabStop = false;
            this.btn_StartPlay.Visible = false;
            this.btn_StartPlay.Zoom = 10;
            this.btn_StartPlay.Click += new System.EventHandler(this.Btn_StartPlay_Click);
            // 
            // btn_StartRecording
            // 
            this.btn_StartRecording.BackColor = System.Drawing.Color.Transparent;
            this.btn_StartRecording.Image = global::Artco.Properties.Resources.RecordingForm_recording;
            this.btn_StartRecording.ImageActive = null;
            this.btn_StartRecording.Location = new System.Drawing.Point(183, 246);
            this.btn_StartRecording.Name = "btn_StartRecording";
            this.btn_StartRecording.Size = new System.Drawing.Size(51, 51);
            this.btn_StartRecording.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_StartRecording.TabIndex = 16;
            this.btn_StartRecording.TabStop = false;
            this.btn_StartRecording.Zoom = 10;
            this.btn_StartRecording.Click += new System.EventHandler(this.Btn_StartRecording_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.BackColor = System.Drawing.Color.Transparent;
            this.btn_Stop.Image = global::Artco.Properties.Resources.RecordingForm_stop;
            this.btn_Stop.ImageActive = null;
            this.btn_Stop.Location = new System.Drawing.Point(265, 246);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(51, 51);
            this.btn_Stop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Stop.TabIndex = 16;
            this.btn_Stop.TabStop = false;
            this.btn_Stop.Zoom = 10;
            this.btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.btn_Close.Image = global::Artco.Properties.Resources.Close;
            this.btn_Close.ImageActive = null;
            this.btn_Close.Location = new System.Drawing.Point(389, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(20, 19);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Close.TabIndex = 17;
            this.btn_Close.TabStop = false;
            this.btn_Close.Zoom = 10;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // btn_Pause
            // 
            this.btn_Pause.BackColor = System.Drawing.Color.Transparent;
            this.btn_Pause.Image = global::Artco.Properties.Resources.RecordingForm_pause;
            this.btn_Pause.ImageActive = null;
            this.btn_Pause.Location = new System.Drawing.Point(183, 246);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(51, 51);
            this.btn_Pause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Pause.TabIndex = 18;
            this.btn_Pause.TabStop = false;
            this.btn_Pause.Visible = false;
            this.btn_Pause.Zoom = 10;
            this.btn_Pause.Click += new System.EventHandler(this.Btn_Pause_Click);
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Time.Font = new System.Drawing.Font("Gulim", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Time.Location = new System.Drawing.Point(206, 175);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(135, 29);
            this.lbl_Time.TabIndex = 19;
            this.lbl_Time.Text = "00:00:00";
            // 
            // AudioRecordingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(414, 328);
            this.Controls.Add(this.lbl_Time);
            this.Controls.Add(this.btn_Pause);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_StartRecording);
            this.Controls.Add(this.btn_StartPlay);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AudioRecordingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AudioRecordingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AudioRecordingForm_FormClosing);
            this.Load += new System.EventHandler(this.AudioRecordingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_StartPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_StartRecording)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Stop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Pause)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuImageButton btn_StartPlay;
        private Bunifu.Framework.UI.BunifuImageButton btn_StartRecording;
        private Bunifu.Framework.UI.BunifuImageButton btn_Stop;
        private Bunifu.Framework.UI.BunifuImageButton btn_Close;
        private Bunifu.Framework.UI.BunifuImageButton btn_Pause;
        private System.Windows.Forms.Label lbl_Time;
    }
}