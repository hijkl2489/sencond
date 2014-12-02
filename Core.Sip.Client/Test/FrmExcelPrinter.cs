using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
//using Core.ZGGJMes.Client.Printer;


namespace Core.Sip.Client.Test
{
    public partial class FrmExcelPrinter : Form
    {
        public FrmExcelPrinter()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //printer p = new printer();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("模版类型");
            dt.Columns.Add("合同号");
            dt.Columns.Add("标准号");
            dt.Columns.Add("牌号");
            dt.Columns.Add("炉号");
            dt.Columns.Add("规格");
            dt.Columns.Add("重量");
            dt.Columns.Add("支数");
            dt.Columns.Add("检验号");
            dt.Columns.Add("日期");
            dt.Columns.Add("条码号");
            ds.Tables.Add(dt);
            ds.Tables[0].Rows.Add("内销", "合同号1", "标准号1", "牌号1", "炉号1", "规格1", "重量1", "支数1", "检验号1", "日期1", "1234567890111");
            ds.Tables[0].Rows.Add("内销", "合同号2", "标准号2", "牌号2", "炉号2", "规格2", "重量2", "支数2", "检验号2", "日期2", "1234567890122");
            ds.Tables[0].Rows.Add("内销", "合同号3", "标准号3", "牌号3", "炉号3", "规格3", "重量3", "支数3", "检验号3", "日期3", "1234567890131");
            ds.Tables[0].Rows.Add("内销", "合同号4", "标准号4", "牌号4", "炉号4", "规格4", "重量4", "支数4", "检验号4", "日期4", "1234567890142");
            ds.Tables[0].Rows.Add("内销", "合同号5", "标准号5", "牌号5", "炉号5", "规格5", "重量5", "支数5", "检验号5", "日期5", "1234567890151");
            ds.Tables[0].Rows.Add("内销", "合同号6", "标准号6", "牌号6", "炉号6", "规格6", "重量6", "支数6", "检验号6", "日期6", "1234567890162");
            ds.Tables[0].Rows.Add("内销", "合同号7", "标准号7", "牌号7", "炉号7", "规格7", "重量7", "支数7", "检验号7", "日期7", "1234567890171");
            ds.Tables[0].Rows.Add("内销", "合同号8", "标准号8", "牌号8", "炉号8", "规格8", "重量8", "支数8", "检验号8", "日期8", "1234567890182");

            //p._printData = ds;
            //p.DoPrint();



            ArrayList strReturn = null;
            Assembly assembly = Assembly.LoadFrom("Core.ZGGJMes.Client.Printer.dll");

            // Create new type
            Type t = assembly.GetType("Core.ZGGJMes.Client.Printer.printer");

            // Call static member function by name
            //string strReturn = (string)t.InvokeMember("GetNewValue",
            //     BindingFlags.DeclaredOnly |
            //     BindingFlags.Public |
            //     BindingFlags.Static |
            //     BindingFlags.InvokeMethod, null,
            //     null, new object[] { 12 });

            // Set class property
            //t.InvokeMember("Name",
            //    BindingFlags.DeclaredOnly |
            //    BindingFlags.Public | BindingFlags.NonPublic |
            //    BindingFlags.Instance | BindingFlags.SetProperty,
            //    null,
            //    obj,
            //    new Object[] { "Test" });

            // Create new object of specific class name
            Object obj = t.InvokeMember(
                null,
                BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                null,
                null);

            FieldInfo myFieldInfo = t.GetField("_printData", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            myFieldInfo.SetValue(obj, ds);

            FieldInfo myFieldInfo2 = t.GetField("_printType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            myFieldInfo2.SetValue(obj, "内销");

            FieldInfo myFieldInfo3 = t.GetField("_printCopies", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            myFieldInfo3.SetValue(obj, "2");  

            // Call member function by name
            strReturn = (ArrayList)t.InvokeMember("DoPrint",
            BindingFlags.DeclaredOnly |
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.InvokeMethod,
            null,
            obj,
            new object[] { });

           

            // Get class property
            strReturn = (ArrayList)t.InvokeMember("Name",
             BindingFlags.DeclaredOnly |
             BindingFlags.Public |
             BindingFlags.NonPublic |
             BindingFlags.Instance |
             BindingFlags.GetProperty,
             null,
             obj,
             null); 
        }
    }
}