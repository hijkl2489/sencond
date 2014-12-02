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
    public partial class DataManage : FrmBase
    {
        public DataManage()
        {
            InitializeComponent();
        }

        private void DataManage_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        ///查询完整的数据
        /// </summary>
        private void ShowGridInfo()       
        {
            TimeSpan ts = new TimeSpan(qDteBegin.Value.Hour, qDteBegin.Value.Minute, qDteBegin.Value.Second);
            qDteBegin.Value = qDteBegin.Value.Add(-ts);//默认当天的0时0分0秒为开始时间
            string beginTime = qDteBegin.Value.ToString("yyyyMMddHHmmss");
            string endTime = qDteBegin.Value.AddMinutes(Convert.ToDouble(1439)).ToString("yyyyMMddHHmmss");
            string stovno = txtLh.Text.Trim();
            string potno = txtGh.Text.Trim();
          
            ArrayList list = new ArrayList();
            dataSet1.Tables[0].Clear();
            list.Add(beginTime);
            list.Add(endTime);
            list.Add(beginTime);
            list.Add(endTime);
            txtLh.Text = "";
            txtGh.Text = "";
            CoreClientParam ccp = new CoreClientParam();
            if (stovno != string.Empty&&potno!=string.Empty)
            {
                list.Add(stovno);
                list.Add(potno);
               
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "query";
                ccp.ServerParams = new object[] { "DATAMANAGE_01.SELECT", list };
                ccp.SourceDataTable = dataSet1.Tables[0];
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                return;
            }
            if(stovno==string.Empty&&potno!=string.Empty)
            {
                list.Add(potno);
                
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "query";
                ccp.ServerParams = new object[] { "DATAMANAGE_01_01.SELECT", list };
                ccp.SourceDataTable = dataSet1.Tables[0];
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                return;
            }
            if (stovno!=string.Empty&&potno == string.Empty)
            {            
                list.Add(stovno);
               
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "query";
                ccp.ServerParams = new object[] { "DATAMANAGE_01_02.SELECT", list };
                ccp.SourceDataTable = dataSet1.Tables[0];
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                return;
            } 
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "DATAMANAGE_01_03.SELECT", list };
            ccp.SourceDataTable = dataSet1.Tables[0];      
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
           
           
        }
       private void uToolBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "find":
                {
                    if (!getStrWhere())
                        break;
                    ShowGridInfo();
                    break;
                }
              
                case "ToExcel":
                {
                    Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1,ultraGrid1);
                    Constant.WaitingForm.Close();
                    break;
                }
            }
        }

        

        /// <summary>
       /// 判断开始日期不能大于结束日期
        /// </summary>
        private bool getStrWhere()
        {
            if (qDteBegin.Value > qDteEnd.Value.AddSeconds(10))
            {
                MessageBox.Show("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }

            return true;
        }

        private void txtLh_Leave(object sender, EventArgs e)
        {
            if (txtLh.Text.Trim() == "") return;
            if (txtLh.Text.Trim().Length > 20)
            {
                MessageBox.Show("炉号不能大于20位，请重新进行输入！");
                txtLh.Focus();
            }
        }

        private void txtGh_Leave(object sender, EventArgs e)
        {
            if (txtGh.Text.Trim() == "") return;
            if (txtGh.Text.Trim().Length > 20)
            {
                MessageBox.Show("罐号不能大于20位，请重新进行输入！");
                txtGh.Focus();
            }
        }

      
       
    }
}
