using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using System.Windows.Forms;
using Infragistics.Win.UltraWinEditors;
using System.Resources;
using System.IO;

namespace YGJZJL.BaseDataManage
{
    public partial class BaseManger : FrmBase
    {
        private string[] DeleteNO = new string[7] { "", "", "", "", "" ,"",""};//定义删除记录的编号存放数组。
        private string QueryContion = "";                                //保存查询的数据编号
        private UltraCheckEditor[] UltraCkED = new UltraCheckEditor[17]; //定义查询的条件存放信息。
        private TextBox[] TextboxFrom = new TextBox[7];                  //定义来源控件数组
        private int RowsIndex = 0;                                       //保存SAP更新物料开始行号
        private byte[] VoiceFile;
        private string VoiceNa = "";                                     //保存要修改的声音文件名
        private string VoiceTy = "";                                      //保存要修改的声音文件所对应的磅房编号

        #region 获取用户信息
        private void GetUserInfor(CheckedListBox checkbox)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "GetRoleData";
            DataTable dtBF = new DataTable();
            ccp.SourceDataTable = dtBF;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dtBF.Rows.Count > 0)
            {
                checkbox.DataSource = dtBF;
                checkbox.DisplayMember = "ROLENAME";
                checkbox.ValueMember = "ROLEID";

            }
        }
        #endregion

        #region 构造函数
        public BaseManger()
        {
            InitializeComponent();
            TextboxFrom[0] = this.txtWLFROM;
            TextboxFrom[1] = this.txtFHFROM;
            TextboxFrom[2] = this.txtGYFROM;
            TextboxFrom[3] = this.txtSHFROM;
            TextboxFrom[4] = this.txtCYFROM;
            TextboxFrom[5] = null;
            TextboxFrom[6] = this.txtVoiceNa;
            UltraCkED[0] = this.ultraCheckEditor0;
            UltraCkED[1] = this.ultraCheckEditor1;
            UltraCkED[2] = this.ultraCheckEditor2;
            UltraCkED[3] = this.ultraCheckEditor3;
            UltraCkED[4] = this.ultraCheckEditor4;
            UltraCkED[5] = this.ultraCheckEditor5;
            UltraCkED[6] = this.ultraCheckEditor6;
            UltraCkED[7] = this.ultraCheckEditor7;
            UltraCkED[8] = this.ultraCheckEditor8;
            UltraCkED[9] = this.ultraCheckEditor9;
            UltraCkED[10] = this.ultraCheckEditor10;
            UltraCkED[11] = this.ultraCheckEditor11;
            UltraCkED[12] = this.ultraCheckEditor12;
            UltraCkED[13] = this.ultraCheckEditor13;
            UltraCkED[14] = this.ultraCheckEditor14;
            UltraCkED[15] = this.ultraCheckEditor15;
            UltraCkED[16] = this.ultraCheckEditor16;
            for (int i = 0; i < UltraCkED.Length; i++)
            {
                UltraCkED[i].AfterCheckStateChanged += new Infragistics.Win.CheckEditor.AfterCheckStateChangedHandler(UltraCkED_Click);
            }
        }
        #endregion
       
        #region 等待窗体
        private void BaseManger_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (Constant.WaitingForm == null)
            {
                Constant.WaitingForm = new WaitingForm();
            }

            Constant.WaitingForm.ShowToUser = true;
            Constant.WaitingForm.Show();
            Constant.WaitingForm.Update();

            try
            {
                if (Constant.RunPath == "")
                {
                    Constant.RunPath = System.Environment.CurrentDirectory;
                }
            }
            catch (System.Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            //Constant.SetViewStyle(this);

            //ControlerInit();
            //QueryAndBindJLGrid();
            QueryBFData("");
            this.Cursor = Cursors.Default;
            Constant.WaitingForm.ShowToUser = false;
            Constant.WaitingForm.Close();

        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// Toolbar按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ToolbarKey"></param>
        public override void ToolBar_Click(object sender, string ToolbarKey)
        {
            base.ToolBar_Click(sender, ToolbarKey);

            switch (ToolbarKey)
            {
                case "Query":
                    if (this.ultabTotal.ActiveTab.VisibleIndex == 1)
                    {
                        QueryTableData(
                            QueryContion,
                            this.ultabBASETABLE.Tabs[this.ultabBASETABLE.ActiveTab.VisibleIndex].Key.ToString().Trim(),
                            this.ultabBASETABLE.ActiveTab.VisibleIndex);
                    }
                    else
                    {
                        
                    }
                    break;
                case "Add":
                    if (this.ultabTotal.ActiveTab.VisibleIndex == 1)
                    {
                        AddTable();
                    }
                    else
                    {
                        InsertBaseInformManger();
                    }
                    break;
                case "Delete":
                    if (this.ultabTotal.ActiveTab.VisibleIndex == 1)
                    {
                        DeleteTable();
                    }
                    else
                    {
                        DeleteBaseInformManger();
                    }
                    break;
                case "Update":
                    if (this.ultabTotal.ActiveTab.VisibleIndex == 1)
                    {
                        UpdateTable();
                    }
                    else
                    {
                        UpdateBaseInformManger();
                    }
                    break;
            }
        }
        #endregion

        #region 磅房信息管理--选择事件
        /// <summary>
        /// UltraTab控件选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ultb_ActiveTabChanged(object sender, Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs e)
        {
            this.lb1.Text = this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[1].Caption;
            this.lb2.Text = this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[2].Caption;
        }
        /// <summary>
        /// 计量点选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ToolbarKey"></param>
        private void cbJLD_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cbJLD.Items.Count > 0 && this.cbJLD.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                QueryPointItemAndDiplay(this.cbJLD.SelectedValue.ToString().Trim());
                for (int i = 0; i < this.ultb.Tabs.Count; i++)
                {
                    if (this.ultb.Tabs[i].Visible == true)
                    {             
                        QueryItemData(this.cbJLD.SelectedValue.ToString(), this.dataSet1.Tables[i].Namespace,
                            this.dataSet1.Tables[i].Columns[0].ColumnName,
                            this.dataSet1.Tables[i].Columns[1].ColumnName,
                            this.dataSet1.Tables[i].Columns[2].ColumnName, i);
                    }
                }
            }
        }
        /// <summary>
        /// 磅房查询子项目及状态显示
        /// </summary>
        private void QueryPointItemAndDiplay(string PointNo)
        {
            QueryItemData(PointNo);
            if (this.dataSet2.Tables["计量点项目"].Rows.Count > 0)
            {
                for (int i = 0; i < this.dataSet2.Tables["计量点项目"].Rows.Count; i++)
                {
                    if (this.dataSet2.Tables["计量点项目"].Rows[i]["FS_POINTITEMSTATE"].ToString() == "1")
                    {
                        this.ultb.Tabs[this.dataSet2.Tables["计量点项目"].Rows[i]["FS_POINTITEM"].ToString()].Visible = true;
                        UltraCkED[this.ultb.Tabs[this.dataSet2.Tables["计量点项目"].Rows[i]["FS_POINTITEM"].ToString()].Index].Checked = true;
                    }
                    else
                    {
                        this.ultb.Tabs[this.dataSet2.Tables["计量点项目"].Rows[i]["FS_POINTITEM"].ToString()].Visible = false;
                        UltraCkED[this.ultb.Tabs[this.dataSet2.Tables["计量点项目"].Rows[i]["FS_POINTITEM"].ToString()].Index].Checked = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < UltraCkED.Length; i++)
                {
                    UltraCkED[i].Checked = false;
                    this.ultb.Tabs[i].Visible = false;
                }
            }
        }
        /// <summary>
        /// 磅房子项目状态改变事件
        /// </summary>
        private void UltraCkED_Click(Object sender, System.EventArgs e)
        {

            if (((UltraCheckEditor)sender).Checked)
            {
                this.ultb.Tabs[((UltraCheckEditor)sender).TabIndex].Visible = true;
                if (this.cbJLD.SelectedValue != null)
                {
                    BaseInformManger(this.cbJLD.SelectedValue.ToString(), "BT_POINTITEM", this.ultb.Tabs[((UltraCheckEditor)sender).TabIndex].Text, 
                        this.ultb.Tabs[((UltraCheckEditor)sender).TabIndex].Text, "1",
                        "FS_POINTCODE", "FS_POINTITEM", "FS_POINTITEMSTATE", "I");
                    QueryItemData(this.cbJLD.SelectedValue.ToString(), this.dataSet1.Tables[((UltraCheckEditor)sender).TabIndex].Namespace,
                           this.dataSet1.Tables[((UltraCheckEditor)sender).TabIndex].Columns[0].ColumnName,
                           this.dataSet1.Tables[((UltraCheckEditor)sender).TabIndex].Columns[1].ColumnName,
                           this.dataSet1.Tables[((UltraCheckEditor)sender).TabIndex].Columns[2].ColumnName, ((UltraCheckEditor)sender).TabIndex);
                }
            }
            else
            {
                this.ultb.Tabs[((UltraCheckEditor)sender).TabIndex].Visible = false;
                if (this.cbJLD.SelectedValue != null)
                {
                    BaseInformManger(this.cbJLD.SelectedValue.ToString(), "BT_POINTITEM", this.ultb.Tabs[((UltraCheckEditor)sender).TabIndex].Text,
                        this.ultb.Tabs[((UltraCheckEditor)sender).TabIndex].Text, "0",
                        "FS_POINTCODE", "FS_POINTITEM", "FS_POINTITEMSTATE", "I");
                }
            }
        }

        #endregion        

        #region 磅房信息管理--查询信息
        /// <summary>
        /// 装载所有磅房信息
        /// </summary>
        /// <param name="dtBF"></param>
        /// <param name="PointType"></param>
        public void QueryBFData(string PointType)
        {            
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryBFData";
            ccp.ServerParams = new object[] { PointType };
            DataTable dtBF = new DataTable();
            ccp.SourceDataTable = dtBF;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dtBF.Rows.Count > 0)
            {
                this.cbJLD.DataSource = dtBF;
                this.cbJLD.DisplayMember = "FS_POINTNAME";
                this.cbJLD.ValueMember = "FS_POINTCODE";
                this.cbJLD.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cbJLD.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }
        /// <summary>
        /// 装载磅房对应的子项目
        /// </summary>
        /// <param name="PointType"></param>
        private void QueryItemData(string PointType)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "BaseInformQuery";
            ccp.ServerParams = new object[] { PointType, "BT_POINTITEM", "FS_POINTCODE", "FS_POINTITEM", "FS_POINTITEMSTATE", "0" };
            ccp.SourceDataTable = this.dataSet2.Tables["计量点项目"];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        }
        /// <summary>
        /// 装载磅房对应的项目具体信息
        /// </summary>
        /// <param name="PointType"></param>
        private void QueryItemData(string PointType,string Fs_Table,string Fs_pointcode,string Fs_pointname,string Fs_times,int tableIndex)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "BaseInformQuery";
            ccp.ServerParams = new object[] { PointType, Fs_Table, Fs_pointcode, Fs_pointname, Fs_times, tableIndex.ToString()};
            ccp.SourceDataTable = this.dataSet1.Tables[tableIndex];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        }

        #endregion      

        #region 磅房信息管理--Insert、Delete、Update
        /// <summary>
        /// 磅房信息表的插入、修改及删除公用方法
        /// </summary>
        /// <param name="PointType"></param>
        private void BaseInformManger(string PointNo,string Basetable,string P_name,string P_nameUpate,string P_fntiems,
            string FS_PonitNo,string FS_NAME,string FS_TIMES,string Method)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "BaseInformManger";
            ccp.ServerParams = new object[] { PointNo, Basetable, P_name,P_nameUpate, P_fntiems, FS_PonitNo, FS_NAME, FS_TIMES, Method };
            this.ExecuteQuery(ccp, CoreInvokeType.Internal);

        }
        #endregion

        #region 磅房信息管理--插入数据
        /// <summary>
        /// 磅房信息表的记录插入
        /// </summary>
        /// <param name="PointType"></param>
        private void InsertBaseInformManger()
        {
            if (this.cbJLD.SelectedValue != null)
            {
                if (this.txt1.Text != "")
                {
                    BaseInformManger(this.cbJLD.SelectedValue.ToString(), this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Namespace, this.txt1.Text, this.txt1.Text, this.txt2.Text,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[0].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[1].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[2].ColumnName, "I");
                    QueryItemData(this.cbJLD.SelectedValue.ToString(), this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Namespace,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[0].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[1].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[2].ColumnName, this.ultb.ActiveTab.VisibleIndex);
                    this.txt1.Text = "";
                    this.txt2.Text = "";
                }
                else
                {
                    MessageBox.Show(this.lb1.Text+"不能为空！");
                }
            }

        }
        #endregion

        #region 磅房信息管理--修改数据
        /// <summary>
        /// 磅房信息表记录修改
        /// </summary>
        /// <param name="PointType"></param>
        private void UpdateBaseInformManger()
        {
            if (this.cbJLD.SelectedValue != null)
            {
                if (this.txt1.Text != "")
                {
                    BaseInformManger(this.cbJLD.SelectedValue.ToString(), this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Namespace, this.txt3.Text,this.txt1.Text, this.txt2.Text,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[0].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[1].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[2].ColumnName, "I");
                    QueryItemData(this.cbJLD.SelectedValue.ToString(), this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Namespace,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[0].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[1].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[2].ColumnName, this.ultb.ActiveTab.VisibleIndex);
                    this.txt1.Text = "";
                    this.txt2.Text = "";
                }
                else
                {
                    MessageBox.Show(this.lb1.Text + "不能为空！");
                }
            }
        }

        #endregion

        #region 磅房信息管理--删除数据
        /// <summary>
        /// 磅房信息表记录删除
        /// </summary>
        /// <param name="PointType"></param>
        private void DeleteBaseInformManger()
        {
            if (this.cbJLD.SelectedValue != null)
            {
                if (this.txt1.Text != "")
                {
                    BaseInformManger(this.cbJLD.SelectedValue.ToString(), this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Namespace, this.txt1.Text, this.txt1.Text, this.txt2.Text,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[0].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[1].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[2].ColumnName, "D");
                    QueryItemData(this.cbJLD.SelectedValue.ToString(), this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Namespace,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[0].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[1].ColumnName,
                        this.dataSet1.Tables[this.ultb.ActiveTab.VisibleIndex].Columns[2].ColumnName, this.ultb.ActiveTab.VisibleIndex);
                    this.txt1.Text = "";
                    this.txt2.Text = "";
                }
                else
                {
                    MessageBox.Show(this.lb1.Text + "不能为空！");
                }
            }

        }

        #endregion

        #region 磅房信息管理--单元格双击事件
        private void ulgrdCarNo_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdCarNo.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdCarNo.Rows[e.Row.Index].Cells[this.dataSet1.Tables["车号"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdCarNo.Rows[e.Row.Index].Cells[this.dataSet1.Tables["车号"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdCarNo.Rows[e.Row.Index].Cells[this.dataSet1.Tables["车号"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdWL_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdWL.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdWL.Rows[e.Row.Index].Cells[this.dataSet1.Tables["物料"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdWL.Rows[e.Row.Index].Cells[this.dataSet1.Tables["物料"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdWL.Rows[e.Row.Index].Cells[this.dataSet1.Tables["物料"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdSender_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdSender.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdSender.Rows[e.Row.Index].Cells[this.dataSet1.Tables["生产单位"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdSender.Rows[e.Row.Index].Cells[this.dataSet1.Tables["生产单位"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdSender.Rows[e.Row.Index].Cells[this.dataSet1.Tables["生产单位"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdTran_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdTran.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdTran.Rows[e.Row.Index].Cells[this.dataSet1.Tables["承运单位"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdTran.Rows[e.Row.Index].Cells[this.dataSet1.Tables["承运单位"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdTran.Rows[e.Row.Index].Cells[this.dataSet1.Tables["承运单位"].Columns[1].ColumnName].Value.ToString();
            } 
        }

        private void ulgrdSupply_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdSupply.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdSupply.Rows[e.Row.Index].Cells[this.dataSet1.Tables["供应单位"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdSupply.Rows[e.Row.Index].Cells[this.dataSet1.Tables["供应单位"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdSupply.Rows[e.Row.Index].Cells[this.dataSet1.Tables["供应单位"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdFaceLevel_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdFaceLevel.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdFaceLevel.Rows[e.Row.Index].Cells[this.dataSet1.Tables["表面级别"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdFaceLevel.Rows[e.Row.Index].Cells[this.dataSet1.Tables["表面级别"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdFaceLevel.Rows[e.Row.Index].Cells[this.dataSet1.Tables["表面级别"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdFaceState_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdFaceState.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdFaceState.Rows[e.Row.Index].Cells[this.dataSet1.Tables["表面状态"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdFaceState.Rows[e.Row.Index].Cells[this.dataSet1.Tables["表面状态"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdFaceState.Rows[e.Row.Index].Cells[this.dataSet1.Tables["表面状态"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdDemo_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdDemo.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdDemo.Rows[e.Row.Index].Cells[this.dataSet1.Tables["备注"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdDemo.Rows[e.Row.Index].Cells[this.dataSet1.Tables["备注"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdDemo.Rows[e.Row.Index].Cells[this.dataSet1.Tables["备注"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdStander_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdStander.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdStander.Rows[e.Row.Index].Cells[this.dataSet1.Tables["产品标准"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdStander.Rows[e.Row.Index].Cells[this.dataSet1.Tables["产品标准"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdStander.Rows[e.Row.Index].Cells[this.dataSet1.Tables["产品标准"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdSpec_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdSpec.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdSpec.Rows[e.Row.Index].Cells[this.dataSet1.Tables["规格"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdSpec.Rows[e.Row.Index].Cells[this.dataSet1.Tables["规格"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdSpec.Rows[e.Row.Index].Cells[this.dataSet1.Tables["规格"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdStelTye_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdStelTye.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdStelTye.Rows[e.Row.Index].Cells[this.dataSet1.Tables["钢种"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdStelTye.Rows[e.Row.Index].Cells[this.dataSet1.Tables["钢种"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdStelTye.Rows[e.Row.Index].Cells[this.dataSet1.Tables["钢种"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdContrat_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdContrat.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdContrat.Rows[e.Row.Index].Cells[this.dataSet1.Tables["合同"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdContrat.Rows[e.Row.Index].Cells[this.dataSet1.Tables["合同"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdContrat.Rows[e.Row.Index].Cells[this.dataSet1.Tables["合同"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdCheck_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdCheck.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdCheck.Rows[e.Row.Index].Cells[this.dataSet1.Tables["校验员"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdCheck.Rows[e.Row.Index].Cells[this.dataSet1.Tables["校验员"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdCheck.Rows[e.Row.Index].Cells[this.dataSet1.Tables["校验员"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdColor_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdColor.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdColor.Rows[e.Row.Index].Cells[this.dataSet1.Tables["颜色"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdColor.Rows[e.Row.Index].Cells[this.dataSet1.Tables["颜色"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdColor.Rows[e.Row.Index].Cells[this.dataSet1.Tables["颜色"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdPrintMet_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdPrintMet.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdPrintMet.Rows[e.Row.Index].Cells[this.dataSet1.Tables["打印物料"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdPrintMet.Rows[e.Row.Index].Cells[this.dataSet1.Tables["打印物料"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdPrintMet.Rows[e.Row.Index].Cells[this.dataSet1.Tables["打印物料"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ulgrdRecie_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdRecie.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdRecie.Rows[e.Row.Index].Cells[this.dataSet1.Tables["收货单位"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdRecie.Rows[e.Row.Index].Cells[this.dataSet1.Tables["收货单位"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdRecie.Rows[e.Row.Index].Cells[this.dataSet1.Tables["收货单位"].Columns[1].ColumnName].Value.ToString();
            }
        }
        private void ulgrdFlow_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (ulgrdFlow.Rows.Count > 0)
            {
                this.txt1.Text = this.ulgrdFlow.Rows[e.Row.Index].Cells[this.dataSet1.Tables["流向"].Columns[1].ColumnName].Value.ToString();
                this.txt2.Text = this.ulgrdFlow.Rows[e.Row.Index].Cells[this.dataSet1.Tables["流向"].Columns[2].ColumnName].Value.ToString();
                this.txt3.Text = this.ulgrdFlow.Rows[e.Row.Index].Cells[this.dataSet1.Tables["流向"].Columns[1].ColumnName].Value.ToString();
            }
        }

        private void ultb_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            this.txt1.Text = "";
            this.txt2.Text = "";
            this.txt3.Text = "";
        }

        #endregion

        #region 基础表管理--查询数据
        /// <summary>
        /// 基础表查询
        /// </summary>
        private void QueryTableData(string ItemNO,string MethodName,int TableIndex)
        {
            if (true)
            {
                this.dataSet2.Tables[TableIndex].Clear();
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
                ccp.MethodName = MethodName;
                if (TableIndex == 6)
                {
                    ccp.ServerParams = new object[] { ItemNO, "" };
                }
                else
                {
                    ccp.ServerParams = new object[] { ItemNO };
                }
                ccp.SourceDataTable = this.dataSet2.Tables[TableIndex];
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            else
            {
                //MessageBox.Show("请输入查询条件！");
            }
        }
        /// <summary>
        /// 磅房类型查询
        /// </summary>
        private void QueryPointTypeToCb(ComboBox cb)
        {
            CoreClientParam cpp = new CoreClientParam();
            DataTable tabale = new DataTable();
            cpp.ServerName = "ygjzjl.basedatamanage.PondTypeBaseInfo";
            cpp.MethodName = "query";
            cpp.ServerParams = new object[] {""};
            cpp.SourceDataTable = tabale;
            this.ExecuteQueryToDataTable(cpp, CoreInvokeType.Internal);
            if (tabale.Rows.Count > 0)
            {
                foreach (DataRow row in tabale.Rows)
                {
                    cb.Items.Add(row["FS_PONDTYPENO"].ToString() +"-"+ row["FS_PONDTYPENAME"].ToString());
                }
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }
        #endregion

        #region 基础表管理--插入数据
        /// <summary>
        /// 物料基础表插入数据
        /// </summary>
        private string InsertWLtable(string WLNAME, string WLTYPE, string WLGROUP, string WLUNIT, string WLGROUPDESCRIBE, string HELPCODE, string FROM,string FACTOR)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "InsertWLData";
            ccp.ServerParams = new object[] {WLNAME, WLTYPE, WLGROUP, WLUNIT,WLGROUPDESCRIBE,HELPCODE,FROM ,FACTOR};
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnObject != null)
            {
                return ccp.ReturnObject.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 发货单位基础表插入数据
        /// </summary>
        private string InsertFHtable(string FHDW, string FHPLANT, string FHCARNOBEGIN, string FHCARNOEND, string HELPCODE, string FROM,string USERROLE)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "InsertFHDWData";
            ccp.ServerParams = new object[] { FHDW, FHPLANT, FHCARNOBEGIN, FHCARNOEND, HELPCODE, FROM, USERROLE };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnObject != null)
            {
                return ccp.ReturnObject.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 收货基础表插入数据
        /// </summary>
        private string InsertSHtable(string SHDW, string SHFACTNO, string HELPCODE, string FROM, string USERROLE)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "InsertSHDWData";
            ccp.ServerParams = new object[] { SHDW, SHFACTNO, HELPCODE, FROM, USERROLE };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnObject != null)
            {
                return ccp.ReturnObject.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 供应商基础表插入数据
        /// </summary>
        private string InsertGYtable(string GYDW,string HELPCODE,string FROM, string USERROLE)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "InsertGYDWData";
            ccp.ServerParams = new object[] { GYDW, HELPCODE, FROM, USERROLE };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnObject != null)
            {
                return ccp.ReturnObject.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 承运单位基础表插入数据
        /// </summary>
        private string InsertCYtable(string CYDW, string HELPCODE, string FROM, string USERROLE)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "InsertCYDWData";
            ccp.ServerParams = new object[] { CYDW, HELPCODE, FROM,USERROLE };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnObject != null)
            {
                return ccp.ReturnObject.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 声音基础表插入数据
        /// </summary>
        private void InsertVoiceData(string VOICENAME, string INSTRTYPE, string MEMO, byte[] VOICEFILE)
        {
            if (VOICEFILE != null)
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
                ccp.MethodName = "InsertVoiceData";
                ccp.IfShowErrMsg = false;                
                ccp.ServerParams = new object[] { VOICENAME, INSTRTYPE, MEMO, VOICEFILE };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                string err = ccp.ReturnInfo;
                if (err.Contains("ORA-00001"))
                    MessageBox.Show("记录重复！");
                if (ccp.ReturnCode == 0)
                {
                    ClearVoiceTableData();
                }
            }
            else
            {
                MessageBox.Show("声音文件不能为空！");
            }
            
        }
        #endregion

        #region 基础表管理--删除数据
        /// <summary>
        /// 公用基础表删除数据
        /// </summary>
        private void Deletetable(string ItemNO,string MethodName)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = MethodName;
            ccp.ServerParams = new object[] { ItemNO };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 声音基础表删除数据
        /// </summary>
        private void DeleteVoictable(string ItemNO, string PointType)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "DeletVoiceData";
            ccp.ServerParams = new object[] { ItemNO,PointType };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                ClearVoiceTableData();
            }
        }
        #endregion

        #region 基础表管理--更新数据
        /// <summary>
        /// 物料基础表更新数据
        /// </summary>
        private void UpdateWLtable(string WLNO,string WLNAME, string WLTYPE, string WLGROUP, string WLUNIT, string WLGROUPDESCRIBE, string HELPCODE, string FROM,string FACTOR)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "UpdateWLData";
            ccp.ServerParams = new object[] { WLNO,WLNAME, WLTYPE, WLGROUP, WLUNIT, WLGROUPDESCRIBE, HELPCODE, FROM ,FACTOR};
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 发货单位基础表更新数据
        /// </summary>
        private void UpdateFHtable(string FHNO, string FHDW, string FHPLANT, string FHCARNOBEGIN, string FHCARNOEND, string HELPCODE, string FROM, string USERROLES)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "UpdateFHDWData";
            ccp.ServerParams = new object[] { FHNO, FHDW, FHPLANT, FHCARNOBEGIN, FHCARNOEND, HELPCODE, FROM, USERROLES };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 收货基础表更新数据
        /// </summary>
        private void UpdateSHtable(string SHNO, string SHDW, string SHFACTNO, string HELPCODE, string FROM, string USERROLES)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "UpdateSHDWData";
            ccp.ServerParams = new object[] { SHNO, SHDW, SHFACTNO, HELPCODE, FROM, USERROLES };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 供应商基础表更新数据
        /// </summary>
        private void UpdateGYtable(string GYNO, string GYDW, string FROM, string HELPCODE, string USERROLES)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "UpdateGYDWData";
            ccp.ServerParams = new object[] { GYNO, GYDW, HELPCODE, FROM, USERROLES };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 承运单位基础表更新数据
        /// </summary>
        private void UpdateCYtable(string CYNO, string CYDW, string HELPCODE, string FROM, string USERROLES)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "UpdateCYDWData";
            ccp.ServerParams = new object[] { CYNO, CYDW, HELPCODE, FROM, USERROLES };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 声音基础表更新数据
        /// </summary>
        private void UpdateVoiceData(string Voicenam,string VoiceTyp,string VOICENAME, string INSTRTYPE, string MEMO, byte[] VOICEFILE)
        {
            if (VOICEFILE != null)
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
                ccp.MethodName = "UpdateVoiceData";
                ccp.ServerParams = new object[] { Voicenam, VoiceTyp, VOICENAME, INSTRTYPE, MEMO, VOICEFILE };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnCode == 0)
                {
                    ClearVoiceTableData();
                }
            }
            else
            {
                MessageBox.Show("声音文件为空，请双击修改的记录或打开声音文件！");
            }
        }
        #endregion

        #region 基础表管理--Insert、Delete、Update
        private void AddTable()
        {
            string result = "";
            switch (this.ultabBASETABLE.ActiveTab.VisibleIndex)
            {
                case 0:
                    if (this.txtWLMC.Text != "")
                    {
                        result=InsertWLtable(this.txtWLMC.Text.Trim(), this.txtWLTYPE.Text.Trim(),
                            this.txtWLZ.Text.Trim(), this.txtWLUNIT.Text.Trim(), this.txtWLZMS.Text.Trim(),
                            this.txtWLPYZJM.Text.Trim(), this.txtWLFROM.Text.Trim(),this.txtFACTOR.Text);
                    }
                    else
                    {
                        MessageBox.Show("物料名称不能为空！");
                    }
                    break;
                case 1:
                    if (this.txtFHCJMS.Text != "" && this.txtFHPYZJM.Text != "" && this.txtFHGCDM.Text!="")
                    {
                        result = InsertFHtable(this.txtFHCJMS.Text.Trim(), this.txtFHGCDM.Text.Trim(), this.txtFHCARNOBEGIN.Text.Trim(), this.txtFHCARNOEND.Text.Trim(),
                            this.txtFHPYZJM.Text.Trim(), this.txtFHFROM.Text.Trim(), UserCKChangeSet(this.CKFHUser));
                    }
                    else
                    {
                        MessageBox.Show("车间描述、拼音助记码或工厂代码不能为空！");
                    }
                    break;
                case 2:
                    if (this.txtGYNAME.Text != ""&&this.txtGYPYZJM.Text!="")
                    {
                        result = InsertGYtable(this.txtGYNAME.Text.Trim(), this.txtGYFROM.Text.Trim(), this.txtGYPYZJM.Text.Trim(), UserCKChangeSet(this.CKGYuser));
                    }
                    else
                    {
                        MessageBox.Show("供应商描述或拼音助记码不能为空！");
                    }
                    break;
                case 3:
                    if (this.txtSHNAME.Text != ""&&this.txtSHPYZJM.Text!="")
                    {
                        result = InsertSHtable(this.txtSHNAME.Text.Trim(), this.txtSHFACTNO.Text.Trim(), this.txtSHPYZJM.Text.Trim(),
                            this.txtSHFROM.Text.Trim(), UserCKChangeSet(this.CKSHuser));
                    }
                    else
                    {
                        MessageBox.Show("收货方描述或拼音助记码不能为空！");
                    }
                    break;
                case 4:
                    if (this.txtCYNAME.Text != ""&&this.txtCYPYZJM.Text!="")
                    {
                        result = InsertCYtable(this.txtCYNAME.Text.Trim(), this.txtCYPYZJM.Text.Trim(), this.txtCYFROM.Text.Trim(), UserCKChangeSet(this.CKCYuser));
                    }
                    else
                    {
                        MessageBox.Show("承运方描述或拼音助记码不能为空！");
                    }
                    break;
                case 6:
                    if (this.txtVoiceNa.Text != ""&&this.cbxPointType.Text!=""&&this.txtVoiceDe.Text!="")
                    {
                        InsertVoiceData(this.txtVoiceNa.Text.Trim(), this.cbxPointType.Text.Trim(), this.txtVoiceDe.Text.Trim(), VoiceFile);
                    }
                    else
                    {
                        MessageBox.Show("修改数据项不能为空！");
                    }
                    break;

            }
            if (result != "")
            {

                if (result.Contains("已存在相同"))
                {
                    MessageBox.Show(result);
                }
                else
                {
                    QueryContion = result;
                }
            }
            QueryTableData(QueryContion, this.ultabBASETABLE.Tabs[this.ultabBASETABLE.ActiveTab.VisibleIndex].Key.ToString().Trim(),
                    this.ultabBASETABLE.ActiveTab.VisibleIndex);
            InitBaseTableItem(this.ultabBASETABLE.ActiveTab.VisibleIndex);
            QueryContion = "";
        }

        private void InitBaseTableItem(int activeTabIndex)
        {
            switch (activeTabIndex)
            {
                case 0:
                    this.txtWLNO.Text = "";
                    break;
                case 1:
                    this.txtFHNO.Text = "";
                    break;
                case 2:
                    this.txtGYNO.Text = "";
                    break;
                case 3:
                    this.txtSHNO.Text = "";
                    break;
                case 4:
                    this.txtCYNO.Text = "";
                    break;
                case 6:
                    break;
            }
        }
        private void DeleteTable()
        {
            MessageBoxButtons buttons =MessageBoxButtons.YesNo;
            DialogResult result;
            string[] DeleteMethod = new string[] { "DeleteWLData", "DeleteFHData", "DeleteGYData", "DeleteSHData", "DeleteCYData", "","DeletVoiceData" };
            if (TextboxFrom[this.ultabBASETABLE.ActiveTab.VisibleIndex].Text != "SAP")
            {
                if (DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] != "")
                {
                    if (DeleteMethod[this.ultabBASETABLE.ActiveTab.VisibleIndex] != "DeletVoiceData")
                    {
                        result = MessageBox.Show("是否要删除该记录！", "提示", buttons);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                            Deletetable(DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex], DeleteMethod[this.ultabBASETABLE.ActiveTab.VisibleIndex]);
                        QueryTableData(QueryContion, this.ultabBASETABLE.Tabs[this.ultabBASETABLE.ActiveTab.VisibleIndex].Key.ToString().Trim(),
                        this.ultabBASETABLE.ActiveTab.VisibleIndex);
                        DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] = "";
                    }
                    else
                    {
                        DeleteVoictable(DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex], this.txtVoiceTy.Text);
                        QueryTableData(QueryContion, this.ultabBASETABLE.Tabs[this.ultabBASETABLE.ActiveTab.VisibleIndex].Key.ToString().Trim(),
                        this.ultabBASETABLE.ActiveTab.VisibleIndex);
                        DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] = "";
                    }
                }
                else
                {
                    MessageBox.Show("请选择要删除的记录！");
                }
            }
            else
            {
                MessageBox.Show("SAP数据不能删除！");
            }
                
        }

        private void UpdateTable()
        {
            if (true)//TextboxFrom[this.ultabBASETABLE.ActiveTab.VisibleIndex].Text != "SAP")
            {
                switch (this.ultabBASETABLE.ActiveTab.VisibleIndex)
                {
                    case 0:
                        if (DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] != "")
                        {
                            UpdateWLtable(DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex], this.txtWLMC.Text.Trim(), this.txtWLTYPE.Text.Trim(),
                                this.txtWLZ.Text.Trim(), this.txtWLUNIT.Text.Trim(), this.txtWLZMS.Text.Trim(), this.txtWLPYZJM.Text.Trim(), this.txtWLFROM.Text.Trim(),this.txtFACTOR.Text);
                        }
                        else
                        {
                            MessageBox.Show("请选择要修改的记录！");
                        }
                        break;
                    case 1:
                        if (DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] != "")
                        {
                            UpdateFHtable(DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex], this.txtFHCJMS.Text.Trim(), this.txtFHGCDM.Text.Trim(),
                                this.txtFHCARNOBEGIN.Text.Trim(), this.txtFHCARNOEND.Text.Trim(),
                                this.txtFHPYZJM.Text.Trim(), this.txtFHFROM.Text.Trim(), UserCKChangeSet(this.CKFHUser));
                        }
                        else
                        {
                            MessageBox.Show("请选择要修改的记录！");
                        }
                        break;
                    case 2:
                        if (DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] != "")
                        {
                            UpdateGYtable(DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex],
                                this.txtGYNAME.Text.Trim(), this.txtGYFROM.Text.Trim(), this.txtGYPYZJM.Text.Trim(), UserCKChangeSet(this.CKGYuser));
                        }
                        else
                        {
                            MessageBox.Show("请选择要修改的记录！");
                        }
                        break;
                    case 3:
                        if (DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] != "")
                        {
                            UpdateSHtable(DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex],
                                this.txtSHNAME.Text.Trim(), this.txtSHFACTNO.Text.Trim(), this.txtSHPYZJM.Text.Trim(), this.txtSHFROM.Text.Trim(), UserCKChangeSet(this.CKSHuser));
                        }
                        else
                        {
                            MessageBox.Show("请选择要修改的记录！");
                        }
                        break;
                    case 4:
                        if (DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] != "")
                        {
                            UpdateCYtable(DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex],
                                this.txtCYNAME.Text.Trim(), this.txtCYPYZJM.Text.Trim(), this.txtCYFROM.Text.Trim(),UserCKChangeSet(this.CKCYuser));
                        }
                        else
                        {
                            MessageBox.Show("请选择要修改的记录！");
                        }
                        break;
                    case 6:
                        if (DeleteNO[this.ultabBASETABLE.ActiveTab.VisibleIndex] != "")
                        {
                            UpdateVoiceData(VoiceNa,VoiceTy, this.txtVoiceNa.Text.Trim(), this.cbxPointType.Text.Trim(), this.txtVoiceDe.Text.Trim(), VoiceFile);
                        }
                        else
                        {
                            MessageBox.Show("请选择要修改的记录！");
                        }
                        break;

                }
                QueryTableData(QueryContion, this.ultabBASETABLE.Tabs[this.ultabBASETABLE.ActiveTab.VisibleIndex].Key.ToString().Trim(),
                   this.ultabBASETABLE.ActiveTab.VisibleIndex);
            }
            else
            {
                //MessageBox.Show("SAP数据不能修改！");
            }
           
        }
        #endregion

        #region 基础表管理--基础表记录双击事件
        private void ulgWLTABLE_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (this.ulgWLTABLE.Rows.Count > 0)
            {
                DeleteNO[0] = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_WL"].Value.ToString();
                this.txtWLMC.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_MATERIALNAME"].Value.ToString();
                this.txtWLZMS.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_GROUPDESCRIBE"].Value.ToString();
                this.txtWLTYPE.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_MATERIALTYPE"].Value.ToString();
                this.txtWLUNIT.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_WEIGHTUNIT"].Value.ToString();
                this.txtWLZ.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_MATERIALGROUP"].Value.ToString();
                this.txtWLFROM.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_FROM"].Value.ToString();
                this.txtWLPYZJM.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_HELPCODE"].Value.ToString();
                this.txtJLMeNoSt.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FS_WL"].Value.ToString();
                this.txtFACTOR.Text = this.ulgWLTABLE.Rows[e.Row.Index].Cells["FN_FACTOR"].Value.ToString();
                RowsIndex = e.Row.Index;
            }
        }
        private void ulgFHTABLE_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (this.ulgFHTABLE.Rows.Count > 0)
            {
                DeleteNO[1] = this.ulgFHTABLE.Rows[e.Row.Index].Cells["FS_FH"].Value.ToString();
                this.txtFHGCDM.Text = this.ulgFHTABLE.Rows[e.Row.Index].Cells["FS_PLANT"].Value.ToString();
                this.txtFHCJMS.Text = this.ulgFHTABLE.Rows[e.Row.Index].Cells["FS_MEMO"].Value.ToString();
                this.txtFHCARNOBEGIN.Text = this.ulgFHTABLE.Rows[e.Row.Index].Cells["FS_CARNOBEGIN"].Value.ToString();
                this.txtFHCARNOEND.Text = this.ulgFHTABLE.Rows[e.Row.Index].Cells["FS_CARNOEND"].Value.ToString();
                this.txtFHFROM.Text = this.ulgFHTABLE.Rows[e.Row.Index].Cells["FS_FROM"].Value.ToString();
                this.txtFHPYZJM.Text = this.ulgFHTABLE.Rows[e.Row.Index].Cells["FS_HELPCODE"].Value.ToString();
                SetUserCkchange(this.CKFHUser,this.ulgFHTABLE.Rows[e.Row.Index].Cells["FS_USERROLES"].Value.ToString());
            }
        }
        private void ulgGYTABLE_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (this.ulgGYTABLE.Rows.Count > 0)
            {
                DeleteNO[2] = this.ulgGYTABLE.Rows[e.Row.Index].Cells["FS_GY"].Value.ToString();
                this.txtGYNAME.Text = this.ulgGYTABLE.Rows[e.Row.Index].Cells["FS_SUPPLIERNAME"].Value.ToString();
                this.txtGYFROM.Text = this.ulgGYTABLE.Rows[e.Row.Index].Cells["FS_FROM"].Value.ToString();
                this.txtGYPYZJM.Text = this.ulgGYTABLE.Rows[e.Row.Index].Cells["FS_HELPCODE"].Value.ToString();
                SetUserCkchange(this.CKGYuser, this.ulgGYTABLE.Rows[e.Row.Index].Cells["FS_USERROLES"].Value.ToString());
            }

        }

        private void ulgSHTABLE_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (this.ulgSHTABLE.Rows.Count > 0)
            {
                DeleteNO[3] = this.ulgSHTABLE.Rows[e.Row.Index].Cells["FS_SH"].Value.ToString();
                this.txtSHNAME.Text = this.ulgSHTABLE.Rows[e.Row.Index].Cells["FS_MEMO"].Value.ToString();
                this.txtSHFACTNO.Text = this.ulgSHTABLE.Rows[e.Row.Index].Cells["FS_FACTORYNO"].Value.ToString();
                this.txtSHFROM.Text = this.ulgSHTABLE.Rows[e.Row.Index].Cells["FS_FROM"].Value.ToString();
                this.txtSHPYZJM.Text = this.ulgSHTABLE.Rows[e.Row.Index].Cells["FS_HELPCODE"].Value.ToString();
                SetUserCkchange(this.CKSHuser, this.ulgSHTABLE.Rows[e.Row.Index].Cells["FS_USERROLES"].Value.ToString());
            }

        }
        
        private void ulgCYTABLE_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (this.ulgCYTABLE.Rows.Count > 0)
            {
                DeleteNO[4] = this.ulgCYTABLE.Rows[e.Row.Index].Cells["FS_CY"].Value.ToString();
                this.txtCYNAME.Text = this.ulgCYTABLE.Rows[e.Row.Index].Cells["FS_TRANSNAME"].Value.ToString();
                this.txtCYFROM.Text = this.ulgCYTABLE.Rows[e.Row.Index].Cells["FS_FROM"].Value.ToString();
                this.txtCYPYZJM.Text = this.ulgCYTABLE.Rows[e.Row.Index].Cells["FS_HELPCODE"].Value.ToString();
                SetUserCkchange(this.CKCYuser, this.ulgCYTABLE.Rows[e.Row.Index].Cells["FS_USERROLES"].Value.ToString());
            }
        }
        private void ulgrdVoice_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (this.ulgrdVoice.Rows.Count > 0)
            {
                DeleteNO[6] = this.ulgrdVoice.Rows[e.Row.Index].Cells["FS_VOICENAME"].Value.ToString();
                this.txtVoiceNa.Text = this.ulgrdVoice.Rows[e.Row.Index].Cells["FS_VOICENAME"].Value.ToString();
                this.txtVoiceTy.Text = this.ulgrdVoice.Rows[e.Row.Index].Cells["FS_INSTRTYPE"].Value.ToString();
                VoiceNa = this.ulgrdVoice.Rows[e.Row.Index].Cells["FS_VOICENAME"].Value.ToString();
                VoiceTy = this.ulgrdVoice.Rows[e.Row.Index].Cells["FS_INSTRTYPE"].Value.ToString();
                this.txtVoiceDe.Text = this.ulgrdVoice.Rows[e.Row.Index].Cells["FS_MEMO"].Value.ToString();
                VoiceFile = (byte[])this.ulgrdVoice.Rows[e.Row.Index].Cells["FS_VOICEFILE"].Value;

            }
        }
        private void ultabBASETABLE_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            
            switch (this.ultabBASETABLE.ActiveTab.VisibleIndex)
            {
                case 0:
                    QueryContion = this.txtWLNO.Text.Trim();
                    break;
                case 1:
                    QueryContion = this.txtFHNO.Text.Trim();
                    GetUserInfor(this.CKFHUser);
                    break;
                case 2:
                    QueryContion = this.txtGYNO.Text.Trim();
                    GetUserInfor(this.CKGYuser);
                    break;
                case 3:
                    QueryContion = this.txtSHNO.Text.Trim();
                    GetUserInfor(this.CKSHuser);
                    break;
                case 4:
                    QueryContion = this.txtCYNO.Text.Trim();
                    GetUserInfor(this.CKCYuser);
                    break;
                case 6:
                    QueryContion = this.txtVoiceName.Text.Trim();
                    QueryPointTypeToCb(this.cbxPointType);
                    break;
            }
            
        }
        private void txtWLNO_TextChanged(object sender, EventArgs e)
        {
            QueryContion = this.txtWLNO.Text.Trim();
        }

        private void txtFHNO_TextChanged(object sender, EventArgs e)
        {
            QueryContion = this.txtFHNO.Text.Trim();
        }

        private void txtGYNO_TextChanged(object sender, EventArgs e)
        {
            QueryContion = this.txtGYNO.Text.Trim();
        }

        private void txtSHNO_TextChanged(object sender, EventArgs e)
        {
            QueryContion = this.txtSHNO.Text.Trim();
        }

        private void txtCYNO_TextChanged(object sender, EventArgs e)
        {
            QueryContion = this.txtCYNO.Text.Trim();
        }
        private void txtVoiceName_TextChanged(object sender, EventArgs e)
        {
            QueryContion = this.txtVoiceName.Text.Trim();
        }

        #endregion

        #region DOCK显示管理
        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            switch (this.ultabTotal.ActiveTab.VisibleIndex)
            {
                case 0:
                    this.ulDockManger.ControlPanes[0].Closed = false;
                    this.ulDockManger.ControlPanes[0].Pinned = false;
                    this.ulDockManger.ControlPanes[1].Closed = true;
                    
                    break;
                case 1:
                    this.ulDockManger.ControlPanes[1].Closed = false;
                    this.ulDockManger.ControlPanes[1].Pinned = false;
                    this.ulDockManger.ControlPanes[0].Closed = true;
                    break;
            }
        }
        #endregion

        #region 从SAP下载、更新基础表信息
        /// <summary>
        /// 物料SAP下载方法
        /// </summary>
        private void down_IT_MATERIAL(string MaterialNo)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.Sap.DownloadSapRfc";
            ccp.MethodName = "down_IT_MATERIAL";
            ccp.ServerParams = new object[] { "ZJL_MATERIALEXTRA_DOWN", MaterialNo};
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 发货单位SAP下载方法
        /// </summary>
        private void down_IT_MRP()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.Sap.DownloadSapRfc";
            ccp.MethodName = "down_IT_MRP";
            ccp.ServerParams = new object[] {"ZJL_MRP_DEPART" };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 收货单位SAP下载方法
        /// </summary>
        private void down_IT_STORE()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.Sap.DownloadSapRfc";
            ccp.MethodName = "down_IT_STORE";
            ccp.ServerParams = new object[] {"ZJL_OTHER_MASTER","8000"};
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        
        /// <summary>
        /// 物料SAP下载事件
        /// </summary>
        private void ultraButton1_Click(object sender, EventArgs e)
        {
            if (this.txtSapMeterialNo.Text != "")
            {
                down_IT_MATERIAL(this.txtSapMeterialNo.Text);
            }
            if (this.txtJLMeNoSt.Text != "")
            {
                for (int i = RowsIndex; i < this.ulgWLTABLE.Rows.Count; i++)
                {
                    down_IT_MATERIAL(this.ulgWLTABLE.Rows[i].Cells["FS_SAPCODE"].Value.ToString());
                    if (this.ulgWLTABLE.Rows[i].Cells["FS_WL"].Value.ToString() == this.txtJLMeNoEnd.Text)
                    {
                        return;
                    }
                   
                }
            }
            
        }
        /// <summary>
        /// 发货单位SAP下载
        /// </summary>
        private void ultraButton2_Click(object sender, EventArgs e)
        {
            down_IT_MRP();
        }
        /// <summary>
        /// 收货单位SAP下载
        /// </summary>
        private void ultraButton3_Click(object sender, EventArgs e)
        {
            down_IT_STORE();
        }
       
        #endregion

        #region 角色权限分配处理

        /// <summary>
        /// 角色选择状态改变
        /// </summary>
        private string UserCKChangeSet(CheckedListBox checkbox)
        {
            string checkStr = "";
            if (checkbox.Items.Count > 0)
            {
                for (int i = 0; i < checkbox.Items.Count; i++)
                {
                    if (checkbox.GetItemCheckState(i) == CheckState.Checked)
                    {
                        checkbox.SelectedIndex = i;
                        checkStr += checkbox.SelectedValue + ",";
                        
                    }
                }
            }
            return checkStr;
        }
        /// <summary>
        /// 角色选择状态设置
        /// </summary>
        private void SetUserCkchange(CheckedListBox checkbox,string UserInfor)
        {
            if (checkbox.Items.Count > 0)
            {
                if (UserInfor == "")
                    {
                        for (int j = 0; j < checkbox.Items.Count; j++)
                        {
                            checkbox.SetItemChecked(j, false);
                        }
                    }
                else
                {
                    for (int i = 0; i < checkbox.Items.Count; i++)
                    {
                        checkbox.SelectedIndex = i;
                        if (UserInfor.Contains(checkbox.SelectedValue.ToString().Trim()))
                        {
                            checkbox.SetItemChecked(i, true);
                        }
                        else
                        {
                            checkbox.SetItemChecked(i, false);
                        }

                    }

                }
                
            }
        }
        #endregion

        #region 声音文件选择（*.wav）
        private void btnVoiceOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"F:\昆钢项目\";  //指定打开文件默认路径
            openFileDialog.Filter = "Wav　文件(*.wav)|*.wav"; ;     //指定打开默认选择文件类型名
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {                
                byte[] buffer = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                if (buffer.Length > 1)
                {
                    VoiceFile = buffer;
                    this.txtVoiceNa.Text = openFileDialog.SafeFileName;
                }
                else
                {
                    MessageBox.Show("声音文件打开失败！");
                }
                

            }
        }
        #endregion

        #region 修改数据项清空
        private void ClearVoiceTableData()
        {
            VoiceFile = null;
            this.txtVoiceNa.Text = "";
            this.txtVoiceTy.Text = "";
            this.txtVoiceDe.Text = "";
        }
        #endregion

        #region 语音基础表磅房类型下拉框处理
        private void cbxPointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPointType.SelectedIndex == -1)
                return;
            if (cbxPointType.Items[cbxPointType.SelectedIndex] != null)
            this.txtVoiceTy.Text = cbxPointType.Items[cbxPointType.SelectedIndex].ToString().Substring(0,2);
        }

        private void txtVoiceTy_TextChanged(object sender, EventArgs e)
        {
            if (this.txtVoiceTy.Text != "")
            {
                int index=cbxPointType.FindString(this.txtVoiceTy.Text);
                cbxPointType.SelectedIndex = index;
            }
        }
        #endregion        

        #region 输入文本长度判断
        private void CheckTextLenth(TextBox tb, int length)
        {
            if (tb.Text.Length > length)
            {
                MessageBox.Show("流向长度不能大于" + length + "位！");
                string S = tb.Text;
                tb.Text = S.Remove(length, tb.Text.Length - length);
            }
            else
            {
                return;
            }
        }
        #endregion

        #region 发货单位修改项输入检查
        private void txtFHCJMS_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtFHCJMS, 100);
        }
        private void txtFHPYZJM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtFHPYZJM, 30);
        }

        private void txtFHGCDM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtFHGCDM, 4);
        }

        private void txtFHFROM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtFHFROM, 4);
        }

        private void txtFHCARNOBEGIN_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtFHCARNOBEGIN, 7);
        }

        private void txtFHCARNOEND_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtFHCARNOEND, 7);
        }
        #endregion

        #region 供应单位修改项输入检查

        private void txtGYNAME_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtGYNAME, 100);
        }

        private void txtGYPYZJM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtGYPYZJM, 30);
        }

        private void txtGYFROM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtGYFROM, 4);
        }
        #endregion

        #region 收货单位修改项输入检查
        private void txtSHNAME_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtSHNAME, 100);
        }

        private void txtSHPYZJM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtSHPYZJM, 30);
        }

        private void txtSHFACTNO_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtSHFACTNO, 4);
        }

        private void txtSHFROM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtSHFROM, 4);
        }
        #endregion

        #region 承运单位修改输入检查
        private void txtCYNAME_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtCYNAME, 50);
        }

        private void txtCYFROM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtCYFROM, 4);
        }

        private void txtCYPYZJM_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtCYPYZJM, 30);
        }
        #endregion

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    if (this.ultabTotal.ActiveTab.VisibleIndex == 1)
                    {
                        QueryTableData(
                            QueryContion,
                            this.ultabBASETABLE.Tabs[this.ultabBASETABLE.ActiveTab.VisibleIndex].Key.ToString().Trim(),
                            this.ultabBASETABLE.ActiveTab.VisibleIndex);
                    }
                    else
                    {

                    }
                    break;
                case "Add":
                    if (this.ultabTotal.ActiveTab.VisibleIndex == 1)
                    {
                        AddTable();
                    }
                    else
                    {
                        InsertBaseInformManger();
                    }
                    break;
                case "Delete":
                    if (this.ultabTotal.ActiveTab.VisibleIndex == 1)
                    {
                        DeleteTable();
                    }
                    else
                    {
                        DeleteBaseInformManger();
                    }
                    break;
                case "Update":
                    if (this.ultabTotal.ActiveTab.VisibleIndex == 1)
                    {
                        UpdateTable();
                    }
                    else
                    {
                        UpdateBaseInformManger();
                    }
                    break;
            }
        }

        private void txtWLMC_TextChanged(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtWLMC, 100);
            Constant.ConvertTOspell(this.txtWLMC, this.txtWLPYZJM);
        }

        private void txtFHCJMS_TextChanged_1(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtFHCJMS, 100);
            Constant.ConvertTOspell(this.txtFHCJMS, this.txtFHPYZJM);
        }

        private void txtGYNO_TextChanged_1(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtGYNAME, 100);
            Constant.ConvertTOspell(this.txtGYNAME, this.txtGYPYZJM);
        }

        private void txtSHNAME_TextChanged_1(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtSHNAME, 100);
            Constant.ConvertTOspell(this.txtSHNAME, this.txtSHPYZJM);
        }

        private void txtCYNAME_TextChanged_1(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtCYNAME, 50);
            Constant.ConvertTOspell(this.txtCYNAME, this.txtCYPYZJM);
        }

        private void txtGYNAME_TextChanged_1(object sender, EventArgs e)
        {
            CheckTextLenth(this.txtGYNAME, 100);
            Constant.ConvertTOspell(this.txtGYNAME, this.txtGYPYZJM);
        }


    }
}
