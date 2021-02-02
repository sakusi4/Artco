using System.Collections.Generic;
using System.Drawing;

namespace Artco
{
    static class ActivatedSpriteController
    {
        private static readonly List<ActivatedSprite> _sprite_list = new List<ActivatedSprite>();
        private static int _cur_sprite_num = -1;

        public static List<Bitmap> CloneBitmapList(List<Bitmap> src)
        {
            List<Bitmap> dst = new List<Bitmap>();

            for (int i = 0; i < src.Count; i++)
                dst.Add(new Bitmap(src[i]));

            return dst;
        }

        public static void TranslateSizeAndLoc(bool is_fool)
        {
            var (rate_x, rate_y) = StagePlayer.GetScreenSizeRate();

            for (int i = 0; i < sprite_list.Count; i++) {
                var sprite = sprite_list[i];
                int num = sprite.cur_img_num;

                sprite.img_list[num] = (is_fool) ? sprite.zoomed_img_list[num] : sprite.org_img_list[num];
                sprite.x = (int)(sprite.x * rate_x);
                sprite.y = (int)(sprite.y * rate_y);
            }
        }

        public static void SetSpritesViewSize()
        {
            int width = (SpriteView.sprite_list_panel.Width / 2) - 30;
            int height = (int)(width * 1.1);

            foreach (var sprite in _sprite_list) {
                sprite.sprite_view.sprite_view.Size = new Size(width, height);
            }
        }

        public static void Focus(int new_sprite_num)
        {
            if (new_sprite_num == -1) {
                _cur_sprite_num = -1;
            } else {
                int old_sprite_num = _cur_sprite_num;

                if ((new_sprite_num != old_sprite_num) && old_sprite_num != -1) {
                    sprite_list[old_sprite_num].sprite_view.SetMiniViewBackImage(Properties.Resources.Sprite_Info);
                    sprite_list[old_sprite_num].code_editor.Hide();
                }

                _cur_sprite_num = new_sprite_num;
                sprite_list[new_sprite_num].sprite_view.SetMiniViewBackImage(Properties.Resources.Sprite_Info_Active);
                sprite_list[new_sprite_num].code_editor.Show();
            }

            MainForm.invalidate_editor_panel?.Invoke();
        }

        public static void AddNewSprite(Sprite sprite)
        {
            ActivatedSprite act_sprite = new ActivatedSprite(sprite);
            act_sprite.sprite_view.AddMiniViewToPanel();
            UserVariableManager.AddComboBoxSpriteName(act_sprite.name);

            sprite_list.Add(act_sprite);
            Focus(sprite_list.Count - 1);
        }

        public static void ClearActSpriteList()
        {
            Focus(-1);
            for (int i = 0; i < sprite_list.Count; i++)
                sprite_list[i].Dispose();

            sprite_list.RemoveRange(0, sprite_list.Count);
        }

        public static ActivatedSprite GetActSpriteWithLoc(int loc_x, int loc_y)
        {
            for (int i = sprite_list.Count - 1; i >= 0; i--) {
                var sprite = sprite_list[i];
                int right_up_x = sprite.x + sprite.width;
                int left_bot_y = sprite.y + sprite.height;

                if (sprite.x < loc_x && right_up_x > loc_x && sprite.y < loc_y && left_bot_y > loc_y)
                    return sprite_list[i];
            }
            return null;
        }

        public static List<string> GetNamesWithoutMe()
        {
            if (_cur_sprite_num == -1)
                return null;

            List<string> names = new List<string>();
            for (int i = 0; i < sprite_list.Count; i++) {
                if (i == _cur_sprite_num)
                    continue;

                names.Add(sprite_list[i].name);
            }

            return names;
        }

        public static void InitActSprites()
        {
            foreach (var sprite in sprite_list) {
                foreach (var clone in sprite.cloned_sprite_list)
                    clone.Dispose();

                sprite.cloned_sprite_list.Clear();
                sprite.wait_signal_obj.Clear();
                sprite.sprite_runner = null;
                sprite.cur_img_num = 0;
                sprite.cur_img = sprite.GetOriginalImg();
                sprite.speak_text = null;
                sprite.is_visible = true;
                sprite.is_cw = false;
                sprite.angle = 0;
                sprite.arrow = sprite.sarrow;
                sprite.x = sprite.sx;
                sprite.y = sprite.sy;
            }
        }

        public static ActivatedSprite GetSprite(int idx)
        {
            return sprite_list[idx];
        }

        public static bool IsEmpty()
        {
            return (_cur_sprite_num == -1) ? true : false;
        }

        public static List<ActivatedSprite> sprite_list {
            get {
                return _sprite_list;
            }
        }

        public static ActivatedSprite cur_sprite {
            get {
                if (_cur_sprite_num == -1)
                    return null;

                return sprite_list[_cur_sprite_num];
            }
        }

        public static bool IsActSprite(string name)
        {            
            foreach (var item in sprite_list) {
                var ret = sprite_list.Find(item => item.name.Equals(name));
                if (ret != null)
                    return true;
            }

            return false;
        }
    }
}
