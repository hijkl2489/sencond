using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YGJZJL.CarSip.Client.Printer;
namespace YGJZJL.CarSip.Client.Meas
{

    public class LablePrinter : ZplPrinter
    {
        /******************* 标签位置 ***********************/    
        int blankOffset = 5;   //边界宽带       单位:mm
        int labBoxLen1 = 26;   //左边标签盒长度 单位:mm
        int labBoxLen2 = 29;   //右边标签盒长度 单位:mm
        int fillBoxLen1 = 58;  //左边填充盒长度 单位:mm
        int fillBoxLen2 = 60;  //右边填充盒长度 单位:mm
        int rowHeight = 11;    //行高           单位:mm
        int labHeadLen = 173;  //标签头长度     单位:mm
        int labHeadWith = 29;  //标签头宽带     单位:mm
        int offsetY = 0;       //X坐标偏移量    单位:mm
        int offsetX = 0;       //Y坐标偏移量    单位:mm 
        int version = 2;       //标签版本   1: 横向版本， 2: 纵向版本

        /******************* 可变参数 ***********************/
        string strCmd = ""; // 指令
        int xPos = 0;       // X坐标
        int yPos = 0;       // Y坐标
        int rowNum = 0;     // 字段行数

        /******************* 可变参数 ***********************/

        /// <summary>
        /// 构造函数
        /// </summary>
        public LablePrinter()
            : base()
        {
            Coord = new Coordinate[8];
            for (int i = 0; i < Coord.Length; i++)
            {
                Coord[i] = new Coordinate();
            }
            Data = new string[8];
            Data[0] = "P2303774 12"; // 批号
            Data[1] = "SPCC"; // 牌号
            Data[2] = "_03_A6/18mm"; // 规格
            Data[3] = "9mm";  // 长度
            Data[4] = "2743 kg"; // 重量
            Data[5] = "160"; // 支数
            Data[6] = "2012-03-28 1"; // 生产日期
            Data[7] = "P2303774";  // 条码
            //initLabelSetting();
        }
        public void PrintData()
        {
            string strCmd = "";
            int i = 0;
            DPI = 200;
            FontDirect = 'I';
            FontHeight = 20;
            FontWidth = 8;
            strCmd += getBeginCmd();
            strCmd += getLableHomeCmd(0, 0);
            strCmd += getUtf16BECmd();
            strCmd += getDefaultFontCmd();
            for(; i<Coord.Length - 1; i++)
            {
                strCmd = strCmd + getFontCmd() + fillDataCmd(Coord[i].X, Coord[i].Y, Data[i]);
            }
            // 打印条码 
            BarcodeHeight = 100;
            strCmd += getBarcodeCmd(Coord[i].X, Coord[i].Y, "N", Data[i]);
            strCmd += getEndCmd();
            sendCommand(strCmd);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public bool Init(string ipAddr)
            
        {
            IP = ipAddr;
            initLabelSetting();
            return true;

        }

        /// <summary>
        /// 标签版本属性
        /// </summary>
        public int LableVersion
        {
            get
            {
                return version;
            }
            set 
            {
                version = value;
            }
        }
        /// <summary>
        /// 变量初始化
        /// </summary>
        public void initLabelSetting()
        {
            int topHight = 44;
            int lineHight = 8 * getDotMM();
            int box1Width = 12;
            int batchBoxWidth = 64;
            int box2Width = 27;
            int box3Width = 12;
            int leftX = box1Width * getDotMM();
            int rightX = leftX + (box2Width + box3Width) * getDotMM();
            int initHight = topHight * getDotMM();
            int prodDataX = 10 * getDotMM();
            // line 1
            Coord[0].X = leftX;
            Coord[0].Y = initHight;
            // line 2
            Coord[1].X = rightX;
            Coord[1].Y = Coord[0].Y + lineHight;
            // line 3
            Coord[2].X = leftX;
            Coord[2].Y = Coord[1].Y + lineHight;
            Coord[3].X = rightX;
            Coord[3].Y = Coord[2].Y;
            //line 4
            Coord[4].X = leftX;
            Coord[4].Y = Coord[3].Y + lineHight;
            Coord[5].X = rightX;
            Coord[5].Y = Coord[4].Y;
            //line 5
            Coord[6].X = prodDataX;
            Coord[6].Y = Coord[5].Y + lineHight;
            //line 6  barcode
            Coord[7].X = leftX;
            Coord[7].Y = Coord[6].Y + lineHight;
          
        }

        /// <summary>
        /// 打印标签的一行数据
        /// </summary>
        /// <param name="leftData">左边格子中的数据</param>
        /// <param name="rightData">右边格子中的数据</param>
        private void printLine(string leftData, string rightData)
        {            
            switch (version)
            {
                case 1:
                    // 打印左边的数据
                    xPos = (blankOffset + labBoxLen1 + offsetX) * getDotMM();
                    yPos = (blankOffset + labHeadWith + offsetY) * getDotMM() + rowNum * rowHeight * getDotMM();
                    strCmd += fillDataCmd(xPos, yPos, leftData);
                    // 打印右边的数据, Y 坐标不变
                    xPos = (blankOffset + labBoxLen1 + fillBoxLen1 + labBoxLen2 + offsetX) * getDotMM();
                    strCmd += fillDataCmd(xPos, yPos, rightData);
                    break;
                case 2:
                    xPos = (blankOffset + labHeadWith + rowNum * rowHeight + offsetX) * getDotMM() + FontHeight;
                    yPos = (blankOffset + offsetY) * getDotMM() + fillBoxLen2 * getDotMM() - getStringDots(rightData); //fillBoxLen2
                    strCmd += getFontCmd() + fillDataCmd(xPos, yPos, rightData);
                    // X 坐标不变
                    //xPos = (blankOffset + labHeadWith  + rowNum * rowHeight + offsetX) * dotsMm +fontHeight ;
                    yPos = (blankOffset + fillBoxLen2 + labBoxLen2 + offsetY) * getDotMM() + fillBoxLen1 * getDotMM() - getStringDots(leftData);// 
                    strCmd += getFontCmd() + fillDataCmd(xPos, yPos, leftData);                    
                    break;
            }
            rowNum++;          
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="data">条码数据项</param>
        private void printBarcode(string data)
        {
            //int codeHeight = 150;
            int offset = 5;
            string orientation = "B"; //  N = 正常 （Normal);R = 顺时针旋转90度（Roated);I = 顺时针旋转180度（Inverted);B = 顺时针旋转270度 (Bottom)
            switch (version)
            {
                case 1:
                    xPos = (offset + blankOffset )* getDotMM();
                    yPos = (blankOffset + labHeadWith  + rowNum * rowHeight + offsetY) * getDotMM();
                    orientation = "N";
                    strCmd += getBarcodeCmd(xPos, yPos, orientation, data);
                    break;
                case 2:
                    xPos = (blankOffset + labHeadWith +  rowNum * rowHeight + offset) * getDotMM();//+ barcodeHeight
                    yPos = labHeadLen * getDotMM() - data.Length * FontWidth; //(rowY + offsetY) * dotsMm + rowNum * rowHeight * dotsMm;
                    orientation = "B";
                    strCmd += getBarcodeCmd(xPos, yPos, orientation, data);
                    break;
            }
            
        }
        

     
        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="coilLabel">标签内容</param>
        public void printLable(ColdCoilLable coilLabel)
        {
            initLabelSetting();
            strCmd += getBeginCmd();
            strCmd += getLableHomeCmd(0, 0);
            if(1 == version)strCmd += getDefaultFontCmd();
            printLine(coilLabel.rollNo, coilLabel.specification);
            printLine(coilLabel.coilNo, coilLabel.steelGrade);
            printLine(coilLabel.heatNo, coilLabel.dimension);
            printLine(coilLabel.contractNo, coilLabel.weight);
            printLine(coilLabel.prodDate, coilLabel.checker);
            printLine(coilLabel.licenseNo, coilLabel.licenseMark);
            printBarcode(coilLabel.barcode);
            strCmd += getEndCmd();
            sendCommand(strCmd);
            
        }
        public void TestPrintChinese()
        {
            string sChn = "Φ";
            StringBuilder sResult = new StringBuilder();
            sResult.Append("^XA");
            int nWidth = 0, nHeight = 0;
            int nStartX = 50, nStartY = 0;
            //int len = GETFONTHEX(sChn, "隶书", 180, 20, 0, 1, 0, hexbuf);
            for (int i = 10; i <= 10; i += 10)
            {
                StringBuilder hexbuf = new StringBuilder(21 * 1024);

                int count = GETFONTHEX(sChn, "Arial", 180, 20, 0, 1, 0, hexbuf);
                if (count > 0)
                {
                    nStartY += nHeight + i + 35;
                    string sEnd = "^FO" + nStartX.ToString() + "," + nStartY.ToString() + "^XGOUTSTR" + i.ToString() + ",1,2^FS ";
                    sResult.Append(hexbuf.ToString().Replace("OUTSTR01", "OUTSTR" + i.ToString()) + sEnd);
                }
            }
            sResult.Append("^XZ");
            sendCommand(sResult.ToString());
            System.IO.StreamWriter sw = System.IO.File.CreateText("A.txt");
            sw.WriteLine(sResult.ToString());
            sw.Close();
        }
        /// <summary>
        /// 测试函数 
        /// </summary>
        public void test()
        {

            ColdCoilLable coilLabel = new ColdCoilLable();
            coilLabel.rollNo = "L1-017355-10";
            coilLabel.specification = "GB2009-2011XGSPC";
            coilLabel.steelGrade = "SPCC";
            coilLabel.dimension = "1.8*1205";
            coilLabel.coilNo = "L1-017355-10AA0";
            coilLabel.weight = "28050";
            coilLabel.heatNo = "J12-56790A";
            coilLabel.prodDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            coilLabel.contractNo = "532011080001";
            coilLabel.licenseNo = "E532011";
            coilLabel.licenseMark = "XGHG";
            coilLabel.checker = "";
            coilLabel.barcode = coilLabel.coilNo;
            printLable(coilLabel);
        }
    }
}
