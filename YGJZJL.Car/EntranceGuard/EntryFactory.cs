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
using YGJZJL.CarSip.Client.App;

namespace YGJZJL.Car.EntranceGuard
{
    public partial class EntryFactory : FrmBase
    {
        private HgIcCard _hgIcCard = null;
        public delegate void CardDelegate(CardData cardData);//绑定委托
        private CardDelegate _readCard;//建立委托变量
        private string _dischargeflag = "";
        private bool _isReadCard = true;
        private int _waitSeconds = 3;

        public EntryFactory()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "ReadCard":
                    if (e.Tool.SharedProps.Caption.Equals("启动"))
                    {
                        startCarCardScan();
                    }
                    else
                    {
                        stopCarCardScan();
                    }
                    break;
                case "LetGo"://放行
                    LetGo();
                    break;
                case "Query":
                    string condtion = "";
                    condtion += ultraDateTimeEditor1.Value != null ? " AND (TO_CHAR(T.FD_ENTERFACTIME,'YYYYMMDD') >= '"+ultraDateTimeEditor1.DateTime.ToString("yyyyMMdd")+"'" : " AND (1=1";
                    condtion += ultraDateTimeEditor2.Value != null ? " AND TO_CHAR(T.FD_EXITFACTIME,'YYYYMMDD') <= '" + ultraDateTimeEditor2.DateTime.ToString("yyyyMMdd") + "'" : "";
                    condtion += !string.IsNullOrEmpty(comboBox1.Text.Trim()) || !string.IsNullOrEmpty(textBox1.Text.Trim()) ? " AND T.FS_CARNO LIKE '%"+comboBox1.Text.Trim()+textBox1.Text.Trim()+"%'" : "";
                    condtion += ultraCheckEditor1.Checked ? " AND T.FN_ENTERFACFLAG = 2)" : "";
                    if (!ultraCheckEditor1.Checked)
                    {
                        condtion += !string.IsNullOrEmpty(condtion.Trim()) ? " OR NVL(T.FN_ENTERFACFLAG,1) = 1)" : "";
                    }
                    Query(condtion);
                    break;
            }
        }

        private void Query(string condition)
        {
            string sql = @"SELECT T.FS_ENTERFACNO,
                                   --T.FS_PLANCODE,
                                   T.FS_CARDNUMBER,
                                   T.FS_CARNO,
                                   TO_CHAR(T.FD_ENTERFACTIME,'YYYY-MM-DD HH24:MI:SS') FD_ENTERFACTIME,
                                   T.FS_ENTERFACPLACE,
                                   (SELECT U.USERNAME FROM CORE_APP_USER U WHERE U.USERID = T.FS_ENTERFACCHECKER) FS_ENTERFACCHECKER,
                                   T.FS_ENTERFACREMARK,
                                   T.FN_ENTERFACFLAG,
                                   DECODE(T.FN_ENTERFACFLAG,2,'出厂','入厂') FN_ENTERFACFLAGNAME,
                                   TO_CHAR(T.FD_EXITFACTIME,'YYYY-MM-DD HH24:MI:SS') FD_EXITFACTIME,
                                   T.FS_EXITFACPLACE,
                                   (SELECT U.USERNAME FROM CORE_APP_USER U WHERE U.USERID = T.FS_EXITFACCHECKER) FS_EXITFACCHECKER,
                                   T.FS_EXITFACREMARK
                                   
                              FROM DT_ENTERFACRECORD T
                             WHERE 1=1
                               {0}
                             ORDER BY T.FD_ENTERFACTIME DESC";
            dataTable2.Clear();
            dataTable1.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "queryBySql";
            ccp.ServerParams = new object[] { string.Format(sql,condition), new ArrayList() };
            ccp.SourceDataTable = dataTable1;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            string sql2 = "select FS_MATERIALNAME,fn_netweight,fs_cardnumber,FHDW_BHTOMC(FS_SENDER) FS_SENDER,SHDW_BHTOMC(FS_RECEIVER) FS_RECEIVER,FN_GROSSWEIGHT,FS_GROSSPOINT,FS_GROSSPERSON,to_char(FD_GROSSDATETIME,'YYYY-MM-DD hh24:mi:ss') AS FD_GROSSDATETIME ,FN_TAREWEIGHT,FS_TAREPOINT,FS_TAREPERSON,to_char(FD_TAREDATETIME,'YYYY-MM-DD hh24:mi:ss') AS FD_TAREDATETIME from dt_carweight_weight where fs_cardnumber in (select T.FS_CARDNUMBER from DT_ENTERFACRECORD T where 1=1  " + condition + ")";


            CoreClientParam ccp2 = new CoreClientParam();
            ccp2.ServerName = "com.dbComm.DBComm";
            ccp2.MethodName = "queryBySql";
            ccp2.ServerParams = new object[] { sql2, new ArrayList() };
            ccp2.SourceDataTable = dataTable2;
            this.ExecuteQueryToDataTable(ccp2, CoreInvokeType.Internal);

        }

        private void LetGo()
        {
            if (string.IsNullOrEmpty(txtCardNo.Text.Trim()))
            {
                MessageBox.Show("请刷卡后再放行！");
                return;
            }
          
            string sql = "{call YG_MCMS_CARWEIGHT.ENTRANCE_GUARD(?,?,?,?,?,?)}";
            Hashtable ht = new Hashtable();
            ht.Add("I1", txtCardNo.Text.Trim());
            ht.Add("I2", cbCH.Text.Trim() + tbCarNo.Text.Trim());
            ht.Add("I3", UserInfo.GetUserID());
            ht.Add("I4", !string.IsNullOrEmpty(CustomInfo) ? CustomInfo + "#门" : "");
            ht.Add("I5", tbRemark.Text.Trim());
            ht.Add("I6",this._dischargeflag);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "executeProcedureBySql2";
            ccp.ServerParams = new object[] { sql, ht };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode == 0)
            {
                MessageBox.Show("操作成功！");
            }
            Clear();

            string condtion = "";
            
            condtion += ultraDateTimeEditor1.Value != null ? " AND (TO_CHAR(T.FD_ENTERFACTIME,'YYYYMMDD') >= '" + ultraDateTimeEditor1.DateTime.ToString("yyyyMMdd") + "'" : " AND (1=1";
            condtion += ultraDateTimeEditor2.Value != null ? " AND TO_CHAR(T.FD_EXITFACTIME,'YYYYMMDD') <= '" + ultraDateTimeEditor2.DateTime.ToString("yyyyMMdd") + "'" : "";
            condtion += !string.IsNullOrEmpty(comboBox1.Text.Trim()) || !string.IsNullOrEmpty(textBox1.Text.Trim()) ? " AND T.FS_CARNO LIKE '%" + comboBox1.Text.Trim() + textBox1.Text.Trim() + "%'" : "";
            condtion += ultraCheckEditor1.Checked ? " AND T.FN_ENTERFACFLAG = 2)" : "";
            if (!ultraCheckEditor1.Checked)
            {
                condtion += !string.IsNullOrEmpty(condtion.Trim()) ? " OR NVL(T.FN_ENTERFACFLAG,1) = 1)" : "";
            }
            Query(condtion);
        }

        private void Clear()
        {
            txtCardNo.Text = "";
            cbCH.Text = "";
            tbCarNo.Text = "";
            tbRemark.Text = "";
        }

        private void EntryFactory_Load(object sender, EventArgs e)
        {
            ultraDateTimeEditor2.Value = null;

            try
            {
                string[] portnames = System.IO.Ports.SerialPort.GetPortNames();//获取计算机串口数组
                if (portnames == null || portnames.Length < 1)
                {
                    portnames = new string[10] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10" };
                }
                comboSerialNO.DataSource = portnames;

                _hgIcCard = new HgIcCard();
            }
            catch (Exception eex3)
            {

            }
        }

        /// <summary>
        /// 启动读卡器
        /// </summary>
        private void startCarCardScan()
        {
            if (_hgIcCard != null)
            {
                _hgIcCard.CardChanged += new CardChangedEventHandler(_hgIcCard_CardChanged);
                if (_hgIcCard.Init(comboSerialNO.Text) && _hgIcCard.Open())
                {
                    ultraToolbarsManager1.Toolbars[0].Tools["ReadCard"].SharedProps.Caption = "关闭";
                    comboSerialNO.Enabled = false;
                }
            }
        }

        private void _hgIcCard_CardChanged(object sender, CardEventArgs e)
        {
            Invoke(new CardDelegate(ReadCard), new object[] { e.Value });
        }

        private void ReadCard(CardData cardData)
        {
            if (!_isReadCard)
                return;
            _isReadCard = false;
            txtCardNo.Text = cardData.CardNo;
            this._dischargeflag = cardData.UnloadFlag;
            if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
            {
                //由卡号带出绑定的车号
                string sql = "select t.fs_sequenceno,t.FS_BINDCARNO from BT_CARDMANAGE t where t.fs_sequenceno='" + cardData.CardNo + "'";

                DataTable dt = new DataTable();
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "queryBySql";
                ccp.ServerParams = new object[] { sql, new ArrayList() };
                ccp.SourceDataTable = dt;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("该卡还未注册，不能使用！");
                    return;
                }
                else
                {
                    txtCardNo.Text = cardData.CardNo;

                    //查找进厂记录
                    string sql2 = "select t.fs_carno from dt_enterfacrecord t where t.fs_cardnumber = '"+cardData.CardNo+"' and nvl(t.fn_enterfacflag,1) = 1";
                    DataTable dt2 = new DataTable();
                    CoreClientParam ccp2 = new CoreClientParam();
                    ccp2.ServerName = "com.dbComm.DBComm";
                    ccp2.MethodName = "queryBySql";
                    ccp2.ServerParams = new object[] { sql2, new ArrayList() };
                    ccp2.SourceDataTable = dt2;
                    this.ExecuteQueryToDataTable(ccp2, CoreInvokeType.Internal);
                    if (dt2.Rows.Count > 0)
                    {
                        string carNo = dt2.Rows[0]["FS_CARNO"].ToString();
                        if (!string.IsNullOrEmpty(carNo) && carNo.Length > 1)
                        {
                            if (carNo.Length == 7)
                            {
                                this.cbCH.Text = carNo.Substring(0, 2);
                                this.tbCarNo.Text = carNo.Substring(2);
                            }
                            else if (carNo.Length == 8)
                            {
                                this.cbCH.Text = carNo.Substring(0, 3);
                                this.tbCarNo.Text = carNo.Substring(3);
                            }
                            else
                            {
                                this.cbCH.Text = carNo.Substring(0, 1);
                                this.tbCarNo.Text = carNo.Substring(1);
                            }
                            //自动出厂
                         /*   if (ultraCheckEditor2.Checked)
                            {
                                LetGo();
                            }*/
                        }
                        //return;
                    }

                    string text = dt.Rows[0]["FS_BINDCARNO"].ToString();
                    
                    if (!string.IsNullOrEmpty(text) && text.Length > 1)
                    {
                        if (text.Length == 7)
                        {
                            this.cbCH.Text = text.Substring(0, 2);
                            this.tbCarNo.Text = text.Substring(2);
                        }
                        else if (text.Length == 8)
                        {
                            this.cbCH.Text = text.Substring(0, 3);
                            this.tbCarNo.Text = text.Substring(3);
                        }
                        else
                        {
                            this.cbCH.Text = text.Substring(0, 1);
                            this.tbCarNo.Text = text.Substring(1);
                        }
                    }
                 /*   else
                    {
                        this.cbCH.Text = "";
                        this.tbCarNo.Text = "";
                    }*/
                    if (checkBox1.Checked)
                    {
                        LetGo();
                    }
                }
            }

            System.Threading.Thread th = new System.Threading.Thread(resetReadStatus);
            th.Start();
        }

        private void resetReadStatus()
        {
            System.Threading.Thread.Sleep(_waitSeconds * 1000);
            _isReadCard = true;
        }

        /// <summary>
        /// 停止读卡器
        /// </summary>
        private void stopCarCardScan()
        {
            try
            {
                if (_hgIcCard != null & _hgIcCard.Close())
                {
                    ultraToolbarsManager1.Toolbars[0].Tools["ReadCard"].SharedProps.Caption = "启动";
                    comboSerialNO.Enabled = true;
                }
            }
            catch (Exception ex4)
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LetGo();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.button1.Enabled = true;
                this.checkBox1.Text = "是否手动放行";
            }
            else {
                this.button1.Enabled = false;
                this.checkBox1.Text = "是否自动放行";
            }
            
        }
    }
}
