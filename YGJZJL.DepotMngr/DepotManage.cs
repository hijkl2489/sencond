using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.DepotMngr
{
    public class DepotManage
    {
        //SQL语句
        private const string _isql = @"insert into ztj_theorytotrue(batch,psting_date,weight,p_plant,theoryweight,barcode,MATERIAL)
                                      values('{0}',getdate(),{1},'1100',{2},'{3}','{4}')";
        private const string _usql = @"update ztj_theorytotrue set material = '{0}' where batch like  '%{1}%'";

        private const string _uwsql = @"update ztj_theorytotrue set weight = '{0}', theoryweight='{1}' where batch =  '{2}'";
        //连接字符串
        //private const string _connstr = @"Provider=SQLOLEDB;Server=10.32.200.2;Database=L3_DepotMngr;uid=sa;pwd=prodepote;";
        private const string _connstr = @"Provider=SQLOLEDB;Server=10.2.1.23;Database=L3_DepotMngr;uid=sa;pwd=prodepote;";

        public bool UploadDepot(DepotObject depotObject)
        {
            SqlAdapter.ConnectionString = _connstr;
            return SqlAdapter.Run(string.Format(_isql, new object[] { depotObject.BatchNo + depotObject.BatchIndex, depotObject.Weight, depotObject.TheoryWeight, depotObject.Barcode, depotObject.Material }));
        }

        public bool UpdateMaterial(string batch, string material)
        {
            SqlAdapter.ConnectionString = _connstr;
            return SqlAdapter.Run(string.Format(_usql, material, batch));
        }

        public bool UpWeight(DepotObject depotObject)
        {
            SqlAdapter.ConnectionString = _connstr;
            return SqlAdapter.Run(string.Format(_uwsql, depotObject.Weight, depotObject.TheoryWeight, depotObject.BatchNo + depotObject.BatchIndex));
        }
        #region 获取条码
        /// <summary>
        /// 条码编码数据 402 + 轧编号 + 吊号
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="batchIndex"></param>
        /// <returns></returns>
        private string GetBarCode(string batchNo, string batchIndex)
        {
            if (string.IsNullOrEmpty(batchNo) || string.IsNullOrEmpty(batchIndex))
            {
                return null;
            }
            batchNo = batchNo.Trim().Insert(1, "1");
            batchIndex = batchIndex.Trim();
            batchIndex = batchIndex.Length == 1 ? ("0" + batchIndex) : batchIndex.Length == 0 ? "01" : batchIndex;
            return (batchNo.StartsWith("N") ? "402" : batchNo.StartsWith("G") ? "405" : "402") + batchNo.Trim() + batchIndex;
        }
        #endregion

        public static void Main()
        {
            //DepotObject depot = new DepotObject();
            //depot.BatchNo = "G9999999";
            //depot.BatchIndex = "1";
            //depot.Weight = "2176";
            //depot.TheoryWeight = "2172";

            //DepotManage dm = new DepotManage();
            //bool flag = dm.UploadDepot(depot);
            //Console.Out.WriteLine(flag.ToString());
            //Console.In.Read();
        }
    }
}
