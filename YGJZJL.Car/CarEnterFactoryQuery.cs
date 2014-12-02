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
    public partial class CarEnterFactoryQuery : FrmBase
    {
        public CarEnterFactoryQuery()
        {
            InitializeComponent();
        }

        private void CarEnterFactoryQuery_Load(object sender, EventArgs e)
        {

        }

        private void ultraToolbarsManager3_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {

                        DateTime begintime = this.dtTimeBegin.Value;
                        DateTime endtime = this.dtTimeEnd.Value;
                        if (begintime > endtime)
                        {
                            MessageBox.Show("截止日期不能小于开始日期！");
                            return;

                        }
                        String begintime_str = begintime.ToString("yyyy-MM-dd 00:00:00");
                        String endtime_str = endtime.ToString("yyyy-MM-dd 23:59:59");
                        //String strSelectSql = "select t.fs_enterfacno as 进厂流水号,t.fs_plancode as 预报编号,t.fs_cardnumber 卡号,t.fs_carno 车号,t.fd_enterfactime,t.fs_enterfacplace,t.fs_enterfacchecker,";
                        //strSelectSql+="t.fs_enterfacremark,t.fd_exitfactime,t.fs_exitfacplace,t.fs_exitfacchecker,t.fs_exitfacremark from dt_enterfacrecord t ";
                        string strSelectSql = " SELECT FS_ENTERFACNO,FS_CARDNUMBER,FS_CARNO ,FS_PLANCODE ,to_char(FD_ENTERFACTIME,'YYYY-MM-DD HH24:MI:SS') FD_ENTERFACTIME,FS_ENTERFACPLACE ,FS_ENTERFACCHECKER ,to_char(FD_EXITFACTIME,'YYYY-MM-DD HH24:MI:SS') FD_EXITFACTIME ,FS_EXITFACPLACE,FS_EXITFACCHECKER,FS_ENTERFACREMARK,FS_EXITFACREMARK FROM DT_ENTERFACRECORD t ";
                        strSelectSql += "where t.fd_enterfactime >=TO_DATE('" + begintime_str + "','YYYY-MM-DD HH24:MI:SS') and t.fd_exitfactime<=TO_DATE('" + endtime_str + "','YYYY-MM-DD HH24:MI:SS') ";
                        strSelectSql += "or (t.fd_enterfactime >=TO_DATE('" + begintime_str + "','YYYY-MM-DD HH24:MI:SS') and fn_enterfacflag=1)";
                        strSelectSql += " and FS_CARNO='" + txtCarNo.Text.Replace("'", "''") + "'";
                        CoreClientParam selectccp = new CoreClientParam();
                        selectccp.ServerName = "ygjzjl.carcard";
                        selectccp.MethodName = "queryByClientSql";
                        selectccp.ServerParams = new object[] { strSelectSql };
                        dataSet1.Tables["车辆入出厂信息"].Clear();
                        selectccp.SourceDataTable = dataSet1.Tables["车辆入出厂信息"];
                        this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
                        break;
                    }
            }
        }

        private void ultraGrid2_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (ultraGrid2.Rows.Count > 0 && e.Row.Index != -1)
            {
                string strEnterFacNo = this.ultraGrid2.Rows[e.Row.Index].Cells["FS_ENTERFACNO"].Value.ToString();
                //SelectIndexGird = e.Row.Index;
                if (strEnterFacNo != "")
                {
                    getWeightInfo(strEnterFacNo);
                }
                else
                {
                    MessageBox.Show("入出厂数据不正确！");
                    return;
                }

            }
        }

        private void getWeightInfo(string strEnterFacNo)
        {
            string strSelectSql = "select FS_WEIGHTNO,FN_GROSSWEIGHT,to_char(FD_GROSSDATETIME,'YYYY-MM-DD HH24:MI:SS') FD_GROSSDATETIME,FN_TAREWEIGHT,to_char(FD_TAREDATETIME,'YYYY-MM-DD HH24:MI:SS') FD_TAREDATETIME,FN_NETWEIGHT ";
            strSelectSql += "from DT_CARWEIGHT_WEIGHT where FS_ENTERFACNO='" + strEnterFacNo + "'";
            CoreClientParam selectccp = new CoreClientParam();
            selectccp.ServerName = "ygjzjl.carcard";
            selectccp.MethodName = "queryByClientSql";
            selectccp.ServerParams = new object[] { strSelectSql };
            dataSet1.Tables["车辆称重信息"].Clear();
            selectccp.SourceDataTable = dataSet1.Tables["车辆称重信息"];
            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
        }
    }
}
