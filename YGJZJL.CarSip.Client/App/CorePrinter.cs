using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
//using System.Collections.Generic;
using System.Drawing.Printing;

namespace YGJZJL.CarSip.Client.App
{
    public class CorePrinter
    {
        #region <成员变量>
        private HgLable _lable = null;
        private PrintDocument _print_doc = null;
        private int _copies = 1;  // 默认打印的页数
        #endregion

        #region <属性>
        public HgLable Data
        {
            get { return _lable; }
            set { _lable = value; }
        }
        public string PrinterName
        {
            get { return _print_doc.PrinterSettings.PrinterName; }
            set { _print_doc.PrinterSettings.PrinterName = value; }
        }

        public PrintDocument Doc
        {
            get { return _print_doc; }
            set { _print_doc = value; }
        }

        public int Copies
        {
            get{return _copies;}
            set{_copies = value;}
        }
        #endregion

        #region <构造函数>
        public CorePrinter()
        {
            _print_doc = new PrintDocument();
            _print_doc.PrintPage += new PrintPageEventHandler(OnPrintPage);
            
        }
        #endregion

        #region <公共方法>  
        public bool Init(string printerName)
        {
            _print_doc.PrinterSettings.PrinterName = printerName;
            return true;
        }
        // 打印高线大标牌
        private void HwBigLablePrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int lableWith = 70, lableLength = 100, lineHight = 7;
            double dotMM = 4;
            // 顶部和底部的大矩形
            Size topBox = new Size(lableWith, 44);
            // 不填数据的小矩形
            Size littleBox = new Size(12, lineHight);
            // 左边空白矩形区
            Size leftBlankBox = new Size(24, lineHight);
            // 右边空白矩形区
            Size rightBlankBox = new Size(22, lineHight);
            
            //
            // 批号的矩形区
            Size bathNoBox = new Size(lableWith - littleBox.Width, lineHight);
            // 规格的矩形区
            Size specBox = new Size(36 - littleBox.Width, lineHight);
            // 重量矩形区
            Size weightBox = new Size();
            weightBox = specBox;
            //生产时间矩形区
            Size prodDateLeftBox = new Size(23, lineHight);
            Size prodDateBlankBox = new Size(40, lineHight);
            // 条码矩形区
            Size barcodeBox = new Size(67,22);
            
            // 地址矩形区
            Size addrBox = new Size(lableWith, 5);

            // 矩形坐标定义
            Rectangle[] _rects = new Rectangle[15];
            int i = 0;
            // 标签头部
            _rects[i] = new Rectangle(0, 0, topBox.Width, topBox.Height);
            //----------------------------------------------------------------------------- 第1行
            // 批号
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, bathNoBox.Width, bathNoBox.Height);
            //----------------------------------------------------------------------------- 第2行
            // 标准
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);
            // 牌号
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, rightBlankBox.Width, rightBlankBox.Height);
            //----------------------------------------------------------------------------- 第3行
            // 规格
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);
            // 重量
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, rightBlankBox.Width, rightBlankBox.Height);
            //----------------------------------------------------------------------------- 第4行
            // 生产日期
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, prodDateLeftBox.Width, prodDateLeftBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, prodDateBlankBox.Width, prodDateBlankBox.Height);
            //----------------------------------------------------------------------------- 第5行
            // 底部的条码区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, barcodeBox.Width, barcodeBox.Height);
            // 地址区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, addrBox.Width, addrBox.Height);
            // 坐标单位转换 mm -> dot
            for (i = 0; i < _rects.Length; i++)
            {
                _rects[i].X = (int)(_rects[i].X * dotMM);
                _rects[i].Y = (int)(_rects[i].Y * dotMM) - 65;
                _rects[i].Width = (int)(_rects[i].Width * dotMM);
                _rects[i].Height = (int)(_rects[i].Height * dotMM);
            }

            // 定义字体
            Font CFont = new Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font EFont = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sEFont = new Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sCFont = new Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //Font ssCFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Font cCFont = new Font("隶书", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font eEFont = new Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;
            // add by [bhb]
            StringFormat formatLeft = new StringFormat();
            formatLeft.Alignment = StringAlignment.Near;
            formatLeft.LineAlignment = StringAlignment.Near;
            //===========================画矩形===========================
            //Pen rectPen = new Pen(Brushes.Blue, 2);
            //for (i = 0; i < _rects.Length; i++)
            //{
            //    e.Graphics.DrawRectangle(rectPen, _rects[i]);
            //}

            //===========================打印数据===========================     
            //打印批次号
            if (_lable.BandNo.Length == 1)
                e.Graphics.DrawString(_lable.BatchNo + "   0" + _lable.BandNo, EFont, Brushes.Black, _rects[2], drawFormat1);
            else
                e.Graphics.DrawString(_lable.BatchNo + "   " + _lable.BandNo, EFont, Brushes.Black, _rects[2], drawFormat1);
            //打印标准          
            //e.Graphics.DrawString("Φ" + _lable.Spec + "mm", EFont, Brushes.Black, _rects[4], drawFormat1);
            // 打印牌号\ 增加X的偏移量
            _rects[6].X -= 10;
            e.Graphics.DrawString(_lable.SteelType, EFont, Brushes.Black, _rects[6], formatLeft);
            //打印规格          
            e.Graphics.DrawString("Φ" + _lable.Spec + "mm", EFont, Brushes.Black, _rects[8], drawFormat1);
            //打印重量
            e.Graphics.DrawString(_lable.Weight + " kg", EFont, Brushes.Black, _rects[10], drawFormat1); //重量
            //打印生产日期
            e.Graphics.DrawString(_lable.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日" + _lable.Term, CFont, Brushes.Black, _rects[12], drawFormat1); //生产日期
            // 打印条码             
            Code128 c128 = new Code128();
            c128.printBigCode1(_lable.BarCode, _rects[13], e);
            
            // 打印地址
            if (Data.PrintAddress)
            {
                e.Graphics.DrawString("云南 ● 蒙自 红河钢铁有限公司", sCFont, Brushes.Black, _rects[14], drawFormat1); //地点
            }
        }
        // 打印高线小标牌
        private void SmallCardPrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int lableWith = 65, lableLength=55, lineHight = 8;
            double dotMM = 4;
            // 顶部和底部的大矩形
            Size bigBox = new Size(lableWith,14);    
            // 不填数据的小矩形
            Size littleBox = new Size(11, lineHight);     
            // 批号的矩形区
            Size bathNoBox = new Size(lableWith-littleBox.Width,lineHight);
            // 规格的矩形区
            Size specBox = new Size(32-littleBox.Width, lineHight); 
            // 重量矩形区
            Size weightBox = new Size();
            weightBox = specBox;
            //生产时间矩形区
            Size prodBox = new Size(36, lineHight); 
            // 条码矩形区
            Size barcodeBox = new Size();
            barcodeBox = bigBox;
            barcodeBox.Height -= 5;
            // 地址矩形区
            Size addrBox = new Size(46,5);
          
            // 矩形坐标定义
            Rectangle[] _rects = new Rectangle[10];
            int i = 0;
            // 标签头部
            _rects[i] = new Rectangle(0, 0, bigBox.Width, bigBox.Height);            
            // 批号
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i-1].X+ _rects[i-1].Width, _rects[i-1].Y, bathNoBox.Width, bathNoBox.Height);
            // 规格
            i++;
            _rects[i] = new Rectangle(0, _rects[i-1].Y + _rects[i-1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, specBox.Width, specBox.Height);
            // 重量
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, weightBox.Width, weightBox.Height);
            // 生产日期
            i++;
            _rects[i] = new Rectangle(22, _rects[i - 1].Y + _rects[i - 1].Height, prodBox.Width, prodBox.Height);
            // 底部的条码区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, barcodeBox.Width, barcodeBox.Height);
            // 地址区
            i++;
            _rects[i] = new Rectangle(10, _rects[i - 1].Y + _rects[i - 1].Height, addrBox.Width, addrBox.Height);
            // 坐标单位转换 mm -> dot
            for (i = 0; i < _rects.Length; i++)
            {
                _rects[i].X = (int)(_rects[i].X * dotMM);
                _rects[i].Y = (int)(_rects[i].Y*dotMM) -40;
                _rects[i].Width = (int)(_rects[i].Width * dotMM);
                _rects[i].Height = (int)(_rects[i].Height * dotMM);
            }           
            
            // 定义字体
            Font CFont = new Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font EFont = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sEFont = new Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sCFont = new Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //Font ssCFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Font cCFont = new Font("隶书", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font eEFont = new Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;
            //===========================画矩形===========================
            //Pen rectPen = new Pen(Brushes.Blue, 2);
            //for (i = 0; i < _rects.Length; i++)
            //{
            //    e.Graphics.DrawRectangle(rectPen, _rects[i]);
            //}

            //===========================打印数据===========================     
            //打印批次号
            if (_lable.BandNo.Length == 1)
                e.Graphics.DrawString(_lable.BatchNo + " 0" + _lable.BandNo, EFont, Brushes.Black, _rects[2], drawFormat1);  
            else
                e.Graphics.DrawString(_lable.BatchNo + " " + _lable.BandNo, EFont, Brushes.Black, _rects[2], drawFormat1); 
           
            //打印规格          
            e.Graphics.DrawString("Φ" + _lable.Spec + "mm", EFont, Brushes.Black, _rects[4], drawFormat1);
            //打印重量
            e.Graphics.DrawString(_lable.Weight + " kg", EFont, Brushes.Black, _rects[6], drawFormat1); //重量
            //打印生产日期
            e.Graphics.DrawString(_lable.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日" + _lable.Term, CFont, Brushes.Black, _rects[7], drawFormat1); //生产日期
            // 打印条码             
            Code128 c128 = new Code128();
            c128.printSmallCode(_lable.BarCode,_rects[8], e);
            // 打印地址
            if(Data.PrintAddress) e.Graphics.DrawString("云南 ● 蒙自 红河钢铁有限公司", sCFont, Brushes.Black, _rects[9], drawFormat1); //地点
        }
        private void SmallCardPrint_V2(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int lableWith = 65, lableLength = 55, lineHight = 8-2;
            double dotMM = 4;
            // 顶部和底部的大矩形
            Size bigBox = new Size(lableWith, 14);
            Size topBox = new Size();
            topBox = bigBox;
            topBox.Height += 5;
            // 不填数据的小矩形
            Size littleBox = new Size(11-2, lineHight);
            // 批号的矩形区
            Size bathNoBox = new Size(lableWith - littleBox.Width, lineHight);
            // 规格的矩形区
            Size specBox = new Size(32 - littleBox.Width, lineHight);
            // 重量矩形区
            Size weightBox = new Size();
            weightBox = specBox;
            //生产时间矩形区
            Size prodBox = new Size(36, lineHight);
            // 条码矩形区
            Size barcodeBox = new Size();
            barcodeBox = bigBox;
            barcodeBox.Height -= 5-1;
            // 地址矩形区
            Size addrBox = new Size(46, 5);

            // 矩形坐标定义
            Rectangle[] _rects = new Rectangle[10];
            int i = 0;
            // 标签头部
            _rects[i] = new Rectangle(0, 0, bigBox.Width, bigBox.Height);
            // 批号
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, bathNoBox.Width, bathNoBox.Height);
            // 规格
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, specBox.Width, specBox.Height);
            // 重量
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, weightBox.Width, weightBox.Height);
            // 生产日期
            i++;
            _rects[i] = new Rectangle(22, _rects[i - 1].Y + _rects[i - 1].Height, prodBox.Width, prodBox.Height);
            // 底部的条码区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, barcodeBox.Width, barcodeBox.Height);
            // 地址区
            i++;
            _rects[i] = new Rectangle(10, _rects[i - 1].Y + _rects[i - 1].Height, addrBox.Width, addrBox.Height);
            // 坐标单位转换 mm -> dot
            for (i = 0; i < _rects.Length; i++)
            {
                _rects[i].X = (int)(_rects[i].X * dotMM);
                _rects[i].Y = (int)(_rects[i].Y * dotMM) - 20;
                _rects[i].Width = (int)(_rects[i].Width * dotMM);
                _rects[i].Height = (int)(_rects[i].Height * dotMM);
            }

            // 定义字体
            Font CFont = new Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font EFont = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sEFont = new Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sCFont = new Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //Font ssCFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Font cCFont = new Font("隶书", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font eEFont = new Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;
            //===========================画矩形===========================
            //Pen rectPen = new Pen(Brushes.Blue, 2);
            //for (i = 0; i < _rects.Length; i++)
            //{
            //    e.Graphics.DrawRectangle(rectPen, _rects[i]);
            //}

            //===========================打印数据===========================     
            //打印批次号
            if (_lable.BandNo.Length == 1)
                e.Graphics.DrawString(_lable.BatchNo + " 0" + _lable.BandNo, EFont, Brushes.Black, _rects[2], drawFormat1);
            else
                e.Graphics.DrawString(_lable.BatchNo + " " + _lable.BandNo, EFont, Brushes.Black, _rects[2], drawFormat1);

            //打印规格          
            e.Graphics.DrawString("Φ" + _lable.Spec + "mm", EFont, Brushes.Black, _rects[4], drawFormat1);
            //打印重量
            e.Graphics.DrawString(_lable.Weight + " kg", EFont, Brushes.Black, _rects[6], drawFormat1); //重量
            //打印生产日期
            e.Graphics.DrawString(_lable.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日" + _lable.Term, CFont, Brushes.Black, _rects[7], drawFormat1); //生产日期
            // 打印条码             
            Code128 c128 = new Code128();
            c128.printSmallCode(_lable.BarCode, _rects[8], e);
            // 打印地址
            if (Data.PrintAddress) e.Graphics.DrawString("云南 ● 蒙自 红河钢铁有限公司", sCFont, Brushes.Black, _rects[9], drawFormat1); //地点
        }

        // 棒材大标牌
        private void BigCardPrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int oX = -3, oY = -13;  //偏移量
            int offLine = -15;
            int i = 43;
            int j = 2;

            int[] xPos = { 0, 50, 150, 200, 300 };
            int[] yPos = { 0, 182, 212, 242, 272, 302, 332, 362, 412, 420 };

            Font CFont = new Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font EFont = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font EFont1 = new Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sEFont = new Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sCFont = new Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //Font ssCFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Font cCFont = new Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font eEFont = new Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;
            StringFormat formatLeft = new StringFormat();
            formatLeft.Alignment = StringAlignment.Near;
            formatLeft.LineAlignment = StringAlignment.Near;
            //------------------------------------------------------------------------------------------1行
            Rectangle rec;


            rec = new Rectangle(oX + xPos[1] - 10 - j, yPos[1] + oY + offLine + 5 - i, xPos[4] - xPos[1], yPos[2] - yPos[1]);
            rec.Height = 30;

            if (_lable.BandNo.Length == 1)
                e.Graphics.DrawString(_lable.BatchNo.Trim() + "  0" + _lable.BandNo.Trim(), EFont, Brushes.Black, rec, drawFormat1);  //批次号
            else
                e.Graphics.DrawString(_lable.BatchNo.Trim() + "  " + _lable.BandNo.Trim(), EFont, Brushes.Black, rec, drawFormat1);  //批次号


            if (_lable.Standard.Length > 0)
            {
                rec.X = xPos[1] + oX - j;
                rec.Y = yPos[2] + oY + offLine - i;
                rec.Width = xPos[2] - xPos[1];
                rec.Height = 30;
                e.Graphics.DrawString(_lable.Standard.Trim(), sEFont, Brushes.Black, rec, drawFormat1); //标准
            }

            if (_lable.SteelType.Length > 0)
            {
                rec.X = xPos[3] + oX - j - 35; //35
                rec.Y = yPos[2] + oY + offLine - i;
                rec.Width = xPos[4] - xPos[3] + 40;
                rec.Height = 34;
                e.Graphics.DrawString(_lable.SteelType.Trim(), EFont, Brushes.Black, rec, drawFormat1); //牌号
            }
            //------------------------------------------------------------------------------------------3行

            rec.X = xPos[1] + oX - j;
            rec.Y = yPos[3] + oY + offLine - i;
            rec.Width = xPos[2] - xPos[1];
            rec.Height = 30;
            e.Graphics.DrawString("Φ" + _lable.Spec.Trim() + "mm", EFont, Brushes.Black, rec, drawFormat1); //规格



            rec.X = xPos[3] + oX - j; //35
            rec.Y = yPos[3] + oY + offLine - i;
            rec.Width = xPos[4] - xPos[3];
            rec.Height = 34;
            e.Graphics.DrawString(_lable.Length.Trim() + "m", EFont, Brushes.Black, rec, drawFormat1); //长度

            //--------------------------------------------------------------------------------------------4行
            rec.X = xPos[1] + oX - j;
            rec.Y = yPos[4] + oY + offLine - i;
            rec.Width = xPos[2] - xPos[1];
            rec.Height = 30;
            e.Graphics.DrawString(_lable.Weight.Trim() + "kg", EFont, Brushes.Black, rec, drawFormat1); //



            rec.X = xPos[3] + oX - j; //35
            rec.Y = yPos[4] + oY + offLine - i;
            rec.Width = xPos[4] - xPos[3];
            rec.Height = 30;
            e.Graphics.DrawString(_lable.Count.Trim(), EFont, Brushes.Black, rec, drawFormat1); //重量



            //---------------------------------------------------------------------------------------------5行
            rec.X = xPos[1] + oX + xPos[1] - 10;
            rec.Y = yPos[5] + oY + offLine - 5 - i;
            rec.Width = xPos[2] - xPos[1] + 80;
            rec.Height = 30;
            e.Graphics.DrawString(_lable.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日" + _lable.Term.Trim(), CFont, Brushes.Black, rec, drawFormat1); //生产日期



            //---------------------------------------------------------------------------------------------7行
            if (_lable.PrintAddress)
            {
                rec.X = xPos[0] + oX;
                rec.Y = yPos[8] + oY - 43 - i;
                rec.Width = xPos[4] - xPos[0];
                rec.Height = 20;
                e.Graphics.DrawString("云南 ● 蒙自 红河钢铁有限公司", sCFont, Brushes.Black, rec, drawFormat1); //地点
            }

            Code128 c128 = new Code128();
            c128.printBigCode(_lable.BarCode, 280, 50, e);
        }
        private void HwBigLablePrint_V2(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int lableWith = 70, lableLength = 100, lineHight = 7+1;
            double dotMM = 4;
            // 顶部和底部的大矩形
            Size topBox = new Size(lableWith, 44+3);
            // 不填数据的小矩形
            Size littleBox = new Size(12, lineHight);
            // 左边空白矩形区
            Size leftBlankBox = new Size(24, lineHight);
            // 右边空白矩形区
            Size rightBlankBox = new Size(22, lineHight);

            
            // 批号的矩形区
            Size bathNoBox = new Size(lableWith - littleBox.Width, lineHight);
            //标准
           
            //牌号
            // 规格的矩形区
            Size specBox = new Size(36 - littleBox.Width, lineHight);
            // 重量矩形区
            Size weightBox = new Size();
            weightBox = specBox;
            //生产时间矩形区
            Size prodDateLeftBox = new Size(23, lineHight);
            Size prodDateBlankBox = new Size(40, lineHight);
            // 条码矩形区
            Size barcodeBox = new Size(67, 22-7);

            // 地址矩形区
            Size addrBox = new Size(lableWith, 5);

            // 矩形坐标定义
            Rectangle[] _rects = new Rectangle[15];
            int i = 0;
            // 标签头部
            _rects[i] = new Rectangle(0, 0, topBox.Width, topBox.Height);
            //----------------------------------------------------------------------------- 第1行
            // 批号
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, bathNoBox.Width, bathNoBox.Height);
            //----------------------------------------------------------------------------- 第2行
            // 标准
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);
            // 牌号
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, rightBlankBox.Width, rightBlankBox.Height);
            //----------------------------------------------------------------------------- 第3行
            // 规格
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);
            // 重量
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, rightBlankBox.Width, rightBlankBox.Height);
            //----------------------------------------------------------------------------- 第4行
            // 生产日期
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, prodDateLeftBox.Width, prodDateLeftBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, prodDateBlankBox.Width, prodDateBlankBox.Height);
            //----------------------------------------------------------------------------- 第5行
            // 底部的条码区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, barcodeBox.Width, barcodeBox.Height);
            // 地址区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, addrBox.Width, addrBox.Height);
            // 坐标单位转换 mm -> dot
            for (i = 0; i < _rects.Length; i++)
            {
                _rects[i].X = (int)(_rects[i].X * dotMM);
                _rects[i].Y = (int)(_rects[i].Y * dotMM) - 70;
                _rects[i].Width = (int)(_rects[i].Width * dotMM);
                _rects[i].Height = (int)(_rects[i].Height * dotMM);
            }

            // 定义字体
            Font CFont = new Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font EFont = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sEFont = new Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sCFont = new Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //Font ssCFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Font cCFont = new Font("隶书", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font eEFont = new Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;
            // add by [bhb]
            StringFormat formatLeft = new StringFormat();
            formatLeft.Alignment = StringAlignment.Near;
            formatLeft.LineAlignment = StringAlignment.Near;
            //===========================画矩形===========================
            //Pen rectPen = new Pen(Brushes.Blue, 2);
            //for (i = 0; i < _rects.Length; i++)
            //{
            //    e.Graphics.DrawRectangle(rectPen, _rects[i]);
            //}

            //===========================打印数据===========================     
            //打印批次号
            if (_lable.BandNo.Length == 1)
                e.Graphics.DrawString(_lable.BatchNo + "   0" + _lable.BandNo, EFont, Brushes.Black, _rects[2], drawFormat1);
            else
                e.Graphics.DrawString(_lable.BatchNo + "   " + _lable.BandNo, EFont, Brushes.Black, _rects[2], drawFormat1);
            //打印标准          
            //e.Graphics.DrawString("Φ" + _lable.Spec + "mm", EFont, Brushes.Black, _rects[4], drawFormat1);
            // 打印牌号\ 增加X的偏移量
            _rects[6].X -= 10;
            _rects[6].Y += 7;
            e.Graphics.DrawString(_lable.SteelType, EFont, Brushes.Black, _rects[6], formatLeft);
            //打印规格          
            e.Graphics.DrawString("Φ" + _lable.Spec + "mm", EFont, Brushes.Black, _rects[8], drawFormat1);
            //打印重量
            e.Graphics.DrawString(_lable.Weight + " kg", EFont, Brushes.Black, _rects[10], drawFormat1); //重量
            //打印生产日期
            e.Graphics.DrawString(_lable.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日" + _lable.Term, CFont, Brushes.Black, _rects[12], drawFormat1); //生产日期
            // 打印条码             
            Code128 c128 = new Code128();
            _rects[13].Y -= 10;
            _rects[13].Height -= 10;
            c128.printBigCode1(_lable.BarCode, _rects[13], e);

            // 打印地址
            if (Data.PrintAddress)
            {
                e.Graphics.DrawString("云南 ● 蒙自 红河钢铁有限公司", sCFont, Brushes.Black, _rects[14], drawFormat1); //地点
            }
        }
        // 二次计量榜单,包括二次钢坯
        private void HandRecodPrint(PrintPageEventArgs e)
        {
            int oX = 20, oY = 40;  //偏移量
            int xStep = 232;
            int yStep = 33;

            Font headFont = new Font("Arial", 14, FontStyle.Bold);
            Font drawFont = new Font("Arial", 9);
            Pen blackPen = new Pen(Color.Black, 2);
            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;

            StringFormat drawFormat2 = new StringFormat();
            drawFormat2.Alignment = StringAlignment.Near;
            drawFormat2.LineAlignment = StringAlignment.Center;

            StringFormat drawFormat3 = new StringFormat();
            drawFormat3.Alignment = StringAlignment.Far;
            drawFormat3.LineAlignment = StringAlignment.Center;

            Rectangle headRec = new Rectangle(oX, oY, 286, yStep);
            Rectangle rec = new Rectangle(oX, oY, xStep, yStep);

            //Pen pen = new Pen(Color.Black, 10);

            headRec.X = oX / 2;
            headRec.Y = oY / 8;
            e.Graphics.DrawString("玉溪联合企业物资计量单", headFont, Brushes.Black, headRec, drawFormat1);

            //合同号
            rec.Y = oY;
            rec.Width = 300; //设置控件宽度
            //rec.Width = xStep;
            e.Graphics.DrawString("合同号: " + Data.ContractNo, drawFont, Brushes.Black, rec, drawFormat2);



            //发货单位
            rec.Y = oY + 1 * yStep;
            //rec.Width = 300;
            e.Graphics.DrawString("发货单位: " + Data.SupplierName, drawFont, Brushes.Black, rec, drawFormat2);

            //收货单位
            rec.Y = oY + 2 * yStep;
            e.Graphics.DrawString("收货单位: " + Data.Receiver, drawFont, Brushes.Black, rec, drawFormat2);

            //物资名称           
            rec.Width = 239; //物料名称太长了换行
            rec.Y = oY + 3 * yStep;
            e.Graphics.DrawString("物资名称: " + Data.MaterialName, drawFont, Brushes.Black, rec, drawFormat2);
            rec.Width = 300; //物料名称太长了换行后还原

            //承运单位
            rec.Y = oY + 4 * yStep;
            e.Graphics.DrawString("承运单位: " + Data.TransName, drawFont, Brushes.Black, rec, drawFormat2);


            if (Data.StoveNo != "")
            {
                //车号
                rec.Y = oY + 7 * yStep;
                rec.Width = xStep; //设置控件宽度
                e.Graphics.DrawString("车号: " + Data.CarNo, drawFont, Brushes.Black, rec, drawFormat2);
            }
            else
            {
                //车号
                rec.Y = oY + 5 * yStep;
                rec.Width = xStep; //设置控件宽度
                e.Graphics.DrawString("车号: " + Data.CarNo, drawFont, Brushes.Black, rec, drawFormat2);
            }

            if (Data.StoveNo != "")
            {
                //炉号
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("炉号: " + Data.StoveNo, drawFont, Brushes.Black, rec, drawFormat2);

                //轧制建议
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("轧制建议: " + Data.MillComment, drawFont, Brushes.Black, rec, drawFormat3);

                //支数
                //rec.Y = oY + 5 * yStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("支(块)数: " + Data.Count, drawFont, Brushes.Black, rec, drawFormat2);

                //建议轧制规格
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("建议轧制规格: " + Data.PlanSpec, drawFont, Brushes.Black, rec, drawFormat3);

                //日期
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("日期: " + Data.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日", drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("时间: " + Data.Date.ToString("HH") + "时" + Data.Date.ToString("mm") + "分" + Data.Date.ToString("ss")+"秒", drawFont, Brushes.Black, rec, drawFormat2);

                //毛重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 7 * yStep;
                e.Graphics.DrawString("毛重: " +  Data.GrossWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //皮重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("皮重: " + Data.TareWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //净重
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("净重: " + Data.NetWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //计量点
                rec.Y = oY + 10 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, drawFormat2);

                //计量员
                rec.Y = oY + 11 * yStep;
                e.Graphics.DrawString("编号: " + Data.BarCode, drawFont, Brushes.Black, rec, drawFormat2);


                //备注
                rec.Y = oY + 12 * yStep;
                yStep = 36;
                e.Graphics.DrawString("备注: " + Data.CarComment, drawFont, Brushes.Black, rec, drawFormat2);



                //打印条码
                Code128 c128 = new Code128();
                //钢坯二次计量
                c128.printCarCode(Data.BarCode, 320, 80, 3, e);

                //注意
                rec.Y = oY + 14 * yStep;
                //yStep = 66;前面备注那已经赋值了
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, drawFormat2);
                e.HasMorePages = false;
            }
            else
            {

                //日期
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("日期: " + Data.Date.ToString(), drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 7 * yStep;
                e.Graphics.DrawString("时间: " + Data.Date.ToString(), drawFont, Brushes.Black, rec, drawFormat2);

                //毛重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("毛重: " + Data.GrossWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //皮重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("皮重: " + Data.TareWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

                // YKL 应扣量 YKBL 应扣比例
                if ((string.IsNullOrEmpty(Data.Rate)) && (string.IsNullOrEmpty(Data.DeductWeight)))
                {
                    //净重
                    rec.Y = oY + 7 * yStep;
                    e.Graphics.DrawString("净重: " +  Data.NetWeight+ " t", drawFont, Brushes.Black, rec, drawFormat3);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Data.DeductWeight))
                    {
                        //扣渣
                        rec.Y = oY + 7 * yStep;
                        e.Graphics.DrawString("扣渣量: " + Data.DeductWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);
                    }
                    else
                    {
                        //扣渣
                        rec.Y = oY + 7 * yStep;
                        e.Graphics.DrawString("扣渣比例: " + Data.Rate, drawFont, Brushes.Black, rec, drawFormat3);
                    }
                    rec.Y = oY + 8 * yStep;
                    e.Graphics.DrawString("净重(扣后): " + Data.DeductAfterWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

                }
                //计量点
                rec.Y = oY + 8 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, drawFormat2);

                //计量员
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("编号: " + Data.BarCode, drawFont, Brushes.Black, rec, drawFormat2);


                //备注
                rec.Y = oY + 10 * yStep;
                yStep = 36;
                e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);

                //打印条码
                Code128 c128 = new Code128();
                // 二次重量
                c128.printCarCode(Data.BarCode, 320, 80, 0, e);

                //注意
                rec.Y = oY + 12 * yStep;

                //e.Graphics.DrawString("注意：本凭证请妥善保管遇有问题请致电（8610918）", drawFont, Brushes.Black, rec, drawFormat2);
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, drawFormat2);
                e.HasMorePages = false;
            }
        }
        //外协计量榜单
       private void AssistDataPrint(PrintPageEventArgs e)
       {
           int oX = 20, oY = 40;  //偏移量
           int xStep = 232;
           int yStep = 33;

           Font headFont = new Font("Arial", 14, FontStyle.Bold);
           Font drawFont = new Font("Arial", 9);
           Pen blackPen = new Pen(Color.Black, 2);
           StringFormat drawFormat1 = new StringFormat();
           drawFormat1.Alignment = StringAlignment.Center;
           drawFormat1.LineAlignment = StringAlignment.Center;

           StringFormat drawFormat2 = new StringFormat();
           drawFormat2.Alignment = StringAlignment.Near;
           drawFormat2.LineAlignment = StringAlignment.Center;

           StringFormat drawFormat3 = new StringFormat();
           drawFormat3.Alignment = StringAlignment.Far;
           drawFormat3.LineAlignment = StringAlignment.Center;

           Rectangle headRec = new Rectangle(oX, oY, 286, yStep);
           Rectangle rec = new Rectangle(oX, oY, xStep, yStep);

           //Pen pen = new Pen(Color.Black, 10);

           headRec.X = oX / 2;
           headRec.Y = oY / 8;
           e.Graphics.DrawString("玉溪联合企业物资计量单", headFont, Brushes.Black, headRec, drawFormat1);


           //车号
           rec.Y = oY;
           rec.Width = xStep; //设置控件宽度
           e.Graphics.DrawString("车号: " + Data.CarNo, drawFont, Brushes.Black, rec, drawFormat2);

           //日期
           rec.Y = oY + 1 * yStep;
           e.Graphics.DrawString("日期: " + Data.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日", drawFont, Brushes.Black, rec, drawFormat2);
           //时间
           rec.Y = oY + 1 * yStep;
           e.Graphics.DrawString("时间: " + Data.Date.ToString("HH") + "时" + Data.Date.ToString("mm") + "分" + Data.Date.ToString("ss") + "秒", drawFont, Brushes.Black, rec, drawFormat3);

           //重量   ???
           rec.Y = oY;
           e.Graphics.DrawString("重量: " + Data.Weight + " t", drawFont, Brushes.Black, rec, drawFormat3);


           //计量点
           rec.Y = oY + 2 * yStep;
           rec.Width = 300;
           e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, drawFormat2);

           //计量员 改为编号
           rec.Y = oY + 3 * yStep;
           e.Graphics.DrawString("编号: " +  Data.BarCode, drawFont, Brushes.Black, rec, drawFormat2);

           //备注
           rec.Y = oY + 4 * yStep;
           yStep = 36;
           e.Graphics.DrawString("备注:" + " 外协" + "    收费金额:" + Data.Charge + " 元", drawFont, Brushes.Black, rec, drawFormat2);

           //打印条码
           Code128 c128 = new Code128();
           c128.printCarCode(Data.BarCode, 320, 80, 2, e);

           //注意
           rec.Y = oY + 6 * yStep;
           //yStep = 66;前面备注那已经赋值了
           e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, drawFormat2);
           return;
       }
     // 二次计量  ?? 
     private void SecondWeightPrint(PrintPageEventArgs e)
     {


         int oX = 20, oY = 40;  //偏移量
         int xStep = 232;
         int yStep = 33;

         Font headFont = new Font("Arial", 14, FontStyle.Bold);
         Font drawFont = new Font("Arial", 9);
         Pen blackPen = new Pen(Color.Black, 2);
         StringFormat drawFormat1 = new StringFormat();
         drawFormat1.Alignment = StringAlignment.Center;
         drawFormat1.LineAlignment = StringAlignment.Center;

         StringFormat drawFormat2 = new StringFormat();
         drawFormat2.Alignment = StringAlignment.Near;
         drawFormat2.LineAlignment = StringAlignment.Center;

         StringFormat drawFormat3 = new StringFormat();
         drawFormat3.Alignment = StringAlignment.Far;
         drawFormat3.LineAlignment = StringAlignment.Center;

         Rectangle headRec = new Rectangle(oX, oY, 286, yStep);
         Rectangle rec = new Rectangle(oX, oY, xStep, yStep);

         //Pen pen = new Pen(Color.Black, 10);

         headRec.X = oX / 2;
         headRec.Y = oY / 8;
         e.Graphics.DrawString("玉溪联合企业物资计量单", headFont, Brushes.Black, headRec, drawFormat1);

         //合同号
         rec.Y = oY;
         rec.Width = 300; //设置控件宽度
         //rec.Width = xStep;
         e.Graphics.DrawString("合同号: " + Data.ContractNo, drawFont, Brushes.Black, rec, drawFormat2);



         //发货单位
         rec.Y = oY + 1 * yStep;
         //rec.Width = 300;
         e.Graphics.DrawString("发货单位: " + Data.SupplierName, drawFont, Brushes.Black, rec, drawFormat2);

         //收货单位
         rec.Y = oY + 2 * yStep;
         e.Graphics.DrawString("收货单位: " + Data.Receiver, drawFont, Brushes.Black, rec, drawFormat2);

         //物资名称           
         rec.Width = 239; //物料名称太长了换行
         rec.Y = oY + 3 * yStep;
         e.Graphics.DrawString("物资名称: " + Data.MaterialName, drawFont, Brushes.Black, rec, drawFormat2);
         rec.Width = 300; //物料名称太长了换行后还原

         //承运单位
         rec.Y = oY + 4 * yStep;
         e.Graphics.DrawString("承运单位: " + Data.TransName, drawFont, Brushes.Black, rec, drawFormat2);


         if (!string.IsNullOrEmpty(Data.StoveNo))
         {
             //车号
             rec.Y = oY + 7 * yStep;
             rec.Width = xStep; //设置控件宽度
             e.Graphics.DrawString("车号: " + Data.CarNo, drawFont, Brushes.Black, rec, drawFormat2);
         }
         else
         {
             //车号
             rec.Y = oY + 5 * yStep;
             rec.Width = xStep; //设置控件宽度
             e.Graphics.DrawString("车号: " + Data.CarNo, drawFont, Brushes.Black, rec, drawFormat2);
         }

         if (Data.StoveNo != "")
         {
             //炉号
             rec.Y = oY + 5 * yStep;
             e.Graphics.DrawString("炉号: " + Data.StoveNo, drawFont, Brushes.Black, rec, drawFormat2);

             //轧制建议
             //rec.X = oX + 2 * xStep;
             rec.Y = oY + 5 * yStep;
             e.Graphics.DrawString("轧制建议: " + Data.MillComment, drawFont, Brushes.Black, rec, drawFormat3);

             //支数
             //rec.Y = oY + 5 * yStep;
             rec.Y = oY + 6 * yStep;
             e.Graphics.DrawString("支(块)数: " + Data.Count, drawFont, Brushes.Black, rec, drawFormat2);

             //建议轧制规格
             //rec.X = oX + 2 * xStep;
             rec.Y = oY + 6 * yStep;
             e.Graphics.DrawString("建议轧制规格: " + Data.PlanSpec, drawFont, Brushes.Black, rec, drawFormat3);

             //日期
             rec.Y = oY + 8 * yStep;
             e.Graphics.DrawString("日期: " + "日期: " + Data.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日", drawFont, Brushes.Black, rec, drawFormat2);
             //时间
             rec.Y = oY + 9 * yStep;
             e.Graphics.DrawString("时间: " + "时间: " + Data.Date.ToString("HH") + "时" + Data.Date.ToString("mm") + "分" + Data.Date.ToString("ss") + "秒", drawFont, Brushes.Black, rec, drawFormat2);

             //毛重
             //rec.X = oX + 2 * xStep;
             rec.Y = oY + 7 * yStep;
             e.Graphics.DrawString("毛重: " + Data.GrossWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

             //皮重
             //rec.X = oX + 2 * xStep;
             rec.Y = oY + 8 * yStep;
             e.Graphics.DrawString("皮重: " + Data.TareWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

             //净重
             rec.Y = oY + 9 * yStep;
             e.Graphics.DrawString("净重: " + Data.NetWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

             //计量点
             rec.Y = oY + 10 * yStep;
             rec.Width = 300;
             e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, drawFormat2);

             //计量员
             rec.Y = oY + 11 * yStep;
             e.Graphics.DrawString("编号: " + Data.BarCode, drawFont, Brushes.Black, rec, drawFormat2);


             //备注
             rec.Y = oY + 12 * yStep;
             yStep = 36;
             e.Graphics.DrawString("备注: " + Data.CarComment, drawFont, Brushes.Black, rec, drawFormat2);



             //打印条码
             Code128 c128 = new Code128();
             //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
             c128.printCarCode(Data.BarCode, 320, 80, 3, e);

             //注意
             rec.Y = oY + 14 * yStep;
             //yStep = 66;前面备注那已经赋值了
             e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, drawFormat2);
             e.HasMorePages = false;
         }
         else
         {

             //日期
             rec.Y = oY + 6 * yStep;
             e.Graphics.DrawString("日期: " +  Data.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日", drawFont, Brushes.Black, rec, drawFormat2);
             //时间
             rec.Y = oY + 7 * yStep;
             e.Graphics.DrawString("时间: " + Data.Date.ToString("HH") + "时" + Data.Date.ToString("mm") + "分" + Data.Date.ToString("ss") + "秒", drawFont, Brushes.Black, rec, drawFormat2);

             //毛重
             //rec.X = oX + 2 * xStep;
             rec.Y = oY + 5 * yStep;
             e.Graphics.DrawString("毛重: " + Data.GrossWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

             //皮重
             //rec.X = oX + 2 * xStep;
             rec.Y = oY + 6 * yStep;
             e.Graphics.DrawString("皮重: " + Data.TareWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);


             bool isYKL = false;
             if (string.IsNullOrEmpty(Data.DeductWeight) && Data.DeductWeight != "0") isYKL = true;
                 

             bool isYKBL = false;
             if (string.IsNullOrEmpty(Data.Rate) && Data.Rate != "0") isYKBL = true;
                 

             if ((!isYKL) && (!isYKBL))
             {
                 //净重
                 rec.Y = oY + 7 * yStep;
                 e.Graphics.DrawString("净重: " + Data.NetWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);
             }
             else
             {
                 if (isYKL)
                 {
                     //扣渣
                     rec.Y = oY + 7 * yStep;
                     e.Graphics.DrawString("扣渣量: " + Data.DeductWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);
                 }
                 else
                 {
                     //扣渣
                     rec.Y = oY + 7 * yStep;
                     e.Graphics.DrawString("扣渣比例: " + Data.Rate, drawFont, Brushes.Black, rec, drawFormat3);
                 }


                 rec.Y = oY + 8 * yStep;
                 e.Graphics.DrawString("净重(扣后): " + Data.DeductAfterWeight + " t", drawFont, Brushes.Black, rec, drawFormat3);

             }




             //计量点
             rec.Y = oY + 8 * yStep;
             rec.Width = 300;
             e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, drawFormat2);

             //计量员
             rec.Y = oY + 9 * yStep;
             e.Graphics.DrawString("编号: " +Data.BarCode, drawFont, Brushes.Black, rec, drawFormat2);


             //备注
             rec.Y = oY + 10 * yStep;
             yStep = 36;
             e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);

             //打印条码
             Code128 c128 = new Code128();
             //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
             c128.printCarCode(Data.BarCode, 320, 80, 0, e);

             //注意
             rec.Y = oY + 12 * yStep;

             //e.Graphics.DrawString("注意：本凭证请妥善保管遇有问题请致电（8610918）", drawFont, Brushes.Black, rec, drawFormat2);
             e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, drawFormat2);
             e.HasMorePages = false;
         }
     }
     // 打印汽车衡榜单
     private void CarWeightPrint(PrintPageEventArgs e)
     {
         int oX = 20, oY = 40;  //偏移量
         int xStep = 232 + 40;
         int yStep = 25;

         Font headFont = new Font("Arial", 14, FontStyle.Bold);
         Font drawFont = new Font("Arial", 9);
         Pen blackPen = new Pen(Color.Black, 2);
         StringFormat formatCenter = new StringFormat();
         formatCenter.Alignment = StringAlignment.Center;
         formatCenter.LineAlignment = StringAlignment.Center;

         StringFormat formatLeft = new StringFormat();
         formatLeft.Alignment = StringAlignment.Near;
         formatLeft.LineAlignment = StringAlignment.Center;

         StringFormat formatRight = new StringFormat();
         formatRight.Alignment = StringAlignment.Far;
         formatRight.LineAlignment = StringAlignment.Center;

         Rectangle headRec = new Rectangle(oX, oY, 286, yStep);
         Rectangle rec = new Rectangle(oX, oY, xStep, yStep);
         Rectangle rec2 = new Rectangle(oX, oY, xStep, yStep*2);

         //Pen pen = new Pen(Color.Black, 10);

         headRec.X = oX / 2;
         headRec.Y = oY / 8;
         e.Graphics.DrawString("玉溪联合企业物资计量单", headFont, Brushes.Black, headRec, formatCenter);

         //获取服务器时间
         string strDate = Data.Date.ToString("yyyy") + "-" + _lable.Date.ToString("MM") + "-" + _lable.Date.ToString("dd");
         string strTime = Data.Date.ToString("HH") + ":" + Data.Date.ToString("mm") + ":" + Data.Date.ToString("ss");

         #region 外协
         if (!string.IsNullOrEmpty(Data.Charge))
         {
             //车号
             rec.Y = oY;
             rec.Width = xStep; //设置控件宽度
             e.Graphics.DrawString("车号: " + Data.CarNo, drawFont, Brushes.Black, rec, formatLeft);

             //日期
             rec.Y = oY + 1 * yStep;
             e.Graphics.DrawString("日期: " + strDate, drawFont, Brushes.Black, rec, formatLeft);
             //时间
             rec.Y = oY + 1 * yStep;
             //rec.X += 10;
             e.Graphics.DrawString("时间: " + strTime, drawFont, Brushes.Black, rec, formatRight);

             //重量
             rec.Y = oY;
             e.Graphics.DrawString("重量: " + Data.Weight + " t", drawFont, Brushes.Black, rec, formatRight);

             //计量点
             rec.Y = oY + 2 * yStep;
             rec.Width = 300;
             e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, formatLeft);

             //计量员 改为编号
             rec.Y = oY + 3 * yStep;
             rec.Width = xStep;
             e.Graphics.DrawString("编号: " + Data.BarCode, drawFont, Brushes.Black, rec, formatLeft);

             //卡号
             rec.Y = oY + 3 * yStep;
             e.Graphics.DrawString("卡号: " + Data.CardNumber, drawFont, Brushes.Black, rec, formatRight);

             //备注
             rec.Y = oY + 4 * yStep;
             rec.Width = 300;
             yStep = 36;
             e.Graphics.DrawString("备注: 外协     收费金额:" + Data.Charge + " 元", drawFont, Brushes.Black, rec, formatLeft);

             //打印条码
             Code128 c128 = new Code128();
             c128.printCarCode(Data.BarCode, 320, 60, 2, e);

             //注意
             rec.Y = oY + 6 * yStep - 10;
             //yStep = 66;前面备注那已经赋值了
             rec.Height += 20;
             rec.Width = 300;
             e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, formatLeft);
             return;
         }
         #endregion
         //合同号
         rec.Y = oY;
         rec.Width = 300; //设置控件宽度
         //rec.Width = xStep;
         e.Graphics.DrawString("合同(订单)号: " + Data.ContractNo, drawFont, Brushes.Black, rec, formatLeft);




         //发货单位
         rec.Y = oY + 1 * yStep;
         //rec.Width = 300;
         e.Graphics.DrawString("发货单位: " + Data.SupplierName, drawFont, Brushes.Black, rec, formatLeft);

         //收货单位
         rec.Y = oY + 2 * yStep;
         e.Graphics.DrawString("收货单位: " + Data.Receiver, drawFont, Brushes.Black, rec, formatLeft);

         //物资名称     
         rec.Width = 239; //物料名称太长了换行
         rec.Y = oY + 3 * yStep;
         e.Graphics.DrawString("物资名称: " + Data.MaterialName, drawFont, Brushes.Black, rec, formatLeft);
         rec.Width = 300; //物料名称太长了换行后还原

         //承运单位
         rec.Y = oY + 4 * yStep;
         e.Graphics.DrawString("承运单位: " + Data.TransName, drawFont, Brushes.Black, rec, formatLeft);

         if (!string.IsNullOrEmpty(Data.StoveNo))
         {
             //车号
             rec.Y = oY + 8 * yStep;
             rec.Width = xStep; //设置控件宽度
             e.Graphics.DrawString("车号: " + Data.CarNo, drawFont, Brushes.Black, rec, formatLeft);
         }
         else
         {
             //车号
             rec.Y = oY + 5 * yStep;
             rec.Width = xStep; //设置控件宽度
             e.Graphics.DrawString("车号: " + Data.CarNo, drawFont, Brushes.Black, rec, formatLeft);
         }

         ////承运单位
         //rec.Y = oY + 5 * yStep;
         //e.Graphics.DrawString("承运单位:" + comboBox5.Text, drawFont, Brushes.Black, rec, formatLeft);

         //钢坯二次计量打印
         if (!string.IsNullOrEmpty(Data.StoveNo) && Data.WeightNum == "1")
         {
             //炉号
             rec.Y = oY + 5 * yStep;
             rec2.X = rec.X;
             rec2.Y = rec.Y;
             e.Graphics.DrawString("炉号: " + Data.StoveNo, drawFont, Brushes.Black, rec2, formatLeft);

             //支数
             rec.Y = oY + 7 * yStep;
             e.Graphics.DrawString("支(块)数: " + Data.Count, drawFont, Brushes.Black, rec, formatLeft);
             //毛重
             rec.Y = oY + 7* yStep;
             e.Graphics.DrawString("毛重: " + Data.GrossWeight + " t", drawFont, Brushes.Black, rec, formatRight);


             //皮重
             rec.Y = oY + 8 * yStep;
             e.Graphics.DrawString("皮重: " + Data.TareWeight + " t", drawFont, Brushes.Black, rec, formatRight);

             //计量点
             rec.Y = oY + 9* yStep;
             e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, formatLeft);

             //净重
             rec.Y = oY + 9 * yStep;
             e.Graphics.DrawString("净重: " + Data.NetWeight + " t", drawFont, Brushes.Black, rec, formatRight);

             //日期
             rec.Y = oY + 10 * yStep;
             e.Graphics.DrawString("时间: " + strDate + " " + strTime, drawFont, Brushes.Black, rec, formatLeft);

             //计量员
             rec.Y = oY + 11 * yStep;
             rec.Width = xStep;
             e.Graphics.DrawString("编号: " + Data.BarCode, drawFont, Brushes.Black, rec, formatLeft);

             //卡号
             rec.Y = oY + 11 * yStep;
             e.Graphics.DrawString("卡号: " + Data.CardNumber, drawFont, Brushes.Black, rec, formatRight);

             //备注
             rec.Y = oY + 12 * yStep;
             yStep = 36;
             rec.Width = 300;
             e.Graphics.DrawString("备注:" + Data.CarComment, drawFont, Brushes.Black, rec, formatLeft);

             //打印条码
             Code128 c128 = new Code128();
             c128.printCarCode(Data.BarCode, 320, 60, 3, e);

             //注意
             rec.Y = oY + 13 * yStep - 45;
             rec.Height += 20;
             rec.Width = 300;
             e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, formatLeft);
             return;
         }
         //钢坯一次计量打印
         else if (!string.IsNullOrEmpty(Data.StoveNo) && Data.WeightNum == "")
         {
             //炉号
             rec.Y = oY + 5 * yStep;
             rec2.X = rec.X;
             rec2.Y = rec.Y;
             e.Graphics.DrawString("炉号: " + Data.StoveNo, drawFont, Brushes.Black, rec2, formatLeft);

             //支数
             rec.Y = oY + 7 * yStep;
             e.Graphics.DrawString("支(块)数: " + Data.Count, drawFont, Brushes.Black, rec, formatLeft);

             //重量 ??? 暂时不清楚
             rec.Y = oY + 8 * yStep;
             e.Graphics.DrawString("重量: " + Data.Weight + " t", drawFont, Brushes.Black, rec, formatRight);

             //日期
             rec.Y = oY + 9 * yStep;
             e.Graphics.DrawString("时间: " + strDate + " " + strTime, drawFont, Brushes.Black, rec, formatLeft);
             ////时间
             //rec.Y = oY + 8 * yStep;
             //e.Graphics.DrawString("时间: " + strTime, drawFont, Brushes.Black, rec, formatRight);


             //计量点
             rec.Y = oY + 10 * yStep;
             rec.Width = 300;
             e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, formatLeft);

             //计量员
             rec.Y = oY + 11 * yStep;
             rec.Width = xStep;
             e.Graphics.DrawString("编号: " + Data.BarCode, drawFont, Brushes.Black, rec, formatLeft);

             //卡号
             rec.Y = oY + 11 * yStep;
             e.Graphics.DrawString("卡号: " + Data.CardNumber, drawFont, Brushes.Black, rec, formatRight);

             //备注
             rec.Y = oY + 12 * yStep;
             rec.Width = 300;
             yStep = 36;
             e.Graphics.DrawString("备注:" + Data.CarComment, drawFont, Brushes.Black, rec, formatLeft);

             //打印条码
             Code128 c128 = new Code128();
             c128.printCarCode(Data.BarCode, 320, 60, 4, e);

             //注意
             rec.Y = oY + 13 * yStep - 45;
             rec.Height += 20;
             rec.Width = 300;
             e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, formatLeft);
             return;
         }
         // 二次计量打印
         if (string.IsNullOrEmpty(Data.StoveNo) && Data.WeightNum == "1")
         {
             //毛重
             rec.Y = oY + 5 * yStep;
             e.Graphics.DrawString("毛重: " + Data.GrossWeight + " t", drawFont, Brushes.Black, rec, formatRight);

             //日期
             rec.Y = oY + 6 * yStep;
             e.Graphics.DrawString("日期: " + strDate + " " + strTime, drawFont, Brushes.Black, rec, formatLeft);
             ////时间
             //rec.Y = oY + 7 * yStep;
             //e.Graphics.DrawString("时间: " + strTime, drawFont, Brushes.Black, rec, formatLeft);


             //皮重
             rec.Y = oY + 6 * yStep;
             e.Graphics.DrawString("皮重: " + Data.TareWeight + " t", drawFont, Brushes.Black, rec, formatRight);

             if (string.IsNullOrEmpty(Data.Rate) && string.IsNullOrEmpty(Data.DeductWeight))
             {
                 //净重
                 rec.Y = oY + 7 * yStep;
                 e.Graphics.DrawString("净重: " + Data.NetWeight + " t", drawFont, Brushes.Black, rec, formatRight);
             }
             else
             {
                 if (!string.IsNullOrEmpty(Data.DeductWeight))
                 {
                     //扣渣
                     rec.Y = oY + 7 * yStep;
                     e.Graphics.DrawString("扣渣量: " + Data.DeductWeight + " t", drawFont, Brushes.Black, rec, formatLeft);
                 }
                 else
                 {
                     //扣渣
                     rec.Y = oY + 7 * yStep;
                     e.Graphics.DrawString("扣渣比例: " + Data.Rate, drawFont, Brushes.Black, rec, formatLeft);
                 }


                 rec.Y = oY + 7 * yStep;
                 e.Graphics.DrawString("净重(扣后): " + Data.DeductAfterWeight + " t", drawFont, Brushes.Black, rec, formatRight);

             }
             //计量点
             rec.Y = oY + 8 * yStep;
             rec.Width = 300;
             e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, formatLeft);

             //计量编号
             rec.Y = oY + 9 * yStep;
             rec.Width = xStep;
             e.Graphics.DrawString("编号: " + Data.BarCode, drawFont, Brushes.Black, rec, formatLeft);

             //卡号
             rec.Y = oY + 9 * yStep;
             e.Graphics.DrawString("卡号: " + Data.CardNumber, drawFont, Brushes.Black, rec, formatRight);

             //备注
             rec.Y = oY + 10 * yStep;
             rec.Width = 300;
             yStep = 36;
             e.Graphics.DrawString("备注:" + Data.CarComment, drawFont, Brushes.Black, rec, formatLeft);

             //打印条码
             Code128 c128 = new Code128();
             c128.printCarCode(Data.BarCode, 320, 60, 0, e);

             //注意
             rec.Y = oY + 12 * yStep - 70;
             rec.Height += 20;
             rec.Width = 300;
             e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, formatLeft);
         }
         // 一次计量打印
         else
         {
             //重量
             rec.Y = oY + 5 * yStep;
             e.Graphics.DrawString("重量: " + Data.Weight + " t", drawFont, Brushes.Black, rec, formatRight);

             //时间
             rec.Y = oY + 6 * yStep;
             e.Graphics.DrawString("时间: " + strDate + " " + strTime, drawFont, Brushes.Black, rec, formatLeft);

             //计量点
             rec.Y = oY + 7 * yStep;
             rec.Width = 300;
             e.Graphics.DrawString("计量点: " + Data.WeightPoint, drawFont, Brushes.Black, rec, formatLeft);

             //计量员
             rec.Y = oY + 8 * yStep;
             rec.Width= xStep;
             e.Graphics.DrawString("编号: " + Data.BarCode, drawFont, Brushes.Black, rec, formatLeft);

             //卡号
             rec.Y = oY + 8 * yStep;
             e.Graphics.DrawString("卡号: " + Data.CardNumber, drawFont, Brushes.Black, rec, formatRight);

             //备注
             rec.Y = oY + 9 * yStep;
             rec.Width = 300;
             e.Graphics.DrawString("备注:" + Data.CarComment, drawFont, Brushes.Black, rec, formatLeft);

             //打印条码
             Code128 c128 = new Code128();
             c128.printCarCode(Data.BarCode, 320, 60, 1, e);

             //注意
             rec.Y = oY + 13 * yStep+10;
             rec.Height += 20;
             rec.Width = 300;
             e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, formatLeft);
         }
         e.HasMorePages = false;
     }

     private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            switch (_lable.Type)
            {
                case LableType.BIG:                  
                    BigCardPrint(e);
                    break;
                case LableType.LITTLE:
                    SmallCardPrint(e);                    
                    break;
                case LableType.LITTLE2:                    
                    SmallCardPrint_V2(e);
                    break;
                case LableType.BIG1:
                    HwBigLablePrint(e);                    
                    break;
                case LableType.BIG2:
                    HwBigLablePrint_V2(e);
                    break;
                case LableType.CAR:
                    CarWeightPrint(e);
                    break;
                default:
                    break;
            }
        }
        public void Print()
        {          
            //设置打印机名称            
            if(string.IsNullOrEmpty(PrinterName))
            {
                PrinterName = new PrintDocument().PrinterSettings.PrinterName;
            }
            System.Drawing.Printing.Margins margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            _print_doc.DefaultPageSettings.Margins = margins;
            
            // 设置纸张
            PaperSize paperSize = new PaperSize();
            switch (_lable.Type)
            {
                case LableType.BIG:
                case LableType.BIG1:
                case LableType.BIG2:
                    paperSize.Height = 394; // 70*100
                    paperSize.Width = 285;
                    paperSize.PaperName = "大标牌";
                    _print_doc.DefaultPageSettings.PaperSize = paperSize;
                    break;
                case LableType.LITTLE:
                case LableType.LITTLE2:
                     paperSize.Height = 217;
                     paperSize.Width = 264;
                     paperSize.PaperName = "小标牌";
                     _print_doc.DefaultPageSettings.PaperSize = paperSize;
                    break;                
                default:
                    break;
            }
           
            _print_doc.DefaultPageSettings.PrinterSettings.Copies = (short)_copies;
            _print_doc.Print();            
        }
        #endregion
    }
}
