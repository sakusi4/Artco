using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public class SpriteView : IDisposable
    {
        private static readonly List<Block> _blocks_buf = new List<Block>();
        private readonly ActivatedSprite _sprite;

        public static DoubleBufferedFlowPanel sprite_list_panel { get; set; }
        public SpriteMiniView sprite_view { get; set; }

        public SpriteView(ActivatedSprite sprite, string name, Bitmap image)
        {
            _sprite = sprite;
            int width = (sprite_list_panel.Width / 2) - 30;
            int height = (int)(width * 1.1);

            sprite.name = NameDuplicateCheck(name);
            sprite_view = new SpriteMiniView() {
                Size = new Size(width, height),                
                spriteName = sprite.name,
                spriteImage = image
            };

            sprite_view.miniViewLClick += MiniViewLClick;
            sprite_view.miniViewRClick += MiniViewRClick;
            sprite_view.miniViewDBClick += MiniViewDBClick;
            sprite_view.miniViewRemove += MiniViewRemoveClick;
        }

        public void SetMiniViewBackImage(Bitmap image)
        {
            sprite_view.BackgroundImage = image;
        }

        public void AddMiniViewToPanel()
        {
            if (sprite_list_panel.InvokeRequired)
                sprite_list_panel.Invoke(new Action(AddMiniViewToPanel));
            else
                sprite_list_panel.Controls.Add(sprite_view);
        }

        private void MiniViewLClick(object sender, EventArgs e)
        {
            EffectSound.mouse_click_sound.Play();

            SpriteMiniView mini_view = (SpriteMiniView)sender;
            ActivatedSpriteController.Focus(sprite_list_panel.Controls.IndexOf(mini_view));
        }

        private void MiniViewRClick(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            MiniViewLClick(sender, e);

            string[] titles = { "图片复制", "保存到电脑", "复印积木", "粘贴积木", "重新定位", "往后送", "图片编辑" };
            var actions = new List<Action<object, EventArgs>>()
            {
                CopySprite, SaveSprite, CopySpriteBlock, PasteSpriteBlock, ResetPosition, SendToBack, EditSprite
            };

            MouseEventArgs args = e as MouseEventArgs;
            Utility.ShowContextMenu(sender, args.Location.X + 14, args.Location.Y + 12, titles, actions);
        }

        private void CopySprite(object sender, EventArgs e)
        {
            var sprite = new Sprite(_sprite.name, null, false, null);
            sprite.SetTmpImgList(ActivatedSpriteController.CloneBitmapList(_sprite.org_img_list));
            MainForm.select_sprite_cb(sprite);
        }

        private void SaveSprite(object sender, EventArgs e)
        {
            RenameSpriteForm rename_spriet_form = new RenameSpriteForm();
            if (rename_spriet_form.ShowDialog() != DialogResult.OK)
                return;

            if (new ArtcoObject().SaveObject(_sprite, rename_spriet_form.new_name)) {
                using MsgBoxForm msg_box_form = new MsgBoxForm("保存完毕");
                msg_box_form.ShowDialog();
            }
        }

        private void CopySpriteBlock(object sender, EventArgs e)
        {
            _blocks_buf.RemoveRange(0, _blocks_buf.Count);

            for (int i = 0; i < _sprite.code_list.Count; i++)
                for (int j = 0; j < _sprite.code_list[i].Count; j++)
                    _blocks_buf.Add(_sprite.code_list[i][j]);
        }

        private void PasteSpriteBlock(object sender, EventArgs e)
        {
            var sprite = ActivatedSpriteController.cur_sprite;
            for (int i = 0; i < _blocks_buf.Count; i++) {
                var copied_code = _blocks_buf[i];
                Block code = new Block(copied_code);
                sprite.code_editor.AddCode(code);

                if (copied_code.block_view.controls == null)
                    continue;

                for (int j = 0; j < copied_code.block_view.controls.Count; j++)
                    code.block_view.controls[j].Text = copied_code.block_view.controls[j].Text;
            }
        }

        private void ResetPosition(object sender, EventArgs e)
        {
            _sprite.x = new Random(Guid.NewGuid().GetHashCode()).Next(800);
            _sprite.y = new Random(Guid.NewGuid().GetHashCode()).Next(400);

            MainForm.invalidate_stage_form?.Invoke();
        }

        private void SendToBack(object sender, EventArgs e)
        {
            ActivatedSpriteController.sprite_list.Remove(_sprite);
            ActivatedSpriteController.sprite_list.Insert(0, _sprite);

            RepositionMiniViews();
        }

        private void MiniViewDBClick(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            EditSprite(sender, e);
        }

        public void EditSprite(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.FULLSCREEN))
                return;

            using SpriteEditForm sprite_edit_form = new SpriteEditForm() {
                original_imgs = ActivatedSpriteController.CloneBitmapList(_sprite.img_list)
            };

            if (DialogResult.OK == sprite_edit_form.ShowDialog()) {
                _sprite.org_img_list.RemoveRange(0, _sprite.org_img_list.Count);
                _sprite.org_img_list = ActivatedSpriteController.CloneBitmapList(sprite_edit_form.original_imgs);

                _sprite.zoomed_img_list.RemoveRange(0, _sprite.zoomed_img_list.Count);
                _sprite.img_list.RemoveRange(0, _sprite.img_list_count);

                for (int i = 0; i < _sprite.org_img_list.Count; i++) {
                    var original_img = _sprite.org_img_list[i];
                    _sprite.zoomed_img_list.Add(ImageUtility.GetResizedBitmap(original_img, (int)(original_img.Width * 1.92), (int)(original_img.Height * 1.94)));
                    _sprite.img_list.Add(new Bitmap(_sprite.org_img_list[i]));
                }

                _sprite.cur_img_num = 0;
                sprite_view.spriteImage = _sprite.cur_img;
                MainForm.invalidate_stage_form?.Invoke();
            }
        }

        private void MiniViewRemoveClick(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            var sprites = ActivatedSpriteController.sprite_list;
            ActivatedSpriteController.Focus(-1);

            sprites.Remove(_sprite);
            sprite_list_panel.Controls.Remove(sprite_view);
            _sprite.Dispose();

            int next_idx = (sprites.Count > 0) ? sprites.Count - 1 : -1;
            ActivatedSpriteController.Focus(next_idx);

            MainForm.invalidate_stage_form();
        }

        private static void RepositionMiniViews()
        {
            sprite_list_panel.Controls.Clear();
            for (int i = 0; i < ActivatedSpriteController.sprite_list.Count; i++)
                sprite_list_panel.Controls.Add(ActivatedSpriteController.sprite_list[i].sprite_view.sprite_view);

            ActivatedSpriteController.Focus(0);
        }

        private static string NameDuplicateCheck(string name)
        {
            string original_name = name;

            int cnt = 2;
            var sprites = ActivatedSpriteController.sprite_list;
            for (int i = 0; i < sprites.Count; i++) {
                if (sprites[i].name.Equals(name)) {
                    name = original_name + "(" + cnt++ + ")";
                    i = -1;
                }
            }

            return name;
        }

        #region IDisposable Support
        private bool _disposed_value = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed_value) {
                if (disposing) {
                    sprite_view.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed_value = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SpriteView()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
