using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using YGJZJL.PublicComponent;

namespace YGJZJL.StrapWeight
{
    public partial class WeightQuery_Strap : FrmBase
    {
        public string PointID = "";   //计量点编码
        public string WeightNo = "";   //作业号
        public string flag = "";
        public string FS_WL = "";   //物料代码
        public string FS_FH = "";   //发货单位代码
        public string FS_SH = "";   //收货单位代码
        public string valueWL = "";   //预报（查询获得）物料代码
        public string valueFH = "";   //预报（查询获得）发货单位代码
        public string valueSH = "";   //预报（查询获得）收货单位代码
        private string stRunPath;
        private string strExcelName;
        System.Data.DataTable dtHTH = new System.Data.DataTable();
        GetBaseInfo BaseInfo;

        LimitQueryTime limitQueryTime = new LimitQueryTime();//为判断时间区间设定的变量
        DateTime beginTime;
        DateTime endTime;
        bool decisionResult;

        public WeightQuery_Strap()
        {
            InitializeComponent();
        }

        #region Toolbar按钮事件

        public override void ToolBar_Click(object sender, string ToolbarKey)
        {
            base.ToolBar_Click(sender, ToolbarKey);

            switch (ToolbarKey)
            {
                case "Query":
                    if (this.dateTimePicker1.Value >= this.dateTimePicker2.Value)
                    { 
                        MessageBox.Show("查询起始时间应小于结束时间"); 
                        return; 
                    }
                    this.DoQueryMain();
                    break;

                case "Update":
                    break;
                case"Delete":
                    break;

                default:
                    break;
            }
        }
        #endregion

        private bool Decision()//判断所选时间区间是否大于60天，如果大于则返回false//杨滔添加
        {
            beginTime = dateTimePicker1.Value.Date;
            endTime = dateTimePicker2.Value.Date;
            decisionResult = limitQueryTime.ParseTime(beginTime, endTime);
            if (!decisionResult)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void DoQueryMain()
        {
            string startTime = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string str = " and A.FD_ENDTIME between  to_date('" + startTime + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + endTime + "','yyyy-MM-dd HH24:mi:ss')";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.strapweight.StrapWeight";
            ccp.MethodName = "QueryJLData";
            ccp.ServerParams = new object[] { str };

            dataSet1.Tables[0].Clear();

            ccp.SourceDataTable = dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
            {
                if (dataSet1.Tables[0].Rows[i]["FS_CHECKED"].ToString() == "1")
                    dataSet1.Tables[0].Rows[i]["FS_CHECKED"] = "已审核";
                else
                    dataSet1.Tables[0].Rows[i]["FS_CHECKED"] = "未审核";
            }
            Constant.RefreshAndAutoSize(ultraGrid1);
        }

    

        private void WeightQuery_Strap_Load(object sender, EventArgs e)
        {
            BaseInfo = new GetBaseInfo();
            pictureBox5.Hide();
            stRunPath = System.Environment.CurrentDirectory;//当前界面自己定义路径
            dateTimePicker1.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dateTimePicker2.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }

        private void ultraGrid1_Click(object sender, EventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;
            WeightNo = ultraGrid1.ActiveRow.Cells["FS_WEIGHTNO"].Text.Trim();
            PointID = ultraGrid1.ActiveRow.Cells["FS_POINTID"].Text.Trim();
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            //ShowPic(ultraGrid1.ActiveRow.Cells["FS_WEIGHTNO"].Text.Trim());
            
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        if (!Decision())//判断时间区间//杨滔添加
                        {
                            return;
                        }
                        if (this.dateTimePicker1.Value >= this.dateTimePicker2.Value)
                        {
                            MessageBox.Show("查询起始时间应小于结束时间");
                            return;
                        }
                        this.DoQueryMain();
                        break;
                    }
                case "Print":
                    {
                        PrintExcel();
                        break;
                    }
                case "ToExcel":
                    {
                        Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, ultraGrid1);
                        Constant.WaitingForm.Close();
                        break;
                    }
                default:
                    break;
            }

        }


       

       private void PrintExcel()
       {
           Excel.Application ExApp = new Excel.Application();
           object MissingValue = Type.Missing;
           ExApp.Visible = false;


           ExApp.Application.Workbooks.Open(stRunPath + "\\temp\\皮带料斗秤计量数据查询" + strExcelName,
                    MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue,
                    MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue
                    );

           ExApp.ActiveWindow.SelectedSheets.PrintOut(1, 1, 1, false, "", false, false, null);

           ExApp.ActiveWorkbook.Close(false, Constant.RunPath + strExcelName, null);
           ExApp.Quit();
           ExApp = null;

           System.Threading.Thread.Sleep(6000);
       }

       private void pictureBox1_Click(object sender, EventArgs e)
       {
           pictureBox5.Image = pictureBox1.Image;
           pictureBox5.Show();
       }
       private void pictureBox2_Click(object sender, EventArgs e)
       {
           pictureBox5.Image = pictureBox2.Image;
           pictureBox5.Show();
       }   
       private void pictureBox3_Click(object sender, EventArgs e)
       {
           pictureBox5.Image = pictureBox3.Image;
           pictureBox5.Show();
       }

       private void pictureBox4_Click(object sender, EventArgs e)
       {
           pictureBox5.Image = pictureBox4.Image;
           pictureBox5.Show();
       }
       private void pictureBox5_Click(object sender, EventArgs e)
       {
           pictureBox5.Hide();
       }
       private void ShowPic(string WeightNo)
       {

           if (WeightNo.Length <= 0)
               return;

           BaseInfo getImage = new BaseInfo();

           getImage.QueryPDImage(WeightNo);
           DataTable dtTP = getImage.dtImage;
           if (dtTP.Rows.Count > 0)
           {
               byte[] imagebytes1 = (byte[])dtTP.Rows[0]["FB_IMAGE1"];
               byte[] imagebytes2 = (byte[])dtTP.Rows[0]["FB_IMAGE2"];
               byte[] imagebytes3 = (byte[])dtTP.Rows[0]["FB_IMAGE3"];
               byte[] imagebytes4 = (byte[])dtTP.Rows[0]["FB_IMAGE4"];
               if (imagebytes3.Length > 1 && imagebytes4.Length >1)
               {
                   getImage.BitmapToImage(imagebytes1, pictureBox1, pictureBox1.Width, pictureBox1.Height);
                   getImage.BitmapToImage(imagebytes2, pictureBox2, pictureBox2.Width, pictureBox2.Height);
                   getImage.BitmapToImage(imagebytes3, pictureBox3, pictureBox3.Width, pictureBox3.Height);
                   getImage.BitmapToImage(imagebytes4, pictureBox4, pictureBox4.Width, pictureBox4.Height);
               }
               else
               {
                   getImage.BitmapToImage(imagebytes1, pictureBox1, pictureBox1.Width, pictureBox1.Height);
                   getImage.BitmapToImage(imagebytes2, pictureBox3, pictureBox3.Width, pictureBox3.Height);
               }
           }
           else
           { 
               this.pictureBox1.Image = null;
               this.pictureBox2.Image = null;
               this.pictureBox3.Image = null;
               this.pictureBox4.Image = null;
           }
       }

     

    }
}
