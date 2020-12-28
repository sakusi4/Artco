using ArtcoCustomControl;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    class EditSprite
    {
        public static List<EditSprite> edit_sprites = new List<EditSprite>();
        public static List<Point> picked_area_points;
        public static int cur_sprite_idx = 0;
        public static Bitmap background_bitmap = new Bitmap(1000, 550);

        public Bitmap img;
        public PickedArea picked_area;
        public SpriteMiniView mini_view;
        public List<Bitmap> history_imgs = new List<Bitmap>();
        public int history_idx;

        public static void InitializeEditSprite()
        {
            edit_sprites.Clear();
            picked_area_points = null;
            cur_sprite_idx = 0;
        }

        public static EditSprite GetCurSprite() { return edit_sprites[cur_sprite_idx]; }

        public static List<Bitmap> GetBitmapList()
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            for (int i = 0; i < edit_sprites.Count; i++)
                bitmaps.Add(ImageUtility.AutoCrop(edit_sprites[i].img));

            return bitmaps;
        }

        public static void ChangeCurSprite(int new_idx)
        {
            int old_idx = cur_sprite_idx;
            if (new_idx == old_idx)
                return;

            if (old_idx < edit_sprites.Count)
                edit_sprites[old_idx].mini_view.BackgroundImage = Properties.Resources.Sprite_Info;

            edit_sprites[new_idx].mini_view.BackgroundImage = Properties.Resources.Sprite_Info_Active;
            cur_sprite_idx = new_idx;
        }

        public static Point GetCenterPos(Bitmap back_img, Bitmap for_img)
        {
            int x = (back_img.Width / 2) - (for_img.Width / 2);
            int y = (back_img.Height / 2) - (for_img.Height / 2);
            return new Point(x, y);
        }

        public EditSprite(Bitmap img, SpriteMiniView miniview)
        {
            this.img = img;
            this.mini_view = miniview;
        }

        public void SaveCurrentImage()
        {
            Bitmap bitmap = ImageUtility.AutoCrop(img);
            history_imgs.Add((bitmap == null) ? new Bitmap(10, 10) : bitmap);
            history_idx = history_imgs.Count - 1;
        }

        public void LoadSavedImage(bool is_undo)
        {
            if (picked_area != null) {
                MessageBox.Show("图片编辑中", "Artco", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (is_undo) {
                if (history_idx < 1)
                    return;
            } else {
                if (history_idx >= history_imgs.Count - 1)
                    return;
            }

            history_idx = (is_undo) ? history_idx - 1 : history_idx + 1;

            Bitmap load_img = history_imgs[history_idx];
            img = ImageUtility.BmpDrawToBmp(background_bitmap, load_img, GetCenterPos(background_bitmap, load_img));
        }

        public void DrawPickedImage()
        {
            if (picked_area == null)
                return;

            img = ImageUtility.BmpDrawToBmp(picked_area.without_picked_bmp, picked_area.picked_bmp, picked_area.x, picked_area.y);
        }

        public void DrawImageToPanel(Bitmap image)
        {
            int x = (background_bitmap.Width / 2) - (image.Width / 2);
            int y = (background_bitmap.Height / 2) - (image.Height / 2);
            img = ImageUtility.BmpDrawToBmp(background_bitmap, image, x, y);
            SaveCurrentImage();
        }

        public void FlipXImage()
        {
            if (picked_area != null) {
                picked_area.picked_bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                DrawPickedImage();
            } else {
                Bitmap croped_bitmap = ImageUtility.AutoCrop(img);
                croped_bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                DrawImageToPanel(croped_bitmap);
            }
        }

        public void FlipYImage()
        {
            if (picked_area != null) {
                picked_area.picked_bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                DrawPickedImage();
            } else {
                Bitmap croped_bitmap = ImageUtility.AutoCrop(img);
                croped_bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                DrawImageToPanel(croped_bitmap);
            }
        }

        public void RRotateImage()
        {
            if (picked_area != null) {
                picked_area.picked_bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                DrawPickedImage();
            } else {
                Bitmap croped_bitmap = ImageUtility.AutoCrop(img);
                croped_bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                DrawImageToPanel(croped_bitmap);
            }
        }

        public void LRotateImage()
        {
            if (picked_area != null) {
                picked_area.picked_bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                DrawPickedImage();
            } else {
                Bitmap croped_bitmap = ImageUtility.AutoCrop(img);
                croped_bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                DrawImageToPanel(croped_bitmap);
            }
        }

        public void RotateImageWithValue(float angle)
        {
            if (picked_area == null)
                return;

            picked_area.RotateImage(angle);
            DrawPickedImage();
        }

        public bool ResizeImage(double resize_rate, Bitmap resize_bitmap)
        {
            if (picked_area != null) {
                PickedArea temp_area = picked_area.DeepCopy();

                int width = (int)(picked_area.resize_bmp.Width * resize_rate);
                int height = (int)(picked_area.resize_bmp.Height * resize_rate);
                if (width >= 1000 || height >= 550 || width <= 20 || height <= 20)
                    return false;

                picked_area_points = new List<Point>()
                {
                    new Point(picked_area.x, picked_area.y),
                    new Point(picked_area.x + width, picked_area.y),
                    new Point(picked_area.x + width, picked_area.y + height),
                    new Point(picked_area.x, picked_area.y + height)
                };

                temp_area.picked_bmp = ImageUtility.GetResizedBitmap(picked_area.resize_bmp, width, height);
                temp_area.rotate_bmp = new Bitmap(temp_area.picked_bmp);
                temp_area.SetSize(picked_area_points);

                picked_area?.Dispose();
                picked_area = temp_area;
                DrawPickedImage();
            } else {
                int width = (int)(resize_bitmap.Width * resize_rate);
                int height = (int)(resize_bitmap.Height * resize_rate);
                if (width >= 1000 || height >= 550 || width <= 20 || height <= 20)
                    return false;

                DrawImageToPanel(ImageUtility.GetResizedBitmap(resize_bitmap, width, height));
            }

            return true;
        }
    }
}
