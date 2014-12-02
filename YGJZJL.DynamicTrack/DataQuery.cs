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
using YGJZJL.PublicComponent;
using Infragistics.Win.UltraWinGrid;

namespace YGJZJL.DynamicTrack
{
    public partial class DataQuery : FrmBase
    {
        public DataQuery()
        {
            InitializeComponent();
        }

        private void DataQuery_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 点击工具栏按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uToolBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    QueryData();
                    break;
                case "Export":
                    Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, ultraGrid1);
                    //Constant.WaitingForm.Close();
                    break;
            }
        }


        /// <summary>
        /// 查询
        /// </summary>
        private void QueryData()
        {
            string strBeginDate = this.dtTimeBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            string strEndDate = this.dtTimeEnd.Value.ToString("yyyy-MM-dd 23:59:59");


            if (dtTimeBegin.Value > dtTimeEnd.Value)
            {
                dtTimeBegin.Value = dtTimeEnd.Value;
                MessageBox.Show("计量开始时间不能大于结束时间！");
                return;
            }

            string strStoveNo = txtStoveNo.Text.Trim().Replace("'", "''");
            ArrayList list = new ArrayList();
            dataSet1.Tables[1].Clear();
            dataSet1.Tables[0].Clear();
            list.Add(strBeginDate);
            list.Add(strEndDate);

            if (!cbStoveSeat1.Checked && !cbStoveSeat2.Checked && !cbStoveSeat3.Checked)
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

                if (cbStoveSeat1.Checked)
                {
                    strStoveSeat1 = "1";
                }

                if (cbStoveSeat2.Checked)
                {
                    strStoveSeat2 = "2";
                }

                if (cbStoveSeat3.Checked)
                {
                    strStoveSeat3 = "3";
                }

                list.Add(strStoveSeat1);
                list.Add(strStoveSeat2);
                list.Add(strStoveSeat3);
            }

            list.Add(strStoveNo);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "DATAQUERY_01.SELECT", list };
            ccp.SourceDataTable = dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            string sql = @"SELECT FN_TAREWEIGHT,FN_GROSSWEIGHT,FN_NETWEIGHT,
		                     to_char(to_date(FS_GROSSTIME,'yyyymmddhh24miss'),'yyyy-mm-dd hh24:mi:ss')FS_GROSSTIME,
		                     to_char(to_date(FD_TARETIME,'yyyymmddhh24miss'),'yyyy-mm-dd hh24:mi:ss')FD_TARETIME
		                    ,FS_POTNO,FS_STOVENO,round(FN_TSPEED,3) FN_TSPEED,round(FN_GSPEED,3) FN_GSPEED
		                    ,FS_RECEIVER,FS_GROSSPERSON,FS_TAREPERSON
		                    ,to_char(FD_RECEIVETIME,'yyyy-MM-dd hh24:mi:ss') FD_RECEIVETIME
		                    ,decode(FS_RECEIVEFACTORY,'1','炼钢','2','铸铁','炼钢') FS_RECEIVEFACTORY
		                    FROM DT_IRONWEIGHT 
		                    WHERE FS_STOVENO in (select FS_STOVENO FROM DT_IRONWEIGHT 
		                    WHERE FN_GROSSWEIGHTIME BETWEEN ? and ? 
		                    and FS_STOVESEATNO in (?,?,?) and
		                    FS_STOVENO||'&' like '%'||?||'%' and (FN_ISVALID<>0 or FN_ISVALID is null)) and FS_STOVENO is not null  order by FS_STOVESEATNO,FS_STOVENO";
            dataSet1.Tables[1].Clear();
            ccp.MethodName = "queryBySql";
            ccp.ServerParams = new object[] { sql, list };
            ccp.SourceDataTable = dataSet1.Tables[1];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }
    }
}