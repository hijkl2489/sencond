using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreFS.CA06;
using Core.Sip.Client.Meas;

namespace YGJZJL.PublicComponent
{
    public class WeighDevice :FrmBase
    {
        public WeighDevice(OpeBase op)
        {
            this.ob = op;
        }

        /// <summary>
        /// 获取计量点设备信息
        /// </summary>
        /// <param name="cabinetCode">计量点编号</param>
        /// <returns>计量点设备组</returns>
        public BTDevice[] GetDevice(string cabinetCode)
        {
            BTDevice []devices = null;
            DataTable dt = new DataTable();
            ArrayList param = new ArrayList();
            param.Add(cabinetCode);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHDEVICE_01.SELECT", param };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                devices = new BTDevice[dt.Rows.Count];
                DataRow dr = null ;
                for (int i = 0; i < devices.Length; i++)
                {
                    dr = dt.Rows[i];
                    devices[i] = ConvertDataRowToDeviceObject(dr);
                }
            }
            return devices;
        }

        /// <summary>
        /// 将DataRow转换成BTDEVICE
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private BTDevice ConvertDataRowToDeviceObject(DataRow dr)
        {
            BTDevice device = null;

            if (dr != null)
            {
                try
                {
                    device = new BTDevice();

                    device.FS_CODE = dr["FS_CODE"].ToString();   //设备编码
                    device.FS_NAME = dr["FS_NAME"].ToString(); //设备名称
                    device.FS_TYPE = dr["DEVICE_TYPE"].ToString();//设备类型
                    device.FS_PARAM = dr["FS_PARAM"].ToString();//设备参数
                    device.FS_DESC = dr["FS_DESC"].ToString();   //设备描述
                }
                catch (Exception e)
                {
                }
            }

            return device;
        }
    }
}
