using System.Windows.Forms;

namespace Artco
{
    public class TransparentPanel : Panel
    {
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Doublebuffered
                return cp;
            }
        }
    }

    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }
    }

    public class DoubleBufferedFlowPanel : FlowLayoutPanel
    {
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Doublebuffered
                return cp;
            }
        }

        //public DoubleBufferedFlowPanel()
        //{
        //        this.DoubleBuffered = true;
        //        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        //        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        //        this.UpdateStyles();
        //}
    }

    public class StagePanel : Panel
    {
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Doublebuffered
                return cp;
            }
        }

        public StagePanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }
    }
}