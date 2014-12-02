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

namespace Core.Mcms.DynamicTrack
{
    public partial class FirstDataQuery : FrmBase
    {
        public FirstDataQuery()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case"查询":
                    QueryFirstData();
                    break;

            }
        }
        private void QueryFirstData()
        {
            string beginDate = dateTimePicker1.Value.ToString("yyyyMMdd0000");
            string endData = dateTimePicker4.Value.ToString("yyyyMMdd2359");

            //DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ArrayList list = new ArrayList();
            list.Add(beginDate);
            list.Add(endData);
            if (!uce1.Checked && !uce2.Checked && !uce3.Checked)
            {
                list.Add("1");
                list.Add("2");
                list.Add("3");
            }
            else
            {
                string strStoveSeat1 = string.Empty;
                string strStoveSeat2 = string.Empty;
                string strStoveSeat3 = string.Empty;

                if (uce1.Checked)
                {
                    strStoveSeat1 = "1";
                }

                if (uce2.Checked)
                {
                    strStoveSeat2 = "2";
                }

                if (uce3.Checked)
                {
                    strStoveSeat3 = "3";
                }

                list.Add(strStoveSeat1);
                list.Add(strStoveSeat2);
                list.Add(strStoveSeat3);
            }
            dataSet1.Tables[2].Clear();
            ccp.ServerParams = new object[] { "FIRSTDATAQUERY_01.SELECT", list };
            ccp.SourceDataTable = dataSet1.Tables[2];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

           
        }

        private void ultraGrid1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }


    }
}
