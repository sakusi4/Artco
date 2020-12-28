using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Artco
{
    public class ActivatedSprite : Sprite, IDisposable
    {
        private List<Bitmap> _img_list = new List<Bitmap>();
        private List<Bitmap> _org_img_list = new List<Bitmap>();
        private List<Bitmap> _zoomed_img_list = new List<Bitmap>();
        private int _cur_img_num;
        private int _x = new Random(Guid.NewGuid().GetHashCode()).Next(800);
        private int _y = new Random(Guid.NewGuid().GetHashCode()).Next(400);
        private int _arrow = -1;
        private int _angle;
        private bool _is_visible = true;
        private bool _is_cw;
        private string _speak_text;

        public SpriteView sprite_view { get; set; }
        public CodeEditor code_editor { get; set; }
        public LoopStack loop_stack { get; set; } = new LoopStack();
        public RuntimeSprite sprite_runner { get; set; }
        public List<List<Block>> code_list { get; set; } = new List<List<Block>>();
        public List<ManualResetEvent> wait_signal_obj { get; set; } = new List<ManualResetEvent>();
        public List<ActivatedSprite> cloned_sprite_list { get; set; } = new List<ActivatedSprite>();
        public int sarrow { get; set; } // start arrow
        public int sx { get; set; } // start loc x
        public int sy { get; set; } // start loc y
        public int[] pc { get; set; } // program counter

        private ActivatedSprite() { }

        public ActivatedSprite(Sprite sprite) : base(sprite)
        {
            var tmp_imgs = sprite.GetTmpImgList();

            if (tmp_imgs == null) {
                Bitmap img = ImageUtility.GetImageFromURL(remote_path);
                org_img_list.Add(ImageUtility.GetNormalizedSizeImg(img));

                // 사용자 이미지의 경우 배경 제거
                if (is_remove_back) {
                    ImageUtility.RemoveImageBack(org_img_list[0], 100);
                    org_img_list = ImageUtility.CropBitmaps(org_img_list);
                }

                MakeZoomedImg(org_img_list[0]);
                _img_list.Add(new Bitmap(org_img_list[0]));
            } else {
                org_img_list = ActivatedSpriteController.CloneBitmapList(tmp_imgs);

                for (int i = 0; i < org_img_list.Count; i++) {
                    org_img_list[i] = ImageUtility.GetNormalizedSizeImg(org_img_list[i]);
                    MakeZoomedImg(org_img_list[i]);
                    _img_list.Add(new Bitmap(org_img_list[i]));
                }
            }

            sprite_view = new SpriteView(this, name, _img_list[0]);
            code_editor = new CodeEditor(this);
        }

        public ActivatedSprite GetClone()
        {
            ActivatedSprite sprite = new ActivatedSprite {
                name = name,
                x = x,
                y = y
            };

            Bitmap bitmap;
            lock (this) {
                bitmap = new Bitmap(cur_img);
            }

            sprite.org_img_list.Add(bitmap);
            sprite.MakeZoomedImg(sprite.org_img_list[0]);
            sprite.img_list.Add(new Bitmap(sprite.org_img_list[0]));

            return sprite;
        }

        public void ChangeLoc(int dx, int dy)
        {
            var (rate_x, rate_y) = StagePlayer.GetFullScreenSizeFactor();

            x += (StagePlayer.ORCheckFlags(StagePlayer.Flag.FULLSCREEN)) ? (int)(dx * rate_x) : dx;
            y += (StagePlayer.ORCheckFlags(StagePlayer.Flag.FULLSCREEN)) ? (int)(dy * rate_y) : dy;
        }

        public void MakeZoomedImg(Bitmap normal_img)
        {
            var (rate_x, rate_y) = StagePlayer.GetFullScreenSizeFactor();

            zoomed_img_list.Add(ImageUtility.GetResizedBitmap(normal_img, (int)(normal_img.Width * rate_x), (int)(normal_img.Height * rate_y)));
        }

        public Bitmap GetOriginalImg()
        {
            return (StagePlayer.ORCheckFlags(StagePlayer.Flag.FULLSCREEN)) ? new Bitmap(zoomed_img_list[cur_img_num]) : new Bitmap(org_img_list[cur_img_num]);
        }

        public void SetPC(int count)
        {
            pc = new int[count];
            loop_stack.Init(count);
        }

        public int x {
            get {
                lock (this) {
                    return _x;
                }
            }

            set {
                lock (this) {
                    _x = value;
                }
            }
        }

        public int y {
            get {
                lock (this) {
                    return _y;
                }
            }

            set {
                lock (this) {
                    _y = value;
                }
            }
        }

        public int width {
            get {
                lock (this) {
                    return _img_list[cur_img_num].Width;
                }
            }
        }

        public int height {
            get {
                lock (this) {
                    return _img_list[cur_img_num].Height;
                }
            }
        }

        public int arrow {
            get {
                lock (this) {
                    return _arrow;
                }
            }
            set {
                lock (this) {
                    _arrow = value;
                }
            }
        }

        public int angle {
            get {
                lock (this) {
                    return _angle;
                }
            }
            set {
                lock (this) {
                    _angle = value;
                }
            }
        }

        public bool is_visible {
            get {
                lock (this) {
                    return _is_visible;
                }
            }
            set {
                lock (this) {
                    _is_visible = value;
                }
            }
        }

        public bool is_cw {
            get {
                lock (this) {
                    return _is_cw;
                }
            }
            set {
                lock (this) {
                    _is_cw = value;
                }
            }
        }

        public Bitmap cur_img {
            get {
                lock (this) {
                    return _img_list[cur_img_num];
                }
            }
            set {
                lock (this) {
                    _img_list[cur_img_num] = value;
                }
            }
        }

        public int cur_img_num {
            get {
                lock (this) {
                    return _cur_img_num;
                }
            }
            set {
                lock (this) {
                    _cur_img_num = value;
                }
            }
        }

        public List<Bitmap> img_list {
            get {
                lock (this) {
                    return _img_list;
                }
            }
            set {
                lock (this) {
                    _img_list = value;
                }
            }
        }

        public int img_list_count {
            get {
                lock (this) {
                    return _img_list.Count;
                }
            }
        }

        public List<Bitmap> org_img_list {
            get {
                lock (this) {
                    return _org_img_list;
                }
            }
            set {
                lock (this) {
                    _org_img_list = value;
                }
            }
        }

        public List<Bitmap> zoomed_img_list {
            get {
                lock (this) {
                    return _zoomed_img_list;
                }
            }
            set {
                lock (this) {
                    _zoomed_img_list = value;
                }
            }
        }

        public string speak_text {
            get {
                lock (this) {
                    return _speak_text;
                }
            }
            set {
                lock (this) {
                    _speak_text = value;
                }
            }
        }

        #region IDisposable Support

        private bool _disposed_value = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed_value) {
                if (disposing) {
                    for (int i = 0; i < _img_list.Count; i++)
                        _img_list[i]?.Dispose();

                    for (int i = 0; i < org_img_list.Count; i++)
                        org_img_list[i]?.Dispose();

                    for (int i = 0; i < zoomed_img_list.Count; i++)
                        zoomed_img_list[i]?.Dispose();

                    for (int i = 0; i < code_list.Count; i++) {
                        for (int j = 0; j < code_list[i].Count; j++) {
                            code_list[i][j]?.Dispose();
                        }
                    }

                    code_editor?.Dispose();
                    sprite_view?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed_value = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ActivatedSprite()
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

        #endregion IDisposable Support
    }
}
