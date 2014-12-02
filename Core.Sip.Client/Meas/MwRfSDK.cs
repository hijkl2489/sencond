using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Core.Sip.Client.Meas
{
    class MwRfSDK
    {
        #region <API 接口>

        [DllImport("mwrf32.dll", EntryPoint = "rf_init", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int rf_init(Int16 port, int baud);

        [DllImport("mwrf32.dll", EntryPoint = "rf_halt", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_halt(int icdev);

        [DllImport("mwrf32.dll", EntryPoint = "rf_anticoll", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_anticoll(int icdev, int bcnt, out uint snr);

        [DllImport("mwrf32.dll", EntryPoint = "rf_reset", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_reset(int icdev, int msec);


        [DllImport("mwrf32.dll", EntryPoint = "rf_request", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_request(int icdev, int mode, out UInt16 tagtype);

        [DllImport("mwrf32.dll", EntryPoint = "rf_beep", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_beep(int icdev, int msec);

        [DllImport("mwrf32.dll", EntryPoint = "a_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)]byte[] hex, int len);

        [DllImport("mwrf32.dll", EntryPoint = "rf_load_key", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_load_key(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);

        [DllImport("mwrf32.dll", EntryPoint = "hex_a", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 hex_a([MarshalAs(UnmanagedType.LPArray)]byte[] hex, [MarshalAs(UnmanagedType.LPArray)]byte[] asc, int len);


        [DllImport("mwrf32.dll", EntryPoint = "rf_authentication", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_authentication(int icdev, int mode, int secnr);

        // ML卡验证密码
        /*
         * 参 数：icdev：通讯设备标识符
         * _Mode：密码验证模式mode_auth
         * KeyNr：=0
         * Adr：=0
         * 返 回：成功则返回 0
         */
        [DllImport("mwrf32.dll", EntryPoint = "rf_authentication_2")]
        public static extern int rf_authentication_2(int icdev, byte _Mode, byte KeyNr, byte Adr);

        [DllImport("mwrf32.dll", EntryPoint = "rf_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_read(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);
        [DllImport("mwrf32.dll", EntryPoint = "rf_read_hex", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]       
        public static extern Int16 rf_read_hex(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] buff32);

        [DllImport("mwrf32.dll", EntryPoint = "rf_select", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,
     CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern Int16 rf_select(int icdev, uint snr, out byte size);

        [DllImport("mwrf32.dll", EntryPoint = "rf_write", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_write(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        [DllImport("mwrf32.dll", EntryPoint = "rf_write_hex", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_write_hex(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] buff32);

        [DllImport("mwrf32.dll", EntryPoint = "rf_exit", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        //说明：    关闭通讯口
        public static extern Int16 rf_exit(int icdev);

        
        /// icdev：通讯设备标识符
        ///_Mode：寻卡模式mode_card
        ///_Snr：返回的卡序列号
        ///返 回：成功则返回 0
        [DllImportAttribute("mwrf32.dll", EntryPoint = "rf_card", CallingConvention = CallingConvention.StdCall)]
        public static extern short rf_card(int icdev, byte _Mode, ref uint _Snr);

        #endregion
     
    }
}
