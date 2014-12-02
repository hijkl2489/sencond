using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreFS.CA06;
using Core.Sip.Client.App;

namespace YGJZJL.PublicComponent
{
    public class WeighPoint : FrmBase
    {
        public WeighPoint(OpeBase op)
        {
            this.ob = op;
        }
        /// <summary>
        /// 获取计量点信息
        /// </summary>
        /// <param name="pointCode">计量点编号</param>
        /// <returns>计量点实体类</returns>
        public BT_POINT GetPoint(string pointCode)
        {
            BT_POINT point = null;
            DataTable dt = new DataTable();
            ArrayList param = new ArrayList();
            param.Add(pointCode);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHPOINT_01.SELECT", param };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                point = ConvertDataRowToPointObject(dr);
            }
            return point;
        }

        /// <summary>
        /// 获取计量点信息
        /// </summary>
        /// <param name="pointType">计量点类型</param>
        /// <returns>计量点数组</returns>
        public BT_POINT[] GetPoints(string pointType)
        {
            BT_POINT[] points = null;
            DataTable dt = new DataTable();
            ArrayList param = new ArrayList();
            param.Add(pointType);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHPOINT_02.SELECT", param };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                points = new BT_POINT[dt.Rows.Count];
                BT_POINT point = null;
                DataRow dr = null;
                for (int i = 0;i < dt.Rows.Count;i++)
                {
                    dr = dt.Rows[i];
                    points[i] = ConvertDataRowToPointObject(dr);
                }
                
            }
            return points;
        }

        /// <summary>
        /// 将DataRow转换成BT_POINT
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private BT_POINT ConvertDataRowToPointObject(DataRow dr)
        {
            BT_POINT point = null;

            if (dr != null)
            {
                try
                {
                    point = new BT_POINT();

                    point.FS_POINTCODE = dr["FS_POINTCODE"].ToString();   //计量点编码
                    point.FS_POINTNAME = dr["FS_POINTNAME"].ToString(); //计量点名称
                    point.FS_POINTDEPART = dr["FS_POINTDEPART"].ToString();//计量点所属工厂代码,只能是内部单位，参考 IT_Factory工厂基础表
                    point.FS_POINTTYPE = dr["FS_POINTTYPE"].ToString();

                    point.FS_VIEDOIP = dr["FS_VIEDOIP"].ToString();   //硬盘录像机IP
                    point.FS_VIEDOPORT = dr["FS_VIEDOPORT"].ToString(); //硬盘录像机端口
                    point.FS_VIEDOUSER = dr["FS_VIEDOUSER"].ToString();  //硬盘录像机用户名
                    point.FS_VIEDOPWD = dr["FS_VIEDOPWD"].ToString();  //硬盘录像机密码
                    point.FS_METERTYPE = dr["FS_METERTYPE"].ToString(); //仪表类型
                    point.FS_METERPARA = dr["FS_METERPARA"].ToString(); //仪表参数
                    point.FS_MOXAIP = dr["FS_MOXAIP"].ToString();    //MOXA卡IP
                    point.FS_MOXAPORT = dr["FS_MOXAPORT"].ToString();   //仪表MOXA端口
                    point.FS_RTUIP = dr["FS_RTUIP"].ToString();      //RTUIP
                    point.FS_RTUPORT = dr["FS_RTUPORT"].ToString();     //RTU端口
                    point.FS_PRINTERIP = dr["FS_PRINTERIP"].ToString();     //打印服务器IP
                    point.FS_PRINTERNAME = dr["FS_PRINTERNAME"].ToString();   //打印机名称
                    point.FS_PRINTTYPECODE = dr["FS_PRINTTYPECODE"].ToString();  //打印机类型代码
                    point.FN_USEDPRINTPAPER = dr["FN_USEDPRINTPAPER"].ToString();//剩余纸张量
                    point.FN_USEDPRINTINK = dr["FN_USEDPRINTINK"].ToString(); //剩余碳带量
                    point.FS_LEDIP = dr["FS_LEDIP"].ToString(); //LED屏IP
                    point.FS_LEDPORT = dr["FS_LEDPORT"].ToString();//LED屏端口
                    point.FN_VALUE = dr["FN_VALUE"].ToString();//复位值
                    point.FS_ALLOWOTHERTARE = dr["FS_ALLOWOTHERTARE"].ToString();//允许异地去皮
                    point.FS_SIGN = dr["FS_SIGN"].ToString();//计量点标志(1:为已被选择,0:没选择)
                    point.FS_DISPLAYPORT = dr["FS_DISPLAYPORT"].ToString();//液晶屏MOXA端口
                    point.FS_DISPLAYPARA = dr["FS_DISPLAYPARA"].ToString();//液晶屏MOXA参数
                    point.FS_READERPORT = dr["FS_READERPORT"].ToString();//读卡器MOXA端口
                    point.FS_READERPARA = dr["FS_READERPARA"].ToString();//读卡器MOXA参数
                    point.FS_READERTYPE = dr["FS_READERTYPE"].ToString();//读卡器类型
                    point.FS_DISPLAYTYPE = dr["FS_DISPLAYTYPE"].ToString();//液晶屏类型
                    point.FS_LEDTYPE = dr["FS_LEDTYPE"].ToString(); //LED类型
                    point.FF_CLEARVALUE = dr["FF_CLEARVALUE"].ToString(); //清零值（差值）
                    point.FS_POINTSTATE = dr["FS_POINTSTATE"].ToString();
                }
                catch (Exception e)
                {
                }
            }

            return point;
        }
    }
}
