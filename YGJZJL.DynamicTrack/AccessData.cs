using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using CoreFS.CA06;

namespace YGJZJL.DynamicTrack
{
    public class AccessData : FrmBase
    {
        #region 定义基本对象
        protected static OleDbConnection conn = new OleDbConnection();
        protected static OleDbCommand comm = new OleDbCommand();
        protected static OleDbDataAdapter conAdt = new OleDbDataAdapter();
        protected static string strDataAddress = "";

        /// <summary>
        /// 连接测试(MessageBox消息)
        /// </summary>
        public  void Test()
        {
            try
            {
                OpenConn();
                //MessageBox.Show("    连接成功！    ");

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            finally
            { CloseConn(); }
        }

        /// <summary>
        /// 是否连接到当前数据库
        /// </summary>
        /// <returns></returns>
        public  bool isConn()
        {
            try
            {
                OpenConn();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            { 
                CloseConn(); 
            }
        }

        #endregion

        public AccessData(object obb)
        {
            this.ob = (OpeBase)obb;
           
        }
     
        #region 获得连接字符串
        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        private static string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\";
        
        //"Server=(local);DataBase=db_17;Uid=sa;Pwd=";


        /// <summary>
        /// 获得或者设置连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }

            set
            {
                connectionString = value;
            }
        }


      
        #endregion



        #region 基本动作
        /// <summary>
        /// 开启连接
        /// </summary>
        private  void OpenConn()
        {


             getip();
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = ConnectionString + strDataAddress + "\\cz\\database\\GDH1.mdb";
                comm.Connection = conn;
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                { throw new Exception(e.Message); }

            }

        }
        
        /// <summary>
        /// 关闭连接
        /// </summary>
        private  void CloseConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                comm.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// 执行SQL语句并得到执行相关结果 (用于执行及测试)
        /// </summary>
        /// <param name="SQL"></param>
        public  void excuteSql(string SQL)
        {
            try
            {

                OpenConn();
                comm.CommandType = CommandType.Text;
                comm.CommandText = SQL;
                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
            }
            finally
            { CloseConn(); }
        }

        /// <summary>
        /// 执行SQL语句并得到执行是否成功的布尔值
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public  bool Run(string SQL)
        {
            try
            {
                OpenConn();
                comm.CommandType = CommandType.Text;
                comm.CommandText = SQL;
                comm.ExecuteNonQuery();
                return true;

            }
            catch
            {
                return false;
                //throw new Exception ( e.Message );
            }
            finally
            {
                CloseConn();
            }
        }

        public  void CommQuery(string SQL,DataSet ds)
        {
            try
            {
                OpenConn();
                comm.CommandType = CommandType.Text;
                comm.CommandText = SQL;
                conAdt.SelectCommand = comm;
                conAdt.Fill(ds);
            }
            catch(Exception e)
            {

            }
            finally
            {
                CloseConn();
            }
        }

        public void getip()
          {
              DataTable dt = new DataTable();
              string strSql = "select t.fs_meterpara from BT_POINT T WHERE T.Fs_Pointtype='DG'";

              CoreClientParam ccp = new CoreClientParam();
              ccp.ServerName = "ygjzjl.base.QueryData";
              ccp.MethodName = "queryByClientSql";
              ccp.ServerParams = new object[] { strSql };
              ccp.SourceDataTable = dt;
              this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
              if (dt.Rows.Count > 0)
              {
                  strDataAddress = dt.Rows[0][0].ToString();
              }

          }
    }

}
