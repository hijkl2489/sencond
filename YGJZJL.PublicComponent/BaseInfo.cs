using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
using SDK_Com;
using System.Net;
using System.Management;

namespace YGJZJL.PublicComponent
{
    public partial class BaseInfo : FrmBase
    {
        private string fileName1;    //图片1保存名称
        private string fileName2;    //图片2保存名称
        private string fileName3;    //图片3保存名称

        private byte[] m_ImageStream; //图片流
        private DataTable m_dtImage; //图片内存表
        private string sZL;//图片上打印重量

        //SDK_Com.HKDVR sdk;

        string stRunPath = System.Environment.CurrentDirectory;
        public SystemTime SynTime = new SystemTime();
        /// <summary>
        /// 定义矩形框
        /// </summary>
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public struct SystemTime
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        public BaseInfo()
        {
            InitializeComponent();
        }
        public BaseInfo(object obb)
        {
           
            this.ob = (OpeBase)obb;
        }
        public byte[] ImageStream
        {
            get
            {
                return m_ImageStream;
            }
        }
        public DataTable dtImage
        {
            get
            {
                return m_dtImage;
            }
        }

        #region 引用重要方法
        //设置本机系统时间
        [DllImport("kernel32.dll ")]
        public static extern int SetLocalTime(ref   SystemTime lpSystemTime);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(int hwnd, ref RECT rc);

        [DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreateDC(
                string lpszDriver,   //   驱动名称   
                string lpszDevice,   //   设备名称   
                string lpszOutput,   //   无用，可以设定位"NULL"   
                IntPtr lpInitData   //   任意的打印机数据   
                );

        [DllImport("Gdi32.dll")]
        public static extern bool BitBlt(
                IntPtr hdcDest,   //   目标设备的句柄   
                int nXDest,   //   目标对象的左上角的X坐标   
                int nYDest,   //   目标对象的左上角的X坐标   
                int nWidth,   //   目标对象的矩形的宽度   
                int nHeight,   //   目标对象的矩形的长度   
                IntPtr hdcSrc,   //   源设备的句柄   
                int nXSrc,   //   源对象的左上角的X坐标   
                int nYSrc,   //   源对象的左上角的X坐标   
                System.Int32 dwRop   //   光栅的操作值   
                );

        #endregion

        #region 获取图片方法
        /// <summary>
        /// 抓取句柄所指窗口
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        private Bitmap getscreenfromhandle(int hwnd)
        {
            RECT rc = new RECT();
            GetWindowRect(hwnd, ref  rc);
            return getscreen(rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top);

        }

        private Bitmap getscreen(int left, int top, int width, int height)  //获得屏幕指定区域 
        {
            IntPtr dc1 = CreateDC("DISPLAY", null, null, (IntPtr)null);
            Graphics newGraphics = Graphics.FromHdc(dc1);
            Bitmap img = new Bitmap(width, height, newGraphics);
            Graphics thisGraphics = Graphics.FromImage(img);
            IntPtr dc2 = thisGraphics.GetHdc();
            IntPtr dc3 = newGraphics.GetHdc();
            BitBlt(dc2, 0, 0, width, height, dc3, left, top, 13369376);
            thisGraphics.ReleaseHdc(dc2);
            newGraphics.ReleaseHdc(dc3);
            return img;
        }
        #endregion

        #region 抓图并保存
        ///// <summary>
        ///// 抓三张图并保存(PictureBox命名为pictureBox1,pictureBox2,pictureBox3)  File为文件夹名字， strTPID为Guid
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void GraspImage(string strNumber, string File, PictureBox pic1, PictureBox pic2, PictureBox pic3, string strTPID)
        //{
        //    //DateTime now = DateTime.Now;
        //    //strNumber = strJLDID + "-" + now.ToString("yyyyMMddHHmmss");

        //    fileName1 = strNumber + "1.bmp";
        //    fileName2 = strNumber + "2.bmp";
        //    fileName3 = strNumber + "3.bmp";

        //    //抓第一张图
        //    try
        //    {
        //        Bitmap img = getscreenfromhandle((int)pic1.Handle);
        //        img.Save(stRunPath + "\\" + File + "\\" + fileName1, ImageFormat.Bmp);
        //    }
        //    catch (System.Exception error)
        //    {
        //        MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    //抓第二张图
        //    try
        //    {
        //        Bitmap img = getscreenfromhandle((int)pic2.Handle);
        //        img.Save(stRunPath + "\\" + File + "\\" + fileName2, ImageFormat.Bmp);
        //    }
        //    catch (System.Exception error)
        //    {
        //        MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    //抓第三张图
        //    try
        //    {
        //        Bitmap img = getscreenfromhandle((int)pic3.Handle);
        //        img.Save(stRunPath + "\\" + File + "\\" + fileName3, ImageFormat.Bmp);
        //    }
        //    catch (System.Exception error)
        //    {
        //        MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    byte[] TP1 = GetImageFile(File, strNumber, 1);
        //    byte[] TP2 = GetImageFile(File, strNumber, 2);
        //    byte[] TP3 = GetImageFile(File, strNumber, 3);

        //    CoreClientParam ccp = new CoreClientParam();
        //    ccp.ServerName = "Core.KgMcms.CarWeigh.WeighMeasureInfo";
        //    ccp.MethodName = "SaveTPData";
        //    ccp.ServerParams = new object[] { strTPID, TP1, TP2, TP3 };

        //    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        //}
        /// <summary>
        /// 轨道衡，抓两张图并保存(PictureBox命名为pictureBox1,pictureBox2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSaveGDHImage(PictureBox pic1, PictureBox pic2, string strTPID)
        {
            string strNumber = "KgGHD";

            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic2.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            byte[] TP1 = GetImageFile(strNumber, 1);
            byte[] TP2 = GetImageFile(strNumber, 2);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveTwoTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 钢坯，抓一张图并使用已保存的另一张图保存(PictureBox命名为pictureBox1,pictureBox2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public Bitmap Getscreenfromhandle(int hwnd)
        {
            return getscreenfromhandle(hwnd);
        }
        public void GraspAndSaveGPImage(PictureBox pic1,string strTPID, string strZL) //int Channel1, int Channel2, 如有错，可以再加参数，用sdk中的方法截图
        {
            string strNumber = "KgGP";
            sZL = strZL;

            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";

            //抓第一张图
            try
            {
                //sdk.SDK_CapturePicture(Channel1, stRunPath + "\\qcpicture\\" + fileName1);
                //Thread.Sleep(200);

                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            //try
            //{
            //    sdk.SDK_CapturePicture(Channel2, stRunPath + "\\qcpicture\\" + fileName2);
            //    Thread.Sleep(200);

            //    Bitmap img = getscreenfromhandle((int)pic2.Handle);
            //    img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
            //    Thread.Sleep(200);
            //}
            //catch (System.Exception error)
            //{
            //    MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            byte[] TP1 = GetImageFile(strNumber, 1);
            byte[] TP2 = GetImageFile(strNumber, 2);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveGPTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 钢坯，抓两张图并保存(PictureBox命名为pictureBox1,pictureBox2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSaveGPImage(PictureBox pic1, PictureBox pic2, string strTPID, string strZL) //int Channel1, int Channel2, 如有错，可以再加参数，用sdk中的方法截图
        {
            string strNumber = "KgGP";
            sZL = strZL;

            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";

            //抓第一张图
            try
            {
                //sdk.SDK_CapturePicture(Channel1, stRunPath + "\\qcpicture\\" + fileName1);
                //Thread.Sleep(200);

                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                if (!Directory.Exists(stRunPath + "\\pic\\"))
                {
                    Directory.CreateDirectory(stRunPath + "\\pic\\");
                }

                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                //sdk.SDK_CapturePicture(Channel2, stRunPath + "\\qcpicture\\" + fileName2);
                //Thread.Sleep(200);

                Bitmap img = getscreenfromhandle((int)pic2.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            byte[] TP1 = GetImageFile(strNumber, 1);
            byte[] TP2 = GetImageFile(strNumber, 2);
            
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveGPTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 铁水，抓两张图并保存(PictureBox命名为pictureBox1,pictureBox2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSaveTSImage(PictureBox pic1, PictureBox pic2, string strTPID, string strZL)
        {
            string strNumber = "KgTS";
            sZL = strZL;
            
            fileName1 = strNumber + "1.bmp"; 
            fileName2 = strNumber + "2.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic2.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            byte[] TP1 = GetImageFile(strNumber, 1);
            byte[] TP2 = GetImageFile(strNumber, 2);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveTSTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 入库称，抓两张图并保存(PictureBox命名为pictureBox1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSaveRKImage(string PicFile1, string PicFile2, string strTPID)
        {
            byte[] TP1;
            byte[] TP2;
            if (System.IO.File.Exists(PicFile1) && System.IO.File.Exists(PicFile2))
            {
                TP1 = System.IO.File.ReadAllBytes(PicFile1);
                TP2 = System.IO.File.ReadAllBytes(PicFile2);

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "SaveRKTPData";
                ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
                ccp.IfShowErrMsg = false;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
        }
        /// <summary>
        /// 入库称，抓两张图并保存(PictureBox命名为pictureBox1,pictureBox2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSaveRKImage(PictureBox pic1, PictureBox pic2, string strTPID, string strZL, string strJLDID)
        {
            string strNumber = strJLDID;
            sZL = strZL;

            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic2.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            byte[] TP1 = GetImageFile(strNumber, 1);
            byte[] TP2 = GetImageFile(strNumber, 2);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveRKTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 高线，抓一张图并保存(PictureBox命名为pictureBox1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSaveRKImage(PictureBox pic1, string strTPID, string strZL)
        {
            string strNumber = "KgGX";
            sZL = strZL;
            fileName1 = strNumber + "1.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            byte[] TP1 = GetImageFile(strNumber, 1);
            byte[] TP2 = new byte[1];

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveRKTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 皮带称，抓一张图并保存(PictureBox命名为pictureBox1,pictureBox2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSavePDImage(PictureBox pic1, string strTPID, string strJLDID)
        {
            string strNumber = strJLDID;
            sZL = "";

            fileName1 = strNumber + "1.bmp";
            //fileName2 = strNumber + "2.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            byte[] TP1 = GetImageFile(strNumber, 1);
            byte[] TP2 = new byte[1];

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SavePDTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 皮带称，抓两张图并保存(PictureBox命名为pictureBox1,pictureBox2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSavePDImage(PictureBox pic1, PictureBox pic2, string strTPID, string strJLDID)
        {
            string strNumber = strJLDID;
            sZL = "";

            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic2.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            byte[] TP1 = GetImageFile(strNumber, 1);
            byte[] TP2 = GetImageFile(strNumber, 2);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SavePDTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 皮带称，抓两张图并保存(PictureBox命名为pictureBox1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSavePDImage(string PicFile1, string PicFile2, string strTPID)
        {
            byte[] TP1;
            byte[] TP2;
            byte[] TP3;
            byte[] TP4;
            if (System.IO.File.Exists(PicFile1) && System.IO.File.Exists(PicFile2))
            {
                TP1 = System.IO.File.ReadAllBytes(PicFile1);
                TP2 = System.IO.File.ReadAllBytes(PicFile2);
                TP3 = new byte[1];
                TP4 = new byte[1];

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "SavePDTPData";
                ccp.ServerParams = new object[] { strTPID, TP1, TP2, TP3, TP4 };
                ccp.IfShowErrMsg = false;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
        }

        /// <summary>
        /// 皮带称，抓四张图并保存(PictureBox命名为pictureBox1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSavePDImage(string PicFile1, string PicFile2, string PicFile3, string PicFile4, string strTPID)
        {
            byte[] TP1;
            byte[] TP2;
            byte[] TP3;
            byte[] TP4;
            if (System.IO.File.Exists(PicFile1) && System.IO.File.Exists(PicFile2) && System.IO.File.Exists(PicFile3) && System.IO.File.Exists(PicFile4))
            {
                TP1 = System.IO.File.ReadAllBytes(PicFile1);
                TP2 = System.IO.File.ReadAllBytes(PicFile2);

                TP3 = System.IO.File.ReadAllBytes(PicFile3);
                TP4 = System.IO.File.ReadAllBytes(PicFile4);

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "SavePDTPData";
                ccp.ServerParams = new object[] { strTPID, TP1, TP2, TP3, TP4 };
                ccp.IfShowErrMsg = false;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
        }

        /// <summary>
        /// 材秤，抓一张图并保存(PictureBox命名为pictureBox1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GraspAndSaveRKImage(string picName1, string picName2, string strTPID, string strZL)
        {
            sZL = strZL;
            fileName1 = picName1.Substring(0, picName1.Length - 5);
            fileName2 = picName2.Substring(0, picName2.Length - 5);

            byte[] TP1;
            byte[] TP2;

            TP1 = GetImageFile(fileName1, 1);
            if (picName2 != "")
            {
                TP2 = GetImageFile(fileName2, 2);
            }
            else
            {
                TP2 = new byte[1];
            }

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveRKTPData";
            ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }



        /// <summary>
        /// 板带，抓图并保存
        /// </summary>
        /// <param name="picName1">图片1</param>
        /// <param name="picName2">图片2</param>
        /// <param name="strTPID">计量数据主键</param>
        /// <param name="strZL">图片上嵌入的文字信息</param>
        public void GraspAndSaveBanDaiImage(string picName1, string picName2, string strTPID, string strZL)
        {
            sZL = strZL;
            fileName1 = picName1.Substring(0, picName1.Length - 4);
            fileName2 = picName2.Substring(0, picName2.Length - 4);

            byte[] TP1;
            byte[] TP2;

            TP1 = GetImageFile_BanDai(fileName1, 1);
            if (picName2 != "")
            {
                TP2 = GetImageFile_BanDai(fileName2, 2);
            }
            else
            {
                TP2 = new byte[1];
            }
            string selectsql = "select FS_WEIGHTNO from DT_STORAGEWEIGHTIMAGE where FS_WEIGHTNO='" + strTPID.Trim() + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StorageWeight.StoreageWeight_JZ";
            ccp.MethodName = "QueryTableData";
            ccp.ServerParams = new object[] { selectsql };
            DataTable temptable = new DataTable();
            ccp.SourceDataTable = temptable;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (temptable.Rows.Count > 0)//已存在，更新
            {
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "UpdateRKTPData";
                ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
                ccp.IfShowErrMsg = false;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
            else//不存在，插入
            {
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "SaveRKTPData";
                ccp.ServerParams = new object[] { strTPID, TP1, TP2 };
                ccp.IfShowErrMsg = false;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
        }

        #endregion

        #region 获取本地图片并转换成二进制流
        private byte[] GetImageFile(string strNumber, int index)
        {
            byte[] FileContent;

            if (System.IO.File.Exists(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".bmp") == true)
            {
                Bitmap img = new Bitmap(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".bmp");
                //System.Drawing.Image.GetThumbnailImageAbort callb = null;
                ////System.Drawing.Image newimage = img.GetThumbnailImage(img.Width / 2, img.Height / 2, callb, new System.IntPtr());
                //System.Drawing.Image newimage = img.GetThumbnailImage(img.Width, img.Height, callb, new System.IntPtr());

                System.Drawing.Image newimage = System.Drawing.Image.FromFile(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".bmp");

                if (index == 1)
                {
                    Graphics g = Graphics.FromImage(newimage);
                    g.DrawImage(newimage, 0, 0, newimage.Width, newimage.Height);
                    Font f = new Font("宋体", 20);
                    Brush b = new SolidBrush(Color.Red);
                    string addText = sZL;
                    g.DrawString(addText, f, b, 100, 20);
                }
                  
                newimage.Save(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                newimage.Dispose();
                FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG");

                return FileContent;
            }

            if (System.IO.File.Exists(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG") == true)
            {
                FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG");

                return FileContent;
            }

            FileContent = null;
            //FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG");
            return FileContent;
        }
        #endregion


        #region 获取板带本地图片并转换成二进制流
        private byte[] GetImageFile_BanDai(string strNumber, int index)
        {
            byte[] FileContent;
            
            if (System.IO.File.Exists(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".bmp") == true)
            {
                //Bitmap img = new Bitmap(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".bmp");
                //System.Drawing.Image.GetThumbnailImageAbort callb = null;
                ////System.Drawing.Image newimage = img.GetThumbnailImage(img.Width / 2, img.Height / 2, callb, new System.IntPtr());
                //System.Drawing.Image newimage = img.GetThumbnailImage(img.Width, img.Height, callb, new System.IntPtr());
                try
                {
                    //System.Drawing.Image newimage = System.Drawing.Image.FromFile(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".bmp");

                    //if (index == 2)
                    //{
                    //    Graphics g = Graphics.FromImage(newimage);
                    //    g.DrawImage(newimage, 0, 0, newimage.Width, newimage.Height);
                    //    Font f = new Font("宋体", 38);
                    //    Brush b = new SolidBrush(Color.Red);
                    //    string addText = sZL;
                    //    g.DrawString(addText, f, b, 30, 50);
                    //}
                    //try
                    //{
                    //    newimage.Save(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".JPG", System.Drawing.Imaging.ImageFormat.Jpeg);

                    //    //img.Dispose();
                    //    newimage.Dispose();
                    //    FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".JPG");

                    //    return FileContent;
                    //}
                    //catch (Exception ex1)
                    //{
                    //    MessageBox.Show(ex1.Message + "图片处理出错,有可能JZPicture文件夹为只读，请更改！");
                    //}
                    //改使用副本方式，避免生存周期问题
                    System.Drawing.Image img = System.Drawing.Image.FromFile(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".bmp");
                    Bitmap newimage = new Bitmap(img);
                    if (index == 2)
                    {
                        Graphics g = Graphics.FromImage(newimage);
                        g.DrawImage(newimage, 0, 0, newimage.Width, newimage.Height);
                        Font f = new Font("宋体", 38);
                        Brush b = new SolidBrush(Color.Red);
                        string addText = sZL;
                        g.DrawString(addText, f, b, 30, 50);
                        g.Dispose();
                    }
                    try
                    {
                        newimage.Save(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".JPG", System.Drawing.Imaging.ImageFormat.Jpeg);

                        newimage.Dispose();
                        img.Dispose();
                        FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".JPG");

                        return FileContent;
                    }
                    catch (Exception ex1)
                    {
                        MessageBox.Show(ex1.Message + "图片处理出错,有可能JZPicture文件夹为只读，请更改！");
                    }
                }
                catch (Exception ex1)
                {
                    MessageBox.Show("22222222222222图片处理出错:" + ex1.Message);
                }
                
            }

            //if (System.IO.File.Exists(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".JPG") == true)
            //{
            //    try
            //    {
            //        FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\JZPicture\\" + strNumber + index.ToString() + ".JPG");

            //        return FileContent;
            //    }
            //    catch (Exception ex1)
            //    {
            //        MessageBox.Show("333333333333333图片处理出错:" + ex1.Message);
            //    }
                
            //}

            FileContent = new byte[1];
            //FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG");
            return FileContent;
        }
        #endregion

        #region 获取钢坯辊道本地图片并转换成二进制流
        private byte[] GetImageFile_GP(string strNumber, int index)
        {
            byte[] FileContent;

            if (System.IO.File.Exists(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".bmp") == true)
            {
                Bitmap img = new Bitmap(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".bmp");
                //System.Drawing.Image.GetThumbnailImageAbort callb = null;
                ////System.Drawing.Image newimage = img.GetThumbnailImage(img.Width / 2, img.Height / 2, callb, new System.IntPtr());
                //System.Drawing.Image newimage = img.GetThumbnailImage(img.Width, img.Height, callb, new System.IntPtr());

                System.Drawing.Image newimage = System.Drawing.Image.FromFile(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".bmp");

                if (index == 1)
                {
                    Graphics g = Graphics.FromImage(newimage);
                    g.DrawImage(newimage, 0, 0, newimage.Width, newimage.Height);
                    Font f = new Font("宋体", 10);
                    Brush b = new SolidBrush(Color.Red);
                    string addText = sZL;
                    g.DrawString(addText, f, b, 10, 20);
                }

                newimage.Save(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                newimage.Dispose();
                FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG");

                return FileContent;
            }

            if (System.IO.File.Exists(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG") == true)
            {
                FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG");

                return FileContent;
            }

            FileContent = null;
            //FileContent = System.IO.File.ReadAllBytes(stRunPath + "\\pic\\" + strNumber + index.ToString() + ".JPG");
            return FileContent;
        }
        #endregion

        #region 修改图片
        /// <summary>
        /// 修改轨道衡二次计量图片数据
        /// </summary>
        /// <param name="PictureName"></param>
        public void UpdateGDHTPData(PictureBox pic1, PictureBox pic2, string strTPID)
        {
            string strNumber = "KgGDH";

            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";
            //fileName3 = strNumber + "3.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic2.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            byte[] TP3 = GetImageFile(strNumber, 1);
            byte[] TP4 = GetImageFile(strNumber, 2);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdateGDHTPData";
            ccp.ServerParams = new object[] { strTPID, TP3, TP4 };
            
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 修改铁水二次计量图片数据
        /// </summary>
        /// <param name="PictureName"></param>
        public void UpdateTSTPData(PictureBox pic1, PictureBox pic2, string strTPID, string strZL)
        {
            string strNumber = "KgTS";
            sZL = strZL;

            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";
            //fileName3 = strNumber + "3.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic2.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            byte[] TP3 = GetImageFile(strNumber, 1);
            byte[] TP4 = GetImageFile(strNumber, 2);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdateTSTPData";
            ccp.ServerParams = new object[] { strTPID, TP3, TP4 };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 修改皮带称二次计量一张图片数据
        /// </summary>
        /// <param name="PictureName"></param>
        public void UpdatePDTPData(PictureBox pic1, string strTPID, string strJLDID)
        {
            string strNumber = strJLDID;

            fileName1 = strNumber + "1.bmp";
            //fileName2 = strNumber + "2.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            byte[] TP3 = GetImageFile(strNumber, 1);
            byte[] TP4 = new byte[1];

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdatePDTPData";
            ccp.ServerParams = new object[] { strTPID, TP3, TP4 };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 修改皮带称二次计量图片数据
        /// </summary>
        /// <param name="PictureName"></param>
        public void UpdatePDTPData(PictureBox pic1, PictureBox pic2, string strTPID, string strJLDID)
        {
            string strNumber = strJLDID;

            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";
            //fileName3 = strNumber + "3.bmp";

            //抓第一张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic1.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                Bitmap img = getscreenfromhandle((int)pic2.Handle);
                img.Save(stRunPath + "\\pic\\" + fileName2, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            byte[] TP3 = GetImageFile(strNumber, 1);
            byte[] TP4 = GetImageFile(strNumber, 2);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdatePDTPData";
            ccp.ServerParams = new object[] { strTPID, TP3, TP4 };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        #endregion

        #region 查询图片方法
        /// <summary>
        /// 查询汽车衡图片
        /// </summary>
        /// <param name="strID"></param>
        public void QueryQCImage(string strID)
        {
            DataTable dtTP = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryYCTXData";
            ccp.ServerParams = new Object[] { strID };
            ccp.SourceDataTable = dtTP;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            m_dtImage = dtTP;
        }

        /// <summary>
        /// 查询轨道衡图片
        /// </summary>
        /// <param name="strID"></param>
        public void QueryGDHImage(string strID)
        {
            DataTable dtTP = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryGDHImage";
            ccp.ServerParams = new Object[] { strID };
            ccp.SourceDataTable = dtTP;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            m_dtImage = dtTP;
        }
        /// <summary>
        /// 查询钢坯（汽车衡钢坯除外）图片
        /// </summary>
        /// <param name="strID"></param>
        public void QueryGPImage(string strID)
        {
            DataTable dtTP = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryGPImage";
            ccp.ServerParams = new Object[] { strID };
            ccp.SourceDataTable = dtTP;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            m_dtImage = dtTP;
        }
        /// <summary>
        /// 查询铁水图片
        /// </summary>
        /// <param name="strID"></param>
        public void QueryTSImage(string strID)
        {
            DataTable dtTP = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryTSImage";
            ccp.ServerParams = new Object[] { strID };
            ccp.SourceDataTable = dtTP;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            m_dtImage = dtTP;
        }
        /// <summary>
        /// 查询入库（汽车衡钢坯除外）图片
        /// </summary>
        /// <param name="strID"></param>
        public void QueryRKImage(string strID)
        {
            DataTable dtTP = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryRKImage";
            ccp.ServerParams = new Object[] { strID };
            ccp.SourceDataTable = dtTP;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            m_dtImage = dtTP;
        }

        /// <summary>
        /// 查询皮带称图片
        /// </summary>
        /// <param name="strID"></param>
        public void QueryPDImage(string strID)
        {
            DataTable dtTP = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryPDImage";
            ccp.ServerParams = new Object[] { strID };
            ccp.SourceDataTable = dtTP;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            m_dtImage = dtTP;
        }

        #endregion

        #region Bitmap转换成Image
        //Bitmap转换成Image,等比例缩放功能
        public bool ThumbnailCallback()
        {
            return false;
        }
        /// <summary>
        /// 图片显示到PictureBox中(字节流，PictureBox，PictureBox的宽，PictureBox的高)
        /// </summary>
        /// <param name="imagebytes"></param>
        /// <param name="pic"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public void BitmapToImage(byte[] imagebytes, PictureBox pic, int Width, int Height)
        {
            MemoryStream stream = new MemoryStream(imagebytes, true); // 创建一个内存流，支持写入，用于存放图片二进制数据 

            Bitmap FinalImage;

            if (imagebytes == null)
            {
                return;
            }

            if (imagebytes[0] == 0)
            {
                FinalImage = null;
            }
            else
            {
                stream.Write(imagebytes, 0, imagebytes.Length);
                FinalImage = new Bitmap(stream);
            }
            //stream.Write(imagebytes, 0, imagebytes.Length);
            //Bitmap FinalImage = new Bitmap(stream);
            if (FinalImage == null)
            {
                return;
            }
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            int intPictureBoxWidth = Width;
            int intPictureBoxHeight = Height;

            int intOriginalWidth = FinalImage.Width;
            int intOriginalHeight = FinalImage.Height;
            int intNewWidth = FinalImage.Width;
            int intNewHeight = FinalImage.Height;

            if (intOriginalWidth <= (int)intPictureBoxWidth && intOriginalHeight <= (int)intPictureBoxHeight)
            {
                //宽和高都不大于intPictureBoxWidth和intPictureBoxHeight
                if (intPictureBoxWidth / intOriginalWidth > intPictureBoxHeight / intOriginalHeight)
                {
                    intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
                    intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
                    //宽大于PictureBox控件的宽，再按比例缩放
                    if (intNewWidth > intPictureBoxWidth)
                    {
                        intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intNewHeight) * intPictureBoxWidth / Convert.ToDecimal(intNewWidth), 0));
                        intNewHeight = intPictureBoxWidth;
                    }
                }
                else
                {
                    intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));
                    intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));

                    //高大于PictureBox控件的高，再按比例缩放
                    if (intNewHeight > intPictureBoxHeight)
                    {
                        //intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intNewWidth) * intPictureBoxHeight / Convert.ToDecimal(intNewHeight), 0));
                        intNewWidth = intPictureBoxWidth;
                        intNewHeight = intPictureBoxHeight;
                    }
                }

            }
            else if (intOriginalWidth > (int)intPictureBoxWidth && Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0) <= (int)intPictureBoxHeight)
            {
                //宽大于intPictureBoxWidth且高等比例缩放后不大于intPictureBoxHeight
                intNewWidth = (int)intPictureBoxWidth;
                intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));
            }
            else if (intOriginalHeight > (int)intPictureBoxHeight && Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0) <= (int)intPictureBoxWidth)
            {
                //高大于intPictureBoxHeight且宽等比例缩放后不大于intPictureBoxWidth
                intNewHeight = (int)intPictureBoxHeight;
                intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
            }
            else
            {
                //否则以缩放比例大的缩放高和宽
                if (intPictureBoxWidth / intOriginalWidth > intPictureBoxHeight / intOriginalHeight)
                {
                    intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
                    intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
                }
                else
                {
                    intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));
                    intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));
                }
            }
            Image myImage = FinalImage.GetThumbnailImage(intNewWidth, intNewHeight, myCallback, IntPtr.Zero);
            //return myImage;
            pic.Image = myImage;
        }
        #endregion

        public string GetServerTime()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "GetServerTime";
            DataTable dtTime = new DataTable();
            ccp.SourceDataTable = dtTime;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            string time = dtTime.Rows[0][0].ToString();
            return time;
        }

        public void SynServerTime()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "GetServerTime";
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            System.DateTime ServerTime = Convert.ToDateTime(ccp.ReturnObject);

            //SystemTime st = new SystemTime();
            SynTime.wYear = (short)ServerTime.Year;
            SynTime.wDay = (short)ServerTime.Day;
            SynTime.wMonth = (short)ServerTime.Month;
            SynTime.wHour = (short)ServerTime.Hour;
            SynTime.wMinute = (short)ServerTime.Minute;
            SynTime.wSecond = (short)ServerTime.Second;
            //修改本地端的时间和日期 
            SetLocalTime(ref SynTime);
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string getIPAddress()
        {
            IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
            string ip = ipEntry.AddressList[0].ToString();
            return ip;
        }

        /// <summary>
        /// 获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string getMACAddress()
        {
            string mac = "";
            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return mac;
        }

        /// <summary>
        /// 插入数据库操作日志
        /// </summary>
        /// <param name="strDateTime">操作时间</param>
        /// <param name="strOperationType">操作类型</param>
        /// <param name="strOperater">操作人</param>
        /// <param name="strOperationIP">IP地址</param>
        /// <param name="strOperationMAC">MAC地址</param>
        /// <param name="strOperationMemo">操作内容</param>
        /// <param name="strKeyWord">关键字</param>
        /// <param name="strStoveNo">冶炼炉号</param>
        /// <param name="strBatchNo">轧制编号</param>
        /// <param name="strCarNo">车号</param>
        /// <param name="strBandNo">吊(支)号</param>
        /// <param name="strTableName">表名</param>
        /// <param name="strModuleName">模块名</param>
        public void insertLog(string strDateTime,string strOperationType,string strOperater,string strOperationIP,string strOperationMAC,string strOperationMemo,string strKeyWord,string strStoveNo,string strBatchNo,string strCarNo,string strBandNo,string strTableName,string strModuleName)
        {
            string sql = " insert into dt_techCardOperation (FD_DATATIME,FS_OPERATIONTYPE,Fs_Operater,Fs_Operationip,Fs_Operationmac,FS_OPERATIONMEMO,FS_KeyWord,FS_StoveNo,FS_BatchNo,FS_CARNO,FS_BANDNO,FS_TABLENAME,FS_MODULENAME)  values (to_date('" + strDateTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strOperationType + "','" + strOperater + "','" + strOperationIP + "','" + strOperationMAC + "','" + strOperationMemo + "','" + strKeyWord + "','" + strStoveNo + "','" + strBatchNo + "','" + strCarNo + "','" + strBandNo + "','" + strTableName + "','" + strModuleName + "')";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);        
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public DataTable QueryData(string strSql)
        {
            DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dt;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            return dt;
        }

        /// <summary>
        /// 查询校秤图片
        /// </summary>
        /// <param name="strID"></param>
        public void QueryCorrentionImage(string strID)
        {
            DataTable dtTP = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryCorrentionImage";
            ccp.ServerParams = new Object[] { strID };
            ccp.SourceDataTable = dtTP;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            m_dtImage = dtTP;
        }
        public DataTable FindCorrentionPointInformation(string p_FS_POINT)
        {
            string strSql = "select FS_POINTCODE,FS_POINTNAME,FN_WEIGHT,FN_DEVIATION ";
            strSql += " from bt_corrention_point ";
            strSql += " where  FS_POINTCODE = '" + p_FS_POINT + "'";
            DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StorageWeight.StoreageWeight_JZ";
            ccp.MethodName = "QueryTableData";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            return dt;
        }
        public void InsertCorrentionInformation(string WEIGHTNO, double dbDeviation,double db_fm_Weight,double p_FN_WEIGHT,double error,string Qualified,string p_FS_PERSON,string p_FS_SHIFT,string p_FS_TERM,string  p_FS_POINT)
        {

            string sql = " insert into dt_corrention_detail (FS_WEIGHTNO,FD_COMPATEDATE,FN_ALLOWDIFF,FN_WEIGHT,FN_SHOWWEIGHT,FN_DIFFWEIGHT,FS_JUDGERESULT,FS_OPERATOR,FS_SHIFT,FS_TERM,FS_MEMO,FS_POINTCODE )  values (";
            sql += "'" + WEIGHTNO + "',to_date('" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"','yyyy-MM-dd hh24:mi:ss')," +
                dbDeviation + "," + db_fm_Weight + "," 
                + p_FN_WEIGHT + "," + error + ",'" 
                + Qualified + "','" + p_FS_PERSON + "','" 
                + p_FS_SHIFT + "','" + p_FS_TERM + "','','" + p_FS_POINT + "'";
            sql += ")";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);   
        }
        public void GraspAndSaveCorrentionImage(string picName1, string strTPID, string strZL) 
        {
            sZL = strZL+"吨";
            fileName1 = picName1.Substring(0, picName1.Length - 5);


            byte[] TP1;
            TP1 = GetImageFile_BanDai(fileName1, 1);

            string selectsql = "select FS_WEIGHTNO from DT_CORRENTION_IMAGE where FS_WEIGHTNO='" + strTPID.Trim() + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StorageWeight.StoreageWeight_JZ";
            ccp.MethodName = "QueryTableData";
            ccp.ServerParams = new object[] { selectsql };
            DataTable temptable = new DataTable();
            ccp.SourceDataTable = temptable;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (temptable.Rows.Count > 0)//已存在，更新
            {
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "UpdateCorrentionImage";
                ccp.ServerParams = new object[] { strTPID, TP1 };
                ccp.IfShowErrMsg = false;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
            else//不存在，插入
            {
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "SaveCorrentionImage";
                ccp.ServerParams = new object[] { strTPID, TP1};
                ccp.IfShowErrMsg = false;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
        }
        /// <summary>
        /// 校秤信息
        /// </summary>
        /// <param name="pointCode">计量点</param>
        /// <param name="operater">操作员</param>
        /// <param name="shift">班次</param>
        /// <param name="term">班组</param>
        /// <param name="correntionWeight">显示重量</param>

        public bool correntionInformation(string correntionWeightNo,string pointCode,string operater,string shift,string term,string correntionWeight)
        {
            DataTable dt = new DataTable();//用来临时存储校秤计量点信息表
            Double db_fm_Weight = 0, dbDeviation = 0, db_temp = 0, error = 0,p_FN_WEIGHT = 0;;
            string Qualified = "";//是否合格
            string fm_Weight = "";
            string strDeviation = "";

            string str_FN_WEIGHT = correntionWeight;//重量
            if (str_FN_WEIGHT == null || str_FN_WEIGHT.Equals(""))
            {
                MessageBox.Show("重量不能为空！");
                return false;
            }
            else
            {
                try
                {
                    p_FN_WEIGHT = Math.Round(Convert.ToDouble(str_FN_WEIGHT), 3) * 1000;//重量
                }
                catch (Exception ee5)
                {
                    MessageBox.Show("重量必须为数值！");
                    return false;
                }
            }
            //if (p_FN_WEIGHT == 0)
            //{
            //    MessageBox.Show("仪表重量值必须大于零！");
            //    return;
            //}

            dt = FindCorrentionPointInformation(pointCode);
            if (dt.Rows.Count > 0)
            {
                fm_Weight = dt.Rows[0]["FN_WEIGHT"].ToString();
                strDeviation = dt.Rows[0]["FN_DEVIATION"].ToString();

            }
            if ((fm_Weight == null || fm_Weight.Equals("")) && (strDeviation == null || strDeviation.Equals("")))
            {
                MessageBox.Show("砝码值和偏差量不能为空！");
                return false;
            }
            else
            {
                try
                {
                    db_fm_Weight = Math.Round(Convert.ToDouble(fm_Weight), 3);//砝码
                    dbDeviation = Math.Round(Convert.ToDouble(strDeviation), 3);//偏差
                }
                catch (Exception ee5)
                {
                    MessageBox.Show("砝码值和偏差值必须为数值！");

                    return false;
                }
            }
            error = p_FN_WEIGHT - db_fm_Weight;
            db_temp = Math.Abs(error);//求绝对值
            if (db_temp > dbDeviation)
            {
                Qualified = "不合格";
            }
            else
            {
                Qualified = "合格";
            }

            InsertCorrentionInformation(correntionWeightNo, dbDeviation, db_fm_Weight, p_FN_WEIGHT, error, Qualified, operater, shift, term, pointCode);
            return true;
        }


        #region 修改重量日记写入
        /// <summary>
        /// 修改重量日记写入
        /// </summary>
        /// <param name="strWeightNo">称重流水号</param>
        /// <param name="strPageName">界面名称</param>
        /// <param name="strWeightPre">修改前重量</param>
        /// <param name="strWeightCur">修改后重量</param>
        /// <param name="strModifier">修改人</param>
        /// <param name="strModifyType">修改类型(汽车，动轨等）</param>
        /// <param name="strModifyIP">修改人IP</param>
        public void writeModifyWeightLog(string strWeightNo, string strPageName, string strWeightPre, string strWeightCur, string strModifier, string strModifyType, string strModifyIP)
        {
            string strSerialNo = DateTime.Now.ToString("yyyyMMddhhmmss");
            string strModifyDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "WriteModifyWeightLog";
            //传入参数顺序：流水号，称重流水号，界面名称，修改前重量，后重量，修改人，时间，称重类型，修改IP
            ccp.ServerParams = new object[] { strSerialNo, strWeightNo, strPageName, strWeightPre, strWeightCur, strModifier, strModifyDate, strModifyType, strModifyIP };
            ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 将操作日志存储进DT_OPERATIONLOG表中
        /// </summary>
        /// <returns>void</returns>
        public void WriteOperationLog(string strTableName, string strDepart, string username, string strType, string strLog, string strGraphName, string strDataName,string  strWeightType)
        {
            System.Net.IPAddress addr;
            addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);

            string strIP = addr.ToString();

            string guid = System.Guid.NewGuid().ToString();
            string strOperatTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strInsertSql = "insert into DT_OPERATIONLOG T (FS_OPERATIONNO,FS_TABLENAME,FS_DEPART,FS_OPERATOR,FS_TYPE,FD_TIME,FS_DATA,FS_IP,FS_GRAPHNAME,FS_DATANAME,FS_WEIGHTTYPE) values";
            strInsertSql += "('" + guid + "','" + strTableName + "','" + strDepart + "','" + username + "','" + strType + "',to_date('" + strOperatTime + "','yyyy-MM-dd hh24:mi:ss'),'" + strLog + "','" + strIP + "','" + strGraphName + "','" + strDataName + "','"+strWeightType+"')";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strInsertSql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        #endregion
    }
}
