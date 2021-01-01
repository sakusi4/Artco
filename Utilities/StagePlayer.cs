using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Artco
{
    public class StagePlayer
    {
        public enum Flag
        {
            None = 0,
            LOADING = 1 << 0,
            PLAYING = 1 << 1,
            GAME = 1 << 2,
            RECORDING = 1 << 3,
            FULLSCREEN = 1 << 4,
            NOREPEAT = 1 << 5,
        }

        private VideoCapture _capture;
        private readonly Action<Image> _draw_function;
        private readonly Action _complete_function;
        private Background _cur_back;
        private Background _start_back;
        private bool _is_stop;

        public static Flag stage_flag { get; set; }
        public Thread stage_player_thread { get; set; }

        public static void SetFlagZero()
        {
            stage_flag = Flag.None;
        }

        public static void SetFlags(params Flag[] flag)
        {
            for (int i = 0; i < flag.Length; i++)
                stage_flag |= flag[i];
        }

        public static void UnsetFlag(params Flag[] flag)
        {
            for (int i = 0; i < flag.Length; i++)
                stage_flag &= ~flag[i];
        }

        public static bool ORCheckFlags(params Flag[] flag)
        {
            for (int i = 0; i < flag.Length; i++) {
                if (stage_flag.HasFlag(flag[i]))
                    return true;
            }

            return false;
        }

        public StagePlayer(Action<Image> draw_function, Action complete_function)
        {
            _draw_function = draw_function;
            _complete_function = complete_function;
        }

        public void SetBackground(Background back)
        {
            _cur_back = back;
        }

        public Background GetBackground()
        {
            return _cur_back;
        }

        public void SaveStartBackground()
        {
            if (_cur_back != null)
                _start_back = _cur_back;
        }

        public void LoadStartBackground()
        {
            if (_start_back != null)
                _cur_back = _start_back;

            _start_back = null;
        }

        public void SetInitializeImage()
        {
            VideoCapture video_capture = new VideoCapture(_cur_back.background_path);
            if (!video_capture.IsOpened())
                return;

            using Mat mat = new Mat();
            video_capture.Read(mat);
            if (mat.Empty())
                return;

            _draw_function(mat.ToBitmap());
        }

        public void Start()
        {
            _is_stop = false;
            _capture = new VideoCapture(_cur_back.background_path);

            if (!_capture.IsOpened())
                return;


            stage_player_thread = new Thread(RunPlayerThreadStart) { IsBackground = true };
            stage_player_thread.Start();
        }

        public void Stop()
        {
            lock (this) {
                _is_stop = true;
            }
        }

        public void RunPlayerThreadStart()
        {
            int fps = (int)_capture.Fps;
            int expected_process_time_per_frame = 1000 / fps;
            Stopwatch st = new Stopwatch();
            st.Start();

            Mat frame = new Mat();
            while (true) {
                lock (this) {
                    if (_is_stop)
                        break;
                }

                long started = st.ElapsedMilliseconds;

                _capture.Read(frame);
                if (frame.Empty()) {
                    if (ORCheckFlags(Flag.NOREPEAT))
                        break;

                    _capture.Set(VideoCaptureProperties.PosAviRatio, 0);
                    continue;
                }

                _draw_function(frame.ToBitmap());

                int elapsed = (int)(st.ElapsedMilliseconds - started);
                int delay = expected_process_time_per_frame - elapsed;
                Thread.Sleep((delay > 0) ? delay : 30);
            }

            _capture.Dispose();
            frame.Dispose();
            _complete_function?.Invoke();
        }

        public static void SetHomeImage()
        {
            MainForm.draw_background(Properties.Resources.HomeImage);
        }

        public static (double, double) GetFullScreenSizeFactor()
        {
            return (1.9238, 1.9439);
        }

        public static (double, double) GetNormalScreenSizeFactor()
        {
            return (0.5197, 0.5144);
        }

        public static (double, double) GetScreenSizeRate()
        {
            var (full_x, full_y) = GetFullScreenSizeFactor();
            var (norml_x, norml_y) = GetNormalScreenSizeFactor();

            double rate_x = (ORCheckFlags(Flag.FULLSCREEN)) ? full_x : norml_x;
            double rate_y = (ORCheckFlags(Flag.FULLSCREEN)) ? full_y : norml_y;

            return (rate_x, rate_y);
        }
    }
}