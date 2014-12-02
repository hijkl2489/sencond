using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
namespace Core.Sip.Client.Meas 
{
    public class CoreIoLogik : CoreDevice, IDevice
    {

        #region <定义成员>
        Int32[] _connection = null;
        string _ip;             // 设备 IP 地址
        ushort _port;           // 设备 端口
        uint _timeout;          // 连接超时
        string _password;       // 密码
        bool[] _DO = null;      //DO状态值
        bool[] _DI = null;      //DI状态
        #endregion      
 
        #region <属性>
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public int Port
        {
            get { return _port; }
            set { _port = Convert.ToUInt16(value); }
        }
        public bool[] DO
        {
            get { return _DO; }
            set { _DO = value; }
        }
        public bool[] DI
        {
            get { return _DI; }
            set { _DI = value; }            
        }
        #endregion
        public CoreIoLogik()
        {
            _connection = new Int32[1];
            _ip = "10.25.3.11";
            _port = 502;
            _timeout = 500;
            _password = "";
            _DO = new bool[6];
            _DI = new bool[6];
        
        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="configParam">逗号为分隔符的参数字符串</param>
        /// <returns></returns>
        public bool Init(string configParam)
        {
            string[] strParam = configParam.Split(new char[] { ',' });
            IP = strParam[0];
            if (strParam.Length > 1)
            {
                Port = Convert.ToInt32(strParam[1]);
            }
            return true;
        }
        /// <summary>
        /// 初始化IOLogik
        /// </summary>
        /// <returns>成功返回0,失败返回错误代码</returns>
        public bool Open()
        {
            int ret = 0;
            // 初始化SDK
            ret = MXIO_NET.MXEIO_Init();
            if (ret != 0) return false;
            //连接IOLogik
            ret = MXIO_NET.MXEIO_E1K_Connect(Encoding.UTF8.GetBytes(_ip), _port, _timeout, _connection, System.Text.Encoding.UTF8.GetBytes(_password));
            if (ret != 0) return false;
            return true;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            int ret = 0;
            ret = MXIO_NET.MXEIO_Disconnect(_connection[0]);
            if (ret != 0) return false;
            MXIO_NET.MXEIO_Exit();
            return true;
        }
        /// <summary>
        /// 将二进制位转换成整数
        /// </summary>
        /// <param name="ba">位集合</param>
        /// <returns></returns>
        private int BitArray2Int(BitArray ba)
        {
            Int32 ret = 0;
            for (Int32 i = 0; i < ba.Length; i++)
            {
                if (ba.Get(i))
                {
                    ret |= (1 << i);
                }
            }
            return ret;
        }

        /// <summary>
        /// 写DO
        /// </summary>
        /// <param name="WriteValue">写入值序列</param>
        /// <returns>成功返回0,失败返回错误代码</returns>
        public bool WriteDO()
        {
            int ret = 0;
            byte bytCount = 6;
            byte bytStartChannel = 0;            
            BitArray bits = new BitArray(_DO);            
            UInt32 dwSetDOValue = Convert.ToUInt32(BitArray2Int(bits));
            ret = MXIO_NET.E1K_DO_Writes(_connection[0], bytStartChannel, bytCount, dwSetDOValue);
            if (ret != 0) return false;
            return true;
        }

        /// <summary>
        /// 读DO
        /// </summary>
        /// <returns>成功返回DO序列值,失败返回-1</returns>
        public bool[] ReadDO()
        {
            int reVal = 0;

            byte bytCount = 6;
            byte bytStartChannel = 0;
            UInt32[] dwGetDOValue = new UInt32[1];
            reVal = MXIO_NET.E1K_DO_Reads(_connection[0], bytStartChannel, bytCount, dwGetDOValue);
            byte[] d = new byte[1];
            d[0] = Convert.ToByte(dwGetDOValue[0]);
            BitArray bits = new BitArray(d);
            for (int i = 0; i < _DO.Length; i++ )
            {
                _DO[i] = bits.Get(i);
            }
            return _DO;          
        }

        /// <summary>
        /// 读DI
        /// </summary>
        /// <returns>成功返回DI序列值,失败返回-1</returns>
        public bool[] ReadDI()
        {
            int reVal = 0;

            byte bytCount = 6;
            byte bytStartChannel = 0;
            UInt32[] dwGetDIValue = new UInt32[1];
            reVal = MXIO_NET.E1K_DI_Reads(_connection[0], bytStartChannel, bytCount, dwGetDIValue);
            byte[] d = new byte[1];
            d[0] = Convert.ToByte(dwGetDIValue[0]);
            BitArray bits = new BitArray(d);
            for (int i = 0; i < _DI.Length; i++)
            {
                _DI[i] = bits.Get(i);
            }
            return _DI;            
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns>成功返回0,失败返回错误代码</returns>
        public int Moxa_DisConnect()
        {
            int reVal = 0;
            reVal = MXIO_NET.MXEIO_Disconnect(_connection[0]);
            return reVal;
        }
        /// <summary>
        /// 打开开关
        /// </summary>
        /// <param name="index">DO索引号0～5</param>
        public bool OpenSwitch(int index)
        {
            _DO[index] = true;
            return WriteDO();
        }

        /// <summary>
        /// 关闭开关
        /// </summary>
        /// <param name="index">DO索引号0～5</param>
        public bool CloseSwith(int index)
        {
            _DO[index] = false;
            return WriteDO();
        }

    }
}
