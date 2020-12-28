using System.Collections.Generic;
using System.IO;
using System.Media;

namespace Artco
{
    public class Sound
    {
        public static List<List<Sound>> sounds = new List<List<Sound>>();
        public static SoundPlayer preview_player;
        public static int user_tab_num = 5;

        public string name;
        public string local_path;

        public Sound(string name, string local_path)
        {
            this.name = name;
            this.local_path = local_path;
        }

        public static void AddSoundData(string[] datas)
        {
            const int row_cnt = 5;
            for (int i = 0; i <= datas.Length - row_cnt; i += row_cnt) {
                string name = (Setting.language.Equals("Korean")) ? datas[i] : datas[i + 1];
                //int idx = int.Parse(datas[i + 2]);
                int category = int.Parse(datas[i + 3]);
                string local_path = "./" + datas[i + 4];

                for (; category >= sounds.Count;)
                    sounds.Add(new List<Sound>());

                sounds[category].Add(new Sound(name, local_path));
            }

            sounds.Add(new List<Sound>()); // user tab
            DirectoryInfo di = new DirectoryInfo(Setting.user_sound_path);
            if (!di.Exists)
                di.Create();

            foreach (var file in di.GetFiles()) {
                if (file.Extension.Equals(".wav")) {
                    string only_name = file.Name.Substring(0, file.Name.Length - 4);
                    string local_path = Setting.user_sound_path + "/" + file.Name;
                    sounds[sounds.Count - 1].Add(new Sound(only_name, local_path));
                }
            }
        }

        public static Sound GetNameToSound(string name)
        {
            foreach (var sound in sounds) {
                var ret = sound.Find(item => item.name.Equals(name));
                if (ret != null)
                    return ret;
            }

            return null;
        }
    }

    public static class EffectSound
    {
        public static SoundPlayer mouse_click_sound = new SoundPlayer(Properties.Resources.mouse_click);
        public static SoundPlayer block_link_sound = new SoundPlayer(Properties.Resources.block_link);
        public static SoundPlayer remove_code_sound = new SoundPlayer(Properties.Resources.recycle);
        public static SoundPlayer all_clear_sound = new SoundPlayer(Properties.Resources.all_clear);
        public static SoundPlayer move_failed_sound = new SoundPlayer(Properties.Resources.move_fail);
        public static SoundPlayer move_successed_sound = new SoundPlayer(Properties.Resources.move_success);
        public static SoundPlayer finish_game_sound = new SoundPlayer(Properties.Resources.arrive_dest);
    }
}