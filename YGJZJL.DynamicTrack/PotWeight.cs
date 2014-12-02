using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.DynamicTrack
{
    /// <summary>
    /// 动轨采集数据类
    /// </summary>
    public class PotWeight
    {
        private string _potNo;//罐号

        public string PotNo
        {
            get { return _potNo; }
            set { _potNo = value; }
        }
        private double _potWeight;//重量

        public double Weight
        {
            get { return _potWeight; }
            set { _potWeight = value; }
        }
        private double _speed;//速度

        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        private string weighTime;//计量时间

        public string WeighTime
        {
            get { return weighTime; }
            set { weighTime = value; }
        }
    }
}
