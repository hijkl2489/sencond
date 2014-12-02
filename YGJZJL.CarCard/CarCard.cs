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
using YGJZJL.PublicComponent;
using YGJZJL.CarSip.Client.Meas;
using YGJZJL.CarSip.Client.App;

namespace YGJZJL.CarCard
{
    public partial class CarCard : FrmBase
    {
        //周胜
        int rowno = -1;
        String departmentcode = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment().Trim();//获得用户所在单位代码，部门代码未获得问孙工
        //String departmentcode = "HarporTest1";//测试用
        //String departmentcode = "HarporTest2";//测试用
        String username = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName().Trim();//获得用户
        //String username = "harpor";

        //读卡器操作
        //private System.Threading.Thread m_hThread = null;
        //bool m_bRunning = false;

        /// <summary>
        /// 定义一个读卡器操作对象,用于对MW读写器进行初始化(例如连接设备和打开通讯模块)等操作
        /// </summary>
        private RegistIcCard _icCard = null;


        /// <summary>
        /// 在构造函数（绑定串口数组到ComboBox控件）
        /// </summary>
        public CarCard()
        {
            InitializeComponent();
            try
            {
                string[] portnames = System.IO.Ports.SerialPort.GetPortNames();//获取本地计算机可连接的串口数组
                if (portnames is Nullable || portnames.Length < 1)
                {
                    portnames = new string[10] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10" };
                }
                comboSerialNO.DataSource = portnames;

                _icCard = new RegistIcCard();
            }
            catch (Exception eex3)
            {

            }

        }



        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            String czkh = textCZKH.Text;
            String czkxlh = textCZKXLH.Text;

            String fpr = textFPR.Text;
            String fpdw = comboFPDW.Text;
            //String yt = textYT.Text;
            String jb = "";

            String strXb = comboSex.Text.Trim();
            String strGwei = comboGwei.Text.Trim();
            String strBmen = comboBmen.Text.Trim();
            String strSfzh = textSFZH.Text.Trim();
            String strLxdh = textLXDH.Text.Trim();
            try
            {
                jb = comboJB.SelectedValue.ToString();//级别(车证卡级别：身份卡，专用卡)
            }
            catch (Exception ee1)
            {

            }

            String jbname = comboJB.Text.ToString();
            String yt = "";
            String bz = textBZ.Text;

            switch (e.Tool.Key.ToString())
            {
                case "ReadCard":
                    {
                        return;

                        rowno = -1;
                        short serialno = -1;
                        try
                        {
                            serialno = (short)(short.Parse(comboSerialNO.Text.Replace("COM", "")) - 1);
                        }
                        catch (Exception ee4)
                        {
                            MessageBox.Show("请选择串口号！");
                            return;
                        }

                        try
                        {
                            CardData cardData = _icCard.ReadCard();
                            textCZKXLH.Text = cardData.CardNo;
                            textCZKH.Text = cardData.ID;
                            textCZKXLH.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("串口打开失败，请检查设备、串口号设置！");
                        }
                        break;
                    }

                //注册应该在刷卡时判断是否已注册，并且只能一次注册一张卡
                case "CardRegist":
                    RegistCard();
                    break;
                case "CardManage": //分配车证卡
                    {
                        //卡号，序列号，制证单位，制证人，制证时间不可编辑
                        //以下为分配车证卡确认时更新相关信息代码
                        if (rowno < 0)
                        {
                            MessageBox.Show("没有选择车证卡信息，请查询并双击选择！");
                            return;
                        }
                        if (czkh == null || czkh.Equals("") || czkxlh == null || czkxlh.Equals(""))
                        {
                            MessageBox.Show("没有车证卡信息，请查询选择！");
                            return;
                        }
                        if (fpr == null || fpr.Equals("") || fpdw == null || fpdw.Equals(""))
                        {
                            MessageBox.Show("必须填写分配人和分配单位！");
                            return;
                        }

                        //Ksh:当车证卡的级别为0时代表身份卡,应输入岗位(卸货员/管理员)字段 
                        if (jb.Equals("0"))
                        {
                            if (string.IsNullOrEmpty(strGwei.Trim()))
                            {
                                MessageBox.Show("请选择岗位！");
                                return;
                            }
                        }

                        if (!checkInputValue())
                        {
                            return;
                        }

                        if (strSfzh.Length > 25)//中文一字符长度为2
                        {
                            MessageBox.Show("身份证号字符数不能超过25！");
                            textSFZH.Focus();
                            return;
                        }

                        if (strLxdh.Length > 15)//中文一字符长度为2
                        {
                            MessageBox.Show("联系电话字符数不能超过15！");
                            textLXDH.Focus();
                            return;
                        }

                        if (strGwei.Length > 15)//中文一字符长度为2
                        {
                            MessageBox.Show("岗位字符数不能超过15！");
                            comboGwei.Focus();
                            return;
                        }

                        if (strBmen.Length > 25)//中文一字符长度为2
                        {
                            MessageBox.Show("部门字符数不能超过25！");
                            comboBmen.Focus();
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("0"))
                        {
                            MessageBox.Show("该卡已注销，不能再分配！");
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("2") || this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("4") || this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("5"))
                        {
                            MessageBox.Show("该卡已分配给二级单位，不能再分配，请先回收！");
                            return;
                        }


                        String sxbz_str = "2";
                        String updatesql = "update  BT_CARDMANAGE set FS_ASSIGNDEPART='" + fpdw + "',FS_ASSIGNUSER='" + fpr + "',FS_USEPURPOSE='" + yt + "',FS_MEMO='" + bz + "',FS_CARDLEVEL='" + jb + "',FS_ISVALID='" + sxbz_str + "',FS_SEX='" + strXb + "',FS_IDENTITYNO='" + strSfzh + "',FS_CONTACTPHONE='" + strLxdh + "',FS_APARTMENT='" + strBmen + "',FS_ROLE='" + strGwei + "' where FS_CARDNUMBER='" + czkh + "'";
                        CoreClientParam fpccp = new CoreClientParam();
                        fpccp.ServerName = "ygjzjl.carcard.CarCard";
                        fpccp.MethodName = "updateByClientSql";
                        fpccp.ServerParams = new object[] { updatesql };
                        this.ExecuteNonQuery(fpccp, CoreInvokeType.Internal);

                        if (fpccp.ReturnCode == 0) //更新成功
                        {
                            MessageBox.Show("车证卡分配成功！");
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ASSIGNDEPART"] = fpdw;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ASSIGNUSER"] = fpr;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_CARDLEVEL"] = jb;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_CARDLEVELNAME"] = jbname;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_USEPURPOSE"] = yt;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_MEMO"] = bz;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALID"] = sxbz_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALIDNAME"] = "一级分配";
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_SEX"] = strXb;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_IDENTITYNO"] = strSfzh;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_CONTACTPHONE"] = strLxdh;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_APARTMENT"] = strBmen;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ROLE"] = strGwei;

                            this.setTextboxvalue();
                            //以上为分配车证卡确认时更新相关信息代码

                            //车证卡操作表
                            String operationNO = Guid.NewGuid().ToString();

                            DateTime czsj = System.DateTime.Now;
                            String insertsql = "insert into  DT_CARDOPERATION(FS_OPERATENO,FS_SEQUENCENO,FS_CARDNO,FS_APPLYDEPART,FS_APPLYUSER,FS_OPERATIONTYPE,FS_OPERATEDEPART,FS_OPERATOR,FD_OPERATIONTIME) values ('" + operationNO + "','" + czkxlh + "','" + czkh + "','" + fpdw + "','" + fpr + "','一级分配','" + departmentcode + "','" + username + "',TO_DATE('" + czsj + "','YYYY-MM-DD HH24:MI:SS'))";
                            CoreClientParam ccp = new CoreClientParam();
                            ccp.ServerName = "ygjzjl.carcard.CarCard";
                            ccp.MethodName = "insertByClientSql";
                            ccp.ServerParams = new object[] { insertsql };
                            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                            //车证卡操作表
                        }

                        break;
                    }
                case "CardDispose":
                    {
                        //以下为注销车证卡确认时更新相关信息代码
                        if (rowno < 0)
                        {
                            MessageBox.Show("没有选择车证卡信息，请查询并双击选择！");
                            return;
                        }
                        if (czkh == null || czkh.Equals("") || czkxlh == null || czkxlh.Equals(""))
                        {
                            MessageBox.Show("没有车证卡信息，请查询选择！");
                            return;
                        }

                        if (!checkInputValue())
                        {
                            return;
                        }

                        if (DialogResult.No == MessageBox.Show("注销卡号后，该卡将不能再使用，确定注销卡号：" + czkh + "吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("0"))
                        {
                            MessageBox.Show("该卡已注销，无须再注销！");
                            return;
                        }

                        String sxbz_str = "0";
                        String updatesql = "update  BT_CARDMANAGE set FS_ASSIGNDEPART='" + fpdw + "',FS_ASSIGNUSER='" + fpr + "',FS_CARDLEVEL='" + jb + "',FS_USEPURPOSE='" + yt + "',FS_MEMO='" + bz + "',FS_ISVALID='" + sxbz_str + "' where FS_CARDNUMBER='" + czkh + "'";
                        CoreClientParam fpccp = new CoreClientParam();
                        fpccp.ServerName = "ygjzjl.carcard.CarCard";
                        fpccp.MethodName = "updateByClientSql";
                        fpccp.ServerParams = new object[] { updatesql };
                        this.ExecuteNonQuery(fpccp, CoreInvokeType.Internal);

                        if (fpccp.ReturnCode == 0) //更新成功
                        {
                            MessageBox.Show("车证卡注销成功！");
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ASSIGNDEPART"] = fpdw;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ASSIGNUSER"] = fpr;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_CARDLEVEL"] = jb;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_CARDLEVELNAME"] = jbname;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_USEPURPOSE"] = yt;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_MEMO"] = bz;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALID"] = sxbz_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALIDNAME"] = "注销";
                            this.setTextboxvalue();
                            //以上为注销车证卡确认时更新相关信息代码

                            //车证卡操作表
                            String operationNO = Guid.NewGuid().ToString();

                            String sqdw = this.ultraGrid3.Rows[rowno].GetCellValue("FS_ASSIGNDEPART").ToString().Trim();
                            String sqr = this.ultraGrid3.Rows[rowno].GetCellValue("FS_ASSIGNUSER").ToString().Trim();

                            DateTime czsj = System.DateTime.Now;
                            String insertsql = "insert into  DT_CARDOPERATION(FS_OPERATENO,FS_SEQUENCENO,FS_CARDNO,FS_APPLYDEPART,FS_APPLYUSER,FS_OPERATIONTYPE,FS_OPERATEDEPART,FS_OPERATOR,FD_OPERATIONTIME) values ('" + operationNO + "','" + czkxlh + "','" + czkh + "','" + sqdw + "','" + sqr + "','注销','" + departmentcode + "','" + username + "',TO_DATE('" + czsj + "','YYYY-MM-DD HH24:MI:SS'))";
                            CoreClientParam ccp = new CoreClientParam();
                            ccp.ServerName = "ygjzjl.carcard.CarCard";
                            ccp.MethodName = "insertByClientSql";
                            ccp.ServerParams = new object[] { insertsql };
                            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                            //车证卡操作表
                        }

                        break;
                    }
                case "Back":
                    {
                        //以下为回收车证卡确认时更新相关信息代码
                        if (rowno < 0)
                        {
                            MessageBox.Show("没有选择车证卡信息，请查询并双击选择！");
                            return;
                        }
                        if (czkh == null || czkh.Equals("") || czkxlh == null || czkxlh.Equals(""))
                        {
                            MessageBox.Show("没有车证卡信息，请查询选择！");
                            return;
                        }

                        if (!checkInputValue())
                        {
                            return;
                        }

                        if (strSfzh.Length > 25)//中文一字符长度为2
                        {
                            MessageBox.Show("身份证号字符数不能超过25！");
                            textSFZH.Focus();
                            return;
                        }

                        if (strLxdh.Length > 15)//中文一字符长度为2
                        {
                            MessageBox.Show("联系电话字符数不能超过15！");
                            textLXDH.Focus();
                            return;
                        }

                        if (strGwei.Length > 15)//中文一字符长度为2
                        {
                            MessageBox.Show("岗位字符数不能超过15！");
                            comboGwei.Focus();
                            return;
                        }

                        if (strBmen.Length > 25)//中文一字符长度为2
                        {
                            MessageBox.Show("部门字符数不能超过25！");
                            comboBmen.Focus();
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("3"))
                        {
                            MessageBox.Show("该卡已回收，无须再回收！");
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("0"))
                        {
                            MessageBox.Show("该卡已注销，不能再回收！");
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("1"))
                        {
                            MessageBox.Show("该卡刚注册，不能回收！");
                            return;
                        }

                        if (this.ultraGrid3.Rows[rowno].GetCellValue("FS_ISVALID").ToString().Trim().Equals("4"))
                        {
                            MessageBox.Show("该卡已被二级单位分配出去，二级单位必须先回收！");
                            return;
                        }

                        String sxbz_str = "3";
                        String updatesql = "update  BT_CARDMANAGE set FS_ASSIGNDEPART='" + fpdw + "',FS_ASSIGNUSER='" + fpr + "',FS_CARDLEVEL='" + jb + "',FS_USEPURPOSE='" + yt + "',FS_MEMO='" + bz + "',FS_ISVALID='" + sxbz_str + "',FS_SEX='" + strXb + "',FS_IDENTITYNO='" + strSfzh + "',FS_CONTACTPHONE='" + strLxdh + "',FS_APARTMENT='" + strBmen + "',FS_ROLE='" + strGwei + "' where FS_CARDNUMBER='" + czkh + "'";

                        CoreClientParam fpccp = new CoreClientParam();
                        fpccp.ServerName = "ygjzjl.carcard.CarCard";
                        fpccp.MethodName = "updateByClientSql";
                        fpccp.ServerParams = new object[] { updatesql };
                        this.ExecuteNonQuery(fpccp, CoreInvokeType.Internal);
                        if (fpccp.ReturnCode == 0) //更新成功
                        {
                            MessageBox.Show("车证卡回收成功！");
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ASSIGNDEPART"] = fpdw;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ASSIGNUSER"] = fpr;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_CARDLEVEL"] = jb;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_CARDLEVELNAME"] = jbname;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_USEPURPOSE"] = yt;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_MEMO"] = bz;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALID"] = sxbz_str;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ISVALIDNAME"] = "一级回收";
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_SEX"] = strXb;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_IDENTITYNO"] = strSfzh;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_CONTACTPHONE"] = strLxdh;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_APARTMENT"] = strBmen;
                            this.dataSet1.Tables["车证卡基础表"].Rows[rowno]["FS_ROLE"] = strGwei;
                            this.setTextboxvalue();
                            //以上为回收车证卡确认时更新相关信息代码

                            //车证卡操作表
                            String operationNO = Guid.NewGuid().ToString();

                            String sqdw = this.ultraGrid3.Rows[rowno].GetCellValue("FS_ASSIGNDEPART").ToString().Trim();
                            String sqr = this.ultraGrid3.Rows[rowno].GetCellValue("FS_ASSIGNUSER").ToString().Trim();

                            DateTime czsj = System.DateTime.Now;
                            String insertsql = "insert into  DT_CARDOPERATION(FS_OPERATENO,FS_SEQUENCENO,FS_CARDNO,FS_APPLYDEPART,FS_APPLYUSER,FS_OPERATIONTYPE,FS_OPERATEDEPART,FS_OPERATOR,FD_OPERATIONTIME) values ('" + operationNO + "','" + czkxlh + "','" + czkh + "','" + sqdw + "','" + sqr + "','一级回收','" + departmentcode + "','" + username + "',TO_DATE('" + czsj + "','YYYY-MM-DD HH24:MI:SS'))";
                            CoreClientParam ccp = new CoreClientParam();
                            ccp.ServerName = "ygjzjl.carcard.CarCard";
                            ccp.MethodName = "insertByClientSql";
                            ccp.ServerParams = new object[] { insertsql };
                            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                            //车证卡操作表
                        }

                        break;
                    }
                case "BatchFP":
                    {
                        String strCHFrom = txtCHFrom.Text.Trim();
                        String strCHTo = txtCHTo.Text.Trim();
                        int intCHFrom = 0, intCHTo = 0;
                        if (strCHFrom.Length != 5)
                        {
                            MessageBox.Show("起始车证卡号必须为5位数字编号！");
                            return;
                        }
                        if (strCHTo.Length != 5)
                        {
                            MessageBox.Show("截止车证卡号必须为5位数字编号！");
                            return;
                        }
                        try
                        {
                            intCHFrom = int.Parse(strCHFrom);
                        }
                        catch (Exception ee2)
                        {
                            MessageBox.Show("起始车证卡号必须为5位数字编号！");
                            return;
                        }
                        try
                        {
                            intCHTo = int.Parse(strCHTo);
                        }
                        catch (Exception ee2)
                        {
                            MessageBox.Show("截止车证卡号必须为5位数字编号！");
                            return;
                        }
                        if (intCHFrom > intCHTo)
                        {
                            MessageBox.Show("截止车证卡号必须大于起始编号！");
                            return;
                        }
                        if (fpr == null || fpr.Equals("") || fpdw == null || fpdw.Equals(""))
                        {
                            MessageBox.Show("必须填写分配人和分配单位！");
                            return;
                        }
                        if (jb.Equals("0"))
                        {
                            MessageBox.Show("身份卡不能批量分配！");
                            return;
                        }
                        if (textFPR.Text.Trim().Length > 10)//中文一字符长度为2
                        {
                            MessageBox.Show("分配人字符数不能超过10！");
                            textFPR.Focus();
                            return;
                        }
                        if (comboFPDW.Text.Trim().Length > 25)//中文一字符长度为2
                        {
                            MessageBox.Show("分配单位字符数不能超过25！");
                            comboFPDW.Focus();
                            return;
                        }
                        if (textBZ.Text.Trim().Length > 125)//中文一字符长度为2
                        {
                            MessageBox.Show("备注字符数不能超过125！");
                            textBZ.Focus();
                            return;
                        }
                        if (DialogResult.No == MessageBox.Show("批量分配只能对处于注册和一级回收状态的卡号有效，确定分配：" + strCHFrom + "至：" + strCHTo + "的卡号吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            return;
                        }

                        String sxbz_str = "2";
                        String updatesql = "update  BT_CARDMANAGE set FS_ASSIGNDEPART='" + fpdw + "',FS_ASSIGNUSER='" + fpr + "',FS_USEPURPOSE='" + yt + "',FS_MEMO='" + bz + "',FS_CARDLEVEL='" + jb + "',FS_ISVALID='" + sxbz_str + "' where (To_number(SUBSTR(FS_SEQUENCENO,1,5)) between " + intCHFrom + " and " + intCHTo + ") and (FS_ISVALID='1' or FS_ISVALID='3')";
                        CoreClientParam fpccp = new CoreClientParam();
                        fpccp.ServerName = "ygjzjl.carcard.CarCard";
                        fpccp.MethodName = "updateByClientSql";
                        fpccp.ServerParams = new object[] { updatesql };
                        this.ExecuteNonQuery(fpccp, CoreInvokeType.Internal);

                        if (fpccp.ReturnCode == 0) //更新成功
                        {
                            MessageBox.Show("车证卡批量分配成功！");

                        }
                        break;
                    }
            }
        }

        private void RegistCard()
        {
            String czkh = textCZKH.Text; //车证卡芯片号
            String czkxlh = textCZKXLH.Text;//车证卡编号
            String fpr = textFPR.Text;      //分配人(分配给谁用)
            String fpdw = comboFPDW.Text;   //分配单位(对卸货身份卡而言,就是卸货点所属单位)
            //String yt = textYT.Text;
            String jb = ""; //车证卡级别(卸货卡，身份卡，临时卡)
            String strXb = comboSex.Text.Trim();
            String strGwei = comboGwei.Text.Trim();//岗位(对卸货身份卡而言，就是权限 卸货员/管理员)
            String strBmen = comboBmen.Text.Trim();
            String strSfzh = textSFZH.Text.Trim();//身份证号
            String strLxdh = textLXDH.Text.Trim();//联系电话  

            try
            {
                jb = comboJB.SelectedValue.ToString();//车证卡级别
            }
            catch (Exception ee1)
            {

            }

            String jbname = comboJB.Text.ToString();
            String yt = ""; //用途
            String bz = textBZ.Text;//备注
            //卡号不可编辑
            //以下为注册车证卡确认时更新相关信息代码

            rowno = -1;//防止应该注册的操作，去操作其他按钮
            if (!textCZKXLH.Enabled)
            {
                MessageBox.Show("还未读卡，请先读卡！");//车证卡芯片序列号
                return;
            }
            czkh = textCZKH.Text.Trim();
            czkxlh = textCZKXLH.Text.Trim();
            if (czkh == null || czkh.Equals(""))
            {
                MessageBox.Show("没有车证卡芯片信息，请刷卡！");
                return;
            }
            if (czkxlh == null || czkxlh.Equals(""))
            {
                MessageBox.Show("请输入车证卡号！");
                return;
            }
            if (czkxlh.Length != 5)
            {
                MessageBox.Show("车证卡号必须为5位数字编号！");
                return;
            }
            try
            {
                int.Parse(czkxlh);
            }
            catch (Exception ee2)
            {
                MessageBox.Show("车证卡号必须为5位数字编号！");
                return;
            }

            if (!checkInputValue())
            {
                return;
            }
            DateTime zzsj = System.DateTime.Now;
            String sxbz_str = "1";//生效标志位

            DataTable testdatatable = new DataTable();//测试输入的卡号和芯片号是否已经存在
            String isExistsql = "select t.FS_SEQUENCENO from BT_CARDMANAGE t where t.FS_SEQUENCENO='" + czkxlh + "'";
            CoreClientParam selectccp = new CoreClientParam();
            selectccp.ServerName = "ygjzjl.carcard.CarCard";
            selectccp.MethodName = "queryByClientSql";
            selectccp.ServerParams = new object[] { isExistsql };
            selectccp.SourceDataTable = testdatatable;
            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
            if (testdatatable.Rows.Count > 0)
            {
                MessageBox.Show("该序列号已经存在，不能注册！");
                return;
            }
            testdatatable.Clear();//若该卡号不存在，则清空临时表testdatatable，并继续用其验证芯片号是否存在
            isExistsql = "select t.FS_CARDNUMBER from BT_CARDMANAGE t where t.FS_CARDNUMBER='" + czkh + "'";
            //CoreClientParam selectccp = new CoreClientParam();
            selectccp.ServerName = "ygjzjl.carcard.CarCard";
            selectccp.MethodName = "queryByClientSql";
            selectccp.ServerParams = new object[] { isExistsql };
            selectccp.SourceDataTable = testdatatable;
            this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
            if (testdatatable.Rows.Count > 0)
            {
                if (DialogResult.No == MessageBox.Show("芯片号：" + czkh + "的车证卡已经注册，要更新吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    textCZKXLH.Text = "";
                    textCZKH.Text = "";
                    textCZKXLH.Enabled = false;//设置为不可编辑
                    return;
                }
                else //如果确定要更新已经注册的车证卡(芯片号已经存在，允许修改车证卡编号) 
                {
                    CardData cardData = new CardData();
                    cardData.CardNo = czkxlh;
                    if (checkBox1.Checked && _icCard.CardThread.ThreadState == System.Threading.ThreadState.Running)
                    {
                        _icCard.CardThread.Suspend();
                    }
                    switch (comboJB.Text.Trim())
                    {
                        case "身份卡":
                            cardData.CardType = "0";
                            if (string.IsNullOrEmpty(fpr.Trim()))
                            {
                                MessageBox.Show("请选择分配人！");
                                return;
                            }
                            cardData.FirLocNo = fpr.Trim();

                            if (string.IsNullOrEmpty(strGwei.Trim()))
                            {
                                MessageBox.Show("请选择岗位！");
                                return;
                            }
                            cardData.FirWeight = strGwei.Trim();

                            if (string.IsNullOrEmpty(fpdw.Trim()))
                            {
                                MessageBox.Show("请选择分配单位！");
                                return;
                            }
                            cardData.MateriaName = fpdw.Trim();
                            break;
                        case "计量卡":
                            cardData.CardType = "1";
                            break;
                        case "临时卡":
                            cardData.CardType = "2";
                            break;
                        default:
                            MessageBox.Show("请选择车证卡类型！");
                            return;
                    }
                    bool flag = _icCard.WriteCard(cardData);//写卡操作
                    if (checkBox1.Checked && _icCard.CardThread.ThreadState == System.Threading.ThreadState.Suspended)
                    {
                        _icCard.CardThread.Resume();
                    }
                    if (!flag)
                    {
                        MessageBox.Show("车证卡号没有成功写入卡内扇区，请确认卡放置正确！");
                        textCZKXLH.Text = "";
                        textCZKH.Text = "";
                        textCZKXLH.Enabled = false;
                        return;
                    }
                    //String manageinsertsql = "update BT_CARDMANAGE t set t.FS_SEQUENCENO='" + czkxlh + "',t.FS_INITDEPART='" + departmentcode + "',t.FS_INITUSER='" + username + "',t.FD_INITTIME=TO_DATE('" + zzsj + "','YYYY-MM-DD HH24:MI:SS'),t.FS_USEPURPOSE='" + yt + "',t.FS_MEMO='" + bz + "',t.FS_CARDLEVEL='" + jb + "',t.FS_ISVALID='" + sxbz_str + "' where t.FS_CARDNUMBER='" + czkh + "'";
                    String manageinsertsql = @"update BT_CARDMANAGE t 
                                                set 
                                                    t.FS_SEQUENCENO='" + czkxlh + @"',
                                                    t.FS_INITDEPART='" + departmentcode + @"',
                                                    t.FS_INITUSER='" + username + @"',
                                                    t.FD_INITTIME=TO_DATE('" + zzsj + @"','YYYY-MM-DD HH24:MI:SS'),
                                                    t.FS_USEPURPOSE='" + yt + @"',
                                                    t.FS_MEMO='" + bz + @"',
                                                    t.FS_CARDLEVEL='" + jb + @"',
                                                    t.FS_ISVALID='" + sxbz_str + @"' ,

                                                    t.FS_ASSIGNDEPART ='" + fpdw + @"',
                                                    t.FS_ASSIGNUSER ='" + fpr + @"'
                                                where
                                                    t.FS_CARDNUMBER='" + czkh + @"'";

                    CoreClientParam reccp = new CoreClientParam();
                    reccp.ServerName = "ygjzjl.carcard.CarCard";
                    reccp.MethodName = "updateByClientSql";
                    reccp.ServerParams = new object[] { manageinsertsql };
                    this.ExecuteNonQuery(reccp, CoreInvokeType.Internal);
                    if (reccp.ReturnCode == 0) //更新成功
                    {
                        MessageBox.Show("车证卡更新成功！");
                        DataRow newczkrow = this.dataSet1.Tables["车证卡基础表"].NewRow();
                        newczkrow["FS_CARDNUMBER"] = czkh;
                        newczkrow["FS_SEQUENCENO"] = czkxlh;
                        newczkrow["FS_INITDEPART"] = departmentcode;
                        newczkrow["FS_INITUSER"] = username;
                        newczkrow["FD_INITTIME"] = zzsj.ToString();
                        newczkrow["FS_USEPURPOSE"] = yt;
                        newczkrow["FS_MEMO"] = bz;
                        newczkrow["FS_CARDLEVEL"] = jb;
                        newczkrow["FS_CARDLEVELNAME"] = jbname;
                        newczkrow["FS_ISVALID"] = sxbz_str;
                        newczkrow["FS_ISVALIDNAME"] = "注册";

                        this.dataSet1.Tables["车证卡基础表"].Rows.Add(newczkrow);
                        dataSet1.AcceptChanges();
                        //以上为注册车证卡确认时更新相关信息代码

                        //车证卡操作表(车证卡操作日志)
                        String operationNO = Guid.NewGuid().ToString();
                        String insertsql = "insert into  DT_CARDOPERATION(FS_OPERATENO,FS_SEQUENCENO,FS_CARDNO,FS_APPLYDEPART,FS_APPLYUSER,FS_OPERATIONTYPE,FS_OPERATEDEPART,FS_OPERATOR,FD_OPERATIONTIME) values ('" + operationNO + "','" + czkxlh + "','" + czkh + "','','','更新','" + departmentcode + "','" + username + "',TO_DATE('" + zzsj + "','YYYY-MM-DD HH24:MI:SS'))";
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.carcard.CarCard";
                        ccp.MethodName = "insertByClientSql";
                        ccp.ServerParams = new object[] { insertsql };
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        //车证卡操作表
                    }

                    textCZKXLH.Enabled = false;//注册完后，设置为不可编辑
                }
            }
            else//不存在，则插入(卡号和芯片号均不存在)
            {
                CardData cardData = new CardData();
                cardData.CardNo = czkxlh; //车证卡编号
                if (checkBox1.Checked && _icCard.CardThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    _icCard.CardThread.Suspend(); //如果(自动读卡&&线程正在运行)则挂起ic卡线程,等待写卡操作
                }
                switch (comboJB.Text.Trim())
                {
                    case "身份卡":
                        cardData.CardType = "0";
                        if (string.IsNullOrEmpty(fpr.Trim()))
                        {
                            MessageBox.Show("请填写分配人！");
                            return;
                        }
                        cardData.FirLocNo = fpr.Trim();
                        if (string.IsNullOrEmpty(strGwei.Trim()))
                        {
                            MessageBox.Show("请选择岗位！");
                            return;
                        }
                        cardData.FirWeight = strGwei.Trim();

                        //在此处添加分配单位(卸货身份卡：卸货地点)
                        if (string.IsNullOrEmpty(fpdw.Trim()))
                        {
                            MessageBox.Show("请选择分配单位！");
                            return;
                        }
                        cardData.MateriaName = fpdw.Trim();//写卡到 Scetor2 扇区
                        break;
                    case "计量卡":
                        cardData.CardType = "1";
                        break;
                    case "临时卡":
                        cardData.CardType = "2";
                        break;
                    default:
                        MessageBox.Show("请选择车证卡类型！");
                        return;
                }
                bool flag = _icCard.WriteCard(cardData);//执行写卡操作，并返回写卡成功true(失败false)标志
                if (checkBox1.Checked && _icCard.CardThread.ThreadState == System.Threading.ThreadState.Suspended)
                {
                    _icCard.CardThread.Resume();  //继续已挂起的线程
                }
                if (!flag)
                {
                    MessageBox.Show("车证卡号没有成功写入卡内扇区，请确认卡放置正确或者是否同一张卡！");
                    textCZKXLH.Text = "";
                    textCZKH.Text = "";
                    textCZKXLH.Enabled = false;
                    return;
                }
                //String manageinsertsql = "insert into BT_CARDMANAGE t(t.FS_CARDNUMBER,t.FS_SEQUENCENO,t.FS_INITDEPART,t.FS_INITUSER,t.FD_INITTIME,t.FS_USEPURPOSE,t.FS_MEMO,t.FS_CARDLEVEL,t.FS_ISVALID) values ('" + czkh + "','" + czkxlh + "','" + departmentcode + "','" + username + "',TO_DATE('" + zzsj + "','YYYY-MM-DD HH24:MI:SS'),'" + yt + "','" + bz + "','" + jb + "','" + sxbz_str + "')";
                String manageinsertsql = @"insert into BT_CARDMANAGE t
                                                            (
                                                                 t.FS_CARDNUMBER,
                                                                 t.FS_SEQUENCENO,
                                                                 t.FS_INITDEPART,
                                                                 t.FS_INITUSER,
                                                                 t.FD_INITTIME,
                                                                 t.FS_USEPURPOSE,
                                                                 t.FS_MEMO,
                                                                 t.FS_CARDLEVEL,
                                                                 t.FS_ISVALID,
                                                                 t.FS_ASSIGNDEPART, 
                                                                 t.FS_ASSIGNUSER 
                                                            ) 
                                                        values 
                                                            (
                                                                '" + czkh + @"',
                                                                '" + czkxlh + @"',
                                                                '" + departmentcode + @"',
                                                                '" + username + @"',
                                                                TO_DATE('" + zzsj + @"','YYYY-MM-DD HH24:MI:SS'),
                                                                '" + yt + @"',
                                                                '" + bz + @"',
                                                                '" + jb + @"',
                                                                '" + sxbz_str + @"',
                                                                '" + fpdw + @"',
                                                                '" + fpr + @"'
                                                            )";

                CoreClientParam reccp = new CoreClientParam();
                reccp.ServerName = "ygjzjl.carcard.CarCard";
                reccp.MethodName = "insertByClientSql";
                reccp.ServerParams = new object[] { manageinsertsql };
                this.ExecuteNonQuery(reccp, CoreInvokeType.Internal);
                if (reccp.ReturnCode == 0) //插入成功
                {
                    MessageBox.Show("车证卡注册成功！");
                    DataRow newczkrow = this.dataSet1.Tables["车证卡基础表"].NewRow();
                    newczkrow["FS_CARDNUMBER"] = czkh;
                    newczkrow["FS_SEQUENCENO"] = czkxlh;
                    newczkrow["FS_INITDEPART"] = departmentcode;
                    newczkrow["FS_INITUSER"] = username;
                    newczkrow["FD_INITTIME"] = zzsj.ToString();
                    newczkrow["FS_USEPURPOSE"] = yt;
                    newczkrow["FS_MEMO"] = bz;
                    newczkrow["FS_CARDLEVEL"] = jb;
                    newczkrow["FS_CARDLEVELNAME"] = jbname;
                    newczkrow["FS_ISVALID"] = sxbz_str;
                    newczkrow["FS_ISVALIDNAME"] = "注册";

                    //Ksh增加对分配人和分配单位字段的刷新
                    newczkrow["FS_ASSIGNDEPART"] = fpdw;
                    newczkrow["FS_ASSIGNUSER"] = fpr;

                    this.dataSet1.Tables["车证卡基础表"].Rows.Add(newczkrow);
                    dataSet1.AcceptChanges();


                    //以上为注册车证卡确认时更新相关信息代码

                    //车证卡操作表(车证卡操作日志)
                    String operationNO = Guid.NewGuid().ToString();
                    String insertsql = "insert into  DT_CARDOPERATION(FS_OPERATENO,FS_SEQUENCENO,FS_CARDNO,FS_APPLYDEPART,FS_APPLYUSER,FS_OPERATIONTYPE,FS_OPERATEDEPART,FS_OPERATOR,FD_OPERATIONTIME) values ('" + operationNO + "','" + czkxlh + "','" + czkh + "','','','注册','" + departmentcode + "','" + username + "',TO_DATE('" + zzsj + "','YYYY-MM-DD HH24:MI:SS'))";
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.carcard.CarCard";
                    ccp.MethodName = "insertByClientSql";
                    ccp.ServerParams = new object[] { insertsql };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    //车证卡操作表
                }

                textCZKXLH.Enabled = false;//注册完后，设置为不可编辑
            }
        }

        // ultragrid过滤设置步骤：1,basic settings/feature picker/filtering/filter UI type/filter row,2,band and column settings/band[]/columns/fiteroperatordefaultvalue/fiteroperatordropdownitems
        private void ultraToolbarsManager2_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {

            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        Query();
                        break;
                    }

                case "Delete":
                    {
                        if (rowno < 0)
                        {
                            MessageBox.Show("请双击选择要删除的车证卡信息！");
                            return;
                        }
                        else
                        {
                            DateTime zzsj = System.DateTime.Now;
                            String czkh = textCZKH.Text; //车证卡芯片号
                            String czkxlh = textCZKXLH.Text;//车证卡编号
                            String fpr = textFPR.Text;      //分配人(分配给谁用)
                            String fpdw = comboFPDW.Text;   //分配单位(对卸货身份卡而言,就是卸货点所属单位)
                            //String yt = textYT.Text;
                            String jb = ""; //车证卡级别(卸货卡，身份卡，临时卡)
                            String strXb = comboSex.Text.Trim();
                            String strGwei = comboGwei.Text.Trim();//岗位(对卸货身份卡而言，就是权限 卸货员/管理员)
                            String strBmen = comboBmen.Text.Trim();
                            String strSfzh = textSFZH.Text.Trim();//身份证号
                            String strLxdh = textLXDH.Text.Trim();//联系电话  


                            if (DialogResult.Yes == MessageBox.Show("是否确认要删除当前数据?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                string strDelete = "DELETE BT_CARDMANAGE T WHERE T.FS_CARDNUMBER='" + this.ultraGrid3.Rows[rowno].Cells["FS_CARDNUMBER"].Text + "'";
                                CoreClientParam cardqj = new CoreClientParam();
                                cardqj.ServerName = "ygjzjl.carcard.CarCard";
                                cardqj.MethodName = "queryByClientSql";
                                cardqj.ServerParams = new object[] { strDelete };

                                this.ExecuteNonQuery(cardqj, CoreInvokeType.Internal);

                                if (cardqj.ReturnCode == 0)
                                {
                                    MessageBox.Show("删除成功！");
                                    //车证卡操作表(车证卡操作日志)


                                    String operationNO = Guid.NewGuid().ToString();
                                    String insertsql = "insert into  DT_CARDOPERATION(FS_OPERATENO,FS_SEQUENCENO,FS_CARDNO,FS_APPLYDEPART,FS_APPLYUSER,FS_OPERATIONTYPE,FS_OPERATEDEPART,FS_OPERATOR,FD_OPERATIONTIME) values ('" + operationNO + "','" + czkxlh + "','" + czkh + "','','','删除','" + departmentcode + "','" + username + "',TO_DATE('" + zzsj + "','YYYY-MM-DD HH24:MI:SS'))";
                                    CoreClientParam ccp = new CoreClientParam();
                                    ccp.ServerName = "ygjzjl.carcard.CarCard";
                                    ccp.MethodName = "insertByClientSql";
                                    ccp.ServerParams = new object[] { insertsql };
                                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                                  
                                }
                            }
                        }
                       
                        rowno = -1;
                        Query();
                        break;
                    }

            }
        }

        /// <summary>
        /// 窗体加载事件，当卡变化时触发相应的读卡动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarCard_Load(object sender, EventArgs e)
        {
            RefreshComBo();
            _icCard.Init(comboSerialNO.Text + ",115200");
            _icCard.CardChanged += new CardChangedEventHandlerRegist(OnCardChanged);
            _icCard.Open();

            //checkBox1.Checked = false;

        }
        //处理读卡事件
        public void OnCardChanged(object sender, CardEventArgsRegist e)
        {
            try
            {
                CardData cardData = (CardData)e.Value;
                Invoke(new GetCardText(HandleCardChange), new object[] { cardData.ID,cardData.CardNo,cardData.CardType });
            }
            catch (Exception ex)
            {

            }
        }
        delegate void GetCardText(string id,string cardNo,string cardType);
        private void HandleCardChange(string id, string cardNo, string cardType)
        {
            if (checkBox1.Checked && !string.IsNullOrEmpty(id.Trim()))
            {
                // MessageBox.Show(data.ToString());
                textCZKH.Text = id;//卡芯片序列号
                textCZKXLH.Text = cardNo;//卡号  
                  
                try
                {
                    //----------------------------------- ksh 在刷卡时带出车证卡所属级别(0身份卡；1车证卡；2临时卡)---------------
                    comboJB.SelectedValue = !string.IsNullOrEmpty(cardType) ? cardType : null;

                    String sql_byCardNumber = @"Select * From BT_CARDMANAGE t Where t.fs_cardnumber ='" + id + "'";
                    DataTable dt_byCardNumber = new DataTable();

                    dt_byCardNumber.Clear();
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.carcard.CarCard";
                    ccp.MethodName = "queryByClientSql";
                    ccp.ServerParams = new object[] { sql_byCardNumber };
                    ccp.SourceDataTable = dt_byCardNumber;
                    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                    if (dt_byCardNumber.Rows.Count > 0)
                    {    
                        //绑定相关控件
                        this.textFPR.Text = dt_byCardNumber.Rows[0]["FS_ASSIGNUSER"].ToString().Trim();//绑定分配人
                        this.comboFPDW.Text = dt_byCardNumber.Rows[0]["FS_ASSIGNDEPART"].ToString().Trim();//绑定分配单位 
                    }
                    else 
                    {
                        //清空相关控件
                        this.textFPR.Text = "";
                        this.comboFPDW.Text = "";  
                    }

                }
                catch (Exception ex_byCardNumber)
                {
                    //MessageBox.Show(ex_byCardNumber.ToString());
                    //return;
                }
                //--------------------------------------------------------------------------------------------------------

                
            }

            textCZKXLH.Enabled = true;
            textCZKXLH.Focus();
        }

        /// <summary>
        /// 在页面加载时绑定数据并刷新ComboBox等控件
        /// </summary>
        private void RefreshComBo()
        {
            //String selectsql = "select distinct t.FS_ASSIGNDEPART from BT_CARDMANAGE t where t.FS_INITDEPART='" + departmentcode + "'";
            String selectsql = "select distinct t.FS_ASSIGNDEPART from BT_CARDMANAGE t ";
            DataTable testdatatable1 = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.carcard.CarCard";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { selectsql };
            ccp.SourceDataTable = testdatatable1;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (testdatatable1.Rows.Count > 0)
            {
                //DataRow newrow = testdatatable1.NewRow();
                //newrow["FS_ASSIGNDEPART"] = "";
                //testdatatable1.Rows.InsertAt(newrow, 0);//加入一个空值

                comboFPDW.DataSource = testdatatable1;//分配单位
                comboFPDW.DisplayMember = "FS_ASSIGNDEPART";
            }

            //selectsql = "select distinct t.FS_APARTMENT from BT_CARDMANAGE t where t.FS_INITDEPART='" + departmentcode + "'";
            selectsql = "select distinct t.FS_APARTMENT from BT_CARDMANAGE t ";
            DataTable testdatatable2 = new DataTable();
            ccp.ServerParams = new object[] { selectsql };
            ccp.SourceDataTable = testdatatable2;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (testdatatable2.Rows.Count > 0)
            {
                //DataRow newrow = testdatatable2.NewRow();
                //newrow["FS_APARTMENT"] = "";
                //testdatatable2.Rows.InsertAt(newrow, 0);//加入一个空值

                comboBmen.DataSource = testdatatable2; //部门
                comboBmen.DisplayMember = "FS_APARTMENT";
            }

            //selectsql = "select distinct t.FS_ROLE from BT_CARDMANAGE t where t.FS_INITDEPART='" + departmentcode + "'";
            selectsql = "select distinct t.FS_ROLE from BT_CARDMANAGE t ";
            DataTable testdatatable3 = new DataTable();
            ccp.ServerParams = new object[] { selectsql };
            ccp.SourceDataTable = testdatatable3;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (testdatatable3.Rows.Count > 0)
            {
                //DataRow newrow = testdatatable3.NewRow();
                //newrow["FS_ROLE"] = "";
                //testdatatable3.Rows.InsertAt(newrow, 0);//加入一个空值

                comboGwei.DataSource = testdatatable3;//岗位
                comboGwei.DisplayMember = "FS_ROLE";
            }

            String cardlevelselectsql = "select t.FS_FLAG, t.FS_NAME from BT_FLAGCORRESPONDING t where t.FS_TYPE='CARDLEVEL'";
            DataTable cardleveldatatable = new DataTable();
            ccp.ServerParams = new object[] { cardlevelselectsql };
            ccp.SourceDataTable = cardleveldatatable;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (cardleveldatatable.Rows.Count > 0)
            {
                comboJB.DataSource = cardleveldatatable;//车证卡级别(标记对应表)
                comboJB.ValueMember = "FS_FLAG";
                comboJB.DisplayMember = "FS_NAME";
            }

        }

        /// <summary>
        /// 设置窗体控件的绑定数据
        /// </summary>
        private void setTextboxvalue()
        {
            textCZKH.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_CARDNUMBER").ToString();
            textCZKXLH.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_SEQUENCENO").ToString();
            textFPR.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_ASSIGNUSER").ToString();
            comboFPDW.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_ASSIGNDEPART").ToString();
            //textYT.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_USEPURPOSE").ToString();
            comboJB.SelectedValue = ultraGrid3.Rows[rowno].GetCellValue("FS_CARDLEVEL").ToString();
            textBZ.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_MEMO").ToString();

            textSFZH.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_IDENTITYNO").ToString();
            textLXDH.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_CONTACTPHONE").ToString();
            comboSex.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_SEX").ToString();
            comboGwei.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_ROLE").ToString();
            comboBmen.Text = ultraGrid3.Rows[rowno].GetCellValue("FS_APARTMENT").ToString();
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


        /// <summary>
        /// 检查窗体输入的有效性验证
        /// </summary>
        /// <returns>bool,通过验证返回true,否则返回false</returns>
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


            if (textFPR.Text.Trim().Length > 10)//中文一字符长度为2
            {
                MessageBox.Show("分配人字符数不能超过10！");
                textFPR.Focus();
                return false;
            }


            if (comboFPDW.Text.Trim().Length > 25)//中文一字符长度为2
            {
                MessageBox.Show("分配单位字符数不能超过25！");
                comboFPDW.Focus();
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

        private void CarCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _icCard.Close();
                _icCard = null;

            }
            catch (Exception ex1)
            {

            }

        }

        private void comboJB_SelectedValueChanged(object sender, EventArgs e)
        {
            String strJB = comboJB.SelectedValue.ToString();
            if (strJB.Equals("0"))
            {
                setComponentStatus(true);
            }
            else
            {
                setComponentStatus(false);
            }
        }

        private void setComponentStatus(bool isVisible)
        {
            label6.Visible = isVisible;
            comboSex.Visible = isVisible;
            label10.Visible = isVisible;
            comboGwei.Visible = isVisible;
            label11.Visible = isVisible;
            comboBmen.Visible = isVisible;
            label7.Visible = isVisible;
            textSFZH.Visible = isVisible;
            label9.Visible = isVisible;
            textLXDH.Visible = isVisible;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
        }

        /// <summary>
        /// ComboBox串口数据项发生变化时的事件（ComboBox初始状态时数据为空，当绑定数据时引发SelectedIndexChanged事件）
        /// </summary>
        /// <param name="sender">事件的发送者</param>
        /// <param name="e">事件</param>
        private void comboSerialNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_icCard != null)
                {
                    _icCard.Close();
                    _icCard.Init(comboSerialNO.Text + ",115200");//波特率为115200
                    _icCard.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开读卡器失败，请检查设备是否连接正常并重新启动！");
            }
        }

        private void textCZKXLH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                RegistCard();
            }
        }

        private void Query()
        {

            //以下为，只能查询和分配该登陆员单位所分配的车证卡区间
            textCZKXLH.Enabled = false;
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
            if (czkqjtable.Rows.Count > 0)
            {
                cardnobegin = czkqjtable.Rows[0]["FS_CARNOBEGIN"].ToString().Trim();
                cardnoend = czkqjtable.Rows[0]["FS_CARNOEND"].ToString().Trim();
            }
            try
            {
                if (cardnobegin != null)
                {
                    //String aa = cardnobegin.Substring(2, 5);
                    //cardnobegin_int = int.Parse(aa);
                    cardnobegin_int = int.Parse(cardnobegin);
                }
                if (cardnoend != null)
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
                MessageBox.Show("请与管理员联系，正确配置单位拥有的车证卡区间！");
                return;
            }
            //以上为，只能查询和分配该登陆员单位所分配的车证卡区间


            //以下为查询相关信息代码并显示
            DateTime begintime = dateTimeBegin.Value;
            DateTime endtime = dateTimeEnd.Value;

            String xlh_str = txtCardXLH.Text.Trim();
            if (begintime > endtime)
            {
                MessageBox.Show("截止日期不能小于开始日期！");
                return;

            }
            String begintime_str = begintime.ToString("yyyy-MM-dd 00:00:00");
            String endtime_str = endtime.ToString("yyyy-MM-dd 23:59:59");
            String selectsql_ch = "select t.FS_CARDNUMBER,t.FS_SEQUENCENO,t.FS_INITDEPART,t.FS_INITUSER,to_char(t.FD_INITTIME,'YYYY-MM-DD HH24:MI:SS') as FD_INITTIME,t.FS_ASSIGNDEPART,t.FS_ASSIGNUSER,t.FS_USERCODE,t.FS_USEDEPART,t.FS_ISBINDTOCAR,t.FS_BINDCARNO,t.FS_USEPURPOSE,t.FS_MEMO,t.FS_CARDLEVEL,t.FS_ISVALID,t.FS_SECONDDEPART,t.FS_SEX,t.FS_IDENTITYNO,t.FS_CONTACTPHONE,t.FS_APARTMENT,t.FS_ROLE,a.fs_name as FS_CARDLEVELNAME,b.fs_name as FS_ISVALIDNAME from BT_CARDMANAGE t left join BT_FLAGCORRESPONDING a on a.FS_FLAG=t.FS_CARDLEVEL and a.fs_type='CARDLEVEL' left join BT_FLAGCORRESPONDING b on b.FS_FLAG=t.FS_ISVALID and b.fs_type='CARDVALID' where t.FD_INITTIME>=TO_DATE('" + begintime_str + "','YYYY-MM-DD HH24:MI:SS') and t.FD_INITTIME<=TO_DATE('" + endtime_str + "','YYYY-MM-DD HH24:MI:SS') and (To_number(SUBSTR(t.FS_SEQUENCENO,1,5)) between " + cardnobegin_int + " and " + cardnoend_int + ")";

            if (xlh_str != null && !xlh_str.Equals(""))
            {
                selectsql_ch += " and t.FS_SEQUENCENO='" + xlh_str + "'";

            }
            //String selectsql_ch = "select t.FS_CARDNUMBER,t.FS_SEQUENCENO,t.FS_INITDEPART,t.FS_INITUSER,to_char(t.FD_INITTIME,'YYYY-MM-DD HH24:MI:SS') as FD_INITTIME,t.FS_ASSIGNDEPART,t.FS_ASSIGNUSER,t.FS_USERCODE,t.FS_USEDEPART,t.FS_ISBINDTOCAR,t.FS_BINDCARNO,t.FS_USEPURPOSE,t.FS_MEMO,t.FS_CARDLEVEL,t.FS_ISVALID from BT_CARDMANAGE t where t.FS_SEQUENCENO='" + xlh_str + "' and (To_number(SUBSTR(t.FS_SEQUENCENO,3,5)) between " + cardnobegin_int + " and " + cardnoend_int + ")";
            CoreClientParam ccpquery = new CoreClientParam();
            ccpquery.ServerName = "ygjzjl.carcard.CarCard";
            ccpquery.MethodName = "queryByClientSql";
            ccpquery.ServerParams = new object[] { selectsql_ch };
            ccpquery.SourceDataTable = this.dataSet1.Tables["车证卡基础表"];
            this.dataSet1.Tables["车证卡基础表"].Clear();
            this.ExecuteQueryToDataTable(ccpquery, CoreInvokeType.Internal);

            //this.setTextboxvalue();
            //以上为查询相关信息代码并显示
            rowno = -1;
        }
    }
}
