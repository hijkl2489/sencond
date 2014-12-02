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
using Infragistics.Win;
using System.Diagnostics;
using YGJZJL.PublicComponent;

namespace YGJZJL.BoardBand
{
    public partial class WeightUpdate_BoardNew : CoreFS.CA06.FrmBase
    {
        int rowid = -1;
        string strWeightno="";
        string strBatch = "";
        BaseInfo m_BaseInfo_1 = new BaseInfo();
        string strUserName = "";
        string strDepartMent = "";

        public WeightUpdate_BoardNew()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            { 
                case "查询":
                    query();
                    break;
                case "修改":
                    update();
                    query();
                    break;
                case "删除":
                    delete();
                    query();
                    break;
                case "新增":
                    addnew();
                    query();
                    break;
                case "取样":
                    sample();
                    query();

                    break;
                case "精整":
                    jingzheng();
                    query();
                    break;
                case "复磅":
                    reweight();
                    query();
                    break;

                case "导出":
                    CommonMethod.ExportDataWithSaveDialog2(ref this.ultraGrid1, this.Text);
                    break;
                case "修改订单":
                    updatePro();
                    query();
                    break;

            }
        }


        private void query()
        {
            try
            {
                if (this.dtpBegintime.Value > this.dtpEendtime.Value)
                {
                    MessageBox.Show("查询开始时间不能大于结束时间");
                    return;
                }

                string strStartTime = dtpBegintime.Value.ToString("yyyy-MM-dd 00:00:00");
                string strEndTime = dtpEendtime.Value.ToString("yyyy-MM-dd 23:59:59");

                string strSql = @"select T.FS_WEIGHTNO,
       T.FS_BATCHNO,
       FN_BANDNO,
       TO_CHAR(T.FN_WEIGHT, '999.000') FN_WEIGHT,
       
       T.FS_UPLOADFLAG,
       to_char(T.FD_DATETIME, 'yyyy-MM-dd hh24:mi:ss') FD_DATETIME,
       T.FS_PLANT,
       T.FS_SAPSTORE,
       T.FS_ACCOUNTDATE,
       T.FS_PRODUCTNO,
       FS_POINTNAME AS FS_POINT,
       TO_CHAR(FN_YKL, '99990.000') FN_YKL,
       TO_CHAR(FN_KHJZ, '9999.000') FN_KHJZ,
       
       FS_REEL,
       T.FS_STOVENO,
       T.FS_PERSON,
       DECODE(T.FS_SHIFT, '0', '常白班', '1', '早', '2', '中', '3', '夜') as FS_SHIFT,
       DECODE(T.FS_TERM,
              '0',
              '常白班',
              '1',
              '甲',
              '2',
              '乙',
              '3',
              '丙',
              '4',
              '丁') as FS_TERM,
       T.FS_SPEC,
       T.FS_STEELTYPE,
       FS_KZTYPE,
       FS_SAMPLEPERSON,
       TO_CHAR(C.FN_WEIGHT, '999.000') FN_BPWEIGHT,
       TO_CHAR(FD_SAMPLETIME, 'yyyy-MM-dd hh24:mi:ss') FD_SAMPLETIME,
       T.FS_MEMO ,
       D.FS_ADVISESPEC
  from dt_zkd_productweightdetail t, BT_POINT B, dt_boardweightmain C,it_fp_techcard D
 where (t.fd_datetime between
       to_date('" + strStartTime+"', 'yyyy-MM-dd hh24:mi:ss') and to_date('"+strEndTime+"', 'yyyy-MM-dd hh24:mi:ss'))"+
  " and t.fs_deleteflag != '1'"+
  " AND T.FS_POINT = B.FS_POINTCODE"+
  " AND T.FS_STOVENO = C.FS_STOVENO(+) "+
  " AND T.FS_STOVENO = D.FS_GP_STOVENO(+) ";
                strSql = string.Format(strSql, strStartTime, strEndTime);
                if (this.txtBatchnoQuery.Text.Trim() != string.Empty)
                {
                    strSql += " and t.FS_BATCHNO like  '%" + this.txtBatchnoQuery.Text.Trim().Replace("'", "''") + "%'";
                }



                strSql += " order by t.fd_datetime desc";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                ccp.SourceDataTable = this.dataSet1.Tables[0];
                this.dataSet1.Tables[0].Clear();
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                CommonMethod.RefreshAndAutoSize(ultraGrid1);
               

                
            }
            catch(Exception ee)
            { 

            }
          
        }


        private void update()
        {
            if (rowid < 0)
            {
                MessageBox.Show("请双击选择需要操作的数据！");
                return;
            }
            if (isvalid())
            {
               
                string strBatchno = txtBatchno.Text.Trim();
                string strBandno = txtbandno.Text.Trim();
                string strStoveno =txtStoveno.Text.Trim();
                string strReel = txtreel.Text.Trim();
                string strWeight = txtweight.Text.Trim();
                string strYkl =txtykl.Text.Trim();

                string strKhjz = "";
                if (this.txtykl.Text != "")
                {
                    strKhjz=(Convert.ToDouble(txtweight.Text.Trim()) - Convert.ToDouble(txtykl.Text.Trim())).ToString();
                }
                else
                {
                    strKhjz = this.txtweight.Text;
                }
                string strProductno = txtproductno.Text.ToString().Trim();
                string strSpec = txtspec.Text.Trim();
                string strSteelType = txtsteeltype.Text.Trim();
                string strKZtype = cbKZTYPE.Text.Trim();
                string strMemo = txtMemo.Text.Trim();
              
                string strSql = @"UPDATE  dt_zkd_productweightdetail T SET
                                   T.FS_BATCHNO='" + strBatchno + "'," +
                                   "T.FS_REEL='" + strReel + "'," +
                                  "T.FS_SPEC='" + strSpec + "'," +
                                  "T.FS_STEELTYPE='" + strSteelType + "'," +
                                  "T.FN_WEIGHT='" + strWeight + "'," +
                                  "T.FN_YKL='" + strYkl + "'," +
                                  "T.FN_KHJZ='" + strKhjz + "'," +
                                  "T.FS_PRODUCTNO='"+strProductno+"',"+
                                  "T.FS_STOVENO='"+strStoveno+"',"+
                                  "T.FN_BANDNO='"+strBandno+"',"+
                                  "T.FS_MEMO='"+strMemo+"',"+
                                 " FS_KZTYPE='修改',FS_SAMPLEPERSON='" + strUserName + "',FD_SAMPLETIME= sysdate"+
                                  " WHERE FS_WEIGHTNO='" + strWeightno + "'";





                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { strSql };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                if (ccp.ReturnCode == 0)
                {
                    MessageBox.Show("修改数据成功！");



                    string txtBatchnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text.ToString();
                    string strBandnoLog = this.ultraGrid1.Rows[rowid].Cells["FN_BANDNO"].Text.ToString();
                    string strReelLog = this.ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text.ToString();
                    string strStovenoLog = this.ultraGrid1.Rows[rowid].Cells["FS_STOVENO"].Text.ToString();
                    string strWeightLog = this.ultraGrid1.Rows[rowid].Cells["FN_WEIGHT"].Text.ToString();
                    string strYklLog = this.ultraGrid1.Rows[rowid].Cells["FN_YKL"].Text.ToString();
                    string strKhjzLog = this.ultraGrid1.Rows[rowid].Cells["FN_KHJZ"].Text.ToString();
                    string strProductnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_PRODUCTNO"].Text.ToString();
                    string strSteeltypeLog = this.ultraGrid1.Rows[rowid].Cells["FS_STEELTYPE"].Text.ToString();
                    string strSpecLog = this.ultraGrid1.Rows[rowid].Cells["FS_SPEC"].Text.ToString();
                    string strMemoLog = this.ultraGrid1.Rows[rowid].Cells["FS_KZTYPE"].Text.ToString();


                    string strLog = "轧制编号：" + txtBatchnoLog + ">" + txtBatchno.Text + ",吊号：" + strBandnoLog + ">" + txtbandno.Text + ",卷号："
                        + strReelLog + ">" + txtreel.Text + ",钢坯炉号：" + strStovenoLog + ">" + txtStoveno.Text + ",重量："
                        + strWeightLog + ">" + txtweight.Text + ",应扣量：" + strYklLog + ">" + txtykl.Text + ",扣后净重："
                        + strKhjzLog + ">" + strKhjz + ",订单号：" + strProductnoLog + ">" + txtproductno.Text + ",钢种："
                        + strSteeltypeLog + ">" + txtsteeltype.Text + ",规格：" + strSpecLog + ">" + txtspec.Text + ",备注：" + strMemoLog+">"+txtMemo.Text;

                    m_BaseInfo_1.ob = this.ob;
                    this.m_BaseInfo_1.WriteOperationLog("DT_ZKD_PRODUCTWEIGHTDETAIL", strDepartMent, strUserName, "修改", strLog, "中宽带计量数据维护", "中宽带计量明细表", "中宽带成品");//调用写操作日志方法

                }
                else
                {
                    MessageBox.Show("修改数据失败！");
                    rowid = -1;
                    return;
                }


                strSql = @"UPDATE  dt_zkd_plan T SET
                                   T.FS_REEL='" + strReel + "'," +
                                 "T.FS_SPEC='" + strSpec + "'," +
                                 "T.FS_STEELTYPE='" + strSteelType + "'," +
                               
                                 "T.FS_PRODUCTNO='" + strProductno + "'" +
                                 " WHERE FS_REEL='" + this.ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text.ToString()+"'";
                ccp.ServerParams = new object[] { strSql };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                
                rowid = -1;
               



             
              
            }
        }


        private void delete()
        {
            if (rowid < 0)
            {
                MessageBox.Show("请选择需要删除的数据");
            }

            if (DialogResult.No == MessageBox.Show("是否删除批次号：" + ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text + "卷号：" + ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text+"的数据？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }
            string strSql = @"delete  dt_zkd_productweightdetail T  WHERE FS_WEIGHTNO='" + strWeightno + "'";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strSql };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode == 0)
            {
                MessageBox.Show("删除数据成功！");

                string txtBatchnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text.ToString();
                string strBandnoLog = this.ultraGrid1.Rows[rowid].Cells["FN_BANDNO"].Text.ToString();
                string strReelLog = this.ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text.ToString();
                string strStovenoLog = this.ultraGrid1.Rows[rowid].Cells["FS_STOVENO"].Text.ToString();
                string strWeightLog = this.ultraGrid1.Rows[rowid].Cells["FN_WEIGHT"].Text.ToString();
                string strYklLog = this.ultraGrid1.Rows[rowid].Cells["FN_YKL"].Text.ToString();
                string strKhjzLog = this.ultraGrid1.Rows[rowid].Cells["FN_KHJZ"].Text.ToString();
                string strProductnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_PRODUCTNO"].Text.ToString();
                string strSteeltypeLog = this.ultraGrid1.Rows[rowid].Cells["FS_STEELTYPE"].Text.ToString();
                string strSpecLog = this.ultraGrid1.Rows[rowid].Cells["FS_SPEC"].Text.ToString();
                string strMemo = this.ultraGrid1.Rows[rowid].Cells["FS_MEMO"].Text.ToString();


                string strLog = "轧制编号：" + txtBatchnoLog  + ",吊号：" + strBandnoLog +  ",卷号："
                    + strReelLog +  ",钢坯炉号：" + strStovenoLog +  ",重量："
                    + strWeightLog +",应扣量：" + strYklLog +  ",扣后净重："
                    + strKhjzLog +  ",订单号：" + strProductnoLog +  ",钢种："
                    + strSteeltypeLog + ",规格：" + strSpecLog+",备注："+strMemo;

                m_BaseInfo_1.ob = this.ob;
                this.m_BaseInfo_1.WriteOperationLog("DT_ZKD_PRODUCTWEIGHTDETAIL", strDepartMent, strUserName, "删除", strLog, "中宽带计量数据维护", "中宽带计量明细表", "中宽带成品");//调用写操作日志方法


            }
            else
            {
                MessageBox.Show("删除数据失败！");
            }

        }


        private void addnew()
        {
            if (isvalid())
            {
                string strBatchno = txtBatchno.Text.Trim();
                string strBandno = txtbandno.Text.Trim();
                string strStoveno = txtStoveno.Text.Trim();
                string strReel = txtreel.Text.Trim();
                string strWeight = txtweight.Text.Trim();
                string strYkl = txtykl.Text.Trim();
                string strKhjz = "";
                if (this.txtykl.Text != "")
                {
                    strKhjz = (Convert.ToDouble(txtweight.Text.Trim()) - Convert.ToDouble(txtykl.Text.Trim())).ToString();
                }
                else
                {
                    strKhjz = this.txtweight.Text;
                }
                string strProductno = txtproductno.Text.ToString().Trim();
                string strSpec = txtspec.Text.Trim();
                string strSteelType = txtsteeltype.Text.Trim();
                //string strDatetime = DateTime.Now.ToString("YYYY-MM-dd HH:mm:ss");
                string strGuid = Guid.NewGuid().ToString();
                string p_FS_PERSON = UserInfo.GetUserName();//计量员
                string p_FS_SHIFT = UserInfo.GetUserOrder();//班次
                string p_FS_TERM = UserInfo.GetUserGroup();//班别
                string strPoint="K23";

                DataTable dt = new DataTable();
                string strQuerySql = "select * from dt_boardweightmain t where t.fs_stoveno='" + strStoveno + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strQuerySql };
                ccp.SourceDataTable = dt;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("系统中不存在该炉号的板坯计量数据，请查证后再输入！");
                    return;
                }

                string strSql = @"insert into  dt_zkd_productweightdetail T (
                                                        FS_WEIGHTNO,
                                                        FS_BATCHNO,
                                                        FN_BANDNO,
                                                        FN_WEIGHT,
                                                        FD_DATETIME,
                                                        FS_PRODUCTNO,
                                                        FS_POINT,
                                                        FN_YKL,
                                                        FN_KHJZ,
                                                        FS_REEL,
                                                        FS_STOVENO,
                                                        FS_PERSON,
                                                        FS_SHIFT,
                                                        FS_TERM,
                                                        FS_SPEC,
                                                        FS_STEELTYPE,
                                                        FS_KZTYPE,
                                                        FS_SAMPLEPERSON,
                                                        FD_SAMPLETIME)values('"
                                                        + strGuid + "','"
                                                        + strBatchno + "','"
                                                        + strBandno + "','"
                                                        + strWeight + "',"
                                                        +"sysdate,'"                              
                                                        + strProductno + "','"
                                                        + strPoint + "','"
                                                        + strYkl + "','"
                                                        + strKhjz + "','"
                                                        + strReel + "','"
                                                        + strStoveno + "','"
                                                        + p_FS_PERSON + "','"
                                                        + p_FS_SHIFT + "','"
                                                        + p_FS_TERM + "','"
                                                        + strSpec + "','"
                                                        + strSteelType + "','"
                                                        + "新增','"
                                                        + strUserName+"',"
                                                        +" sysdate"+
                                                        ")";



                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { strSql };
            
                
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnCode == 0)
                {
                    MessageBox.Show("新增数据成功！");
                                     
                    string strLog = "轧制编号：" + strBatchno + ",吊号：" + strBandno + ",卷号："
                                      + strReel + ",钢坯炉号：" + strStoveno + ",重量："
                                      + strWeight + ",应扣量：" + strYkl + ",扣后净重："
                                      + strKhjz + ",订单号：" + strProductno + ",钢种："
                                      + strSteelType + ",规格：" + strSpec;
                    m_BaseInfo_1.ob = this.ob;
                    this.m_BaseInfo_1.WriteOperationLog("DT_ZKD_PRODUCTWEIGHTDETAIL", strDepartMent, strUserName, "新增", strLog, "中宽带计量数据维护", "中宽带计量明细表", "中宽带成品");//调用写操作日志方法


                }
                else
                {
                    MessageBox.Show("新增数据失败！");
                }
             


            }
        }


        private void sample()
        {
            if (rowid < 0)
            {
                MessageBox.Show("请双击选择需要操作的数据！");
                return;
            }


            if (DialogResult.No == MessageBox.Show("是否对批次号：" + ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text + "卷号：" + ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text + "的数据进行取样操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            try
            {
                Convert.ToDouble(this.txtykl.Text.Trim());
            }
            catch
            {
                MessageBox.Show("请输入数字格式的应扣量！");
                this.txtykl.Focus();
                return;
            }

            if (Convert.ToDouble(txtweight.Text.Trim()) < Convert.ToDouble(this.txtykl.Text.Trim()))
            {
                MessageBox.Show("输入的应扣量应比重量小！");
                this.txtykl.Focus();
                return;
            }

            string strKhjz = (Convert.ToDouble(txtweight.Text.Trim()) - Convert.ToDouble(txtykl.Text.Trim())).ToString();
            string strYkl = txtykl.Text.Trim();

            string strUpdateSql = "update dt_zkd_productweightdetail t set t.FN_YKL='" + strYkl + "',T.FN_KHJZ='" + strKhjz + "',FS_KZTYPE='取样',FS_MEMO='取样',FS_SAMPLEPERSON='" + strUserName + "',FD_SAMPLETIME= sysdate   WHERE T.FS_WEIGHTNO='" + strWeightno + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strUpdateSql };
       
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode ==0)
            {
                MessageBox.Show("取样成功！");

                string txtBatchnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text.ToString();
                string strBandnoLog = this.ultraGrid1.Rows[rowid].Cells["FN_BANDNO"].Text.ToString();
                string strReelLog = this.ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text.ToString();
                string strYklLog = this.ultraGrid1.Rows[rowid].Cells["FN_YKL"].Text.ToString();
                string strKhjzLog = this.ultraGrid1.Rows[rowid].Cells["FN_KHJZ"].Text.ToString();
               



                string strLog = "轧制编号：" + txtBatchnoLog + ">" + txtBatchnoLog + ",吊号：" + strBandnoLog + ">" + strBandnoLog + ",卷号："
                    + strReelLog + ">" + strReelLog + ",应扣量：" + strYklLog + ">" + txtykl.Text + ",扣后净重："
                    + strKhjzLog + ">" + strKhjz;

                m_BaseInfo_1.ob = this.ob;
                this.m_BaseInfo_1.WriteOperationLog("DT_ZKD_PRODUCTWEIGHTDETAIL", strDepartMent, strUserName, "取样", strLog, "中宽带成品取样", "中宽带计量明细表", "中宽带成品");//调用写操作日志方法


            }
            else
            {
                MessageBox.Show("取样失败！");
            }

        }


        private void jingzheng()
        {
            if (rowid < 0)
            {
                MessageBox.Show("请双击选择需要操作的数据！");
                return;
            }

            if (DialogResult.No == MessageBox.Show("是否对批次号：" + ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text + " 卷号：" + ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text + "的数据进行精整操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            try
            {
                Convert.ToDouble(this.txtykl.Text.Trim());
                
            }
            catch
            {
                MessageBox.Show("请输入数字格式的应扣量！");
                this.txtykl.Focus();
                return;
            }

            if (Convert.ToDouble(txtweight.Text.Trim()) < Convert.ToDouble(this.txtykl.Text.Trim()))
            {
                MessageBox.Show("输入的应扣量应比重量小！");
                this.txtykl.Focus();
                return;
            }

            string strKhjz = (Convert.ToDouble(txtweight.Text.Trim()) - Convert.ToDouble(txtykl.Text.Trim())).ToString();
            string strYkl = txtykl.Text.Trim();

            string strUpdateSql = "update dt_zkd_productweightdetail t set t.FN_YKL='" + strYkl + "',T.FN_KHJZ='" + strKhjz + "',FS_KZTYPE='精整' ,FS_MEMO='精整',FS_SAMPLEPERSON='" + strUserName + "',FD_SAMPLETIME= sysdate   WHERE T.FS_WEIGHTNO='" + strWeightno + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strUpdateSql };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                MessageBox.Show("操作成功！");

                string txtBatchnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text.ToString();
                string strBandnoLog = this.ultraGrid1.Rows[rowid].Cells["FN_BANDNO"].Text.ToString();
                string strReelLog = this.ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text.ToString();
                string strYklLog = this.ultraGrid1.Rows[rowid].Cells["FN_YKL"].Text.ToString();
                string strKhjzLog = this.ultraGrid1.Rows[rowid].Cells["FN_KHJZ"].Text.ToString();




                string strLog = "轧制编号：" + txtBatchnoLog + ">" + txtBatchnoLog + ",吊号：" + strBandnoLog + ">" + strBandnoLog + ",卷号："
                    + strReelLog + ">" + strReelLog + ",应扣量：" + strYklLog + ">" + txtykl.Text + ",扣后净重："
                    + strKhjzLog + ">" + strKhjz;

                m_BaseInfo_1.ob = this.ob;
                this.m_BaseInfo_1.WriteOperationLog("DT_ZKD_PRODUCTWEIGHTDETAIL", strDepartMent, strUserName, "精整", strLog, "中宽带成品取样", "中宽带计量明细表", "中宽带成品");//调用写操作日志方法


            }
            else
            {
                MessageBox.Show("操作失败！");
            }

        }


        private void reweight()
        {
            if (rowid < 0)
            {
                MessageBox.Show("请双击选择需要操作的数据！");
                return;
            }

            if (DialogResult.No == MessageBox.Show("是否对批次号：" + ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text + " 卷号：" + ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text + "的数据进行复磅操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }
            try
            {
               Convert.ToDouble(this.txtweight.Text.Trim());
            
            }
            catch
            {
                MessageBox.Show("请输入数字格式的重量！");
                this.txtweight.Focus();
                return;
            }

            try
            {
                if (txtykl.Text != "")
                {
                    Convert.ToDouble(this.txtykl.Text.Trim());

                    if (Convert.ToDouble(txtweight.Text.Trim()) < Convert.ToDouble(this.txtykl.Text.Trim()))
                    {
                        MessageBox.Show("输入的应扣量应比重量小！");
                        this.txtykl.Focus();
                        return;
                    }
                }

            }
            catch
            {
                MessageBox.Show("请输入数字格式的应扣量！");
                this.txtykl.Focus();
                return;
            }
            
           


            string strWeight = txtweight.Text.Trim();
            string strYkl = txtykl.Text.Trim();

            string strKhjz = "";
            if (this.txtykl.Text != "")
            {
                strKhjz = (Convert.ToDouble(txtweight.Text.Trim()) - Convert.ToDouble(txtykl.Text.Trim())).ToString();
            }
            else
            {
                strKhjz = this.txtweight.Text;
            }

            string strUpdateSql = "update dt_zkd_productweightdetail t set t.FN_WEIGHT='" + strWeight + "', t.FN_YKL='" + strYkl + "',T.FN_KHJZ='" + strKhjz + "',FS_KZTYPE='复磅' ,FS_MEMO='复磅',FS_SAMPLEPERSON='" + strUserName + "',FD_SAMPLETIME= sysdate   WHERE T.FS_WEIGHTNO='" + strWeightno + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strUpdateSql };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                MessageBox.Show("操作成功！");

                string txtBatchnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text.ToString();
                string strBandnoLog = this.ultraGrid1.Rows[rowid].Cells["FN_BANDNO"].Text.ToString();
                string strReelLog = this.ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text.ToString();
                string strYklLog = this.ultraGrid1.Rows[rowid].Cells["FN_YKL"].Text.ToString();
                string strKhjzLog = this.ultraGrid1.Rows[rowid].Cells["FN_KHJZ"].Text.ToString();
                string strWeightLog = this.ultraGrid1.Rows[rowid].Cells["FN_WEIGHT"].Text.ToString();



                string strLog = "轧制编号：" + txtBatchnoLog + ">" + txtBatchnoLog + ",吊号：" + strBandnoLog + ">" + strBandnoLog + ",卷号："
                    + strReelLog + ">" + strReelLog +",重量："+ strWeightLog +">" +strWeight+ ",应扣量：" + strYklLog + ">" + txtykl.Text + ",扣后净重："
                    + strKhjzLog + ">" + strKhjz;

                m_BaseInfo_1.ob = this.ob;
                this.m_BaseInfo_1.WriteOperationLog("DT_ZKD_PRODUCTWEIGHTDETAIL", strDepartMent, strUserName, "复磅", strLog, "中宽带成品取样", "中宽带计量明细表", "中宽带成品");//调用写操作日志方法


            }
            else
            {
                MessageBox.Show("操作失败！");
            }


        }


        private void  updatePro()
            {
                if (rowid < 0)
                {
                    MessageBox.Show("请双击选择需要操作的数据！");
                    return;
                }
                if (isvalid())
                {

                  

                    string strKhjz = "";
                    if (this.txtykl.Text != "")
                    {
                        strKhjz = (Convert.ToDouble(txtweight.Text.Trim()) - Convert.ToDouble(txtykl.Text.Trim())).ToString();
                    }
                    else
                    {
                        strKhjz = this.txtweight.Text;
                    }
                    string strProductno = txtproductno.Text.ToString().Trim();
                    string strSpec = txtspec.Text.Trim();
                    string strSteelType = txtsteeltype.Text.Trim();
                    string strKZtype = cbKZTYPE.Text.Trim();
                    string strMemo = txtMemo.Text.Trim();

                    string strSql = @"UPDATE  dt_zkd_productweightdetail T SET
                                  
                                   T.FS_PRODUCTNO='" + strProductno + 
                                      
                                      "' WHERE FS_BATCHNO='" + strBatch + "'";





                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { strSql };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    if (ccp.ReturnCode == 0)
                    {
                        MessageBox.Show("修改数据成功！");



                       
                        string strProductnoLog = this.ultraGrid1.Rows[rowid].Cells["FS_PRODUCTNO"].Text.ToString();
                      


                        string strLog = "订单号：" + strProductnoLog + ">" + txtproductno.Text ;

                        m_BaseInfo_1.ob = this.ob;
                        this.m_BaseInfo_1.WriteOperationLog("DT_ZKD_PRODUCTWEIGHTDETAIL", strDepartMent, strUserName, "修改", strLog, "中宽带计量数据维护", "中宽带计量明细表", "中宽带成品");//调用写操作日志方法

                    }
                    else
                    {
                        MessageBox.Show("修改数据失败！");
                        rowid = -1;
                        return;
                    }


                    strSql = @"UPDATE  dt_zkd_plan T SET
                                  T.FS_PRODUCTNO='" + strProductno + "'" +
                                     " WHERE FS_BATCHNO='" + strBatch + "'";
                    ccp.ServerParams = new object[] { strSql };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    rowid = -1;






                }
            }

        private void ultraGrid1_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            rowid = ultraGrid1.ActiveRow.Index;
         
            if (rowid >= 0)
            {
                try
                {
                    this.txtBatchno.Text = this.ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text.ToString();
                    this.txtbandno.Text = this.ultraGrid1.Rows[rowid].Cells["FN_BANDNO"].Text.ToString();
                    this.txtreel.Text = this.ultraGrid1.Rows[rowid].Cells["FS_REEL"].Text.ToString();
                    this.txtStoveno.Text = this.ultraGrid1.Rows[rowid].Cells["FS_STOVENO"].Text.ToString();
                    this.txtweight.Text = this.ultraGrid1.Rows[rowid].Cells["FN_WEIGHT"].Text.ToString();
                    this.txtykl.Text = this.ultraGrid1.Rows[rowid].Cells["FN_YKL"].Text.ToString();
                    this.txtkhjz.Text = this.ultraGrid1.Rows[rowid].Cells["FN_KHJZ"].Text.ToString();
                    this.txtproductno.Text = this.ultraGrid1.Rows[rowid].Cells["FS_PRODUCTNO"].Text.ToString();
                    this.txtsteeltype.Text = this.ultraGrid1.Rows[rowid].Cells["FS_STEELTYPE"].Text.ToString();
                    this.txtspec.Text = this.ultraGrid1.Rows[rowid].Cells["FS_SPEC"].Text.ToString();
                    this.txtMemo.Text = this.ultraGrid1.Rows[rowid].Cells["FS_MEMO"].Text.ToString();
                    strWeightno = this.ultraGrid1.Rows[rowid].Cells["FS_WEIGHTNO"].Text.ToString();
                    strBatch = this.ultraGrid1.Rows[rowid].Cells["FS_BATCHNO"].Text.ToString();
                }

                catch
                {
                }
            }

        }




        private bool isvalid()
        {
            if (this.txtBatchno.Text == "")
            {
                MessageBox.Show("请输入轧制编号！");
                this.txtBatchno.Focus();
                return false;
            }
            try
            {
                Convert.ToInt16(txtbandno.Text);
               
            }
            catch
            {
                MessageBox.Show("输入数字格式的吊号！");
                this.txtbandno.Focus();
                return false;
            }
            if (this.txtreel.Text == "")
            {
                MessageBox.Show("请输入卷号！");
                this.txtreel.Focus();
                return false;
            }
            try
            {
                Convert.ToDouble(txtweight.Text.Trim());
            }
            catch
            {
                MessageBox.Show("请输入数字格式的重量！");
                this.txtweight.Focus();
                return false;
            }
             try
            {
                if (this.txtykl.Text != "")
                {
                    Convert.ToDouble(this.txtykl.Text.Trim());
                    //if (this.cbKZTYPE.Text == "")
                    //{
                    //    MessageBox.Show("请选择扣重类型！");
                    //    this.cbKZTYPE.Focus();
                    //    return false;

                    //}
                }

            }
            catch
            {
                MessageBox.Show("请输入数字格式的应扣量！");
                this.txtykl.Focus();
                return false;
            }
             if (this.txtykl.Text != "")
             {
                 if (Convert.ToDouble(txtweight.Text.Trim()) < Convert.ToDouble(this.txtykl.Text.Trim()))
                 {
                     MessageBox.Show("输入的应扣量应比重量小！");
                     this.txtykl.Focus();
                     return false;
                 }
             }
            if (this.txtStoveno.Text == "")
            {
                MessageBox.Show("请输入板坯炉号！");
                this.txtStoveno.Focus();
                return false;
            }
            if (this.txtproductno.Text == "")
            {
                MessageBox.Show("请输入订单号！");
                this.txtproductno.Focus();
                return false;
            }
            if (this.txtsteeltype.Text == "")
            {
                MessageBox.Show("请输入钢种！");
                this.txtsteeltype.Focus();
                return false;
            }
            if (this.txtspec.Text == "")
             {
                 MessageBox.Show("请输入规格！");
                 this.txtspec.Focus();
                 return false;
             }
            return true;
        
        }

        private void WeightUpdate_BoardNew_Load(object sender, EventArgs e)
        {
            m_BaseInfo_1.ob = this.ob;
          strUserName = UserInfo.GetUserName();//取样人
          strDepartMent = UserInfo.GetDepartment();//部门
            switch (this.CustomInfo)
            {
                case "sa"://取样界面
                   
                  
                    this.ultraToolbarsManager1.Toolbars[0].Tools["删除"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["新增"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["修改"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["复磅"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["修改订单"].SharedProps.Visible = false;
                    break;
                case"qu"://查询界面
                    this.splitContainer1.SplitterDistance = panel1.Height;
                    this.ultraGroupBox1.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["修改"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["删除"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["新增"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["取样"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["精整"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["复磅"].SharedProps.Visible = false;
                    break;
                case "up"://维护界面

                    
                    this.ultraToolbarsManager1.Toolbars[0].Tools["取样"].SharedProps.Visible = false;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["精整"].SharedProps.Visible = false;
                    
                    
                    break;
            }

        }

        private string GetNextReel()
        {
            string strResult = string.Empty;
            string strSQL = "select substr(to_char(sysdate,'yyyymm'),3,4)||'-'||LPAD(NVL(TO_NUMBER(SUBSTR(MAX(FS_REEL),6,4)),0)+1,4,'0') as fs_reel from dt_zkd_plan ";
            strSQL += " where FS_REEL like substr(to_char(sysdate,'yyyymm'),3,4)||'%'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            DataTable dt = new DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                strResult = dt.Rows[0]["fs_reel"].ToString();
            }
            return strResult;
        }

        private void txtykl_Leave(object sender, EventArgs e)
        {
            if (this.txtykl.Text != "")
            {
                if (Convert.ToDouble(txtweight.Text.Trim()) < Convert.ToDouble(this.txtykl.Text.Trim()))
                {
                    MessageBox.Show("输入的应扣量应比重量小！");
                    this.txtykl.Focus();
                    return;
                }
            }
            if (this.txtykl.Text != "")
            {
                this.txtkhjz.Text = (Convert.ToDouble(txtweight.Text.Trim()) - Convert.ToDouble(txtykl.Text.Trim())).ToString();
            }
            else
            {
                this.txtkhjz.Text = this.txtweight.Text;
            }
        }
    }
}
