using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using System.Collections;

namespace YGJZJL.CarCard
{
    public partial class CarCardSecond : FrmBase
    {
        int rowno = -1;
        String departmentcode = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment().Trim();//获得用户所在单位代码
        //String departmentcode = "HarporTest1";//测试用
        //String departmentcode = "HarporTest2";//测试用
        String username = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName().Trim();//获得用户
        //String username = "harpor";
        
        public CarCardSecond()
        {
            InitializeComponent();
        }

        private void CarCardSecond_Load(object sender, EventArgs e)
        {
            RefreshComBo();
        }

        // ultragrid过滤设置步骤：1,basic settings/feature picker/filtering/filter UI type/filter row,2,band and column settings/band[]/columns/fiteroperatordefaultvalue/fiteroperatordropdownitems
        private void ultraToolbarsManager1_ToolClick_1(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "CardQuery":
                    {
                        //以下为，只能查询和分配该登陆员单位所分配的车证卡区间

                        String selectsql_cardqj = "select t.FS_CARNOBEGIN,t.FS_CARNOEND from IT_MRP t where t.FS_MEMO='" + departmentcode + "'";

                        CoreClientParam cardqj = new CoreClientParam();
                        cardqj.ServerName = "ygjzjl.carcard.CarCard";
                        cardqj.MethodName = "queryByClientSql";
                        cardqj.ServerParams = new object[] { selectsql_cardqj };
                        cardqj.SourceDataTable = this.dataSet1.Tables["车证卡区间"];
                        this.dataSet1.Tables["车证卡区间"].Clear();
                        this.ExecuteQueryToDataTable(cardqj, CoreInvokeType.Internal);
                        DataTable czkqjtable = this.dataSet1.Tables["车证卡区间"];

                        String cardnobegin = "", cardnoend = "";
                        int cardnobegin_int = 0, cardnoend_int = 0;
                        if (czkqjtable.Rows.Count >0)
                        {
                            cardnobegin = czkqjtable.Rows[0]["FS_CARNOBEGIN"].ToString().Trim();
                            cardnoend = czkqjtable.Rows[0]["FS_CARNOEND"].ToString().Trim();
                        }
                        try
                        {
                            if (cardnobegin != null && !cardnobegin.Equals(""))
                            {
                                //String aa = cardnobegin.Substring(2, 5);
                                //cardnobegin_int = int.Parse(aa);
                                cardnobegin_int = int.Parse(cardnobegin);
                            }
                            if (cardnoend != null && !cardnoend.Equals(""))
                            {
                                //String aa = cardnoend.Substring(2, 5);
                                //cardnoend_int = int.Parse(aa);
                                cardnoend_int = int.Parse(cardnoend);
                            }
                        }
                        catch (Exception harporexc)
                        {
                            cardnobegin_int = 0;
                            cardnoend_int = 0;
                        }
                        //以上为，只能查询和分配该登陆员单位所分配的车证卡区间


                        //以下为根据车证卡序列号查询相关信息代码并显示，有且仅有一条记录
                        DateTime begintime = dateTimeBegin.Value;
                        DateTime endtime = dateTimeEnd.Value;
                        String xlh_str = txtXLH.Text.Trim();
                        if (begintime > endtime)
                        {
                            MessageBox.Show("截止日期不能小于开始日期！");
                            return;

                        }
                        String begintime_str = begintime.ToString("yyyy-MM-dd 00:00:00");
                        String endtime_str = endtime.ToString("yyyy-MM-dd 23:59:59");
                        String selectsql_ch = "select t.FS_CARDNUMBER,t.FS_SEQUENCENO,t.FS_INITDEPART,t.FS_INITUSER,to_char(t.FD_INITTIME,'YYYY-MM-DD HH24:MI:SS') as FD_INITTIME,t.FS_ASSIGNDEPART,t.FS_ASSIGNUSER,t.FS_USERCODE,t.FS_USEDEPART,t.FS_ISBINDTOCAR,t.FS_BINDCARNO,t.FS_USEPURPOSE,t.FS_MEMO,t.FS_CARDLEVEL,t.FS_ISVALID,t.FS_SECONDDEPART,a.fs_name as FS_CARDLEVELNAME,b.fs_name as FS_ISVALIDNAME from BT_CARDMANAGE t left join BT_FLAGCORRESPONDING a on a.FS_FLAG=t.FS_CARDLEVEL and a.fs_type='CARDLEVEL' left join BT_FLAGCORRESPONDING b on b.FS_FLAG=t.FS_ISVALID and b.fs_type='CARDVALID' where t.FD_INITTIME>=TO_DATE('" + begintime_str + "','YYYY-MM-DD HH24:MI:SS') and t.FD_INITTIME<=TO_DATE('" + endtime_str + "','YYYY-MM-DD HH24:MI:SS') and (To_number(SUBSTR(t.FS_SEQUENCENO,1,5)) between " + cardnobegin_int + " and " + cardnoend_int + ")";
                        
                        if (xlh_str != null && !xlh_str.Equals(""))
                        {
                            selectsql_ch+=" and t.FS_SEQUENCENO='" + xlh_str + "'";

                        }
                        CoreClientParam ccpquery = new CoreClientParam();
                        ccpquery.ServerName = "ygjzjl.carcard.CarCard";
                        ccpquery.MethodName = "queryByClientSql";
                        ccpquery.ServerParams = new object[] { selectsql_ch };
                        this.dataSet1.Tables["车证卡基础表"].Clear();
                        ccpquery.SourceDataTable = this.dataSet1.Tables["车证卡基础表"];
                        this.ExecuteQueryToDataTable(ccpquery, CoreInvokeType.Internal);

                        //this.setTextboxvalue();
                        //以上为根据车证卡序列号查询相关信息代码并显示，有且仅有一条记录，只能查该登陆员单位所分配的车证卡区间

                        break;
                    }

            }
        }

        private void ultraToolbarsManager2_ToolClick_1(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            String czkh = textCZKH.Text;
            String czkxlh = textCZKXLH.Text;
            if (czkh == null || czkh.Equals(""))
            {
                MessageBox.Show("没有车证卡信息，请查询选择！");
                return;
            }
            String ckr = textCKR.Text;
            String ckdw = comboCKDW.Text;

            //String yt = textYT.Text;
            String yt = "";
            String bdch = textBDCH.Text;
            String bdchbz = comboBDCHBZ.Text;
            String bz = textBZ.Text;
            //textbox还要再完善校验，未完成
            

            switch (e.Tool.Key.ToString())
            {
                case "CardManage":
                    {
                        //以下为二级单位分配车证卡确认时更新相关信息代码
                        if (ckr == null || ckr.Equals("")||ckdw == null || ckdw.Equals("")) 
                        {
                            MessageBox.Show("必须填写持卡人和持卡单位！");
                            return;
                        }

                        if (!checkInputValue())
                        {
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("0"))
                        {
                            MessageBox.Show("该卡已注销，不能再分配！");
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("4"))
                        {
                            MessageBox.Show("该卡已分配，不能再分配，请先回收！");
                            return;
                        }

                        if (!this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("2") && !this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("5"))
                        {
                            MessageBox.Show("一级单位尚为分配该卡给二级单位，请联系一级单位！");
                            return;
                        }

                        if (!bdch.Trim().Equals("") && bdchbz.Equals("0"))
                        {
                            MessageBox.Show("车号不为空，请将绑定车号标志置1");
                            return;
                        }

                        if (bdch.Trim().Equals("") && bdchbz.Equals("1"))
                        {
                            MessageBox.Show("车号为空，请将绑定车号标志置0");
                            return;
                        }

                        String sxbz_str = "4";
                        String updatesql = "update  BT_CARDMANAGE set FS_USERCODE='" + ckr + "',FS_USEDEPART='" + ckdw + "',FS_ISBINDTOCAR='" + bdchbz + "',FS_BINDCARNO='" + bdch + "',FS_USEPURPOSE='" + yt + "',FS_MEMO='" + bz + "',FS_ISVALID='" + sxbz_str + "',FS_SECONDDEPART='" + departmentcode + "'  where FS_CARDNUMBER='" + czkh + "'";
                        CoreClientParam fpccp = new CoreClientParam();
                        fpccp.ServerName = "ygjzjl.carcard.CarCard";
                        fpccp.MethodName = "updateByClientSql";
                        fpccp.ServerParams = new object[] { updatesql };
                        this.ExecuteNonQuery(fpccp, CoreInvokeType.Internal);
                        if (fpccp.ReturnCode == 0) //更新成功
                        {
                            MessageBox.Show("车证卡分配成功！");
                            //this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ASSIGNDEPART"] = departmentcode;
                            //this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ASSIGNUSER"] = username;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_USERCODE"] = ckr;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_USEDEPART"] = ckdw;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISBINDTOCAR"] = bdchbz;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_BINDCARNO"] = bdch;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_USEPURPOSE"] = yt;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_MEMO"] = bz;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALID"] = sxbz_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALIDNAME"] = "二级分配";

                            this.setTextboxvalue();
                            //以上为分配车证卡确认时更新相关信息代码

                            //车证卡操作表
                            String operationNO = Guid.NewGuid().ToString();

                            DateTime czsj = System.DateTime.Now;
                            String insertsql = "insert into  DT_CARDOPERATION(FS_OPERATENO,FS_SEQUENCENO,FS_CARDNO,FS_APPLYDEPART,FS_APPLYUSER,FS_OPERATIONTYPE,FS_OPERATEDEPART,FS_OPERATOR,FD_OPERATIONTIME) values ('" + operationNO + "','" + czkxlh + "','" + czkh + "','" + ckdw + "','" + ckr + "','二级分配','" + departmentcode + "','" + username + "',TO_DATE('" + czsj + "','YYYY-MM-DD HH24:MI:SS'))";
                            CoreClientParam ccp = new CoreClientParam();
                            ccp.ServerName = "ygjzjl.carcard.CarCard";
                            ccp.MethodName = "insertByClientSql";
                            ccp.ServerParams = new object[] { insertsql };
                            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                            //车证卡操作表
                        }
                        
                        break;
                    }
                case "CardBack":
                    {
                        //以下为回收车证卡确认时更新相关信息代码
                        
                        String ckr_str = "";//回收肯定三级为空
                        String ckdw_str = "";//回收肯定三级为空
                        String bdchbz_str = "";//回收肯定三级为空
                        String bdch_str = "";//回收肯定三级为空
                        String sxbz_str = "5";

                        if (!checkInputValue())
                        {
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("0"))
                        {
                            MessageBox.Show("该卡已注销，不能再回收！");
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("5"))
                        {
                            MessageBox.Show("该卡已回收，无须再回收！");
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("2"))
                        {
                            MessageBox.Show("该卡还未分配，无须回收！");
                            return;
                        }

                        if ( !this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("4"))
                        {
                            MessageBox.Show("一级单位尚为分配该卡给二级单位，不能回收！");
                            return;
                        }

                        String updatesql = "update  BT_CARDMANAGE set FS_USERCODE='" + ckr_str + "',FS_USEDEPART='" + ckdw_str + "',FS_ISBINDTOCAR='" + bdchbz_str + "',FS_BINDCARNO='" + bdch_str + "',FS_MEMO='" + bz + "',FS_ISVALID='" + sxbz_str + "' where FS_CARDNUMBER='" + czkh + "'";
                        CoreClientParam fpccp = new CoreClientParam();
                        fpccp.ServerName = "ygjzjl.carcard.CarCard";
                        fpccp.MethodName = "updateByClientSql";
                        fpccp.ServerParams = new object[] { updatesql };
                        this.ExecuteNonQuery(fpccp, CoreInvokeType.Internal);
                        if (fpccp.ReturnCode == 0) //更新成功
                        {
                            MessageBox.Show("车证卡回收成功！");
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_USERCODE"] = ckr_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_USEDEPART"] = ckdw_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISBINDTOCAR"] = bdchbz_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_BINDCARNO"] = bdch_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_MEMO"] = bz;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALID"] = sxbz_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALIDNAME"] = "二级回收";

                            this.setTextboxvalue();
                            //以上为回收车证卡确认时更新相关信息代码

                            //车证卡操作表
                            String operationNO = Guid.NewGuid().ToString();

                            String sqdw = this.ultraGrid3.Rows[rowno].GetCellValue("FS_USEDEPART").ToString().Trim();
                            String sqr = this.ultraGrid3.Rows[rowno].GetCellValue("FS_USERCODE").ToString().Trim();

                            DateTime czsj = System.DateTime.Now;
                            String insertsql = "insert into  DT_CARDOPERATION(FS_OPERATENO,FS_SEQUENCENO,FS_CARDNO,FS_APPLYDEPART,FS_APPLYUSER,FS_OPERATIONTYPE,FS_OPERATEDEPART,FS_OPERATOR,FD_OPERATIONTIME) values ('" + operationNO + "','" + czkxlh + "','" + czkh + "','" + sqdw + "','" + sqr + "','二级回收','" + departmentcode + "','" + username + "',TO_DATE('" + czsj + "','YYYY-MM-DD HH24:MI:SS'))";
                            CoreClientParam ccp = new CoreClientParam();
                            ccp.ServerName = "ygjzjl.carcard.CarCard";
                            ccp.MethodName = "insertByClientSql";
                            ccp.ServerParams = new object[] { insertsql };
                            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                            //车证卡操作表
                        }
                        
                        break;
                    }

            }
        }

        private void ultraGrid3_AfterRowActivate(object sender, EventArgs e)
        {
            //try
            //{
            //    rowno = ultraGrid3.ActiveRow.Index;
            //}
            //catch (Exception e1)
            //{
            //    MessageBox.Show("没有激活行！");
            //    return;
            //}
        }

        private void ultraGrid3_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            //DataTable czkjcb=this.dataSet1.Tables["车证卡基础表"];

            rowno = ultraGrid3.ActiveRow.Index;
            if (rowno > -1)
            {
                this.setTextboxvalue();
            }
            
        }



        private void RefreshComBo()
        {
            BDCH_new tscl0 = new BDCH_new("0", "0");
            BDCH_new tscl1 = new BDCH_new("1", "1");
            List<BDCH_new> listtscl = new List<BDCH_new>();
            listtscl.Add(tscl0);
            listtscl.Add(tscl1);

            comboBDCHBZ.DataSource = listtscl;
            comboBDCHBZ.DisplayMember = "name";
            comboBDCHBZ.ValueMember = "flag";
            //此处应该根据单位代码反查单位名称，IT_MRP
            String selectsql = "select distinct t.FS_USEDEPART from BT_CARDMANAGE t where t.FS_SECONDDEPART='" + departmentcode + "'";
            DataTable testdatatable = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.carcard.CarCard";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { selectsql };
            ccp.SourceDataTable = testdatatable;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (testdatatable.Rows.Count > 0)
            {
                DataRow newrow = testdatatable.NewRow();
                newrow["FS_USEDEPART"] = "";
                testdatatable.Rows.InsertAt(newrow, 0);//加入一个空值
                comboCKDW.DataSource = testdatatable;
                comboCKDW.DisplayMember = "FS_USEDEPART";
            }
        }


        private bool checkInputValue()
        {

            if (textCZKH.Text.Trim().Length > 30)
            {
                MessageBox.Show("车证卡号字符数不能超过30！");
                textCZKH.Focus();
                return false;
            }

            if (textCZKXLH.Text.Trim().Length > 10)
            {
                MessageBox.Show("车证卡序列号字符数不能超过10！");
                textCZKXLH.Focus();
                return false;
            }


            if (textCKR.Text.Trim().Length > 10)//中文一字符长度为2
            {
                MessageBox.Show("持卡人字符数不能超过10！");
                textCKR.Focus();
                return false;
            }

            if (textBDCH.Text.Trim().Length > 10)//中文一字符长度为2
            {
                MessageBox.Show("绑定车号字符数不能超过10！");
                textBDCH.Focus();
                return false;
            }


            if (comboCKDW.Text.Trim().Length > 25)//中文一字符长度为2
            {
                MessageBox.Show("持卡单位字符数不能超过25！");
                comboCKDW.Focus();
                return false;
            }

            if (textBZ.Text.Trim().Length > 125)//中文一字符长度为2
            {
                MessageBox.Show("备注字符数不能超过125！");
                textBZ.Focus();
                return false;
            }
            return true;
        }


        
        private void setTextboxvalue()
        {
            textCZKH.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_CARDNUMBER").ToString();
            textCZKXLH.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_SEQUENCENO").ToString();
            textCKR.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_USERCODE").ToString();
            comboCKDW.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_USEDEPART").ToString();
            //textYT.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_USEPURPOSE").ToString();
            textBDCH.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_BINDCARNO").ToString();
            comboBDCHBZ.SelectedValue = ultraGrid3.Rows[rowno].GetCellValue("FS_ISBINDTOCAR").ToString();
            textBZ.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_MEMO").ToString();
        }



        public class BDCH_new
        {
            private string name;
            public string Name
            {
                get
                {
                    return name;
                }
            }

            private string flag;
            public string Flag
            {
                get
                {
                    return flag;
                }
            }

            public BDCH_new(string name, string flag)
            {
                this.name = name;
                this.flag = flag;
            }

            public override string ToString()
            {
                return name + "," + flag;
            }
        }

       


    }
}
