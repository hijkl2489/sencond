using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
//using System.Collections.Generic;
using System.Drawing.Printing;

namespace Core.Sip.Client.App
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
                e.Graphics.DrawString("云南 ● 玉溪", sCFont, Brushes.Black, _rects[14], drawFormat1); //地点
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
            if (_lable.PrintAddress) e.Graphics.DrawString("云南 ● 玉溪", sCFont, Brushes.Black, _rects[9], drawFormat1); //地点
        }

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


            rec = new Rectangle(oX + xPos[1] - 10 - j, yPos[1] + oY + offLine+5 - i, xPos[4] - xPos[1], yPos[2] - yPos[1]);
            rec.Height = 30;

            if (_lable.BandNo.Length == 1)
                e.Graphics.DrawString(_lable.BatchNo.Trim() + "  0" + _lable.BandNo.Trim(), EFont, Brushes.Black, rec, drawFormat1);  //批次号
            else
                e.Graphics.DrawString(_lable.BatchNo.Trim() + "  " + _lable.BandNo.Trim(), EFont, Brushes.Black, rec, drawFormat1);  //批次号

          
            if (_lable.Standard.Length > 0) { 
            rec.X = xPos[1] + oX - j;
            rec.Y = yPos[2] + oY + offLine - i;
            rec.Width = xPos[2] - xPos[1];
            rec.Height = 30;
            e.Graphics.DrawString(_lable.Standard.Trim(), sEFont, Brushes.Black, rec, drawFormat1); //标准
            }

            if (_lable.SteelType.Length > 0) { 
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
                e.Graphics.DrawString("Φ" + _lable.Spec.Trim()+"mm", EFont, Brushes.Black, rec, drawFormat1); //规格



                rec.X = xPos[3] + oX - j; //35
                rec.Y = yPos[3] + oY + offLine - i;
                rec.Width = xPos[4] - xPos[3];
                rec.Height = 34;
                e.Graphics.DrawString(_lable.Length.Trim()+"m", EFont, Brushes.Black, rec, drawFormat1); //长度
           
            //--------------------------------------------------------------------------------------------4行
            rec.X = xPos[1] + oX - j;
            rec.Y = yPos[4] + oY + offLine - i;
            rec.Width = xPos[2] - xPos[1];
            rec.Height = 30;
            e.Graphics.DrawString(_lable.Weight.Trim() + "kg", EFont, Brushes.Black, rec, drawFormat1); //



            rec.X = xPos[3] + oX  - j; //35
            rec.Y = yPos[4] + oY + offLine - i;
            rec.Width = xPos[4] - xPos[3];
            rec.Height = 30;
            e.Graphics.DrawString(_lable.Count.Trim(), EFont, Brushes.Black, rec, drawFormat1); //重量



            //---------------------------------------------------------------------------------------------5行
            rec.X = xPos[1] + oX + xPos[1] - 10 ;
            rec.Y = yPos[5] + oY + offLine -5 - i;
            rec.Width = xPos[2] - xPos[1] + 80;
            rec.Height = 30;
            e.Graphics.DrawString(_lable.Date.ToString("yyyy") + "年" + _lable.Date.ToString("MM") + "月" + _lable.Date.ToString("dd") + "日" + _lable.Term.Trim(), CFont, Brushes.Black, rec, drawFormat1); //生产日期
            


            //---------------------------------------------------------------------------------------------7行
            if (_lable.PrintAddress )
            {
                rec.X = xPos[0] + oX;
                rec.Y = yPos[8] + oY - 43 - i;
                rec.Width = xPos[4] - xPos[0];
                rec.Height = 20;
                e.Graphics.DrawString("云南 ● 玉溪", sCFont, Brushes.Black, rec, drawFormat1); //地点
            }

            Code128 c128 = new Code128();
            c128.printBigCode(_lable.BarCode, 280, 50, e);
        }


        // 打印高线大标牌
        private void PipeCardPrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int lableWith = 70, lableLength = 100, lineHight = 6;
            double dotMM = 4;
            // 顶部和底部的大矩形
            Size topBox = new Size(lableWith,55);
            //// 不填数据的小矩形
            //Size littleBox = new Size(12, lineHight);
            //// 左边空白矩形区
            //Size leftBlankBox = new Size(24, lineHight);
            //// 右边空白矩形区
            //Size rightBlankBox = new Size(22, lineHight);

            ////
            //// 批号的矩形区
            //Size bathNoBox = new Size(lableWith - littleBox.Width, lineHight);
            //// 规格的矩形区
            //Size specBox = new Size(36 - littleBox.Width, lineHight);
            //// 重量矩形区
            //Size weightBox = new Size();
            //weightBox = specBox;
            ////生产时间矩形区
            //Size prodDateLeftBox = new Size(23, lineHight);
            //Size prodDateBlankBox = new Size(40, lineHight);
            //// 条码矩形区
            //Size barcodeBox = new Size(67, 22);

            //// 地址矩形区
            //Size addrBox = new Size(lableWith, 5);

            //左边空白矩形区
            Size littleBox = new Size(14, lineHight);
            //左边空白矩形区
            Size leftBlankBox = new Size(24, lineHight);
            //一行只有一个类型
            Size oneTypeBox = new Size(lableWith - littleBox.Width, lineHight);
            ////生产时间矩形区
            Size prodDateLeftBox = new Size(20, lineHight);
            Size prodDateBlankBox = new Size(40, lineHight);

            // 条码矩形区
            Size barcodeBox = new Size(67, 18);

            // 地址矩形区
            Size addrBox = new Size(lableWith, 5);

            // 矩形坐标定义
            Rectangle[] _rects = new Rectangle[19];
            int i = 0;
            // 标签头部
            _rects[i] = new Rectangle(0, 0, topBox.Width, topBox.Height);
            //----------------------------------------------------------------------------- 第1行
            // 品名
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);
            //----------------------------------------------------------------------------- 第2行
            // 批号
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);
            //----------------------------------------------------------------------------- 第3行
            //标准
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);
            
            //牌号
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);


            //----------------------------------------------------------------------------- 第4行
            // 规格
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);

            //----------------------------------------------------------------------------- 第5行
            //重量
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);

            //支数
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);

            //----------------------------------------------------------------------------- 第6行
            // 生产日期
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, prodDateLeftBox.Width, prodDateLeftBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, prodDateBlankBox.Width, prodDateBlankBox.Height);
            //----------------------------------------------------------------------------- 第7行
            // 底部的条码区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, barcodeBox.Width, barcodeBox.Height);
            // 地址区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height+1, addrBox.Width, addrBox.Height);
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
            Font sEFont = new Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            //打印品名
            e.Graphics.DrawString(_lable.PM, EFont, Brushes.Black, _rects[2], drawFormat1);

            //打印批次号
            if (_lable.BandNo.Length == 1)
                e.Graphics.DrawString(_lable.BatchNo + "   0" + _lable.BandNo, EFont, Brushes.Black, _rects[4], drawFormat1);
            else
                e.Graphics.DrawString(_lable.BatchNo + "   " + _lable.BandNo, EFont, Brushes.Black, _rects[4], drawFormat1);
            //打印标准          
            e.Graphics.DrawString(_lable.Standard, sEFont, Brushes.Black, _rects[6], drawFormat1);
            // 打印牌号\ 增加X的偏移量
           // _rects[6].X -= 10;
            e.Graphics.DrawString(_lable.SteelType, EFont, Brushes.Black, _rects[8], formatLeft);
            //打印规格          
            e.Graphics.DrawString( _lable.Spec, EFont, Brushes.Black, _rects[10], drawFormat1);
            //打印重量
            e.Graphics.DrawString(_lable.Weight, EFont, Brushes.Black, _rects[12], drawFormat1); //重量
            //打印支数
            e.Graphics.DrawString(_lable.Count, EFont, Brushes.Black, _rects[14], drawFormat1); //重量
            //打印生产日期
            e.Graphics.DrawString(_lable.Date.ToString("yyyy") + "-" + _lable.Date.ToString("MM") + "-" + _lable.Date.ToString("dd") + "    " + _lable.Term, CFont, Brushes.Black, _rects[16], drawFormat1); //生产日期
            // 打印条码             
            Code128 c128 = new Code128();
            c128.printPipeCode(_lable.BarCode, _rects[17], e);

            // 打印地址
            if (Data.PrintAddress)
            {
                e.Graphics.DrawString("云南 ● 玉溪", sCFont, Brushes.Black, _rects[18], drawFormat1); //地点
            }
        }


        // 打印制管大标牌
        private void PipeCardPrint2(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int lableWith = 70, lableLength = 100, lineHight = 6;
            double dotMM = 4;
            // 顶部和底部的大矩形
            Size topBox = new Size(lableWith,55);

            //左边空白矩形区
            Size littleBox = new Size(14, lineHight);
            //左边空白矩形区
            Size leftBlankBox = new Size(24, lineHight);
            //一行只有一个类型
            Size oneTypeBox = new Size(lableWith - littleBox.Width, lineHight);
            ////生产时间矩形区
            Size prodDateLeftBox = new Size(20, lineHight);
            Size prodDateBlankBox = new Size(40, lineHight);

            // 条码矩形区
            Size barcodeBox = new Size(67, 18);

            // 地址矩形区
            Size addrBox = new Size(lableWith, 5);

            // 矩形坐标定义
            Rectangle[] _rects = new Rectangle[19];
            int i = 0;
            // 标签头部
            _rects[i] = new Rectangle(0, 0, topBox.Width, topBox.Height);
            //----------------------------------------------------------------------------- 第1行
            // 品名
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);
            //----------------------------------------------------------------------------- 第2行
            // 批号
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);
            //----------------------------------------------------------------------------- 第3行
            //标准
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);
            
            //牌号
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);


            //----------------------------------------------------------------------------- 第4行
            // 规格
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);

            //----------------------------------------------------------------------------- 第5行
            //重量
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, oneTypeBox.Width, oneTypeBox.Height);

            //支数
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, littleBox.Width, littleBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, leftBlankBox.Width, leftBlankBox.Height);

            //----------------------------------------------------------------------------- 第6行
            // 生产日期
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, prodDateLeftBox.Width, prodDateLeftBox.Height);
            i++;
            _rects[i] = new Rectangle(_rects[i - 1].X + _rects[i - 1].Width, _rects[i - 1].Y, prodDateBlankBox.Width, prodDateBlankBox.Height);
            //----------------------------------------------------------------------------- 第7行
            // 底部的条码区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height, barcodeBox.Width, barcodeBox.Height);
            // 地址区
            i++;
            _rects[i] = new Rectangle(0, _rects[i - 1].Y + _rects[i - 1].Height+1, addrBox.Width, addrBox.Height);
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
            Font sEFont = new Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            //打印品名
            _rects[2].Y -= 24;
            e.Graphics.DrawString(_lable.PM, EFont, Brushes.Black, _rects[2], drawFormat1);
            _rects[4].Y -= 24;
            //打印批次号
            if (_lable.BandNo.Length == 1)
                e.Graphics.DrawString(_lable.BatchNo + "   0" + _lable.BandNo, EFont, Brushes.Black, _rects[4], drawFormat1);
            else
                e.Graphics.DrawString(_lable.BatchNo + "   " + _lable.BandNo, EFont, Brushes.Black, _rects[4], drawFormat1);
            
            //打印标准          
            e.Graphics.DrawString(_lable.Standard, EFont, Brushes.Black, _rects[6], drawFormat1);
            // 打印牌号\ 增加X的偏移量
           // _rects[6].X -= 10;
            _rects[8].Y -= 24;
            _rects[8].X = 56;
            e.Graphics.DrawString(_lable.SteelType, EFont, Brushes.Black, _rects[8], drawFormat1);
            
            //打印规格          
            e.Graphics.DrawString( _lable.Spec, EFont, Brushes.Black, _rects[10], drawFormat1);
            //打印重量
            
            e.Graphics.DrawString(_lable.Weight, EFont, Brushes.Black, _rects[12], drawFormat1); //重量
            //打印支数
            //e.Graphics.DrawString(_lable.Count, EFont, Brushes.Black, _rects[14], drawFormat1); //重量
            //打印生产日期
            e.Graphics.DrawString(_lable.Date.ToString("yyyy") + "-" + _lable.Date.ToString("MM") + "-" + _lable.Date.ToString("dd") + "    " + _lable.Term, CFont, Brushes.Black, _rects[16], drawFormat1); //生产日期
            // 打印条码             
            Code128 c128 = new Code128();
            c128.printPipeCode(_lable.BarCode, _rects[17], e);

            // 打印地址
            if (Data.PrintAddress)
            {
                e.Graphics.DrawString("云南 ● 玉溪", sCFont, Brushes.Black, _rects[18], drawFormat1); //地点
            }
        }
       
        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            switch(_lable.Type)
            {
                case LableType.BIG:
                    BigCardPrint(e);
                    break;
                case LableType.LITTLE:
                    SmallCardPrint(e);
                    break;
                case LableType.BIG1:
                    HwBigLablePrint(e);
                    break;
                case LableType.PIPE:
                    PipeCardPrint(e);
                    break;
                case LableType.PIPE2:
                    PipeCardPrint2(e);
                    break;
                default:
                    break;
            }
        }      
        public void Print()
        {          
            //打印
            if (string.IsNullOrEmpty(_print_doc.PrinterSettings.PrinterName))
            {
                _print_doc.PrinterSettings.PrinterName = new System.Drawing.Printing.PrintDocument().PrinterSettings.PrinterName;
            }
            //_print_doc.PrinterSettings.PrinterName = "Zebra 105SL"; // "Microsoft XPS Document Writer"; //
            System.Drawing.Printing.Margins margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            _print_doc.DefaultPageSettings.Margins = margins;
            // 设置纸张
            PaperSize paperSize = new PaperSize();
            switch (_lable.Type)
            {
                case LableType.BIG:
                case LableType.BIG1:
                case LableType.PIPE:
                case LableType.PIPE2:
                    paperSize.Height = 394;
                    paperSize.Width = 285;
                    paperSize.PaperName = "大标牌";
                    _print_doc.DefaultPageSettings.PaperSize = paperSize;
                    break;
                case LableType.LITTLE:
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
