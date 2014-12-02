﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using YGJZJL.PublicComponent;

namespace YGJZJL.BaseDataManage
{
    public partial class SettleMentManager : FrmBase
    {
        private string strCurrentSupplierCode = "";
        private string strCurrentMrpCode = "";
        public SettleMentManager()
        {
            InitializeComponent();
        }

        private void btnTemplateOk_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string strSql1 = @"select FS_GY,FS_SETTLEMENTNAME from IT_SUPPLIER_SETTLEMENT where FS_GY = '" + this.strCurrentSupplierCode + "'";
            string strSql2 = "";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql1 };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    strSql2 = @"update IT_SUPPLIER_SETTLEMENT set FS_SETTLEMENTNAME='" + this.ugcpFS_SETTLEMENTNAME.Text + "' where FS_GY = '" + this.strCurrentSupplierCode + "'";
                   ;

                }
                else
                {
                    strSql2 = @"Insert into IT_SUPPLIER_SETTLEMENT (FS_GY,FS_SETTLEMENTNAME) values ('" + this.strCurrentSupplierCode + "','" + this.ugcpFS_SETTLEMENTNAME.Text + "')";
                }
                 CoreClientParam ccp1 = new CoreClientParam();
                 ccp1.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp1.MethodName = "ExcuteNonQuery";
                ccp1.ServerParams = new object[] { strSql2 };
                ccp1 = this.ExecuteNonQuery(ccp1, CoreInvokeType.Internal);
                if (ccp1.ReturnCode == 0)
                {
                    MessageBox.Show("更新成功！");
                }
                else
                {
                    MessageBox.Show("更新失败！");
                    return;
                }
            }
            // This code was automatically generated by the RowEditTemplate Wizard
            // 
            // Close the template and save any pending changes.
            this.ultraGridRowEditTemplate1.Close(true);

        }

        private void btnTemplateCancel_Click(object sender, EventArgs e)
        {
            // This code was automatically generated by the RowEditTemplate Wizard
            // 
            // Close the template and discard any pending changes.
            this.ultraGridRowEditTemplate1.Close(false);

        }

        private void btnTemplateOk1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string strSql1 = @"select FS_FH,FS_SETTLEMENTNAME from IT_MRP_SETTLEMENT where FS_FH = '" + this.strCurrentMrpCode + "'";
            string strSql2 = "";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql1 };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    strSql2 = @"update IT_MRP_SETTLEMENT set FS_SETTLEMENTNAME='" + this.ugcpFS_SETTLEMENTNAME1.Text + "' where FS_FH = '" + this.strCurrentMrpCode + "'";
                    ;

                }
                else
                {
                    strSql2 = @"Insert into IT_MRP_SETTLEMENT (FS_FH,FS_SETTLEMENTNAME) values ('" + this.strCurrentMrpCode + "','" + this.ugcpFS_SETTLEMENTNAME1.Text + "')";
                }
                CoreClientParam ccp1 = new CoreClientParam();
                ccp1.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp1.MethodName = "ExcuteNonQuery";
                ccp1.ServerParams = new object[] { strSql2 };
                ccp1 = this.ExecuteNonQuery(ccp1, CoreInvokeType.Internal);
                if (ccp1.ReturnCode == 0)
                {
                    MessageBox.Show("更新成功！");
                }
                else
                {
                    MessageBox.Show("更新失败！");
                    return;
                }
            }
            // This code was automatically generated by the RowEditTemplate Wizard
            // 
            // Close the template and save any pending changes.
            this.ultraGridRowEditTemplate2.Close(true);

        }

        private void btnTemplateCancel1_Click(object sender, EventArgs e)
        {
            // This code was automatically generated by the RowEditTemplate Wizard
            // 
            // Close the template and discard any pending changes.
            this.ultraGridRowEditTemplate2.Close(false);

        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            if (e.Tool.Key == "Query")
            {
                DoQuery();
            }
        }
        private void DoQuery()
        {
            string strSql1 = @"select FS_GY,FS_SUPPLIERNAME,FS_HELPCODE,GY_CODETOSETTLEMENTNAME(FS_GY) FS_SETTLEMENTNAME from it_supplier";
            string strSql2=@"select FS_FH,FS_MEMO,FS_HELPCODE,FH_CODETOSETTLEMENTNAME(FS_FH) FS_SETTLEMENTNAME from IT_MRP";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql1 };
            dataSet1.Tables[0].Clear();
            ccp.SourceDataTable = dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            ccp.ServerParams = new object[] { strSql2 };
            dataSet1.Tables[1].Clear();
            ccp.SourceDataTable = dataSet1.Tables[1];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);


        }

        private void ultraGrid1_AfterRowEditTemplateDisplayed(object sender, Infragistics.Win.UltraWinGrid.AfterRowEditTemplateDisplayedEventArgs e)
        {
            strCurrentSupplierCode = e.Template.Row.Cells["FS_GY"].Value.ToString();
        }

        private void ultraGrid2_AfterRowEditTemplateDisplayed(object sender, Infragistics.Win.UltraWinGrid.AfterRowEditTemplateDisplayedEventArgs e)
        {
            strCurrentMrpCode = e.Template.Row.Cells["FS_FH"].Value.ToString();
        }

    }
}