using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YGJZJL.Car
{
    public enum TermTares
    {
        Hours24 = 0,
        Today = 1
    }

    public partial class WeighEditorControl : UserControl
    {
        #region 属性
        private string _pointCode = "";//计量点编号

        public DataTable tempMaterial = new DataTable();  //物料临时表；用于磅房筛选和助记码筛选
        public DataTable tempReveiver = new DataTable();
        public DataTable tempSender = new DataTable();
        public DataTable tempTrans = new DataTable();
        public DataTable m_CarNoTable = new DataTable();
        public DataTable tempCarNo = new DataTable();

        /// <summary>
        /// 卡号
        /// </summary>
        public bool IFYBFirst
        {
            get { return this.checkIFYBFirst.Checked; }
            set { this.checkIFYBFirst.Checked = value; }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return this.txtCZH.Text.Trim(); }
            set { this.txtCZH.Text = value; }
        }
        /// <summary>
        /// 车号
        /// </summary>
        public string CarNo
        {
            get { return this.cbCH.Text.Trim()+this.cbCH1.Text.Trim(); }
            set 
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 1)
                {
                    if (value.Length == 7)
                    {
                        this.cbCH.Text = value.Substring(0, 2);
                        this.cbCH1.Text = value.Substring(2);
                    }
                    else if (value.Length == 8)
                    {
                        this.cbCH.Text = value.Substring(0, 3);
                        this.cbCH1.Text = value.Substring(3);
                    }
                    else
                    {
                        //this.cbCH.Text = value.Substring(0, 1);
                        //this.cbCH1.Text = value.Substring(1);
                    }
                }
                else
                {
                    
                    this.cbCH.Text = "";
                  
                    this.cbCH1.Text = "";
                }
            }
        }
        /// <summary>
        /// 快速预报号
        /// </summary>
        public string QuickPlan
        {
            get { return this.textBox2.Text.Trim(); }
            set { this.textBox2.Text = value; }
        }
        /// <summary>
        /// 炉号1
        /// </summary>
        public string StoveNo1
        {
            get { return this.txtLH.Text.Trim(); }
            set { this.txtLH.Text = value; }
        }
        /// <summary>
        /// 炉号2
        /// </summary>
        public string StoveNo2
        {
            get { return this.txtLH2.Text.Trim(); }
            set { this.txtLH2.Text = value; }
        }
        /// <summary>
        /// 炉号3
        /// </summary>
        public string StoveNo3
        {
            get { return this.txtLH3.Text.Trim(); }
            set { this.txtLH2.Text = value; }
        }
        /// <summary>
        /// 合同号
        /// </summary>
        public string OrderNo
        {
            get { return this.txtHTH.Text.Trim(); }
            set { this.txtHTH.Text = value; }
        }
        /// <summary>
        /// 合同项目号
        /// </summary>
        public string OrderSeq
        {
            get { return this.txtHTXMH.Text.Trim() ; }
            set { this.txtHTXMH.Text = value; }
        }
        /// <summary>
        /// 是否期限皮重
        /// </summary>
        public bool IsTermTare
        {
            get { return this.chbQXPZ.Checked; }
            set { this.chbQXPZ.Checked = value; }
        }

        /// <summary>
        /// 期限皮重类型
        /// </summary>
        public TermTares TermTareType
        {
            get { return ultraOptionSet1.Value != null && ultraOptionSet1.Value.ToString().Equals("1") ? TermTares.Today : TermTares.Hours24; }
            set { ultraOptionSet1.Value = value == TermTares.Today ? 1 : 0; }
        }
        /// <summary>
        /// 支数1
        /// </summary>
        public int StoveCount1
        {
            get 
            {
                int count = 0;
                int.TryParse(this.txtZS.Text.Trim(), out count);
                return count; 
            }
            set { this.txtZS.Text = value.ToString(); }
        }
        /// <summary>
        /// 支数2
        /// </summary>
        public int StoveCount2
        {
            get
            {
                int count = 0;
                int.TryParse(this.txtZS2.Text.Trim(), out count);
                return count;
            }
            set { this.txtZS2.Text = value.ToString(); }
        }
        /// <summary>
        /// 支数3
        /// </summary>
        public int StoveCount3
        {
            get
            {
                int count = 0;
                int.TryParse(this.txtZS3.Text.Trim(), out count);
                return count;
            }
            set { this.txtZS3.Text = value.ToString(); }
        }
        /// <summary>
        /// 物料
        /// </summary>
        public string Material
        {
            get { return this.cbWLMC.SelectedValue != null ? this.cbWLMC.SelectedValue.ToString() : ""; }
            set { this.cbWLMC.SelectedValue = value; }
        }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName
        {
            get { return this.cbWLMC.Text.Trim(); }
            set { this.cbWLMC.Text = value; }
        }
        /// <summary>
        /// 流向
        /// </summary>
        public string Flow
        {
            get { return this.cbLX.SelectedValue != null ? this.cbLX.SelectedValue.ToString() : ""; }
            set { this.cbLX.SelectedValue = value; }
        }
        /// <summary>
        /// 承运单位
        /// </summary>
        public string TransCompany
        {
            get { return this.cbCYDW.SelectedValue != null ? this.cbCYDW.SelectedValue.ToString() : ""; }
            set { this.cbCYDW.SelectedValue = value; }
        }

        /// <summary>
        /// 承运单位名称
        /// </summary>
        public string TransCompanyName
        {
            get { return this.cbCYDW.Text.Trim(); }
            set { this.cbCYDW.Text = value; }
        }

        /// <summary>
        /// 发货单位
        /// </summary>
        public string SendCompany
        {
            get { return this.cbFHDW.SelectedValue != null ? this.cbFHDW.SelectedValue.ToString() : ""; }
            set { this.cbFHDW.SelectedValue = value; }
        }

        /// <summary>
        /// 发货单位名称
        /// </summary>
        public string SendCompanyName
        {
            get { return this.cbFHDW.Text.Trim(); }
            set { this.cbFHDW.Text = value; }
        }
        /// <summary>
        /// 收货单位
        /// </summary>
        public string ReceiveCompany
        {
            get { return this.cbSHDW.SelectedValue != null ? this.cbSHDW.SelectedValue.ToString() : ""; }
            set { this.cbSHDW.SelectedValue = value; }
        }

        /// <summary>
        /// 收货单位名称
        /// </summary>
        public string ReceiveCompanyName
        {
            get { return this.cbSHDW.Text.Trim(); }
            set { this.cbSHDW.Text = value; }
        }
        /// <summary>
        /// 发货地点
        /// </summary>
        public string SendPlace
        {
            get { return this.tbSenderPlace.Text.Trim(); }
            set { this.tbSenderPlace.Text = value; }
        }
        /// <summary>
        /// 卸货地点
        /// </summary>
        public string DischargePlace
        {
            get { return this.tbReceiverPlace.Text.Trim(); }
            set { this.tbReceiverPlace.Text = value; }
        }
        /// <summary>
        /// 计量类型
        /// </summary>
        public int WeightType
        {
            get { return this.cbJLLX.SelectedIndex; }
            set { this.cbJLLX.SelectedIndex = value; }
        }
        /// <summary>
        /// 费用
        /// </summary>
        public double Cost
        {
            get 
            {
                double cost = 0;
                double.TryParse(this.tbCharge.Text.Trim(), out cost);
                return cost; 
            }
            set { this.tbCharge.Text = value.ToString(); }
        }
        /// <summary>
        /// 应扣量
        /// </summary>
        public double Deduction
        {
            get 
            {
                double deduction = 0;
                double.TryParse(this.txtYKL.Text.Trim(),out deduction);
                return deduction; 
            }
            set { this.txtYKL.Text = value.ToString(); }
        }
        /// <summary>
        /// 毛重
        /// </summary>
        public double GrossWeight
        {
            get
            {
                double grossWeight = 0;
                double.TryParse(this.txtMZ.Text.Trim(), out grossWeight);
                return grossWeight;
            }
            set { this.txtMZ.Text = value.ToString(); }
        }
        /// <summary>
        /// 皮重
        /// </summary>
        public double TareWeight
        {
            get
            {
                double tareWeight = 0;
                double.TryParse(this.txtPZ.Text.Trim(), out tareWeight);
                return tareWeight;
            }
            set { this.txtPZ.Text = value.ToString(); }
        }
        /// <summary>
        /// 净重
        /// </summary>
        public double NetWeight
        {
            get
            {
                double netWeight = 0;
                double.TryParse(this.txtJZ.Text.Trim(), out netWeight);
                return netWeight;
            }
            set { this.txtJZ.Text = value.ToString(); }
        }
        /// <summary>
        /// 票据编号
        /// </summary>
        public string BillNo
        {
            get { return this.txtPJBH.Text.Trim(); }
            set { this.txtPJBH.Text = value; }
        }
        /// <summary>
        /// 是否一车多料
        /// </summary>
        public bool IsMultiMaterials
        {
            get { return this.checkBox2.Checked; }
            set { this.checkBox2.Checked = value; }
        }

        /// <summary>
        /// 对方毛重
        /// </summary>
        public double SenderGrosssWeight
        {
            get
            {
                double weight = 0;
                double.TryParse(txtDFMZ.Text.Trim(), out weight);
                return weight;
            }

            set
            {
                txtDFMZ.Text = value.ToString();
            }
        }

        /// <summary>
        /// 对方皮重
        /// </summary>
        public double SenderTareWeight
        {
            get
            {
                double weight = 0;
                double.TryParse(txtDFPZ.Text.Trim(), out weight);
                return weight;
            }

            set
            {
                txtDFPZ.Text = value.ToString();
            }
        }

        /// <summary>
        /// 对方净重
        /// </summary>
        public double SenderWeight
        {
            get
            {
                double senderWeight = 0;
                double.TryParse(this.txtDFJZ.Text.Trim(), out senderWeight);
                return senderWeight;
            }
            set { this.txtDFJZ.Text = value.ToString(); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return this.tbBZ.Text.Trim(); }
            set { this.tbBZ.Text = value; }
        }

        /// <summary>
        /// 计量状态
        /// </summary>
        public string WeighStatus
        {
            get { return this.lblStatus.Text.Trim(); }
            set { this.lblStatus.Text = value; }
        }

        /// <summary>
        /// 物料Table
        /// </summary>
        public DataTable DT_Material
        {
            get { return this.dataTable7; }
            set { this.dataTable7 = value; }
        }

        /// <summary>
        /// 发货单位Table
        /// </summary>
        public DataTable DT_Sender
        {
            get { return this.dataTable8; }
            set { this.dataTable8 = value; }
        }

        /// <summary>
        /// 收货单位Table
        /// </summary>
        public DataTable DT_Receiver
        {
            get { return this.dataTable9; }
            set { this.dataTable9 = value; }
        }

        /// <summary>
        /// 承运单位Table
        /// </summary>
        public DataTable DT_Trans
        {
            get { return this.dataTable10; }
            set { this.dataTable10 = value; }
        }

        /// <summary>
        /// 车号Table
        /// </summary>
        public DataTable DT_Car
        {
            get { return this.m_CarNoTable; }
            set { this.m_CarNoTable = value; }
        }

        /// <summary>
        /// 流向Table
        /// </summary>
        public DataTable DT_Flow
        {
            get { return this.dataTable11; }
            set { this.dataTable11 = value; }
        }

        /// <summary>
        /// 预报Table
        /// </summary>
        public DataTable DT_TransPlan
        {
            get { return this.dataTable4; }
            set { this.dataTable4 = value; }
        }

        
        /// <summary>
        /// 计量点编码
        /// </summary>
        public string PointCode
        {
            get { return this._pointCode; }
            set 
            { 
                this._pointCode = value;
                if (!string.IsNullOrEmpty(value))
                {
                    //BandPointCarNo(value);
                    BandPointMaterial(value);
                    BandPointReceiver(value);
                    BandPointSender(value);
                    BandPointTrans(value);
                }
            }
        }
        /// <summary>
        /// 计量点名称
        /// </summary>
        public string PointName
        {
            get { return this.txtJLD.Text.Trim(); }
            set { this.txtJLD.Text = value; }
        }
        /// <summary>
        /// 计量班次
        /// </summary>
        public string WeighShift
        {
            get { return this.txtBC.Text.Trim(); }
            set { this.txtBC.Text = value; }
        }
        /// <summary>
        /// 计量班组
        /// </summary>
        public string WeighGroup
        {
            get { return this.textBox1.Text.Trim(); }
            set { this.textBox1.Text = value; }
        }
        /// <summary>
        /// 计量员
        /// </summary>
        public string Weigher
        {
            get { return this.txtJLY.Text.Trim(); }
            set { this.txtJLY.Text = value; }
        }

        private string _transPlanNo = "";
        /// <summary>
        /// 预报编号
        /// </summary>
        ///
        public string TransPlanNo
        {
            get { return this._transPlanNo; }
            set { this._transPlanNo = value; }
        }

        private bool _isNeedDischarge = true;
        /// <summary>
        /// 是否需要卸货
        /// </summary>
        public bool IsNeedDischarge
        {
            get { return _isNeedDischarge; }
            set { _isNeedDischarge = value; }
        }

        private string _dischargeFlag = "";
        /// <summary>
        /// 卸货标志
        /// </summary>
        public string DischargeFlag
        {
            get { return _dischargeFlag; }
            set { _dischargeFlag = value; }
        }

        private string _discharger = "";
        /// <summary>
        /// 卸货人
        /// </summary>
        public string Discharger
        {
            get { return _discharger; }
            set { _discharger = value; }
        }

        private string _dischargeTime = "";
        /// <summary>
        /// 卸货时间
        /// </summary>
        public string DischargeTime
        {
            get { return _dischargeTime; }
            set { _dischargeTime = value; }
        }

        private string _dischargeDepart = "";
        /// <summary>
        /// 卸货单位
        /// </summary>
        public string DischargeDepart
        {
            get { return _dischargeDepart; }
            set { _dischargeDepart = value; }
        }


        private string _cardWeightNo = "";
        /// <summary>
        /// 卡上计量编号
        /// </summary>
        public string CardWeightNo
        {
            get { return _cardWeightNo; }
            set { _cardWeightNo = value; }
        }

        private string _cardDedution = "";
        /// <summary>
        /// 卡上扣渣量
        /// </summary>
        public string CardDedution
        {
            get { return _cardDedution; }
            set { _cardDedution = value; }
        }

        public bool AlowTareWeight
        {
            get { return checkBox3.Checked; }
        }

        public event EventHandler weighEditorEvent;
        public event EventHandler weightEditorTextChahgeEvent;
        #endregion

        public WeighEditorControl()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
            textBox2.BackColor = checkBox1.Checked ? Color.Bisque : Color.White;
        }

        private void cbLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLX.SelectedValue != null)
            {
                string flow = cbLX.SelectedValue.ToString();
                switch (flow)
                {
                    case "001":
                    case "007":
                        chbQXPZ.Checked = false;
                        chbQXPZ.Enabled = false;
                        break;
                    default:
                        //chbQXPZ.Checked = true;
                        chbQXPZ.Enabled = true;
                        break;
                }
            }
        }

        private void cbJLLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCharge.Text = "0";

            if (cbJLLX.Text.Trim().Equals("外协"))
            {
                tbCharge.Enabled = true;
            }
            else
            {
                tbCharge.Enabled = false;
            }
        }

        private void WeighEditorControl_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnRefresh, "重新刷卡");
        }

        private void cbFHDW_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 设置计量点信息
        /// </summary>
        /// <param name="pointCode"></param>
        /// <param name="pointName"></param>
        /// <param name="weigher"></param>
        /// <param name="weighShift"></param>
        /// <param name="weighGroup"></param>
        public void SetPointInfo(string pointCode, string pointName, string weigher, string weighShift, string weighGroup)
        {
            this.PointCode = pointCode;
            this.PointName = pointName;
            this.Weigher = weigher;
            this.WeighShift = weighShift;
            this.WeighGroup = weighGroup;
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            //txtCZH.Text = "";
            //cbCH.Text = "";
            //cbCH1.Text = "";
            textBox2.Text = "";
            txtLH.Text = "";
            txtLH2.Text = "";
            txtLH3.Text = "";
            txtHTH.Text = "";
            txtHTXMH.Text = "";
            chbQXPZ.Checked = false;
            txtZS.Text = "";
            txtZS2.Text = "";
            txtZS3.Text = "";
            cbWLMC.Text = "";
            cbWLMC.SelectedValue = "";
            cbLX.SelectedIndex = -1;
            cbCYDW.Text = "";
            cbCYDW.SelectedValue = "";
            cbSHDW.Text = "";
            cbSHDW.SelectedValue = "";
            cbFHDW.Text = "";
            cbFHDW.SelectedValue = "";
            tbSenderPlace.Text = "";
            tbReceiverPlace.Text = "";
            cbJLLX.SelectedIndex = -1;
            tbCharge.Text = "0";
            txtYKL.Text = "0";
            chbSFYC.Checked = false;
            txtMZ.Text = "";
            txtPZ.Text = "";
            txtJZ.Text = "";
            txtPJBH.Text = "";
            txtZL.Text = "";
            checkBox2.Checked = false;
            txtDFJZ.Text = "";
            //checkBox1.Checked = false;
            textBox2.Text = "";
            lblStatus.Text = "";

            TransPlanNo = "";
            IsNeedDischarge = true;
            Discharger = "";
            DischargeFlag = "";
            DischargeTime = "";

            tbBZ.Text = "";
            txtDFMZ.Text = "";
            txtDFPZ.Text = "";

            checkBox3.Checked = false;
        }

        #region 绑定筛选控件
        //按磅房筛选物料信息
        private void BandPointMaterial(string PointID)
        {
            DataRow[] drs = null;

            tempMaterial = this.DT_Material.Clone();

            drs = this.DT_Material.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            tempMaterial.Clear();
            foreach (DataRow dr in drs)
            {
                tempMaterial.Rows.Add(dr.ItemArray);
            }

            DataRow drz = tempMaterial.NewRow();
            tempMaterial.Rows.InsertAt(drz, 0);
            cbWLMC.DataSource = tempMaterial;
            cbWLMC.DisplayMember = "fs_materialname";
            cbWLMC.ValueMember = "FS_MATERIALNO";
        }

        //按磅房筛选收货单位
        private void BandPointReceiver(string PointID)
        {
            DataRow[] drs = null;

            this.tempReveiver = this.DT_Receiver.Clone();

            drs = this.DT_Receiver.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempReveiver.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempReveiver.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempReveiver.NewRow();
            this.tempReveiver.Rows.InsertAt(drz, 0);
            this.cbSHDW.DataSource = this.tempReveiver;
            this.cbSHDW.DisplayMember = "FS_MEMO";
            this.cbSHDW.ValueMember = "FS_Receiver";
        }

        //按磅房筛选发货单位
        private void BandPointSender(string PointID)
        {
            DataRow[] drs = null;

            this.tempSender = this.DT_Sender.Clone();

            drs = this.DT_Sender.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempSender.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempSender.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempSender.NewRow();
            this.tempSender.Rows.InsertAt(drz, 0);
            this.cbFHDW.DataSource = this.tempSender;
            this.cbFHDW.DisplayMember = "FS_SUPPLIERName";
            this.cbFHDW.ValueMember = "FS_SUPPLIER";
        }

        //按磅房筛选承运单位
        private void BandPointTrans(string PointID)
        {
            DataRow[] drs = null;

            this.tempTrans = this.DT_Trans.Clone();

            drs = this.DT_Trans.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempTrans.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempTrans.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempTrans.NewRow();
            this.tempTrans.Rows.InsertAt(drz, 0);
            this.cbCYDW.DataSource = this.tempTrans;
            cbCYDW.DisplayMember = "FS_TRANSNAME";
            cbCYDW.ValueMember = "FS_TRANSNO";

        }

        //按磅房筛选车号
        private void BandPointCarNo(string PointID)
        {
            DataRow[] drs = null;
            
            this.tempCarNo = this.m_CarNoTable.Clone();

            drs = this.m_CarNoTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempCarNo.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempCarNo.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempCarNo.NewRow();
            this.tempCarNo.Rows.InsertAt(drz, 0);
            this.cbCH.DataSource = this.tempCarNo;
            cbCH.DisplayMember = "FS_CARNO";
        }
        #endregion

        private void btnWLMC_Click(object sender, EventArgs e)
        {
            showDialog(sender);
        }

        private void btnCYDW_Click(object sender, EventArgs e)
        {
            showDialog(sender);
        }

        private void btnFHDW_Click(object sender, EventArgs e)
        {
            showDialog(sender);
        }

        private void btnSHDW_Click(object sender, EventArgs e)
        {
            showDialog(sender);
        }

        private void showDialog(object sender)
        {
            if (string.IsNullOrEmpty(PointCode))
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);
            MoreBaseInfo frm = new MoreBaseInfo(this.ParentForm, Btn.Tag.ToString(), ((CoreFS.CA06.FrmBase)this.ParentForm).ob);
            frm.Owner = this.ParentForm;
            frm.ShowDialog();
        }

        private void cbCH_TextChanged(object sender, EventArgs e)
        {
            cbCH.Text = cbCH.Text.ToUpper();
            cbCH.Focus();
            cbCH.SelectionStart = cbCH.Text.Length;
            OnWeightEditorTextChange(sender);
            //cbCH.f
        }

        private void txtCZH_TextChanged(object sender, EventArgs e)
        {
            OnWeightEditorTextChange(sender);
        }

        private void cbCH1_TextChanged(object sender, EventArgs e)
        {
            cbCH1.Text = cbCH1.Text.ToUpper();
            cbCH1.Focus();
            cbCH1.SelectionStart = cbCH1.Text.Length;
            OnWeightEditorTextChange(sender);
        }

        private void txtLH_TextChanged(object sender, EventArgs e)
        {
            txtLH.Text = txtLH.Text.ToUpper();
            txtLH.Focus();
            txtLH.SelectionStart = txtLH.Text.Length;
        }

        private void txtLH2_TextChanged(object sender, EventArgs e)
        {
            txtLH2.Text = txtLH2.Text.ToUpper();
            txtLH2.Focus();
            txtLH2.SelectionStart = txtLH2.Text.Length;
        }

        private void txtLH3_TextChanged(object sender, EventArgs e)
        {
            txtLH3.Text = txtLH3.Text.ToUpper();
            txtLH3.Focus();
            txtLH3.SelectionStart = txtLH3.Text.Length;
        }

        public new void Focus()
        {
            this.cbCH1.Focus();
        }

        #region 查询预报
        private void txtCZH_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PointCode) && !string.IsNullOrEmpty(CardNo) && string.IsNullOrEmpty(CarNo))
            {
                OnWeightEditorChange(sender);
            }
        }

        private void cbCH1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PointCode) && !string.IsNullOrEmpty(CarNo) && string.IsNullOrEmpty(CardNo))
            {
                OnWeightEditorChange(sender);
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(QuickPlan))
            {
                OnWeightEditorChange(sender);
            }
        }

        /// <summary>
        /// 绑定计量预报
        /// </summary>
        public void BandTransPlan()
        {
            try
            {
                if (DT_TransPlan.Rows.Count > 0)
                {
                    DataRow dr = DT_TransPlan.Rows[0];
                    
                    txtHTH.Text = dr["FS_CONTRACTNO"].ToString();
                    txtHTXMH.Text = dr["FS_CONTRACTITEM"].ToString();
                    txtZS.Text = dr["FN_BILLETCOUNT"].ToString();
                    txtLH.Text = dr["FS_STOVENO"].ToString();
                    txtLH2.Text = dr["FS_STOVENO2"].ToString();
                    txtZS2.Text = dr["FN_COUNT2"].ToString();
                    txtLH3.Text = dr["FS_STOVENO3"].ToString();
                    txtZS3.Text = dr["FN_COUNT3"].ToString();
                    //cbWLMC.Text = dr["FS_MATERIALNAME"].ToString();
                    //物料下拉框添加该磅房未选择过的物料
                    string str_FS_MATERIAL = dr["FS_MATERIAL"].ToString().Trim();
                    cbWLMC.SelectedValue = str_FS_MATERIAL;
                    if (cbWLMC.SelectedValue == null)
                    {
                        System.Data.DataRow tempdr = DT_Material.NewRow();
                        tempdr["fs_materialname"] = dr["FS_MATERIALNAME"].ToString().Trim();
                        tempdr["FS_MATERIALNO"] = str_FS_MATERIAL;
                        tempMaterial.Rows.Add(tempdr.ItemArray);
                        cbWLMC.SelectedValue = str_FS_MATERIAL;
                    }    
                    
                    cbLX.Text = dr["FS_LX"].ToString();
                    //cbCYDW.Text = dr["FS_CYDW"].ToString();
                    //承运单位下拉框添加该磅房未选择过的承运单位
                    string str_FS_TRANSNO= dr["FS_TRANSNO"].ToString().Trim();
                    cbCYDW.SelectedValue = str_FS_TRANSNO;
                    if (cbCYDW.SelectedValue == null)
                    {
                        System.Data.DataRow tempdr = DT_Trans.NewRow();
                        tempdr["FS_TRANSNAME"] = dr["FS_CYDW"].ToString().Trim();
                        tempdr["FS_TRANSNO"] = str_FS_TRANSNO;
                        tempTrans.Rows.Add(tempdr.ItemArray);
                        cbCYDW.SelectedValue = str_FS_TRANSNO;
                    }   

                    //cbFHDW.Text = dr["FS_FHDW"].ToString();
                    //发货单位下拉框添加该磅房未选择过的发货单位
                    string str_FS_SENDER = dr["FS_SENDER"].ToString().Trim();
                    cbFHDW.SelectedValue = str_FS_SENDER;
                    if (cbFHDW.SelectedValue == null)
                    {
                        System.Data.DataRow tempdr = DT_Sender.NewRow();
                        tempdr["FS_SUPPLIERName"] = dr["FS_FHDW"].ToString().Trim();
                        tempdr["FS_SUPPLIER"] = str_FS_SENDER;
                        tempSender.Rows.Add(tempdr.ItemArray);
                        cbFHDW.SelectedValue = str_FS_SENDER;
                    } 
                    //cbSHDW.Text = dr["FS_SHDW"].ToString();
                    //收货单位下拉框添加该磅房未选择过的收货单位
                    string str_FS_RECEIVERFACTORY = dr["FS_RECEIVERFACTORY"].ToString().Trim();
                    cbSHDW.SelectedValue = str_FS_RECEIVERFACTORY;
                    if (cbSHDW.SelectedValue == null)
                    {
                        System.Data.DataRow tempdr = DT_Receiver.NewRow();
                        tempdr["fs_memo"] = dr["FS_SHDW"].ToString().Trim();
                        tempdr["FS_Receiver"] = str_FS_RECEIVERFACTORY;
                        tempReveiver.Rows.Add(tempdr.ItemArray);
                        cbSHDW.SelectedValue = str_FS_RECEIVERFACTORY;
                    } 
                    tbSenderPlace.Text = dr["FS_SENDERSTORE"].ToString();
                    tbReceiverPlace.Text = dr["FS_RECEIVERSTORE"].ToString();
                    tbBZ.Text = dr["FS_DRIVERREMARK"].ToString();
                    TransPlanNo = dr["FS_PLANCODE"].ToString();
                    IsNeedDischarge = dr["FS_LEVEL"].ToString().Equals("0") ? false : true;
                    txtDFMZ.Text = dr["FS_DFMZ"] != null ? dr["FS_DFMZ"].ToString() : "";
                    txtDFPZ.Text = dr["FS_DFPZ"] != null ? dr["FS_DFPZ"].ToString() : "";
                    txtDFJZ.Text = dr["FS_DFJZ"] != null ? dr["FS_DFJZ"].ToString() : "";

                    if (!string.IsNullOrEmpty(dr["FS_CARNO"].ToString()))
                    {
                        CarNo = dr["FS_CARNO"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["FS_CARDNUMBER"].ToString()))
                    {
                        //txtCZH.Text = dr["FS_CARDNUMBER"].ToString();
                    }
                }
                else
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.Error("WeighEditorControl.BandTransPlan:" + ex.Message);
            }
        }
        #endregion 

        #region 检验输入
        public bool CheckInput()
        {
            if (string.IsNullOrEmpty(PointCode))
            {
                MessageBox.Show("请选择计量点！");
                return false;
            }
            if (string.IsNullOrEmpty(CarNo))
            {
                MessageBox.Show("请输入车号！");
                this.cbCH.Focus();
                return false;
            }
            
            if (string.IsNullOrEmpty(Material))
            {
                MessageBox.Show("请选择物料！");
                this.cbWLMC.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(TransCompany))
            {
                MessageBox.Show("请选择承运单位！");
                this.cbCYDW.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(SendCompany))
            {
                MessageBox.Show("请选择发货单位！");
                this.cbFHDW.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(ReceiveCompany))
            {
                MessageBox.Show("请选择收货单位！");
                this.cbSHDW.Focus();
                return false;
            }
            //20120525彭海波暂时注释
            //if (!string.IsNullOrEmpty(Material) && Material.Equals("WL110185"))
            //{
            //    if (string.IsNullOrEmpty(StoveNo1))
            //    {
            //        MessageBox.Show("请输入炉号！");
            //        this.txtLH.Focus();
            //        return false;
            //    }
            //    if (StoveCount1 == 0)
            //    {
            //        MessageBox.Show("请输入正确的支数！");
            //        this.txtZS.Focus();
            //        return false;
            //    }
            //}
            double cost = 0;
            if (WeightType == 1 && !string.IsNullOrEmpty(this.tbCharge.Text.Trim()) && !double.TryParse(this.tbCharge.Text.Trim(), out cost))
            {
                MessageBox.Show("外协过磅请输入正确的费用！");
                this.tbCharge.Focus();
                return false;
            }
            double deduction = 0;
            if (!string.IsNullOrEmpty(this.txtYKL.Text.Trim()) && !double.TryParse(this.txtYKL.Text.Trim(), out deduction))
            {
                MessageBox.Show("应扣量输入错误！");
                this.txtYKL.Focus();
                return false;
            }

            double senderWeight = 0;
            if (!string.IsNullOrEmpty(txtDFMZ.Text.Trim()) && !double.TryParse(txtDFMZ.Text.Trim(), out senderWeight))
            {
                MessageBox.Show("对方毛重输入错误！");
                txtDFMZ.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtDFPZ.Text.Trim()) && !double.TryParse(txtDFPZ.Text.Trim(), out senderWeight))
            {
                MessageBox.Show("对方皮重输入错误！");
                txtDFPZ.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtDFJZ.Text.Trim()) && !double.TryParse(txtDFJZ.Text.Trim(), out senderWeight))
            {
                MessageBox.Show("对方净重输入错误！");
                txtDFJZ.Focus();
                return false;
            }

            return true;
        }

        #endregion

        public virtual void OnWeightEditorChange(object sender)
        {
            DT_TransPlan.Clear();
            EventArgs arg = new EventArgs();
            weighEditorEvent(sender, arg);
        }

        public virtual void OnWeightEditorTextChange(object sender)
        {
            EventArgs arg = new EventArgs();
            weightEditorTextChahgeEvent(sender, arg);
        }

        #region 顺序控制
        private void txtCZH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                cbCH.Focus();
                e.Handled = true;
            }
        }

        private void cbCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                cbCH1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left || (e.KeyCode == Keys.Back && cbCH.Text.Length == 0))
            {
                txtCZH.Focus();
                txtCZH.SelectionLength = txtCZH.Text.Length;
                e.Handled = true;
            }
        }

        private void cbCH1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                if (checkBox1.Checked)
                    textBox2.Focus();
                else
                    txtLH.Focus();

                if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(PointCode) && !string.IsNullOrEmpty(CardNo))
                {
                    OnWeightEditorChange(sender);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left || (e.KeyCode == Keys.Back && cbCH1.Text.Length == 0))
            {
                cbCH.Focus();
                cbCH.SelectionLength = cbCH.Text.Length;
                e.Handled = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                this.txtJLD.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left || (e.KeyCode == Keys.Back && cbCH1.Text.Length == 0))
            {
                cbCH1.Focus();
                cbCH1.SelectionLength = cbCH.Text.Length;
                e.Handled = true;
            }
        }

        private void txtLH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtLH2.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)// || (e.KeyCode == Keys.Back && txtLH.Text.Length == 0))
            {
                if (checkBox1.Checked)
                    textBox2.Focus();
                else
                    cbCH1.Focus();
                e.Handled = true;
            }
        }

        private void txtLH2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtLH3.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtLH.Focus();
                e.Handled = true;
            }
        }

        private void txtLH3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtHTH.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtLH2.Focus();
                e.Handled = true;
            }
        }

        private void txtHTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtHTXMH.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtLH3.Focus();
                e.Handled = true;
            }
        }

        private void txtHTXMH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtZS.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtHTH.Focus();
                e.Handled = true;
            }
        }

        private void txtZS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtZS2.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtHTXMH.Focus();
                e.Handled = true;
            }
        }

        private void txtZS2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtZS3.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtZS.Focus();
                e.Handled = true;
            }
        }

        private void txtZS3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                cbWLMC.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtZS2.Focus();
                e.Handled = true;
            }
        }

        private void cbWLMC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                cbLX.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtZS3.Focus();
                e.Handled = true;
            }
        }

        private void cbLX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                cbCYDW.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                cbWLMC.Focus();
                e.Handled = true;
            }
        }

        private void cbCYDW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                cbFHDW.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                cbLX.Focus();
                e.Handled = true;
            }
        }

        private void cbFHDW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                cbSHDW.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                cbCYDW.Focus();
                e.Handled = true;
            }
        }

        private void cbSHDW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                tbSenderPlace.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                cbFHDW.Focus();
                e.Handled = true;
            }
        }

        private void tbSenderPlace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                tbReceiverPlace.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                cbSHDW.Focus();
                e.Handled = true;
            }
        }

        private void tbReceiverPlace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                cbJLLX.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                tbSenderPlace.Focus();
                e.Handled = true;
            }
        }

        private void cbJLLX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                if (tbCharge.Enabled)
                {
                    tbCharge.Focus();
                }
                else
                {
                    txtYKL.Focus();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                tbReceiverPlace.Focus();
                e.Handled = true;
            }
        }

        private void tbCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtYKL.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                cbJLLX.Focus();
                e.Handled = true;
            }
        }

        private void txtYKL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtMZ.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (tbCharge.Enabled)
                {
                    tbCharge.Focus();
                }
                else
                {
                    cbJLLX.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtMZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtPZ.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtYKL.Focus();
                e.Handled = true;
            }
        }

        private void txtPZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtJZ.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtMZ.Focus();
                e.Handled = true;
            }
        }

        private void txtJZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtPJBH.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtPZ.Focus();
                e.Handled = true;
            }
        }

        private void txtPJBH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtZL.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtJZ.Focus();
                e.Handled = true;
            }
        }

        private void txtZL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtDFJZ.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtPJBH.Focus();
                e.Handled = true;
            }
        }

        private void txtDFJZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                tbBZ.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtZL.Focus();
                e.Handled = true;
            }
        }

        private void tbBZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                txtCZH.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtDFJZ.Focus();
                e.Handled = true;
            }
        }
        #endregion

        /// <summary>
        /// 重新刷卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DT_TransPlan.Clear();
            EventArgs arg = new EventArgs();
            weighEditorEvent(sender, arg);
        }

        private void chbQXPZ_CheckedChanged(object sender, EventArgs e)
        {
            ultraOptionSet1.Visible = chbQXPZ.Checked;
            ultraOptionSet1.Value = 0;
        }

        #region 拼音助记码快速查询控制
        private ComboBox _combo = null;
        private DataTable _tmpTable = null;
        private string _filterMemeber = "FS_HELPCODE";
        private string _orderMemeber = "FN_TIMES";

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && _combo != null)
            {
                _combo.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                _combo.Focus();
                listBox1.Hide();
            }

        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                listBox1_Click(sender, e);
            }
            else if (((e.KeyChar > 64 && e.KeyChar < 91) || (e.KeyChar > 96 && e.KeyChar < 123)) && _combo != null)
            {
                listBox1.Items.Clear();
                _combo.Focus();
                _combo.Text = _combo.Text + e.KeyChar;
                _combo.SelectionStart = _combo.Text.Length;
            }
        }

        private void cbWLMC_TextChanged(object sender, EventArgs e)
        {
            Filter(sender);
        }

        private void cbCYDW_TextChanged(object sender, EventArgs e)
        {
            Filter(sender);
        }

        private void cbSHDW_TextChanged(object sender, EventArgs e)
        {
            Filter(sender);
        }

        private void cbFHDW_TextChanged(object sender, EventArgs e)
        {
            Filter(sender);
        }

        private void Filter(object sender)
        {
            if (!(sender is ComboBox))
            {
                return;
            }
            _combo = (ComboBox)sender;
            switch (_combo.Name)
            {
                case "cbWLMC":
                    //_tmpTable = ((DataSet)_combo.DataSource).Tables[0];
                    _tmpTable = tempMaterial;
                    break;
                case "cbFHDW":
                    //_tmpTable = ((DataSet)_combo.DataSource).Tables[1];
                    _tmpTable = tempSender;
                    break;
                case "cbSHDW":
                    //_tmpTable = ((DataSet)_combo.DataSource).Tables[2];
                    _tmpTable = tempReveiver;
                    break;
                case "cbCYDW":
                    //_tmpTable = ((DataSet)_combo.DataSource).Tables[3];
                    _tmpTable = tempTrans;
                    break;

            }
            ComboBox comboBox1 = (ComboBox)sender;
            if (comboBox1.Text.Trim().Length == 0 || comboBox1.Text.Trim().Equals("System.Data.DataRowView") || !comboBox1.Focused)
            {
                listBox1.Hide();
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < comboBox1.Text.Length; i++)
            {
                if (!Char.IsLower(comboBox1.Text[i]) && !Char.IsUpper(comboBox1.Text[i]))  //是否纯字母
                {
                    listBox1.Hide();
                    return;
                }
            }
            DataRow[] matchRows = new DataRow[0];

            if (!string.IsNullOrEmpty(_filterMemeber))
            {
                if (!string.IsNullOrEmpty(_filterMemeber))
                {
                    matchRows = _tmpTable.Select(_filterMemeber + " LIKE '%" + comboBox1.Text.Trim().ToUpper() + "%'", _orderMemeber + " desc");
                }
                else
                {
                    matchRows = _tmpTable.Select(_filterMemeber + " LIKE '%" + comboBox1.Text.Trim().ToUpper() + "%'");
                }
            }

            listBox1.Items.Clear();

            foreach (DataRow dr in matchRows)
            {
                //object tempitem = dr[comboBox1.DisplayMember.Split(new char[] { '.' })[1].ToString()].ToString();
                object tempitem = dr[2];
                listBox1.Items.Add(tempitem);
            }

            listBox1.Left = comboBox1.Left;
            listBox1.Top = comboBox1.Top + comboBox1.Height;
            listBox1.Width = comboBox1.Width;
            listBox1.Height = (int)(comboBox1.Height * matchRows.Length * 0.7) + 20;
            listBox1.Visible = matchRows.Length > 0 ? true : false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    if (listBox1.Visible && listBox1.Items.Count > 0 && _combo != null && _combo.Focused)
                    {
                        listBox1.SetSelected(0, true);
                        listBox1.Focus();
                        return true;
                    }
                    return false;
                case Keys.Up:
                    return false;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
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

        /// <summary>
        /// 全角字符从的unicode编码从65281~65374   
        /// 半角字符从的unicode编码从33~126   
        /// 差值65248
        /// 空格比较特殊,全角为       12288,半角为       32 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="isChange"></param>
        /// <returns></returns>
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
            return Convert.ToChar(System.Text.Encoding.Unicode.GetString(bytes));
        }
        #endregion


        private void txtDFMZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int kc = (int)e.KeyChar;
                if (kc != 8)
                {
                    if (kc != 46)
                    {
                        if (kc < 47 || kc > 58)
                        {
                            e.Handled = true;
                        }
                    }
                }
                if (kc == 46)                           //小数点
                {
                    if (txtDFMZ.Text.Length <= 0)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void txtDFMZ_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtDFPZ.Text != string.Empty)
            {
                try
                {

                    this.txtDFJZ.Text = Convert.ToString(double.Parse(this.txtDFMZ.Text.ToString().Trim()) - double.Parse(txtDFPZ.Text));
                }
                catch
                {

                }
            }
        }

        private void txtDFPZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int kc = (int)e.KeyChar;
                if (kc != 8)
                {
                    if (kc != 46)
                    {
                        if (kc < 47 || kc > 58)
                        {
                            e.Handled = true;
                        }
                    }
                }

                if (kc == 46)                           //小数点
                {
                    if (txtDFPZ.Text.Length <= 0)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void txtDFPZ_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtDFMZ.Text != string.Empty)
            {
                try
                {

                    this.txtDFJZ.Text = Convert.ToString(double.Parse(this.txtDFMZ.Text.ToString().Trim()) - double.Parse(txtDFPZ.Text));
                }
                catch
                {

                }
            }
        }
    }
}
