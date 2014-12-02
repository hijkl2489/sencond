using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace YGJZJL.DepotMngr
{
    class SqlAdapter
    {
        #region 定义基本对象
        protected static OleDbConnection conn = new OleDbConnection();
        protected static OleDbCommand comm = new OleDbCommand();
        protected static OleDbDataAdapter conAdt = new OleDbDataAdapter();


        /// <summary>
        /// 连接测试(MessageBox消息)
        /// </summary>
        public static void Test()
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
        public static bool isConn()
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

        #region 获得连接字符串
        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        private static string connectionString = @"";
        //@"Provider=SQLOLEDB;Server=10.25.3.234;Database=gcp;uid=admin;pwd=888888"


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


        public SqlAdapter()
        {

        }
        #endregion

        #region 基本动作
        /// <summary>
        /// 开启连接
        /// </summary>
        private static void OpenConn()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = ConnectionString;
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
        private static void CloseConn()
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
        public static void excuteSql(string SQL)
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
        public static bool Run(string SQL)
        {
            try
            {
                OpenConn();
                comm.CommandType = CommandType.Text;
                comm.CommandText = SQL;
                comm.ExecuteNonQuery();
                return true;

            }
            catch(Exception ex)
            {
                return false;
                //throw new Exception ( e.Message );
            }
            finally
            {
                CloseConn();
            }
        }

        public static void CommQuery(string SQL,DataSet ds)
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
    }
}
