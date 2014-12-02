/********************************************************************************************************
* 添加一个界面对表bt_bctheoryweightinfo进行增、删、查、改操作 
* 作    者      : 
* 创建日期      :
* 注意事项      :                     
* 遗留BUG       :         
* 修改人员      : Ksh新建 on 20140101
* 修改内容      : 
********************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using YGJZJL.PublicComponent;
using Infragistics.Win.UltraWinGrid;

namespace YGJZJL.Bar
{
    public partial class TheoryWeightInfo_BC : FrmBase
    {
        #region 变量定义
        BaseInfo baseinfo = new BaseInfo();
        string strUserName = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        string strDepartMent = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TheoryWeightInfo_BC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BilletStock_Load(object sender, EventArgs e)
        {
            CommonMethod.RefreshAndAutoSize(ultraGrid1);          
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        private void GetBCtheoryWeightInfo()
        {
            string strSql = @"select   'false' checked, 
                                       FS_SPEC,
                                       FN_LENGTH,
                                       FN_WEIGHT,
                                       FN_BILLETCOUNT,
                                       FN_ID,
                                       FS_MINDIFFERENTRATE,
                                       FS_MAXDIFFERENTRATE,
                                       FS_BUNDLEWEIGHT,
                                       FS_BUNDLEMINWWEIGHT,
                                       FS_BUNDLEMAXWWEIGHT
                              from BT_BCTHEORYWEIGHTINFO  t  
                              where 1 = 1 ";

            if (!string.IsNullOrEmpty(tbqueryFS_SPEC.Text.Trim()))//规格
            {
                strSql += Convert.ToString("   and t.FS_SPEC like '%" + tbqueryFS_SPEC.Text.Trim() + "%'").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbqueryFN_LENGTH.Text.Trim()))//长度
            {
                strSql += Convert.ToString("   and t.FN_LENGTH like '%" + tbqueryFN_LENGTH.Text.Trim() + "%'").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbqueryFN_WEIGHT.Text.Trim()))//单支理论重量
            {
                strSql += Convert.ToString("   and t.FN_WEIGHT like '%" + tbqueryFN_WEIGHT.Text.Trim() + "%'").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbqueryFN_BILLETCOUNT.Text.Trim())) //单捆包装支数
            {
                strSql += Convert.ToString("   and t.FN_BILLETCOUNT like '%" + tbqueryFN_BILLETCOUNT.Text.Trim() + "%'").Trim() + " ";
            }
            strSql += Convert.ToString(" order by t.FN_ID asc").Trim();

            string err = "";
            DataSet ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable table = ds.Tables[0];
                CommonMethod.CopyDataToDatatable(ref table, ref dataTable1, true);
            }
            else
            {
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("查询出错！\n" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dataTable1.Rows.Clear();
            }

            CommonMethod.RefreshAndAutoSize(ultraGrid1);

            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];
            }
            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                ultraGrid1.Rows[i].Cells["FN_ID"].Appearance.BackColor = Color.FromArgb(192, 255, 192);
            }


            //for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            //{
            //    try
            //    {
            //        if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_CARDNO"].Value).Equals(""))
            //        {
            //            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FS_CARDNO", false);
            //        }

            //        if (ultraGrid1.Rows[i].Cells["FS_UNQUALIFIED"].Value.ToString() == "×")
            //        {
            //            ultraGrid1.Rows[i].Appearance.ForeColor = Color.Red;
            //        }
            //    }
            //    catch { }
            //}
        }

        /// <summary>
        /// 数据有效性验证
        /// </summary>
        /// <param name="alistCardNo"></param>
        /// <param name="alistCount"></param>
        /// <param name="alistWeight"></param>
        /// <param name="alistPosition"></param>
        /// <param name="alistStoveNo"></param>
        /// <returns></returns>
        private bool DataValidation_Save(out ArrayList alistCardNo, out ArrayList alistCount, out  ArrayList alistWeight, out ArrayList alistPosition, out ArrayList alistStoveNo)
        {
            alistStoveNo = new ArrayList();
            alistCardNo = new ArrayList();
            alistCount = new ArrayList();
            alistWeight = new ArrayList();
            alistPosition = new ArrayList();

            ultraGrid1.UpdateData();

            try
            {
                UltraGridRow row = null;
                string strCardNo = "", strCount = "", strWeight = "", strPosition = "", strStoveNo = "";

                string str_FS_SPEC;
                string str_FN_LENGTH;
                string str_FN_WEIGHT;
                string str_FN_BILLETCOUNT;
                string str_FN_ID;
                string str_FS_MINDIFFERENTRATE;
                string str_FS_MAXDIFFERENTRATE;
                string str_FS_BUNDLEWEIGHT;
                string str_FS_BUNDLEMINWWEIGHT;
                string str_FS_BUNDLEMAXWWEIGHT;

                for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
                {
                    row = this.ultraGrid1.Rows[i];

                    if (Convert.ToBoolean(row.Cells["CHECKED"].Value))
                    {
                        strCardNo = Convert.ToString(row.Cells["FS_CARDNO"].Value).Trim();
                        strCount = Convert.ToString(row.Cells["FN_GPYS_NUMBER"].Value).Trim();
                        strWeight = Convert.ToString(row.Cells["FN_GPYS_WEIGHT"].Value).Trim();
                        strPosition = Convert.ToString(row.Cells["FS_DJH"].Value).Trim();
                        strStoveNo = Convert.ToString(row.Cells["FS_GP_STOVENO"].Value).Trim();

                        //BT_BCTHEORYWEIGHTINFO的所有字段
                        str_FS_SPEC = Convert.ToString(row.Cells["FS_SPEC"].Value).Trim();
                        str_FN_LENGTH = Convert.ToString(row.Cells["FN_LENGTH"].Value).Trim();
                        str_FN_WEIGHT = Convert.ToString(row.Cells["FN_WEIGHT"].Value).Trim();
                        str_FN_BILLETCOUNT = Convert.ToString(row.Cells["FN_BILLETCOUNT"].Value).Trim();
                        str_FN_ID = Convert.ToString(row.Cells["FN_ID"].Value).Trim();
                        str_FS_MINDIFFERENTRATE = Convert.ToString(row.Cells["FS_MINDIFFERENTRATE"].Value).Trim();
                        str_FS_MAXDIFFERENTRATE = Convert.ToString(row.Cells["FS_MAXDIFFERENTRATE"].Value).Trim();
                        str_FS_BUNDLEWEIGHT = Convert.ToString(row.Cells["FS_BUNDLEWEIGHT"].Value).Trim();
                        str_FS_BUNDLEMINWWEIGHT = Convert.ToString(row.Cells["FS_BUNDLEMINWWEIGHT"].Value).Trim();
                        str_FS_BUNDLEMAXWWEIGHT = Convert.ToString(row.Cells["FS_BUNDLEMAXWWEIGHT"].Value).Trim();


                        if (string.IsNullOrEmpty(strCount))
                        {
                            MessageBox.Show("请输入验收条数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_NUMBER", true);
                            return false;
                        }

                        int iCount = 0;
                        bool bOK = false;
                        bOK = int.TryParse(strCount, out iCount);
                        if (!bOK)
                        {
                            MessageBox.Show("验收条数必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_NUMBER", true);
                            return false;
                        }

                        if (iCount <= 0)
                        {
                            MessageBox.Show("验收条数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_NUMBER", true);
                            return false;
                        }

                        if (string.IsNullOrEmpty(strWeight))
                        {
                            MessageBox.Show("请输入验收重量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_WEIGHT", true);
                            return false;
                        }

                        decimal dWeight = 0.0M;
                        bOK = decimal.TryParse(strWeight, out dWeight);
                        if (!bOK)
                        {
                            MessageBox.Show("验收重量必须是数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_WEIGHT", true);
                            return false;
                        }

                        if (dWeight <= 0)
                        {
                            MessageBox.Show("验收重量必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_WEIGHT", true);
                            return false;
                        }

                        if (string.IsNullOrEmpty(strPosition))
                        {
                            MessageBox.Show("请输入货架号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FS_DJH", true);
                            return false;
                        }

                        alistCardNo.Add(strCardNo);
                        alistCount.Add(strCount);
                        alistWeight.Add(strWeight);
                        alistPosition.Add(strPosition);
                        alistStoveNo.Add(strStoveNo);










                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据验证失败！\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 批量操作时数据有效性验证：在批量删除时验证FN_ID字段
        /// </summary>
        /// <param name="alistFN_ID">字段FN_ID的数组值</param>
        /// <returns>验证通过返回true,否则返回false</returns>
        private bool DataValidation_Delete(out ArrayList alistFN_ID, out ArrayList alistFS_SPEC, out ArrayList alistFN_LENGTH)
        {
            alistFN_ID = new ArrayList();
            alistFS_SPEC = new ArrayList();//规格
            alistFN_LENGTH = new ArrayList();//长度
            this.ultraGrid1.UpdateData();
            try
            {
                for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(this.ultraGrid1.Rows[i].Cells["CHECKED"].Value))
                    {
                        alistFN_ID.Add(Convert.ToString(this.ultraGrid1.Rows[i].Cells["FN_ID"].Value).Trim());
                        alistFS_SPEC.Add(Convert.ToString(this.ultraGrid1.Rows[i].Cells["FS_SPEC"].Value).Trim());
                        alistFN_LENGTH.Add(Convert.ToString(this.ultraGrid1.Rows[i].Cells["FN_LENGTH"].Value).Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("字段FN_ID的数组数据验证失败！\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }
           
        /// <summary>
        /// 修改记录
        /// </summary>
        private void Update()
        {
            if (ultraGrid1.Rows.Count <= 0)
            {
                MessageBox.Show("请先执行查询操作，并激活要修改的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //规格和长度为组合主键(PK_BCTHEIRYWEIGHTINFO: Primary >>  FS_SPEC, FN_LENGTH)
            if (this.tbFS_SPEC.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("请在‘数据编辑区’输入规格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (this.tbFN_LENGTH.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("请在‘数据编辑区’输入长度！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string FS_SPEC = this.tbFS_SPEC.Text.Trim();    //规格
            string FN_LENGTH = this.tbFN_LENGTH.Text.Trim();//长度
            string FN_ID = this.tbFN_ID.Text.Trim();        //记录ID号

            string strSql = @"select   FN_ID, FS_SPEC, FN_LENGTH 
                              from  BT_BCTHEORYWEIGHTINFO  t  
                              where 1 = 1   
                                and t.FN_ID <>  '" + FN_ID + @"'  
                                and t.FS_SPEC = '" + FS_SPEC + @"' 
                                and t.FN_LENGTH='" + FN_LENGTH + @"'";
            string err = "";
            DataSet ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //DataTable dt = ds.Tables[0];
                MessageBox.Show("该规格和长度(规格和长度为组合主键)的理重配置已存在,请勿录入重复的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("查询(规格和长度为组合主键)出错：\n" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    string FN_WEIGHT = this.tbFN_WEIGHT.Text.Trim();
                    string FN_BILLETCOUNT = this.tbFN_BILLETCOUNT.Text.Trim();
                    string FS_MINDIFFERENTRATE = this.tbFS_MINDIFFERENTRATE.Text.Trim();
                    string FS_MAXDIFFERENTRATE = this.tbFS_MAXDIFFERENTRATE.Text.Trim();
                    string FS_BUNDLEWEIGHT = this.tbFS_BUNDLEWEIGHT.Text.Trim();
                    string FS_BUNDLEMINWWEIGHT = this.tbFS_BUNDLEMINWWEIGHT.Text.Trim();
                    string FS_BUNDLEMAXWWEIGHT = this.tbFS_BUNDLEMAXWWEIGHT.Text.Trim();
                    string sql = @"update  bt_bctheoryweightinfo 
                                   set  FS_SPEC=        '" + FS_SPEC + @"',
                                        FN_LENGTH=      '" + FN_LENGTH + @"',
                                        FN_WEIGHT=      '" + FN_WEIGHT + @"',
                                        FN_BILLETCOUNT= '" + FN_BILLETCOUNT + @"',             
                                        FS_MINDIFFERENTRATE='" + FS_MINDIFFERENTRATE + @"',
                                        FS_MAXDIFFERENTRATE='" + FS_MAXDIFFERENTRATE + @"',
                                        FS_BUNDLEWEIGHT=    '" + FS_BUNDLEWEIGHT + @"',
                                        FS_BUNDLEMINWWEIGHT='" + FS_BUNDLEMINWWEIGHT + @"',
                                        FS_BUNDLEMAXWWEIGHT='" + FS_BUNDLEMAXWWEIGHT + @"'
                                    where
                                        FN_ID = '" + FN_ID + "'";

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { sql };
                    ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    if (ccp.ReturnCode == 0)
                    {
                        //调用写操作日志方法   
                        string strold_FS_SPEC = this.ultraGrid1.ActiveRow.Cells["FS_SPEC"].Text;
                        string strold_FN_LENGTH = this.ultraGrid1.ActiveRow.Cells["FN_LENGTH"].Text;
                        string strold_FN_WEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_WEIGHT"].Text;
                        string strold_FN_BILLETCOUNT = this.ultraGrid1.ActiveRow.Cells["FN_BILLETCOUNT"].Text;
                        string strold_FS_MINDIFFERENTRATE = this.ultraGrid1.ActiveRow.Cells["FS_MINDIFFERENTRATE"].Text;
                        string strold_FS_MAXDIFFERENTRATE = this.ultraGrid1.ActiveRow.Cells["FS_MAXDIFFERENTRATE"].Text;
                        string strold_FS_BUNDLEWEIGHT = this.ultraGrid1.ActiveRow.Cells["FS_BUNDLEWEIGHT"].Text;
                        string strold_FS_BUNDLEMINWWEIGHT = this.ultraGrid1.ActiveRow.Cells["FS_BUNDLEMINWWEIGHT"].Text;
                        string strold_FS_BUNDLEMAXWWEIGHT = this.ultraGrid1.ActiveRow.Cells["FS_BUNDLEMAXWWEIGHT"].Text;
                        string strLog = "ID=" + FN_ID
                                     + ",规格：" + strold_FS_SPEC + ">" + FS_SPEC
                                     + ",长度：" + strold_FN_LENGTH + ">" + FN_LENGTH
                                     + ",单支理论重量：" + strold_FN_WEIGHT + ">" + FN_WEIGHT
                                     + ",单捆包装支数：" + strold_FN_BILLETCOUNT + ">" + FN_BILLETCOUNT
                                     + ",重量负差率下限：" + strold_FS_MINDIFFERENTRATE + ">" + FS_MINDIFFERENTRATE
                                     + ",重量负差率上限：" + strold_FS_MAXDIFFERENTRATE + ">" + FS_MAXDIFFERENTRATE
                                     + ",单捆理论重量：" + strold_FS_BUNDLEWEIGHT + ">" + FS_BUNDLEWEIGHT
                                     + ",单捆理论重量下限：" + strold_FS_BUNDLEMINWWEIGHT + ">" + FS_BUNDLEMINWWEIGHT
                                     + ",单捆理论重量上限：" + strold_FS_BUNDLEMAXWWEIGHT + ">" + FS_BUNDLEMAXWWEIGHT;
                        baseinfo.ob = this.ob;
                        this.baseinfo.WriteOperationLog("BT_BCTHEORYWEIGHTINFO", strDepartMent, strUserName, "修改", strLog, "棒材理重基础信息维护", "棒材上传垛帐系统理论重量对应表", "棒材计量");

                        this.GetBCtheoryWeightInfo();
                        MessageBox.Show("修改数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("新增数据失败：\n" + ex.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }

        }

        private void Save()
        {
            try
            {
                if (ultraGrid1.Rows.Count <= 0)
                {
                    return;
                }
                ArrayList alistCardNo = new ArrayList();
                ArrayList alistCount = new ArrayList();
                ArrayList alistWeight = new ArrayList();
                ArrayList alistPosition = new ArrayList();
                ArrayList alistStoveNo = new ArrayList();

                bool bOK = DataValidation_Save(out alistCardNo, out alistCount, out alistWeight, out alistPosition, out alistStoveNo);

                if (!bOK)
                    return;

                if (alistCardNo.Count == 0)
                {
                    MessageBox.Show("请选择需要验收的炉号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                object[] sArgs;
                string strProcedure = "", strErr = "";
                ArrayList obj = null;

                Hashtable htblValue = new Hashtable();

                sArgs = new object[] { alistCardNo, alistCount, alistWeight, alistPosition, this.UserInfo.GetUserName() };

                strProcedure = "KG_MCMS_FLOWCARD.StockBilletReceive";
                obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                /*if (int.Parse(obj[2].ToString()) > 0)
                {
                    //加入验收后获取最终成分功能，调用甘俊的代码
                    try
                    {
                        string[] Stoves = GetStoveArray(alistStoveNo);
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
                        ccp.MethodName = "updateJudgeElements";
                        ccp.ServerParams = new object[] { Stoves };
                        CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    }
                    catch { }

                    this.GetStockInfo(alistCardNo[0].ToString());
                    MessageBox.Show("数据保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/

                this.GetBCtheoryWeightInfo();
                MessageBox.Show("验收成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch { }
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        private void Add()
        {
            //规格和长度为组合主键(PK_BCTHEIRYWEIGHTINFO: Primary >>  FS_SPEC, FN_LENGTH)
            if (this.tbFS_SPEC.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("请在‘数据编辑区’输入规格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (this.tbFN_LENGTH.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("请在‘数据编辑区’输入长度！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string FS_SPEC = this.tbFS_SPEC.Text.Trim();
            string FN_LENGTH = this.tbFN_LENGTH.Text.Trim();
            string FN_ID = string.Empty; //记录ID号
            string strSql = @"select   FN_ID, FS_SPEC, FN_LENGTH 
                              from  BT_BCTHEORYWEIGHTINFO  t  
                              where 1 = 1 and t.FS_SPEC = '" + FS_SPEC + @"' and t.FN_LENGTH='" + FN_LENGTH + @"'";
            string err = "";
            DataSet ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {                //DataTable dt = ds.Tables[0];
                MessageBox.Show("该规格和长度(规格和长度为组合主键)的理重配置已存在,请修改规格或长度！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("查询(规格和长度为组合主键)出错：\n" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //获取数据库中最大的记录ID号并将 ID+1 作为被新增记录的ID
                string strSql_GetMaxID = @"select max(FN_ID) MaxID from BT_BCTHEORYWEIGHTINFO";
                ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql_GetMaxID }, out err);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int MaxId = -1;
                    FN_ID = ds.Tables[0].Rows[0]["MAXID"].ToString();
                    try
                    {
                        MaxId = Convert.ToInt16(FN_ID);
                        MaxId++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("字段FN_ID值转换出错：" + ex.Message.ToString());
                        return;
                    }
                    FN_ID = Convert.ToString(MaxId);
                }
                else
                {
                    MessageBox.Show("查询MaxID值出错！");
                    return;
                }
            }
            try
            {
                string FN_WEIGHT = this.tbFN_WEIGHT.Text.Trim();
                string FN_BILLETCOUNT = this.tbFN_BILLETCOUNT.Text.Trim();
                string FS_MINDIFFERENTRATE = this.tbFS_MINDIFFERENTRATE.Text.Trim();
                string FS_MAXDIFFERENTRATE = this.tbFS_MAXDIFFERENTRATE.Text.Trim();
                string FS_BUNDLEWEIGHT = this.tbFS_BUNDLEWEIGHT.Text.Trim();
                string FS_BUNDLEMINWWEIGHT = this.tbFS_BUNDLEMINWWEIGHT.Text.Trim();
                string FS_BUNDLEMAXWWEIGHT = this.tbFS_BUNDLEMAXWWEIGHT.Text.Trim();
                string sql = @"insert into bt_bctheoryweightinfo
                                (   FS_SPEC,
                                    FN_LENGTH,
                                    FN_WEIGHT,
                                    FN_BILLETCOUNT,
                                    FN_ID,
                                    FS_MINDIFFERENTRATE,
                                    FS_MAXDIFFERENTRATE,
                                    FS_BUNDLEWEIGHT,
                                    FS_BUNDLEMINWWEIGHT,
                                    FS_BUNDLEMAXWWEIGHT  ) 
                                values 
                                ('" + FS_SPEC + @"','"
                                     + FN_LENGTH + @"','"
                                     + FN_WEIGHT + @"','"
                                     + FN_BILLETCOUNT + @"','"
                                     + FN_ID + @"','"
                                     + FS_MINDIFFERENTRATE + @"','"
                                     + FS_MAXDIFFERENTRATE + @"','"
                                     + FS_BUNDLEWEIGHT + @"','"
                                     + FS_BUNDLEMINWWEIGHT + @"','"
                                     + FS_BUNDLEMAXWWEIGHT + @"' )";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { sql };
                ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnCode == 0)
                {
                    this.GetBCtheoryWeightInfo();
                    MessageBox.Show("新增数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    //调用写操作日志方法
                    string strLog = "规格=" + FS_SPEC
                                 + ",长度=" + FN_LENGTH
                                 + ",单支理论重量=" + FN_WEIGHT
                                 + ",单捆包装支数=" + FN_BILLETCOUNT
                                 + ",ID=" + FN_ID
                                 + ",重量负差率下限=" + FS_MINDIFFERENTRATE
                                 + ",重量负差率上限=" + FS_MAXDIFFERENTRATE
                                 + ",单捆理论重量=" + FS_BUNDLEWEIGHT
                                 + ",单捆理论重量下限=" + FS_BUNDLEMINWWEIGHT
                                 + ",单捆理论重量上限=" + FS_BUNDLEMAXWWEIGHT + ",";
                    baseinfo.ob = this.ob;
                    this.baseinfo.WriteOperationLog("BT_BCTHEORYWEIGHTINFO", strDepartMent, strUserName, "新增", strLog, "棒材理重基础信息维护", "棒材上传垛帐系统理论重量对应表", "棒材计量");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("新增数据失败：\n" + ex.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }

        /// <summary>
        /// 删除（批量删除）
        /// </summary>
        private void Delete()
        {
            try
            {
                if (ultraGrid1.Rows.Count <= 0)
                {
                    MessageBox.Show("请先执行查询操作，并选择要删除的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                ArrayList alistFN_ID = new ArrayList();
                ArrayList alistFS_SPEC = new ArrayList();
                ArrayList alistFN_LENGTH = new ArrayList();

                bool bOK = DataValidation_Delete(out alistFN_ID, out alistFS_SPEC, out alistFN_LENGTH);
                if (!bOK) return;
                if (alistFN_ID.Count == 0)
                {
                    MessageBox.Show("请您勾选需要删除的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("确定要删除勾选的记录吗？请谨慎操作！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string strBatch_FN_ID = string.Empty;
                    for (int i = 0; i < alistFN_ID.Count; i++) { strBatch_FN_ID += "'" + alistFN_ID[i].ToString() + "',"; }
                    strBatch_FN_ID = strBatch_FN_ID.Substring(0, strBatch_FN_ID.Length - 1);

                    string sql = @"delete from  bt_bctheoryweightinfo t where t.FN_ID in(" + strBatch_FN_ID + ")";
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { sql };
                    ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    if (ccp.ReturnCode == 0)
                    {
                        this.GetBCtheoryWeightInfo();
                        MessageBox.Show("删除数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        if (alistFN_ID.Count == alistFS_SPEC.Count && alistFN_ID.Count == alistFN_LENGTH.Count)
                        {
                            baseinfo.ob = this.ob;
                            for (int i = 0; i < alistFN_ID.Count; i++)
                            {
                                //调用写操作日志方法
                                string strLog = "原记录ID=" + alistFN_ID[i].ToString() + ",规格=" + alistFS_SPEC[i].ToString() + ",长度=" + alistFN_LENGTH[i].ToString() + ",";
                                this.baseinfo.WriteOperationLog("BT_BCTHEORYWEIGHTINFO", strDepartMent, strUserName, "删除", strLog, "棒材理重基础信息维护", "棒材上传垛帐系统理论重量对应表", "棒材计量");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除数据失败：\n" + ex.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    {
                        this.GetBCtheoryWeightInfo();
                        break;
                    }

                case "Add":
                    {
                        this.Add();
                        break;
                    }
                case "Update":
                    {
                        this.Update();
                        break;
                    }
                case "Delete":
                    {
                        this.Delete();
                        break;
                    }

            }

        }

        #region 访问服务端
        private DataSet CreateDataSet(DataTable dt)
        {
            DataSet ds = new DataSet();

            if (dt != null)
            {
                ds.Tables.Add(dt);
            }

            return ds;
        }
        private ArrayList ProcReturnDS(string ServerName, string MethodName, object[] obj, out string err)
        {
            try
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = ServerName;//服务端类名
                ccp.MethodName = MethodName;//上面类中的指定方法      
                ccp.ServerParams = obj;
                ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);//执行                
                err = "";
                return (ArrayList)ccp.ReturnObject;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="MethodName"></param>
        /// <param name="obj"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private DataSet SelectReturnDS(string ServerName, string MethodName, object[] obj, out string err)
        {
            try
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = ServerName;//服务端类名
                ccp.MethodName = MethodName;//上面类中的指定方法    
                ccp.ServerParams = obj;
                ccp = this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);//执行   
                DataSet ds = CreateDataSet(ccp.SourceDataTable);
                err = ccp.ReturnInfo;
                return ds;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }
        }
        #endregion              

        private void ultraGrid1_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key.Equals("CHECKED") || e.Cell.Column.Key.Equals("FN_GPYS_NUMBER") ||
                    e.Cell.Column.Key.Equals("FN_GPYS_WEIGHT") || e.Cell.Column.Key.Equals("FS_DJH"))
                {
                    this.ultraGrid1.UpdateData();
                }
            }
            catch { }
        }

        private void ultraGrid1_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key.Equals("CHECKED"))
                {
                    if (Convert.ToBoolean(e.Cell.Value))
                    {
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_NUMBER"].Value = this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GP_TOTALCOUNT"].Value;
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_JJ_WEIGHT"].Value;
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value = this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_JJ_WEIGHT_LL"].Value;

                        string strWeight = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value).Trim();

                        if (string.IsNullOrEmpty(strWeight) || strWeight == "0")
                        {
                            this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value;
                        }

                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, e.Cell.Row.Index, "FN_GPYS_NUMBER", true);
                    }
                    else
                    {
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_NUMBER"].Value = "";
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = ""; ;
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value = "";
                    }
                }
                else if (e.Cell.Column.Key.Equals("FS_DJH") || e.Cell.Column.Key.Equals("FN_GPYS_WEIGHT"))
                {
                    string strValue = Convert.ToString(e.Cell.Value).Trim();

                    if (!Convert.ToBoolean(ultraGrid1.Rows[e.Cell.Row.Index].Cells["CHECKED"].Value) && !string.IsNullOrEmpty(strValue))
                    {
                        e.Cell.Value = "";
                        return;
                    }
                }
                else if (e.Cell.Column.Key.Equals("FN_GPYS_NUMBER"))
                {
                    string strCount = Convert.ToString(e.Cell.Value).Trim();

                    if (!Convert.ToBoolean(ultraGrid1.Rows[e.Cell.Row.Index].Cells["CHECKED"].Value) && !string.IsNullOrEmpty(strCount))
                    {
                        e.Cell.Value = "";
                        return;
                    }

                    string strCountLG = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GP_TOTALCOUNT"].Value).Trim();

                    if (string.IsNullOrEmpty(strCount))
                    {
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = "";
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value = "";
                        return;
                    }

                    int iCount = 0;
                    int iCountLG = Convert.ToInt16(strCountLG);

                    if (!int.TryParse(strCount, out iCount))
                    {
                        MessageBox.Show("验收数必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountLG.ToString();
                        return;
                    }

                    if (iCount <= 0)
                    {
                        MessageBox.Show("验收数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountLG.ToString();
                        return;
                    }

                    if (iCount > iCountLG)
                    {
                        MessageBox.Show("验收数不能大于总条数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountLG.ToString();
                        return;
                    }

                    try
                    {
                        string strWeightLG = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_JJ_WEIGHT"].Value).Trim();

                        if (!string.IsNullOrEmpty(strWeightLG))
                        {
                            decimal dWeightYS = Convert.ToDecimal(strWeightLG);

                            decimal dWeight = Math.Round(iCount * dWeightYS / iCountLG, 3);

                            this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = dWeight.ToString();
                        }
                    }
                    catch { }

                    try
                    {
                        string strWeightLGLL = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_JJ_WEIGHT_LL"].Value).Trim();

                        if (!string.IsNullOrEmpty(strWeightLGLL))
                        {
                            decimal dWeightYS = Convert.ToDecimal(strWeightLGLL);

                            decimal dWeightLL = Math.Round(iCount * dWeightYS / iCountLG, 3);

                            this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value = dWeightLL.ToString();

                            string strWeightSZ = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value).Trim();

                            if (string.IsNullOrEmpty(strWeightSZ) || strWeightSZ == "0")
                            {
                                this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = dWeightLL.ToString();
                            }
                        }
                    }
                    catch { }




                }
            }
            catch { }
        }

        /// <summary>
        /// 数据行被激活事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            this.tbFS_SPEC.Text = this.ultraGrid1.ActiveRow.Cells["FS_SPEC"].Text.Trim();
            this.tbFN_LENGTH.Text = this.ultraGrid1.ActiveRow.Cells["FN_LENGTH"].Text.Trim();
            this.tbFN_WEIGHT.Text = this.ultraGrid1.ActiveRow.Cells["FN_WEIGHT"].Text.Trim();
            this.tbFN_BILLETCOUNT.Text = this.ultraGrid1.ActiveRow.Cells["FN_BILLETCOUNT"].Text.Trim();
            this.tbFN_ID.Text = this.ultraGrid1.ActiveRow.Cells["FN_ID"].Text.Trim();
            this.tbFS_MINDIFFERENTRATE.Text = this.ultraGrid1.ActiveRow.Cells["FS_MINDIFFERENTRATE"].Text.Trim();
            this.tbFS_MAXDIFFERENTRATE.Text = this.ultraGrid1.ActiveRow.Cells["FS_MAXDIFFERENTRATE"].Text.Trim();
            this.tbFS_BUNDLEWEIGHT.Text = this.ultraGrid1.ActiveRow.Cells["FS_BUNDLEWEIGHT"].Text.Trim();
            this.tbFS_BUNDLEMINWWEIGHT.Text = this.ultraGrid1.ActiveRow.Cells["FS_BUNDLEMINWWEIGHT"].Text.Trim();
            this.tbFS_BUNDLEMAXWWEIGHT.Text = this.ultraGrid1.ActiveRow.Cells["FS_BUNDLEMAXWWEIGHT"].Text.Trim();

            for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
            {
                if (this.ultraGrid1.Rows[i].Activated)
                {
                    this.ultraGrid1.Rows[i].Appearance.BackColor = Color.FromArgb(192, 255, 192);
                }
                else
                {
                    this.ultraGrid1.Rows[i].Appearance.BackColor = Color.White;
                }
            }
        }
        
    }
}