using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Diagnostics;
using Infragistics.Win.UltraWinGrid;
using System.IO;

namespace YGJZJL.PublicComponent
{
    public class LimitQueryTime
    {
        public LimitQueryTime()
        { 

        }
        //public bool ParseTime(DateTime beginTime ,DateTime endTime) 
        //{
        //    string strPlusTime = (endTime - beginTime).ToString().Substring(0, 2);
        //    int strTime = Convert.ToInt32(strPlusTime);
        //    if (strTime > 60)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        public bool ParseTime(DateTime beginTime, DateTime endTime)
        {
            //string strPlusTime = (endTime - beginTime).ToString().Substring(0, 2);
            double strTime = endTime.Subtract(beginTime).TotalDays;
            //int strTime = Convert.ToInt32(strPlusTime);
            if (strTime > 60)
            {
                MessageBox.Show("所选时间区间大于 60 天，数据量过大，请从新选择时间区间！");
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
