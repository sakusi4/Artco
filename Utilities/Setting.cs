using NAudio.Wave;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Artco
{
    internal static class Setting
    {
        /* General */
        public static string user_name { get; set; }
        public static string serial_num { get; set; }
        public static string language { get; set; }
        public static List<string> resolution_list { get; set; }

        /* Audio */
        public static IEnumerable<WaveInCapabilities> devices { get; set; }
        public static WaveInCapabilities device { get; set; }
        public static int sample_rate { get; set; }
        public static int channels { get; set; }
        public static int device_number { get; set; }
        public static string user_sound_path { get; set; }
        public static string user_back_path { get; set; }
        public static string user_music_path { get; set; }

        /* Video */
        public static string video_path { get; set; }
        public static int video_fps { get; set; }
        public static string save_path { get; set; }

        static Setting()
        {
            resolution_list = new List<string> {
                "1920x1080",
                "1600x900",
                "1440x900",
                "1280x720"
            };

            devices = Enumerable.Range(-1, WaveIn.DeviceCount + 1).Select(n => WaveIn.GetCapabilities(n)).ToArray();
            if (devices.ToArray().Length > 0) {
                device = (devices.ToArray())[0];
                sample_rate = 8000;
                channels = 1; // mono, 2:Stereo
                device_number = -1;
            }

            video_path = Application.LocalUserAppDataPath + "\\videos";
            user_sound_path = Application.LocalUserAppDataPath + "\\sounds";
            save_path = Application.LocalUserAppDataPath + "\\saves";
            user_music_path = Application.LocalUserAppDataPath + "\\bgms";
            user_back_path = Application.LocalUserAppDataPath + "\\backgrounds";
            video_fps = 30;

            if (!Directory.Exists(video_path))
                Directory.CreateDirectory(video_path);

            if (!Directory.Exists(user_sound_path))
                Directory.CreateDirectory(user_sound_path);

            if (!Directory.Exists(save_path))
                Directory.CreateDirectory(save_path);

            if (!Directory.Exists(user_music_path))
                Directory.CreateDirectory(user_music_path);

            if (!Directory.Exists(user_back_path))
                Directory.CreateDirectory(user_back_path);

        }
    }
}