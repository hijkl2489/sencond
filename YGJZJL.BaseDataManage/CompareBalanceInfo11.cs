using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using YGJZJL.PublicComponent;

namespace YGJZJL.BaseDataManage
{
    public partial class CompareBalanceInfo11 : FrmBase
    {

        string strNumber = "";
        public CompareBalanceInfo11()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            if (e.Tool.Key == "Query")
            {
                DoQuery();
            }
            if (e.Tool.Key == "Update")
            {
                DoUpdate();
                DoQuery();               
            }
            if (e.Tool.Key == "Add")
            {
                DoAdd();
                DoQuery();                
            }
            if (e.Tool.Key == "Delete")
            {
                DoDelete();
                DoQuery();
            }
            if (e.Tool.Key == "Excel")
            {
                DoExcel();
            }
        }

        private void DoQuery()
        {
            //string strBeginTime = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss')";
            //string strEndTIme = dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
            string sql = "select FS_DEVICENO,to_char(FD_COMPAREDATE,'yyyy-mm-dd hh24:mi:ss') AS FD_COMPAREDATE,FS_SPECTYPE,FS_NUMBER,FN_ALLOWDIFF,FN_WEIGHT,";
            sql += "FN_SHOWWEIGHT,FN_DIFFWEIGHT,FS_JUDGERESULT,FS_OPERATOR,FS_SHIFT,FS_TERM,FS_MEMO ";
            sql += " from DT_COMPAREBALANCE where FD_COMPAREDATE>= to_date('" + dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
            sql += " and FD_COMPAREDATE <= to_date('" + dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
            sql += " order by FD_COMPAREDATE ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        private void DoAdd()
        {
            string strDeviceNo = this.tbDeviceNo.Text.Trim();
            string strCompareDate = this.dtCompareDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strSpecType = this.tbSpecType.Text.Trim();
            string strAllowDiff = this.tbAllowDiff.Text.Trim();
            string strWeight = this.tbWeight.Text.Trim();
            string strShowWeight = this.tbShowWeight.Text.Trim();
            string strDiffWeight = this.tbDiffWeight.Text.Trim();
            string strJudgeResult = this.tbJudgeResult.Text.Trim();
            string strOperator = this.tbOperator.Text.Trim();
            string strShift = this.tbShift.Text.Trim();
            string strTerm = this.tbTerm.Text.Trim();
            string strMemo = this.tbMemo.Text.Trim();
            string strGuid = Guid.NewGuid().ToString();

            string sql  = "insert into DT_COMPAREBALANCE (FS_DEVICENO,FD_COMPAREDATE,FS_SPECTYPE,FS_NUMBER,FN_ALLOWDIFF,";
            sql += "FN_WEIGHT,FN_SHOWWEIGHT,FN_DIFFWEIGHT,FS_JUDGERESULT,FS_OPERATOR,FS_SHIFT,FS_TERM,FS_MEMO) values(";
            sql += "'" + strDeviceNo + "',to_date('" + strCompareDate + "','yyyy-MM-dd hh24:mi:ss'),'" + strSpecType + "',";
            sql += "'" + strGuid + "','" + strAllowDiff + "','" + strWeight + "','" + strShowWeight + "','" + strDiffWeight + "',";
            sql += "'" + strJudgeResult + "','" + strOperator + "','" + strShift + "','" + strTerm + "','" + strMemo + "')";

            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        private void DoExcel()
        {
            if (ultraGrid1.Rows.Count > 0)
            {
                Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, ultraGrid1);
                
            }
        }

        private void DoDelete()
        {
            if (strNumber == "")
            {
                MessageBox.Show("请先选择需要删除的记录");
                return;
            }

            string sql = "delete DT_COMPAREBALANCE where FS_NUMBER = '" + strNumber + "'";

            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

        }

        private void DoUpdate()
        {
            if (strNumber == "")
            {
                MessageBox.Show("请先选择需要修改的记录");
                return;
            }

            string strDeviceNo = this.tbDeviceNo.Text.Trim();
            string strCompareDate = this.dtCompareDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strSpecType = this.tbSpecType.Text.Trim();
            string strAllowDiff = this.tbAllowDiff.Text.Trim();
            string strWeight = this.tbWeight.Text.Trim();
            string strShowWeight =  this.tbShowWeight.Text.Trim();
            string strDiffWeight = this.tbDiffWeight.Text.Trim();
            string strJudgeResult = this.tbJudgeResult.Text.Trim();
            string strOperator = this.tbOperator.Text.Trim();
            string strShift = this.tbShift.Text.Trim();
            string strTerm = this.tbTerm.Text.Trim();
            string strMemo = this.tbMemo.Text.Trim();

            string sql = "update DT_COMPAREBALANCE set FS_DEVICENO = '" + strDeviceNo + "',FD_COMPAREDATE = to_Date('" + strCompareDate + "','yyyy-MM-dd hh24:mi:ss'),";
            sql += " FS_SPECTYPE = '" + strSpecType + "',FN_ALLOWDIFF='" + strAllowDiff + "',FN_WEIGHT = '" + strWeight + "',FN_SHOWWEIGHT = '"+strShowWeight+"',";
            sql += " FN_DIFFWEIGHT = '" + strDiffWeight + "',FS_JUDGERESULT = '" + strJudgeResult + "',FS_OPERATOR = '" + strOperator + "',FS_SHIFT = '" + strShift + "',";
            sql += " FS_TERM = '" + strTerm + "',FS_MEMO = '" + strMemo + "' where FS_NUMBER = '" + strNumber + "'";

             CoreClientParam ccp = new CoreClientParam();

             ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { sql };
                ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        private void ultraGrid1_Click(object sender, EventArgs e)
        {

            UltraGridRow ugr;
            ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null)
                return;

            this.tbDeviceNo.Text = ultraGrid1.ActiveRow.Cells["FS_DEVICENO"].Text.Trim();
            this.dtCompareDate.Value = Convert.ToDateTime(ultraGrid1.ActiveRow.Cells["FD_COMPAREDATE"].Text.Trim());
            this.tbSpecType.Text = ultraGrid1.ActiveRow.Cells["FS_SPECTYPE"].Text.Trim();
            this.strNumber = ultraGrid1.ActiveRow.Cells["FS_NUMBER"].Text.Trim();
            this.tbAllowDiff.Text = ultraGrid1.ActiveRow.Cells["FN_ALLOWDIFF"].Text.Trim();
            this.tbWeight.Text = ultraGrid1.ActiveRow.Cells["FN_WEIGHT"].Text.Trim();
            this.tbShowWeight.Text = ultraGrid1.ActiveRow.Cells["FN_SHOWWEIGHT"].Text.Trim();
            this.tbDiffWeight.Text = ultraGrid1.ActiveRow.Cells["FN_DIFFWEIGHT"].Text.Trim();
            this.tbJudgeResult.Text = ultraGrid1.ActiveRow.Cells["FS_JUDGERESULT"].Text.Trim();
            this.tbOperator.Text = ultraGrid1.ActiveRow.Cells["FS_OPERATOR"].Text.Trim();
            this.tbShift.Text = ultraGrid1.ActiveRow.Cells["FS_SHIFT"].Text.Trim();
            this.tbTerm.Text = ultraGrid1.ActiveRow.Cells["FS_TERM"].Text.Trim();
            this.tbMemo.Text = ultraGrid1.ActiveRow.Cells["FS_MEMO"].Text.Trim();
        }

        private void CompareBalanceInfo_Load(object sender, EventArgs e)
        {
            tbOperator.Text = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            tbTerm.Text = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
            tbShift.Text = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            pictureBox2.Hide();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
            this.pictureBox2.Show();
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            this.pictureBox2.Hide();
        }
    }
}
