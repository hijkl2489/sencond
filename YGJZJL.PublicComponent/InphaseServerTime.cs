using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

class Win32API
{
    [DllImport("kernel32.dll ")]
    public static extern int SetLocalTime(ref   SystemTime lpSystemTime);
}

public struct SystemTime
{
    public short wYear;
    public short wMonth;
    public short wDayOfWeek;
    public short wDay;
    public short wHour;
    public short wMinute;
    public short wSecond;
    public short wMilliseconds;
}

namespace YGJZJL.PublicComponent
{
    public class InphaseServerTime : System.Windows.Forms.Form
    {
        public const string strConAll = "Provider= OraOLEDB.Oracle;Data Source = jzjl7;User Id=kgdata;Password=test";

        #region   进入系统之前检查时间
        public int Check_Before_Login()
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(strConAll);
                string selectcmd = "select sysdate from dual";

                OleDbCommand comm = new OleDbCommand(selectcmd, conn);

                OleDbDataAdapter da = new OleDbDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //System.DateTime SQLServer_time = (System.DateTime)comm.ExecuteScalar();
                System.DateTime SQLServer_time = (System.DateTime)ds.Tables[0].Rows[0][0];
                conn.Close();


                //SqlConnection conn = new SqlConnection(strConAll);
                //conn.Open();
                //SqlCommand scmd = new SqlCommand(selectcmd, conn);
                //System.DateTime SQLServer_time = (System.DateTime)scmd.ExecuteScalar();
                //conn.Close();

                //根据得到的时间日期，来定义时间、日期 
                SystemTime st = new SystemTime();
                st.wYear = (short)SQLServer_time.Year;
                st.wDay = (short)SQLServer_time.Day;
                st.wMonth = (short)SQLServer_time.Month;
                st.wHour = (short)SQLServer_time.Hour;
                st.wMinute = (short)SQLServer_time.Minute;
                st.wSecond = (short)SQLServer_time.Second;
                //修改本地端的时间和日期 
                Win32API.SetLocalTime(ref st);
                return 1;

            }
            catch
            {
                MessageBox.Show("服务器可能没启动，请检查以后重新进入！ ", "错误 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
                //Application.Exit(); 
            }
        }
        #endregion 

    }

}
