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
    public partial class MaintenanceCorrentPoint : FrmBase
    {

        string strTemp = "";
        public MaintenanceCorrentPoint()
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
            string sql = "select FS_POINTCODE,FS_POINTNAME,FN_WEIGHT,FN_DEVIATION,FS_DEVICENO,FS_SPECTYPE,FS_DEVICENUMBER";
            sql += " from BT_CORRENTION_POINT ";
          
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
            string strSpecType = this.tbSpecType.Text.Trim();
            string strAllowDiff = this.tbAllowDiff.Text.Trim();
            string strWeight = this.tbWeight.Text.Trim();
            string strPointCode = this.tbPointCode.Text.Trim();
            string strPoint = this.tbPoint.Text.Trim();
            string strDeviceNumber = this.tbDeviceNumber.Text.Trim();

            string sql = "insert into BT_CORRENTION_POINT (FS_DEVICENO,FS_SPECTYPE,FN_DEVIATION,";
            sql += "FN_WEIGHT,FS_POINTCODE,FS_POINTNAME,FS_DEVICENUMBER) values(";
            sql += "'" + strDeviceNo + "','" + strSpecType + "',";
            sql += strAllowDiff + "," + strWeight + ",'" + strPointCode + "','" + strPoint + "','"+strDeviceNumber+"' )";
        

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
            if (strTemp == "")
            {
                MessageBox.Show("请先选择需要删除的记录");
                return;
            }

            string sql = "delete BT_CORRENTION_POINT where FS_POINTCODE = '" + strTemp + "'";

            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

        }

        private void DoUpdate()
        {
            if (strTemp == "")
            {
                MessageBox.Show("请先选择需要修改的记录");
                return;
            }

            string strDeviceNo = this.tbDeviceNo.Text.Trim();
            string strSpecType = this.tbSpecType.Text.Trim();
            string strAllowDiff = this.tbAllowDiff.Text.Trim();
            string strWeight = this.tbWeight.Text.Trim();
            string strPointCode = this.tbPointCode.Text.Trim();
            string strPoint = this.tbPoint.Text.Trim();
            string strDeviceNumber = this.tbDeviceNumber.Text.Trim();


            string sql = "update BT_CORRENTION_POINT set FS_DEVICENO = '" + strDeviceNo + "',";
            sql += " FS_SPECTYPE = '" + strSpecType + "',FN_DEVIATION='" + strAllowDiff + "',FN_WEIGHT = '" + strWeight + "',";
            sql += " FS_POINTCODE = '" + strPointCode + "',FS_POINTNAME = '" + strPoint + "' ,FS_DEVICENUMBER = '" + strDeviceNumber+"'";
            sql += " where FS_POINTCODE = '" + strTemp + "'";

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
            this.strTemp = ultraGrid1.ActiveRow.Cells["FS_POINTCODE"].Text.Trim();
            this.tbDeviceNo.Text = ultraGrid1.ActiveRow.Cells["FS_DEVICENO"].Text.Trim();
            this.tbSpecType.Text = ultraGrid1.ActiveRow.Cells["FS_SPECTYPE"].Text.Trim();
            this.tbAllowDiff.Text = ultraGrid1.ActiveRow.Cells["FN_DEVIATION"].Text.Trim();
            this.tbWeight.Text = ultraGrid1.ActiveRow.Cells["FN_WEIGHT"].Text.Trim();
            this.tbPointCode.Text = ultraGrid1.ActiveRow.Cells["FS_POINTCODE"].Text.Trim();
            this.tbPoint.Text = ultraGrid1.ActiveRow.Cells["FS_POINTNAME"].Text.Trim();
            this.tbDeviceNumber.Text = ultraGrid1.ActiveRow.Cells["FS_DEVICENUMBER"].Text.Trim();
 
        }

        private void MaintenanceCorrentPoint_Load(object sender, EventArgs e)
        {
            //tbOperator.Text = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            //tbTerm.Text = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
            //tbShift.Text = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            DoQuery();
        }


    }
}
