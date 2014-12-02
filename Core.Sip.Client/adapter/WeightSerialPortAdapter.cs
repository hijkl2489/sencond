using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
//using log4net;
namespace Core.Sip.Client.Adapter
{
    //1.定义delegate
    public delegate void WeightChangedEventHandler(object sender, WeightEventArgs e);
    public delegate void WeightCompletedEventHandler(object sender, WeightEventArgs e);
    public class WeightEventArgs : EventArgs
    {
        public readonly string value;
        public WeightEventArgs(string value)
        {
            this.value = value;
        }
    }

    public class WeightSerialPortAdapter
    {
  //      public static readonly log4net.ILog log = log4net.LogManager.GetLogger("root");
        public static SerialPort _sp;

        /*
         * 重量变化事件
         * 
         */

        //2.用event 关键字声明事件对象
        public event WeightChangedEventHandler WeightChanged;
        //3.事件触发方法
        protected virtual void OnWeightChanged(WeightEventArgs e)
        {
            if (WeightChanged != null)
                WeightChanged(this, e);
        }

        /*
         * 称重完成事件
         * 
         */

        public event WeightCompletedEventHandler WeightCompleted;
        protected virtual void OnWeightCompleted(WeightEventArgs e)
        {
            if (WeightCompleted != null)
                WeightCompleted(this, e);
        }

        // 基础属性
        byte[] _beginFlag;
        int _fixedBytesLen = 16;
        int _minBytesLen = 16 - 4;
        double _weight = 0;
        double _preWeight = 0;
        double _weightLimit = 600 * 1000;
        int _count = 0;
        public WeightSerialPortAdapter()
        {
            init();
           // log.Debug("构造函数初始化");
        }

        ~WeightSerialPortAdapter()
        {
            if (_sp.IsOpen) _sp.Close();
           // log.Debug("析构函数");
        }

        public void init()
        {
            _sp = new SerialPort();
            _sp.PortName = "COM1";
            _sp.BaudRate = 1200;
            _sp.DataBits = 7;
            _sp.StopBits = StopBits.One;
            _sp.Parity = Parity.Even;
            _sp.ReceivedBytesThreshold = 1;
            _sp.NewLine = Encoding.ASCII.GetString(new byte[] { 0x0D });
            _sp.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
            _fixedBytesLen = 16;
            _minBytesLen = _fixedBytesLen - 4;
            _count = 0;
            _sp.Open();
        }

        //数据解析函数
        public void OnDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string data = _sp.ReadLine();
            int bytesLen = data.Length;
            if (data.Length != _fixedBytesLen) return;   // 数据不符合条件
            String displayWeight = data.Substring(4, 6); // 显示重量(毛重或净重)
            String grossWeight = data.Substring(10, 6);  // 毛重
            try
            {
                _weight = double.Parse(displayWeight);
            }
            catch (Exception ex)
            {
                _weight = _preWeight;
                return;
            }
            // 重量变化
            if (_weight != _preWeight)
            {
                _preWeight = _weight;
                _count = 0;
                RaiseWeightChanged(_weight.ToString());
            }
            else
            {
                _count++;
                if (_count > 8 && _weight > 0) // 稳定值
                {
                    WeightEventArgs arg = new WeightEventArgs(_weight.ToString());
                    WeightCompleted(this, arg);// 称重完成事件
                    _count = 0;
                }
            }
        }

        //引发事件
        public void RaiseWeightChanged(string weight)
        {
            WeightEventArgs e = new WeightEventArgs(weight);
            OnWeightChanged(e);
        }
    }
}
