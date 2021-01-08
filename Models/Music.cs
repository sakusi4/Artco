using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

namespace Artco
{
    public class Music
    {
        public static List<List<Music>> musics = new List<List<Music>>();
        public static SoundPlayer preview_player;
        public static SoundPlayer cur_music = null;
        public static bool is_play_bgm = true;

        public string name;
        public string local_path;

        public Music(string name, string path)
        {
            this.name = name;
            this.local_path = path;
        }

        public static void SetMusic(string path)
        {
            cur_music = new SoundPlayer(path);
        }

        public static void PlayBackMusic()
        {
            if (MainForm.stage_player == null || cur_music == null || !is_play_bgm)
                return;

            try {
                cur_music?.PlayLooping();
            } catch (Exception) {
                return;
            }
        }

        public static void StopBackMusic()
        {
            cur_music?.Stop();
        }

        public static void ReleaseMusic()
        {
            cur_music?.Stop();
            cur_music?.Dispose();
            cur_music = null;
        }

        public static void AddMusicData(string[] datas)
        {
            const int row_cnt = 5;
            for (int i = 0; i <= datas.Length - row_cnt; i += row_cnt) {
                string name = (Setting.language.Equals("Korean")) ? datas[i] : datas[i + 1];
                int category = int.Parse(datas[i + 2]);
                //int idx = int.Parse(datas[i + 3]);
                string path = "./" + Path.GetDirectoryName(datas[i + 4]) + "/" +
                    Convert.ToBase64String(Encoding.Unicode.GetBytes(Path.GetFileNameWithoutExtension(datas[i + 4]))) + ".artcowav";

                for (; category >= musics.Count;)
                    musics.Add(new List<Music>());

                musics[category].Add(new Music(name, path));
            }

            musics.Add(new List<Music>()); // user tab
            DirectoryInfo di = new DirectoryInfo(Setting.user_music_path);
            if (!di.Exists)
                di.Create();

            foreach (var file in di.GetFiles()) {
                if (file.Extension.Equals(".wav")) {
                    string only_name = file.Name.Substring(0, file.Name.Length - 4);
                    string local_path = file.FullName;
                    musics[musics.Count - 1].Add(new Music(only_name, local_path));
                }
            }
        }
    }
}