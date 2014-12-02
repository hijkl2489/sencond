using System;
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

namespace YGJZJL.Sap
{
    public partial class AddMaterialNo : FrmBase
    {
        public AddMaterialNo()
        {
            InitializeComponent();
        }

        private void uToolBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            { 
                case "Query":

                    Query();
                    break;
                case "UpdateAll":
                    UpdateAll();
                    break;
            }
        }

        private void Query()
        {
            string strBeginTime = this.dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strEndTime = this.dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strQuery = "select t.fs_batchno,t.fs_productno,t.fs_materialno,to_char(t.fd_starttime,'yyyy-MM-dd hh24:mi:ss') as fd_starttime"
                              + " from dt_productweightmain t where t.fs_uploadflag='1' and t.fs_materialno is null "
                              + "and (t.fd_starttime between to_date('" + strBeginTime + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + strEndTime + "','yyyy-MM-dd hh24:mi:ss')) order by t.fd_starttime desc";
            string strTmpTable, strTmpField, strTmpOrder;
            this.dataSet1.Tables[0].Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strQuery };
            ccp.SourceDataTable = dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        private void UpdateAll()
        {
            if (this.ultraGrid1.Rows.Count < 1)
            {
               
                return;
            }
            else
            {
                DataTable ds = new DataTable();
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";

                ccp.SourceDataTable = ds;

             
                for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
                {

                    string strQuery = "select t.fs_material from it_productdetail t where t.fs_productno='"+this.ultraGrid1.Rows[i].Cells["fs_productno"].Text+"'";
                    ccp.ServerParams = new object[] { strQuery };

                    ccp=this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                    if (ds.Rows.Count == 0)
                    {
                        downOrderInfo(this.ultraGrid1.Rows[i].Cells["fs_productno"].Text);
                        ccp=this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                    }
                    string strMaterialNo = ds.Rows[0][0].ToString();


                    UpdateMain(strMaterialNo, this.ultraGrid1.Rows[i].Cells["fs_productno"].Text);
                    Query();
                }
            }
        }

        private void AddMaterialNo_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            this.dateTimePicker2.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

        }

        /// <summary>
        /// 根据订单号从SAP下载订单信息
        /// </summary>
        private void downOrderInfo(string sDdh)
        {
            DataTable dtOrder = new DataTable();
            CoreClientParam ccpProductNo = new CoreClientParam();
            ccpProductNo.ServerName = "ygjzjl.base.SapOperation";
            ccpProductNo.MethodName = "queryProductNo";
            ccpProductNo.ServerParams = new object[] { sDdh };
            dtOrder.Clear();
            ccpProductNo.SourceDataTable = dtOrder;
            this.ExecuteQueryToDataTable(ccpProductNo, CoreInvokeType.Internal);

         
        }

        private void UpdateMain(string MaterialNo,string ProductNo)
        {
            string strUpdate = "update dt_productweightmain t set t.fs_materialno='" + MaterialNo + "' where t.fs_productno='" + ProductNo + "' and t.fs_uploadflag='1' and t.fs_materialno is null";
            CoreClientParam ccp1 = new CoreClientParam();
            ccp1.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp1.MethodName = "ExcuteNonQuery";
            ccp1.ServerParams = new object[] { strUpdate };
            ccp1=this.ExecuteNonQuery(ccp1, CoreInvokeType.Internal);
            if (ccp1.ReturnCode == 0)
            {

            }
        }
    }
}
