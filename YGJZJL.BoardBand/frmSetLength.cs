using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YGJZJL.BoardBand
{
    public enum CodeType_XC
    {
        DEFAULT,
        BC
    }

    public partial class frmSetSpec : Form
    {
        private bool _LastSuccess = false;
        private string _LastBatchNo = "";
        private string _LastOrderNo = "";
        private string _LastSpec = "";
        private string _LastLength = "";

        private CodeType_XC _CODETYPE = CodeType_XC.DEFAULT;
        private ProduceLine _PRODUCELINE = ProduceLine.GX;

        public string BATCHNO
        {
            get
            {
                return cbx_BatchNo.Checked ? Edt_BatchNo.Text.Trim() : "";
            }
        }

        public string ORDERNO
        {
            get
            {
                return Edt_OrderNo.Text.Trim();
            }
        }

        public string SPEC
        {
            get
            {
                return Edt_Spec.Text.Trim();
            }
        }

        public string LEN
        {
            get
            {
                return Edt_Length.Text.Trim();
            }
        }

        public frmSetSpec(ProduceLine ProdLine, CodeType_XC CodeType, bool Success, string BatchNo, string OrderNo, string Spec, string Length)
        {
            InitializeComponent();

            this._LastBatchNo = BatchNo;
            this._LastOrderNo = OrderNo;
            this._LastSpec = Spec;
            this._LastLength = Length;
            this._LastSuccess = Success;

            this._CODETYPE = CodeType;
            this._PRODUCELINE = ProdLine;

            if (_PRODUCELINE == ProduceLine.GX)
            {
                Edt_Length.Clear();
                Edt_Length.Enabled = false;
            }
        }

        private bool BatchNoCheck(string strBatchNo)
        {
            string Segment = GetSign();

            if (string.IsNullOrEmpty(Segment))
                return false;

            string Segment1 = @"(\d[0-1]\d\d\d\d\d)";
            return Regex.IsMatch(strBatchNo, ("^" + Segment + Segment1 + "$"));
        }

        private string GetSign()
        {
            switch (_PRODUCELINE)
            {
                case ProduceLine.BC:
                    {
                        return "N";
                    }
                case ProduceLine.XC:
                    {
                        if (_CODETYPE == CodeType_XC.DEFAULT)
                            return "X";
                        else if (_CODETYPE == CodeType_XC.BC)
                            return "P";
                        else
                            return "X";
                    }
                case ProduceLine.GX:
                    {
                        return "G";
                    }
                case ProduceLine.ZKD:
                    {
                        return "F";
                    }
                    
                default:
                    {
                        return "";
                    }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string strSign = GetSign();
                if (string.IsNullOrEmpty(strSign))
                {
                    MessageBox.Show("产线未知，无法组批！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (cbx_BatchNo.Checked)
                {
                    if (string.IsNullOrEmpty(Edt_BatchNo.Text.Trim()))
                    {
                        MessageBox.Show("请输入轧制编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Edt_BatchNo.SelectAll();
                        Edt_BatchNo.Focus();
                        return;
                    }

                    if (!BatchNoCheck(Edt_BatchNo.Text.Trim()))
                    {
                        MessageBox.Show("轧制编号不满足编码规则（" + strSign + " ＋ 1位年份(\\d) ＋ 2位月份[0-1]\\d ＋ 4位流水号\\d\\d\\d\\d、如" + strSign + "2050001）！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Edt_BatchNo.SelectAll();
                        Edt_BatchNo.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(Edt_OrderNo.Text.Trim()))
                {
                    MessageBox.Show("请输入生产订单号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Edt_OrderNo.SelectAll();
                    Edt_OrderNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(Edt_Spec.Text.Trim()))
                {
                    MessageBox.Show("未指定轧制规格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Edt_Spec.SelectAll();
                    Edt_Spec.Focus();
                    return;
                }

                if (_PRODUCELINE != ProduceLine.GX)
                {
                    if (string.IsNullOrEmpty(Edt_Length.Text.Trim()))
                    {
                        MessageBox.Show("未指定定尺长度！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Edt_Length.SelectAll();
                        Edt_Length.Focus();
                        return;
                    }

                    decimal dLen = 0.0M;

                    if (!(decimal.TryParse(Edt_Length.Text.Trim(), out dLen)))
                    {
                        MessageBox.Show("定尺长度必须是数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Edt_Length.SelectAll();
                        Edt_Length.Focus();
                        return;
                    }

                    if (dLen <= 0)
                    {
                        MessageBox.Show("定尺长度必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Edt_Length.SelectAll();
                        Edt_Length.Focus();
                        return;
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void cbx_BatchNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Edt_BatchNo.Enabled = cbx_BatchNo.Checked;

                if (!cbx_BatchNo.Checked)
                {
                    Edt_BatchNo.Tag = Edt_BatchNo.Text;
                    Edt_BatchNo.Text = "自动";

                    Edt_OrderNo.SelectAll();
                    Edt_OrderNo.Focus();
                }
                else
                {
                    if (Edt_BatchNo.Tag != null)
                    {
                        Edt_BatchNo.Text = Convert.ToString(Edt_BatchNo.Tag);
                    }

                    if (Edt_BatchNo.Text.Contains("自动"))
                    {
                        Edt_BatchNo.Clear();
                    }

                    Edt_BatchNo.SelectAll();
                    Edt_BatchNo.Focus();
                }
            }
            catch { }
        }

        private void Edt_BatchNo_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            if (e.Button.Key.EndsWith("INIT"))
            {
                Edt_BatchNo.Text = GetSign() + DateTime.Today.ToString("yyMM").Substring(1);
                Edt_BatchNo.Select(Edt_BatchNo.Text.Length, 0);
                Edt_BatchNo.Focus();
            }
        }

        private void Edt_OrderNo_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            if (_LastSuccess && e.Button.Key.EndsWith("LAST"))
            {
                if (!Edt_OrderNo.Text.Equals(this._LastOrderNo))
                {
                    Edt_OrderNo.Text = this._LastOrderNo;
                    Edt_OrderNo.Focus();
                }
                else
                {
                    Edt_OrderNo.Clear();
                    Edt_OrderNo.Focus();
                }
            }
        }

        private void Edt_Spec_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            if (_LastSuccess && e.Button.Key.EndsWith("LAST"))
            {
                if (!Edt_Spec.Text.Equals(this._LastSpec))
                {
                    Edt_Spec.Text = this._LastSpec;
                    Edt_Spec.Focus();
                }
                else
                {
                    Edt_Spec.Clear();
                    Edt_Spec.Focus();
                }
            }
        }

        private void Edt_Length_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            if (_PRODUCELINE != ProduceLine.GX && _LastSuccess && e.Button.Key.EndsWith("LAST"))
            {
                if (!Edt_Length.Text.Equals(this._LastLength))
                {
                    Edt_Length.Text = this._LastLength;
                    Edt_Length.Focus();
                }
                else
                {
                    Edt_Length.Clear();
                    Edt_Length.Focus();
                }
            }
        }
    }
}