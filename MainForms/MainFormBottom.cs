using System;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public partial class MainForm
    {
        public static bool is_full_editor { get; set; }

        private void Pnl_EditorBox_Paint(object sender, PaintEventArgs e)
        {
            if (ActivatedSpriteController.IsEmpty())
                return;

            var cur_sprite = ActivatedSpriteController.cur_sprite;
            var editor = cur_sprite.code_editor;
            if (editor.cur_line_num == -1)
                return;

            int y = editor.code_panel.Controls[editor.cur_line_num].Location.Y + 32;
            e.Graphics.DrawImage(Properties.Resources.EditorSelectedLine, 0, y);


            // 선택 된 블럭 그리는 영역
            if (editor.selected_code == null)
                return;

            var codes = cur_sprite.code_list;
            int start = editor.range_cnt = codes[editor.cur_line_num].IndexOf(editor.selected_code);
            string name = codes[editor.cur_line_num][start].name;

            if (name.Equals("ControlLoop") || name.Equals("ControlLoopN") ||
                name.Equals("ControlCondition")) {
                editor.range_cnt = editor.GetRangeCount(editor.cur_line_num, start);
            }

            Point p1 = editor.code_panel.Location;
            Point p2 = editor.code_panel.Controls[editor.cur_line_num].Location;

            for (int i = start; i <= editor.range_cnt; i++) {
                int x = p1.X + p2.X + codes[editor.cur_line_num][i].block_view.Location.X - 5;
                y = p1.Y + p2.Y + codes[editor.cur_line_num][i].block_view.Location.Y - 4;
                e.Graphics.DrawImage(Properties.Resources.BlockOutline, x, y);
            }
        }

        private void Btn_CodeClear_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING) ||
                ActivatedSpriteController.IsEmpty())
                return;

            EffectSound.all_clear_sound.Play();

            ActivatedSpriteController.cur_sprite.code_editor.ClearCode();
            InvalidateEditorPanel();
        }

        private int _scroll_pos;

        private void Btn_ChangeEditorSize_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            if (!is_full_editor) {
                pnl_EditorBox.Height *= 2;
                btn_ChangeEditorSize.Image = Properties.Resources.GlassNormalScreen;
            } else {
                pnl_EditorBox.Height /= 2;
                btn_ChangeEditorSize.Image = Properties.Resources.GlassFullScreen;
            }

            is_full_editor ^= true;

            if (ActivatedSpriteController.IsEmpty())
                return;

            ActivatedSpriteController.cur_sprite.code_editor.code_panel.AutoScrollPosition = new Point(0, 0);
        }

        private void Pnl_EditorBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ActivatedSpriteController.IsEmpty())
                return;

            var editor = ActivatedSpriteController.cur_sprite.code_editor;
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

            if (lines > 0) {
                if (_scroll_pos - 100 > 0) {
                    _scroll_pos -= 100;
                    editor.code_panel.VerticalScroll.Value = _scroll_pos;
                } else {
                    _scroll_pos = 0;
                    editor.code_panel.AutoScrollPosition = new Point(0, _scroll_pos);
                }
            } else if (lines < 0) {
                if (_scroll_pos + 100 < editor.code_panel.VerticalScroll.Maximum) {
                    _scroll_pos += 100;
                    editor.code_panel.VerticalScroll.Value = _scroll_pos;
                } else {
                    _scroll_pos = editor.code_panel.VerticalScroll.Maximum;
                    editor.code_panel.AutoScrollPosition = new Point(0, _scroll_pos);
                }
            }

            InvalidateEditorPanel();
        }

        private void MoveCodeBlock(bool is_left)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            if (ActivatedSpriteController.IsEmpty())
                return;

            ActivatedSpriteController.cur_sprite.code_editor.MoveCode(is_left);
        }
    }
}
