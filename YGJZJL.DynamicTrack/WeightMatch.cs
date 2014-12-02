using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using System.Threading;
using System.IO;
using CoreFS.CA06;
using YGJZJL.DynamicTrack;
using INI;

namespace YGJZJL.DynamicTrack
{
    public partial class WeightMatch : FrmBase
    {
        private Thread thread = null;
        /// <summary>
        /// 报警最大速度限制
        /// </summary>
        private double _alarmMaxSpeed = 10;

        private int grid2Index = -1;

        /// <summary>
        /// 计量点个数
        /// </summary>
        private int m_nPointCount;

        /// <summary>
        /// 计量点数组
        /// </summary>
        private PoundRoom[] m_PoundRoomArray;
        
        /// <summary>
        /// 放大图片句柄
        /// </summary>
        int BigChannel = 0;

        /// <summary>
        /// 当前放大的是哪一个通道
        /// </summary>
        int m_CurSelBigChannel = -1;

        /// <summary>
        /// 流媒体服务器IP
        /// </summary>
        private string _videoServerIp = "";

        /// <summary>
        /// 流媒体服务器端口
        /// </summary>
        private string _videoServerPort = "";

        /// <summary>
        /// 存储一列火车已匹配罐号
        /// </summary>
        private ArrayList matchedPots = new ArrayList();

        private string preWeighTime = "";
        AccessData accdata = null;
        public WeightMatch()
        {
            InitializeComponent();
        }

        private void ultraCheckEditor1_CheckedChanged(object sender, EventArgs e)
        {
            ultraButton1.Enabled = !ultraCheckEditor1.Checked;
            if (ultraCheckEditor1.Checked)
            {
                if (thread.ThreadState == ThreadState.Suspended)
                {
                    thread.Resume();
                }
            }
            else
            {
                thread.Suspend();
            }
        }
        #region 窗体加载事件
        private void WeightMatch_Load(object sender, EventArgs e)
        {
            ultraDateTimeEditor2.Value = ultraDateTimeEditor1.DateTime.Add(TimeSpan.FromMinutes(1439));
            ultraDateTimeEditor4.Value = ultraDateTimeEditor3.DateTime.Add(TimeSpan.FromMinutes(1439));
           
            QueryWeightData();
            //   thread = new Thread(RefreshData);
            //   thread.Start();
            ultraCheckEditor1.Checked = false;
            ultraCheckEditor1.Enabled = false;
            if (CustomInfo.Equals("leader"))
            {
                ultraTextEditor2.ReadOnly = false;
               
            }
            else
            {
                ultraTextEditor2.ReadOnly = true;
            }
            ultraOptionSet2.CheckedIndex = -1;
            ultraOptionSet1.CheckedIndex = 0;
            ultraTextEditor1.BackColor = Color.Pink;
            ultraTextEditor2.BackColor = Color.Pink;


            this.QueryAndBindJLDData();//查询绑定计量点信息
            //打开视频
            this.RecordOpen(0);





        }
        #endregion

        #region 从动轨称重系统获取数据插入计量系统
        private int ReadAccessData()
        {
            //查询已接收最大序号
            DataTable dtTmp = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHTMATCH_08.SELECT", new ArrayList() };
            ccp.SourceDataTable = dtTmp;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            string maxID = "";
            if (dtTmp.Rows.Count > 0)
            {
                maxID = dtTmp.Rows[0]["FS_ITNO"].ToString();
            }
            else
            {
                return 0;
                //maxID = DateTime.Now.Add(TimeSpan.FromDays(-3)).ToString("yyyyMMdd")+"0000";
            }
            //查询未接收数据
            int retCode = 0;
            string SQL = @"select xh,mz,cs,format(sj,'yyyy-mm-dd Hh:Nn:Ss') as ssj from jlb where xh > '" + maxID + "'";
            DataSet ds = new DataSet();
            accdata = new AccessData(this.ob);
             this.accdata.CommQuery(SQL, ds);
            ArrayList weightList = ConvertDataSetToArray(ds);
            if (weightList != null && weightList.Count > 0)
            {
                //更新读取标识
                //string sqlUpd = "update jlb set rcv = '1' where xh in ";
                //sqlUpd += "('-1'";
                //foreach (object obj in weightList)
                //{
                //    sqlUpd += ",'" + ((string[])obj)[0]+"'";
                //}
                //sqlUpd += ")";
                //if (AccessData.Run(sqlUpd))
                //{
                AddTrackWeight(weightList);
                retCode = weightList.Count;
                //}
            }
            return retCode;
        }

        private ArrayList ConvertDataSetToArray(DataSet ds)
        {
            ArrayList list = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = new ArrayList();
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    string[] pws = new string[4];
                    pws[0] = dr["xh"].ToString();
                    pws[1] = dr["mz"].ToString();
                    pws[2] = dr["cs"].ToString();
                    pws[3] = dr["ssj"].ToString();
                    list.Add(pws);
                }
            }

            return list;
        }

        private void AddTrackWeight(ArrayList list)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.dynTrack.WeightMatch";
            ccp.MethodName = "insertAccessData";
            ccp.ServerParams = new object[] { list };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        #endregion

        #region 后台线程刷新动轨称重系统数据
        private void RefreshData()
        {
            while (true)
            {
                if (ReadAccessData() > 0)
                {
                    QueryWeightData();
                }
                Thread.Sleep(600000);
            }
        }
        #endregion

        #region 查询已获取重量数据
        private void QueryWeightData()
        {
            dataSet1.Tables[0].Clear();
            //DataTable dt = new DataTable();
            ArrayList list = new ArrayList();
            list.Add(ultraDateTimeEditor3.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            list.Add(ultraDateTimeEditor4.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHTMATCH_01.SELECT", list };
            ccp.SourceDataTable = dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow ugr in ultraGrid1.Rows)
            {
                try
                {
                    if (double.Parse(ugr.Cells["FN_SPEED"].Value.ToString()) > _alarmMaxSpeed)
                    {
                        ugr.Appearance.ForeColor = Color.Red;
                    }
                }
                catch (Exception e)
                {
                }
            }

            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.Rows[0].Activated = true;
                ultraGrid1_AfterRowActivate(ultraGrid1, EventArgs.Empty);
            }
        }
        #endregion

        #region 窗体关闭中止线程
        private void WeightMatch_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }

            //关闭硬盘录像机视频
            RecordClose(0);
        }
        #endregion

        #region 刷新控钮点击事件
        private void ultraButton1_Click(object sender, EventArgs e)
        {
            ReadAccessData();
            QueryWeightData();
        }
        #endregion

        private void SetControlValue(string weighNo, string weight, string weighTime)
        {
            textBox1.Text = weighNo;
            ultraTextEditor1.Text = weighTime;
            ultraTextEditor2.Text = weight;
        }

        #region 罐号匹配
        private void ultraTextEditor3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                ultraButton2_Click(sender, e);
            }
            ultraTextEditor3.Focus();
        }

        private void ultraButton2_Click(object sender, EventArgs e)
        {
            #region 输入项判断
            string potNo = ultraTextEditor3.Text.Trim();
            if (string.IsNullOrEmpty(potNo))
            {
                MessageBox.Show("请输入罐号！");
                return;
            }
            string weightNo = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(weightNo))
            {
                MessageBox.Show("请选择匹配的称重记录！");
                return;
            }
            int weighType = ultraOptionSet1.CheckedIndex;
            if (weighType < 0)
            {
                MessageBox.Show("请选择称重类型！");
                return;
            }

            double weight = 0;
            if (!double.TryParse(ultraTextEditor2.Text.Trim(), out weight) && ultraTextEditor2.Enabled)
            {
                MessageBox.Show("请输入正确的重量！");
                return;
            }

            string furnaceNo = ultraOptionSet2.CheckedIndex > -1 ? ultraOptionSet2.Value.ToString() : "";
            if (string.IsNullOrEmpty(furnaceNo))
            {
                MessageBox.Show("请选择炉座号！");
                return;
            }

            string receiveUnit = ultraOptionSet1.CheckedIndex > -1 ? ultraOptionSet1.Value.ToString() : "";
            if (string.IsNullOrEmpty(receiveUnit))
            {
                MessageBox.Show("请选择流向！");
                return;
            }

            string heatNo = ultraTextEditor5.Text.Trim();
            if (string.IsNullOrEmpty(heatNo))
            {
                MessageBox.Show("请输入炉号！");
                return;
            }

            int gridIndex = ultraGrid1.ActiveRow.Index;
            if (matchedPots.Contains(potNo))
            {
                MessageBox.Show("同一批计量数据中不能输入两次相同的罐号！");
                ultraTextEditor3.Focus();
                ultraGrid1.Rows[gridIndex].Activated = false;
                ultraGrid1.Rows[gridIndex].Activated = true;
                ultraTextEditor3.Text = "";
                return;
            }
            #endregion

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.dynTrack.WeightMatch";
            ccp.MethodName = "matchWeight";
            String userorder = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder()).ToString();
            ccp.ServerParams = new object[] { weightNo, potNo, weight.ToString()
                , weighType, UserInfo.GetUserID(), userorder, UserInfo.GetUserGroup(), ultraTextEditor1.Text.Trim() 
                , furnaceNo, receiveUnit, heatNo};
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode == 0)
            {
                //QueryWeightData();
                SetControlValue("", "", "");
                QueryMatchWeight(2);
                ultraGrid1.ActiveRow.Appearance.BackColor = Color.LightGreen;
                ultraTextEditor3.Text = "";
            }
            Next(potNo);
            //this.
        }

        private void Next(string potNo)
        {
            int gridIndex = ultraGrid1.ActiveRow.Index;
            if (gridIndex < 0)
            {
                MessageBox.Show("请选择需要匹配的过磅记录!");
                return;
            }

            string curWeighTime = ultraGrid1.ActiveRow.Cells["FD_WEIGHTTIME"].Value.ToString();
            string nextWeighTime = gridIndex + 1 < ultraGrid1.Rows.Count ? ultraGrid1.Rows[gridIndex + 1].Cells["FD_WEIGHTTIME"].Value.ToString() : "";

            if (!curWeighTime.Equals(nextWeighTime))
            {
                MessageBox.Show("本批过磅数据已计量完成！");
                ReadAccessData();
                QueryWeightData();
                ultraTextEditor3.Focus();

                matchedPots.Clear();

                //生成炉号
                ultraOptionSet2_ValueChanged(null, null);
                return;
            }
            else
            {
                matchedPots.Add(potNo);
            }

            if (ultraGrid1.Rows.Count > ultraGrid1.ActiveRow.Index + 1)
            {
                ultraGrid1.Rows[ultraGrid1.ActiveRow.Index + 1].Activated = true;
            }
        }
        #endregion

        private void ultraButton3_Click(object sender, EventArgs e)
        {
            QueryMatchWeight(1);
        }

        #region 查询已匹配计量记录
        /// <summary>
        /// 查询已匹配计量记录
        /// </summary>
        /// <param name="type"></param>
        private void QueryMatchWeight(int type)
        {
            string beginDate = ultraDateTimeEditor1.DateTime.ToString("yyyyMMddHHmm");
            string endData = ultraDateTimeEditor2.DateTime.ToString("yyyyMMddHHmm");
            dataSet1.Tables[1].Clear();
            //DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ArrayList list = new ArrayList();
            list.Add(beginDate);
            list.Add(endData);
            list.Add(beginDate);
            list.Add(endData);
            list.Add(ultraTextEditor4.Text.Trim());
            if (type == 1)
            {
                ccp.ServerParams = new object[] { "WEIGHTMATCH_05.SELECT", list };
            }
            else
            {
                ccp.ServerParams = new object[] { "WEIGHTMATCH_06.SELECT", new ArrayList() };
            }
            ccp.SourceDataTable = dataSet1.Tables[1];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            grid2Index = -1;
        }
        #endregion

        #region 罐号修改
        private void ultraButton4_Click(object sender, EventArgs e)
        {
            if (grid2Index > -1)
            {
                if (string.IsNullOrEmpty(ultraTextEditor3.Text.Trim()))
                {
                    MessageBox.Show("罐号不能为空！");
                    return;
                }
                if (!ultraGrid2.Rows[grid2Index].Cells["FN_WEIGHCOUNT"].Value.ToString().Equals("1"))
                {
                    MessageBox.Show("只能对一次匹配数据进行修改！");
                    return;
                }
                string furnaceNo = ultraOptionSet2.CheckedIndex > -1 ? ultraOptionSet2.Value.ToString() : "";
                if (string.IsNullOrEmpty(furnaceNo))
                {
                    MessageBox.Show("请选择炉座号！");
                    return;
                }

                string receiveUnit = ultraOptionSet1.CheckedIndex > -1 ? ultraOptionSet1.Value.ToString() : "";
                if (string.IsNullOrEmpty(receiveUnit))
                {
                    MessageBox.Show("请选择流向！");
                    return;
                }

                string heatNo = ultraTextEditor5.Text.Trim();
                if (string.IsNullOrEmpty(heatNo))
                {
                    MessageBox.Show("请输入炉号！");
                    return;
                }
                if (MessageBox.Show("确定要修改匹配记录吗？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ArrayList list = new ArrayList();

                    list.Add(ultraTextEditor3.Text.Trim());
                    list.Add(furnaceNo);
                    list.Add(heatNo);
                    list.Add(furnaceNo);
                    list.Add(receiveUnit);
                    list.Add(ultraGrid2.Rows[grid2Index].Cells["FS_WEIGHTNO"].Value.ToString());
                    //修改一次匹配罐号
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "com.dbComm.DBComm";
                    ccp.MethodName = "save";
                    ccp.ServerParams = new object[] { "WEIGHTMATCH_04.UPDATE", list };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    if (ccp.ReturnCode == 0)
                    {
                        QueryMatchWeight(2);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择需要修改的选项！");
            }
        }
        #endregion

        #region 全选
        private void ultraCheckEditor2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow ugr in ultraGrid1.Rows)
            {
                ugr.Cells["CHK"].Value = ultraCheckEditor2.Checked;
            }

        }
        #endregion

        #region 删除废弃记录
        private void ultraButton5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除采集的称重记录？", "玉钢MCMS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ArrayList list = new ArrayList();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow ugr in ultraGrid1.Rows)
                {
                    if (Convert.ToBoolean(ugr.Cells["CHK"].Value))
                    {
                        list.Add(ugr.Cells["FS_WEIGHTNO"].Value.ToString());
                    }
                }
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.dynTrack.WeightMatch";
                ccp.MethodName = "deleteFirstData";
                ccp.ServerParams = new object[] { list, UserInfo.GetUserID() };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.QueryWeightData();
            }
        }
        #endregion

        #region 查询记量点信息
        private void QueryAndBindJLDData()
        {
            dataTable4.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHTMATCH_07.SELECT", new ArrayList() };
            ccp.SourceDataTable = dataTable4;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            InitPound(dataTable4);
        }
        #endregion

        #region 初始化计量点
        /// <summary>
        /// 根据数据库查询BT_POINT结果初始化计量点
        /// </summary>
        /// <param name="dt"></param>
        private void InitPound(DataTable dt)
        {
            string Current = Directory.GetCurrentDirectory();//获取当前根目录
            Ini ini = new Ini(Current + "/mcms.ini");
            // 读取ini
            _videoServerIp = ini.ReadValue("VIDEOSERVER", "ip");
            _videoServerPort = ini.ReadValue("VIDEOSERVER", "port");

            if (string.IsNullOrEmpty(_videoServerIp) || string.IsNullOrEmpty(_videoServerPort))
            {
                MessageBox.Show("计量点初始化失败！");
                return;
            }
            //构建计量点列表，所用信息从查询计量点表 JL_POINTINFO 获取
            m_nPointCount = dt.Rows.Count;
            m_PoundRoomArray = new PoundRoom[m_nPointCount];
            int i = 0;
            for (i = 0; i < m_nPointCount; i++)
            {
                m_PoundRoomArray[i] = new PoundRoom();
                m_PoundRoomArray[i].POINTID = dt.Rows[i]["FS_POINTCODE"].ToString().Trim();
                m_PoundRoomArray[i].POINTNAME = dt.Rows[i]["FS_POINTNAME"].ToString().Trim();
                m_PoundRoomArray[i].POINTTYPE = dt.Rows[i]["FS_POINTTYPE"].ToString().Trim();

                m_PoundRoomArray[i].VIDEOIP = dt.Rows[i]["FS_VIEDOIP"].ToString().Trim();
                m_PoundRoomArray[i].VIDEOPORT = dt.Rows[i]["FS_VIEDOPORT"].ToString().Trim();
                m_PoundRoomArray[i].VIDEOUSER = dt.Rows[i]["FS_VIEDOUSER"].ToString().Trim();
                m_PoundRoomArray[i].VIDEOPWD = dt.Rows[i]["FS_VIEDOPWD"].ToString().Trim();
                m_PoundRoomArray[i].Signed = true;
            }
        }
        #endregion

        #region 打开计量点的硬盘录像机
        /// <summary>
        /// 打开计量点的硬盘录像机
        /// </summary>
        /// 计量点索引
        private void RecordOpen(int iPoundRoom)
        {
            if (m_nPointCount < 1 || m_PoundRoomArray == null || m_PoundRoomArray.Length < 1 || iPoundRoom >= m_nPointCount)
            {
                MessageBox.Show("请检查动态轨态衡计量点配置信息！");
                return;
            }
            PoundRoom pr = m_PoundRoomArray[iPoundRoom];

            //流媒体
            m_PoundRoomArray[iPoundRoom].Channel1 = axActiveVideo1.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, pr.Channel1, pr.VIDEOUSER, pr.VIDEOPWD));
            m_PoundRoomArray[iPoundRoom].Channel2 = axActiveVideo2.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, pr.Channel2, pr.VIDEOUSER, pr.VIDEOPWD));
            m_PoundRoomArray[iPoundRoom].Channel3 = axActiveVideo3.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, pr.Channel3, pr.VIDEOUSER, pr.VIDEOPWD));
        }
        #endregion

        #region 关闭视频
        private void RecordClose(int iPoundRoom)
        {
            if (m_nPointCount < 1 || m_PoundRoomArray == null || m_PoundRoomArray.Length < 1 || iPoundRoom >= m_nPointCount)
            {
                return;
            }

            if (m_PoundRoomArray[iPoundRoom].Signed != true)//未接管的计量点
            {
                return;
            }

            //关闭第1通道御览
            if (m_PoundRoomArray[iPoundRoom].Channel1 >= 0)
            {
                m_PoundRoomArray[iPoundRoom].Channel1 = axActiveVideo1.StopPlay();
                axActiveVideo1.Refresh();
            }

            //关闭第2通道御览
            if (m_PoundRoomArray[iPoundRoom].Channel2 >= 0)
            {
                m_PoundRoomArray[iPoundRoom].Channel2 = axActiveVideo2.StopPlay();
                axActiveVideo2.Refresh();
            }

            //关闭第3通道御览
            if (m_PoundRoomArray[iPoundRoom].Channel3 >= 0)
            {
                m_PoundRoomArray[iPoundRoom].Channel3 = axActiveVideo3.StopPlay();
                axActiveVideo3.Refresh();
            }

            //关闭第4通道御览
            if (BigChannel >= 0)
            {
                BigChannel = axActiveVideo4.StopPlay();
                axActiveVideo4.Refresh();
            }
        }
        #endregion

        #region 视频窗口单击事件
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            VideoChannelClick(1);
        }

        private void axActiveVideo1_Enter(object sender, EventArgs e)
        {
            VideoChannelClick(1);
        }

        private void axActiveVideo2_Enter(object sender, EventArgs e)
        {
            VideoChannelClick(2);
        }

        private void axActiveVideo3_Enter(object sender, EventArgs e)
        {
            VideoChannelClick(3);
        }

        private void VideoChannelClick(int channelId)
        {
            switch (channelId)
            {
                case 1:
                    panel4.BorderStyle = BorderStyle.Fixed3D;
                    panel5.BorderStyle = BorderStyle.None;
                    panel6.BorderStyle = BorderStyle.None;
                    break;
                case 2:
                    panel4.BorderStyle = BorderStyle.None;
                    panel5.BorderStyle = BorderStyle.Fixed3D;
                    panel6.BorderStyle = BorderStyle.None;
                    break;
                case 3:
                    panel4.BorderStyle = BorderStyle.None;
                    panel5.BorderStyle = BorderStyle.None;
                    panel6.BorderStyle = BorderStyle.Fixed3D;
                    break;
                default:
                    panel4.BorderStyle = BorderStyle.None;
                    panel5.BorderStyle = BorderStyle.None;
                    panel6.BorderStyle = BorderStyle.None;
                    break;
            }
        }
        #endregion

        #region 双击切换视频画面
        /// <summary>
        /// 关闭大图监视，还原小图监视
        /// </summary>
        private void CloseBigPicture(int selectPount)
        {
            int i = selectPount;

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIDEOIP == null || m_PoundRoomArray[i].VIDEOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点 
            {
                return;
            }

            if (BigChannel > 0 && m_CurSelBigChannel >= 0)
            {
                BigChannel = axActiveVideo4.StopPlay();
                BigChannel = 0;
                PoundRoom pr = m_PoundRoomArray[i];

                if (m_CurSelBigChannel == 0)
                {
                    pr.Channel1 = axActiveVideo1.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, 0, pr.VIDEOUSER, pr.VIDEOPWD));
                }
                else if (m_CurSelBigChannel == 1)
                {
                    pr.Channel2 = axActiveVideo2.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, 1, pr.VIDEOUSER, pr.VIDEOPWD));
                }
                else if (m_CurSelBigChannel == 2)
                {
                    pr.Channel3 = axActiveVideo3.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, 2, pr.VIDEOUSER, pr.VIDEOPWD));
                }

                m_CurSelBigChannel = -1;
            }
            panel11.Visible = false;
            panel11.Refresh();
           
        }

        private void OpenBigPicture(int selectPound, int iChannel)
        {
            int i = selectPound;

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIDEOIP == null || m_PoundRoomArray[i].VIDEOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            PoundRoom pr = m_PoundRoomArray[i];

            if (iChannel == 0)
            {

                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel1 >= 0)
                {
                    //m_PoundRoomArray[i].Channel1 = 0;
                    BigChannel = axActiveVideo4.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, 0, pr.VIDEOUSER, pr.VIDEOPWD));
                }
            }
            else if (iChannel == 1)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel2 >= 0)
                {
                    //m_PoundRoomArray[i].Channel2 = 0;
                    BigChannel = axActiveVideo4.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, 1, pr.VIDEOUSER, pr.VIDEOPWD));
                }
            }
            else if (iChannel == 2)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel3 >= 0)
                {
                    //m_PoundRoomArray[i].Channel3 = 0;
                    BigChannel = axActiveVideo4.PlayerUrl(GetVideoPlayStr(_videoServerIp, _videoServerPort, pr.VIDEOIP, pr.VIDEOPORT, 2, pr.VIDEOUSER, pr.VIDEOPWD));
                }
            }

            m_CurSelBigChannel = BigChannel > 0 ? iChannel : -1;

            if (BigChannel >= 0)
            {
                panel11.Width = panel4.Width * 4;
                panel11.Height = panel4.Height * 4;
                panel11.Top = 20;
                panel11.Left = 20;
                panel11.Visible = true;
                panel11.BringToFront();
            }
        }

        private void axActiveVideo1_DblClick(object sender, EventArgs e)
        {
            CloseBigPicture(0);
            OpenBigPicture(0, 0);
        }

        private void axActiveVideo2_DblClick(object sender, EventArgs e)
        {
            CloseBigPicture(0);
            OpenBigPicture(0, 1);
        }

        private void axActiveVideo3_DblClick(object sender, EventArgs e)
        {
            CloseBigPicture(0);
            OpenBigPicture(0, 2);
        }

        private void axActiveVideo4_DblClick(object sender, EventArgs e)
        {
            CloseBigPicture(0);
        }
        #endregion

        #region 获取流媒体服务器字符串
        private string GetVideoPlayStr(string videoServerIp, string videoServerPort, string videoIp, string videoPort, int channelId, string userName, string password)
        {
            return "rtsp://" + videoServerIp + ":" + videoServerPort + "/" + videoIp + ":" + videoPort + ":HIK-DS8000HC:" + channelId + ":0:" + userName + ":" + password + "/av_stream";
        }
        #endregion

        #region grid事件
        private void ultraGrid2_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Key == "CHK")
            {
                if (!Convert.ToBoolean(e.Cell.Value))
                {
                    if (grid2Index > -1 && e.Cell.Row.Index != grid2Index)
                    {
                        ultraGrid2.Rows[grid2Index].Cells["CHK"].Value = "FALSE";
                    }
                    grid2Index = e.Cell.Row.Index;
                    ultraTextEditor3.Text = e.Cell.Row.Cells["FS_POTNO"].Value.ToString();
                    
                }
                else
                {
                    grid2Index = -1;
                    ultraTextEditor3.Text = string.Empty;
                }
                ultraGrid2.UpdateData();
            }
        }

        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            if (ultraGrid1.ActiveRow.Index < 0)
            {
                return;
            }
            string weighNo = this.ultraGrid1.ActiveRow.Cells["FS_WEIGHTNO"].Value.ToString();
            string weighTime = this.ultraGrid1.ActiveRow.Cells["FD_WEIGHTTIME"].Value.ToString();
            string weight = this.ultraGrid1.ActiveRow.Cells["FN_WEIGHT"].Value.ToString();
            SetControlValue(weighNo, weight, weighTime);
            //ultraactiverow = ultraGrid1.ActiveRow;
            if (preWeighTime != ultraGrid1.ActiveRow.Cells["FD_WEIGHTTIME"].Value.ToString())
            {
                matchedPots.Clear();
            }
        }



        private void ultraGrid1_BeforeRowActivate(object sender, RowEventArgs e)
        {
            preWeighTime = ultraGrid1.ActiveRow != null && ultraGrid1.ActiveRow.Index > -1 ? ultraGrid1.ActiveRow.Cells["FD_WEIGHTTIME"].Value.ToString() : "";
        }
        #endregion

        #region 生成炉号
        private void ultraOptionSet2_ValueChanged(object sender, EventArgs e)
        {
            ultraTextEditor5.Text = CreateNewHeatNo(ultraOptionSet2.Value.ToString());
        }

        private string CreateNewHeatNo(string furnaceNo)
        {
            string heatNo = "";
            ArrayList list = new ArrayList();
            list.Add(furnaceNo);
            DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHTMATCH_09.SELECT", list };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt != null && dt.Rows.Count > 0)
            {
                heatNo = dt.Rows[0]["HEATNO"].ToString();
            }

            return heatNo;
        }
        #endregion;

        //private void ultraButton6_Click(object sender, EventArgs e)
        //{
        //    if (grid2Index > -1)
        //    {
        //        if (MessageBox.Show("确定要删除匹配的记录吗？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            ArrayList list = new ArrayList();
        //            list.Add(ultraGrid2.Rows[grid2Index].Cells["FS_WEIGHTNO"].Value.ToString());
        //            //修改一次匹配罐号
        //            CoreClientParam ccp = new CoreClientParam();
        //            ccp.ServerName = "com.dbComm.DBComm";
        //            ccp.MethodName = "save";
        //            ccp.ServerParams = new object[] { "WEIGHTMATCH_01.DELETE", list };
        //            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        //            if (ccp.ReturnCode == 0)
        //            {
        //                QueryMatchWeight(2);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("请选择需要删除的罐号！");
        //    }
        //}

        private void ultraButton7_Click(object sender, EventArgs e)
        {
            DataRenew dr = new DataRenew(this.ob);
            dr.Show();
        }

        private void button1_Click(object sender, EventArgs e)//删除一次匹配数据
        {
            if (MessageBox.Show("是否确定删除匹配记录？", "玉钢MCMS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ArrayList list = new ArrayList();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow ugr in ultraGrid2.Rows)
                {
                    if (Convert.ToBoolean(ugr.Cells["CHK"].Value))
                    {
                        list.Add(ugr.Cells["FS_WEIGHTNO"].Value.ToString());
                    }
                }
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "save";
                ccp.ServerParams = new object[] { "DATAMODIFY_01.DELETE", list };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnCode == 0)
                {
                    MessageBox.Show("记录删除成功！");
                }
                this.QueryMatchWeight(1);
            }
        }
    }
}
