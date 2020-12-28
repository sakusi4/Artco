using NAudio.Wave;
using System;
using System.Media;
using System.Windows.Forms;

namespace Artco
{
    public partial class AudioRecordingForm : Form
    {
        private IWaveIn _capture_device;
        private WaveFileWriter _writer;
        private SoundPlayer _recorded_player;
        private string _audio_name;
        private bool _is_paused;

        public AudioRecordingForm()
        {
            InitializeComponent();
        }

        private void AudioRecordingForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());
            BackgroundImage = DynamicResources.b_recording_form;
        }

        private IWaveIn CreateWaveInDevice()
        {
            IWaveIn new_wave_in;
            new_wave_in = new WaveIn() {
                DeviceNumber = Setting.device_number
            };

            new_wave_in.WaveFormat = new WaveFormat(Setting.sample_rate, Setting.channels);

            new_wave_in.DataAvailable += OnDataAvailable;
            new_wave_in.RecordingStopped += OnRecordingStopped;

            return new_wave_in;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (InvokeRequired) {
                BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
            } else {
                _writer.Write(e.Buffer, 0, e.BytesRecorded);
                int seconds_recorded = (int)(_writer.Length / _writer.WaveFormat.AverageBytesPerSecond);

                lbl_Time.Text = "00:00:" + seconds_recorded.ToString();
                if (seconds_recorded >= 60) {
                    _capture_device?.StopRecording();
                    btn_StartRecording.Visible = true;
                    btn_Pause.Visible = false;
                }
            }
        }

        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            if (InvokeRequired) {
                BeginInvoke(new EventHandler<StoppedEventArgs>(OnRecordingStopped), sender, e);
            } else {
                FinalizeWaveFile();
            }
        }

        private void Btn_StartPlay_Click(object sender, EventArgs e)
        {
            if (_audio_name != null) {
                btn_StartRecording.Visible = false;

                _recorded_player = new SoundPlayer(Setting.user_sound_path + "/" + _audio_name + ".wav");
                _recorded_player.Play();
            }
        }

        private void Btn_StartRecording_Click(object sender, EventArgs e)
        {
            if (!_is_paused)
                Cleanup();
            else
                ResumeRecording();

            if (_capture_device == null) {
                btn_StartPlay.Visible = false;

                RenameSpriteForm rename_sprite_form = new RenameSpriteForm();

                if (rename_sprite_form.ShowDialog() == DialogResult.OK) {
                    _capture_device = CreateWaveInDevice();

                    _audio_name = rename_sprite_form.new_name;
                    _writer = new WaveFileWriter(Setting.user_sound_path + "/" + _audio_name + ".wav", _capture_device.WaveFormat);
                    _capture_device.StartRecording();

                    btn_StartRecording.Visible = false;
                    btn_Pause.Visible = true;
                }
            }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            if (_is_paused) {
                _capture_device.DataAvailable -= OnDataAvailable;
                _is_paused = false;
            }

            if (_capture_device != null) {
                _capture_device.StopRecording();
                _capture_device = null;

                btn_StartRecording.Visible = true;
                btn_Pause.Visible = false;

                new MsgBoxForm("录音完成").ShowDialog();

                lbl_Time.Text = "00:00:00";

                btn_StartPlay.Visible = true;
            }

            _recorded_player?.Stop();
            _recorded_player?.Dispose();
            _recorded_player = null;
            btn_StartRecording.Visible = true;
        }

        private void Btn_Pause_Click(object sender, EventArgs e)
        {
            PauseRecording();
        }

        private void PauseRecording()
        {
            if (_is_paused == false) {
                _capture_device.DataAvailable -= OnDataAvailable;
                _is_paused = true;

                btn_StartRecording.Visible = true;
                btn_Pause.Visible = false;
            }
        }

        private void ResumeRecording()
        {
            if (_is_paused == true) {
                _capture_device.DataAvailable += OnDataAvailable;
                _is_paused = false;

                btn_StartRecording.Visible = false;
                btn_Pause.Visible = true;
            }
        }

        private void Cleanup()
        {
            _capture_device?.Dispose();
            _capture_device = null;

            _recorded_player?.Dispose();
            _recorded_player = null;

            FinalizeWaveFile();
        }

        private void FinalizeWaveFile()
        {
            _writer?.Dispose();
            _writer = null;
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AudioRecordingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cleanup();
        }
    }
}