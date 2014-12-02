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

namespace YGJZJL.DynamicTrack
{
    public partial class DataRenew : FrmBase
    {
        private int grid1Index = -1;
        public DataRenew(OpeBase op)
        {
            InitializeComponent();
            this.ob = op;
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case"查询":
                    QueryWeightData();
                    break;
                case"恢复":
                    RenewWeightData();
                    break;
           
            }
        }
        #region 查询已获取重量数据
        private void QueryWeightData()
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                dateTimePicker1.Value = dateTimePicker2.Value;
                MessageBox.Show("计量开始时间不能大于结束时间！");
                return;
            }
            dataSet1.Tables[0].Clear();
            ArrayList list = new ArrayList();
            list.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00"));
            list.Add(dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            //CoreClientParam ccp = new CoreClientParam();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHTMATCH_01_01.SELECT", list };
            ccp.SourceDataTable = dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

      
        }
        #endregion

        private void RenewWeightData()
        {
            if (MessageBox.Show("是否确定恢复采集的称重记录？", "玉钢MCMS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ArrayList list = new ArrayList();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow ugr in ultraGrid1.Rows)
                {
                    if (Convert.ToBoolean(ugr.Cells["CHK"].Value))
                    {
                        list.Add(ugr.Cells["FS_WEIGHTNO"].Value.ToString());
                    }
                }
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.dynTrack.WeightMatch";
                ccp.MethodName = "RenewFirstData";
                ccp.ServerParams = new object[] { list, UserInfo.GetUserID() };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.QueryWeightData();
            }
        }

        private void ultraGrid1_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "CHK")
            {
                if (Convert.ToBoolean(e.Cell.Value)==false)
                {
                    e.Cell.Value = "True";
                }
            }

        }

    }
}
