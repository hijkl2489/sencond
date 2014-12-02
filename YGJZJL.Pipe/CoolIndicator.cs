using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;


namespace YGJZJL.Pipe
{
    public partial class CoolIndicator : Control
    {
        private bool m_bGradient;//是否渐变
        private Bitmap m_MemBmp = null;

        public CoolIndicator()
        {
            m_bGradient = true;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (m_bGradient)
            {
                DrawGradient(pe.Graphics, 0, 0, Width, Height);
            }
            else
            {
                DrawFlat(pe.Graphics, 0, 0, Width, Height);
            }

            base.OnPaint(pe);
        }

        private void DrawGradient(Graphics graphics, int x, int y, int w, int h)
        {
            if (m_MemBmp == null)
            {
                m_MemBmp = new Bitmap(this.Width, this.Height);
            }

            Graphics g = Graphics.FromImage(m_MemBmp);
            g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(x, y, w, h);

            PathGradientBrush pthGrBrush = new PathGradientBrush(path);

            Color[] color = { ForeColor };
            pthGrBrush.SurroundColors = color;

            pthGrBrush.CenterColor = Color.White;

            g.FillPath(pthGrBrush, path);

            g.Dispose();

            graphics.DrawImage(m_MemBmp, 0, 0);
        }

        private void DrawFlat(Graphics graphics, int x, int y, int w, int h)
        {
            if (m_MemBmp == null)
            {
                m_MemBmp = new Bitmap(this.Width, this.Height);
            }

            Graphics g = Graphics.FromImage(m_MemBmp);
            g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillEllipse(new SolidBrush(ForeColor), x, y, w, h);

            g.Dispose();

            graphics.DrawImage(m_MemBmp, 0, 0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Width > Height)
                Height = Width;
            else
                Width = Height;

            base.OnSizeChanged(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            return;
        }

        public bool Gradient
        {
            get
            {
                return m_bGradient;
            }
            set
            {
                m_bGradient = value;
                this.Refresh();
            }
        }
    }
}
