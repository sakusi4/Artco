using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    class FloatingTextbox
    {
        private readonly TextBox _text_box;
        private enum EdgeType { None, LeftTop, Left, Top, RightTop, Right, RightBottom, Bottom, LeftBottom }
        private EdgeType _cur_size_operation = EdgeType.None;
        private Point _mouse_down_point = new Point(0, 0);

        public FloatingTextbox(Font font, Point location)
        {
            _text_box = new TextBox {
                Multiline = true,
                Location = location,
                Font = font,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Width = 100,
                Height = 30,
                ImeMode = ImeMode.NoControl
            };

            _text_box.MouseDown += TextBox_MouseDown;
            _text_box.MouseUp += TextBox_MouseUp;
            _text_box.MouseMove += TextBox_MouseMove;
        }

        public TextBox GetItem()
        {
            return _text_box;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(_text_box.Location, new Size(_text_box.Width, _text_box.Height));
        }

        private void TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            Control ctl = (Control)sender;
            EdgeType edge = EdgeFind(ctl, e.Location);

            MouseCusorSet(edge);

            if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                switch (_cur_size_operation) {
                    case EdgeType.None:
                        Cursor.Current = Cursors.SizeAll;
                        ctl.Location = new Point(ctl.Left - _mouse_down_point.X + e.X, ctl.Top - _mouse_down_point.Y + e.Y);
                        break;
                    case EdgeType.Left:
                        ctl.Left = ctl.Left - _mouse_down_point.X + e.X;
                        ctl.Width = ctl.Width + _mouse_down_point.X - e.X;
                        break;
                    case EdgeType.Right:
                        ctl.Width = ctl.Width - _mouse_down_point.X + e.X;
                        _mouse_down_point.X = e.X;
                        break;
                    case EdgeType.Top:
                        ctl.Top = ctl.Top - _mouse_down_point.Y + e.Y;
                        ctl.Height = ctl.Height + _mouse_down_point.Y - e.Y;
                        break;
                    case EdgeType.Bottom:
                        ctl.Height = ctl.Height - _mouse_down_point.Y + e.Y;
                        _mouse_down_point.Y = e.Y;
                        break;
                    case EdgeType.LeftTop:
                        ctl.Location = new Point(ctl.Left - _mouse_down_point.X + e.X, ctl.Top - _mouse_down_point.Y + e.Y);
                        ctl.Size = new Size(ctl.Width + _mouse_down_point.X - e.X, ctl.Height + _mouse_down_point.Y - e.Y);
                        break;
                    case EdgeType.RightBottom:
                        ctl.Size = new Size(ctl.Width - _mouse_down_point.X + e.X, ctl.Height - _mouse_down_point.Y + e.Y);
                        _mouse_down_point = e.Location;
                        break;
                    case EdgeType.RightTop:
                        ctl.Top = ctl.Top - _mouse_down_point.Y + e.Y;
                        ctl.Size = new Size(ctl.Width - _mouse_down_point.X + e.X, ctl.Height + _mouse_down_point.Y - e.Y);
                        _mouse_down_point.X = e.X;
                        break;
                    case EdgeType.LeftBottom:
                        ctl.Left = ctl.Left - _mouse_down_point.X + e.X;
                        ctl.Size = new Size(ctl.Width + _mouse_down_point.X - e.X, ctl.Height - _mouse_down_point.Y + e.Y);
                        _mouse_down_point.Y = e.Y;
                        break;
                }
            }
        }

        private void TextBox_MouseUp(object sender, MouseEventArgs e)
        {
            _cur_size_operation = EdgeType.None;
        }

        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                _mouse_down_point = e.Location;
                Control ctl = (Control)sender;
                _cur_size_operation = EdgeFind(ctl, _mouse_down_point);

                if (_cur_size_operation == EdgeType.None)
                    Cursor.Current = Cursors.SizeAll;
            }
        }

        private EdgeType EdgeFind(Control ctl, Point p)
        {
            const int n_edge_thickness = 8;

            EdgeType rtn_val = EdgeType.None;

            if (p.X < n_edge_thickness && p.Y < n_edge_thickness) rtn_val = EdgeType.LeftTop;
            else if (p.X < n_edge_thickness && p.Y > ctl.Height - n_edge_thickness) rtn_val = EdgeType.LeftBottom;
            else if (p.Y < n_edge_thickness && p.X > ctl.Width - n_edge_thickness) rtn_val = EdgeType.RightTop;
            else if (p.X > ctl.Width - n_edge_thickness && p.Y > ctl.Height - n_edge_thickness) rtn_val = EdgeType.RightBottom;
            else if (p.Y < n_edge_thickness) rtn_val = EdgeType.Top;
            else if (p.Y > ctl.Height - n_edge_thickness) rtn_val = EdgeType.Bottom;
            else if (p.X < n_edge_thickness) rtn_val = EdgeType.Left;
            else if (p.X > ctl.Width - n_edge_thickness) rtn_val = EdgeType.Right;

            return rtn_val;
        }

        private void MouseCusorSet(EdgeType edge)
        {
            switch (edge) {
                case EdgeType.None:
                    Cursor.Current = Cursors.Arrow;
                    break;
                case EdgeType.Left:
                case EdgeType.Right:
                    Cursor.Current = Cursors.SizeWE;
                    break;
                case EdgeType.Top:
                case EdgeType.Bottom:
                    Cursor.Current = Cursors.SizeNS;
                    break;
                case EdgeType.LeftTop:
                case EdgeType.RightBottom:
                    Cursor.Current = Cursors.SizeNWSE;
                    break;
                case EdgeType.RightTop:
                case EdgeType.LeftBottom:
                    Cursor.Current = Cursors.SizeNESW;
                    break;
            }
        }
    }
}
