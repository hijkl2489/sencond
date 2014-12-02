using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;

namespace YGJZJL.Car
{
    public partial class HandRecordSecondWeight : FrmBase
    {
        GetBaseInfo m_BaseInfo = new GetBaseInfo();
        string m_SelectPointID = "";
        string m_WeightNo = "";

        public HandRecordSecondWeight()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        Query();                        
                        break;
                    }
                case "Update":
                    {
                        UpdateWeight();
                        Query();
                        break;
                    }
                case "Add":
                        {
                        Add();
                            Query();
                            break;
                        }

                case "Delete":
                        {
                        Delete();
                            Query();
                            break;
                        }
                case "Save":
                        {
                            Save();
                            Query();
                            break;
                        }
                default :
                    break;
            }
        }

        //下载流向信息  ,add by luobin 
        private void DownLoadFlow()
        {
             DataTable m_FlowTable = new DataTable();//流向数据表
             DataColumn dc;
             dc = new DataColumn("FS_TYPECODE".ToUpper()); m_FlowTable.Columns.Add(dc);
             dc = new DataColumn("FS_TYPENAME".ToUpper()); m_FlowTable.Columns.Add(dc);

            string sql = "select FS_TYPECODE, FS_TYPENAME From BT_WEIGHTTYPE order by FS_TYPECODE ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = m_FlowTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            DataRow dr = m_FlowTable.NewRow();
            dr["FS_TYPECODE"] = "";
            dr["FS_TYPENAME"] = "";
            m_FlowTable.Rows.InsertAt(dr, 0);

            cbFlow.DataSource = m_FlowTable;
            cbFlow.DisplayMember = "FS_TYPENAME";
            cbFlow.ValueMember = "FS_TYPECODE";

        }

        private string GetShift(System.DateTime dt)
        {
            string strShift = "";
            string strDateTime = dt.ToString("yyyy-MM-dd HH:mm:ss");

            if (string.Compare(strDateTime.Substring(11, 8), "00:00:00") >= 0 && string.Compare(strDateTime.Substring(11, 8), "08:00:00") < 0)
                strShift = "夜";
            if (string.Compare(strDateTime.Substring(11, 8), "080:00:00") >= 0 && string.Compare(strDateTime.Substring(11, 8), "16:00:00") < 0)
                strShift = "早";
            if (string.Compare(strDateTime.Substring(11, 8), "16:00:00") >= 0 && string.Compare(strDateTime.Substring(11, 8), "24:00:00") < 0)
                strShift = "中";

            return strShift;
        }

        //二次数据保存
        private void Save()
        {
            if (IsValid() == false)
                return;

            if (m_WeightNo == "")
            {
                MessageBox.Show("请选择需要修改的数据");
                return;
            }

            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strSenderPlace = this.tbSenderPlace.Text.Trim();
            string strReceiverPlace = this.tbReceiverPlace.Text.Trim();

            //string strMaterialName = this.cbMaterial.Text.Trim();
            string strMaterial = "";
            if (this.cbMaterial.SelectedValue == null || this.cbMaterial.SelectedValue.ToString().Trim() == "")
                strMaterial = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbMaterial.Text.Trim(), "SaveWLData");
            else
                strMaterial = this.cbMaterial.SelectedValue.ToString().Trim();

            string strSender = "";
            if (this.cbSender.SelectedValue == null || this.cbSender.SelectedValue.ToString().Trim() == "")
                strSender = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbSender.Text.Trim(), "SaveGYDWData");
            else
                strSender = this.cbSender.SelectedValue.ToString().Trim();

            string strReceiver = "";
            if (this.cbReceiver.SelectedValue == null || this.cbReceiver.SelectedValue.ToString().Trim() == "")
                strReceiver = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbReceiver.Text.Trim(), "SaveSHDWData");
            else
                strReceiver = this.cbReceiver.SelectedValue.ToString().Trim();

            string strTrans = "";
            if (this.cbTrans.SelectedValue == null || this.cbTrans.SelectedValue.ToString().Trim() == "")
                strTrans = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbTrans.Text.Trim(), "SaveCYDWData");
            else
                strTrans = this.cbTrans.SelectedValue.ToString().Trim();
            
            string strFlow = this.cbFlow.SelectedValue.ToString().Trim();
            string strMaterialName = this.cbMaterial.Text.Trim();
            string strMemo = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + " 手工补录";

            double inGrossWeight = 0;
            double inTareWeight = 0;
            double inNetWeight = 0;

            string strGrossPoint = "";
            string strGrossWeighter = "";
            string strGrossTime = "";
            string strGrossShift = "";

            string strTarePoint = "";
            string strTareWeighter = "";
            string strTareTime = "";
            string strTareShift = "";
            
            bool CurIsTare = false; //当次是否是皮重
            if (Convert.ToSingle(tbWeight.Text.Trim()) <= Convert.ToSingle(ultraGrid3.ActiveRow.Cells["FN_WEIGHT"].Text.Trim()))
                CurIsTare = true;

            if (CurIsTare)//皮重
            {
                inGrossWeight = Math.Round(Convert.ToSingle(ultraGrid3.ActiveRow.Cells["FN_WEIGHT"].Text.Trim()), 3);
                inTareWeight = Math.Round(Convert.ToSingle(tbWeight.Text.Trim()), 3);
                inNetWeight = Math.Round((inGrossWeight - inTareWeight), 3);

                strGrossPoint = ultraGrid3.ActiveRow.Cells["FS_POUNDTYPE"].Text.Trim();
                strGrossWeighter = ultraGrid3.ActiveRow.Cells["FS_WEIGHTER"].Text.Trim();
                strGrossTime = ultraGrid3.ActiveRow.Cells["FD_WEIGHTTIME"].Text.Trim();
                strGrossShift = ultraGrid3.ActiveRow.Cells["FS_SHIFT"].Text.Trim();

                strTarePoint = this.cbBF.SelectedValue.ToString().Trim();
                strTareWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                strTareTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                strTareShift = this.GetShift(System.DateTime.Now);
            }
            else//毛重
            {
                inTareWeight = Math.Round(Convert.ToSingle(ultraGrid3.ActiveRow.Cells["FN_WEIGHT"].Text.Trim()), 3);
                inGrossWeight = Math.Round(Convert.ToSingle(tbWeight.Text.Trim()), 3);
                inNetWeight = Math.Round((inGrossWeight - inTareWeight), 3);

                strTarePoint = ultraGrid3.ActiveRow.Cells["FS_POUNDTYPE"].Text.Trim();
                strTareWeighter = ultraGrid3.ActiveRow.Cells["FS_WEIGHTER"].Text.Trim();
                strTareTime = ultraGrid3.ActiveRow.Cells["FD_WEIGHTTIME"].Text.Trim();
                strTareShift = ultraGrid3.ActiveRow.Cells["FS_SHIFT"].Text.Trim();

                strGrossPoint = this.cbBF.SelectedValue.ToString().Trim();
                strGrossWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                strGrossTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                strGrossShift = this.GetShift(System.DateTime.Now);
            }
            string strYKL = ultraGrid3.ActiveRow.Cells["FS_YKL"].Text.Trim();
            string strYKBL = ultraGrid3.ActiveRow.Cells["FS_YKBL"].Text.Trim();
            string strKHZL = "";
            //扣渣
            if (strYKL != "" && strYKL != "0")
            {
                decimal YKL = Convert.ToDecimal(strYKL);
                strKHZL = (Convert.ToDecimal(inNetWeight) - YKL).ToString();
            }
            else if (strYKBL != "")
            {
                decimal KHJZ = Math.Round(Convert.ToDecimal(inNetWeight) - Convert.ToDecimal(inNetWeight) * Convert.ToDecimal(strYKBL), 3);
                strKHZL = KHJZ.ToString();
            }
            else
            {
                strKHZL = inNetWeight.ToString();
            }
            string strBillNumber = ultraGrid3.ActiveRow.Cells["FS_BILLNUMBER"].Text.Trim();
            string strDriverName = ultraGrid3.ActiveRow.Cells["FS_DRIVERNAME"].Text.Trim();
            string strDriverCard = ultraGrid3.ActiveRow.Cells["FS_DRIVERIDCARD"].Text.Trim();
            string strYCSFYC = ultraGrid3.ActiveRow.Cells["FS_YCSFYC"].Text.Trim();
            string strFirstLabelID = ultraGrid3.ActiveRow.Cells["FS_FIRSTLABELID"].Text.Trim();
            string strContractNo = ultraGrid3.ActiveRow.Cells["FS_CONTRACTNO"].Text.Trim();
            string strContractItem = ultraGrid3.ActiveRow.Cells["FS_CONTRACTITEM"].Text.Trim();
            string strStoveNo = ultraGrid3.ActiveRow.Cells["FS_STOVENO"].Text.Trim();
            string strCount = ultraGrid3.ActiveRow.Cells["FN_COUNT"].Text.Trim();
            string strWeightNo = ultraGrid3.ActiveRow.Cells["FS_WEIGHTNO"].Text.Trim();

            string strBarCode = System.DateTime.Now.ToString().Trim().Substring(0, 4);
            strBarCode += strTareTime.Substring(5, 2);
            strBarCode += strTareTime.Substring(8, 2);
            strBarCode += strTareTime.Substring(11, 2);
            strBarCode += strTareTime.Substring(14, 2);
            strBarCode += strTareTime.Substring(17, 2);
            strBarCode += m_SelectPointID;


            string strSamplePerson = ultraGrid3.ActiveRow.Cells["FS_SAMPLEPERSON"].Text.Trim();
            string strSampleTime = ultraGrid3.ActiveRow.Cells["FD_SAMPLETIME"].Text.Trim();
            string strSamplePlace = ultraGrid3.ActiveRow.Cells["FS_SAMPLEPLACE"].Text.Trim();
            string strSampleFlag = ultraGrid3.ActiveRow.Cells["FS_SAMPLEFLAG"].Text.Trim();

            string strUnloadPerson = ultraGrid3.ActiveRow.Cells["FS_UNLOADPERSON"].Text.Trim();
            string strUnloadTime = ultraGrid3.ActiveRow.Cells["FD_UNLOADTIME"].Text.Trim();
            string strUnloadPlace = ultraGrid3.ActiveRow.Cells["FS_UNLOADPLACE"].Text.Trim();
            string strUnloadFlag = ultraGrid3.ActiveRow.Cells["FS_UNLOADFLAG"].Text.Trim();

            string strCheckPerson = ultraGrid3.ActiveRow.Cells["FS_CHECKPERSON"].Text.Trim();
            string strCheckTime = ultraGrid3.ActiveRow.Cells["FD_CHECKTIME"].Text.Trim();
            string strCheckPlace = ultraGrid3.ActiveRow.Cells["FS_CHECKPLACE"].Text.Trim();
            string strCheckFlag = ultraGrid3.ActiveRow.Cells["FS_CHECKFLAG"].Text.Trim();

            string strProvider = "";
            if (this.cbProvider.SelectedValue != null)
                strProvider = this.cbProvider.SelectedValue.ToString();
            else
            {
                if (this.cbProvider.Text.Trim() != "")
                    strProvider = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbProvider.Text.Trim(), "SaveSPDWData");
            }

            string strBZ = this.tbBZ.Text.Trim();

            string sql = "insert into dt_carweight_weight (FS_WEIGHTNO,FS_CONTRACTNO,FS_CONTRACTITEM,";//3
                   sql += "FS_STOVENO,FN_COUNT,FS_CARDNUMBER,FS_CARNO,FS_MATERIAL,FS_MATERIALNAME,FS_SENDER,";//7
                   sql += "FS_TRANSNO,FS_RECEIVER,FS_WEIGHTTYPE,FN_GROSSWEIGHT,FS_GROSSPOINT,FS_GROSSPERSON,";//6
                   sql += "FD_GROSSDATETIME,FS_GROSSSHIFT,FN_TAREWEIGHT,FS_TAREPOINT,FS_TAREPERSON,FD_TAREDATETIME,";//6
                   sql += "FS_TARESHIFT,FS_FIRSTLABELID,FS_FULLLABELID,FS_UNLOADFLAG,";//4
                   sql += "FN_NETWEIGHT,";//1
                   sql += "FS_SAMPLEPERSON,FS_YCSFYC,FS_YKL,FD_SAMPLETIME,FS_SAMPLEPLACE,FS_SAMPLEFLAG,";//6
                   sql += "FS_UNLOADPERSON,FD_UNLOADTIME,FS_UNLOADPLACE,FS_CHECKPERSON,FD_CHECKTIME,FS_CHECKPLACE,FS_CHECKFLAG,";//7
                   sql += "FS_DRIVERNAME,FS_DRIVERIDCARD,FS_SENDERSTORE,FS_BILLNUMBER,";//4

                   sql += "FD_ECJLSJ,FS_YKBL,FS_KHJZ,FS_RECEIVERSTORE,FS_JZDATE,FS_MEMO,FS_Provider,FS_BZ)";//5
                   sql += " values ('" + strWeightNo + "','" + strContractNo + "','" + strContractItem + "',";//3
                   sql += "'" + strStoveNo + "','" + strCount + "','" + strCardNo + "','" + strCarNo + "','" + strMaterial + "','" + strMaterialName + "','" + strSender + "',";//7
                   sql += "'" + strTrans + "','" + strReceiver + "','" + strFlow + "','" + inGrossWeight + "','" + strGrossPoint + "','" + strGrossWeighter + "',";//6
                   sql += "TO_DATE('" + strGrossTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strGrossShift + "','" + inTareWeight + "','" + strTarePoint + "','" + strTareWeighter + "',TO_DATE('" + strGrossTime + "','yyyy-MM-dd HH24:mi:ss'),";//6
                   sql += "'" + strTareShift + "','" + strFirstLabelID + "','" + strBarCode + "','"+strUnloadFlag+"',";//4
                   sql += "'" + inNetWeight + "',";//1
                   sql += "'" + strSamplePerson + "','" + strYCSFYC + "','" + strYKL + "',TO_DATE('" + strSampleTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strSamplePlace + "','" + strSampleFlag + "',";//6
                   sql += "'" + strUnloadPerson + "',TO_DATE('" + strUnloadTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strUnloadPlace + "','" + strCheckPerson + "',TO_DATE('" + strCheckTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strCheckPlace + "','" + strCheckFlag + "',";//7
                   sql += "'" + strDriverName + "','" + strDriverCard + "','" + strSenderPlace + "','" + strBillNumber + "',";//4
                   sql += "TO_DATE('" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-MM-dd HH24:mi:ss'),'" + strYKBL + "','" + strKHZL + "','" + strReceiverPlace + "',TO_DATE('" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-MM-dd HH24:mi:ss'),'" + strMemo + "','" + strProvider + "','" + strBZ + "'";//5
            sql += ")";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                sql = "delete DT_FIRSTCARWEIGHT where FS_WEIGHTNO = '" + strWeightNo + "'";
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { sql };
                ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
            this.tbWeight.Text = "";//为防止错点“保存”按钮，在保存后将二次重量置空

        }

        //查询
        private void Query()
        {
            string strBeginTime =  BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss')";
            string strEndTIme = EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
            string strCarNo = this.txtCarNo.Text.Trim();
            string sql = "select A.FS_WEIGHTNO,A.FS_CARDNUMBER,A.FS_CARNO,B.FS_MATERIALNAME,C.FS_SUPPLIERNAME,d.fs_memo as FS_Receiver,";
                   sql += "A.Fs_Senderstore,A.FS_RECEIVERSTORE,E.FS_TRANSNAME,F.FS_TYPENAME,G.FS_POINTNAME,A.FN_WEIGHT,";
                   sql += " A.Fs_Weighter,to_char(A.Fd_Weighttime, 'yyyy-MM-dd HH24:mi:ss') as FD_Weighttime,A.Fs_Weighter,";
                   sql += "to_char(A.Fd_Weighttime, 'yyyy-MM-dd HH24:mi:ss') as FD_Weighttime,A.fs_Memo,A.Fs_Poundtype, ";
                   sql += " A.FS_SAMPLEPERSON,A.FS_SAMPLEPLACE,to_char(A.FD_SAMPLETIME, 'yyyy-MM-dd HH24:mi:ss') as FD_SAMPLETIME,A.FS_SAMPLEFLAG, ";
                   sql += " A.FS_UNLOADPERSON,A.FS_UNLOADPLACE,to_char(A.FD_UNLOADTIME, 'yyyy-MM-dd HH24:mi:ss') as FD_UNLOADTIME,A.FS_UNLOADFLAG, ";
                   sql += " A.FS_REWEIGHTFLAG,to_char(A.FD_REWEIGHTTIME, 'yyyy-MM-dd HH24:mi:ss') as FD_REWEIGHTTIME,A.FS_REWEIGHTPLACE,A.FS_REWEIGHTPERSON,";
                   sql += " A.FS_CHECKPERSON,A.FS_CHECKPLACE,to_char(A.FD_CHECKTIME, 'yyyy-MM-dd HH24:mi:ss') as FD_CHECKTIME,A.FS_CHECKFLAG, ";
                   sql += " A.FS_YKL,A.FS_YKBL,A.FS_BILLNUMBER,A.FS_DRIVERNAME,A.FS_DRIVERIDCARD,A.FS_YCSFYC,A.FS_FIRSTLABELID,A.FS_Shift, ";
                   sql += " A.FS_CONTRACTNO,A.FS_CONTRACTITEM,A.FS_STOVENO,A.FN_COUNT,A.FS_BZ,H.FS_ProviderName ";
                   sql += " from dt_firstcarweight A,It_Material B,It_Supplier C,It_Store D,BT_TRANS E,Bt_Weighttype F,Bt_Point G,It_Provider H";
                   sql += " where A.Fs_Material = b.fs_wl(+) and A.FS_SENDER = C.FS_GY(+) and A.Fs_Receiver = d.fs_sh(+)";
                   sql += " and A.Fs_Transno = E.FS_CY(+) and A.Fs_Weighttype = F.FS_TYPECODE(+) and A.Fs_Poundtype = G.FS_POINTCODE(+) and A.FS_Provider = H.FS_SP(+) and A.FS_FALG<>'1'";
                   sql += " and A.FD_Weighttime >= to_date('" + BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
                   sql += " and FD_WEIGHTTIME <= to_date('" + EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
                   if (this.txtCarNo.Text.Trim() != "")
                   {
                       sql += " and A.FS_CARNO like '%" + strCarNo + "%'";
                   }
                   this.dataTable1.Rows.Clear();
                   CoreClientParam ccp = new CoreClientParam();
                   ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                   ccp.MethodName = "ExcuteQuery";
                   ccp.ServerParams = new object[] { sql };
                   ccp.SourceDataTable = dataSet1.Tables[0];

                   this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                   Constant.RefreshAndAutoSize(ultraGrid3);

                   foreach (UltraGridRow ugr in ultraGrid3.Rows)
                   {
                       if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
                       {
                           ugr.Appearance.ForeColor = Color.Red;
                       }
                   }
        }

        //修改
        private void UpdateWeight()
        {
            if (IsValid() == false)
                return;

            if (m_WeightNo == "")
            {
                MessageBox.Show("请选择需要修改的数据");
                return;
            }

            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strSenderPlace = this.tbSenderPlace.Text.Trim();
            string strReceiverPlace = this.tbReceiverPlace.Text.Trim();
            float inWeight = Convert.ToSingle(this.tbWeight.Text.Trim());
            
            string strMaterial = "";
            if (this.cbMaterial.SelectedValue == null || this.cbMaterial.SelectedValue.ToString().Trim() == "")
                strMaterial = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbMaterial.Text.Trim(), "SaveWLData");
            else
                strMaterial = this.cbMaterial.SelectedValue.ToString().Trim();

            string strSender = "";
            if (this.cbSender.SelectedValue == null || this.cbSender.SelectedValue.ToString().Trim() == "")
                strSender = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbSender.Text.Trim(), "SaveGYDWData");
            else
                strSender = this.cbSender.SelectedValue.ToString().Trim();

            string strReceiver = "";
            if (this.cbReceiver.SelectedValue == null || this.cbReceiver.SelectedValue.ToString().Trim() == "")
                strReceiver = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbReceiver.Text.Trim(), "SaveSHDWData");
            else
                strReceiver = this.cbReceiver.SelectedValue.ToString().Trim();

            string strTrans = "";
            if (this.cbTrans.SelectedValue == null || this.cbTrans.SelectedValue.ToString().Trim() == "")
                strTrans = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbTrans.Text.Trim(), "SaveCYDWData");
            else
                strTrans = this.cbTrans.SelectedValue.ToString().Trim();


            string strProvider = "";
            if (this.cbProvider.SelectedValue != null)
                strProvider = this.cbProvider.SelectedValue.ToString();
            else
            {
                if (this.cbProvider.Text.Trim() != "")
                    strProvider = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbProvider.Text.Trim(), "SaveSPDWData");
            }

            string strBZ = this.tbBZ.Text.Trim();

            string strFlow = this.cbFlow.SelectedValue.ToString().Trim();
            string strMaterialName = this.cbMaterial.Text.Trim();
            string strMemo = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + " 手工补录";


            string sql = "update dt_firstcarweight SET FS_CARDNUMBER = '" + strCardNo + "',";
                   sql += " FS_CARNO = '" + strCarNo + "',";
                   sql += " Fs_Material = '" + strMaterial + "',";
                   sql += " Fs_MaterialName = '" + strMaterialName + "',";
                   sql += " FS_SENDER  = '" + strSender + "',";
                   sql += " Fs_Receiver  = '" + strReceiver + "',";
                   sql += " Fs_Senderstore  = '" + strSenderPlace + "',";
                   sql += " FS_RECEIVERSTORE  = '" + strReceiverPlace + "',";
                   sql += " Fs_Transno  = '" + strTrans + "',";
                   sql += " Fs_Weighttype  = '" + strFlow + "',";
                   sql += " Fs_Poundtype  = '" + m_SelectPointID + "',";
                   sql += " Fs_Provider  = '" + strProvider + "',";
                   sql += " Fs_BZ  = '" + strBZ + "',";
                   sql += " FN_WEIGHT  = " + inWeight + ",";
                   sql += " fs_Memo  = '" + strMemo + "'";
                   sql += " where FS_WEIGHTNO = '" + m_WeightNo + "'";
                   
                   
                    
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);            
        }

        //增加
        private void Add()
        {
            if (IsValid() == false)
                return;

            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strSenderPlace = this.tbSenderPlace.Text.Trim();
            string strReceiverPlace = this.tbReceiverPlace.Text.Trim();
            float inWeight = Convert.ToSingle(this.tbWeight.Text.Trim());

            string strMaterialName = this.cbMaterial.Text.Trim();
            string strPound = this.cbBF.Text.Trim();
            string strShift = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder();

            string strMaterial = "";
            if (this.cbMaterial.SelectedValue == null || this.cbMaterial.SelectedValue.ToString().Trim() == "")
                strMaterial = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbMaterial.Text.Trim(), "SaveWLData");
            else
                strMaterial = this.cbMaterial.SelectedValue.ToString().Trim();


            string strSender = "";
            if (this.cbSender.SelectedValue == null || this.cbSender.SelectedValue.ToString().Trim() == "")
                strSender = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbSender.Text.Trim(), "SaveGYDWData");
            else
                strSender = this.cbSender.SelectedValue.ToString().Trim();

            string strReceiver = "";
            if (this.cbReceiver.SelectedValue == null || this.cbReceiver.SelectedValue.ToString().Trim() == "")
                strReceiver = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbReceiver.Text.Trim(), "SaveSHDWData");
            else
                strReceiver = this.cbReceiver.SelectedValue.ToString().Trim();

            string strTrans = "";
            if (this.cbTrans.SelectedValue == null || this.cbTrans.SelectedValue.ToString().Trim() == "")
                strTrans = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbTrans.Text.Trim(), "SaveCYDWData");
            else
                strTrans = this.cbTrans.SelectedValue.ToString().Trim();

            string strProvider = "";
            if (this.cbProvider.SelectedValue != null)
                strProvider = this.cbProvider.SelectedValue.ToString();
            else
            {
                if (this.cbProvider.Text.Trim() != "")
                    strProvider = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbProvider.Text.Trim(), "SaveSPDWData");
            }

            string strBZ = this.tbBZ.Text.Trim();


            string strFlow = this.cbFlow.SelectedValue.ToString().Trim();

            string strMemo = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() +" 手工补录";

            CoreClientParam ccp = new CoreClientParam();
            string sql = "select FS_POINTSTATE from bt_point where FS_POINTCODE = '" + m_SelectPointID + "'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dtState = new System.Data.DataTable();
            ccp.SourceDataTable = dtState;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            string strState = dtState.Rows[0][0].ToString().Trim();

            sql = "select count(1) from dt_firstcarweight where FS_CARNO = '" + strCarNo + "' and FS_DATASTATE = '"+strState+"'";
            
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dt = new System.Data.DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (Convert.ToInt32(dt.Rows[0][0].ToString().Trim()) > 0)
            {
                MessageBox.Show("该车号在一次计量表中已经存在记录，不允许添加!");
                return;
            }

            m_WeightNo = Guid.NewGuid().ToString();
            string strWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            string strTime = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");


            sql = "insert into dt_firstcarweight (FS_WEIGHTNO,FS_CARDNUMBER,FS_CARNO,Fs_Material,FS_MaterialName,";
            sql += "FS_SENDER,Fs_Receiver,Fs_Transno,Fs_Weighttype,Fs_Poundtype,FS_Pound,Fs_Senderstore,";
            sql += "FS_RECEIVERSTORE,FN_WEIGHT,Fs_Weighter,Fd_Weighttime,fs_Memo,FS_Shift,FS_DATASTATE,FS_Provider,FS_BZ)";
            sql += " values ('" + m_WeightNo + "','" + strCardNo + "','" + strCarNo + "','" + strMaterial + "','" + strMaterialName + "',";
            sql += " '" + strSender + "','" + strReceiver + "','" + strTrans + "','" + strFlow + "','" + m_SelectPointID + "','" + strPound + "','" + strSenderPlace + "',";
            sql += " '" + strReceiverPlace + "'," + inWeight + ",'" + strWeighter + "',TO_DATE('" + strTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strMemo + "','" + strShift + "','"+strState+"','"+strProvider+"','"+strBZ+"')";

             //CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);      

        }

        //删除
        private void Delete()
        {
            if (m_WeightNo == "")
            {
                MessageBox.Show("请选择需要删除的数据");
                return;
            }

            string sql = "delete dt_firstcarweight where FS_WEIGHTNO = '" + m_WeightNo + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            sql = "delete dt_images where FS_WEIGHTNO = '" + m_WeightNo + "'";
            //CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);     
        }

        //控件判断
        private bool IsValid()
        {
            if (this.cbBF.SelectedValue == null || this.cbBF.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择计量磅房");
                this.cbBF.Focus();
                return false;
            }

            if (this.tbCarNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入车号！");
                this.tbCarNo.Focus();
                return false;
            }

            if (this.m_SelectPointID == "")
            {
                MessageBox.Show("请选择计量磅房！");
                this.cbBF.Focus();
                return false;
            }

            if (this.cbFlow.Text == "")
            {
                MessageBox.Show("请选择流向！");
                this.cbFlow.Focus();
                return false;
            }

            if (this.cbMaterial.Text == "")
            {
                MessageBox.Show("请选择物料！");
                this.cbMaterial.Focus();
                return false;
            }

            if (this.cbSender.Text == "")
            {
                MessageBox.Show("请选择发货单位！");
                this.cbSender.Focus();
                return false;
            }

            if (this.cbReceiver.Text == "")
            {
                MessageBox.Show("请选择收货单位！");
                this.cbReceiver.Focus();
                return false;
            }

            if (this.cbTrans.Text == "")
            {
                MessageBox.Show("请选择承运单位！");
                this.cbTrans.Focus();
                return false;
            }

            if (this.cbFlow.SelectedValue == null || this.cbFlow.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择流向!");
                return false;
            }

            return true;
        }

        private void HandRecordFirstWeight_Load(object sender, EventArgs e)
        {
            m_BaseInfo.ob = this.ob; 
            m_BaseInfo.GetBFData(this.cbBF, "QC");
            DownLoadFlow();          
        }        

        //选择磅房下载对应信息
        private void cbBF_SelectedIndexChanged(object sender, EventArgs e)
        {

            m_SelectPointID = this.cbBF.SelectedValue.ToString().Trim();

            if (m_SelectPointID == "")
                return;

            m_BaseInfo.GetWLData(this.cbMaterial, m_SelectPointID);
            m_BaseInfo.GetLXData(this.cbTrans, m_SelectPointID);
            m_BaseInfo.GetGYDWData(this.cbSender, m_SelectPointID);
            m_BaseInfo.GetSHDWData(this.cbReceiver, m_SelectPointID);
            m_BaseInfo.GetCYDWData(this.cbTrans, m_SelectPointID);
            m_BaseInfo.GetSPDWData(this.cbProvider, m_SelectPointID);
        }

        //Grid 点击事件
        private void ultraGrid3_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null)
                return;
            this.cbBF.Text = ultraGrid3.ActiveRow.Cells["FS_POINTNAME"].Text.Trim();
            m_SelectPointID = ultraGrid3.ActiveRow.Cells["FS_Poundtype"].Text.Trim();
            m_WeightNo = ultraGrid3.ActiveRow.Cells["FS_WEIGHTNO"].Text.Trim();

            this.tbCardNo.Text = ultraGrid3.ActiveRow.Cells["FS_CARDNUMBER"].Text.Trim();
            this.tbCarNo.Text = ultraGrid3.ActiveRow.Cells["FS_CARNO"].Text.Trim();
            this.cbMaterial.Text = ultraGrid3.ActiveRow.Cells["FS_MATERIALNAME"].Text.Trim();
            this.cbSender.Text = ultraGrid3.ActiveRow.Cells["FS_SUPPLIERNAME"].Text.Trim();
            this.cbReceiver.Text = ultraGrid3.ActiveRow.Cells["FS_Receiver"].Text.Trim();
            this.tbSenderPlace.Text = ultraGrid3.ActiveRow.Cells["Fs_Senderstore"].Text.Trim();
            this.tbReceiverPlace.Text = ultraGrid3.ActiveRow.Cells["FS_RECEIVERSTORE"].Text.Trim();
            this.cbTrans.Text = ultraGrid3.ActiveRow.Cells["FS_TRANSNAME"].Text.Trim();
            this.cbFlow.Text = ultraGrid3.ActiveRow.Cells["FS_TYPENAME"].Text.Trim();

            this.tbBZ.Text = ultraGrid3.ActiveRow.Cells["FS_BZ"].Text.Trim();
            this.cbProvider.Text = ultraGrid3.ActiveRow.Cells["FS_PROVIDERNAME"].Text.Trim();
        }

        //控件清空
        private void ClearData() 
        {
            this.cbBF.Text = "";
            this.tbCardNo.Text = "";
            this.tbCarNo.Text = "";
            this.tbWeight.Text = "";
            this.cbMaterial.Text = "";
            this.cbFlow.Text = "";
            this.cbTrans.Text = "";
            this.cbSender.Text = "";
            this.cbReceiver.Text = "";
            this.tbSenderPlace.Text = "";
            this.tbReceiverPlace.Text = "";
            this.cbProvider.Text = "";
            this.tbBZ.Text = "";
        }

    }
}
