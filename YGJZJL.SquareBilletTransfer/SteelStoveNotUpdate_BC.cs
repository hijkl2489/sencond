using System;
using System.Collections;
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

namespace YGJZJL.SquareBilletTransfer
{
    public partial class SteelStoveNotUpdate_BC:CoreFS.CA06.FrmBase
    {
        
        decimal m_BaseData = 0.175M;
        BaseInfo m_BaseInfo_1 = new BaseInfo();
        string strUserName = "";
        string strDepartMent = "";
        public SteelStoveNotUpdate_BC()
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

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            if (e.Tool.Key == "查询")
            {
                QueryBCWeightMainData();
            }

            if (e.Tool.Key == "修改")
            {
                UpdateSteelWeightMain();
            }
            //if (e.Tool.Key == "补录")
            //{
            //    DoHandSaveMainForGP();
            //}
            //if (e.Tool.Key == "删除")
            //{
            //    DeleteSteelWeightMain();
            //}
              
            //if (e.Tool.Key == "修改从表")
            //{
            //    UpdateSteelWeightDetail();
            //}
            //if (e.Tool.Key == "补录从表")
            //{
            //    DoHandSaveForGP();
            //}
            if (e.Tool.Key == "删除从表")
            {
                //DeleteSteelWeightDetail();
            }
            //if (e.Tool.Key == "Export")
            //{
            //    CommonMethod.ExportDataWithSaveDialog2(ref this.ultraGrid1, this.Text);
            //}

            if (e.Tool.Key == "修改炉号")
            {
                UpdateStoveno();
            }
        }

        private void QueryBCWeightMainData()
        {
            try
            {
                if (dtBeginTime.Value > dtEndTime.Value)
                {
                    MessageBox.Show("查询开始时间不能大于结束时间");
                    return;
                }

                string strStartTime = dtBeginTime.Value.ToString("yyyy-MM-dd 00:00:00");
                string strEndTime = dtEndTime.Value.ToString("yyyy-MM-dd 23:59:59");

                string strSql = @"select FS_STOVENO,
                                        FS_PRODUCTNO,
                                        FS_ITEMNO,
                                        FS_PLANT,
                                        FS_MRP,
                                        FS_RECEIVEPLANT,
                                        FS_STORE
                                       ,FS_MATERIAL,FS_MATERIALNAME,FS_STEELTYPE,FS_SPEC,FS_WEIGHTTYPE,FN_LENGTH,FS_POINTID
                                       ,round((select SUM(T1.FN_NETWEIGHT) from DT_STEELWEIGHTDETAILROLL T1 WHERE T1.FS_STOVENO = T.FS_STOVENO),3) FN_TOTALWEIGHT
                                       ,round(0.21* FN_LENGTH,3) *(select COUNT(T1.FS_WEIGHTNO) from DT_STEELWEIGHTDETAILROLL T1 WHERE T1.FS_STOVENO = T.FS_STOVENO) as FN_THEORYWEIGHT,
                                       round(0.21*FN_LENGTH,3) FN_THEORYWEIGHTSINGLE
                                       ,to_char(FD_STARTTIME, 'yyyy-MM-dd HH24:mi:ss') FD_STARTTIME,
                                       to_char(FD_ENDTIME, 'yyyy-MM-dd HH24:mi:ss') FD_ENDTIME,FN_BILLETCOUNT,
                                       decode(FS_COMPLETEFLAG, '1', '是', '否') FS_COMPLETEFLAG,
                                       FS_AUDITOR,to_char(FD_AUDITTIME, 'yyyy-MM-dd HH24:mi:ss') FD_AUDITTIME,
                                       FS_UPLOADFLAG,to_char(FD_TOCENTERTIME, 'yyyy-MM-dd HH24:mi:ss') FD_TOCENTERTIME,
                                       FS_ACCOUNTDATE,to_char(FD_TESTIFYDATE, 'yyyy-MM-dd HH24:mi:ss') FD_TESTIFYDATE,
                                       FS_TRANSTYPE,FS_SHIFT,FS_PERSON,FS_TECHCARD,FS_ISMATCH,FS_SAPSTORE,FS_HEADER,
                                       to_char(FD_SHIFTDATE, 'yyyy-MM-dd HH24:mi:ss') FD_SHIFTDATE
                                  from dt_steelweightmain t
                                 where FD_STARTTIME between
                                       to_date('{0}', 'yyyy-MM-dd HH24:mi:ss') and
                                       to_date('{1}', 'yyyy-MM-dd HH24:mi:ss')";
                strSql = string.Format(strSql, strStartTime, strEndTime);

                if (this.tbx_BatchNo.Text.Trim() != string.Empty)
                {
                    strSql += " and FS_STOVENO = '" + this.tbx_BatchNo.Text.Trim().Replace("'", "''") + "'";
                }

                if (this.cbReceiver.SelectedValue.ToString() != string.Empty)
                {
                    strSql += " and FS_STORE='" + cbReceiver.SelectedValue.ToString() + "'";
                }

                strSql += " order by FS_STOVENO desc";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                ccp.SourceDataTable = this.dataSet1.Tables[0];
                this.dataSet1.Tables[0].Clear();
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                //this.txtStoveNo.ReadOnly = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        //主表修改，不能修改炉号
        private void UpdateSteelWeightMain()
        {
            if (!CheckMainItems())
            {
                return;
            }

            string strStoveNo = txtStoveNo.Text.Trim().Replace("'","''");

            int m_BilletCount = getBilletCountByStoveNo(strStoveNo);

            string strProductNo = txtProductNo.Text.Trim().Replace("'","''");
            string strSteelType = txt_SteelType.Text.Trim().Replace("'","''");
            string strSpec = txtSpec.Text.Trim().Replace("'","''");
            string strLength = txtLength.Text.Trim().Replace("'","''");
            string strBilletCount = txtBilletCount.Text.Trim().Replace("'","''");
            string strTheorySingleWeight = txtTheorySingleWeight.Text.Trim().Replace("'","''");
            string strTheoryWeight = txtTheoryWeight.Text.Trim().Replace("'","''");
            string strTotalWeight = txtTotalWeight.Text.Trim().Replace("'","''");

          

            Hashtable ht = new Hashtable();
            ht.Add("I1", strStoveNo);
            ht.Add("I2", strProductNo);
            ht.Add("I3", strSteelType);
            ht.Add("I4", strSpec);
            ht.Add("I5", strLength);
            ht.Add("I6", "");
            ht.Add("I7", strTheorySingleWeight);
            ht.Add("I8", strTheoryWeight);
          
            ht.Add("I9", strTotalWeight);
            ht.Add("I10", UserInfo.GetUserOrder());
            ht.Add("I11", UserInfo.GetUserGroup());
            ht.Add("I12", UserInfo.GetUserName());
            ht.Add("O13", "");
                      

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "executeProcedureBySql";
            ccp.ServerParams = new object[] { "{call HG_MCMS_SQUAREBITTET.UPDATE_BOARDWEIGHT_MAIN(?,?,?,?,?,?,?,?,?,?,?,?,?)}", ht };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            string result = "";
            if (ccp.ReturnObject != null)
            {
                result = ccp.ReturnObject.ToString();
            }
            if (string.IsNullOrEmpty(result))
            {
                QueryBCWeightMainData();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void DeleteSubRecord(string strStoveNo,int i)
        {
            string strSql = "delete from DT_STEELWEIGHTDETAILROLL where FS_STOVENO='" + strStoveNo + "' and FN_BILLETINDEX=" + i;

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strSql };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        private void AddSubRecord(string strStoveNo, int i,string strTheorySingleWeight)
        {
            string strPerson = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() +" 新增";
            string strSql = "insert into DT_STEELWEIGHTDETAILROLL (FS_WEIGHTNO,FS_STOVENO,FN_BILLETINDEX,FN_NETWEIGHT,FS_PERSON,FD_WEIGHTTIME) ";
            strSql += "values('" +Guid.NewGuid().ToString() + "','" + strStoveNo + "'," + i + "," + strTheorySingleWeight + ",'" + strPerson + "',";
            strSql+="sysdate)";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strSql };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        private int getBilletCountByStoveNo(string strStoveNo)
        {
            string strSql = "select FN_BILLETCOUNT from dt_steelweightmain where FS_STOVENO='" + strStoveNo + "'";
            DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    return Convert.ToInt16(dt.Rows[0]["FN_BILLETCOUNT"].ToString());
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        #region 检查主表选项是否合法
        private bool CheckMainItems()
        {
            if (txtStoveNo.Text.Trim() == string.Empty)
            {
                MessageBox.Show("炉号不能为空！");
                return false;
            }

            //if (txtProductNo.Text.Trim() == string.Empty)
            //{
            //    MessageBox.Show("订单号不能为空！");
            //    return false;
            //}

            if (txt_SteelType.Text.Trim() == string.Empty)
            {
                MessageBox.Show("钢种不能为空！");
                return false;
            }

            if (txtSpec.Text.Trim() == string.Empty)
            {
                MessageBox.Show("钢种不能为空！");
                return false;
            }

            if (txtLength.Text.Trim() == string.Empty)
            {
                MessageBox.Show("长度不能为空！");
                return false;
            }

            try
            {
                Convert.ToDecimal(txtLength.Text.Trim());
            }
            catch
            {
                MessageBox.Show("长度只能为数字！");
                return false;
            }

            if (txtBilletCount.Text.Trim() == string.Empty)
            {
                MessageBox.Show("条数不能为空！");
                return false;
            }

            try
            {
                Convert.ToInt16(txtBilletCount.Text);
            }
            catch
            {
                MessageBox.Show("条数只能为数字！");
                return false;
            }

            if (Convert.ToInt16(txtBilletCount.Text) <= 0)
            {
                MessageBox.Show("条数不能小于0！");
                return false;
            }



            if (this.txtTheorySingleWeight.Text.Trim() != "")
            {
               
            
            try
            {
                Convert.ToDecimal(txtTheorySingleWeight.Text);
            }
            catch
            {
                MessageBox.Show("单支理论重量只能为数字！");
                return false;
            }
            }

            //if (this.txtTotalWeight.Text.Trim() == string.Empty)
            //{
            //    MessageBox.Show("总重量不能为空！");
            //    return false;
            //}

            //try
            //{
            //    Convert.ToDecimal(txtTotalWeight.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("总重量只能为数字！");
            //    return false;
            //}

            //if (this.txtTheoryWeight.Text.Trim() == string.Empty)
            //{
            //    MessageBox.Show("总理重不能为空！");
            //    return false;
            //}

            //try
            //{
            //    Convert.ToDecimal(txtTheoryWeight.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("总理重只能为数字！");
            //    return false;
            //}

            return true;
        }
        #endregion

         #region 检查从表选项是否合法
        private bool CheckDetailItems()
        {
            if (this.txtCBNetWeight.Text.Trim() == string.Empty)
            {
                MessageBox.Show("单支净重不能为空！");
                return false;
            }

            try
            {
                Convert.ToDecimal(txtCBNetWeight.Text);
            }
            catch
            {
                MessageBox.Show("单支净重只能为数字！");
                return false;
            }

            return true;
        }
         #endregion

       // private void UpdateSteelWeightDetail()
       //{
       //    if (!CheckDetailItems())
       //    {
       //        return;
       //    }

       //     string strStoveNo=this.txtCBStoveNo.Text;
       //     string strBilletIndex=this.txtBilletIndex.Text;
       //     string strTheorySingleWeight=this.txtCBNetWeight.Text;
       //    string strPerson = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + " 修改";

       //    string strSql = "update DT_STEELWEIGHTDETAILROLL set FN_NETWEIGHT=" + strTheorySingleWeight + ",FS_PERSON='" + strPerson + "',FD_WEIGHTTIME=sysdate,";
       //    strSql += "FS_THWEITSINGLE='" + ultraCombo1.SelectedRow.Cells["Value"].Text+"' where FS_STOVENO='" + strStoveNo + "'and FN_BILLETINDEX=" + strBilletIndex;

       //    CoreClientParam ccp = new CoreClientParam();
       //    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
       //    ccp.MethodName = "ExcuteNonQuery";
       //    ccp.ServerParams = new object[] { strSql };
       //    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

       //    if (ccp.ReturnCode == 0)
       //    {
       //        MessageBox.Show("从表记录修改成功！");
       //    }

       //    string totalWeight = getTotalWeight(strStoveNo);

       //    if (totalWeight == string.Empty)
       //    {
       //        return;
       //    }
       //    else//更新主表的重量信息
       //    {
       //        string strSqlTemp = "update dt_steelweightmain set FN_THEORYWEIGHT=" + totalWeight + " where FS_STOVENO='" + strStoveNo + "'";
       //        ccp.ServerParams = new object[] { strSqlTemp };
       //        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
       //    }

       //    QueryBCWeightMainData();

       //    for (int i = 0; i < ultraGrid1.Rows.Count; i++)
       //    {
       //        if (ultraGrid1.Rows[i].Cells["FS_STOVENO"].Text == strStoveNo)
       //        {
       //            ultraGrid1.Rows[i].Activated = true;
       //            ultraGrid1.Rows[i].ExpandAll();
       //        }
       //    }
       //}

        public string getTotalWeight(string strStoveNo)
        {
            string strSql = "select sum(FN_NETWEIGHT) totalWeight from DT_STEELWEIGHTDETAILROLL where FS_STOVENO='" + strStoveNo + "'";
            DataTable dt = new DataTable();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            string strTotalWeight = string.Empty;
            if (dt.Rows.Count > 0)
            {
                strTotalWeight = dt.Rows[0]["totalWeight"].ToString();
            }

            return strTotalWeight;
        }

        private void SelectActiveRow(string strStoveNo)
        {
            if (this.ultraGrid1.Rows.Count == 0)
                return;
            for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
            {
                if (this.ultraGrid1.Rows[i].Cells["FS_STOVENO"].Text.Trim() == strStoveNo)
                {
                    this.ultraGrid1.Rows[i].Selected = true;
                    this.ultraGrid1.Rows[i].Activated = true;
                    return;
                }
            }

        }

        private void WeightRePrint_BC_Load(object sender, EventArgs e)
        {
            strUserName = UserInfo.GetUserName();//操作人
            strDepartMent = UserInfo.GetDepartment();//所属部门
            BindComboData();
            BindLXType();
            //this.txtTheorySingleWeight.ReadOnly = false;
            //this.txtTotalWeight.ReadOnly = false;
            string strKey = this.CustomInfo.ToUpper();
            if (strKey =="QU")
            {
                SetToolButtonVisible("修改", false);
                SetToolButtonVisible("删除", false);
                SetToolButtonVisible("补录", false);
                this.panel6.Visible = false;
            }
            if (strKey == "UPSTOVENO")

            {
                SetToolButtonVisible("修改", false);
                SetToolButtonVisible("删除", false);
                SetToolButtonVisible("补录", false);
            }
            //m_BaseInfo = new BaseInfo();
            //m_BaseInfo.ob = this.ob;
            //this.dtBeginTime.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            //m_szCurBC = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            //m_szCurBZ = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
        }

        private void SetToolButtonVisible(string strKey, bool Visible)
        {
            try
            {
                this.ultraToolbarsManager1.Tools[strKey].SharedProps.Visible = Visible;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void BindLXType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            // dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows.Add("", "");
            dt.Rows.Add("SH000098", "棒材厂");
            dt.Rows.Add("SH000100", "高线厂");
            this.cbReceiver.DataSource = dt;
            cbReceiver.ValueMember = "Value";
            cbReceiver.DisplayMember = "Text";
        }

        /// <summary>
        /// 检查是否已完炉，没找到卡片返回0，完炉返回1，没完炉返回2,20110210彭海波增加
        /// </summary>
        private int IsGpCompleted(string str_stoveno)
        {
            int int_isqyg = -1;
            try
            {
                string strSql = "SELECT fs_gp_completeflag from IT_FP_TECHCARD WHERE FS_GP_STOVENO='" + str_stoveno.Trim() + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.StorageInfo";
                ccp.MethodName = "queryByClientSql";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    string str_cardno = dt_temp.Rows[0]["fs_gp_completeflag"].ToString().Trim();
                    if (str_cardno.Equals("1"))
                    {
                        int_isqyg = 1;
                    }
                    else
                    {
                        int_isqyg = 2;
                    }
                }
                else
                {
                    int_isqyg = 0;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return int_isqyg;
            }
            return int_isqyg;
        }


       

        private bool CheckIsExist(string strBatchNo)
        {
            try
            {
                string strSql = "select * from DT_PRODUCTPLAN where FS_STOVENO = '" + strBatchNo + "'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
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

        private void UpdateStoveno()//修改炉号
        {

            UltraGridRow ugr = ultraGrid1.ActiveRow;
            if (ugr == null)
            {
                MessageBox.Show("请先选择要修改的记录！");
                return;
            }

            //if (!this.txtStoveNo.Text.Contains("-"))
            //{
            //    MessageBox.Show("炉号必须包含“-”！");
            //    return;
            //}

            if (MessageBox.Show("此操作会彻底修改该炉钢坯的炉号，确定？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            string strStoveNo = ugr.Cells["FS_STOVENO"].Text;
            string strNewStoveno = this.txtStoveNo.Text.Trim();


            try
            {

                //string updateMain = "update DT_STEELWEIGHTMAIN set FS_STOVENO = '" + strNewStoveno + "' where FS_STOVENO = '" + strStoveNo + "'";
                //string updateDetail = "update DT_STEELWEIGHTDETAILROLL  set FS_STOVENO = '" + strNewStoveno + "' where FS_STOVENO = '" + strStoveNo + "'";
                //string updateTechcard = "update IT_FP_TECHCARD  set FS_GP_STOVENO = '" + strNewStoveno + "' where FS_GP_STOVENO = '" + strStoveNo + "'";
                //string updateProductplan = "update DT_PRODUCTPLAN  set FS_STOVENO = '" + strNewStoveno + "' where FS_STOVENO = '" + strStoveNo + "'";
                

                //CoreClientParam ccp = new CoreClientParam();
                //ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                //ccp.MethodName = "ExcuteNonQuery";
                //if (CheckIsExist(strStoveNo)==true)
                //{
                //    ccp.ServerParams = new object[] { updateProductplan };
                //    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                //}

                //ccp.ServerParams = new object[] { updateTechcard };
                //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                //ccp.ServerParams = new object[] { updateMain };
                //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                //ccp.ServerParams = new object[] { updateDetail };
                //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                Hashtable ht = new Hashtable();
                ht.Add("I1", strStoveNo);
                ht.Add("I2", strNewStoveno);
                ht.Add("O3", "");

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "executeProcedureBySql";
                ccp.ServerParams = new object[] { "{call HG_MCMS_SQUAREBITTET.UpdateStoveno1(?,?,?)}", ht };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                string result = "";




                if (ccp.ReturnObject != null)
                {
                    result = ccp.ReturnObject.ToString();

                    //string txtBatchnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text.ToString();
                    //string strBandnoLog = this.ultraGrid1.Rows[rowid].Cells["FN_BANDNO"].Text.ToString();
                    //string strReelLog = this.ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text.ToString();
                    string strStovenoLog = ugr.Cells["FS_STOVENO"].Text.ToString();
                    //string strWeightLog = this.ultraGrid1.Rows[rowid].Cells["FN_WEIGHT"].Text.ToString();
                    //string strYklLog = this.ultraGrid1.Rows[rowid].Cells["FN_YKL"].Text.ToString();
                    //string strKhjzLog = this.ultraGrid1.Rows[rowid].Cells["FN_KHJZ"].Text.ToString();
                    //string strProductnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_PRODUCTNO"].Text.ToString();
                    //string strSteeltypeLog = this.ultraGrid1.Rows[rowid].Cells["FS_STEELTYPE"].Text.ToString();
                    //string strSpecLog = this.ultraGrid1.Rows[rowid].Cells["FS_SPEC"].Text.ToString();
                    //string strMemoLog = this.ultraGrid1.Rows[rowid].Cells["FS_KZTYPE"].Text.ToString();


                    string strLog = "炉号：" + strStovenoLog + ">" + strNewStoveno ;

                    m_BaseInfo_1.ob = this.ob;
                    this.m_BaseInfo_1.WriteOperationLog("DT_STEELWEIGHTMAIN", strDepartMent, strUserName, "修改炉号", strLog, "方坯炉号修改", "方坯计量表、工艺流动卡表、方坯预报表", "方坯炉号修改");//调用写操作日志方法


                    QueryBCWeightMainData();
                }


                MessageBox.Show(result);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }


        }

      

        private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null)
            {
                return;
            }

            //如果是双击主表
            if (ugr.ParentRow == null)
            {
                this.ultraTabControl1.Tabs[0].Selected = true;
                //if (this.CustomInfo.ToUpper() != "UPSTOVENO")
                //{
                //    this.txtStoveNo.ReadOnly = true;
                //}

                this.txtStoveNo.Text = ugr.Cells["FS_STOVENO"].Text;
                this.txtProductNo.Text = ugr.Cells["FS_PRODUCTNO"].Text;
                this.txt_SteelType.Text = ugr.Cells["FS_STEELTYPE"].Text;
                this.txtSpec.Text = ugr.Cells["FS_SPEC"].Text;
                this.txtLength.Text = ugr.Cells["FN_LENGTH"].Text;
                //this.txtBilletCount.Text = ugr.Cells["FN_BILLETCOUNT"].Text;
                //this.txtTheorySingleWeight.Text = ugr.Cells["FN_THEORYWEIGHTSINGLE"].Text;
                this.txtTheorySingleWeight.Text = ugr.Cells["FN_THEORYWEIGHT"].Text;
                this.txtTotalWeight.Text = ugr.Cells["FN_WEIGHT"].Text;

                int m_BilletCount = 0;

                try
                {
                    m_BilletCount = Convert.ToInt16(ugr.Cells["FN_BILLETCOUNT"].Text);
                }
                catch
                {
                    m_BilletCount = 0;
                }

                if (ugr.Cells["FN_LENGTH"].Text == string.Empty)
                {
                    this.txtLength.Text = "11.80";
                }

                if (ugr.Cells["FN_THEORYWEIGHTSINGLE"].Text.Trim() == string.Empty)
                {
                    txtTheorySingleWeight.Text = (m_BilletCount * 0.175).ToString();
                }

                if (ugr.Cells["FN_THEORYWEIGHT"].Text.Trim() == string.Empty)
                {
                    try
                    {
                        txtTheoryWeight.Text = (m_BilletCount * m_BaseData * Convert.ToDecimal(ugr.Cells["FN_LENGTH"].Text)).ToString();
                    }
                    catch
                    {

                    }
                }
            }
            //else//如果是双击从表
            //{
            //    this.ultraTabControl1.Tabs[1].Selected = true;
            //    this.txtCBStoveNo.ReadOnly = true;
            //    this.txtBilletIndex.ReadOnly = true;

            //    this.txtCBStoveNo.Text = ugr.Cells["FS_STOVENO"].Text;
            //    this.txtBilletIndex.Text = ugr.Cells["FN_BILLETINDEX"].Text;
            //    this.txtCBNetWeight.Text = ugr.Cells["FN_NETWEIGHT"].Text;
            //    string isTheoryWeight = ugr.Cells["FS_THWEITSINGLE"].Text;
            //    if (isTheoryWeight == "是")
            //    {
            //        this.ultraCombo1.Rows[0].Selected = true;
            //    }
            //    else
            //    {
            //        this.ultraCombo1.Rows[1].Selected = true;
            //    }
            //}
        }

        private void BindComboData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");
            dt.Rows.Add("是", "0");
            dt.Rows.Add("否", "1");
            //this.ultraCombo1.DataSource = dt;
            //this.ultraCombo1.ValueMember = "Value";
            //this.ultraCombo1.DisplayMember = "Text";
        }
        
        private bool isFloat(string str)
        {
            try
            {
                Convert.ToDecimal(str);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void txtBilletCount_KeyUp(object sender, KeyEventArgs e)
        {
            if (isFloat(txtLength.Text.ToString()))
            {
                try
                {
                    txtTheorySingleWeight.Text = (Convert.ToDecimal(txtLength.Text) * m_BaseData).ToString();
                    txtTheoryWeight.Text = (Convert.ToDecimal(txtLength.Text) * m_BaseData * Convert.ToDecimal(txtBilletCount.Text)).ToString();
                }
                catch
                {
                    txtTheorySingleWeight.Text = string.Empty;
                    txtTheoryWeight.Text = string.Empty;
                    txtBilletCount.Text = string.Empty;
                }
            }
        }

        private void ultraGrid1_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            ultraGrid1_DoubleClick(ultraGrid1, null);
        }

        /// <summary>
        /// 执行存储过程2
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected CoreClientParam excuteProcedure2(string sql, Hashtable param)
        {
            if (param == null)
            {
                param = new Hashtable();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "executeProcedureBySql2";
            ccp.ServerParams = new object[] { sql, param };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            return ccp;
        }

    }

}
