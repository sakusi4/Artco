using System.Collections.Generic;
using System.IO;

namespace Artco
{
    public partial class Background
    {
        public static List<List<List<Background>>> backgrounds = new List<List<List<Background>>>() {
            new List<List<Background>>(), new List<List<Background>>()
        };

        public static int is_edu;
        public string name;
        public string background_path;
        public string preview_path;
        public int mode;
        public bool is_user;
        public int level;

        public Background(string name, int mode, int level,
                string background_path, string preview_path, bool is_user)
        {
            this.name = name;
            this.mode = mode;
            this.level = level;
            this.background_path = background_path;
            this.preview_path = preview_path;
            this.is_user = is_user;
        }

        public static void AddBackgroundData(string[] datas)
        {
            const int row_cnt = 4;
            for (int i = 0; i <= datas.Length - row_cnt; i += row_cnt) {
                string name = (Setting.language.Equals("Korean")) ? datas[i] : datas[i + 1];
                int category = int.Parse(datas[i + 2]) - 5;
#if (FREE)
                string background_path = FileManager.http_root_dir + "resource/free/" + datas[i + 3].Replace(" ", "%20") + ".mp4";
                string preview_path = FileManager.http_root_dir + "resource/free/" + datas[i + 3].Replace(" ", "%20") + ".jpg";
#else
                string background_path = "./" + datas[i + 3] + ".mp4";
                string preview_path = "./" + datas[i + 3] + ".jpg";
#endif
                for (; category >= backgrounds[0].Count;)
                    backgrounds[0].Add(new List<Background>());

                backgrounds[0][category].Add(new Background(name, 0, 0, background_path, preview_path, false));
            }

            backgrounds[0].Add(new List<Background>()); // user tab
            DirectoryInfo di = new DirectoryInfo(Setting.user_back_path);
            if (!di.Exists)
                di.Create();

            foreach (var file in di.GetFiles()) {
                if (file.Extension.Equals(".mp4")) {
                    string only_name = file.Name.Substring(0, file.Name.Length - 4);
                    string local_path = file.FullName;
                    string preview_path = file.FullName.Substring(0, file.FullName.Length - 4) + ".jpg";

                    backgrounds[0][backgrounds[0].Count - 1].Add(new Background(only_name, 0, 0, local_path, preview_path, true));
                }
            }
        }

        public static void AddEduBackgroundData(string[] datas)
        {
            const int row_cnt = 6;
            for (int i = 0; i <= datas.Length - row_cnt; i += row_cnt) {
                string name = (Setting.language.Equals("Korean")) ? datas[i] : datas[i + 1];
                int category = int.Parse(datas[i + 2]);
                int mode = int.Parse(datas[i + 3]);
#if (FREE)
                string background_path = FileManager.http_root_dir + "resource/free/" + datas[i + 4].Replace(" ", "%20") + ".mp4";
                string preview_path = FileManager.http_root_dir + "resource/free/" + datas[i + 4].Replace(" ", "%20") + ".jpg";
#else
                string background_path = "./" + datas[i + 4] + ".mp4";
                string preview_path = "./" + datas[i + 4] + ".jpg";
#endif
                int level = int.Parse(datas[i + 5]);

                for (; category >= backgrounds[1].Count;)
                    backgrounds[1].Add(new List<Background>());

                backgrounds[1][category].Add(new Background(name, mode, level, background_path, preview_path, false));
            }
        }

        public static Background GetNextBack()
        {
            int next_idx = backgrounds[1][0].IndexOf(MainForm.stage_player.GetBackground()) + 1;
            if (next_idx >= backgrounds[1][0].Count)
                return null;

            return backgrounds[1][0][next_idx];
        }

        public static Background GetNameToBack(string name)
        {
            foreach (var backgrounds in backgrounds) {
                foreach (var tab in backgrounds) {
                    var ret = tab.Find(item => item.name.Equals(name));
                    if (ret != null)
                        return ret;
                }
            }

            return null;
        }

        public static Background GetPathToBack(string path)
        {
            foreach (var backgrounds in backgrounds) {
                foreach (var tab in backgrounds) {
                    var ret = tab.Find(item => item.background_path.Equals(path));
                    if (ret != null)
                        return ret;
                }
            }

            return null;
        }
    }
}