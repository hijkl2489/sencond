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


namespace YGJZJL.BaseDataManage
{
    public partial class PointBaseInfo : FrmBase
    {
        public PointBaseInfo()
        {
            InitializeComponent();
        }

        #region Toolbar按钮事件

        public override void ToolBar_Click(object sender, string ToolbarKey)
        {
            base.ToolBar_Click(sender, ToolbarKey);

            switch (ToolbarKey)
            {
                case "Query":
                    this.DoQuery();
                   
                    break;
                case "Add":
                    this.DoAdd();
                    break;
                case "Update":
                    this.DoUpdate();
                    break;
                case "Delete":
                    this.DoDelete();
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void DoQuery()
        {
            string str = "";

            
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PointBaseInfo";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { str };
            this.dataSet1.Tables[0].Clear();
            ccp.SourceDataTable = this.dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

           Constant.RefreshAndAutoSize(ultraGrid1);
          
        }

        private void DoAdd()
        {
            if (DoCheck())
            {
               
                string FS_POINTCODE = tbPointCode.Text.Trim();
                string FS_POINTNAME = tbPointName.Text.Trim();
                string FS_PointDepart = cbDepart.SelectedValue.ToString().Trim();
                string FS_POINTTYPE = cbPondType.SelectedValue.ToString().Trim();

                string Fs_Viedoip = tbVideoIP.Text.Trim();
                string Fs_Viedoport = tbVideoPort.Text.Trim();
                string Fs_Viedouser = tbVideoUser.Text.Trim();
                string Fs_Viedopwd = tbVideoPwd.Text.Trim();
                
                string Fs_Metertype = tbMeterType.Text.Trim();
                string Fs_Meterpara = tbMeterPara.Text.Trim();
                string Fs_Moxaip = tbMoxaIP.Text.Trim();
                string Fs_Moxaport = tbMoxaPort.Text.Trim();
                
                string Fs_Rtuip = tbRtuIP.Text.Trim();
                string Fs_Rtuport = tbRtuPort.Text.Trim();
                string Fs_Printerip = tbPrinterIP.Text.Trim();
                string Fs_Printername = tbPrinterName.Text.Trim();

                string Fs_Printtypecode = cbPrinterType.SelectedValue.ToString().Trim();
                string Fs_Ledip = tbLedIP.Text.Trim();
                string Fs_Ledport = tbLedPort.Text.Trim();

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.PointBaseInfo";
                ccp.MethodName = "add";
                ccp.ServerParams = new object[] { FS_POINTCODE, FS_POINTNAME, FS_PointDepart, FS_POINTTYPE, Fs_Viedoip, Fs_Viedoport, Fs_Viedouser, Fs_Viedopwd, Fs_Metertype, Fs_Meterpara, Fs_Moxaip, Fs_Moxaport, Fs_Rtuip, Fs_Rtuport, Fs_Printerip, Fs_Printername, Fs_Printtypecode, Fs_Ledip, Fs_Ledport };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.DoClear();
                this.DoQuery();
            }
        }

        private void DoUpdate()
        {

            if (DoCheck())
            {
                string FS_OldPointCode = "";

                string FS_POINTCODE = tbPointCode.Text.Trim();
                string FS_POINTNAME = tbPointName.Text.Trim();
                string FS_PointDepart = cbDepart.SelectedValue.ToString().Trim();
                string FS_POINTTYPE = cbPondType.SelectedValue.ToString().Trim();

                string Fs_Viedoip = tbVideoIP.Text.Trim();
                string Fs_Viedoport = tbVideoPort.Text.Trim();
                string Fs_Viedouser = tbVideoUser.Text.Trim();
                string Fs_Viedopwd = tbVideoPwd.Text.Trim();

                string Fs_Metertype = tbMeterType.Text.Trim();
                string Fs_Meterpara = tbMeterPara.Text.Trim();
                string Fs_Moxaip = tbMoxaIP.Text.Trim();
                string Fs_Moxaport = tbMoxaPort.Text.Trim();

                string Fs_Rtuip = tbRtuIP.Text.Trim();
                string Fs_Rtuport = tbRtuPort.Text.Trim();
                string Fs_Printerip = tbPrinterIP.Text.Trim();
                string Fs_Printername = tbPrinterName.Text.Trim();

                string Fs_Printtypecode = cbPrinterType.SelectedValue.ToString().Trim();
                string Fs_Ledip = tbLedIP.Text.Trim();
                string Fs_Ledport = tbLedPort.Text.Trim();

                string Fs_LcdPara = tb_LcdPara.Text.Trim();
                string Fs_ReaderPara = tb_ReaderPara.Text.Trim();

                UltraGridRow ugr = this.ultraGrid1.ActiveRow;
                if (ugr == null)
                    FS_OldPointCode = FS_POINTCODE;
                else
                    FS_OldPointCode = ugr.Cells["FS_POINTCODE"].Text.Trim();


                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.PointBaseInfo";
                ccp.MethodName = "update";
                ccp.ServerParams = new object[] { FS_OldPointCode, FS_POINTCODE, FS_POINTNAME, FS_PointDepart, FS_POINTTYPE, Fs_Viedoip, Fs_Viedoport, Fs_Viedouser, Fs_Viedopwd, Fs_Metertype, Fs_Meterpara, Fs_Moxaip, Fs_Moxaport, Fs_Rtuip, Fs_Rtuport, Fs_Printerip, Fs_Printername, Fs_Printtypecode, Fs_Ledip, Fs_Ledport, Fs_LcdPara, Fs_ReaderPara };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.DoClear();
                this.DoQuery();
            }
        }

        private void DoDelete()
        {
            if (tbPointCode.Text.Trim().Length <= 0)
            {
                MessageBox.Show("计量点代码不能为空！");
                tbPointCode.Focus();
                return;
            }
            string FS_POINTCODE = tbPointCode.Text.Trim();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PointBaseInfo";
            ccp.MethodName = "delete";
            ccp.ServerParams = new object[] { FS_POINTCODE };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.DoClear();
            this.DoQuery();
        }

        private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        {

            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;


            tbPointCode.Text = ugr.Cells["FS_POINTCODE"].Text.Trim();
            tbPointName.Text = ugr.Cells["FS_POINTNAME"].Text.Trim();
            cbDepart.Text = ugr.Cells["FS_Memo"].Text.Trim();
            cbPondType.Text = ugr.Cells["FS_PondTypeName"].Text.Trim();

            tbVideoIP.Text = ugr.Cells["FS_VIEDOIP"].Text.Trim();
            tbVideoPort.Text = ugr.Cells["FS_VIEDOPort"].Text.Trim();
            tbVideoUser.Text = ugr.Cells["FS_VIEDOUser"].Text.Trim();
            tbVideoPwd.Text = ugr.Cells["FS_VIEDOPwd"].Text.Trim();

            tbMeterType.Text = ugr.Cells["FS_METERTYPE"].Text.Trim();
            tbMeterPara.Text = ugr.Cells["FS_METERPARA"].Text.Trim();
            tbMoxaIP.Text = ugr.Cells["FS_MOXAIP"].Text.Trim();
            tbMoxaPort.Text = ugr.Cells["FS_MOXAPORT"].Text.Trim();


            tbRtuIP.Text = ugr.Cells["FS_RTUIP"].Text.Trim();
            tbRtuPort.Text = ugr.Cells["FS_RTUPORT"].Text.Trim();
            tbLedIP.Text = ugr.Cells["FS_LEDIP"].Text.Trim();
            tbLedPort.Text = ugr.Cells["FS_LEDPORT"].Text.Trim();

            tbPrinterIP.Text = ugr.Cells["FS_PRINTERIP"].Text.Trim();
            tbPrinterName.Text = ugr.Cells["FS_PRINTERNAME"].Text.Trim();
            cbPrinterType.Text = ugr.Cells["FS_PRINTTYPECODE"].Text.Trim();
            tb_LcdPara.Text = ugr.Cells["FS_DISPLAYPARA"].Text.Trim();
            tb_ReaderPara.Text = ugr.Cells["FS_READERPARA"].Text.Trim();
           // tbPrinterPaper.Text = ugr.Cells["FN_PaperNUM"].Text.Trim();
            //tbPrinterInk.Text = ugr.Cells["FN_INKNUM"].Text.Trim();

            //getPrintDetail(ugr.Cells["FS_PRINTTYPECODE"].Text.Trim());
        }

        private void DoClear()
        {
           
        }

        private bool DoCheck()
        {
            bool isCheck = true;
           

            if (tbPointCode.Text.Trim().Length <= 0)
            {
                MessageBox.Show("计量点代码不能为空！");
                isCheck = false;
                tbPointCode.Focus();
            }

           
            return isCheck;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void ultraExpandableGroupBox3_ExpandedStateChanging(object sender, CancelEventArgs e)
        {

        }

        private void PointBaseInfo_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(UserInfo.GetDepartment() + "---" + UserInfo.GetDeptid());
            getDepart();
            getPond();
            getPrint();
        }

        private void getDepart()
        {          


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PointBaseInfo";
            ccp.MethodName = "queryDepart";
            ccp.ServerParams = new object[] {  };
            
            ccp.SourceDataTable = this.dataSet2.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            DataRow newDr = dataSet2.Tables[0].NewRow();

            dataSet2.Tables[0].Rows.InsertAt(newDr, 0);
            //newDr["FS_MRP"] = "";
            //newDr["FS_MEMO"] = "";
            //dataSet2.Tables[0].Rows.Add(newDr);
          
            dataSet2.Tables[0].DefaultView.Sort = "FS_MRP";
            cbDepart.DataSource = dataSet2.Tables[0];
            cbDepart.DisplayMember = "FS_MEMO";
            cbDepart.ValueMember = "FS_MRP";


        }

        private void getPond()
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PointBaseInfo";
            ccp.MethodName = "queryPond";
            ccp.ServerParams = new object[] { };

            ccp.SourceDataTable = this.dataSet3.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            DataRow newDr = dataSet3.Tables[0].NewRow();
            dataSet3.Tables[0].Rows.InsertAt(newDr, 0);
            //newDr["FS_PONDTYPENO"] = "";
            //newDr["FS_PONDTYPENAME"] = "";
            //dataSet3.Tables[0].Rows.Add(newDr);

            dataSet3.Tables[0].DefaultView.Sort = "FS_PONDTYPENO";
            cbPondType.DataSource = dataSet3.Tables[0];
            cbPondType.DisplayMember = "FS_PONDTYPENAME";
            cbPondType.ValueMember = "FS_PONDTYPENO";

        }

        private void getPrint()
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PointBaseInfo";
            ccp.MethodName = "queryPrint";
            ccp.ServerParams = new object[] { "" };

            ccp.SourceDataTable = this.dataSet4.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            DataRow newDr = dataSet4.Tables[0].NewRow();
            //newDr["FS_PRINTTYPECODE"] = "";
            
            //dataSet4.Tables[0].Rows.Add(newDr);
            dataSet4.Tables[0].Rows.InsertAt(newDr, 0);

            dataSet4.Tables[0].DefaultView.Sort = "FS_PRINTTYPECODE";
           
            cbPrinterType.DataSource = dataSet4.Tables[0];
            cbPrinterType.DisplayMember = "FS_PRINTTYPECODE";
            cbPrinterType.ValueMember = "FS_PRINTTYPECODE";
            
            

        }

        private void getPrintDetail(string p_PrintType)
        {
            if (cbPrinterType.Text.Trim().Contains("System"))
                return;

            if (p_PrintType.Length > 0)
            {
                string str = "FS_PRINTTYPECODE='" + p_PrintType + "'";
                DataRow[] drs = dataSet4.Tables[0].Select(str);

                tbPrinterPaper.Text = drs[0]["FN_PAPERNUM"].ToString();
                tbPrinterInk.Text = drs[0]["FN_INKNUM"].ToString();
            }
            else
            {
                tbPrinterPaper.Text = "";
                tbPrinterInk.Text = "";
            }

            
        
        }

        private void cbPrinterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbPrinterType_TextChanged(object sender, EventArgs e)
        {
            //getPrintDetail();
        }

        private void cbPrinterType_SelectedValueChanged(object sender, EventArgs e)
        {
            //getPrintDetail(); 
        }

        private void cbPrinterType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string p_PrintType = cbPrinterType.Text.Trim();

            getPrintDetail(p_PrintType);
        }

        private void ultraGrid1_ClickCellButton(object sender, CellEventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;

            MessageBox.Show(ugr.Cells["FS_PointCode"].Text.Trim());
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    this.DoQuery();
                    break;
                case "Add":
                    this.DoAdd();
                    break;
                case "Update":
                    this.DoUpdate();
                    break;
                case "Delete":
                    this.DoDelete();
                    break;
                default:
                    break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_Lcdpara_TextChanged(object sender, EventArgs e)
        {

        }

        private void ultraGroupBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
