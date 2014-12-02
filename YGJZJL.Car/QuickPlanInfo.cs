using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using YGJZJL.PublicComponent;

namespace YGJZJL.Car
{
    public partial class QuickPlanInfo : FrmBase
    {
        private string s_FS_PLANCODE; //预报编号
        private string s_FS_PLANID; //预报编号
        private string s_FS_SENDERSTORE; //发货地点
        private string s_FS_SENDER;//发货单位
        private string s_FS_MATERIALNAME; //物料名称
        private string s_FS_RECEIVERFACTORY; //收货单位
        private string s_FS_RECEIVERSTORE; //卸货地点
        private string s_FS_TRANSNO; //承运方
        private string s_FS_POINTFLOW;//流向

        public QuickPlanInfo(OpeBase op)
        {
            InitializeComponent();
            this.ob = op;
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        public void ParameData()
        {
            s_FS_PLANCODE = "";
            s_FS_PLANID = "";
            s_FS_SENDERSTORE = ""; //发货地点
            s_FS_SENDER = "";//发货单位           
            s_FS_MATERIALNAME = ""; //物料名称
            s_FS_RECEIVERFACTORY = ""; //收货单位
            s_FS_RECEIVERSTORE = ""; //卸货地点
            s_FS_TRANSNO = ""; //承运方
            s_FS_POINTFLOW = "";//流向
        }

        #region 属性
        /// <summary>
        /// 发货地点
        /// </summary>
        public string s_SENDERSTORE
        {
            get
            {
                return this.s_FS_SENDERSTORE;
            }
            set
            {
                this.s_FS_SENDERSTORE = value;
            }
         
        }
        /// <summary>
        /// 发货单位
        /// </summary>
        public string s_SENDER
        {
           
            get
            {
                return this.s_FS_SENDER;
            }
            set
            {
                this.s_FS_SENDER = value;
            }
            
        }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string s_MATERIALNAME
        {
            get
            {
                return this.s_FS_MATERIALNAME;
            }
            set
            {
                this.s_FS_MATERIALNAME = value;
            }
           
        }
     
        /// <summary>
        /// 收货单位
        /// </summary>
        public string s_RECEIVERFACTORY
        {
            get
            {
                return this.s_FS_RECEIVERFACTORY;
            }
            set
            {
                this.s_FS_RECEIVERFACTORY = value;
            }
            
        }

        /// <summary>
        /// 卸货地点
        /// </summary>
        public string s_RECEIVERSTORE
        {
            get
            {
                return this.s_FS_RECEIVERSTORE;
            }
            set
            {
                this.s_FS_RECEIVERSTORE = value;
            }
        }
        
        /// <summary>
        /// 承运方
        /// </summary>
        public string s_TRANSNO
        {
            get
            {
                return this.s_FS_TRANSNO;
            }
            set
            {
                this.s_FS_TRANSNO = value;
            }
        }

        /// <summary>
        /// 流向
        /// </summary>
        public string s_POINTFLOW
        {
            get
            {
                return this.s_FS_POINTFLOW;
            }
            set
            {
                this.s_FS_POINTFLOW = value;
            }
            
        }
        #endregion

        #region  网格显示设置
        /// <summary>
        /// 网格显示设置
        /// </summary>
        private void DataGridInit()
        {
            //行编辑器显示序号
            ultraGrid3.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
            ultraGrid3.DisplayLayout.Override.RowSelectorWidth = 28;
            ultraGrid3.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            for (int i = 0; i <= ultraGrid3.DisplayLayout.Bands[0].Columns.Count - 1; i++)
            {
                ultraGrid3.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ultraGrid3.DisplayLayout.Bands[0].Columns[i].FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Equals;
            }
            
            if (ultraGrid3.Rows.Count > 0)
            {
                ultraGrid3.ActiveRow = null;
            }
            Constant.RefreshAndAutoSize(ultraGrid3);
        }
        #endregion

        private void GuideCardNoInfo_Load(object sender, EventArgs e)
        {
            DataGridInit();
            QueryGuideCardNoInfo();
        }

        /// <summary>
        /// 查询运输计划信息
        /// </summary>
        private void QueryGuideCardNoInfo()
        {

            //string strWhere = " and A.FS_DATETIME >= to_date('" + dateBegin.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
            //strWhere += " and A.FS_DATETIME <= to_date('" + dateEnd.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";

            string strSql = "SELECT FS_PLANCODE,WL_BHTOMC(fs_material) AS fs_materialname,fhdw_bhtomc(fs_sender) as fs_sender,";
            strSql += " shdw_bhtomc(fs_receiverfactory) as fs_receiverfactory,cydw_bhtomc(fs_transno) as fs_transno,";
            strSql += " fs_senderstore,fs_receiverstore,fs_pointflow,FS_POINTID ";
            strSql += " FROM DT_WEIGHTPLAN A WHERE 1=1 And FS_STATUS='3' order by to_number(a.fs_pointid)";

            this.dataTable1.Rows.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "hgjzjl.car.CarCard";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dataTable1;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid3);

        }

        private void ultraGrid3_Click(object sender, EventArgs e)
        {
            UltraGridRow row;
            row = this.ultraGrid3.ActiveRow;
            if (row == null)
            {
                return;
            }
            ultraGrid3.ActiveRow.Selected = true;

            s_FS_PLANCODE = ultraGrid3.ActiveRow.Cells["FS_PLANCODE"].Text.Trim();
            s_FS_PLANID = ultraGrid3.ActiveRow.Cells["FS_POINTID"].Text.Trim();
            s_FS_SENDER = ultraGrid3.ActiveRow.Cells["fs_sender"].Text.Trim();
            s_FS_SENDERSTORE = ultraGrid3.ActiveRow.Cells["FS_SENDERSTORE"].Text.Trim();
            s_FS_MATERIALNAME = ultraGrid3.ActiveRow.Cells["FS_MATERIALNAME"].Text.Trim();
            s_FS_RECEIVERFACTORY = ultraGrid3.ActiveRow.Cells["fs_receiverfactory"].Text.Trim();
            s_FS_TRANSNO = ultraGrid3.ActiveRow.Cells["FS_TRANSNO"].Text.Trim();
            s_FS_POINTFLOW = ultraGrid3.ActiveRow.Cells["FS_POINTFLOW"].Text.Trim();
            s_FS_RECEIVERSTORE = ultraGrid3.ActiveRow.Cells["FS_RECEIVERSTORE"].Text.Trim();
        }

        private void btnQD_Click(object sender, EventArgs e)
        {
            if (s_FS_SENDER == null)
            {
                MessageBox.Show("请选择一条预报计划！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnQX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        QueryGuideCardNoInfo();
                        break;
                    }
            }
        }
    }
}
