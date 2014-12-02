using CoreFS.CA06;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.PublicComponent;
using Excel;
//using SAPFunctionsOCX;
//using SAPLogonCtrl;
//using SAPTableFactoryCtrl;
//using sapRfcClass;

namespace YGJZJL.Sap
{
    /// <summary>
    /// SAP类
    /// </summary>
    class SapClass : FrmBase
    {

        #region 属性

        /// <summary>
        /// 工艺卡查询条件
        /// </summary>
        private string _strCardWhere;
        public string strCardWhere
        {
            get
            {
                return _strCardWhere;
            }
            set
            {
                _strCardWhere = value;
            }
        }

        #endregion

       public SapClass(object obb)
        {
            objBi.ob = (OpeBase)obb;
            this.ob = (OpeBase)obb;
        }
        public SapClass()
        {
           // objBi.ob = (OpeBase)obb;
        }

        #region 全局变量定义

        //使用公用对象
        BaseInfo objBi = new BaseInfo();
        
      //  public sapRfcClass.sapRfcClass sapRfc = new sapRfcClass.sapRfcClass();

        //调用BAPI_GOODSMVT_CREATE后的返回信息
        public string[] strArrayUpload = new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" };

        //调用BAPI_GOODSMVT_CREATE后的返回的错误信息
        public string strSapError;

        //EXCEL应用
        Excel.Application app;
        _Workbook workbook;

        //本地SAP系统部署
    //    private SAPLogonControlClass connctl = new SAPLogonControlClass();
      //  private Connection conn;

        #endregion

        #region 数据信息检查
       
        /// <summary>
        /// 查询数据是否存在，返回布尔
        /// </summary>
        /// <param name="sField">字段</param>
        /// <param name="sTable">数据库表</param>
        /// <param name="sWhere">查询条件</param>
        /// <returns></returns>
        public bool chkBatchDataExist(string sField, string sTable, string sWhere)
        {
            string strTmpOrder;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpOrder = "";

            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpData.ServerParams = new object[] { sTable, sField, sWhere, strTmpOrder }; //
            dtTmpData.Clear();
            ccpData.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            if (dtTmpData.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询数据是否存在重载，返回字符串
        /// </summary>
        /// <param name="sField">字段名</param>
        /// <param name="sTable">数据库表</param>
        /// <param name="sWhere">查询条件</param>
        /// <returns>字符串</returns>
        public string listBatchData(string sField, string sTable, string sWhere)
        {
            string strTmp = "";
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpData.ServerParams = new object[] { sTable, sField, sWhere, " ORDER BY " + sField }; //
            dtTmpData.Clear();
            ccpData.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            
            if (dtTmpData.Rows.Count > 0)
            {
                for (int i = 0; i < dtTmpData.Rows.Count; i++)
                {
                    strTmp += (i == 0 ? dtTmpData.Rows[i][sField] : "," + dtTmpData.Rows[i][sField]);
                }
                return strTmp;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据轧制号检查明细数据是否重复
        /// </summary>
        /// <param name="sZzh">轧制号（批卷号）</param>
        /// <returns></returns>       
        public bool chkRepeatData(string sZzh)
        {
            if (sZzh == "") return false;

            string strTmpTable, strTmpField, strTmpWhere, strTmpOrder;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "DT_PRODUCTWEIGHTDETAIL";
            strTmpField = "(FS_BATCHNO || FN_BANDNO)";
            strTmpWhere = " AND FS_BATCHNO='" + sZzh + "'";
            strTmpOrder = " GROUP BY (FS_BATCHNO || FN_BANDNO) HAVING COUNT (FS_BATCHNO || FN_BANDNO)>1";

            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, strTmpOrder }; //
            dtTmpData.Clear();
            ccpData.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            if (dtTmpData.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查生产订单号
        /// </summary>
        /// <param name="sScdd">生产订单号</param>
        /// <returns></returns>
        public bool chkScdd(string sScdd)
        {
            if (sScdd.Trim().Length == 0)
                return false;

            if (sScdd.Length != 12)
            {
                MessageBox.Show("生产订单号信息必须等于12位，请重新进行输入！");
                return false;
            }

            if (sScdd.Trim().Substring(0, 3) != "000")
            {
                MessageBox.Show("生产订单号信息开头3位必须为0，请重新进行输入！");
                return false;
            }

            return true;
        }

        #endregion

        #region 数据库操作

        /// <summary>
        /// 信息删除
        /// </summary>
        /// <param name="sTable">操作的数据库表</param>
        /// <param name="sWhere">查询条件</param>
        /// <returns>返回布尔值：真-成功；假-失败</returns>
        public bool delData(string sTable, string sWhere)
        {
            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "DeleteData";

            ccp.ServerParams = new object[] { sTable, sWhere };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sTable">数据库表</param>
        /// <param name="sField">查询字段序列</param>
        /// <param name="sWhere">条件</param>
        /// <param name="sOrder">排序分组</param>
        /// <returns>数据表</returns>
        public System.Data.DataTable selData(string sTable, string sField, string sWhere, string sOrder)
        {
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            ccpData.ServerParams = new object[] { sTable, sField, sWhere, sOrder };
            System.Data.DataTable dtTmp = new System.Data.DataTable();
            ccpData.SourceDataTable = dtTmp;
            try
            {
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
                //Constant.RefreshAndAutoSize(uGridData);
            }
            catch (Exception)
            {

            }

            return dtTmp;
        }

        /// <summary>
        /// 存储数据到批数据表
        /// </summary>
        /// <param name="sTable"></param>
        /// <param name="sField"></param>
        /// <param name="sValue"></param>
        public void insData(string sTable, string sField, string sValue)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "insertDataInfo";

            ccp.ServerParams = new object[] { sTable, sField, sValue };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sTable">数据库表</param>
        /// <param name="sField">查询字段序列</param>
        /// <param name="sWhere">条件</param>
        /// <param name="sOrder">排序分组</param>
        /// <returns>字符串</returns>
        public string selDataStr(string sTable, string sField, string sWhere, string sOrder)
        {
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            ccpData.ServerParams = new object[] { sTable, sField, sWhere, sOrder };
            System.Data.DataTable dtTmp = new System.Data.DataTable();
            ccpData.SourceDataTable = dtTmp;
            try
            {
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
                //Constant.RefreshAndAutoSize(uGridData);
            }
            catch (Exception)
            {

            }

            return dtTmp.Rows[0][sField].ToString();
        }

        /// <summary>
        /// 更新数据不返回
        /// </summary>
        /// <param name="sTable"></param>
        /// <param name="sFields"></param>
        /// <param name="sWhere"></param>
        public void uptDataFlag(string sTable, string sFields, string sWhere)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "check_UpdateInfo";

            ccp.ServerParams = new object[] { sTable, sFields, sWhere };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        #endregion

        #region 生产逻辑

        /// <summary>
        /// 根据作业区判断工艺卡卡号与计量数据表是否一致
        /// </summary>
        /// <param name="sTable">数据表</param>
        /// <returns>凭证号</returns>
        public void chkCardNo(string sTable)
        {
            string strTmpTable, strTmpField, strTmpWhere;
            strTmpTable = "IT_FP_TECHCARD M LEFT JOIN " + sTable + " A ON M.FS_ZC_BATCHNO=A.FS_BATCHNO";
            strTmpField = "M.FS_CARDNO AS card1,A.FS_CARDNO AS card2,A.FS_BATCHNO AS FS_BATCHNO";
            strTmpWhere = strCardWhere + " AND M.FS_CARDNO<>A.FS_CARDNO";

            System.Data.DataTable dtTmp;
            dtTmp = selData(strTmpTable, strTmpField, strTmpWhere, "");

            if (dtTmp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTmp.Rows.Count; i++)
                {
                    uptData(sTable, "FS_CARDNO='" + dtTmp.Rows[i]["card1"].ToString() + "'", 
                        "FS_BATCHNO='" + dtTmp.Rows[i]["FS_BATCHNO"].ToString() + "'");
                }
            }
        }

        #endregion

        /// <summary>
        /// 根据订单号从SAP下载订单信息
        /// </summary>
        public System.Data.DataTable downOrderInfo(string sDdh)
        {
            CoreClientParam ccpProductNo = new CoreClientParam();
            ccpProductNo.ServerName = "ygjzjl.base.SapOperation";
            ccpProductNo.MethodName = "queryProductNo";
            ccpProductNo.ServerParams = new object[] { sDdh };

            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpProductNo.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpProductNo, CoreInvokeType.Internal);

          /*  if (dtTmpData.Rows.Count == 0)
            {
                return dtTmpData;
            }*/

            return dtTmpData;
        }

        /// <summary>
        /// 根据凭证号从SAP下载检验批信息
        /// </summary>
        /// <param name="sPzh">检验批</param>
        /// <returns>凭证号</returns>
        public string downSprueflosInfo(string sPzh)
        {
            CoreClientParam ccpSp = new CoreClientParam();
            ccpSp.ServerName = "ygjzjl.sap.DownloadSapRfc";
            ccpSp.MethodName = "down_SPRUEFLOS";
            ccpSp.ServerParams = new object[] { "ZJL_DOWNPRUEFLOS", sPzh };

            this.ExecuteNonQuery(ccpSp, CoreInvokeType.Internal);

            if (ccpSp.ReturnCode == 0)
            {
                return ccpSp.ReturnObject.ToString();
            }
            else
            {
                return "";
            }
        }
        
        /// <summary>
        /// 根据凭证号从SAP下载检验批信息
        /// </summary>
        public string execute261(string sPch)
        {
            string strTmpTable, strTmpField, strTmpOrder, strTmpWhere, strTmpMater;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "DT_BOARDWEIGHTMAIN";
            strTmpField = "FN_WEIGHT,FS_PRODUCTNO,FS_PLANT,FS_SAPSTORE,FS_ACCOUNTDATE";
            strTmpWhere = " AND FS_STOVENO='" + sPch + "'";
            strTmpOrder = "";


            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, strTmpOrder };
            System.Data.DataTable dtTmpData = new System.Data.DataTable();
            ccpData.SourceDataTable = dtTmpData;
            try
            {
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            }
            catch (Exception ex1)
            {
                return ex1.ToString() + "信息查询异常！";
            }

            if (dtTmpData.Rows.Count == 0)
                return "冶炼炉号" + sPch + "在计量系统不存在！";

            string[] strTmpHeader = new string[] { "", "", "" };
            ArrayList listSubItem = new ArrayList();

            strTmpMater = getMaterial(dtTmpData.Rows[0]["FS_PRODUCTNO"].ToString());

            strTmpHeader[0] = Convert.ToDateTime(dtTmpData.Rows[0]["FS_ACCOUNTDATE"].ToString()).ToString("yyyy.MM.dd");
            strTmpHeader[1] = strTmpHeader[0];
            strTmpHeader[2] = "";

            listSubItem.Clear();
            listSubItem.Add(strTmpMater);//物料编号
            listSubItem.Add(dtTmpData.Rows[0]["FS_PLANT"].ToString());//工厂
            listSubItem.Add(dtTmpData.Rows[0]["FS_SAPSTORE"].ToString());//库存地点
            listSubItem.Add(sPch);//批次
            listSubItem.Add("261");//移动类型
            listSubItem.Add(dtTmpData.Rows[0]["FN_WEIGHT"].ToString());//发货数量
            listSubItem.Add("TON");//收货时的计量单位
            listSubItem.Add(dtTmpData.Rows[0]["FS_PRODUCTNO"].ToString());//生产订单编号
            listSubItem.Add("0001");//生产订单行项目号

            string sTmp = BAPI_GOODSMVT_CREATE("03", strTmpHeader, listSubItem);

            if (sTmp != "")
            {
                sTmp = "炉号" + sPch + "-凭证号" + sTmp + "-上传重量" + dtTmpData.Rows[0]["FN_WEIGHT"].ToString();
                sTmp += "，发料成功！";
                return sTmp;
            }
            else
            {
                return "";
            }
        }

        #region 本地SAP系统部署


   /*     private bool sapConn()
        {
            connctl.Client = "800";
            connctl.Language = "ZH";
            connctl.ApplicationServer = "10.1.1.1";
            connctl.SystemNumber = 00;
            connctl.User = "mm3jggyh";
            connctl.Password = "kgerpl3";

            conn = (Connection)connctl.NewConnection();
            if (conn.Logon(null, true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/

        /// <summary>
        /// 批次特性上传
        /// </summary>
        /// <param name="sMatnr">物料号</param>
        /// <param name="sBatch">批次号</param>
        /// <param name="sPlant">工厂</param>
        /// <returns></returns>
     /*   public bool batchValueUp(string sMatnr, string sBatch, string sPlant)
        {
            string strTmpTable, strTmpField, strTmpOrder, strTmpWhere;
            string[,] arrTmp = new string[11, 3];
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "IT_FP_TECHCARD";
            strTmpField = "FS_GP_STOVENO,FN_GP_C,FN_GP_SI,FN_GP_MN,FN_GP_S,FN_GP_P,FN_GP_NI,FN_GP_CR,FN_GP_CU,"
                + "FN_GP_MO,FN_GP_CEQ";
            strTmpWhere = " AND FS_ZC_BATCHNO='" + sBatch + "'";
            strTmpOrder = "";

            System.Data.DataTable dtTmpData = new System.Data.DataTable();
            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, strTmpOrder };
            ccpData.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);

            if (dtTmpData.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                arrTmp[0, 0] = "B_YLLH";
                arrTmp[0, 1] = "0000000882";
                arrTmp[0, 2] = dtTmpData.Rows[0]["FS_GP_STOVENO"].ToString();

                arrTmp[1, 0] = "P9001";
                arrTmp[1, 1] = "0000000903";
                arrTmp[1, 2] = dtTmpData.Rows[0]["FN_GP_C"].ToString();

                arrTmp[2, 0] = "P9002";
                arrTmp[2, 1] = "0000000904";
                arrTmp[2, 2] = dtTmpData.Rows[0]["FN_GP_SI"].ToString();

                arrTmp[3, 0] = "P9003";
                arrTmp[3, 1] = "0000000905";
                arrTmp[3, 2] = dtTmpData.Rows[0]["FN_GP_MN"].ToString();

                arrTmp[4, 0] = "P9004";
                arrTmp[4, 1] = "0000000906";
                arrTmp[4, 2] = dtTmpData.Rows[0]["FN_GP_S"].ToString();

                arrTmp[5, 0] = "P9005";
                arrTmp[5, 1] = "0000000907";
                arrTmp[5, 2] = dtTmpData.Rows[0]["FN_GP_P"].ToString().Substring(0, 5);

                arrTmp[6, 0] = "P9006";
                arrTmp[6, 1] = "0000000908";
                try
                {
                    arrTmp[6, 2] = dtTmpData.Rows[0]["FN_GP_NI"].ToString().Substring(0, 5);
                }
                catch (Exception)
                {
                    arrTmp[6, 2] = "0";
                }
                //if (arrTmp[6, 2] == null) arrTmp[6, 2] = "0";

                arrTmp[7, 0] = "P9007";
                arrTmp[7, 1] = "0000000909";
                try
                {
                    arrTmp[7, 2] = dtTmpData.Rows[0]["FN_GP_CR"].ToString().Substring(0, 5);
                }
                catch (Exception)
                {
                    arrTmp[7, 2] = "0";
                }

                arrTmp[8, 0] = "P9008";
                arrTmp[8, 1] = "0000000910";
                try
                {
                    arrTmp[8, 2] = dtTmpData.Rows[0]["FN_GP_CU"].ToString().Substring(0, 5);
                }
                catch (Exception)
                {
                    arrTmp[8, 2] = "0";
                }

                arrTmp[9, 0] = "P9010";
                arrTmp[9, 1] = "0000000912";
                try
                {
                    arrTmp[9, 2] = dtTmpData.Rows[0]["FN_GP_MO"].ToString();
                }
                catch (Exception)
                {
                    arrTmp[9, 2] = "0";
                }

                arrTmp[10, 0] = "P9009";
                arrTmp[10, 1] = "0000000911";
                try
                {
                    arrTmp[10, 2] = dtTmpData.Rows[0]["FN_GP_CEQ"].ToString();
                }
                catch (Exception)
                {
                    arrTmp[10, 2] = "0";
                }
            }

            if (sapRfc.connSAP())
            {
                string strTmp = sapRfc.runBatchCopy(sMatnr, sBatch, sPlant, arrTmp);
            }
            else
            {
                return false;
            }

            return true;
        }*/

        /// <summary>
        ///  综合判定
        /// </summary>
        /// <param name="sJyp">检验批号</param>
        /// <param name="sDmz"></param>
        /// <param name="sDm"></param>
        /// <param name="sJzrq"></param>
        /// <returns></returns>
   /*     public bool upZhpd(string sJyp, string sDmz, string sDm, string sJzrq)
        {
            if (sapConn())
            {
                SAPFunctionsClass functions = new SAPFunctionsClass();
                functions.Connection = conn;

                //传入Function Name
                Function fucntion = (Function)functions.Add("ZQM_UD");
                //传入值参数
                SAPFunctionsOCX.Parameter parameter1 = (SAPFunctionsOCX.Parameter)fucntion.get_Exports("P_JYP");
                parameter1.Value = sJyp;
                SAPFunctionsOCX.Parameter parameter2 = (SAPFunctionsOCX.Parameter)fucntion.get_Exports("P_DMZ");
                parameter2.Value = sDmz;
                SAPFunctionsOCX.Parameter parameter3 = (SAPFunctionsOCX.Parameter)fucntion.get_Exports("P_DM");
                parameter3.Value = sDm;
                SAPFunctionsOCX.Parameter parameter4 = (SAPFunctionsOCX.Parameter)fucntion.get_Exports("P_JZRQ");
                //sJzrq = "20100913";
                parameter4.Value = sJzrq;

                SAPFunctionsOCX.Parameter parmOut = (SAPFunctionsOCX.Parameter)fucntion.get_Imports("RETURN_VALUE");

                if (fucntion.Call())
                {
                    if (Convert.ToInt32(parmOut.Value) == 0)
                    {
                        strSapError = "";
                        return true;
                    }                  

                    if (Convert.ToInt32(parmOut.Value) <= 1000)
                    {
                        strSapError = "Error in dialog program!";
                    }
                    if (Convert.ToInt32(parmOut.Value) > 1000)
                    {
                        strSapError = "Batch input error!";
                    }
                    return false;
                }
                else
                {
                    strSapError = "SAP调用RFC失败！";
                    return false;
                }
            }
            else
            {
                strSapError = "SAP连接失败！";
                return false;
            }
        }*/

        #endregion 

        #region EXCEL模板导入

        /// <summary>
        /// 检查用户模板文件选择是否正确
        /// </summary>
        /// <param name="iOpt">业务代码</param>
        /// <param name="sFileName">模板文件名</param>
        /// <returns>字符串，返回错误信息</returns>
        private string chkExcute(int iOpt, string sFileName)
        {
            string strTmp="";
            string[] strTmpArray = sFileName.Split('\\');

            sFileName = strTmpArray[strTmpArray.Length - 1];

            switch (iOpt)
            {
                case 1:
                    if (sFileName != "生产收货.xls")
                    {
                        strTmp = "模板选择错误，请选择：<生产收货.xls> 文件！";
                    }
                    break;
                case 2:
                    if (sFileName != "生产发料.xls")
                    {
                        strTmp = "模板选择错误，请选择：<生产发料.xls> 文件！";
                    }
                    break;

            }

            return strTmp;
        }


        /// <summary>
        /// 执行EXCEL导入
        /// </summary>
        /// <param name="iOpt">1-101,2-261</param>
        /// <param name="sCzdw">导入单位名称</param>
        /// <returns>返回错误列表</returns>
        public ArrayList ExcelToDatabase(int iOpt, string sCzdw)
        {
            ArrayList lstTmp = new ArrayList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = @"E:\计量系统\安装3.0\";  //指定打开文件默认路径
            openFileDialog.Filter = "Excel文件|*.xls";     //指定打开默认选择文件类型名
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                app = new Excel.Application();
                app.Visible = false;
                app.UserControl = true;
                Workbooks workbooks = app.Workbooks;
                object MissingValue = Type.Missing;
                workbook = workbooks.Add(openFileDialog.FileName);
            }

            string strTmpFile = openFileDialog.FileName.ToLower();

            if (strTmpFile == "")
            {
                lstTmp.Add("请选择导入的Excel文件！");
                return lstTmp;
            }

           string strTmp = chkExcute(iOpt, strTmpFile);
            if (strTmp != "")
            {
                lstTmp.Add(strTmp);
                return lstTmp;
            }

            lstTmp = ReadAndSaveExcelInfo(iOpt, sCzdw);
            return lstTmp;
        }


        /// <summary>
        /// 读取并保存Excel中的信息到数据库中
        /// </summary>
        /// <param name="iOpt">1-101,2-261</param>
        /// <param name="sCzdw">导入单位名称</param>
        /// <returns>返回错误列表</returns>
        private ArrayList ReadAndSaveExcelInfo(int iOpt, string sCzdw)
        {
            ArrayList lstTmp = new ArrayList();

            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;

            string strTmpTable, strTmpField, strTmpValue;
            string[] arrayTmp;
            arrayTmp = new string[16];
            strTmpTable = "";
            strTmpField = "";
            strTmpValue = "";

            int num = 0;

            Excel.Range range = worksheet.get_Range("A2", "P65535");//A2、O65535表示到Excel从第A列的第2行到第O列的第65535-1行  A2、P65535表示到Excel从第A列的第2行到第P列的第65535-1行 
            System.Array values = (System.Array)range.Formula;
            num = values.GetLength(0);

            for (int i = 1; i <= num; i++)
            {
                if (values.GetValue(i, 1).ToString().Trim() == "" || values.GetValue(i, 2).ToString().Trim() == "")
                {
                    break;
                }
                else
                {
                    string strLRR = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    string strLRD = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
                    switch(iOpt)
                    {
                        case 1:
                            arrayTmp[0] = values.GetValue(i, 1).ToString().Trim();  //记帐日期
                            arrayTmp[1] = values.GetValue(i, 2).ToString().Trim();  //订单号
                            arrayTmp[2] = values.GetValue(i, 3).ToString().Trim();  //订单行项目号
                            arrayTmp[3] = values.GetValue(i, 4).ToString().Trim();  //物料编码
                            //arrayTmp[4] = values.GetValue(i, 5).ToString().Trim();  //物料名称
                            arrayTmp[5] = values.GetValue(i, 5).ToString().Trim();  //批号
                            arrayTmp[6] = values.GetValue(i, 6).ToString().Trim();  //收货数量
                            arrayTmp[7] = values.GetValue(i, 7).ToString().Trim();  //计量单位
                            arrayTmp[8] = values.GetValue(i, 8).ToString().Trim(); //工厂
                            arrayTmp[9] = values.GetValue(i, 9).ToString().Trim(); //库存地点
                            arrayTmp[10] = values.GetValue(i, 10).ToString().Trim(); //抬头文本

                            strTmpTable = "DT_SAP261";
                            strTmpField = "FS_WEIGHTNO,FS_ACCOUNTDATE,FS_PRODUCTNO,FS_ITEMNO,FS_MATERIAL,FS_STOVENO,"
                                + "FN_NETWEIGHT,FS_PLANT,FS_SAPSTORE,FS_HEADER,FS_WEIGHTTYPE,FS_DRDW,FS_AUDITOR";
                            strTmpValue = "'" + Guid.NewGuid().ToString() + "','" + arrayTmp[0] + "','" + arrayTmp[1] + "','"
                                + arrayTmp[2] + "','" + arrayTmp[3] + "','" + arrayTmp[5] + "',"
                                + arrayTmp[6] + ",'" + arrayTmp[8] + "','" + arrayTmp[9] + "','" + arrayTmp[10] + "',"
                                + "'261','" + sCzdw + "','" + strLRR + "'";
                            break;

                        case 2:
                            arrayTmp[0] = values.GetValue(i, 1).ToString().Trim();  //记帐日期
                            arrayTmp[1] = values.GetValue(i, 2).ToString().Trim();  //订单号
                            arrayTmp[2] = values.GetValue(i, 3).ToString().Trim();  //订单行项目号
                            arrayTmp[3] = values.GetValue(i, 4).ToString().Trim();  //物料编码
                            //arrayTmp[4] = values.GetValue(i, 5).ToString().Trim();  //物料名称
                            arrayTmp[5] = values.GetValue(i, 5).ToString().Trim();  //批号
                            arrayTmp[6] = values.GetValue(i, 6).ToString().Trim();  //收货数量
                            arrayTmp[7] = values.GetValue(i, 7).ToString().Trim();  //计量单位
                            arrayTmp[8] = values.GetValue(i, 8).ToString().Trim(); //工厂
                            arrayTmp[9] = values.GetValue(i, 9).ToString().Trim(); //库存地点
                            arrayTmp[10] = values.GetValue(i, 10).ToString().Trim(); //抬头文本                   

                            strTmpTable = "DT_SAP261";
                            strTmpField = "FS_WEIGHTNO,FS_ACCOUNTDATE,FS_PRODUCTNO,FS_ITEMNO,FS_MATERIAL,FS_STOVENO,"
                                + "FN_NETWEIGHT,FS_PLANT,FS_SAPSTORE,FS_HEADER,FS_WEIGHTTYPE,FS_DRDW,FS_AUDITOR";
                            strTmpValue = "'" + Guid.NewGuid().ToString() + "','" + arrayTmp[0] + "','" + arrayTmp[1] + "','" 
                                + arrayTmp[2] + "','" + arrayTmp[3] + "','" + arrayTmp[5] + "'," 
                                + arrayTmp[6] + ",'" + arrayTmp[8] + "','" + arrayTmp[9] + "','" + arrayTmp[10] + "'," 
                                + "'261','" + sCzdw + "','" + strLRR + "'";
                            break;
                   }

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.base.QueryData";
                    ccp.MethodName = "insertDataInfo";
                    ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };
                    try
                    {
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    }
                    catch (Exception ex1)
                    {
                        lstTmp.Add(ex1.Message + "第" + i.ToString() + "行信息导入失败！");
                    }
                }
            }

            app.Quit();
            app = null;

            System.Diagnostics.Process[] myProcesses;
            myProcesses = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process myProcess in myProcesses)
            {
                if (myProcess.ProcessName == "EXCEL")
                    myProcess.Kill();
            }
            if (lstTmp.Count == 0)
                lstTmp.Add("Excel导入成功！");
            return lstTmp;
        }

        #endregion

        /// <summary>
        /// 获取订单号信息
        /// </summary>
        public string[] getOrdInfo(string sDdh)
        {
            string[] strTmpArray = { "", "", "", "", "", "" };
            if (sDdh == "") return strTmpArray;

            string strTmpTable, strTmpField, strTmpWhere;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "IT_PRODUCTDETAIL";
            strTmpField = "FS_ITEMNO,FS_MATERIAL,FS_FACTORY,FS_SPECIALFLAG,FS_SALESORDERNO,FS_SALESORDERINDEX";
            strTmpWhere = " AND FS_PRODUCTNO='" + sDdh + "'";

            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, "" }; //
            dtTmpData.Clear();
            ccpData.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            if (dtTmpData.Rows.Count > 0)
            {
                strTmpArray[0] = dtTmpData.Rows[0]["FS_ITEMNO"].ToString();
                strTmpArray[1] = dtTmpData.Rows[0]["FS_MATERIAL"].ToString();
                strTmpArray[2] = dtTmpData.Rows[0]["FS_FACTORY"].ToString();
                strTmpArray[3] = dtTmpData.Rows[0]["FS_SPECIALFLAG"].ToString();
                strTmpArray[4] = dtTmpData.Rows[0]["FS_SALESORDERNO"].ToString();
                strTmpArray[5] = dtTmpData.Rows[0]["FS_SALESORDERINDEX"].ToString();
            }

            return strTmpArray;
        }

        /// <summary>
        /// 根据订单号获取物料信息
        /// </summary>
        public string getMaterial(string sDdh)
        {
            if (sDdh == "") return "";

            string strTmpTable, strTmpField, strTmpWhere;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "IT_PRODUCTDETAIL";
            strTmpField = "FS_MATERIAL";
            strTmpWhere = " AND FS_PRODUCTNO='" + sDdh + "'";

            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, "" }; //
            dtTmpData.Clear();
            ccpData.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            if (dtTmpData.Rows.Count > 0)
            {
                return dtTmpData.Rows[0]["FS_MATERIAL"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据物料号获取物料名称信息
        /// </summary>
        public string getMaterialName(string sMaterial)
        {
            if (sMaterial == "") return "";

            string strTmpTable, strTmpField, strTmpWhere;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "IT_MATERIAL";
            strTmpField = "FS_MATERIALNAME";
            strTmpWhere = " AND FS_SAPCODE='" + sMaterial + "'";

            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, "" }; //
            dtTmpData.Clear();
            ccpData.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            if (dtTmpData.Rows.Count > 0)
            {
                return dtTmpData.Rows[0]["FS_MATERIALNAME"].ToString();
            }
            else
            {
                return "";
            }
        }

   //     public int insMssqlData(int iServer, string sTable, string sField, string sValue)
     //   {
       //     string strTmpIns = "insert into " + sTable + " (" + sField + ") values (" + sValue + ")";
         //   return MssqlClass.ExecuteNonQuery(MssqlClass.MssqlConString, CommandType.Text, strTmpIns);
       // }

        /// <summary>
        /// 存储数据到261数据表
        /// </summary>
        public void ins261Data(string sMater, string sZl, string sBatch, string sFhdw)
        {
            string strTmpTable, strTmpField, strTmpValue;
            string strTmpDate, strTmpTime;

            strTmpDate = DateTime.Now.ToString("yyyy-MM-dd");
            strTmpTime = DateTime.Now.ToString("HH:mm:ss");

            strTmpTable = "DT_SAP261";
            strTmpField = "FS_WEIGHTNO,FS_MATERIAL,FS_PLANT,FS_WEIGHTTYPE,FN_NETWEIGHT,FS_STOVENO,FS_FHDW";
            strTmpValue = "'" + Guid.NewGuid().ToString() + "','" + sMater + "','1100','261'," + sZl
                + ",'" + sBatch + "','" + sFhdw + "'";

            CoreClientParam ccp261 = new CoreClientParam();
            ccp261.ServerName = "ygjzjl.base.QueryData";
            ccp261.MethodName = "insertDataInfo";

            ccp261.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };

            this.ExecuteNonQuery(ccp261, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 存储数据到批次特性数据拷贝表
        /// </summary>
        public void insChemCopy(string sMater, string sPlant, string sBatch, string sPreBatch)
        {
            string strTmpTable, strTmpField, strTmpValue;

            strTmpTable = "DT_CHEMCOPYDATA";
            strTmpField = "FS_GUID,FS_MATERIAL,FS_PLANT,FS_BATCH,FS_PREBATCH";
            strTmpValue = "'" + Guid.NewGuid().ToString() + "','" + sMater + "','" + sPlant + "','" + sBatch + "','" 
                + sPreBatch + "'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "insertDataInfo";

            ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        //更新SAP日志
        public void uptSapLog(string sTmpNo, string[] sArray)
        {
            string strTmpTable, strTmpField, strTmpValue;

            strTmpTable = "IT_SAPLOG";
            strTmpField = "FS_MATERIALNO,FS_MOVETYPE,FS_DESCRIBE,FS_ORDERNO,FS_BATCHNO,FD_UPLOADTIME,"
                + "FN_UPLOADNUM,FS_USER,FS_USERNO,FS_ITEMNO,FD_STARTTIME,FD_ENDTIME";

            strTmpValue = "";
            for (int i = 0; i < sArray.Length - 1; i++)
            {
                if (i == 0)
                    strTmpValue = "'" + sArray[i] + "'";
                else
                    strTmpValue += ",'" + sArray[i] + "'";
            }

            strTmpValue += ",'" + Convert.ToDateTime(objBi.GetServerTime()).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            CoreClientParam ccpLog = new CoreClientParam();
            ccpLog.ServerName = "ygjzjl.base.QueryData";
            ccpLog.MethodName = "insertDataInfo";

            ccpLog.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };

            this.ExecuteNonQuery(ccpLog, CoreInvokeType.Internal);
        }

        //显示SAP日志
        public string showSapLog(string sLh)
        {
            string strTmpTable, strTmpField, strTmpWhere;
            CoreClientParam ccpOrderInfo = new CoreClientParam();
            ccpOrderInfo.ServerName = "ygjzjl.base.QueryData";
            ccpOrderInfo.MethodName = "queryData";

            strTmpTable = "IT_SAPLOG";
            strTmpField = "FS_DESCRIBE,FS_ORDERNO,FD_UPLOADTIME,FN_UPLOADNUM,FS_USER";
            strTmpWhere = " AND FS_BATCHNO = '" + sLh + "'";
            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpOrderInfo.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, "" };
            ccpOrderInfo.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpOrderInfo, CoreInvokeType.Internal);
            string strTmp = "";
            if (dtTmpData.Rows.Count > 0)
            {
                strTmp = dtTmpData.Rows[0]["FS_USER"].ToString() + "在" + dtTmpData.Rows[0]["FD_UPLOADTIME"].ToString();
                strTmp += "上传" + dtTmpData.Rows[0]["FS_DESCRIBE"].ToString() + "，";
                strTmp += "重量为：" + dtTmpData.Rows[0]["FN_UPLOADNUM"].ToString() + "。";
            }

            return strTmp;
        }

        //调用SAP的BAPI_GOODSMVT_CREATE
        public string BAPI_GOODSMVT_CREATE(string sCode, string[] sArrayHeader, ArrayList listSubItem)
        {
            ArrayList listItem = new ArrayList();
            CoreClientParam ccp = new CoreClientParam();

            strSapError = "";
            Array.Clear(strArrayUpload, 0, strArrayUpload.Length);
            listItem.Clear();
            listItem.Add(listSubItem);

            strArrayUpload[0] = listSubItem[0].ToString();

            if (sCode == "02")
            {
                strArrayUpload[3] = listSubItem[10].ToString();
                strArrayUpload[1] = "101";
                strArrayUpload[9] = listSubItem[9].ToString();
            }
            if (sCode == "03")
            {
                strArrayUpload[1] = "261";
                strArrayUpload[3] = listSubItem[7].ToString();
                strArrayUpload[9] = "0001";
            }
            strArrayUpload[4] = listSubItem[3].ToString();
            strArrayUpload[5] = Convert.ToDateTime(objBi.GetServerTime()).ToString("yyyy-MM-dd HH:mm:ss");
            strArrayUpload[7] = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            strArrayUpload[8] = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID();
            
            strArrayUpload[10] = strArrayUpload[5];

            ccp.ServerName = "ygjzjl.sap.UploadSapRfc";
            
            if (sCode == "02")
                ccp.MethodName = "up_Product";
            if (sCode == "03")
                ccp.MethodName = "fl_Product";
            if (sCode == "04")
                ccp.MethodName = "up_BatchSplit";

            ccp.ServerParams = new object[] { "BAPI_GOODSMVT_CREATE", sArrayHeader, sCode, listItem };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode == 0)
            {
                return ccp.ReturnObject.ToString();
            }
            else
            {
                return "";
            }
        }
       

        /// <summary>
        /// 上传批次特性值
        /// </summary>
        public bool upBatchValue(string sMatnr, string sBatch, string sWerk, ArrayList listSubItem)
        {
            ArrayList listItem = new ArrayList();
            CoreClientParam ccp = new CoreClientParam();
            strSapError = "";
            listItem.Clear();
            listItem.Add(listSubItem);

            ccp.ServerName = "ygjzjl.sap.UploadSapRfc";
            ccp.MethodName = "upBatchValue";

            ccp.ServerParams = new object[] { sMatnr, sBatch, sWerk, listItem };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode != 0)
            {
                strSapError = ccp.ReturnInfo;
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 信息编辑
        /// </summary>
        public bool uptData(string sTable, string sField, string sWhere)
        {
            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "check_UpdateInfo";

            ccp.ServerParams = new object[] { sTable, sField, sWhere };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode != 0)
            {
                strSapError = ccp.ReturnInfo;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SapClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(736, 479);
            this.coreBind.SetDatabasecommand(this, null);
            this.Name = "SapClass";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.SapClass_Load);
            this.ResumeLayout(false);

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SapClass_Load(object sender, EventArgs e)
        {

        }
    }
}