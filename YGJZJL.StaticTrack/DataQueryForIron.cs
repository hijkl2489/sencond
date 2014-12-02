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

namespace YGJZJL.StaticTrack
{
    public partial class DataQueryForIron : FrmBase
    {
        public DataQueryForIron()
        {
            InitializeComponent();
        }

        private void DataQuery_Load(object sender, EventArgs e)
        {

        }

        string PointID = "K38";

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
            string strStoveSeat1 = string.Empty;
            string strStoveSeat2 = string.Empty;
            string strStoveSeat3 = string.Empty;
            if (!cbStoveSeat1.Checked && !cbStoveSeat2.Checked && !cbStoveSeat3.Checked)
            {
                strStoveSeat1 = "1";
                strStoveSeat2 = "2";
                strStoveSeat3 = "3";
            }
            else
            {


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






            string sql = " select *  from (select case when FS_STOVESEATNO is not null and FS_STOVENO is null then "
                  + "FS_STOVESEATNO || '#高炉小计' when FS_STOVESEATNO is null and FS_STOVENO is null then '合计'"
                 + "else FS_STOVESEATNO end FS_STOVESEATNO, FS_STOVENO, FS_RECEIVEFACTORY, sum(FS_POTNO) FS_POTNO,"
              + " sum(FN_TAREWEIGHT) FN_TAREWEIGHT, sum(FN_GROSSWEIGHT) FN_GROSSWEIGHT, sum(FN_NETWEIGHT) FN_NETWEIGHT "
          + "from (SELECT distinct FS_STOVESEATNO,FS_STOVENO,decode(fs_weighttype,'1','炼钢','2','铸铁','炼钢') FS_RECEIVEFACTORY,"
            + "sum(FN_TAREWEIGHT) over(partition by FS_STOVESEATNO, FS_STOVENO, fs_weighttype) FN_TAREWEIGHT,"
         + "sum(FN_GROSSWEIGHT) over(partition by FS_STOVESEATNO, FS_STOVENO, fs_weighttype) FN_GROSSWEIGHT,"
         + " sum(FN_NETWEIGHT) over(partition by FS_STOVESEATNO, FS_STOVENO, fs_weighttype) FN_NETWEIGHT,"
         + "count(FS_POTNO) over(partition by FS_STOVESEATNO, FS_STOVENO, fs_weighttype) FS_POTNO "
         + " FROM dt_statictrackweight_weight t WHERE t.FS_GROSSPOINT='K38' AND  fd_grosstime BETWEEN to_date('" + strBeginDate + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + strEndDate + "','yyyy-MM-dd hh24:mi:ss') "
         + " and FS_STOVESEATNO in ('" + strStoveSeat1 + "','" + strStoveSeat2 + "','" + strStoveSeat3 + "')"
       + " and FS_STOVENO  like '%" + strStoveNo + "%' )"
        + "group by rollup(FS_STOVESEATNO, FS_STOVENO),"
        + "cube(FS_RECEIVEFACTORY) order by FS_STOVESEATNO, FS_STOVENO, FS_RECEIVEFACTORY)"
        + " where not (FS_STOVESEATNO is not null and FS_STOVENO is not null and FS_RECEIVEFACTORY is null)";

  //          string sql = "select decode(t.fs_weighttype,'1','炼钢','2','铸铁','炼钢')fs_receivefactory,"
  //     + "count(t.fs_potno) fs_potno,"
  //     + "sum(t.fn_grossweight) fn_grossweight,"
  //     + "sum(t.fn_tareweight) fn_tareweight,"
  //     + "sum(t.fn_netweight) fn_netweight,"
  //     + "t.fs_stoveno,"
  //     + "t.fs_stoveseatno "
  //+ "from dt_statictrackweight_weight t "
  //+ "group by t.fs_weighttype,t.fs_stoveno,t.fs_stoveseatno "
  // +"  having  t.fs_stoveno in (select fs_stoveno from dt_statictrackweight_weight t where t.fs_grosspoint = 'K38' and t.fd_grosstime between to_date('"+strBeginDate+"', 'yyyy-MM-dd hh24:mi:ss') and to_date('"+strEndDate+"', 'yyyy-MM-dd hh24:mi:ss')"
  // +"and  FS_STOVESEATNO in ('" + strStoveSeat1 + "','" + strStoveSeat2 + "','" + strStoveSeat3 + "'))";
 
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            dataSet1.Tables[3].Clear();
            ccp.SourceDataTable = dataSet1.Tables[3];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);


           string  sql1 = @"SELECT FN_TAREWEIGHT,FN_GROSSWEIGHT,FN_NETWEIGHT,
		                     to_char(FD_GROSSTIME,'yyyy-mm-dd hh24:mi:ss')FS_GROSSTIME,
		                     to_char(FD_TARETIME,'yyyy-mm-dd hh24:mi:ss')FD_TARETIME
		                    ,FS_POTNO,FS_STOVENO
		                    ,FS_RECEIVER,FS_GROSSPERSON,FS_TAREPERSON
		                    ,to_char(FD_RECEIVETIME,'yyyy-MM-dd hh24:mi:ss') FD_RECEIVETIME
		                    ,decode(FS_WEIGHTTYPE,'1','炼钢','2','铸铁','炼钢') FS_RECEIVEFACTORY 
		                    FROM dt_statictrackweight_weight t
		                    WHERE t.FS_STOVENO in (select FS_STOVENO FROM dt_statictrackweight_weight 
		                    WHERE fd_grosstime BETWEEN to_date('" + strBeginDate + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + strEndDate + "','yyyy-MM-dd hh24:mi:ss') "
                            + " and FS_STOVESEATNO in ('" + strStoveSeat1 + "','" + strStoveSeat2 + "','" + strStoveSeat3 + "') and"
                            + " FS_STOVENO  like '%" + strStoveNo + "%' ) and t.FS_STOVENO is not null AND t.FS_GROSSPOINT='" + PointID + "' order by t.FS_STOVESEATNO,t.FS_STOVENO";


//            string sql = @"SELECT FN_TAREWEIGHT,FN_GROSSWEIGHT,FN_NETWEIGHT,
//		                     to_char(fd_grosstime,'yyyy-mm-dd hh24:mi:ss')FS_GROSSTIME,
//		                     to_char(FD_TARETIME,'yyyy-mm-dd hh24:mi:ss')FD_TARETIME
//		                    ,FS_POTNO,FS_STOVENO
//		                    ,FS_RECEIVER,FS_GROSSPERSON,FS_TAREPERSON
//		                    ,to_char(FD_RECEIVETIME,'yyyy-MM-dd hh24:mi:ss') FD_RECEIVETIME
//		                    ,decode(fs_weighttype,'1','炼钢','2','铸铁','炼钢') FS_RECEIVEFACTORY
//		                    FROM DT_STATICTRACKWEIGHT_WEIGHT t
//		                    WHERE FS_STOVENO in (select FS_STOVENO FROM DT_STATICTRACKWEIGHT_WEIGHT t
//		                    WHERE fd_grosstime BETWEEN ? and ? 
//		                    and FS_STOVESEATNO in (?,?,?) and
//		                    FS_STOVENO||'&' like '%'||?||'%' ) and FS_STOVENO is not null  and t.FS_GROSSPOINT='K38' order by FS_STOVESEATNO,FS_STOVENO";
           
//            dataSet1.Tables[1].Clear();
//           ccp.MethodName = "queryBySql";
//           ccp.ServerParams = new object[] { sql, list };
           ccp.ServerParams = new object[] { sql1 };
           dataSet1.Tables[2].Clear();
            ccp.SourceDataTable = dataSet1.Tables[2];
           this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        }

        private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void ultraGrid1_InitializeLayout_1(object sender, InitializeLayoutEventArgs e)
        {

        }
    }
}