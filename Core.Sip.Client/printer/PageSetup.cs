using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Sip.Client.Printer
{
    /// <summary>
    /// 打印纸张设置
    /// </summary>
    public class PageSetup
    {
        private int _Orientation = 0;//1横向 0纵向

        public int Orientation
        {
            get { return _Orientation; }
            set { _Orientation = value; }
        }
        private bool _IsCustom = false;//是否自定义纸张

        public bool IsCustom
        {
            get { return _IsCustom; }
            set { _IsCustom = value; }
        }
        private int width = 0;//自定义纸张宽度

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        private int length = 0;//自定义纸张长度

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private double _TopMargin = 0;

        public double TopMargin
        {
            get { return _TopMargin; }
            set { _TopMargin = value; }
        }
        private double _BottomMargin = 0;

        public double BottomMargin
        {
            get { return _BottomMargin; }
            set { _BottomMargin = value; }
        }
        private double _LeftMargin = 0;

        public double LeftMargin
        {
            get { return _LeftMargin; }
            set { _LeftMargin = value; }
        }
        private double _RightMargin = 0;

        public double RightMargin
        {
            get { return _RightMargin; }
            set { _RightMargin = value; }
        }
    }
}
