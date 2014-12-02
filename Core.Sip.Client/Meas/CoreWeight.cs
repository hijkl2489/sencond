using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Core.Sip.Client.Meas
{
    #region <定义称重代理>
    /// <summary>
    /// 重量变化代理
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">称重事件</param>
    public delegate void WeightChangedEventHandler(object sender, WeightEventArgs e);
    /// <summary>
    /// 重量变化代理
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">称重事件</param>
    public delegate void WeightCompletedEventHandler(object sender, WeightEventArgs e);


    /// <summary>
    /// 重量超限代理
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">称重事件</param>
    public delegate void WeightOutSideEventHandle(object sender, WeightEventArgs e);
    #endregion

    #region <定义称重参数>
    /// <summary>
    /// 称重事件定义
    /// </summary>
    public class WeightEventArgs : EventArgs
    {
        private double _value;
        public double Value
        {
            get { return _value; }
            set { this._value = value; }
        }
        public WeightEventArgs()
        {
            _value = 0;
        }
        public WeightEventArgs(double value)
        {
            this._value = value;
        }
        public WeightEventArgs(string value)
        {
            this._value = Double.Parse(value);
        }
    }
    #endregion

    public class CoreWeight : CoreSieralPort
    {
        #region <事件>
        public event WeightChangedEventHandler WeightChanged;
        public event WeightCompletedEventHandler WeightCompleted;
        #endregion

        #region <成员变量>
        protected string _beginFlag;           // 结束标志
        protected string _endFlag;             // 开始标志
        protected int _msgLen = 16;            // 报文长度
        protected int _weightMsgPos = 0;           // 重量开始位置
        protected int _weightMsgLen = 0;             // 重量结束位置
        protected int _weightMsgReverse = 0;   // 字符是否反转  0=不反转  1=反转
        //private double _weight = 0;              // 当前重量 
        protected double _preWeight = -1;           // 前一次称重值 
        protected double _maxWeight = 200 * 1000;  // 最大重量
        protected double _minWeight = 50;
        protected int _precision = 1;
        protected int _count = 0;
        protected int _maxCount = 5;

        // 缓冲区
        protected byte[] _buffer = null;
        // 
        protected bool isWeightComplete = false;

        public bool _receiveFlag = true;
        // 线程
        Thread _thread = null;
        string _strBuf = "";
        #endregion

        #region <属性>

        // 开始标识位
        public string BeginFlag
        {
            get { return this._beginFlag; }
            set { _beginFlag = value; }
        }
        // 结束标志位
        public string EndFlag
        {
            get { return this._endFlag; }
            set
            {
                _endFlag = value;
                SerialPort.NewLine = _endFlag;
            }
        }
        public int MessageLength
        {
            get { return _msgLen; }
            set { _msgLen = value; }
        }
        // 精度设置
        public int Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        public double MaxWeight
        {
            get { return _maxWeight; }
            set { _maxWeight = value; }
        }
        public double MinWeight
        {
            get { return _minWeight; }
            set { _minWeight = value; }
        }
        public int MaxCount
        {
            get { return _maxCount; }
            set { _maxCount = value; }
        }
        #endregion
        //
        #region <构造函数>
        public CoreWeight()
        {
            isWeightComplete = false;
            _thread = null;
            //SerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.OnDataReceived);
        }
        #endregion

        /*
         * 参数用逗号分隔
         * 串口参数(串口名，波特率，校验位，数据位，停止位)，
         * 称重协议(开始位，结束位，报文长度，重量数据开始位置，重量数据开始位置，重量数据长度,数据是否逆转)，
         * 限定值(最小重量，最大重量，稳定值计数器最大值) 
         */
        public override void init(string config)
        {
            string[] strtmp = config.Split(new char[] { ',' });
            SerialPort.PortName = strtmp[0];
            SerialPort.BaudRate = Convert.ToInt32(strtmp[1]);
            SerialPort.Parity = ConvertParity(strtmp[2]);
            SerialPort.DataBits = Convert.ToInt32(strtmp[3]);
            SerialPort.StopBits = ConvertStopBit(strtmp[4]);
            if (strtmp.Length > 5)
            {
                BeginFlag = HexToStr(strtmp[5]);
                EndFlag = HexToStr(strtmp[6]);
                MessageLength = Convert.ToInt32(strtmp[7]);
                _weightMsgPos = Convert.ToInt32(strtmp[8]);
                _weightMsgLen = Convert.ToInt32(strtmp[9]);
                _weightMsgReverse = Convert.ToInt32(strtmp[10]);
            }
            if (strtmp.Length > 11)
            {
                _precision = Convert.ToInt32(strtmp[11]);
            }
            if (strtmp.Length > 12)
            {
                MinWeight = Convert.ToDouble(strtmp[12]);
                MaxWeight = Convert.ToDouble(strtmp[13]);
                _maxCount = Convert.ToInt32(strtmp[14]);
            }
            _strBuf = "";
        } 
        // add 2012-06-06   增加线程
        public bool Open()
        {
            try
            {
                if (Status == DeviceStatus.CLOSE || Status == DeviceStatus.INIT)
                {
                    base.Open();
                    _thread = new Thread(new ThreadStart(FreshData));
                    _thread.Start();
                }
                //           if (WeightChanged != null || WeightCompleted != null)
                //            {

                //            }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        // add 
        public void FreshData()
        {
            while (true)
            {
                Thread.Sleep(150);
                OnDataReceived();
            }
        }
        private bool IsValidDigit(string data)
        {
            if (!(data[0] == '+' || data[0] == '-' || Char.IsNumber(data[0]))) return false;
            for (int i = 1; i < data.Length; i++)
            {
                if (!Char.IsNumber(data[i]))
                    return false;
            }
            return true;
        }

        ////数据解析函数--板坯专用
        //public void OnDataReceived()
        //{
        //    string data = SerialPort.ReadLine();
        //    //if (data == null || data.Length < _msgLen - EndFlag.Length) return;  // 不满足协议长度
        //    //try{
        //    if (SerialPort != null && SerialPort.IsOpen)
        //    {
        //        try
        //        {
        //            _buffer = Encoding.ASCII.GetBytes(data);
        //            //if (_buffer.Length < _msgLen-EndFlag.Length) return;
        //            if (1 == _weightMsgReverse) Array.Reverse(_buffer, _weightMsgPos - 1, _weightMsgLen + 1);
        //            String strWeight = Encoding.ASCII.GetString(_buffer, _weightMsgPos - 1, _weightMsgLen + 1);
        //            if (IsValidDigit(strWeight))
        //            {
        //                double weight = Double.Parse(strWeight);
        //                HandleWeight(weight);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //}


        //数据解析函数
        public void OnDataReceived()
        {
            string data = null;
            int curPos = 0, prePos = 0, tempPos = 0;
            if (SerialPort != null && SerialPort.IsOpen)
            {
                try
                {
                    _strBuf += SerialPort.ReadExisting().ToString().Replace("?", "");//SerialPort.ReadLine();
                    if (!_receiveFlag)
                    {
                        _strBuf = "";
                        return;
                    }
                    while (prePos < _strBuf.Length)
                    {
                        tempPos = prePos;
                        prePos = _strBuf.IndexOf(BeginFlag, prePos);
                        if (prePos == -1) prePos = tempPos;
                        curPos = _strBuf.IndexOf(EndFlag, prePos + 1);
                        if (-1 == curPos)
                        {
                            _strBuf = _strBuf.Substring(prePos);
                            break;
                        }
                        data = _strBuf.Substring(prePos, curPos - prePos + 1);
                        if (data.Length == _msgLen)
                        {
                            _buffer = Encoding.ASCII.GetBytes(data);
                            if (_buffer.Length < _weightMsgPos + _weightMsgLen)
                            {
                                break;
                            }
                            if (1 == _weightMsgReverse) Array.Reverse(_buffer, _weightMsgPos, _weightMsgLen);
                            String strWeight = Encoding.ASCII.GetString(_buffer, _weightMsgPos, _weightMsgLen).Trim();
                            if (IsValidDigit(strWeight))
                            {
                                double weight = Double.Parse(strWeight);
                                HandleWeight(weight);
                            }
                        }
                        prePos = curPos + EndFlag.Length;
                        if (_strBuf.Length == prePos)
                        {
                            _strBuf = "";
                            break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void OnWeightChange(double weight)
        {
            if (WeightChanged == null) return;
            WeightEventArgs arg = new WeightEventArgs(weight);
            WeightChanged(this, arg);
        }

        private void OnWeightCompleted(double weight)
        {
            if (WeightCompleted == null) return;
            WeightEventArgs arg = new WeightEventArgs(weight);
            _count++;
            if (_count > _maxCount) // 稳定值计算判断
            {
                arg.Value = weight;
                WeightCompleted(this, arg);// 称重完成事件
                isWeightComplete = true;
                _count = 0;
            }
        }

        protected virtual void HandleWeight(object weightObj)
        {
            double weight = (double)weightObj;
            // 保存前一次重量
            double preWeight = _preWeight;
            // 重量变化
            if (weight != _preWeight)
            {
                _preWeight = weight;
                OnWeightChange(weight);
            }
            // 清零操作
            if ((weight < _precision && preWeight < _precision) || weight < _minWeight)
            {
                _count = 0;
                isWeightComplete = false;
                return;
            }

            // 称重完成
            if (Math.Abs(weight - preWeight) <= _precision
                && weight < MaxWeight && weight > MinWeight
                && !isWeightComplete)
            {
                OnWeightCompleted(weight);
            }

        }
        public  bool Close()
        {
            if( _thread != null&&_thread.IsAlive) _thread.Abort();
            base.Close();
            return true;
        }

    }
}
