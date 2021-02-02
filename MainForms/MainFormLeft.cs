using System;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public partial class MainForm
    {
        private void Pnl_Blocks_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int category = Block.selected_category;
            int size = Block.blocks[category].Count;

            for (int i = 0; i < size; i++) {
                var block = Block.blocks[category][i];
                g.DrawImage(block.block_img, block.vx, block.vy, block.width, block.width);
            }
        }

        private void Pnl_Blocks_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING) ||
                ActivatedSpriteController.IsEmpty())
                return;

            MouseEventArgs pos = (MouseEventArgs)e;

            Block block = Block.IsInsideBlock(pos.X, pos.Y);
            if (block == null)
                return;

            ActivatedSpriteController.cur_sprite.code_editor.AddCode(new Block(block));
        }

        private void Btn_MoveTab_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            EffectSound.mouse_click_sound.Play();
            ShowMoveMenu();
        }

        private void ShowMoveMenu()
        {
            ClearMenuBitmaps();

            btn_MoveTab.Image = Properties.Resources.Move2;

            Block.selected_category = 1;
            InvalidateBlockPanel();
        }

        private void Btn_ActionTab_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            EffectSound.mouse_click_sound.Play();

            ClearMenuBitmaps();
            btn_ActionTab.Image = Properties.Resources.Action2;

            Block.selected_category = 2;
            InvalidateBlockPanel();
        }

        private void Btn_ControlTab_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            EffectSound.mouse_click_sound.Play();

            ClearMenuBitmaps();
            btn_ControlTab.Image = Properties.Resources.Control2;

            pnl_VarMenu.Visible = true;

            Block.selected_category = 3;
            InvalidateBlockPanel();
        }

        private void Btn_EventTab_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            EffectSound.mouse_click_sound.Play();

            ClearMenuBitmaps();
            btn_EventTab.Image = Properties.Resources.Event2;

            Block.selected_category = 0;
            InvalidateBlockPanel();
        }

        private void Btn_AI_Click(object sender, EventArgs e)
        {
            //if (IsGameOrPlayOrStart())
            //    return;

            //EffectSound._mouseClickSound.Play();

            //ClearMenuBitmaps();
            //btn_AI.Image = DynamicResources.AI2;

            //Block._selectedCategory = 5;
            //SafeInvalidateBlockPanel();
        }

        private void ShowPracticeMenu()
        {
            ClearMenuBitmaps();
            Block.selected_category = 4;
            InvalidateBlockPanel();
        }

        private void ClearMenuBitmaps()
        {
            btn_AI.Image = Properties.Resources.AI1;
            btn_EventTab.Image = Properties.Resources.Event1;
            btn_MoveTab.Image = Properties.Resources.Move1;
            btn_ActionTab.Image = Properties.Resources.Action1;
            btn_ControlTab.Image = Properties.Resources.Control1;

            pnl_VarMenu.Visible = false;            
        }

        private void Btn_CreateVariable_Click(object sender, EventArgs e)
        {
            using var create_var_form = new CreateVarForm();
            if (create_var_form.ShowDialog() == DialogResult.OK)
                UserVariableManager.AddVariable(create_var_form.var_name, create_var_form.var_valaue);
        }

        private bool _is_show_var_list = true;

        private void Btn_HideVarList_Click(object sender, EventArgs e)
        {
            if (_is_show_var_list) {
                UserVariableManager.HideMiniViewLists();
                btn_HideVarList.Image = Properties.Resources.ShowVarListBtn;
            } else {
                UserVariableManager.ShowMiniViewLists();
                btn_HideVarList.Image = Properties.Resources.HideVarListBtn;
            }
            _is_show_var_list ^= true;
        }
    }
}