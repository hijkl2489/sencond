using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace Core.Sip.Client.HLA
{
    class NPort
    {
        // 端口控制函数
        [DllImport("PCOMM.DLL")]
        public static extern int sio_open(int port);

        [DllImport("PCOMM.DLL")]
        public static extern int sio_close(int port);

        [DllImport("PCOMM.DLL")]
        public static extern int sio_ioctl(int port, int baud, int mode);

        // 输入输出控制
        [DllImport("PCOMM.DLL")]
        public static extern int sio_read(int port, byte[] buf, int len);

        [DllImport("PCOMM.DLL")]
        public static extern int sio_write(int port, byte[] buf, int len);

        // 端口状态
        [DllImport("PCOMM.DLL")]
        public static extern int sio_lstatus(int port);
        [DllImport("PCOMM.DLL")]
        public static extern int sio_iqueue(int port);
        [DllImport("PCOMM.DLL")]
        public static extern int sio_oqueue(int port);
        [DllImport("PCOMM.DLL")]
        public static extern int sio_Tx_hold(int port);
        [DllImport("PCOMM.DLL")]
        public static extern int sio_getbaud(int port);
        [DllImport("PCOMM.DLL")]
        public static extern int sio_getmode(int port);
        [DllImport("PCOMM.DLL")]
        public static extern int sio_getflow(int port);
        [DllImport("PCOMM.DLL")]
        public static extern int sio_data_status(int port);

        //事件服务
        public delegate void termCALLBACK(UInt16 port);
        [DllImport("PCOMM.DLL")]
        public static extern int sio_term_irq(int port, termCALLBACK pfn, char code);


    }
}
