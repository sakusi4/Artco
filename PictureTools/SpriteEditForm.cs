using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Artco
{
    public partial class SpriteEditForm : Form
    {
        private Point _mouse_down_pos;
        private Point _mouse_move_pos;
        private (Font, Color) _font_and_color;
        private int _pen_width = 10;
        private bool _is_click_stage;
        private bool _is_mouse_enter_stage;
        private FloatingTextbox _floating_txtbox;
        private double _resize_rate = 1;

        public List<Bitmap> original_imgs { get; set; }
        public Bitmap last_state_img { get; set; }

        [Flags]
        private enum Flag
        {
            NONE = 0, BRUSH = 1, ERASE = 1 << 1, PICKING = 1 << 2,
            FREEPICKING = 1 << 3, GETCOLOR = 1 << 4, TEXT = 1 << 5,
            ALL = int.MaxValue
        }
        private Flag _flag;

        public SpriteEditForm()
        {
            InitializeComponent();
        }

        private void SpriteEditForm_Load(object sender, EventArgs e)
        {
            Cursor = DynamicResources.cursor;
            _font_and_color.Item1 = DynamicResources.font;
            _font_and_color.Item2 = Color.Black;

            cmbbox_PenWidth.SelectedIndex = 1;
            InitializeEditSprite();
        }

        private void InitializeEditSprite()
        {
            for (int i = 0; i < original_imgs.Count; i++) {
                Bitmap img = original_imgs[i];
                var miniview = CreateMiniView(img, i.ToString(), Properties.Resources.Sprite_Info);

                var pos = EditSprite.GetCenterPos(EditSprite.background_bitmap, img);
                EditSprite.edit_sprites.Add(new EditSprite(ImageUtility.BmpDrawToBmp(EditSprite.background_bitmap, img, pos.X, pos.Y), miniview));
                EditSprite.ChangeCurSprite(i);
                EditSprite.GetCurSprite().SaveCurrentImage();
            }
        }

        private void Btn_CopySprite_Click(object sender, EventArgs e)
        {
            InitializeResources();

            Bitmap copy_bitmap = new Bitmap(EditSprite.GetCurSprite().img);
            Bitmap cover_img = ImageUtility.AutoCrop(copy_bitmap);
            original_imgs.Add(cover_img);

            int id = EditSprite.edit_sprites.Count;

            EditSprite.GetCurSprite().mini_view.spriteImage = cover_img;
            var miniview = CreateMiniView(cover_img, id.ToString(), Properties.Resources.Sprite_Info_Active);

            EditSprite.edit_sprites.Add(new EditSprite(copy_bitmap, miniview));
            EditSprite.ChangeCurSprite(id);
            EditSprite.GetCurSprite().SaveCurrentImage();
            InvalidatePanel();
        }

        private void Btn_OpenSprite_Click(object sender, EventArgs e)
        {
            InitializeResources();
            SpriteStorageForm sprite_form = new SpriteStorageForm((sender) => {
                var sprite = (Sprite)sender;

                Bitmap cover_img = sprite.GetSpriteImage();
                original_imgs.Add(cover_img);

                if (sprite.is_remove_back)
                    ImageUtility.RemoveImageBack(cover_img, 100);

                var pos = EditSprite.GetCenterPos(EditSprite.background_bitmap, cover_img);
                Bitmap img = ImageUtility.BmpDrawToBmp(EditSprite.background_bitmap, cover_img, pos.X, pos.Y);

                int id = EditSprite.edit_sprites.Count;
                var miniview = CreateMiniView(cover_img, id.ToString(), Properties.Resources.Sprite_Info_Active);
                EditSprite.edit_sprites.Add(new EditSprite(img, miniview));
                EditSprite.ChangeCurSprite(id);
                EditSprite.GetCurSprite().SaveCurrentImage();
                InvalidatePanel();
            });
            sprite_form.Show();
        }

        private void Btn_OriginalImage_Click(object sender, EventArgs e)
        {
            InitializeResources();

            Bitmap img = original_imgs[EditSprite.cur_sprite_idx];
            int x = (EditSprite.background_bitmap.Width / 2) - (img.Width / 2);
            int y = (EditSprite.background_bitmap.Height / 2) - (img.Height / 2);

            EditSprite.GetCurSprite().img = ImageUtility.BmpDrawToBmp(EditSprite.background_bitmap, img, x, y);

            InvalidatePanel();
        }

        private void Btn_AddSprite_Click(object sender, EventArgs e)
        {
            InitializeResources();
            SpriteStorageForm sprite_form = new SpriteStorageForm((sender) => {
                var sprite = (Sprite)sender;
                Bitmap image = sprite.GetSpriteImage();
                if (sprite.is_remove_back)
                    ImageUtility.RemoveImageBack(image, 100);

                EditSprite.picked_area_points = new List<Point>()
                        {
                                new Point(0, 0), new Point(image.Width, 0),
                                new Point(image.Width, image.Height), new Point(0, image.Height)
                        };

                var cur_edit_sprite = EditSprite.GetCurSprite();
                cur_edit_sprite.picked_area = new PickedArea();
                cur_edit_sprite.picked_area.SetSize(EditSprite.picked_area_points);
                cur_edit_sprite.picked_area.SetBitmaps(image, new Bitmap(cur_edit_sprite.img));

                angleSelector.Visible = true;
                cur_edit_sprite.DrawPickedImage();
                InvalidatePanel();
            });

            sprite_form.Show();
        }

        private void Btn_Brush_Click(object sender, EventArgs e) { ChangeMode(Flag.BRUSH, btn_Brush); }
        private void Btn_ColorEraser_Click(object sender, EventArgs e) { ChangeMode(Flag.ERASE, btn_ColorEraser); }
        private void Btn_CutImage_Click(object sender, EventArgs e) { ChangeMode(Flag.PICKING, btn_CutImage); }
        private void Btn_FreeCutImage_Click(object sender, EventArgs e) { ChangeMode(Flag.FREEPICKING, btn_FreeCutImage); }
        private void Btn_AddText_Click(object sender, EventArgs e) { ChangeMode(Flag.TEXT, btn_AddText); }
        private void Btn_GetColor_Click(object sender, EventArgs e) { ChangeMode(Flag.GETCOLOR, null); }


        private void MiniViewLClick(object sender, EventArgs e)
        {
            InitializeResources();
            var cur_sprite = EditSprite.GetCurSprite();
            cur_sprite.mini_view.spriteImage = ImageUtility.AutoCrop(cur_sprite.img);
            EditSprite.ChangeCurSprite(int.Parse(((SpriteMiniView)sender).spriteName));
            InvalidatePanel();
        }

        private void MiniViewRemove(object sender, EventArgs e)
        {
            int remove_id = int.Parse(((SpriteMiniView)sender).spriteName);
            if (remove_id == 0) {
                MessageBox.Show("原图不能修改", "Artco", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            EditSprite.ChangeCurSprite(remove_id);
            var remove_sprite = EditSprite.GetCurSprite();
            pnl_SpriteList.Controls.Remove(remove_sprite.mini_view);
            EditSprite.edit_sprites.Remove(remove_sprite);

            for (int i = 0; i < EditSprite.edit_sprites.Count; i++) {
                var sprite = EditSprite.edit_sprites[i];
                sprite.mini_view.spriteName = i.ToString();
            }

            EditSprite.ChangeCurSprite(0);
            InvalidatePanel();
        }

        private void Pnl_Stage_Paint(object sender, PaintEventArgs e)
        {
            var sprite = EditSprite.GetCurSprite();
            e.Graphics.DrawImage(sprite.img, 0, 0);

            if (CompareFlag(_flag, Flag.FREEPICKING) && EditSprite.picked_area_points != null && EditSprite.picked_area_points.Count > 1) {
                using Pen dashed_pen = new Pen(Color.Black) { DashPattern = new float[] { 5, 5 } };
                e.Graphics.DrawLines(dashed_pen, EditSprite.picked_area_points.ToArray());
            } else if (CompareFlag(_flag, Flag.PICKING) && EditSprite.picked_area_points != null && EditSprite.picked_area_points.Count > 1) {
                using Pen dashed_pen = new Pen(Color.Black) { DashPattern = new float[] { 5, 5 } };
                e.Graphics.DrawPolygon(dashed_pen, EditSprite.picked_area_points.ToArray());
            } else if (_is_mouse_enter_stage) {
                if (CompareFlag(_flag, Flag.BRUSH)) {
                    Brush b = new SolidBrush(pnl_ShowCurColor.BackColor);
                    e.Graphics.FillEllipse(b, _mouse_move_pos.X - (_pen_width / 2), _mouse_move_pos.Y - (_pen_width / 2), _pen_width, _pen_width);
                } else if (CompareFlag(_flag, Flag.ERASE)) {
                    Brush b = new SolidBrush(Color.White);
                    e.Graphics.FillEllipse(b, _mouse_move_pos.X - (_pen_width / 2), _mouse_move_pos.Y - (_pen_width / 2), _pen_width, _pen_width);
                }
            }

            if (sprite.picked_area != null) {
                using Pen dashed_pen = new Pen(Color.Black) { DashPattern = new float[] { 5, 5 } };
                e.Graphics.DrawPolygon(dashed_pen, sprite.picked_area.points.ToArray());
            }
        }

        private void Pnl_Stage_MouseDown(object sender, MouseEventArgs e)
        {
            _is_click_stage = true;

            if (CompareFlag(_flag, Flag.BRUSH) || CompareFlag(_flag, Flag.ERASE) || CompareFlag(_flag, Flag.PICKING)) {
                _mouse_down_pos = e.Location;
                return;
            }
            if (CompareFlag(_flag, Flag.FREEPICKING)) {
                EditSprite.picked_area_points = new List<Point>() { new Point(e.X, e.Y) };
                return;
            }
            if (CompareFlag(_flag, Flag.GETCOLOR)) {
                pnl_ShowCurColor.BackColor = EditSprite.GetCurSprite().img.GetPixel(e.X, e.Y);
                return;
            }
            if (CompareFlag(_flag, Flag.TEXT)) {
                InitializeResources();

                _floating_txtbox = new FloatingTextbox(_font_and_color.Item1, e.Location);
                var txtbox = _floating_txtbox.GetItem();
                pnl_Stage.Controls.Add(txtbox);
                txtbox.Focus();
                return;
            }

            var sprite = EditSprite.GetCurSprite();
            if (sprite.picked_area != null) {
                if (sprite.picked_area.IsInsidePickedArea(e.Location)) {
                    sprite.picked_area.is_moving = true;
                    sprite.picked_area.move_start_point = e.Location;
                } else {
                    InitializeResources();
                }

                return;
            }

            InitializeResources();
        }

        private void Pnl_Stage_MouseMove(object sender, MouseEventArgs e)
        {
            _mouse_move_pos = e.Location;
            if (_is_click_stage && CompareFlag(_flag, Flag.BRUSH) && _mouse_down_pos != _mouse_move_pos) {
                DrawLine(_mouse_down_pos, _mouse_move_pos);
                _mouse_down_pos = _mouse_move_pos;
            } else if (_is_click_stage && CompareFlag(_flag, Flag.ERASE) && _mouse_down_pos != _mouse_move_pos) {
                DrawLine(_mouse_down_pos, _mouse_move_pos);
                _mouse_down_pos = _mouse_move_pos;

                EditSprite.GetCurSprite().img.MakeTransparent(Color.FromArgb(255, 255, 255));
            } else if (_is_click_stage && CompareFlag(_flag, Flag.PICKING)) {
                int dx = _mouse_move_pos.X - _mouse_down_pos.X;
                int dy = _mouse_move_pos.Y - _mouse_down_pos.Y;

                if (dx < 0 || dy < 0) {
                    InitializeResources();
                    return;
                }

                EditSprite.picked_area_points = new List<Point>() {
                        new Point(_mouse_down_pos.X, _mouse_down_pos.Y),
                        new Point(_mouse_down_pos.X + dx, _mouse_down_pos.Y),
                        new Point(_mouse_down_pos.X + dx, _mouse_down_pos.Y + dy),
                        new Point(_mouse_down_pos.X, _mouse_down_pos.Y + dy)
                };
            } else if (_is_click_stage && CompareFlag(_flag, Flag.FREEPICKING)) {
                if (_mouse_move_pos.X < 0 || _mouse_move_pos.X > pnl_Stage.Width ||
                    _mouse_move_pos.Y < 0 || _mouse_move_pos.Y > pnl_Stage.Height) {
                    InitializeResources();
                    return;
                }

                EditSprite.picked_area_points.Add(new Point(_mouse_move_pos.X, _mouse_move_pos.Y));
            }

            var sprite = EditSprite.GetCurSprite();
            var picked_area = sprite.picked_area;
            if (_is_click_stage && picked_area != null && picked_area.is_moving) {
                int dx = _mouse_move_pos.X - picked_area.move_start_point.X;
                int dy = _mouse_move_pos.Y - picked_area.move_start_point.Y;
                picked_area.x += dx;
                picked_area.y += dy;
                picked_area.MovePoints(dx, dy);
                picked_area.move_start_point = new Point(_mouse_move_pos.X, _mouse_move_pos.Y);

                sprite.DrawPickedImage();
            }

            InvalidatePanel();
        }

        private void Pnl_Stage_MouseUp(object sender, MouseEventArgs e)
        {
            _is_click_stage = false;
            var sprite = EditSprite.GetCurSprite();

            if (CompareFlag(_flag, Flag.BRUSH) || CompareFlag(_flag, Flag.ERASE)) {
                sprite.SaveCurrentImage();
            } else if (CompareFlag(_flag, Flag.PICKING) || CompareFlag(_flag, Flag.FREEPICKING)) {
                if (e.X < 0 || e.X > pnl_Stage.Width || e.Y < 0 || e.Y > pnl_Stage.Height ||
                    EditSprite.picked_area_points.Count < 3) {
                    InitializeResources();
                    return;
                }

                _flag = Flag.NONE;

                sprite.picked_area = new PickedArea();
                sprite.picked_area.SetSize(EditSprite.picked_area_points);

                Bitmap picked_bmp = ImageUtility.MakeImageWithArea(EditSprite.GetCurSprite().img, EditSprite.picked_area_points);
                picked_bmp = picked_bmp.Clone(sprite.picked_area.GetRectangle(), System.Drawing.Imaging.PixelFormat.DontCare);
                Bitmap without_picked_bmp = ImageUtility.MakeImageWithoutArea(EditSprite.GetCurSprite().img, EditSprite.picked_area_points);
                sprite.picked_area.SetBitmaps(picked_bmp, without_picked_bmp);

                angleSelector.Visible = true;
                InvalidatePanel();
            }

            if (sprite.picked_area != null && sprite.picked_area.is_moving) {
                sprite.picked_area.is_moving = false;
                sprite.SaveCurrentImage();
            }
        }

        private void DrawLine(Point s, Point e)
        {
            using Graphics g = Graphics.FromImage(EditSprite.GetCurSprite().img);
            g.InterpolationMode = (CompareFlag(_flag, Flag.ERASE)) ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
            using Pen pen = new Pen((CompareFlag(_flag, Flag.ERASE)) ? Color.White : pnl_ShowCurColor.BackColor, _pen_width) {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };

            g.DrawLine(pen, s, e);
        }

        private void DrawText()
        {
            using Graphics g = Graphics.FromImage(EditSprite.GetCurSprite().img);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var txt_box = _floating_txtbox.GetItem();
            g.DrawString(txt_box.Text, txt_box.Font, new SolidBrush(txt_box.ForeColor), _floating_txtbox.GetRectangle());
        }

        private void Btn_FlipX_Click(object sender, EventArgs e) { EditSpriteAction(EditSprite.GetCurSprite().FlipXImage); }
        private void Btn_FlipY_Click(object sender, EventArgs e) { EditSpriteAction(EditSprite.GetCurSprite().FlipYImage); }
        private void Btn_RightRotate_Click(object sender, EventArgs e) { EditSpriteAction(EditSprite.GetCurSprite().RRotateImage); }
        private void Btn_LeftRotate_Click(object sender, EventArgs e) { EditSpriteAction(EditSprite.GetCurSprite().LRotateImage); }
        private void Btn_SizeUp_Click(object sender, EventArgs e) { ResizeSprite(0.2); }
        private void Btn_SizeDown_Click(object sender, EventArgs e) { ResizeSprite(-0.2); }

        private void ResizeSprite(double scale)
        {
            if (last_state_img == null)
                last_state_img = ImageUtility.AutoCrop(EditSprite.GetCurSprite().img);

            double old_resize_rate = _resize_rate;
            _resize_rate += scale;
            if (!EditSprite.GetCurSprite().ResizeImage(_resize_rate, last_state_img)) {
                _resize_rate = old_resize_rate;
                return;
            }

            InvalidatePanel();
        }

        private void AngleSelector_AngleChanged()
        {
            EditSprite.GetCurSprite().RotateImageWithValue(angleSelector.Angle);
            InvalidatePanel();
        }

        private void Btn_ColorList_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true; //사용자 지정 색상
            colorDialog1.ShowHelp = true;
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pnl_ShowCurColor.BackColor = colorDialog1.Color;
        }

        // 수정 필요: TrueType이 아닌 글꼴은 에러
        private void Btn_SetFont_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            if (fontDialog1.ShowDialog() != DialogResult.Cancel) {
                _font_and_color.Item1 = fontDialog1.Font;
                _font_and_color.Item2 = fontDialog1.Color;

                if (_floating_txtbox != null) {
                    var txtbox = _floating_txtbox.GetItem();
                    txtbox.Font = _font_and_color.Item1;
                    txtbox.ForeColor = _font_and_color.Item2;
                }
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            var sprite = EditSprite.GetCurSprite();
            if (sprite.picked_area != null) {
                MessageBox.Show("图片编辑中", "Artco", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using var msgbox = new MsgBoxForm("是否保存更改", true);
            if (msgbox.ShowDialog() == DialogResult.Yes) {
                InitializeResources();
                original_imgs = EditSprite.GetBitmapList();
                DialogResult = (original_imgs[0] != null) ? DialogResult.OK : DialogResult.Cancel;
            }

            Close();
        }

        private void Btn_Undo_Click(object sender, EventArgs e)
        {
            EditSprite.GetCurSprite().LoadSavedImage(true);
            InvalidatePanel();
        }

        private void Btn_Redo_Click(object sender, EventArgs e)
        {
            EditSprite.GetCurSprite().LoadSavedImage(false);
            InvalidatePanel();
        }

        private void Pnl_Stage_MouseEnter(object sender, EventArgs e)
        {
            if (CompareFlag(_flag, Flag.TEXT))
                Cursor = Cursors.IBeam;

            _is_mouse_enter_stage = true;
            InvalidatePanel();
        }

        private void Pnl_Stage_MouseLeave(object sender, EventArgs e)
        {
            if (CompareFlag(_flag, Flag.TEXT))
                Cursor = DynamicResources.cursor;

            _is_mouse_enter_stage = false;
            InvalidatePanel();
        }

        private bool CompareFlag(Flag flag1, Flag flag2)
        {
            if ((flag1 & flag2) != 0)
                return true;

            return false;
        }

        private void InitializeResources()
        {
            if (_floating_txtbox != null) {
                DrawText();
                pnl_Stage.Controls.Remove(_floating_txtbox.GetItem());
                _floating_txtbox = null;
            }

            Cursor = DynamicResources.cursor;

            var sprite = EditSprite.GetCurSprite();
            sprite.picked_area?.Dispose();
            sprite.picked_area = null;

            last_state_img = null;
            EditSprite.picked_area_points = new List<Point>();

            _resize_rate = 1;

            btn_CutImage.BackgroundImage = null;
            btn_FreeCutImage.BackgroundImage = null;
            btn_Brush.BackgroundImage = null;
            btn_ColorEraser.BackgroundImage = null;
            btn_AddText.BackgroundImage = null;

            angleSelector.Visible = false;
            angleSelector.Angle = 0;

            _flag = Flag.NONE;

            InvalidatePanel();
        }

        private SpriteMiniView CreateMiniView(Bitmap image, string name, Bitmap backImage)
        {
            SpriteMiniView miniview = new SpriteMiniView() {
                spriteImage = new Bitmap(image),
                spriteName = name
            };

            miniview.BackgroundImage = backImage;
            miniview.BackgroundImageLayout = ImageLayout.Stretch;
            miniview.miniViewLClick += MiniViewLClick;
            miniview.miniViewRemove += MiniViewRemove;
            pnl_SpriteList.Controls.Add(miniview);

            return miniview;
        }

        private void ChangeMode(Flag flag, Bunifu.Framework.UI.BunifuImageButton btn)
        {
            InitializeResources();
            this._flag = flag;
            if (btn != null)
                btn.BackgroundImage = Properties.Resources.Sprite_Info_Active;
        }

        private void EditSpriteAction(Action action)
        {
            action.Invoke();
            InvalidatePanel();
        }

        private void InvalidatePanel() { pnl_Stage.Invalidate(); }
        private void SelectColorHandler(object sender, EventArgs e) { pnl_ShowCurColor.BackColor = ((Panel)sender).BackColor; }
        private void Cmbbox_PenWidth_SelectedIndexChanged(object sender, EventArgs e) { _pen_width = int.Parse(cmbbox_PenWidth.Text); }
        private void Cmbbox_PenWidth_TextUpdate(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbbox_PenWidth.Text))
                return;

            if (int.TryParse(cmbbox_PenWidth.Text, out int n)) {
                _pen_width = n;
            }
        }
        private void SpriteEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            InitializeResources();
            EditSprite.InitializeEditSprite();
        }


        private void PnlTitleMouseDown(object sender, MouseEventArgs e) { _mouse_move_pos = new Point(e.X, e.Y); }
        private void PnlTitleMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && e.Y < 39)
                Location = new Point(this.Left - (_mouse_move_pos.X - e.X), this.Top - (_mouse_move_pos.Y - e.Y));
        }
    }
}