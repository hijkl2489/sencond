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
    public partial class HandRecordTermTare : FrmBase
    {
        GetBaseInfo m_BaseInfo = new GetBaseInfo();
        string m_SelectPointID = "";
        string m_WeightNo = "";

        public HandRecordTermTare()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        Query();
                        break;
                    }
                case "Update":
                    {
                        if (this.ultraGrid3.Rows.Count <= 0 || this.ultraGrid3.ActiveRow == null || this.ultraGrid3.ActiveRow.Selected == false)
                        {
                            MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (DialogResult.Yes == MessageBox.Show("您确定要修改该条信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            Update();
                            Query();
                        }
                        break;
                    }
                case "Add":
                    {
                        Add();
                        Query();
                        break;
                    }

                case "Delete":
                    {
                        if (this.ultraGrid3.Rows.Count <= 0 || this.ultraGrid3.ActiveRow == null || this.ultraGrid3.ActiveRow.Selected == false)
                        {
                            MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (DialogResult.Yes == MessageBox.Show("您确定要删除该条信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            Delete();
                            Query();
                        }
                        break;
                    }
                default:
                    break;
            }
        }



        private void Query()
        {
            string strBeginTime = BeginTime.Value.ToString("yyyy-MM-dd 00:00:00");
            string strEndTIme = EndTime.Value.ToString("yyyy-MM-dd 23:59:59");
            string carNo = this.txtChNum.Text.Trim();
            string sql = @"select A.FS_CARNO,
                           A.FN_TAREWEIGHT,
                           to_char(A.FD_STARTTIME, 'yyyy-MM-dd hh24:mi:ss') as FD_STARTTIME,
                           to_char(A.FD_ENDTIME, 'yyyy-MM-dd hh24:mi:ss') as FD_ENDTIME,
                           A.FS_PERSON,
                           to_char(A.FD_DATETIME, 'yyyy-MM-dd hh24:mi:ss') as FD_DATETIME,
                           A.FS_ISVAILD,
                           A.FS_WEIGHTNO,
                           A.FS_SHIFT,
                           A.FS_CARDNUMBER,
                           A.FS_DATASTATE,
                           A.FS_POINT FS_POINTNAME,
                           A.FS_MATERIALNAME,
                           A.FS_SENDER,
                           A.FS_SENDERSTORE,
                           A.FS_TRANSNO,
                           A.FS_RECEIVER,
                           A.FS_RECEIVERSTORE,
                           A.FS_WEIGHTTYPE,
                           A.FS_SUPPLY
                      from dt_termTare A
                     where A.FS_ISVAILD = '1'
                       and A.FD_DATETIME >=
                           to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                       and A.FD_DATETIME <=
                           to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            if (this.txtChNum.Text.Trim() != "")
            {
                sql += " and A.FS_CARNO='" + carNo + "'";
            }

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { string.Format(sql,strBeginTime,strEndTIme) };
            dataSet1.Tables[0].Clear();
            ccp.SourceDataTable = dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            Constant.RefreshAndAutoSize(ultraGrid3);
        }

        private void Update()
        {
            m_SelectPointID = cbBF.Text.Trim();//PointSelect(cbBF.Text.Trim());
            if (IsValid() == false)
                return;

            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strTareWeight = this.tbWeight.Text.Trim();



            string sql = "update dt_termTare SET FS_CARDNUMBER = '" + strCardNo + "',";
            sql += " FS_CARNO = '" + strCarNo + "',";
            sql += " FN_TAREWEIGHT = '" + strTareWeight + "' ";
            sql += " where FS_WEIGHTNO = '" + m_WeightNo + "'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        private void Add()
        {
            m_SelectPointID = cbBF.Text.Trim();//PointSelect(cbBF.Text.Trim());

            if (IsValid() == false)
                return;



            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strTareWeight = this.tbWeight.Text.Trim();
            string strStartTime = System.DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string strEndTime = System.DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            string strDateTime = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string strPerson = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            string strShift = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder();
            string strIsVaild = "1";

            m_WeightNo = Guid.NewGuid().ToString();

            CoreClientParam ccp = new CoreClientParam();
            string sql = "select FS_POINTSTATE from bt_point where FS_POINTNAME = '" + m_SelectPointID + "'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dtState = new System.Data.DataTable();
            ccp.SourceDataTable = dtState;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            string strState = dtState.Rows.Count > 0 ? dtState.Rows[0][0].ToString().Trim() : "0";


            sql = "insert into dt_termTare (FS_CARNO,FN_TAREWEIGHT,FD_STARTTIME,FD_ENDTIME,FS_POINT,";
            sql += "FS_PERSON,FD_DATETIME,FS_ISVAILD,FS_WEIGHTNO,FS_SHIFT,FS_CARDNUMBER,FS_DATASTATE )";
            sql += " values ('" + strCarNo + "','" + strTareWeight + "',TO_DATE('" + strStartTime + "','yyyy-MM-dd HH24:mi:ss'),TO_DATE('" + strEndTime + "','yyyy-MM-dd HH24:mi:ss'),'" + m_SelectPointID + "',";
            sql += " '" + strPerson + "',TO_DATE('" + strDateTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strIsVaild + "','" + m_WeightNo + "','" + strShift + "','" + strCardNo + "','" + strState + "')";


            //CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

        }

        private void Delete()
        {
            if (m_WeightNo == "")
            {
                MessageBox.Show("请选择需要删除的数据");
                return;
            }

            string sql = "delete dt_termTare where FS_WEIGHTNO = '" + m_WeightNo + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

        }

        private string PointSelect(string str)
        {
            string PointID = "";
            switch (str)
            {

                case "西北门100t重车秤":
                    PointID = "K01";
                    break;
                case "西北门100t空车秤":
                    PointID = "K02";
                    break;
                case "四烧150t汽车秤":
                    PointID = "K03";
                    break;
                case "焦化50t汽车秤":
                    PointID = "K04";
                    break;
                case "二钢80t汽车秤":
                    PointID = "K05";
                    break;
                case "二钢100t汽车秤":
                    PointID = "K06";
                    break;
                case "大营门100t汽车秤":
                    PointID = "K07";
                    break;
                case "三钢100t汽车秤":
                    PointID = "K08";
                    break;
                case "三钢80t汽车秤":
                    PointID = "K09";
                    break;
                case "安海50t汽车秤":
                    PointID = "K10";
                    break;
                default:
                    break;
            }
            return PointID;
        }

        private bool IsValid()
        {
            if (m_SelectPointID == "")
            {
                MessageBox.Show("请选择磅房!");
                return false;
            }

            if (tbCarNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入车号!");
                return false;
            }

            if (tbWeight.Text == "")
            {
                MessageBox.Show("请选择期限皮重!");
                return false;
            }

            try
            {
                decimal.Parse(tbWeight.Text.Trim());

            }
            catch
            {
                MessageBox.Show("期限皮重只能为数字!");
                return false;
            }

            return true;
        }

        private void ultraGrid3_Click(object sender, EventArgs e)
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null)
                return;

            m_WeightNo = ultraGrid3.ActiveRow.Cells["FS_WEIGHTNO"].Text.Trim();
            cbBF.Text = ultraGrid3.ActiveRow.Cells["FS_POINTNAME"].Text.Trim();
            tbCardNo.Text = ultraGrid3.ActiveRow.Cells["FS_CARDNUMBER"].Text.Trim();
            tbCarNo.Text = ultraGrid3.ActiveRow.Cells["FS_CARNO"].Text.Trim();
            tbWeight.Text = ultraGrid3.ActiveRow.Cells["FN_TAREWEIGHT"].Text.Trim();

        }

        private void HandRecordTermTare_Load(object sender, EventArgs e)
        {
            this.GetPoint();
        }

        private void GetPoint()
        {
            DataTable dt = new DataTable();
            string sql = "select t.* from bt_point t where t.fs_pointtype='QC'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.CarCard";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                this.cbBF.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                    this.cbBF.Items.Add(dt.Rows[i]["FS_POINTNAME"].ToString());

            }
        }

    }
}
