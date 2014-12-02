using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.SquareBilletTransfer
{
    class PoundRoom
    {
        private string m_POINTID;//计量点编码
        private string m_POINTNAME;//计量点名称
        private string m_POINTTYPE;//计量点称重类型

        private string m_VIDEOIP;//硬盘录像机IP
        private string m_VIDEOPORT;//硬盘录像机端口
        private string m_VIDEOUSER;//硬盘录像机用户名
        private string m_VIDEOPWD;//硬盘录像机密码
        private string m_VIDEOTYOE;//硬盘录像机型号

        private int m_VideoHandle;//硬盘录像机句柄，SDK_Login获取后赋值
        private int m_Channel1 = 0;//通道1句柄
        private int m_Channel2 = 1;//通道2句柄
        private int m_Channel3 = 2;//通道3句柄
        private int m_Channel4 = 3;//通道4句柄
        private int m_Channel5 = 4;//通道5句柄
        private int m_Channel6 = 5;//通道6句柄

        //sign
        private bool m_bSigned;//接管

        public bool Signed
        {
            get { return m_bSigned; }
            set { m_bSigned = value; }
        }

        public string POINTID
        {
            get { return m_POINTID; }
            set { m_POINTID = value; }
        }

        public string POINTNAME
        {
            get { return m_POINTNAME; }
            set { m_POINTNAME = value; }
        }

        public string POINTTYPE
        {
            get { return m_POINTTYPE; }
            set { m_POINTTYPE = value; }
        }

        public string VIDEOIP
        {
            get { return m_VIDEOIP; }
            set { m_VIDEOIP = value; }
        }

        public string VIDEOPORT
        {
            get { return m_VIDEOPORT; }
            set { m_VIDEOPORT = value; }
        }

        public string VIDEOUSER
        {
            get { return m_VIDEOUSER; }
            set { m_VIDEOUSER = value; }
        }

        public string VIDEOPWD
        {
            get { return m_VIDEOPWD; }
            set { m_VIDEOPWD = value; }
        }

        public string VIDEOTYOE
        {
            get { return m_VIDEOTYOE; }
            set { m_VIDEOTYOE = value; }
        }

        public int VideoHandle
        {
            get { return m_VideoHandle; }
            set { m_VideoHandle = value; }
        }

        public int Channel1
        {
            get { return m_Channel1; }
            set { m_Channel1 = value; }
        }

        public int Channel2
        {
            get { return m_Channel2; }
            set { m_Channel2 = value; }
        }

        public int Channel3
        {
            get { return m_Channel3; }
            set { m_Channel3 = value; }
        }

        public int Channel4
        {
            get { return m_Channel4; }
            set { m_Channel4 = value; }
        }

        public int Channel5
        {
            get { return m_Channel5; }
            set { m_Channel5 = value; }
        }

        public int Channel6
        {
            get { return m_Channel6; }
            set { m_Channel6 = value; }
        }
    }
}
