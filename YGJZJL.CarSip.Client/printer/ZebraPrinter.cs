using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
//using System.Windows.Forms;

namespace YGJZJL.CarSip.Client.Printer
{
    public class ZebraPrinter : ZPL2Command
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
        public ZebraPrinter() : base()
        {
            initLabelSetting();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ZebraPrinter(string ipAddr) : base(ipAddr)
        {
            Addr = ipAddr; 
            initLabelSetting();
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
            blankOffset = 5;   //mm
            labBoxLen1 = 26;   //mm
            labBoxLen2 = 29;   //mm
            fillBoxLen1 = 58;  //
            fillBoxLen2 = 60;  //
            rowHeight = 11;
            labHeadLen = 173;
            labHeadWith = 29;
            offsetY = 0; // 0 -> -50
            offsetX = 0;

            // 指令初始化
            strCmd = "";    // 指令
            xPos = 0;       // X坐标
            yPos = 0;       // Y坐标
            rowNum = 0;     // 字段行数
            //
            FontDirect = 'B';

            // 设定坐标偏移量
            switch (version)
            {
                case 1:
                    offsetX = 5;
                    offsetY = 3;
                    break;
                case 2:
                    offsetX = 0;
                    offsetY = -2; // -2 -> -50   for XI4
                    break;
                default:
                    offsetX = 0;
                    offsetY = -1;
                    break;
            }

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
                    xPos = (blankOffset + labHeadWith + rowNum * rowHeight + offsetX) * getDotMM() + fontHeight;
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
                    yPos = labHeadLen * getDotMM() - data.Length * fontWidth; //(rowY + offsetY) * dotsMm + rowNum * rowHeight * dotsMm;
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
