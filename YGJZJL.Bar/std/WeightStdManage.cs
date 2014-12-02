using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreFS.CA06;

namespace YGJZJL.Bar.std
{
    class WeightStdManage : FrmBase
    {
        private DataTable _dt = null;

        public WeightStdManage(OpeBase op)
        {
            this.ob = op;
            _dt = GetWeightStd();
        }

        public ProdutionWeightStd GetStandard(string spec, string length)
        {
            ProdutionWeightStd pws = null;
            DataRow[] drs = _dt.Select("FS_SPEC ='" + spec + "' and FN_LENGTH = '"+length+"'");
            if(drs.Length > 0)
            {
                try
                {
                    DataRow dr = drs[0];
                    pws = new ProdutionWeightStd();
                    pws.SeqNo = int.Parse(dr["FN_ID"].ToString());
                    pws.Spec = double.Parse(dr["FS_SPEC"].ToString());
                    pws.Lenght = double.Parse(dr["FN_LENGTH"].ToString());
                    pws.MinWeight = double.Parse(dr["FS_BUNDLEMINWWEIGHT"].ToString());
                    pws.MaxWeight = double.Parse(dr["FS_BUNDLEMAXWWEIGHT"].ToString()); 
                }
                catch (Exception ex)
                {

                }
            }
            return pws;
        }

        private DataTable GetWeightStd()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT T.FN_ID,T.FS_SPEC,T.FN_LENGTH,T.FN_WEIGHT,T.FN_BILLETCOUNT,T.FS_BUNDLEWEIGHT
                               ,T.FS_BUNDLEMINWWEIGHT,T.FS_BUNDLEMAXWWEIGHT,T.FS_MINDIFFERENTRATE,T.FS_MAXDIFFERENTRATE 
                               FROM BT_BCTHEORYWEIGHTINFO T";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { sql };
                ccp.SourceDataTable = dt;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            catch (Exception e)
            {
            }
            return dt;
        }
    }
}
