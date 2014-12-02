using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using CoreFS.CA06;

namespace YGJZJL.Car
{
    public abstract class DataMamage : FrmBase
    {
        private string _ip4 = "";

        public string IP4
        {
          get { return _ip4; }
        }

        private string _ip6 = "";

        public string IP6
        {
            get { return _ip6; }
        }

        public DataMamage(OpeBase op)
        {
            this.ob = op;
            getIp();
        }

        private void getIp()
        {
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in IpEntry.AddressList)
            {
                if(ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    _ip4 = ip.ToString();
                }
                else if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    _ip6 = ip.ToString();
                }
            }
        }

        /// <summary>
        /// 通用查询方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected bool CommQuery(DataTable dt,string sql,ArrayList param)
        {
            if (param == null)
            {
                param = new ArrayList();
            }
            dt.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "queryBySql";
            ccp.ServerParams = new object[] { sql, param };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp,CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通用保存方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected CoreClientParam Save(string sql, ArrayList param)
        {
            if (param == null)
            {
                param = new ArrayList();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "saveBySql";
            ccp.ServerParams = new object[] { sql, param };
            ccp = this.ExecuteNonQuery(ccp,CoreInvokeType.Internal);
            return ccp;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected CoreClientParam excuteProcedure(string sql, Hashtable param)
        {
            if (param == null)
            {
                param = new Hashtable();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "executeProcedureBySql";
            ccp.ServerParams = new object[] { sql, param };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            return ccp;
        }

        /// <summary>
        /// 执行存储过程2
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected CoreClientParam excuteProcedure2(string sql, Hashtable param)
        {
            if (param == null)
            {
                param = new Hashtable();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "executeProcedureBySql2";
            ccp.ServerParams = new object[] { sql, param };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            return ccp;
        }
    }
}
