using System;
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

namespace YGJZJL.Car
{
    public partial class CarWeightLogQuery : FrmBase
    {
        GetBaseInfo m_BaseInfo = new GetBaseInfo();
        string m_SelectPointID = "";
        string m_WeightNo = "";
        LimitQueryTime limitQueryTime = new LimitQueryTime();
        public CarWeightLogQuery()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        QueryLog();
                        //Query();                        
                        break;
                    }
                case "Update":
                    {
                        Update();
                        Query();
                        break;
                    }
                //case "Add":
                //        {
                //        Add();
                //            Query();
                //            break;
                //        }

                case "Delete":
                        {
                        Delete();
                            Query();
                            break;
                        }
                default :
                    break;
            }
        }

    

        private void Query()
        {
           
            string strBeginTime =  BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss')";
            string strEndTIme = EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss')";
            string sql = "select A.FS_CARNO,A.FN_TAREWEIGHT,to_char(A.FD_STARTTIME,'yyyy-MM-dd hh24:mi:ss') as FD_STARTTIME,to_char(A.FD_ENDTIME,'yyyy-MM-dd hh24:mi:ss') as FD_ENDTIME,A.FS_PERSON,to_char(A.FD_DATETIME,'yyyy-MM-dd hh24:mi:ss') as FD_DATETIME,";
            sql += "A.FS_ISVAILD,A.FS_WEIGHTNO,A.FS_SHIFT,A.FS_CARDNUMBER,A.FS_DATASTATE,B.FS_POINTNAME ";
                   sql += " from dt_termTare A,Bt_point B ";
                   sql += " where A.FS_POINT = B.FS_POINTCODE ";
                   sql += " and A.FD_DATETIME >= to_date('" + BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
                   sql += " and A.FD_DATETIME <= to_date('" + EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss') order by a.FD_DATETIME desc";

                   CoreClientParam ccp = new CoreClientParam();
                   ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                   ccp.MethodName = "ExcuteQuery";
                   ccp.ServerParams = new object[] { sql };
                   ccp.SourceDataTable = dataSet1.Tables[0];

                   this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                   Constant.RefreshAndAutoSize(ultraGrid3);
        }

        private void Update()
        {
            if (IsValid() == false)
                return;
            m_SelectPointID = this.cbBF.SelectedValue.ToString();
           

            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strTareWeight = this.tbWeight.Text.Trim();

            string sql = "update dt_termTare SET FS_CARDNUMBER = '" + strCardNo + "',";
                   sql += " FS_CARNO = '" + strCarNo + "',";
                   sql += " FN_TAREWEIGHT = '" + strTareWeight + "' ";
                   sql += " where FS_WEIGHTNO = '" + m_WeightNo + "'";
                    
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "updateByClientSql";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);            
        }

        //private void Add()
        //{

        //    if (IsValid() == false)
        //        return;
        //    m_SelectPointID = this.cbBF.SelectedValue.ToString();        

         
            


        //    string strCarNo = this.tbCarNo.Text.Trim();
        //    string strCardNo = this.tbCardNo.Text.Trim();            
        //    string strTareWeight = this.tbWeight.Text.Trim();
        //    string strStartTime = System.DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
        //    string strEndTime = System.DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
        //    string strDateTime = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //    string strPerson = KiscICPlatClient.Communication.Core.UserInfo.userInfo.GetUserName();
        //    string strShift = KiscICPlatClient.Communication.Core.UserInfo.userInfo.GetUserShift();
        //    string strIsVaild = "1";
            
        //    m_WeightNo = Guid.NewGuid().ToString();

        //    CoreClientParam ccp = new CoreClientParam();
        //    string sql = "select FS_POINTSTATE from bt_point where FS_POINTCODE = '" + m_SelectPointID + "'";
        //    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
        //    ccp.MethodName = "ExcuteQuery";
        //    ccp.ServerParams = new object[] { sql };
        //    System.Data.DataTable dtState = new System.Data.DataTable();
        //    ccp.SourceDataTable = dtState;
        //    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        //    string strState = dtState.Rows[0][0].ToString().Trim();


        //    sql = "insert into dt_termTare (FS_CARNO,FN_TAREWEIGHT,FD_STARTTIME,FD_ENDTIME,FS_POINT,";
        //    sql += "FS_PERSON,FD_DATETIME,FS_ISVAILD,FS_WEIGHTNO,FS_SHIFT,FS_CARDNUMBER,FS_DATASTATE )";
        //    sql += " values ('" + strCarNo + "','" + strTareWeight + "',TO_DATE('" + strStartTime + "','yyyy-MM-dd HH24:mi:ss'),TO_DATE('" + strEndTime + "','yyyy-MM-dd HH24:mi:ss'),'" + m_SelectPointID + "',";
        //    sql += " '" + strPerson + "',TO_DATE('" + strDateTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strIsVaild + "','" + m_WeightNo + "','" + strShift + "','" + strCardNo + "','" + strState + "')";
            

        //     //CoreClientParam ccp = new CoreClientParam();
        //    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
        //    ccp.MethodName = "insertByClientSql";
        //    ccp.ServerParams = new object[] { sql };
        //    ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        //    MessageBox.Show("新增成功!");

        //}

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
            ccp.MethodName = "deleteByClientSql";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                
        }

        private string PointSelect(string str)
        {
            string PointID = "";
            switch (str)
            {

                case "大门2#秤":
                    PointID = "K02";
                    break;
                case "桥头3#秤":
                    PointID = "K03";
                    break;
                case "桥头4#秤":
                    PointID = "K04";
                    break;
                case "粗碎站120t":
                    PointID = "K05";
                    break;
                case "进风井":
                    PointID = "K06";
                    break;
                case "400米联道":
                    PointID = "K07";
                    break;
                case "340米":
                    PointID = "K08";
                    break;
                
                default:
                    break;
            }
            return PointID;
        }

        private bool IsValid()
        {            
            if (this.cbBF.SelectedValue==null||this.cbBF.SelectedValue.ToString()=="")
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

            return true;
        }

       

        private void ultraGrid3_Click(object sender, EventArgs e)
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null)            
                return;

            m_WeightNo = ultraGrid3.ActiveRow.Cells["FS_OPERATIONNO"].Text.Trim();


            this.tbLogData.Text = (ultraGrid3.ActiveRow.Cells["FS_TYPE"].Text +","+ ultraGrid3.ActiveRow.Cells["FS_DATA"].Text).Replace(",", "\r\n");
 
            //cbBF.Text = ultraGrid3.ActiveRow.Cells["FS_POINTNAME"].Text.Trim();
            //tbCardNo.Text = ultraGrid3.ActiveRow.Cells["FS_CARDNUMBER"].Text.Trim();
            //tbCarNo.Text = ultraGrid3.ActiveRow.Cells["FS_CARNO"].Text.Trim();
            //tbWeight.Text = ultraGrid3.ActiveRow.Cells["FN_TAREWEIGHT"].Text.Trim(); 
           
        }

        /// <summary>
        /// 从数据库获取发货单位数据(DHS项目实施后要做相应修改,供应商改为发货单位)
        /// </summary>
        private void GetFHDWData()//客户端加参数，调用kiscicplat.common.QueryByClient
        {

            string sql = "select FS_POINTCODE ,FS_POINTNAME from bt_point where FS_POINTTYPE='QC'";//A.FS_POINTNO ='K02' and
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "kiscicplat.dhs.carweight.CarWeightPrediction";
            //ccp.MethodName = "QueryFHDWData";
            //ccp.ServerParams = new object[] { strBFID };
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dtBF = new System.Data.DataTable();
            ccp.SourceDataTable = dtBF;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtBF.Rows.Count > 0)
            {
                DataRow dr = dtBF.NewRow();
                dtBF.Rows.InsertAt(dr, 0);

                this.cbBF.DataSource = dtBF;
                cbBF.DisplayMember = "FS_POINTNAME";
                cbBF.ValueMember = "FS_POINTCODE";

                cbBF.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbBF.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbBF.DataSource = dtBF;

            }
        }

        private void HandRecordTermTare_Load(object sender, EventArgs e)
        {
            //GetFHDWData();
            GetDataName();
        }

        private void GetDataName()
        {
            string sql = "select fs_weighttype from DT_OPERATIONLOG GROUP BY fs_weighttype";//A.FS_POINTNO ='K02' and
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "kiscicplat.dhs.carweight.CarWeightPrediction";
            //ccp.MethodName = "QueryFHDWData";
            //ccp.ServerParams = new object[] { strBFID };
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dtBF = new System.Data.DataTable();
            ccp.SourceDataTable = dtBF;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtBF.Rows.Count > 0)
            {
                DataRow dr = dtBF.NewRow();
                dtBF.Rows.InsertAt(dr, 0);

                this.cbBF.DataSource = dtBF;
                cbBF.DisplayMember = "fs_weighttype";
                cbBF.ValueMember = "fs_weighttype";

                cbBF.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbBF.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbBF.DataSource = dtBF;

            }
        }

        private void QueryLog()
        {

            //if (!limitQueryTime.ParseTimeLegal(BeginTime.Value, EndTime.Value))
            //{
            //    return;
            //}

            string strBeginTime = BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") ;
            string strEndTime = EndTime.Value.ToString("yyyy-MM-dd 23:59:59");

            string strQuerySql = "select  FS_OPERATIONNO,FS_TABLENAME,FS_DEPART,FS_TYPE,to_char(FD_TIME,'yyyy-MM-dd hh24:mi:ss') as FD_TIME ,FS_OPERATOR,FS_DATA,FS_IP,FS_GRAPHNAME,FS_DATANAME,FS_WEIGHTTYPE from DT_OPERATIONLOG t ";

            strQuerySql += " where t.fd_time between to_date('" + strBeginTime + "', 'yyyy-MM-dd hh24:mi:ss') and to_date('" + strEndTime + "', 'yyyy-MM-dd hh24:mi:ss') ";

            if (this.cbBF.SelectedValue != null&& this.cbBF.SelectedValue.ToString()!="")
            {
                strQuerySql += " and t.fs_weighttype='" + this.cbBF.SelectedValue.ToString() + "'";
            }
            strQuerySql += "ORDER BY FD_TIME DESC";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strQuerySql };
           
            ccp.SourceDataTable = this.dataSet1.Tables[1];
            this.dataSet1.Tables[1].Clear();
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        private void cbBF_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryLog();
        }
     
       
    }
}
