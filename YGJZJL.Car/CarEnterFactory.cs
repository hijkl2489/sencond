using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;

namespace YGJZJL.Car
{
    public partial class CarEnterFactory : FrmBase
    {
        GetBaseInfo m_BaseInfo = new GetBaseInfo();
        int rowno = -1;
        bool m_bRunning = false;
        public delegate void BindData2GridDelegate();//绑定委托
        private BindData2GridDelegate m_BindData2GridDelegate;//建立委托变量
        public delegate void ClearCardNoDelegate();//绑定委托
        private ClearCardNoDelegate m_ClearCardNoDelegate;//建立委托变量
        DateTime m_preReadTime = DateTime.Now;
        String username = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName().Trim();//获得用户
        //String username = "harpor";

        //读卡器操作
        //private System.Threading.Thread m_hThread = null;
        //bool m_bRunning = false;
        Color color1 = Color.LimeGreen;
        Color color2 = Color.IndianRed;
        string m_enterFacNo = string.Empty;
        

        PublicComponent.CoolReader m_Reader = null;
        //读卡器操作

        public CarEnterFactory()
        {
            InitializeComponent();
        }

        private void CarEnterFactory_Load(object sender, EventArgs e)
        {
            try
            {
                string[] portnames = System.IO.Ports.SerialPort.GetPortNames();//获取计算机串口数组
                if (portnames == null || portnames.Length < 1)
                {
                    portnames = new string[10] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10" };
                }
                comboSerialNO.DataSource = portnames;
            }
            catch (Exception eex3)
            {

            }
            this.ultraGroupBox3.Dock = DockStyle.Fill;
            this.ultraGroupBox4.Dock = DockStyle.Fill;
            clearCardNo();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "ReadCard":
                    {
                        startCarCardScan();
                        break;
                    }

                case "LetGo"://放行
                    {
                        if (tbCardNo.Text == string.Empty)
                        {
                            MessageBox.Show("卡号不能为空");
                            return;
                        }

                        if (tbCarNo.Text == string.Empty)
                        {
                            MessageBox.Show("车号不能为空");
                            return;
                        }
                        //查看对应的卡号和车号是否存在
                        string strCardIsExist = "select fs_enterfacno from dt_enterfacrecord where fs_cardnumber='" + tbCardNo.Text.ToString().Trim().Replace("'", "''") + "'";
                        strCardIsExist += " and fs_carno='" + tbCarNo.Text.ToString().Trim().Replace("'", "''") + "'";

                        DataTable dtCardIsExist = new DataTable();
                        CoreClientParam selectccp = new CoreClientParam();
                        selectccp.ServerName = "ygjzjl.carcard";
                        selectccp.MethodName = "queryByClientSql";
                        selectccp.ServerParams = new object[] { strCardIsExist };
                        selectccp.SourceDataTable = dtCardIsExist;
                        this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
                        if (dtCardIsExist.Rows.Count == 0)
                        {
                            insertNewRecord();
                            MessageBox.Show("入厂成功！");
                            clearCardNo();
                        }
                        else
                        {
                            dtCardIsExist.Clear();
                            string strIsEnter = "select fs_enterfacno from dt_enterfacrecord where fs_carno='" + tbCarNo.Text.ToString().Trim().Replace("'", "''") + "'";
                            strIsEnter += " and fs_cardnumber='" + tbCardNo.Text.ToString().Trim().Replace("'", "''") + "' and FN_ENTERFACFLAG=1";
                            selectccp.ServerParams = new object[] { strIsEnter };
                            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);

                            if (dtCardIsExist.Rows.Count > 0)
                            {
                                //判断车辆是否走完整个称重流程，如果没有则提示

                                //记录出厂时间，并标识为已出厂
                                string strEnterFacNoTemp = dtCardIsExist.Rows[0]["fs_enterfacno"].ToString();
                                string strExitPlace = string.Empty;
                                string strExitChecker = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName().Trim();
                                string updateSql = "update dt_enterfacrecord set FD_EXITFACTIME=sysdate,FS_EXITFACPLACE='" + strExitPlace + "',FS_EXITFACCHECKER='" + strExitChecker + "',";
                                updateSql += "FS_EXITFACREMARK='" + tbRemark.Text.Trim().Replace("'", "''") + "',FN_ENTERFACFLAG=2 where fs_enterfacno='" + strEnterFacNoTemp + "'";
                                CoreClientParam insertccp = new CoreClientParam();
                                insertccp.ServerName = "ygjzjl.carcard";
                                insertccp.MethodName = "updateByClientSql";
                                insertccp.ServerParams = new object[] { updateSql };
                                this.ExecuteNonQuery(insertccp, CoreInvokeType.Internal);
                                if (insertccp.ReturnCode == 0) //更新成功
                                {
                                    MessageBox.Show("出厂成功！");
                                    clearCardNo();
                                }
                            }
                            else
                            {
                                //新增入厂记录
                                insertNewRecord();
                                MessageBox.Show("入厂成功！");
                                clearCardNo();
                            }
                        }
                    }
                    break;
            }
        }

        public string GetEnterFacNo()
        {
            string strResult = string.Empty;
            string strCardIsExist = "select max(fs_enterfacno) fs_enterfacno from dt_enterfacrecord where substr(fs_enterfacno,0,8)=to_char(sysdate,'yyyymmdd')";

            DataTable testdatatable = new DataTable();
            CoreClientParam selectccp = new CoreClientParam();
            selectccp.ServerName = "ygjzjl.carcard";
            selectccp.MethodName = "queryByClientSql";
            selectccp.ServerParams = new object[] { strCardIsExist };
            selectccp.SourceDataTable = testdatatable;
            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
            if (testdatatable.Rows[0]["fs_enterfacno"].ToString() == string.Empty)
            {
                strResult = System.DateTime.Today.ToString("yyyyMMdd") + "0001";
            }
            else
            {
                string strTemp1 = testdatatable.Rows[0]["fs_enterfacno"].ToString().Substring(0, 8);
                string strTemp2 = testdatatable.Rows[0]["fs_enterfacno"].ToString().Substring(8, 4);
                string strTemp3 = (Convert.ToInt16(strTemp2) + 1).ToString().PadLeft(4, '0');
                strResult = strTemp1 + strTemp3;
            }

            return strResult;
        }

        private void insertNewRecord()
        {
            //车辆入厂，插入新记录
            string strEnterFacNo = GetEnterFacNo();//获取入厂流水号
            string strEnterPlace = string.Empty;
            string strEnterChecker = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName().Trim();
            string insertSql = "insert into dt_enterfacrecord (FS_ENTERFACNO,FS_CARDNUMBER,FS_CARNO,FD_ENTERFACTIME,FS_ENTERFACPLACE,FS_ENTERFACCHECKER,";
            insertSql += "FS_ENTERFACREMARK,FN_ENTERFACFLAG) values ('" + strEnterFacNo + "','" + tbCardNo.Text.ToString().Replace("'", "''") + "',";
            insertSql += "'" + tbCarNo.Text.ToString().Replace("'", "''") + "',sysdate,'" + strEnterPlace + "','" + strEnterChecker + "','";
            insertSql += tbRemark.Text.Trim().Replace("'", "''") + "',1)";
            CoreClientParam insertccp = new CoreClientParam();
            insertccp.ServerName = "ygjzjl.carcard";
            insertccp.MethodName = "updateByClientSql";
            insertccp.ServerParams = new object[] { insertSql };
            this.ExecuteNonQuery(insertccp, CoreInvokeType.Internal);
            if (insertccp.ReturnCode == 0) //更新成功
            {
                clearCardNo();
            }
        }

        /// <summary>
        /// 启动读卡器
        /// </summary>
        private void startCarCardScan()
        {

            if (m_Reader == null)
            {
                short serialno = -1;
                try
                {
                    serialno = (short)(short.Parse(comboSerialNO.Text.Replace("COM", "")) - 1);
                }
                catch (Exception ee4)
                {
                    MessageBox.Show("请选择串口号！");
                    comboSerialNO.BackColor = Color.Red;
                    return;
                }


                m_Reader = new YGJZJL.PublicComponent.CoolReader(serialno, 115200);
                if (!m_Reader.Open())
                {
                    MessageBox.Show("串口打开失败，请检查设备、串口号设置！");
                    m_Reader = null;
                    comboSerialNO.BackColor = Color.Red;
                    return;
                }
                //m_Reader.StartUse();

                m_bRunning = true;

                //m_MainThreadCapPicture = new duka(DateCollect);
                //Invoke(m_MainThreadCapPicture);

                System.Threading.Thread m_hThread = new System.Threading.Thread(DateCollect);
                m_hThread.Start();
                comboSerialNO.BackColor = Color.Lime;
            }

        }

        /// <summary>
        /// 停止读卡器
        /// </summary>
        private void stopCarCardScan()
        {
            try
            {
                m_bRunning = false;
                m_Reader.StopUse();
            }
            catch (Exception ex4)
            {

            }

        }

        /// <summary>
        /// 循环扫描
        /// </summary>
        private void DateCollect()
        {
            while (m_bRunning)
            {
                try
                {
                    if (this.tbCardNo.Text == "")
                    {
                        System.Threading.Thread.Sleep(400);
                        m_Reader.ReadData();
                        if (m_Reader.CardNo != null && !m_Reader.CardNo.Trim().Equals(""))
                        {
                            m_BindData2GridDelegate = new BindData2GridDelegate(BindData2Grid);
                            Invoke(m_BindData2GridDelegate);
                            m_preReadTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        if (m_preReadTime.AddSeconds(30) < DateTime.Now)
                        {
                            m_ClearCardNoDelegate = new ClearCardNoDelegate(clearCardNo);
                            Invoke(m_ClearCardNoDelegate);
                        }
                    }
                    m_Reader.CardNo = "";
                }
                catch (Exception eex4)
                {
                    //MessageBox.Show("请不要长时间放置同一张卡在刷卡器上！");
                }

            }

        }

        private void CarEnterFactory_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopCarCardScan();
        }

        public void BindData2Grid()
        {
            //由卡号带出绑定的车号
            string strCardIsExist = "select t.fs_sequenceno,t.FS_BINDCARNO from BT_CARDMANAGE t where t.fs_sequenceno='" + m_Reader.CardNo + "'";

            DataTable testdatatable = new DataTable();
            CoreClientParam selectccp = new CoreClientParam();
            selectccp.ServerName = "ygjzjl.carcard";
            selectccp.MethodName = "queryByClientSql";
            selectccp.ServerParams = new object[] { strCardIsExist };
            selectccp.SourceDataTable = testdatatable;
            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
            if (testdatatable.Rows.Count == 0)
            {
                MessageBox.Show("该还未注册，不能使用！");
                return;
            }
            else
            {
                tbCardNo.Text = m_Reader.CardNo;
                tbCarNo.Text = testdatatable.Rows[0]["FS_BINDCARNO"].ToString();
            }

            if (isEnterFac(m_Reader.CardNo))
            {
                //出厂显示计量数据
                if (m_enterFacNo == string.Empty)
                {
                    MessageBox.Show("入厂流水号为空！");
                    return;
                }
                else
                {
                    showWeightInfo(m_enterFacNo);
                }
            }
            else
            {
                //进厂显示预报数据
                if (tbCarNo.Text == string.Empty)
                {
                    this.ultraGroupBox4.Visible = true;
                    this.ultraGroupBox3.Visible = false;
                }
                else
                {
                    showProdictInfo(tbCarNo.Text.Trim());
                }
            }
        }

        public void clearCardNo()
        {
            this.tbCardNo.Text = string.Empty;
            this.tbCarNo.Text = string.Empty;
            this.tbRemark.Text = string.Empty;
            this.ultraGroupBox3.Visible = false;
            this.ultraGroupBox4.Visible = false;
        }

        public void showProdictInfo(string strCarNo)
        {
            this.ultraGroupBox4.Visible = true;
            this.ultraGroupBox3.Visible = false;
            string strSelectSql = "select t.FS_PLANCODE,t.FS_MATERIALNAME,t.FS_SENDERSTORE,t.FS_PROVIDER,t.FN_SENDGROSSWEIGHT,t.FN_SENDTAREWEIGHT,t.FN_SENDNETWEIGHT from dt_weightplan t ";
            strSelectSql += "where  t.FS_CARNO='" + strCarNo.Replace("'","''").Replace(" ","") + "'";
            CoreClientParam selectccp = new CoreClientParam();
            selectccp.ServerName = "ygjzjl.carcard";
            selectccp.MethodName = "queryByClientSql";
            selectccp.ServerParams = new object[] { strSelectSql };
            dataSet1.Tables["车辆预报信息"].Clear();
            selectccp.SourceDataTable = dataSet1.Tables["车辆预报信息"];
            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
        }

        public void showWeightInfo(string strEnterFacNo)
        {
            this.ultraGroupBox3.Visible = true;
            this.ultraGroupBox4.Visible = false;
            hiddenLables();

            string strSelectSql="select to_char(a.fd_enterfactime,'YYYY-MM-DD HH24:MI:SS') fd_enterfactime,a.fs_enterfacplace,b.fs_grosspoint,to_char(b.fd_grossdatetime,'YYYY-MM-DD HH24:MI:SS') fd_grossdatetime,";
            strSelectSql+="b.fs_unloadplace,to_char(b.fd_unloadtime,'YYYY-MM-DD HH24:MI:SS') fd_unloadtime,b.FS_TAREPOINT,to_char(b.fd_taredatetime,'YYYY-MM-DD HH24:MI:SS') fd_taredatetime ";
            strSelectSql += "from dt_enterfacrecord a,dt_carweight_weight b where a.fs_enterfacno=b.fs_enterfacno and a.fs_enterfacno='"+strEnterFacNo+"'";
            DataTable dt = new DataTable();
            CoreClientParam selectccp = new CoreClientParam();
            selectccp.ServerName = "ygjzjl.carcard";
            selectccp.MethodName = "queryByClientSql";
            selectccp.ServerParams = new object[] { strSelectSql };
            selectccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);

            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                if (dt.Rows[0]["fd_enterfactime"].ToString() == string.Empty)//入厂时间、地点
                {
                    lbEnter.Appearance.BackColor = color2;
                    lbEnterPlace.Visible = false;
                    lbEnterTime.Visible = false;
                }
                else
                {
                    lbEnter.Appearance.BackColor = color1;
                    lbEnterPlace.Appearance.BackColor = color1;
                    lbEnterTime.Appearance.BackColor = color1;
                    lbEnterPlace.Visible = true;
                    lbEnterTime.Visible = true;
                    lbEnterPlace.Text = "地点:" + dt.Rows[0]["fs_enterfacplace"].ToString();
                    lbEnterTime.Text = "时间:" + dt.Rows[0]["fd_enterfactime"].ToString();
                }

                if (dt.Rows[0]["fd_grossdatetime"].ToString() == string.Empty)//一次计量时间、地点
                {
                    lbFirstWeight.Appearance.BackColor = color2;
                    lbFirstWeightTime.Visible = false;
                    lbFirstWeightWeigh.Visible = false;
                }
                else
                {
                    lbFirstWeight.Appearance.BackColor = color1;
                    lbFirstWeightWeigh.Appearance.BackColor = color1;
                    lbFirstWeightTime.Appearance.BackColor = color1;
                    lbFirstWeightWeigh.Visible = true;
                    lbFirstWeightTime.Visible = true;
                    lbFirstWeightWeigh.Text = "地点:" + dt.Rows[0]["fs_grosspoint"].ToString();
                    lbFirstWeightTime.Text = "时间:" + dt.Rows[0]["fd_grossdatetime"].ToString();
                }

                if (dt.Rows[0]["fd_unloadtime"].ToString() == string.Empty)//卸货时间、地点
                {
                    lbUnload.Appearance.BackColor = color2;
                    lbUnloadPlace.Visible = false;
                    lbUnloadTime.Visible = false;
                }
                else
                {
                    lbUnload.Appearance.BackColor = color1;
                    lbUnloadPlace.Appearance.BackColor = color1;
                    lbUnloadTime.Appearance.BackColor = color1;
                    lbUnloadPlace.Visible = true;
                    lbUnloadTime.Visible = true;
                    lbUnloadPlace.Text = "地点:" + dt.Rows[0]["fs_unloadplace"].ToString();
                    lbUnloadTime.Text = "时间:" + dt.Rows[0]["fd_unloadtime"].ToString();
                }

                if (dt.Rows[0]["fd_taredatetime"].ToString() == string.Empty)//二次计量时间、地点
                {
                    lbSecondWeight.Appearance.BackColor = color2;
                    lbSecondWeightTime.Visible = false;
                    lbSecondWeightWeigh.Visible = false;
                }
                else
                {
                    lbSecondWeight.Appearance.BackColor = color1;
                    lbSecondWeightWeigh.Appearance.BackColor = color1;
                    lbSecondWeightTime.Appearance.BackColor = color1;
                    lbSecondWeightWeigh.Visible = true;
                    lbSecondWeightTime.Visible = true;
                    lbSecondWeightWeigh.Text = "地点:" + dt.Rows[0]["FS_TAREPOINT"].ToString();
                    lbSecondWeightTime.Text = "时间:" + dt.Rows[0]["fd_taredatetime"].ToString();
                }

                lbExitFac.Appearance.BackColor = color2;
            }
        }

        private bool isEnterFac(string strCarNo)
        {
            string strIsEnter = "select fs_enterfacno from dt_enterfacrecord where fs_carno='" + tbCarNo.Text.ToString().Trim().Replace("'", "''") + "'";
            strIsEnter += " and fs_cardnumber='" + strCarNo + "' and FN_ENTERFACFLAG=1";
            DataTable dt = new DataTable();
            CoreClientParam selectccp = new CoreClientParam();
            selectccp.ServerName = "ygjzjl.carcard";
            selectccp.MethodName = "queryByClientSql";
            selectccp.ServerParams = new object[] { strIsEnter };
            selectccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                m_enterFacNo = dt.Rows[0]["fs_enterfacno"].ToString();
                return true;
            }
            else
            {
                m_enterFacNo = string.Empty;
                return false;
            }
        }

        private void hiddenLables()
        {
            this.lbEnterPlace.Visible = false;
            this.lbEnterTime.Visible = false;
            this.lbFirstWeightWeigh.Visible = false;
            this.lbFirstWeightTime.Visible = false;
            this.lbUnloadPlace.Visible = false;
            this.lbUnloadTime.Visible = false;
            this.lbSecondWeightWeigh.Visible = false;
            this.lbSecondWeightTime.Visible = false;
        }
    }
}
