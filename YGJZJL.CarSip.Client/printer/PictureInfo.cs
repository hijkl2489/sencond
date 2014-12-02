using System;
using System.Collections.Generic;
using System.Text;

namespace YGJZJL.CarSip.Client.Printer
{
    public class PictureInfo
    {
        private string picPath = string.Empty;

        public string PicPath
        {
            get { return picPath; }
            set { picPath = value.Contains(":") ? value : AppDomain.CurrentDomain.BaseDirectory + value; }
        }
        private double picTop = 0;

        public double PicTop
        {
            get { return picTop; }
            set { picTop = value; }
        }
        private double picLeft = 0;

        public double PicLeft
        {
            get { return picLeft; }
            set { picLeft = value; }
        }
        private double picWidth = double.MinValue;

        public double PicWidth
        {
            get { return picWidth; }
            set { picWidth = value; }
        }
        private double picHeight = double.MinValue;

        public double PicHeight
        {
            get { return picHeight; }
            set { picHeight = value; }
        }
    }
}
