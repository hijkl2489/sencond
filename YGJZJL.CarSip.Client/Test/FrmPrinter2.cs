using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using System.Diagnostics;
using YGJZJL.PublicComponent;

namespace HGJZJL.HighSpeedWire
{
    public partial class HighWireQuery :CoreFS.CA06.FrmBase
    {
        
        string m_szCurBC = "";
        string m_szCurBZ = "";   
        //钢种对应标准信息
        private DataTable m_SteelTypeStandard = new DataTable();//钢种对应标准信息内存表，存储预报信息
        /// <summary>
        /// 条码打印
        /// </summary>
        struct BarCodeStruct
        {
            public string BatchNo;    //轧制编号
            public string BandNo;     //分卷号
            public string PrintCardType;//标牌打印类型
            public string Spec;       //规格
            public string Weight;     //重量
            public DateTime Date;     //日期
            public string Term;       //班别
            public string BarCode;    //条码
            public string Address;    //地址
            public string Standard;   //标准
            public string SteelType;  //牌号
            public string PrintAddress;   //是否打印地址
            public string PrintStandard;  //是否打印标准
            public string PrintSteelType; //是否打印牌号
        }

        BarCodeStruct m_BarCodeStruct;

        
        LimitQueryTime limitQueryTime = new LimitQueryTime();//为判断时间区间设定的变量
        DateTime beginTime;
        DateTime endTime;
        bool decisionResult;


        public HighWireQuery()
        {
            InitializeComponent();
        }       

        private void SaveWeightForHand()
        {
           
        }

        private bool CheckIsNumber(string strNum)
        {
            try
            {
                float i = Convert.ToSingle(strNum);
                return true;
            }
            catch (Exception exp)
            {
                WriteLog(exp.Message);
                return false;
            }
        }

        private void WriteLog(string str)
        {
            if (System.IO.Directory.Exists(System.Environment.CurrentDirectory + "\\log") == false)
            {
                System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\log");
            }

            string strDate = System.DateTime.Now.ToString("yyyyMMdd");
            System.IO.TextWriter tw = new System.IO.StreamWriter(System.Environment.CurrentDirectory + "\\log\\BCWeight_" + strDate + ".log", true);

            tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            tw.WriteLine(str);
            tw.WriteLine("\r\n");

            tw.Close();
        }

        /// <summary>
        /// 判断录入的轧制编号是否存在
        /// </summary>
        /// <param name="strBatchNo"></param>
        /// <returns></returns>
        private bool CheckIsExist(string strBatchNo)
        {
            try
            {
                string strSql = "select * from DT_GX_STORAGEWEIGHTMAIN where fs_batchno = '" + strBatchNo + "'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return false;
            }
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {

        }

        private void BigCardPrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int oX = -3, oY = -13;  //偏移量

            int i = 53;
            int j = 2;

            int[] xPos = { 0, 50, 150, 200, 300 };
            int[] yPos = { 0, 182, 212, 242, 272, 302, 332, 362, 412, 420 };

            Font CFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font EFont = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font EFont1 = new Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sEFont = new Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sCFont = new Font("隶书", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //Font ssCFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Font cCFont = new Font("隶书", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font eEFont = new Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;
            //------------------------------------------------------------------------------------------1行
            Rectangle rec;


            rec = new Rectangle(oX + xPos[1] - 10 - j, oY + yPos[1] + 17 - i, xPos[4] - xPos[1], yPos[2] - yPos[1]);
            rec.Height = 30;

            if (m_BarCodeStruct.BandNo.Length == 1)
                e.Graphics.DrawString(m_BarCodeStruct.BatchNo + " 0" + m_BarCodeStruct.BandNo, EFont, Brushes.Black, rec, drawFormat1);  //批次号
            else
                e.Graphics.DrawString(m_BarCodeStruct.BatchNo + " " + m_BarCodeStruct.BandNo, EFont, Brushes.Black, rec, drawFormat1);  //批次号

            //------------------------------------------------------------------------------------------2行
            if (m_BarCodeStruct.PrintStandard == "1")
            {
                rec.X = xPos[1] + oX + 4 - j;
                rec.Y = yPos[3] + oY - 12 - i;
                rec.Width = xPos[2] - xPos[1];
                rec.Height = 30;
                e.Graphics.DrawString(m_BarCodeStruct.Standard, sEFont, Brushes.Black, rec, drawFormat1); //标准
            }

            if (m_BarCodeStruct.PrintSteelType == "1")
            {
                rec.X = xPos[3] + oX - 12 - j; //35
                rec.Y = yPos[3] + oY - 12 - i;
                rec.Width = xPos[4] - xPos[3];
                rec.Height = 34;
                e.Graphics.DrawString(m_BarCodeStruct.SteelType, EFont1, Brushes.Black, rec, drawFormat1); //牌号
            }

            //--------------------------------------------------------------------------------------------3行
            rec.X = xPos[1] + oX - j;
            rec.Y = yPos[4] + oY - 10 - i;
            rec.Width = xPos[2] - xPos[1];
            rec.Height = 30;
            e.Graphics.DrawString("Φ" + m_BarCodeStruct.Spec + "mm", EFont, Brushes.Black, rec, drawFormat1); //规格



            rec.X = xPos[3] + oX - 12 - j; //35
            rec.Y = yPos[4] + oY - 10 - i;
            rec.Width = xPos[4] - xPos[3];
            rec.Height = 30;
            e.Graphics.DrawString(Convert.ToString(Convert.ToSingle(m_BarCodeStruct.Weight)) + " kg", EFont, Brushes.Black, rec, drawFormat1); //重量



            //---------------------------------------------------------------------------------------------5行
            rec.X = xPos[1] + oX + xPos[1] - 15;
            rec.Y = yPos[5] + oY - 10 - i;
            rec.Width = xPos[2] - xPos[1] + 30;
            rec.Height = 30;
            e.Graphics.DrawString(m_BarCodeStruct.Date.ToString("yyyy") + "    " + m_BarCodeStruct.Date.ToString("MM") + "    " + m_BarCodeStruct.Date.ToString("dd") + "   ", eEFont, Brushes.Black, rec, drawFormat1); //生产日期

            //---------------------------------------------------------------------------------------------5行
            rec.X = xPos[1] + oX + xPos[1] - 15;
            rec.Y = yPos[5] + oY - 10 - i;
            rec.Width = xPos[2] - xPos[1] + 50;
            rec.Height = 30;
            e.Graphics.DrawString("      年   月    日", cCFont, Brushes.Black, rec, drawFormat1); //生产日期


            rec.X = xPos[3] + oX - 10; //35
            rec.Y = yPos[5] + oY - 10 - i;
            rec.Width = xPos[4] - xPos[3];
            rec.Height = 30;
            e.Graphics.DrawString(m_BarCodeStruct.Term, CFont, Brushes.Black, rec, drawFormat1); //班别


            //---------------------------------------------------------------------------------------------7行
            if (m_BarCodeStruct.PrintAddress == "1")
            {
                rec.X = xPos[0] + oX;
                rec.Y = yPos[8] + oY - 28 - i;
                rec.Width = xPos[4] - xPos[0];
                rec.Height = 20;
                e.Graphics.DrawString("云南 ● 红钢", sCFont, Brushes.Black, rec, drawFormat1); //地点
            }

            Code128 c128 = new Code128();
            c128.printBigCode(m_BarCodeStruct.BarCode, 280, 50, e);
        }

        private void SmallCardPrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int oX = -3, oY = -13;  //偏移量

            int[] xPos = { 0, 50, 150, 200, 300 };
            int[] yPos = { 0, 40, 212, 242, 70, 90, 332, 362, 412, 420 };

            Font CFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font EFont = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sEFont = new Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font sCFont = new Font("隶书", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //Font ssCFont = new Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Font cCFont = new Font("隶书", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Font eEFont = new Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;
            //------------------------------------------------------------------------------------------1行
            Rectangle rec;
            int i = 40;

            rec = new Rectangle(45, 255 - i, 250, 30);
            rec.Height = 30;

            if (m_BarCodeStruct.BandNo.Length == 1)
                e.Graphics.DrawString(m_BarCodeStruct.BatchNo + " 0" + m_BarCodeStruct.BandNo, EFont, Brushes.Black, rec, drawFormat1);  //批次号
            else
                e.Graphics.DrawString(m_BarCodeStruct.BatchNo + " " + m_BarCodeStruct.BandNo, EFont, Brushes.Black, rec, drawFormat1);  //批次号

            //------------------------------------------------------------------------------------------2行
            //if (m_BarCodeStruct.PrintStandard == "1")
            //{
            //    rec.X = xPos[1] + oX + 4;
            //    rec.Y = yPos[3] + oY - 12;
            //    rec.Width = xPos[2] - xPos[1];
            //    rec.Height = 30;
            //    e.Graphics.DrawString("GB1499.1-2008", sEFont, Brushes.Black, rec, drawFormat1); //标准
            //}

            //if (m_BarCodeStruct.PrintSteelType == "1")
            //{
            //    rec.X = xPos[3] + oX - 12; //35
            //    rec.Y = yPos[3] + oY - 12;
            //    rec.Width = xPos[4] - xPos[3];
            //    rec.Height = 34;
            //    e.Graphics.DrawString(m_BarCodeStruct.SteelType, EFont, Brushes.Black, rec, drawFormat1); //牌号
            //}

            //--------------------------------------------------------------------------------------------3行
            rec.X = 60;
            rec.Y = 276 - i;
            rec.Width = 100;
            rec.Height = 30;
            e.Graphics.DrawString("Φ" + m_BarCodeStruct.Spec + "mm", EFont, Brushes.Black, rec, drawFormat1); //规格



            rec.X = 185; //35
            rec.Y = 276 - i;
            rec.Width = 100;
            rec.Height = 30;
            e.Graphics.DrawString(Convert.ToString(Convert.ToSingle(m_BarCodeStruct.Weight)) + " kg", EFont, Brushes.Black, rec, drawFormat1); //重量



            //---------------------------------------------------------------------------------------------5行
            rec.X = 105;
            rec.Y = 303 - i;
            rec.Width = 130;
            rec.Height = 30;
            e.Graphics.DrawString(m_BarCodeStruct.Date.ToString("yyyy") + "     " + m_BarCodeStruct.Date.ToString("MM") + "   " + m_BarCodeStruct.Date.ToString("dd") + "   ", eEFont, Brushes.Black, rec, drawFormat1); //生产日期

            //---------------------------------------------------------------------------------------------5行
            rec.X = 105;
            rec.Y = 303 - i;
            rec.Width = 150;
            rec.Height = 30;
            e.Graphics.DrawString("      年  月   日", cCFont, Brushes.Black, rec, drawFormat1); //生产日期


            rec.X = 230; //35
            rec.Y = 303 - i;
            rec.Width = 50;
            rec.Height = 30;
            e.Graphics.DrawString(m_BarCodeStruct.Term, CFont, Brushes.Black, rec, drawFormat1); //班别


            //---------------------------------------------------------------------------------------------7行
            //if (m_BarCodeStruct.PrintAddress == "1")
            //{
            //    rec.X = xPos[0] + oX;
            //    rec.Y = yPos[8] + oY - 28;
            //    rec.Width = xPos[4] - xPos[0];
            //    rec.Height = 20;
            //    e.Graphics.DrawString("云南 ● 红钢", sCFont, Brushes.Black, rec, drawFormat1); //地点
            //}

            Code128 c128 = new Code128();
            c128.printSmallCode(m_BarCodeStruct.BarCode, 275, 45, e);
        }


        private bool Decision()//判断所选时间区间是否大于60天，如果大于则返回false//杨滔添加
        {
            beginTime = dpk_begin.Value.Date;
            endTime = dpk_end.Value.Date;
            decisionResult = limitQueryTime.ParseTime(beginTime, endTime);
            if (!decisionResult)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void QueryBCWeightMainData()
        {
            try
            {
                string strSql = "select FS_BATCHNO,FS_PRODUCTNO,FS_ITEMNO,FS_MATERIALNO,FS_MATERIALNAME,FS_MRP,FS_STEELTYPE,FS_SPEC,FS_STORE,FN_BANDCOUNT,to_char(FN_TOTALWEIGHT,'FM99990.000') AS FN_TOTALWEIGHT,FD_STARTTIME,FD_ENDTIME,FS_COMPLETEFLAG,FS_AUDITOR,FD_AUDITTIME,FS_UPLOADFLAG,FD_TOCENTERTIME,FD_ACCOUNTDATE,FD_TESTIFYDATE,FS_FLOW,FS_CARDNO,FS_ADDRESSCHECK,FS_STANDARDCHECK,FS_STEELTYPECHECK,FS_PRINTCARDTYPE,FS_SAPSTORE,FS_ACCOUNTDATE,FS_HEADER from DT_GX_STORAGEWEIGHTMAIN where FD_STARTTIME between to_date('" + this.dpk_begin.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + this.dpk_end.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-MM-dd HH24:mi:ss')";
                if (this.tbx_BatchNo.Text.Trim() != "")
                {
                    strSql += " and fs_batchno = '" + this.tbx_BatchNo.Text.Trim() + "'";
                }
                strSql += " order by fs_batchno";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.dataSet1.Tables[0].Rows.Clear();
                this.dataSet2.Tables[0].Rows.Clear();
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        DataRow newRow = this.dataSet1.Tables[0].NewRow();
                        foreach (DataColumn dc in this.dataSet1.Tables[0].Columns)
                        {
                            newRow[dc.ColumnName] = dt_temp.Rows[i][dc.ColumnName].ToString();
                        }
                        this.dataSet1.Tables[0].Rows.Add(newRow);
                    }
                    HGJZJL.PublicComponent.Constant.RefreshAndAutoSize(this.ultraGrid1);
                    this.dataSet1.Tables[0].AcceptChanges();
                }
                else
                { }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }        

        private void QueryBCWeightDetailData(string strBatchNo)
        {
            try
            {
                string strSql = "select FS_WEIGHTNO,FS_BATCHNO,FN_BANDNO,FN_HOOKNO,FN_BANDBILLETCOUNT,to_char(FN_WEIGHT,'FM99990.000') AS FN_WEIGHT,FS_PERSON,FS_POINT,FD_DATETIME,FS_SHIFT,FS_TERM,FS_LABELID,FN_DECWEIGHT,FS_FLAG,FS_ISPACKWIRE from dt_gx_storageweightdetail where ";

                strSql += " fs_batchno = '" + strBatchNo + "'";
                strSql += " order by fn_bandno,length(fn_bandno)";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.dataSet2.Tables[0].Rows.Clear();
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        DataRow newRow = this.dataSet2.Tables[0].NewRow();
                        foreach (DataColumn dc in this.dataSet2.Tables[0].Columns)
                        {
                            newRow[dc.ColumnName] = dt_temp.Rows[i][dc.ColumnName].ToString();
                        }
                        this.dataSet2.Tables[0].Rows.Add(newRow);
                    }

                    HGJZJL.PublicComponent.Constant.RefreshAndAutoSize(this.ultraGrid2);
                    this.dataSet2.Tables[0].AcceptChanges();
                }
                else
                { }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 手工补录从表
        /// </summary>
        private void DoHandSaveForGX()
        {
            if (this.ultraGrid1.Rows.Count == 0 || this.ultraGrid1.ActiveRow == null)
            {
                return;
            }
            if (this.tbx_HookNo.Text.Trim() == "" || this.tbx_weight.Text.Trim() == "" || this.tbx_bandno.Text.Trim() == "") //应该做是否数字的校验,吊号、钩号、重量缺一不可
            {
                return;
            }

            string strBatchNo = this.ultraGrid1.ActiveRow.Cells["FS_BATCHNO"].Text.Trim(); //轧制编号
            string strHookNo = this.tbx_HookNo.Text.Trim(); //钩号
            string strWeight = this.tbx_weight.Text.Trim(); //重量
            string strWeightTime = this.dpk_weighttime.Value.ToString("yyyyMMddHHmmss");// 计量时间
            string strBandNo = this.tbx_bandno.Text.Trim(); //吊号
            string strWeightNo = Guid.NewGuid().ToString();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "hgjzjl.storageweight.StoreageWeight_BC";
            ccp.MethodName = "DoHandSaveForGX";
            ccp.ServerParams = new object[] {strWeightNo,
                strBatchNo,
                strBandNo,
                strHookNo,
                strWeight,
                strWeightTime};
            CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ret.ReturnCode == 0)
            {
                MessageBox.Show("操作成功！");
                this.QueryBCWeightDetailData(strBatchNo);
            }
            else
            {
                MessageBox.Show(ret.ReturnObject.ToString());
            }
        }

        /// <summary>
        /// 手工补录主表
        /// </summary>
        private void DoHandSaveMainForGX()
        {
            try
            {
                if (this.tbx_Ubatchno.Text.Trim() == "" || this.tbx_Uorder.Text.Trim() == "" || this.tbx_Uspec.Text.Trim() == "" || this.tbx_Usteeltype.Text.Trim() == "" || this.tbx_Ustoveno.Text.Trim() == "")
                {
                    MessageBox.Show("要录入的主记录信息不全！");
                    return;
                }
                if (CheckIsExist(this.tbx_Ubatchno.Text.Trim()))
                {
                    MessageBox.Show("该轧制编号已经存在，不能重复录入！");
                    return;
                }
                string strBatchNo = this.tbx_Ubatchno.Text.Trim();
                string strOrderNo = this.tbx_Uorder.Text.Trim();
                string strSpec = this.tbx_Uspec.Text.Trim();
                string strSteelType = this.tbx_Usteeltype.Text.Trim();
                string strStoveNo = this.tbx_Ustoveno.Text.Trim();
                string strBeginTime = this.dpk_Ubegintime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.storageweight.StoreageWeight_BC";
                ccp.MethodName = "DoHandSaveMainForGX";
                ccp.ServerParams = new object[] {strBatchNo,
                strOrderNo,
                strSpec,
                strSteelType,
                strStoveNo,
                strBeginTime};
                CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ret.ReturnCode == 0)
                {
                    MessageBox.Show("操作成功！");
                    this.tbx_BatchNo.Text = strBatchNo;
                    this.QueryBCWeightMainData();
                }
                else
                {
                    MessageBox.Show(ret.ReturnObject.ToString());
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 更新主表记录
        /// </summary>
        private void UpdateBCWeightMain()
        {
            try
            {
                if (this.ultraGrid1.Rows.Count == 0)
                    return;

                this.ultraGrid1.UpdateData();
                DataTable dt = this.dataSet1.Tables[0].GetChanges();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string p_FS_BATCHNO = dt.Rows[i]["FS_BATCHNO"].ToString();
                        string p_FS_PRODUCTNO = dt.Rows[i]["FS_PRODUCTNO"].ToString();
                        string p_FS_ITEMNO = dt.Rows[i]["FS_ITEMNO"].ToString();
                        string p_FS_MATERIALNO = dt.Rows[i]["FS_MATERIALNO"].ToString();
                        string p_FS_MATERIALNAME = dt.Rows[i]["FS_MATERIALNAME"].ToString();
                        string p_FS_MRP = dt.Rows[i]["FS_MRP"].ToString();
                        string p_FS_STEELTYPE = dt.Rows[i]["FS_STEELTYPE"].ToString();
                        string p_FS_SPEC = dt.Rows[i]["FS_SPEC"].ToString();
                        string p_FS_STORE = dt.Rows[i]["FS_STORE"].ToString();
                        string p_FN_BANDCOUNT = dt.Rows[i]["FN_BANDCOUNT"].ToString();
                        string p_FN_TOTALWEIGHT = dt.Rows[i]["FN_TOTALWEIGHT"].ToString();
                        string p_FD_STARTTIME = dt.Rows[i]["FD_STARTTIME"].ToString();
                        string p_FD_ENDTIME = dt.Rows[i]["FD_ENDTIME"].ToString();
                        string p_FS_COMPLETEFLAG = dt.Rows[i]["FS_COMPLETEFLAG"].ToString();
                        string p_FS_AUDITOR = dt.Rows[i]["FS_AUDITOR"].ToString();
                        string p_FD_AUDITTIME = dt.Rows[i]["FD_AUDITTIME"].ToString();
                        string p_FS_UPLOADFLAG = dt.Rows[i]["FS_UPLOADFLAG"].ToString();
                        string p_FD_TOCENTERTIME = dt.Rows[i]["FD_TOCENTERTIME"].ToString();
                        string p_FD_ACCOUNTDATE = dt.Rows[i]["FD_ACCOUNTDATE"].ToString();
                        string p_FD_TESTIFYDATE = dt.Rows[i]["FD_TESTIFYDATE"].ToString();
                        string p_FS_FLOW = dt.Rows[i]["FS_FLOW"].ToString();
                        string p_FS_CARDNO = dt.Rows[i]["FS_CARDNO"].ToString();
                        string p_FS_ADDRESSCHECK = dt.Rows[i]["FS_ADDRESSCHECK"].ToString();
                        string p_FS_STANDARDCHECK = dt.Rows[i]["FS_STANDARDCHECK"].ToString();
                        string p_FS_STEELTYPECHECK = dt.Rows[i]["FS_STEELTYPECHECK"].ToString();
                        string p_FS_PRINTCARDTYPE = dt.Rows[i]["FS_PRINTCARDTYPE"].ToString();
                        string p_FS_SAPSTORE = dt.Rows[i]["FS_SAPSTORE"].ToString();
                        string p_FS_ACCOUNTDATE = dt.Rows[i]["FS_ACCOUNTDATE"].ToString();
                        string p_FS_HEADER = dt.Rows[i]["FS_HEADER"].ToString();
                        string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "hgjzjl.StoreageWeight_BC";
                        ccp.MethodName = "UpdateGXWeightMain";
                        ccp.ServerParams = new object[] {p_FS_BATCHNO ,
                            p_FS_PRODUCTNO ,
                            p_FS_ITEMNO ,
                            p_FS_MATERIALNO ,
                            p_FS_MATERIALNAME ,
                            p_FS_MRP ,
                            p_FS_STEELTYPE ,
                            p_FS_SPEC ,
                            p_FS_STORE ,
                            p_FN_BANDCOUNT ,
                            p_FN_TOTALWEIGHT ,
                            p_FD_STARTTIME ,
                            p_FD_ENDTIME ,
                            p_FS_COMPLETEFLAG ,
                            p_FS_AUDITOR ,
                            p_FD_AUDITTIME ,
                            p_FS_UPLOADFLAG ,
                            p_FD_TOCENTERTIME ,
                            p_FD_ACCOUNTDATE ,
                            p_FD_TESTIFYDATE ,
                            p_FS_FLOW ,
                            p_FS_CARDNO ,
                            p_FS_ADDRESSCHECK ,
                            p_FS_STANDARDCHECK ,
                            p_FS_STEELTYPECHECK ,
                            p_FS_PRINTCARDTYPE ,
                            p_FS_SAPSTORE ,
                            p_FS_ACCOUNTDATE ,
                            p_FS_HEADER,
                            p_UPDATER};
                        CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        
                        if (ret.ReturnCode != 0)
                        {
                            MessageBox.Show("修改保存失败！");
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    this.QueryBCWeightMainData();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 更新从表记录
        /// </summary>
        private void UpdateBCWeightDetail()
        {
            try
            {
                if (this.ultraGrid2.Rows.Count == 0)
                    return;

                this.ultraGrid2.UpdateData();
                DataTable dt = this.dataSet2.Tables[0].GetChanges();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string p_FS_WEIGHTNO = dt.Rows[i]["FS_WEIGHTNO"].ToString();
                        string p_FS_BATCHNO = dt.Rows[i]["FS_BATCHNO"].ToString();
                        string p_FN_BANDNO = dt.Rows[i]["FN_BANDNO"].ToString();
                        string p_FN_HOOKNO = dt.Rows[i]["FN_HOOKNO"].ToString();
                        string p_FN_BANDBILLETCOUNT = dt.Rows[i]["FN_BANDBILLETCOUNT"].ToString();
                        string p_FN_WEIGHT = dt.Rows[i]["FN_WEIGHT"].ToString();
                        string p_FS_PERSON = dt.Rows[i]["FS_PERSON"].ToString();
                        string p_FS_POINT = dt.Rows[i]["FS_POINT"].ToString();
                        string p_FD_DATETIME = dt.Rows[i]["FD_DATETIME"].ToString();
                        string p_FS_SHIFT = dt.Rows[i]["FS_SHIFT"].ToString();
                        string p_FS_TERM = dt.Rows[i]["FS_TERM"].ToString();
                        string p_FS_LABELID = dt.Rows[i]["FS_LABELID"].ToString();
                        string p_FN_DECWEIGHT = dt.Rows[i]["FN_DECWEIGHT"].ToString();
                        string p_FS_FLAG = dt.Rows[i]["FS_FLAG"].ToString();
                        string p_FS_ISPACKWIRE = dt.Rows[i]["FS_ISPACKWIRE"].ToString();
                        string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "hgjzjl.StoreageWeight_BC";
                        ccp.MethodName = "UpdateGXWeightDetail";
                        ccp.ServerParams = new object[] {p_FS_WEIGHTNO,
                            p_FS_BATCHNO,
                            p_FN_BANDNO,
                            p_FN_HOOKNO,
                            p_FN_BANDBILLETCOUNT,
                            p_FN_WEIGHT,
                            p_FS_PERSON,
                            p_FS_POINT,
                            p_FD_DATETIME,
                            p_FS_SHIFT,
                            p_FS_TERM,
                            p_FS_LABELID,
                            p_FN_DECWEIGHT,
                            p_FS_FLAG,
                            p_FS_ISPACKWIRE,
                            p_UPDATER};
                        CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                        if (ret.ReturnCode != 0)
                        {
                            MessageBox.Show("修改保存失败！");
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    this.QueryBCWeightMainData();
                    this.QueryBCWeightDetailData(dt.Rows[0]["FS_BATCHNO"].ToString());
                    
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 删除从表记录
        /// </summary>
        private void DeleteBCWeightDetail()
        {
            try
            {
                if (this.ultraGrid2.Rows.Count == 0 || this.ultraGrid2.ActiveRow == null)
                {
                    MessageBox.Show("请选中要删除的一条明细数据！");
                    return;
                }

                DialogResult result = MessageBox.Show(this,"是否确定删除选定的明细数据？","提示",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    string p_FS_WEIGHTNO = this.ultraGrid2.ActiveRow.Cells["FS_WEIGHTNO"].Text.Trim();
                    string p_FS_BATCHNO = this.ultraGrid2.ActiveRow.Cells["FS_BATCHNO"].Text.Trim();
                    string p_FN_BANDNO = this.ultraGrid2.ActiveRow.Cells["FN_BANDNO"].Text.Trim();
                    string p_FN_HOOKNO = this.ultraGrid2.ActiveRow.Cells["FN_HOOKNO"].Text.Trim();
                    string p_FN_BANDBILLETCOUNT = this.ultraGrid2.ActiveRow.Cells["FN_BANDBILLETCOUNT"].Text.Trim();
                    string p_FN_WEIGHT = this.ultraGrid2.ActiveRow.Cells["FN_WEIGHT"].Text.Trim();
                    string p_FS_PERSON = this.ultraGrid2.ActiveRow.Cells["FS_PERSON"].Text.Trim();
                    string p_FS_POINT = this.ultraGrid2.ActiveRow.Cells["FS_POINT"].Text.Trim();
                    string p_FD_DATETIME = this.ultraGrid2.ActiveRow.Cells["FD_DATETIME"].Text.Trim();
                    string p_FS_SHIFT = this.ultraGrid2.ActiveRow.Cells["FS_SHIFT"].Text.Trim();
                    string p_FS_TERM = this.ultraGrid2.ActiveRow.Cells["FS_TERM"].Text.Trim();
                    string p_FS_LABELID = this.ultraGrid2.ActiveRow.Cells["FS_LABELID"].Text.Trim();
                    string p_FN_DECWEIGHT = this.ultraGrid2.ActiveRow.Cells["FN_DECWEIGHT"].Text.Trim();
                    string p_FS_FLAG = this.ultraGrid2.ActiveRow.Cells["FS_FLAG"].Text.Trim();
                    string p_FS_ISPACKWIRE = this.ultraGrid2.ActiveRow.Cells["FS_ISPACKWIRE"].Text.Trim();
                    string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "hgjzjl.StoreageWeight_BC";
                    ccp.MethodName = "UpdateGXWeightDetail";
                    ccp.ServerParams = new object[] {p_FS_WEIGHTNO,
                            p_FS_BATCHNO,
                            p_FN_BANDNO,
                            p_FN_HOOKNO,
                            p_FN_BANDBILLETCOUNT,
                            p_FN_WEIGHT,
                            p_FS_PERSON,
                            p_FS_POINT,
                            p_FD_DATETIME,
                            p_FS_SHIFT,
                            p_FS_TERM,
                            p_FS_LABELID,
                            p_FN_DECWEIGHT,
                            p_FS_FLAG,
                            "delete",
                            p_UPDATER};
                    CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    if (ret.ReturnCode != 0)
                    {
                        MessageBox.Show("删除失败！");
                    }
                    else
                    {
                        MessageBox.Show("删除成功！");
                    }
                    this.QueryBCWeightMainData();
                    //this.QueryBCWeightDetailData(dt.Rows[0]["FS_BATCHNO"].Text.Trim());
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 删除主表记录
        /// </summary>
        private void DeleteBCWeightMain()
        {
            try
            {
                if (this.ultraGrid1.Rows.Count == 0 || this.ultraGrid1.ActiveRow == null)
                {
                    MessageBox.Show("请选中要删除的一条主表数据！");
                    return;
                }

                DialogResult result = MessageBox.Show(this, "主表数据删除时，从表数据一同删除，是否确定删除选定的主表数据？", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    string p_FS_BATCHNO = this.ultraGrid1.ActiveRow.Cells["FS_BATCHNO"].Text.Trim();
                    string p_FS_PRODUCTNO = this.ultraGrid1.ActiveRow.Cells["FS_PRODUCTNO"].Text.Trim();
                    string p_FS_ITEMNO = this.ultraGrid1.ActiveRow.Cells["FS_ITEMNO"].Text.Trim();
                    string p_FS_MATERIALNO = this.ultraGrid1.ActiveRow.Cells["FS_MATERIALNO"].Text.Trim();
                    string p_FS_MATERIALNAME = this.ultraGrid1.ActiveRow.Cells["FS_MATERIALNAME"].Text.Trim();
                    string p_FS_MRP = this.ultraGrid1.ActiveRow.Cells["FS_MRP"].Text.Trim();
                    string p_FS_STEELTYPE = this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim();
                    string p_FS_SPEC = this.ultraGrid1.ActiveRow.Cells["FS_SPEC"].Text.Trim();
                    string p_FS_STORE = this.ultraGrid1.ActiveRow.Cells["FS_STORE"].Text.Trim();
                    string p_FN_BANDCOUNT = this.ultraGrid1.ActiveRow.Cells["FN_BANDCOUNT"].Text.Trim();
                    string p_FN_TOTALWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_TOTALWEIGHT"].Text.Trim();
                    string p_FD_STARTTIME = this.ultraGrid1.ActiveRow.Cells["FD_STARTTIME"].Text.Trim();
                    string p_FD_ENDTIME = this.ultraGrid1.ActiveRow.Cells["FD_ENDTIME"].Text.Trim();
                    string p_FS_COMPLETEFLAG = this.ultraGrid1.ActiveRow.Cells["FS_COMPLETEFLAG"].Text.Trim();
                    string p_FS_AUDITOR = this.ultraGrid1.ActiveRow.Cells["FS_AUDITOR"].Text.Trim();
                    string p_FD_AUDITTIME = this.ultraGrid1.ActiveRow.Cells["FD_AUDITTIME"].Text.Trim();
                    string p_FS_UPLOADFLAG = this.ultraGrid1.ActiveRow.Cells["FS_UPLOADFLAG"].Text.Trim();
                    string p_FD_TOCENTERTIME = this.ultraGrid1.ActiveRow.Cells["FD_TOCENTERTIME"].Text.Trim();
                    string p_FD_ACCOUNTDATE = this.ultraGrid1.ActiveRow.Cells["FD_ACCOUNTDATE"].Text.Trim();
                    string p_FD_TESTIFYDATE = this.ultraGrid1.ActiveRow.Cells["FD_TESTIFYDATE"].Text.Trim();
                    string p_FS_FLOW = this.ultraGrid1.ActiveRow.Cells["FS_FLOW"].Text.Trim();
                    string p_FS_CARDNO = this.ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Text.Trim();
                    string p_FS_ADDRESSCHECK = this.ultraGrid1.ActiveRow.Cells["FS_ADDRESSCHECK"].Text.Trim();
                    string p_FS_STANDARDCHECK = this.ultraGrid1.ActiveRow.Cells["FS_STANDARDCHECK"].Text.Trim();
                    string p_FS_STEELTYPECHECK = this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPECHECK"].Text.Trim();
                    string p_FS_PRINTCARDTYPE = this.ultraGrid1.ActiveRow.Cells["FS_PRINTCARDTYPE"].Text.Trim();
                    string p_FS_SAPSTORE = this.ultraGrid1.ActiveRow.Cells["FS_SAPSTORE"].Text.Trim();
                    string p_FS_ACCOUNTDATE = this.ultraGrid1.ActiveRow.Cells["FS_ACCOUNTDATE"].Text.Trim();
                    string p_FS_HEADER = "delete";
                    string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "hgjzjl.storageweight.HighWirePredictInfo";
                    ccp.MethodName = "UpdateGXWeightMain";
                    ccp.ServerParams = new object[] {p_FS_BATCHNO ,
                            p_FS_PRODUCTNO ,
                            p_FS_ITEMNO ,
                            p_FS_MATERIALNO ,
                            p_FS_MATERIALNAME ,
                            p_FS_MRP ,
                            p_FS_STEELTYPE ,
                            p_FS_SPEC ,
                            p_FS_STORE ,
                            p_FN_BANDCOUNT ,
                            p_FN_TOTALWEIGHT ,
                            p_FD_STARTTIME ,
                            p_FD_ENDTIME ,
                            p_FS_COMPLETEFLAG ,
                            p_FS_AUDITOR ,
                            p_FD_AUDITTIME ,
                            p_FS_UPLOADFLAG ,
                            p_FD_TOCENTERTIME ,
                            p_FD_ACCOUNTDATE ,
                            p_FD_TESTIFYDATE ,
                            p_FS_FLOW ,
                            p_FS_CARDNO ,
                            p_FS_ADDRESSCHECK ,
                            p_FS_STANDARDCHECK ,
                            p_FS_STEELTYPECHECK ,
                            p_FS_PRINTCARDTYPE ,
                            p_FS_SAPSTORE ,
                            p_FS_ACCOUNTDATE ,
                            p_FS_HEADER,
                            p_UPDATER};
                    CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    if (ret.ReturnCode != 0)
                    {
                        MessageBox.Show("删除失败！");                       
                    }
                    else
                    {
                        this.QueryBCWeightMainData();
                    }
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void WeightRePrint_BC_Load(object sender, EventArgs e)
        {
            this.dpk_begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

            m_szCurBC = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            m_szCurBZ = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
        }

        private void ultraGrid1_Click(object sender, EventArgs e)
        {
            UIElement mainElement;
            UIElement element;
            Point screenPoint;
            Point clientPoint;
            UltraGridRow row;
            mainElement = this.ultraGrid1.DisplayLayout.UIElement;
            screenPoint = Control.MousePosition;
            clientPoint = this.ultraGrid1.PointToClient(screenPoint);
            element = mainElement.ElementFromPoint(clientPoint);
            if (element == null)
            {
                return;
            }
            row = element.GetContext(typeof(UltraGridRow)) as UltraGridRow;
            if (row == null)
            {
                return;
            }
            if (this.ultraGrid1.ActiveRow == null || this.ultraGrid1.ActiveRow.Index < 0)
            {
                return;
            }
            if (this.ultraGrid1.ActiveRow == null)
            {
                return;
            }

            if (this.ultraGrid1.Rows.Count == 0 || this.ultraGrid1.ActiveRow == null)
            {
                return;
            }

            QueryBCWeightDetailData(this.ultraGrid1.ActiveRow.Cells["FS_BATCHNO"].Text.Trim());
        }

        private void ultraGroupBox2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Decision())//判断时间区间//杨滔添加
            {
                return;
            }
            QueryBCWeightMainData();
        }

        /// <summary>
        /// 修改主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateBCWeightMain();
        }

        /// <summary>
        /// 修改从表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            UpdateBCWeightDetail();
        }

        /// <summary>
        /// 补录主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            DoHandSaveMainForGX();
        }

        /// <summary>
        /// 补录从表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            DoHandSaveForGX();
        }

        /// <summary>
        /// 删除主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            DeleteBCWeightMain();
        }

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            DeleteBCWeightDetail();            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (m_BarCodeStruct.PrintCardType == "1")
                this.BigCardPrint(e);
            else
                this.SmallCardPrint(e);
        }

        private void Print()
        {
            try
            {
                if (this.ultraGrid2.Rows.Count == 0 || this.ultraGrid2.ActiveRow == null)
                    return;
                if (this.ultraGrid1.Rows.Count == 0 || this.ultraGrid1.ActiveRow == null || this.ultraGrid1.ActiveRow.Cells["FS_BATCHNO"].Text != this.ultraGrid2.ActiveRow.Cells["FS_BATCHNO"].Text)
                    return;

                string strBarCode = this.ultraGrid2.ActiveRow.Cells["FS_LABELID"].Text.Trim();
                

                m_BarCodeStruct.BatchNo = this.ultraGrid2.ActiveRow.Cells["FS_BATCHNO"].Text.Trim();//轧制批号
                m_BarCodeStruct.BandNo = this.ultraGrid2.ActiveRow.Cells["FN_BANDNO"].Text.Trim();//分卷号

                //if (this.ultraGrid1.ActiveRow.Cells["FS_PRINTCARDTYPE"].Text.Trim() == "1")
                //    m_BarCodeStruct.PrintCardType = "1";
                //else
                //    m_BarCodeStruct.PrintCardType = "0";

                if ((this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim() == "BL2D" || this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim() == "BL2E") && Convert.ToSingle(this.ultraGrid1.ActiveRow.Cells["FS_SPEC"].Text.Trim()) < 22)
                    m_BarCodeStruct.SteelType = "BL2";
                else
                    m_BarCodeStruct.SteelType = this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim();


                m_BarCodeStruct.Spec = this.ultraGrid1.ActiveRow.Cells["FS_SPEC"].Text.Trim();
                m_BarCodeStruct.Weight = (Math.Round(Convert.ToDecimal(this.ultraGrid2.ActiveRow.Cells["FN_WEIGHT"].Text), 3) * 1000).ToString();
                m_BarCodeStruct.BarCode = strBarCode;
                //m_BarCodeStruct.Date = DateTime.Now;
                m_BarCodeStruct.Date = Convert.ToDateTime(this.ultraGrid2.ActiveRow.Cells["FD_DATETIME"].Text.Trim());
                m_BarCodeStruct.Term = this.ultraGrid2.ActiveRow.Cells["FS_TERM"].Text.Trim();//m_szCurBZ;

                if (this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim() == "HPB235")
                    m_BarCodeStruct.PrintCardType = "0";
                else
                    m_BarCodeStruct.PrintCardType = "1";

                if (this.cbPrintStandard.Checked)
                    m_BarCodeStruct.PrintStandard = "1";
                else
                    m_BarCodeStruct.PrintStandard = "0";

                if (this.cbPrintAddress.Checked)
                    m_BarCodeStruct.PrintAddress = "1";
                else
                    m_BarCodeStruct.PrintAddress = "0";

                if (this.cbPrintSteelType.Checked)
                    m_BarCodeStruct.PrintSteelType = "1";
                else
                    m_BarCodeStruct.PrintSteelType = "0";

                m_BarCodeStruct.Standard = QueryStandardBySteelType(m_BarCodeStruct.SteelType);

                

                //打印
                printDocument1.PrinterSettings.PrinterName = "K22";
                System.Drawing.Printing.Margins margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                printDocument1.DefaultPageSettings.Margins = margins;
                printDocument1.Print();
                System.Threading.Thread.Sleep(100);
                printDocument1.Print();

                //PrintPreviewDialog dialog = new PrintPreviewDialog();
                //dialog.Document = this.printDocument1;
                //dialog.PrintPreviewControl.AutoZoom = false;
                //dialog.PrintPreviewControl.Zoom = 0.75;
                //dialog.ShowDialog();  

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 根据钢种查询标准信息
        /// </summary>
        /// <param name="strBatchNo"></param>
        private string QueryStandardBySteelType(string strSteelType)
        {
            if (strSteelType.Length == 0)//轧制编号为空或者该轧制编号的预报信息已经赋值到控件
            {
                return null;
            }

            if (m_SteelTypeStandard == null || m_SteelTypeStandard.Rows == null || m_SteelTypeStandard.Rows.Count == 0)
            {
                QuerySteelTypeStandard();
            }

            int i = 0;
            bool bFound = false;
            for (i = 0; i < m_SteelTypeStandard.Rows.Count; i++)
            {
                if (strSteelType == m_SteelTypeStandard.Rows[i]["fs_SteelType"].ToString().Trim())
                {
                    return m_SteelTypeStandard.Rows[i]["fs_Standard"].ToString().Trim();
                }
            }

            if (bFound == false)//如果没找到，则重新查询数据库
            {
                QuerySteelTypeStandard();

                if (m_SteelTypeStandard == null || m_SteelTypeStandard.Rows == null || m_SteelTypeStandard.Rows.Count == 0)
                {
                    //MessageBox.Show("没有查询到预报信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return "";
                }

                for (i = 0; i < m_SteelTypeStandard.Rows.Count; i++)
                {
                    if (strSteelType == m_SteelTypeStandard.Rows[i]["fs_SteelType"].ToString().Trim())
                    {
                        return m_SteelTypeStandard.Rows[i]["fs_Standard"].ToString().Trim();
                    }
                }
            }

            return "";
        }


        /// <summary>
        /// 查询所有钢种对应标准信息并存储在内存表内
        /// </summary>
        private void QuerySteelTypeStandard()
        {
            try
            {
                m_SteelTypeStandard.Clear();



                string sql = "select fn_id,fs_SteelType,fs_Standard from BT_PRINTCARDSTANDARD";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.highspeedwire.HighSpeedWireInfo";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { sql };
                ccp.SourceDataTable = m_SteelTypeStandard;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Print();
        }
    }
}
