using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;


namespace YGJZJL.PublicComponent
{
    public partial class GetBaseInfo : FrmBase
    {
        public string strBfbh;

        public GetBaseInfo()
        {
            InitializeComponent();
        }
        public GetBaseInfo(object obb)
        {
           
            this.ob = (OpeBase)obb;
        }

        //获取磅房剩余纸张数
        public int GetZZData(string PointID)
        {
            int paperNum = 0;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryZZData";
            ccp.ServerParams = new object[] { PointID };
            System.Data.DataTable dt = new System.Data.DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            paperNum = Convert.ToInt32(dt.Rows[0][0].ToString());
            return paperNum;

        }

        //磅房换纸
        public int SetZZData(string PointID)
        {
            int paperNum = 0;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "SetZZData";
            ccp.ServerParams = new object[] { PointID };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            paperNum = Convert.ToInt32(ccp.ReturnObject.ToString());
            return paperNum;
        }

        //磅房用纸（纸张减一）
        public int SubZZData(string PointID)
        {
            int paperNum = 0;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "SubZZData";
            ccp.ServerParams = new object[] { PointID };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            paperNum = Convert.ToInt32(ccp.ReturnObject.ToString());
            return paperNum;
        }

        //获取磅房剩余纸碳带
        public int GetTDData(string PointID)
        {
            int inkNum = 0;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryTDData";
            ccp.ServerParams = new object[] { PointID };
            System.Data.DataTable dt = new System.Data.DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            inkNum = Convert.ToInt32(dt.Rows[0][0].ToString());
            return inkNum;

        }

        //磅房换碳带
        public int SetTDData(string PointID)
        {
            int inkNum = 0;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "SetTDData";
            ccp.ServerParams = new object[] { PointID };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            inkNum = Convert.ToInt32(ccp.ReturnObject.ToString());
            return inkNum;
        }

        //磅房用碳带（碳带张减一）
        public int SubTDData(string PointID)
        {
            int inkNum = 0;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "SubTDData";
            ccp.ServerParams = new object[] { PointID };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            inkNum = Convert.ToInt32(ccp.ReturnObject.ToString());
            return inkNum;
        }

        //装载磅房信息到计量界面

        public void LoadBFData(DataTable dtBF, string PointType)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "LoadBFData";
            ccp.ServerParams = new object[] { PointType };
            dtBF.Clear();
            ccp.SourceDataTable = dtBF;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //从数据库获取磅房信息到预报界面
        public void GetBFData(ComboBox cb, string PointType)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryBFData";
            ccp.ServerParams = new object[] { PointType };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_POINTNAME";
                cb.ValueMember = "FS_POINTCODE";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }


        //从数据库获取磅房对应车号
        public void GetCHData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryCHData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_CARNO";
                cb.ValueMember = "FS_CARNO";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应检验员
        public void GetJYYData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryJYYData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_CHECKER";
                cb.ValueMember = "FS_CHECKER";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }


        //从数据库获取磅房对应颜色
        public void GetYSData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryYSData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_COLOR";
                cb.ValueMember = "FS_COLOR";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应合同
        public void GetHTData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryHTData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_CONTRACT";
                cb.ValueMember = "FS_CONTRACT";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应表面级别
        public void GetBMJBData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryBMJBData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_FACELEVEL";
                cb.ValueMember = "FS_FACELEVEL";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应表面状态
        public void GetBMZTData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryBMZTData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_FACESTATE";
                cb.ValueMember = "FS_FACESTATE";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应备注
        public void GetBZData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryBZData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_MEMO";
                cb.ValueMember = "FS_MEMO";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应产品标准
        public void GetCPBZData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryCPBZData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_STANDARD";
                cb.ValueMember = "FS_STANDARD";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应钢种
        public void GetGZData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryGZData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_STEELTYPE";
                cb.ValueMember = "FS_STEELTYPE";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应打印物料
        public void GetDYWLData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryDYWLData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_PrintMaterial";
                cb.ValueMember = "FS_PrintMaterial";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应规格
        public void GetGGData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryGGData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dt = new System.Data.DataTable();

            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);

                cb.DataSource = dt;
                cb.DisplayMember = "FS_SPEC";
                cb.ValueMember = "FS_SPEC";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dt;
            }
        }

        //从数据库获取磅房对应物料
        public void GetWLData(ComboBox cb, string PointID)
        {
            try
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
                ccp.MethodName = "QueryWLData";
                ccp.ServerParams = new object[] { PointID };

                System.Data.DataTable dtWL = new System.Data.DataTable();

                ccp.SourceDataTable = dtWL;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                if (dtWL.Rows.Count > 0)
                {
                    DataRow dr = dtWL.NewRow();
                    dtWL.Rows.InsertAt(dr, 0);

                    cb.DataSource = dtWL;
                    cb.DisplayMember = "FS_MATERIALNAME";
                    cb.ValueMember = "FS_MATERIALNO";

                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                else
                {
                    cb.DataSource = dtWL;
                }
            }
            catch (Exception e)
            {

            }
        }
        //从数据库获取磅房对应流向
        public void GetLXData(ComboBox cb, string PointID)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryLXData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dtLX = new System.Data.DataTable();

            ccp.SourceDataTable = dtLX;
            try
            {
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            }
            catch (Exception)
            {
            }

            if (dtLX.Rows.Count > 0)
            {
                DataRow dr = dtLX.NewRow();
                dtLX.Rows.InsertAt(dr, 0);

                cb.DataSource = dtLX;
                cb.DisplayMember = "FS_TYPENAME";
                cb.ValueMember = "FS_FLOW";

                cb.AutoCompleteMode = AutoCompleteMode.None;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtLX;
            }
        }
        //从数据库中获取磅房对应供应商
        public void GetGYDWData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryGYDWData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dtGYDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtGYDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtGYDW.Rows.Count > 0)
            {
                DataRow dr = dtGYDW.NewRow();
                dtGYDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtGYDW;
                cb.DisplayMember = "FS_SUPPLIERNAME";
                cb.ValueMember = "FS_SUPPLIER";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtGYDW;
            }
        }

        //从数据库中获取汽车衡磅房对应供应商
        public void GetSPDWData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QuerySPDWData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dtSPDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtSPDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtSPDW.Rows.Count > 0)
            {
                DataRow dr = dtSPDW.NewRow();
                dtSPDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtSPDW;
                cb.DisplayMember = "FS_PROVIDERNAME";
                cb.ValueMember = "FS_PROVIDER";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtSPDW;
            }
        }

        //从数据库中获取磅房对应收货方
        public void GetSHDWData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QuerySHDWData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dtSHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtSHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtSHDW.Rows.Count > 0)
            {
                DataRow dr = dtSHDW.NewRow();
                dtSHDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtSHDW;
                cb.DisplayMember = "FS_MEMO";
                cb.ValueMember = "FS_RECEIVER";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtSHDW;
            }
        }



        //从数据库中获取磅房对应发货地点
        public void GetSENDLOCATIONData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QuerySENDLOCATIONData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dtSHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtSHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtSHDW.Rows.Count > 0)
            {
                DataRow dr = dtSHDW.NewRow();
                dtSHDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtSHDW;
                cb.DisplayMember = "FS_LOCATIONNAME";
                cb.ValueMember = "FS_LOCATIONNAME";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtSHDW;
            }
        }

        //从数据库中获取磅房对应供应单位
        public void GetFHDWData(ComboBox cb, string PointID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QuerySCDWData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dtFHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtFHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtFHDW.Rows.Count > 0)
            {
                DataRow dr = dtFHDW.NewRow();
                dtFHDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtFHDW;
                cb.DisplayMember = "FS_MEMO";
                cb.ValueMember = "FS_SENDER";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtFHDW;
            }
        }



        // 从数据库获取承运单位数据    
        public void GetCYDWData(ComboBox cb, string PointID)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = "QueryCYDWData";
            ccp.ServerParams = new object[] { PointID };

            System.Data.DataTable dtCYDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtCYDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtCYDW.Rows.Count > 0)
            {
                DataRow dr = dtCYDW.NewRow();
                dtCYDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtCYDW;
                cb.DisplayMember = "FS_TRANSNAME";
                cb.ValueMember = "FS_TRANSNO";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtCYDW;
            }
        }

        // 更新磅房对应信息（无基础表的信息插入和更新次数，有基础信息表的更新次数）
        //PointID计量点编码；Value传入的值；Method服务端调用的方法
        /*服务端调用的方法对应如下：
         * SetBMJBData 表面级别
         * SetBMZTData 表面状态
         * SetBZData   备注
         * SetCHData   车号
         * SetCPBZData 产品标准
         * SetGGData   规格
         * SetGZData   钢种
         * SetHTData   合同
         * SetJJYData  检验员
         * SetYSData   颜色
         * SetWLData   物料（代码）
         * SetSHDWData 收货单位（代码）
         * SetFHDWData 发货单位（代码）
         * SetGYDWData 供应单位（代码）
         * SetCYDWData 承运单位（代码）
         * SetDYWLData 打印物料
         */

        public void SetNonBaseData(string PointID, string Value, string Method)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = Method;
            ccp.ServerParams = new object[] { PointID, Value };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

        }

        // 插入磅房对应信息（有基础表的信息）
        //PointID计量点编码；Value传入的值；Method服务端调用的方法；返回为获取的计量系统代码
        /*服务端调用的方法对应如下：
         * SaveCYDWData 承运单位
         * SaveFHDWData 生产单位
         * SaveGYDWData   供应单位
         * SaveSHDWData   收货单位
         * SaveWLData     物料
         */
        public string InsertBaseData(string PointID, string Value, string Method)
        {

            string reVal = "";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.QueryPointInfo";
            ccp.MethodName = Method;
            ccp.ServerParams = new object[] { PointID, Value, "SGLR" };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnObject != null)
                reVal = ccp.ReturnObject.ToString();
            return reVal;
        }

        #region 传两个计量点ID（毛重计量点ID，皮重计量点ID）
        //从数据库获取磅房对应物料
        public void GetWLData(ComboBox cb, string PointMZID, string PointPZID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.GetBaseInfo";
            ccp.MethodName = "QueryWLIDData";
            ccp.ServerParams = new object[] { PointMZID, PointPZID };

            System.Data.DataTable dtWL = new System.Data.DataTable();

            ccp.SourceDataTable = dtWL;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtWL.Rows.Count > 0)
            {
                DataRow dr = dtWL.NewRow();
                dtWL.Rows.InsertAt(dr, 0);

                cb.DataSource = dtWL;
                cb.DisplayMember = "FS_MATERIALNAME";
                cb.ValueMember = "FS_MATERIALNO";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtWL;
            }
        }
        //从数据库中获取磅房对应发货方
        public void GetFHDWData(ComboBox cb, string PointMZID, string PointPZID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.GetBaseInfo";
            ccp.MethodName = "QueryFHDWIDData";
            ccp.ServerParams = new object[] { PointMZID, PointPZID };

            System.Data.DataTable dtFHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtFHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtFHDW.Rows.Count > 0)
            {
                DataRow dr = dtFHDW.NewRow();
                dtFHDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtFHDW;
                cb.DisplayMember = "FS_SUPPLIERNAME";
                cb.ValueMember = "FS_SUPPLIER";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtFHDW;
            }
        }
        //从数据库中获取磅房对应收货方
        public void GetSHDWData(ComboBox cb, string PointMZID, string PointPZID)
        {

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.GetBaseInfo";
            ccp.MethodName = "QuerySHDWIDData";
            ccp.ServerParams = new object[] { PointMZID, PointPZID };

            System.Data.DataTable dtSHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtSHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtSHDW.Rows.Count > 0)
            {
                DataRow dr = dtSHDW.NewRow();
                dtSHDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtSHDW;
                cb.DisplayMember = "FS_MEMO";
                cb.ValueMember = "FS_RECEIVER";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtSHDW;
            }
        }
        // 从数据库获取承运单位数据    
        public void GetCYDWData(ComboBox cb, string PointMZID, string PointPZID)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.GetBaseInfo";
            ccp.MethodName = "QueryCYDWIDData";
            ccp.ServerParams = new object[] { PointMZID, PointPZID };

            System.Data.DataTable dtCYDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtCYDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtCYDW.Rows.Count > 0)
            {
                DataRow dr = dtCYDW.NewRow();
                dtCYDW.Rows.InsertAt(dr, 0);

                cb.DataSource = dtCYDW;
                cb.DisplayMember = "FS_TRANSNAME";
                cb.ValueMember = "FS_TRANSNO";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtCYDW;
            }
        }
        //从数据库获取磅房对应流向
        public void GetLXData(ComboBox cb, string PointMZID, string PointPZID)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.GetBaseInfo";
            ccp.MethodName = "QueryLXIDData";
            ccp.ServerParams = new object[] { PointMZID, PointPZID };

            System.Data.DataTable dtLX = new System.Data.DataTable();

            ccp.SourceDataTable = dtLX;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtLX.Rows.Count > 0)
            {
                DataRow dr = dtLX.NewRow();
                dtLX.Rows.InsertAt(dr, 0);

                cb.DataSource = dtLX;
                cb.DisplayMember = "FS_TYPENAME";
                cb.ValueMember = "FS_FLOW";

                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cb.DataSource = dtLX;
            }
        }
        #endregion
    }
}
