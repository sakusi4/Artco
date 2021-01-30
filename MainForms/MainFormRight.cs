using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public partial class MainForm
    {
        private void SelectSpriteCB(object sender)
        {
            ActivatedSpriteController.AddNewSprite((Sprite)sender);

            InvalidateStageForm();
            InvalidateBlockPanel();
            InvalidateEditorPanel();
        }

        private void Btn_SelectSprite_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            EffectSound.mouse_click_sound.Play();
            SpriteStorageForm sprite_form = new SpriteStorageForm(SelectSpriteCB);
            sprite_form.Show();
        }

        private void Btn_OpenProject_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            if (new MsgBoxForm("你确定删除现有项目吗？", true).ShowDialog() == DialogResult.Yes)
                RemoveAllSprite();

            using ProjectStorageForm project_form = new ProjectStorageForm(SelectSaveHandler);
            project_form.ShowDialog();
        }

        private void Btn_CreateNewSprite_Click(object sender, EventArgs e)
        {
            using SpriteEditForm sprite_edit_form = new SpriteEditForm {
                original_imgs = new List<Bitmap>() { new Bitmap(10, 10) }
            };

            if (sprite_edit_form.ShowDialog() == DialogResult.OK) {
                Sprite sprite = new Sprite("0", null, false, null);
                sprite.SetTmpImgList(sprite_edit_form.original_imgs);
                SelectSpriteCB(sprite);
            }
        }

        private void BtnRemoveAllSpriteClick(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            if (new MsgBoxForm("你想要删除所有打开的素材吗？", true).ShowDialog() == DialogResult.Yes)
                RemoveAllSprite();
        }

        private void RemoveAllSprite()
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING))
                return;

            ActivatedSpriteController.ClearActSpriteList();
            UserVariableManager.ClearVariables();
            SafeClearSpritePanel();
            InvalidateStageForm();
            InvalidateEditorPanel();
        }

        private void Btn_SaveProject_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            using RenameSpriteForm rename_sprite_form = new RenameSpriteForm();
            if (rename_sprite_form.ShowDialog() != DialogResult.OK)
                return;

            string file_name = rename_sprite_form.new_name + ".artcoproj";
            string save_path = Setting.save_path + "/" + file_name;
            if (new ArtcoProject().SaveProject(save_path)) {
                //FileManager.UploadSaveFile(savePath, fileName);
                new MsgBoxForm("保存完毕").ShowDialog();
            }
        }

        private void SelectSaveHandler(object sender)
        {
            string path = (string)sender;
            if (path.Contains(".ArtcoProject") || path.Contains(".artcoproj")) {
                ArtcoProject artco_project = new ArtcoProject();
                if (!artco_project.LoadProject(path))
                    return;
            } else {
                ArtcoObject artco_object = new ArtcoObject();
                if (!artco_object.LoadObject(path))
                    return;
            }

            InvalidateBlockPanel();
        }
    }
}