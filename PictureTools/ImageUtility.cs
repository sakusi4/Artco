using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Artco
{
    public class PickedArea : IDisposable
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public int history_cnt;
        public bool is_moving;
        public Bitmap picked_bmp;
        public Bitmap without_picked_bmp;
        public Bitmap rotate_bmp;
        public Bitmap resize_bmp;
        public Point move_start_point;
        public List<Point> points;

        public PickedArea DeepCopy()
        {
            PickedArea picked_area = new PickedArea {
                x = this.x,
                y = this.y,
                width = this.width,
                height = this.height,
                picked_bmp = new Bitmap(this.picked_bmp),
                without_picked_bmp = new Bitmap(this.without_picked_bmp),
                rotate_bmp = new Bitmap(this.rotate_bmp),
                resize_bmp = new Bitmap(this.resize_bmp),
                is_moving = this.is_moving,
                move_start_point = this.move_start_point,
                points = this.points
            };

            return picked_area;
        }
        public PickedArea() { }

        public bool SetSize(List<Point> free_points)
        {
            if (free_points.Count < 3)
                return false;

            int min_x = 987654321, min_y = 987654321;
            int max_x = -987654321, max_y = -987654321;
            for (int i = 0; i < free_points.Count; i++) {
                min_x = Math.Min(min_x, free_points[i].X);
                min_y = Math.Min(min_y, free_points[i].Y);

                max_x = Math.Max(max_x, free_points[i].X);
                max_y = Math.Max(max_y, free_points[i].Y);
            }

            x = min_x;
            y = min_y;
            width = max_x - min_x;
            height = max_y - min_y;
            points = free_points;

            return true;
        }

        public void SetBitmaps(Bitmap picked_bmp, Bitmap without_picked_bmp)
        {
            this.picked_bmp = picked_bmp;
            this.without_picked_bmp = without_picked_bmp;
            rotate_bmp = new Bitmap(picked_bmp);
            resize_bmp = new Bitmap(picked_bmp);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(x, y, width, height);
        }

        public bool IsInsidePickedArea(Point point)
        {
            if (point.X > x && point.X < x + width && point.Y > y && point.Y < y + height)
                return true;

            return false;
        }

        public void MovePoints(int dx, int dy)
        {
            for (int i = 0; i < points.Count; i++)
                points[i] = new Point(points[i].X + dx, points[i].Y + dy);
        }

        public void RotateImage(float angle)
        {
            (picked_bmp, x, y) = ImageUtility.RotateImage(rotate_bmp, picked_bmp, angle, x, y);
        }

        #region IDisposable Support

        private bool _disposed_value = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed_value) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                    picked_bmp?.Dispose();
                    without_picked_bmp?.Dispose();
                    rotate_bmp?.Dispose();
                    resize_bmp?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed_value = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PickedArea()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }

    public static class ImageUtility
    {
        public static (Bitmap, int, int) RotateImage(Bitmap origin_bmp, Bitmap cur_bmp, double degree, int cx, int cy)
        {

            while (degree >= 360)
                degree -= 360;

            OpenCvSharp.Mat src = OpenCvSharp.Extensions.BitmapConverter.ToMat(origin_bmp);
            int width = src.Width;
            int height = src.Height;

            OpenCvSharp.Mat dst = new OpenCvSharp.Mat();
            OpenCvSharp.Mat matrix = OpenCvSharp.Cv2.GetRotationMatrix2D(new OpenCvSharp.Point2f(width / 2, height / 2), degree, 1);

            double scale = 1.0;
            double radian = Math.PI * degree / 180.0;
            double sin = Math.Sin(radian);
            double cos = Math.Cos(radian);

            int bound_w = (int)((height * scale * Math.Abs(sin)) + (width * scale * Math.Abs(cos)));
            int bound_h = (int)((height * scale * Math.Abs(cos)) + (width * scale * Math.Abs(sin)));

            double new_mat_w = matrix.Get<double>(0, 2) + ((bound_w / 2) - (width / 2));
            double new_mat_h = matrix.Get<double>(1, 2) + ((bound_h / 2) - (height / 2));
            matrix.Set(0, 2, new_mat_w);
            matrix.Set(1, 2, new_mat_h);

            Point center_point = new Point(cx + (cur_bmp.Width / 2), cy + (cur_bmp.Height / 2));
            OpenCvSharp.Cv2.WarpAffine(src, dst, matrix, new OpenCvSharp.Size(bound_w, bound_h));
            Bitmap new_bmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst);

            cx = center_point.X - (new_bmp.Width / 2);
            cy = center_point.Y - (new_bmp.Height / 2);

            src.Dispose();
            dst.Dispose();
            matrix.Dispose();

            return (new_bmp, cx, cy);
        }

        public static Bitmap GetResizedBitmap(Bitmap src_image, int width, int height)
        {
            var dest_rect = new Rectangle(0, 0, width, height);
            Bitmap dest_image = new Bitmap(width, height);

            dest_image.SetResolution(src_image.HorizontalResolution, src_image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(dest_image)) {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrap_mode = new ImageAttributes();
                wrap_mode.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(src_image, dest_rect, 0, 0, src_image.Width, src_image.Height, GraphicsUnit.Pixel, wrap_mode);
            }

            return dest_image;
        }

        public static Bitmap GetNormalizedSizeImg(Bitmap src_image)
        {
            Bitmap new_image = src_image;

            int width = src_image.Width;
            int height = src_image.Height;

            while (height >= 500 || width >= 900) {
                width = (int)(width * 0.75);
                height = (int)(height * 0.75);
            }

            if (width != src_image.Width || height != src_image.Height) {
                new_image = GetResizedBitmap(new_image, width, height);
            }

            return new_image;
        }

        public static Bitmap BmpDrawToBmp(Bitmap back_image, Bitmap fore_image, int x, int y)
        {
            Bitmap new_back_image = new Bitmap(back_image);

            var fore_rect = new Rectangle(x, y, fore_image.Width, fore_image.Height);
            using (Graphics g = Graphics.FromImage(new_back_image)) {
                //g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrap_mode = new ImageAttributes();
                wrap_mode.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(fore_image, fore_rect, 0, 0, fore_image.Width, fore_image.Height, GraphicsUnit.Pixel, wrap_mode);
            }

            return new_back_image;
        }

        public static Bitmap BmpDrawToBmp(Bitmap back_image, Bitmap fore_image, Point pos)
        {
            return BmpDrawToBmp(back_image, fore_image, pos.X, pos.Y);
        }

        public static Bitmap GetImageFromURL(string url, WebClient web_client = null)
        {
            try {
                Bitmap download_image;
                if (web_client == null) {
                    WebClient downloader = FileManager.GetHttpClient();
                    Stream image_stream = downloader.OpenRead(url);
                    download_image = Image.FromStream(image_stream) as Bitmap;
                    downloader.Dispose();
                } else {
                    download_image = Image.FromStream(web_client.OpenRead(url)) as Bitmap;
                }

                download_image.SetResolution(96, 96);
                return download_image;
            } catch (Exception) {
                return null;
            }
        }

        public static Bitmap MakeImageWithArea(Bitmap source_bm, List<Point> points)
        {
            Bitmap bm = new Bitmap(source_bm.Width, source_bm.Height);

            using (Graphics gr = Graphics.FromImage(bm)) {
                gr.Clear(Color.Transparent);

                using Brush brush = new TextureBrush(source_bm);
                gr.FillPolygon(brush, points.ToArray());
            }

            return bm;
        }

        public static Bitmap MakeImageWithoutArea(Bitmap source_bm, List<Point> points)
        {
            Bitmap bm = new Bitmap(source_bm);

            using (Graphics gr = Graphics.FromImage(bm)) {
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(points.ToArray());
                gr.SetClip(path);
                gr.Clear(Color.Transparent);
                gr.ResetClip();
            }

            return bm;
        }

        public static void RemoveImageBack(Bitmap image, int tolerance)
        {
            for (int i = 0; i < image.Width; i++) {
                for (int j = 0; j < image.Height; j++) {
                    Color temp = image.GetPixel(i, j);
                    if (temp.R >= tolerance && temp.G >= tolerance && temp.B >= tolerance) {
                        int diff1 = Math.Abs(temp.R - temp.G);
                        int diff2 = Math.Abs(temp.R - temp.B);
                        int diff3 = Math.Abs(temp.G - temp.B);
                        if (diff1 <= 30 && diff2 <= 30 && diff3 <= 30)
                            image.MakeTransparent(temp);
                    }
                }
            }
        }

        public static Image ByteArrayToImage(byte[] byte_array_in)
        {
            MemoryStream ms = new MemoryStream(byte_array_in);
            Image return_image = Image.FromStream(ms);
            return return_image;
        }

        public static List<Bitmap> CropBitmaps(List<Bitmap> bitmaps)
        {
            for (int i = 0; i < bitmaps.Count; i++)
                bitmaps[i] = AutoCrop(bitmaps[i]);

            return bitmaps;
        }

        public static Bitmap AutoCrop(this Bitmap bmp)
        {
            if (Image.GetPixelFormatSize(bmp.PixelFormat) != 32)
                throw new InvalidOperationException("Autocrop currently only supports 32 bits per pixel images.");

            var crop_color = Color.Transparent;

            var bottom = 0;
            var left = bmp.Width;
            var right = 0;
            var top = bmp.Height;

            var bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

            unsafe {
                var data_ptr = (byte*)bmp_data.Scan0;

                for (var y = 0; y < bmp.Height; y++) {
                    for (var x = 0; x < bmp.Width; x++) {
                        var rgb_ptr = data_ptr + (x * 4);

                        var b = rgb_ptr[0];
                        var g = rgb_ptr[1];
                        var r = rgb_ptr[2];
                        var a = rgb_ptr[3];

                        if (a != 0) {
                            if (x < left)
                                left = x;

                            if (x >= right)
                                right = x + 1;

                            if (y < top)
                                top = y;

                            if (y >= bottom)
                                bottom = y + 1;
                        }
                    }

                    data_ptr += bmp_data.Stride;
                }
            }

            bmp.UnlockBits(bmp_data);

            if (left < right && top < bottom)
                return bmp.Clone(new Rectangle(left, top, right - left, bottom - top), bmp.PixelFormat);

            return null;
        }
    }
}