﻿using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Artco
{
    public static class VideoRecord
    {
        private static System.Timers.Timer _timer;
        private static Process _prc_ffmpeg;
        private static string _video_name;
        private static int _sec;        

        public static void SetVideoName(string name)
        {
            _video_name = name;
            _timer = new System.Timers.Timer { Interval = 1000 };
            _timer.Elapsed += Timer_Elapsed;
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _sec++;
            if (_sec == 60)
                _sec = 0;
            
            MainForm.show_recording_btn((_sec % 2 != 0));
        }

        public static bool Start()
        {
            if (_video_name == null)
                return false;

            _prc_ffmpeg = new Process();
            ProcessStartInfo process_start_info = new ProcessStartInfo();

            string offset_x = MainForm.client_area.x.ToString();
            string offset_y = MainForm.client_area.y.ToString();
            
            string[] resols = {
                "1920x1008",
                "1600x834",
                "1440x834",
                "1280x660",               
            };
           
            string option = "\"" + Application.StartupPath + "\\ffmpeg.exe\"" + " -y -rtbufsize 200M -f gdigrab -offset_x " + offset_x + " -offset_y " + offset_y + " -video_size " + resols[Properties.Settings.Default.resolution] + " ";
            option += "-thread_queue_size 1024 -probesize 10M -r 30 -draw_mouse 1 -i desktop -f dshow -channel_layout stereo ";
            option += "-thread_queue_size 1024 -i audio=\"virtual-audio-capturer\" -c:v libx264 -r 30 -preset ultrafast -tune zerolatency -crf 25 ";
            option += "-pix_fmt yuv420p -c:a aac -strict -2 -ac 2 -b:a 128k ";
            option += "\"" + Setting.video_path + "\\temp.mp4\"";

            process_start_info.FileName = "cmd.exe";
            process_start_info.Arguments = option;
            process_start_info.UseShellExecute = false;
            process_start_info.CreateNoWindow = true;
            process_start_info.WindowStyle = ProcessWindowStyle.Hidden;
            process_start_info.RedirectStandardInput = true;

            _prc_ffmpeg.StartInfo = process_start_info;
            _prc_ffmpeg.Start();
            _prc_ffmpeg.StandardInput.WriteLine(option);
            
            _timer.Start();
            return true;
        }

        public static bool Stop()
        {
            if (_prc_ffmpeg == null)
                return false;

            _prc_ffmpeg.StandardInput.Write("q");
            _prc_ffmpeg.StandardInput.Close();
                   
            _timer.Stop();
            _sec = 0;
            
            Thread.Sleep(1000);
            _prc_ffmpeg?.Dispose();

            string temp_path = Setting.video_path + "\\temp.mp4";
            string new_path = Setting.video_path + "\\" + _video_name + ".mp4";
            if (File.Exists(temp_path))
                File.Move(temp_path, new_path);

            _video_name = null;
            return true;
        }
    }
}