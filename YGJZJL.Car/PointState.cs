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

namespace YGJZJL.Car
{
    public partial class PointState : FrmBase
    {
        public PointState()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    this.PointQuery();
                    break;
                case "Update":
                    if (DialogResult.Yes == MessageBox.Show("您确认要修改已选择磅房的信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        this.PointUpdate();
                    break;
            }
        }

        /// <summary>
        /// 计量点查询
        /// </summary>
        private void PointQuery()
        {
            string strSQL = "select 'False' XZ,t.FS_POINTCODE,t.FS_POINTNAME,decode(t.FN_POINTFLAG,0,'未接管',1,'已接管') FN_POINTFLAG,t.FS_IP";
            strSQL += " from bt_pointflag t";
            CoreClientParam ccp = new CoreClientParam();
            this.dataTable1.Rows.Clear();
            ccp.ServerName = "ygjzjl.car.CarCard";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSQL };
            ccp.SourceDataTable = dataTable1;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            try
            {

                foreach (UltraGridRow ugr in ultraGrid1.Rows)
                {
                    if (ugr.Cells["FN_POINTFLAG"].Text.ToString() == "已接管")
                    {
                        ugr.Appearance.ForeColor = Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   
        }

        /// <summary>
        /// 计量点修改
        /// </summary>
        private void PointUpdate()
        {
            string strSQL = "";
            try
            {
                string s_pointcode = this.ultraGrid1.ActiveRow.Cells["FS_POINTCODE"].Text.Trim();
                strSQL = "update bt_pointflag t set t.fn_pointflag=0,t.FS_IP = '' where t.fn_pointflag=1";
                strSQL += " and  FS_POINTCODE in ('{0}')";
                ultraGrid1.UpdateData();
                ArrayList points = new ArrayList();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid1.Rows)
                {
                    if (Convert.ToBoolean(uRow.Cells["XZ"].Value))
                    {
                        points.Add(uRow.Cells["FS_POINTCODE"].Value.ToString());
                        if (uRow.Cells["FN_POINTFLAG"].Value.ToString().Trim() != "已接管")
                        {
                            MessageBox.Show("计量点:" + uRow.Cells["FS_POINTCODE"].Value.ToString() + "未被接管");
                            return;
                        }
                    }
                }
                foreach(object point in points)
                {
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.car.CarCard";
                    ccp.MethodName = "updateByClientSql";
                    ccp.ServerParams = new object[] { string.Format(strSQL,point.ToString()) };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
                this.PointQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
