using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using YGJZJL.CarSip.Client.Meas;

namespace YGJZJL.CarSip.Client.App
{
    public enum LCD_PICTURE
    {
        WELCOME = 0,
        TEXT    = 1
    }

    public class HgLcd : LCDScreen
    {
        private int _x = 410;
        private int[] _y;
        private int _offsetY = 47;
        private object[] _data;
        private LCD_PICTURE _lcdStatus;

        #region <属性>
        public LCD_PICTURE LcdStatus
        {
            get { return _lcdStatus; }
            set { _lcdStatus = value; }
        }

        public object[] Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        #endregion
        #region <构造函数>
        public HgLcd()
        {
            // 初始化坐标
            _x = 340;
            _offsetY = 47;
            _y = new int[10];
            _y[0] = 225;
            for (int i = 1; i < _y.Length; i++)
            {
                _y[i] = _y[i - 1]+47;
            }

        }
        public bool Open()
        {
            if (base.Open())
            {
                ClearScreen();
                DrawPicture((int)LCD_PICTURE.WELCOME);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region <公共方法>
        public void DisplayData()
        {
            if (_data == null) return;
            ClearScreen();
            DrawPicture((int)LCD_PICTURE.TEXT);
            for (int i = 0; i < _data.Length; i++)
            {
                WriteText((ushort)_x, (ushort)_y[i], Color.Yellow, _data[i].ToString());
            }
        }
        #endregion


    }
}
