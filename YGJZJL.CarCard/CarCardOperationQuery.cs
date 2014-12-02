using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using System.Collections;

namespace YGJZJL.CarCard
{
    public partial class CarCardOperationQuery : FrmBase
    {
        public CarCardOperationQuery()
        {
            InitializeComponent();
        }

        // ultragrid过滤设置步骤：1,basic settings/feature picker/filtering/filter UI type/filter row,2,band and column settings/band[]/columns/fiteroperatordefaultvalue/fiteroperatordropdownitems
        private void ultraToolbarsManager2_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {

                        //以下为根据车证卡序列号,申请单位，操作日期查询相关信息代码并显示，日期必填，其它可选,根据登陆人所属单位车证号确认查询区间
                        String departmentcode = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment().Trim();//获得用户所在单位代码
                        //String departmentcode = "HarporTest1";//测试用
                        //String departmentcode = "HarporTest2";//测试用

                        String selectsql_cardqj = "select t.FS_CARNOBEGIN,t.FS_CARNOEND from IT_MRP t where t.FS_MEMO='" + departmentcode + "'";

                        CoreClientParam cardqj = new CoreClientParam();
                        cardqj.ServerName = "ygjzjl.carcard.CarCard";
                        cardqj.MethodName = "queryByClientSql";
                        cardqj.ServerParams = new object[] { selectsql_cardqj };
                        cardqj.SourceDataTable = this.dataSet1.Tables["车证卡区间"];
                        this.dataSet1.Tables["车证卡区间"].Clear();
                        this.ExecuteQueryToDataTable(cardqj, CoreInvokeType.Internal);
                        DataTable czkqjtable=this.dataSet1.Tables["车证卡区间"];

                        String cardnobegin="",cardnoend="";
                        int cardnobegin_int = 0, cardnoend_int = 0;
                        if (czkqjtable.Rows.Count >0) 
                        {
                            cardnobegin = czkqjtable.Rows[0]["FS_CARNOBEGIN"].ToString().Trim();
                            cardnoend = czkqjtable.Rows[0]["FS_CARNOEND"].ToString().Trim();
                        }
                        try
                        {
                            if (cardnobegin != null && !cardnobegin.Equals(""))
                            {
                                //String aa = cardnobegin.Substring(2, 5);
                                //cardnobegin_int = int.Parse(aa);
                                cardnobegin_int = int.Parse(cardnobegin);
                            }
                            if (cardnoend != null && !cardnoend.Equals(""))
                            {
                                //String aa = cardnoend.Substring(2, 5);
                                //cardnoend_int = int.Parse(aa);
                                cardnoend_int = int.Parse(cardnoend);
                            }
                        }
                        catch (Exception harporexc) {
                            cardnobegin_int = 0;
                            cardnoend_int = 0;
                        }
 
                        String xlh_str = txtCardXLH.Text.Trim();
                        String sqdw_str = txtSQDW.Text.Trim();
                        DateTime begintime = dateBegin.Value;
                        DateTime endtime = dateEnd.Value;
                        if (begintime > endtime)
                        {
                            MessageBox.Show("截止日期不能小于开始日期！");
                            return;

                        }

                        String begintime_str = begintime.ToString("yyyy-MM-dd 00:00:00");
                        String endtime_str = endtime.ToString("yyyy-MM-dd 23:59:59");

                        String selectsql_ch = "select t.FS_OPERATENO,t.FS_SEQUENCENO,t.FS_CARDNO,t.FS_APPLYDEPART,t.FS_APPLYUSER,t.FS_OPERATIONTYPE,t.FS_OPERATEDEPART,t.FS_OPERATOR,to_char(t.FD_OPERATIONTIME,'YYYY-MM-DD HH24:MI:SS') as FD_OPERATIONTIME from DT_CARDOPERATION t where t.FD_OPERATIONTIME>=TO_DATE('" + begintime_str + "','YYYY-MM-DD HH24:MI:SS') and t.FD_OPERATIONTIME<=TO_DATE('" + endtime_str + "','YYYY-MM-DD HH24:MI:SS')  and (To_number(SUBSTR(t.FS_SEQUENCENO,1,5)) between " + cardnobegin_int + " and " + cardnoend_int + ") ";                   
                        if (xlh_str != null && !xlh_str.Equals(""))
                        {
                            selectsql_ch += " and t.FS_SEQUENCENO='" + xlh_str + "'";
                        }
                        if (sqdw_str != null && !sqdw_str.Equals(""))
                        { 
                            selectsql_ch += " and t.FS_APPLYDEPART='" + sqdw_str + "'";
                        }
                        
                        selectsql_ch += " order by t.FS_SEQUENCENO";
                        CoreClientParam ccpquery = new CoreClientParam();
                        ccpquery.ServerName = "ygjzjl.carcard.CarCard";
                        ccpquery.MethodName = "queryByClientSql";
                        ccpquery.ServerParams = new object[] { selectsql_ch }; 
                        ccpquery.SourceDataTable = this.dataSet1.Tables["车证卡操作表"];
                        this.dataSet1.Tables["车证卡操作表"].Clear();
                        this.ExecuteQueryToDataTable(ccpquery, CoreInvokeType.Internal);
                        //以上为根据车证卡序列号,申请单位，操作日期查询相关信息代码并显示，日期必填，其它可选
                        break;

                    }
                
                
            }
        }

        private void CarCardOperationQuery_Load(object sender, EventArgs e)
        {
            /*暂不考虑
            //以下为查询相关信息代码并显示，分页显示
            String selectsql_ch = "select t.FS_CARDNUMBER,t.FS_SEQUENCENO,t.FS_INITDEPART,t.FS_INITUSER,to_char(t.FD_INITTIME,'YYYY-MM-DD HH24:MI:SS') as FD_INITTIME,t.FS_USERCODE,t.FS_USEDEPART,t.FS_ISBINDTOCAR,t.FS_BINDCARNO,t.FS_USEPURPOSE,t.FS_MEMO,t.FS_CARDLEVEL,t.FS_ISVALID from BT_CARDMANAGE t order by t.FS_SEQUENCENO";
            CoreClientParam ccpquery = new CoreClientParam();
            ccpquery.ServerName = "ygjzjl.carcard.CarCard";
            ccpquery.MethodName = "queryByClientSql";
            ccpquery.ServerParams = new object[] { selectsql_ch };
            ccpquery.SourceDataTable = this.dataSet1.Tables["车证卡基础表"];
            this.ExecuteQueryToDataTable(ccpquery, CoreInvokeType.Internal);
            //以上为查询相关信息代码并显示，分页显示
          */
        }



    }
}
