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
    public partial class MoreBaseInfo : FrmBase
    {
        private string m_parentForm = ""; //窗体参数,看是哪个窗体调用这个窗体
        private string m_DataType = "";  //数据类型 物料;收货单位;发货单位;承运单位

        private DataTable m_MaterialTable = new DataTable();//物料内存表
        private DataTable m_TempMaterialTable = new DataTable(); //助记码筛选物料内存表
        private DataTable m_ReceiverTable = new DataTable();//收货单位内存表
        private DataTable m_TempReceiverTable = new DataTable(); //助记码筛选收货单位内存表
        private DataTable m_SenderTable = new DataTable();//发货单位内存表
        private DataTable m_TempSenderTable = new DataTable(); //助记码筛选发货单位内存表
        private DataTable m_ProviderTable = new DataTable();//供应单位内存表
        private DataTable m_TempProvierTable = new DataTable(); //助记码筛选供应单位内存表
        private DataTable m_TransTable = new DataTable();//承运单位内存表
        private DataTable m_TempTransTable = new DataTable(); //助记码筛选承运单位内存表


        public MoreBaseInfo(Form parentForm, string DataType, OpeBase op)
        {
            InitializeComponent();
            this.ob = op;
            m_DataType = DataType.Trim();
            m_parentForm = parentForm.Tag.ToString();
        }

        private void MoreBaseInfo_Load(object sender, EventArgs e)
        {
            switch (m_DataType)
            {
                case "Material":
                    this.Text = "物料信息选择";
                    this.label1.Text = "物料名称";
                    this.label2.Text = "物料代码";
                    BuildMaterialTable();
                    QueryMaterial();
                    BandMaterail("");
                    break;
                case "Receiver":
                    this.Text = "收货单位选择";
                    this.label1.Text = "收货单位";
                    this.label2.Text = "单位代码";
                    BuildReceiverTable();
                    QueryReceiver();
                    BandReceiver("");
                    break;
                case "Sender":
                    this.Text = "发货单位选择";
                    this.label1.Text = "发货单位";
                    this.label2.Text = "单位代码";
                    BuildSenderTable();
                    QuerySender();
                    BandSender("");
                    break;
                case "Provider":
                    this.Text = "供应单位选择";
                    this.label1.Text = "供应单位";
                    this.label2.Text = "单位代码";
                    BuildProviderTable();
                    QueryProvider();
                    BandProvider("");
                    break;
                case "Trans":
                    this.Text = "承运单位选择";
                    this.label1.Text = "承运单位";
                    this.label2.Text = "单位代码";
                    BuildTransTable();
                    QueryTrans();
                    BandTrans("");
                    break;
                default:
                    break;
            }
        }

        //查询物料表
        private void QueryMaterial()
        {
            string strSql = "select FS_MATERIALNAME, FS_WL, FS_HELPCODE ";
            strSql += " from It_Material ";
            strSql += "  order by FS_WL";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = m_MaterialTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //查询收货单位表
        private void QueryReceiver()
        {
            string strSql = "select FS_Memo, FS_SH, FS_HELPCODE ";
            strSql += " from It_Store ";
            strSql += "  order by FS_SH";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_ReceiverTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //查询发货单位表
        private void QuerySender()
        {
            string strSql = "select FS_SUPPLIERNAME, FS_GY, FS_HELPCODE ";
            strSql += " from IT_SUPPLIER ";
            strSql += "  order by FS_GY";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SenderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //查询供应单位表
        private void QueryProvider()
        {
            string strSql = "select FS_PROVIDERNAME, FS_SP, FS_HELPCODE ";
            strSql += " from IT_PROVIDER ";
            strSql += "  order by FS_SP";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_ProviderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //查询承运单位表
        private void QueryTrans()
        {
            string strSql = "select FS_TRANSNAME, FS_CY, FS_HELPCODE ";
            strSql += " from BT_TRANS ";
            strSql += "  order by FS_CY";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_TransTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }


        //绑定物料数据
        private void BandMaterail(string text)
        {
            DataRow[] matchRows = null;

            matchRows = this.m_MaterialTable.Select("FS_HELPCODE LIKE '%" + text + "%'", "FS_WL");

            this.m_TempMaterialTable.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_TempMaterialTable.Rows.Add(dr.ItemArray);
            }

            ultraGrid1.DataSource = this.m_TempMaterialTable;

            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_MATERIALNAME"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_WL"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_HELPCODE"].Hidden = true;

            Constant.RefreshAndAutoSize(ultraGrid1);
        }


        //绑定收货单位
        private void BandReceiver(string text)
        {
            DataRow[] matchRows = null;

            matchRows = this.m_ReceiverTable.Select("FS_HELPCODE LIKE '%" + text + "%'", "FS_SH");

            this.m_TempReceiverTable.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_TempReceiverTable.Rows.Add(dr.ItemArray);
            }

            ultraGrid1.DataSource = this.m_TempReceiverTable;

            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_Memo"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_SH"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_HELPCODE"].Hidden = true;

            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        //绑定发货单位
        private void BandSender(string text)
        {
            DataRow[] matchRows = null;

            matchRows = this.m_SenderTable.Select("FS_HELPCODE LIKE '%" + text + "%'", "FS_GY");

            this.m_TempSenderTable.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_TempSenderTable.Rows.Add(dr.ItemArray);
            }

            ultraGrid1.DataSource = this.m_TempSenderTable;

            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_SUPPLIERNAME"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_GY"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_HELPCODE"].Hidden = true;

            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        //绑定供应单位
        private void BandProvider(string text)
        {
            DataRow[] matchRows = null;

            matchRows = this.m_ProviderTable.Select("FS_HELPCODE LIKE '%" + text + "%'", "FS_SP");

            this.m_TempProvierTable.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_TempProvierTable.Rows.Add(dr.ItemArray);
            }

            ultraGrid1.DataSource = this.m_TempProvierTable;

            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_PROVIDERNAME"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_SP"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_HELPCODE"].Hidden = true;

            Constant.RefreshAndAutoSize(ultraGrid1);
        }



        //绑定发货单位
        private void BandTrans(string text)
        {
            DataRow[] matchRows = null;

            matchRows = this.m_TransTable.Select("FS_HELPCODE LIKE '%" + text + "%'", "FS_CY");

            this.m_TempTransTable.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_TempTransTable.Rows.Add(dr.ItemArray);
            }

            ultraGrid1.DataSource = this.m_TempTransTable;

            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_TRANSNAME"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_CY"].Hidden = false;
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_HELPCODE"].Hidden = true;

            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        //构建物料表
        private void BuildMaterialTable()
        {
            DataColumn dc;
            dc = new DataColumn("FS_MATERIALNAME"); dc.Caption = "物料名称"; m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("FS_WL"); dc.Caption = "物料编码"; m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE"); dc.Caption = "拼音助记码"; m_MaterialTable.Columns.Add(dc);

            m_TempMaterialTable = m_MaterialTable.Clone();
        }

        //构建收货单位表
        private void BuildReceiverTable()
        {
            DataColumn dc;
            dc = new DataColumn("FS_MEMO"); dc.Caption = "收货单位"; m_ReceiverTable.Columns.Add(dc);
            dc = new DataColumn("FS_SH"); dc.Caption = "收货单位编码"; m_ReceiverTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE"); dc.Caption = "拼音助记码"; m_ReceiverTable.Columns.Add(dc);

            this.m_TempReceiverTable = this.m_ReceiverTable.Clone();
        }

        //构建发货单位表
        private void BuildSenderTable()
        {
            DataColumn dc;
            dc = new DataColumn("FS_SUPPLIERNAME"); dc.Caption = "发货单位"; this.m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_GY"); dc.Caption = "发货单位编码"; m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE"); dc.Caption = "拼音助记码"; m_SenderTable.Columns.Add(dc);

            this.m_TempSenderTable = this.m_SenderTable.Clone();
        }

        //构建发货单位表
        private void BuildProviderTable()
        {
            DataColumn dc;
            dc = new DataColumn("FS_PROVIDERNAME"); dc.Caption = "供应单位"; this.m_ProviderTable.Columns.Add(dc);
            dc = new DataColumn("FS_SP"); dc.Caption = "供应单位编码"; m_ProviderTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE"); dc.Caption = "拼音助记码"; m_ProviderTable.Columns.Add(dc);

            this.m_TempProvierTable = this.m_ProviderTable.Clone();
        }

        //构建承运单位表
        private void BuildTransTable()
        {
            DataColumn dc;
            dc = new DataColumn("FS_TRANSNAME"); dc.Caption = "承运单位"; m_TransTable.Columns.Add(dc);
            dc = new DataColumn("FS_CY"); dc.Caption = "承运单位编码"; m_TransTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE"); dc.Caption = "拼音助记码"; m_TransTable.Columns.Add(dc);

            this.m_TempTransTable = this.m_TransTable.Clone();
        }


        private void ChangeString(object sender)
        {
            if (sender is System.Windows.Forms.TextBox)
            {
                System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
                for (int i = 0; i < tb.Text.Length; i++)
                {
                    int isChange = 0;
                    char newChar = FullCodeToHalfCode(tb.Text[i], ref isChange);
                    if (isChange == 1)
                    {
                        tb.Text = tb.Text.Replace(tb.Text[i], newChar);
                        tb.SelectionStart = i + 1;
                    }
                }

            }
            else if (sender is ComboBox)
            {
                ComboBox cb = (ComboBox)sender;

                for (int i = 0; i < cb.Text.Length; i++)
                {
                    int isChange = 0;
                    char newChar = FullCodeToHalfCode(cb.Text[i], ref isChange);
                    if (isChange == 1)
                    {
                        cb.Text = cb.Text.Replace(cb.Text[i], newChar);
                        cb.SelectionStart = i + 1;
                    }
                }
            }
        }

        /*全角字符从的unicode编码从65281~65374   
         半角字符从的unicode编码从33~126   
         差值65248
         空格比较特殊,全角为       12288,半角为       32 
       */
        private char FullCodeToHalfCode(char c, ref int isChange)
        {
            //得到c的编码
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(c.ToString());

            int H = Convert.ToInt32(bytes[1]);
            int L = Convert.ToInt32(bytes[0]);

            //得到unicode编码
            int value = H * 256 + L;

            //是全角
            if (value >= 65281 && value <= 65374)
            {
                int halfvalue = value - 65248;//65248是全半角间的差值。
                byte halfL = Convert.ToByte(halfvalue);

                bytes[0] = halfL;
                bytes[1] = 0;
                isChange = 1;
            }
            else if (value == 12288)
            {
                int halfvalue = 32;
                byte halfL = Convert.ToByte(halfvalue);

                bytes[0] = halfL;
                bytes[1] = 0;
                isChange = 1;
            }
            else
            {
                isChange = 0;
                return c;
            }

            //将bytes转换成字符
            string ret = System.Text.Encoding.Unicode.GetString(bytes);

            return Convert.ToChar(ret);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            ChangeString(sender);

            for (int i = 0; i < this.tbMaterialName.Text.Length; i++)
            {
                if (Char.IsLower(tbMaterialName.Text[i]) == false && Char.IsUpper(tbMaterialName.Text[i]) == false)  //是否纯字母
                {
                    return;
                }
            }

            string text = this.tbMaterialName.Text;
            text = text.ToUpper();

            switch (m_DataType)
            {
                case "Material":
                    BandMaterail(text);
                    break;
                case "Receiver":
                    BandReceiver(text);
                    break;
                case "Sender":
                    BandSender(text);
                    break;
                case "Provider":
                    BandProvider(text);
                    break;
                case "Trans":
                    BandTrans(text);
                    break;
                default:
                    break;
            }
        }

        private void ultraGrid1_Click(object sender, EventArgs e)
        {
            if (ultraGrid1.ActiveRow == null || ultraGrid1.ActiveRow.Index < 0)
                return;
            switch (m_DataType)
            {
                case "Material":
                    this.tbMaterialName.Text = ultraGrid1.ActiveRow.Cells["FS_MATERIALNAME"].Text.Trim();
                    this.tbMaterialCode.Text = ultraGrid1.ActiveRow.Cells["FS_WL"].Text.Trim();
                    break;
                case "Receiver":
                    this.tbMaterialName.Text = ultraGrid1.ActiveRow.Cells["FS_MEMO"].Text.Trim();
                    this.tbMaterialCode.Text = ultraGrid1.ActiveRow.Cells["FS_SH"].Text.Trim();
                    break;
                case "Sender":
                    this.tbMaterialName.Text = ultraGrid1.ActiveRow.Cells["FS_SUPPLIERNAME"].Text.Trim();
                    this.tbMaterialCode.Text = ultraGrid1.ActiveRow.Cells["FS_GY"].Text.Trim();
                    break;
                case "Trans":
                    this.tbMaterialName.Text = ultraGrid1.ActiveRow.Cells["FS_TRANSNAME"].Text.Trim();
                    this.tbMaterialCode.Text = ultraGrid1.ActiveRow.Cells["FS_CY"].Text.Trim();
                    break;
                case "Provider":
                    Infragistics.Win.UltraWinGrid.UltraGridRow uRow = ultraGrid1.ActiveRow;
                    this.tbMaterialName.Text = uRow.Cells["FS_PROVIDERNAME"].Text.Trim();
                    this.tbMaterialCode.Text = uRow.Cells["FS_SP"].Text.Trim();
                    break;
                default:
                    break;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选择基础信息
        /// </summary>
        private void chooseBaseInfo()
        {
            if (m_parentForm == "CarWeigh")
            {
                CarWeigh cw = (CarWeigh)this.Owner;
                System.Data.DataRow dr;
                switch (m_DataType)
                {
                    case "Material":
                        dr = cw.weighEditorControl1.DT_Material.NewRow();
                        dr["fs_materialname"] = this.tbMaterialName.Text.Trim();
                        dr["FS_MATERIALNO"] = this.tbMaterialCode.Text.Trim();
                        cw.weighEditorControl1.tempMaterial.Rows.Add(dr.ItemArray);
                        cw.weighEditorControl1.cbWLMC.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Receiver":
                        dr = cw.weighEditorControl1.DT_Receiver.NewRow();
                        dr["fs_memo"] = this.tbMaterialName.Text.Trim();
                        dr["FS_Receiver"] = this.tbMaterialCode.Text.Trim();
                        cw.weighEditorControl1.tempReveiver.Rows.Add(dr.ItemArray);
                        cw.weighEditorControl1.cbSHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Sender":
                        dr = cw.weighEditorControl1.DT_Sender.NewRow();
                        dr["FS_SUPPLIERName"] = this.tbMaterialName.Text.Trim();
                        dr["FS_SUPPLIER"] = this.tbMaterialCode.Text.Trim();
                        cw.weighEditorControl1.tempSender.Rows.Add(dr.ItemArray);
                        cw.weighEditorControl1.cbFHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Trans":
                        dr = cw.weighEditorControl1.DT_Trans.NewRow();
                        dr["FS_TRANSNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_TRANSNO"] = this.tbMaterialCode.Text.Trim();
                        cw.weighEditorControl1.tempTrans.Rows.Add(dr.ItemArray);
                        cw.weighEditorControl1.cbCYDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    default:
                        break;

                }
            }
            else if (m_parentForm == "Predict")
            {
                PredictInfo pi = (PredictInfo)this.Owner;
                System.Data.DataRow dr;
                switch (m_DataType)
                {
                    case "Material":
                        dr = pi.tempMaterial.NewRow();
                        dr["fs_materialname"] = this.tbMaterialName.Text.Trim();
                        dr["FS_MATERIALNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempMaterial.Rows.Add(dr.ItemArray);
                        pi.cbWLMC.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Receiver":
                        dr = pi.tempReveiver.NewRow();
                        dr["fs_memo"] = this.tbMaterialName.Text.Trim();
                        dr["FS_Receiver"] = this.tbMaterialCode.Text.Trim();
                        pi.tempReveiver.Rows.Add(dr.ItemArray);
                        pi.cbSHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Sender":
                        dr = pi.tempSender.NewRow();
                        dr["FS_SUPPLIERName"] = this.tbMaterialName.Text.Trim();
                        dr["FS_SUPPLIER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempSender.Rows.Add(dr.ItemArray);
                        pi.cbFHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Provider":
                        dr = pi.tempProvider.NewRow();
                        dr["FS_PROVIDERNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_PROVIDER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempProvider.Rows.Add(dr.ItemArray);
                        pi.cbProvider.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Trans":
                        dr = pi.tempTrans.NewRow();
                        dr["FS_TRANSNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_TRANSNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempTrans.Rows.Add(dr.ItemArray);
                        pi.cbCYDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    default:
                        break;

                }

            }

            else if (m_parentForm == "Predictadmin")
            {
                PredictInfoAdmin pi = (PredictInfoAdmin)this.Owner;
                System.Data.DataRow dr;
                switch (m_DataType)
                {
                    case "Material":
                        dr = pi.tempMaterial.NewRow();
                        dr["fs_materialname"] = this.tbMaterialName.Text.Trim();
                        dr["FS_MATERIALNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempMaterial.Rows.Add(dr.ItemArray);
                        pi.cbWLMC.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Receiver":
                        dr = pi.tempReveiver.NewRow();
                        dr["fs_memo"] = this.tbMaterialName.Text.Trim();
                        dr["FS_Receiver"] = this.tbMaterialCode.Text.Trim();
                        pi.tempReveiver.Rows.Add(dr.ItemArray);
                        pi.cbSHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Sender":
                        dr = pi.tempSender.NewRow();
                        dr["FS_SUPPLIERName"] = this.tbMaterialName.Text.Trim();
                        dr["FS_SUPPLIER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempSender.Rows.Add(dr.ItemArray);
                        pi.cbFHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Provider":
                        dr = pi.tempProvider.NewRow();
                        dr["FS_PROVIDERNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_PROVIDER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempProvider.Rows.Add(dr.ItemArray);
                        pi.cbProvider.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Trans":
                        dr = pi.tempTrans.NewRow();
                        dr["FS_TRANSNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_TRANSNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempTrans.Rows.Add(dr.ItemArray);
                        pi.cbCYDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    default:
                        break;

                }

            }


            else if (m_parentForm == "HandRecordFirstWeight")
            {
                HandRecordFirstWeight pi = (HandRecordFirstWeight)this.Owner;
                System.Data.DataRow dr;
                switch (m_DataType)
                {
                    case "Material":
                        dr = pi.tempMaterial.NewRow();
                        dr["fs_materialname"] = this.tbMaterialName.Text.Trim();
                        dr["FS_MATERIALNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempMaterial.Rows.Add(dr.ItemArray);
                        pi.cbWLMC.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Receiver":
                        dr = pi.tempReveiver.NewRow();
                        dr["fs_memo"] = this.tbMaterialName.Text.Trim();
                        dr["FS_Receiver"] = this.tbMaterialCode.Text.Trim();
                        pi.tempReveiver.Rows.Add(dr.ItemArray);
                        pi.cbSHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Sender":
                        dr = pi.tempSender.NewRow();
                        dr["FS_SUPPLIERName"] = this.tbMaterialName.Text.Trim();
                        dr["FS_SUPPLIER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempSender.Rows.Add(dr.ItemArray);
                        pi.cbFHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Provider":
                        dr = pi.tempProvider.NewRow();
                        dr["FS_PROVIDERNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_PROVIDER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempProvider.Rows.Add(dr.ItemArray);
                        pi.cbProvider.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Trans":
                        dr = pi.tempTrans.NewRow();
                        dr["FS_TRANSNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_TRANSNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempTrans.Rows.Add(dr.ItemArray);
                        pi.cbCYDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    default:
                        break;

                }
            }

            else if (m_parentForm =="HandRecordWeight")
            {
                HandRecordWeight pi = (HandRecordWeight)this.Owner;
                System.Data.DataRow dr;
                switch (m_DataType)
                {
                    case "Material":
                        dr = pi.tempMaterial.NewRow();
                        dr["fs_materialname"] = this.tbMaterialName.Text.Trim();
                        dr["FS_MATERIALNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempMaterial.Rows.Add(dr.ItemArray);
                        pi.cbWLMC.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Receiver":
                        dr = pi.tempReveiver.NewRow();
                        dr["fs_memo"] = this.tbMaterialName.Text.Trim();
                        dr["FS_Receiver"] = this.tbMaterialCode.Text.Trim();
                        pi.tempReveiver.Rows.Add(dr.ItemArray);
                        pi.cbSHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Sender":
                        dr = pi.tempSender.NewRow();
                        dr["FS_SUPPLIERName"] = this.tbMaterialName.Text.Trim();
                        dr["FS_SUPPLIER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempSender.Rows.Add(dr.ItemArray);
                        pi.cbFHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Provider":
                        dr = pi.tempProvider.NewRow();
                        dr["FS_PROVIDERNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_PROVIDER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempProvider.Rows.Add(dr.ItemArray);
                        pi.cbProvider.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Trans":
                        dr = pi.tempTrans.NewRow();
                        dr["FS_TRANSNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_TRANSNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempTrans.Rows.Add(dr.ItemArray);
                        pi.cbCYDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    default:
                        break;

                }
            }
            else if (m_parentForm == "UpdateFirstWeight")
            {
                UpdateFirstWeight pi = (UpdateFirstWeight)this.Owner;
                System.Data.DataRow dr;
                switch (m_DataType)
                {
                    case "Material":
                        dr = pi.tempMaterial.NewRow();
                        dr["fs_materialname"] = this.tbMaterialName.Text.Trim();
                        dr["FS_MATERIALNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempMaterial.Rows.Add(dr.ItemArray);
                        pi.cbWLMC.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Receiver":
                        dr = pi.tempReveiver.NewRow();
                        dr["fs_memo"] = this.tbMaterialName.Text.Trim();
                        dr["FS_Receiver"] = this.tbMaterialCode.Text.Trim();
                        pi.tempReveiver.Rows.Add(dr.ItemArray);
                        pi.cbSHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    case "Sender":
                        dr = pi.tempSender.NewRow();
                        dr["FS_SUPPLIERName"] = this.tbMaterialName.Text.Trim();
                        dr["FS_SUPPLIER"] = this.tbMaterialCode.Text.Trim();
                        pi.tempSender.Rows.Add(dr.ItemArray);
                        pi.cbFHDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    //case "Provider":
                    //    dr = pi.tempProvider.NewRow();
                    //    dr["FS_PROVIDERNAME"] = this.tbMaterialName.Text.Trim();
                    //    dr["FS_PROVIDER"] = this.tbMaterialCode.Text.Trim();
                    //    pi.tempProvider.Rows.Add(dr.ItemArray);
                    //    pi.cbProvider.Text = this.tbMaterialName.Text.Trim();
                    //    break;
                    case "Trans":
                        dr = pi.tempTrans.NewRow();
                        dr["FS_TRANSNAME"] = this.tbMaterialName.Text.Trim();
                        dr["FS_TRANSNO"] = this.tbMaterialCode.Text.Trim();
                        pi.tempTrans.Rows.Add(dr.ItemArray);
                        pi.cbCYDW.Text = this.tbMaterialName.Text.Trim();
                        break;
                    default:
                        break;

                }
            }

            else if (m_parentForm == "UpdateSecondWeight")
              {
                  UpdateSecondWeight pi = (UpdateSecondWeight)this.Owner;
                  System.Data.DataRow dr;
                  switch (m_DataType)
                  {
                      case "Material":
                          dr = pi.tempMaterial.NewRow();
                          dr["fs_materialname"] = this.tbMaterialName.Text.Trim();
                          dr["FS_MATERIALNO"] = this.tbMaterialCode.Text.Trim();
                          pi.tempMaterial.Rows.Add(dr.ItemArray);
                          pi.cbWLMC.Text = this.tbMaterialName.Text.Trim();
                          break;
                      case "Receiver":
                          dr = pi.tempReveiver.NewRow();
                          dr["fs_memo"] = this.tbMaterialName.Text.Trim();
                          dr["FS_Receiver"] = this.tbMaterialCode.Text.Trim();
                          pi.tempReveiver.Rows.Add(dr.ItemArray);
                          pi.cbSHDW.Text = this.tbMaterialName.Text.Trim();
                          break;
                      case "Sender":
                          dr = pi.tempSender.NewRow();
                          dr["FS_SUPPLIERName"] = this.tbMaterialName.Text.Trim();
                          dr["FS_SUPPLIER"] = this.tbMaterialCode.Text.Trim();
                          pi.tempSender.Rows.Add(dr.ItemArray);
                          pi.cbFHDW.Text = this.tbMaterialName.Text.Trim();
                          break;
                      //case "Provider":
                      //    dr = pi.tempProvider.NewRow();
                      //    dr["FS_PROVIDERNAME"] = this.tbMaterialName.Text.Trim();
                      //    dr["FS_PROVIDER"] = this.tbMaterialCode.Text.Trim();
                      //    pi.tempProvider.Rows.Add(dr.ItemArray);
                      //    pi.cbProvider.Text = this.tbMaterialName.Text.Trim();
                      //    break;
                      case "Trans":
                          dr = pi.tempTrans.NewRow();
                          dr["FS_TRANSNAME"] = this.tbMaterialName.Text.Trim();
                          dr["FS_TRANSNO"] = this.tbMaterialCode.Text.Trim();
                          pi.tempTrans.Rows.Add(dr.ItemArray);
                          pi.cbCYDW.Text = this.tbMaterialName.Text.Trim();
                          break;
                      default:
                          break;

                  }

              }
            else
            {
                //WeighMeasureInfo pi = (WeighMeasureInfo)this.Owner;
                //System.Data.DataRow dr;
                //switch (m_DataType)
                //{
                //    case "Material":
                //        dr = pi.tempMaterial.NewRow();
                //        dr["fs_materialname"] = this.tbMaterialName.Text.Trim();
                //        dr["FS_MATERIALNO"] = this.tbMaterialCode.Text.Trim();
                //        pi.tempMaterial.Rows.Add(dr.ItemArray);
                //        pi.cbWLMC.Text = this.tbMaterialName.Text.Trim();
                //        break;
                //    case "Receiver":
                //        dr = pi.tempReveiver.NewRow();
                //        dr["fs_memo"] = this.tbMaterialName.Text.Trim();
                //        dr["FS_Receiver"] = this.tbMaterialCode.Text.Trim();
                //        pi.tempReveiver.Rows.Add(dr.ItemArray);
                //        pi.cbSHDW.Text = this.tbMaterialName.Text.Trim();
                //        break;
                //    case "Sender":
                //        dr = pi.tempSender.NewRow();
                //        dr["FS_SUPPLIERName"] = this.tbMaterialName.Text.Trim();
                //        dr["FS_SUPPLIER"] = this.tbMaterialCode.Text.Trim();
                //        pi.tempSender.Rows.Add(dr.ItemArray);
                //        pi.cbFHDW.Text = this.tbMaterialName.Text.Trim();
                //        break;
                //    case "Provider":
                //        dr = pi.tempProvider.NewRow();
                //        dr["FS_PROVIDERNAME"] = this.tbMaterialName.Text.Trim();
                //        dr["FS_PROVIDER"] = this.tbMaterialCode.Text.Trim();
                //        pi.tempProvider.Rows.Add(dr.ItemArray);
                //        pi.cbProvider.Text = this.tbMaterialName.Text.Trim();
                //        break;
                //    case "Trans":
                //        dr = pi.tempTrans.NewRow();
                //        dr["FS_TRANSNAME"] = this.tbMaterialName.Text.Trim();
                //        dr["FS_TRANSNO"] = this.tbMaterialCode.Text.Trim();
                //        pi.tempTrans.Rows.Add(dr.ItemArray);
                //        pi.cbCYDW.Text = this.tbMaterialName.Text.Trim();
                //        break;
                //    default:
                //        break;

                //}
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chooseBaseInfo();
        }

        private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void ultraGrid1_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null)
            {
                return;
            }
            chooseBaseInfo();

        }
    }
}
