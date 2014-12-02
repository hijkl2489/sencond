using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace YGJZJL.Car
{
    public partial class CoreInfraredRay : Control
    {
         private int m_nLineWidth;//线宽
        private bool m_bConnected;//是否连通
        private Bitmap m_MemBmp = null;

        public CoreInfraredRay()
        {
            m_nLineWidth = 4;
            m_bConnected = true;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Draw(pe.Graphics, 0, 0, Width, Height);
            
            base.OnPaint(pe);
        }

        private void Draw(Graphics graphics, int x, int y, int w, int h)
        {
            if (m_MemBmp == null)
            {
                m_MemBmp = new Bitmap(this.Width, this.Height);
            }

            Graphics g = Graphics.FromImage(m_MemBmp);
            g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            //短线的DashPattern数组
            float[] Short = new float[2];
            Short[0] = 3f;
            Short[1] = 2f;

            Pen pen = new Pen(Color.Black, m_nLineWidth);
            Pen penSolid = new Pen(ForeColor, m_nLineWidth);
            Pen pen1 = new Pen(ForeColor, 1f);
            Pen penDash = new Pen(ForeColor, 1f);
            penDash.DashStyle = DashStyle.Dash;
            penDash.DashCap = DashCap.Flat;
            penDash.DashPattern = Short;

            if (m_bConnected)
            {
                g.DrawLine(pen, x, y, x, y + h);
                g.DrawLine(pen, x + w - 1, y, x + w - 1, y + h);
                g.DrawLine(penDash, x, y + h / 2, x + w, y + h / 2);
                g.DrawLine(pen1, x + w - m_nLineWidth, y + h / 2, x + w - m_nLineWidth - 3, y + h / 2 - 3);
                g.DrawLine(pen1, x + w - m_nLineWidth, y + h / 2, x + w - m_nLineWidth - 3, y + h / 2 + 3);
            }
            else
            {
                g.DrawLine(pen, x, y, x, y + h);
                g.DrawLine(pen, x + w - 1, y, x + w - 1, y + h);
                g.DrawLine(penDash, x, y + h / 2, x + w / 2, y + h / 2);
                g.DrawLine(pen1, x + w / 2 - m_nLineWidth / 2, y + h / 2, x + w / 2 - m_nLineWidth / 2 - 3, y + h / 2 - 3);
                g.DrawLine(pen1, x + w / 2 - m_nLineWidth / 2, y + h / 2, x + w / 2 - m_nLineWidth / 2 - 3, y + h / 2 + 3);

                g.DrawLine(penSolid, (float)(x + w / 2.0), (float)(y + h / 4.0), (float)(x + w / 2.0), (float)(y + h / 4.0 * 3.0));
            }

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

        /// <summary>
        /// 禁止通行
        /// </summary>
        public bool Connected
        {
            get
            {
                return m_bConnected;
            }
            set
            {
                m_bConnected = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// 画笔宽度
        /// </summary>
        public int LineWidth
        {
            get
            {
                return m_nLineWidth;
            }
            set
            {
                if (value > Width / 3 - 3)
                {
                    return;
                }
                m_nLineWidth = value;
                this.Refresh();
            }
        }
    }
}
