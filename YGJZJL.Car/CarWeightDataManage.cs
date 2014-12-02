using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using CoreFS.CA06;
using YGJZJL.CarSip.Client.App;

namespace YGJZJL.Car
{
    public class CarWeightDataManage : DataMamage
    {
        public CarWeightDataManage(OpeBase op) : base(op)
        {

        }

        /// <summary>
        /// 查询计量点信息
        /// </summary>
        /// <returns>DataTable</returns>
        public bool GetWeightPoints(DataTable dt)
        {
            string sql = @"SELECT 'FALSE' AS XZ,
                           T.FS_POINTCODE,
                           T.FS_POINTNAME,
                           T.FS_POINTDEPART,
                           T.FS_POINTTYPE,
                           T.FS_VIEDOIP,
                           T.FS_VIEDOPORT,
                           T.FS_VIEDOUSER,
                           T.FS_VIEDOPWD,
                           T.FS_METERTYPE,
                           T.FS_METERPARA,
                           T.FS_MOXAIP,
                           T.FS_MOXAPORT,
                           T.FS_RTUIP,
                           T.FS_RTUPORT,
                           T.FS_PRINTERIP,
                           T.FS_PRINTERNAME,
                           T.FS_PRINTTYPECODE,
                           T.FN_USEDPRINTPAPER,
                           T.FN_USEDPRINTINK,
                           T.FS_LEDIP,
                           T.FS_LEDPORT,
                           T.FN_VALUE,
                           T.FS_ALLOWOTHERTARE,
                           T.FS_SIGN,
                           T.FS_DISPLAYPORT,
                           T.FS_DISPLAYPARA,
                           T.FS_READERPORT,
                           T.FS_READERPARA,
                           T.FS_READERTYPE,
                           T.FS_DISPLAYTYPE,
                           T.FF_CLEARVALUE,
                           T.FS_POINTSTATE,
                           F.FS_IP,nvl(F.FN_POINTFLAG,0) FN_POINTFLAG
                      FROM BT_POINT T,BT_POINTFLAG F
                     WHERE T.FS_POINTCODE = F.FS_POINTCODE(+)
                       AND T.FS_POINTTYPE = 'QC'
                     ORDER BY T.FS_POINTCODE
                            ";
            return CommQuery(dt,sql, null);
        }

        /// <summary>
        /// 获取物料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetMaterial(DataTable dt)
        {
            string sql = @"select A.FS_PointNo,
                                   A.FS_MATERIALNO,
                                   b.fs_materialname,
                                   b.FS_HELPCODE,
                                   a.fn_times
                              from Bt_Pointmaterial A, It_Material B, Bt_Point C
                             where A.Fs_pointno = C.Fs_Pointcode
                               and A.Fs_Materialno = B.Fs_Wl
                               and C.Fs_Pointtype = 'QC'
                             order by a.fn_times desc
                            ";
            return CommQuery(dt,sql,null);
        }

        /// <summary>
        /// 获取收货单位
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetReceiver(DataTable dt)
        {
            string strSql = "select A.FS_PointNo, A.FS_Receiver, b.fs_memo, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointReceiver A, It_Store B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_Receiver = B.Fs_SH and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";

            return CommQuery(dt, strSql, null);
        }

        /// <summary>
        /// 获取发货单位
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetSender(DataTable dt)
        {
            string strSql = "select A.FS_PointNo, A.FS_SUPPLIER, b.FS_SUPPLIERNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_Pointsupplier A, IT_SUPPLIER B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_SUPPLIER = B.Fs_GY and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";

            return CommQuery(dt, strSql, null);
        }

        /// <summary>
        /// 获取承运单位
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetTrans(DataTable dt)
        {
            string strSql = "select A.FS_PointNo, A.FS_TransNo, b.FS_TRANSNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointTrans A, BT_Trans B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_TransNo = B.Fs_CY and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";

            return CommQuery(dt, strSql, null);
        }

        /// <summary>
        /// 获取车号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetCarNo(DataTable dt)
        {
            string strSql = "select FS_POINTNO, FS_CARNO, FN_TIMES From BT_POINTCARNO order by FN_TIMES desc ";
            return CommQuery(dt, strSql, null);
        }

        /// <summary>
        /// 获取流向
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetFlow(DataTable dt)
        {
            string strSql = "select FS_TYPECODE, FS_TYPENAME From BT_WEIGHTTYPE order by FS_TYPECODE ";
            return CommQuery(dt, strSql, null);
        }

        /// <summary>
        /// 获取汽车衡声音文件
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetVoiceData(DataTable dt)
        {
            string strSql = "select t.fs_voicename,t.fs_voicefile,t.FS_INSTRTYPE,t.fs_memo from bt_voice t where t.FS_INSTRTYPE = 'QC'";
            return CommQuery(dt, strSql, null);
        }

        /// <summary>
        /// 接管计量点
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool BandPoints(ArrayList param)
        {
            string sql = "update BT_POINTFLAG set FN_POINTFLAG=1,FS_IP='{0}' Where FS_POINTCODE='{1}'";
            foreach (object obj in param)
            {
                Save(string.Format(sql, IP4, obj.ToString()), null);
            }
            return true;
        }

        /// <summary>
        /// 取消接管
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool ClosePoints(ArrayList param)
        {
            string sql = "update BT_POINTFLAG set FN_POINTFLAG=0,FS_IP='' Where FS_POINTCODE='{0}'";
            foreach (object obj in param)
            {
                Save(string.Format(sql, obj.ToString()), null);
            }
            return true;
        }

        /// <summary>
        /// 如果优先使用预报未勾选，则先查询一次计量记录否则先查询预报
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardNo"></param>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public bool GetTransPlan(DataTable dt,string cardNo, string carNo,bool isYBFirst)
        {
            if (isYBFirst)
            {
                string sql = @"select FS_PlanCode,
                                                   FS_CARDNUMBER,
                                                   FS_CARNO,
                                                   FS_ContractNo,
                                                   FS_ContractItem,
                                                   FS_Sender,
                                                   FHDW_BHTOMC(FS_Sender) FS_FHDW,
                                                   Provider_BHTOMC(FS_Provider) FS_Provider,
                                                   FS_DRIVERREMARK,
                                                   FS_SenderStore,
                                                   FS_Material,
                                                   WL_BHTOMC(FS_Material) FS_MaterialName,
                                                   FS_ReceiverFactory,
                                                   SHDW_BHTOMC(FS_ReceiverFactory) FS_SHDW,
                                                   FS_ReceiverStore,
                                                   FS_TransNo,
                                                   CYDW_BHTOMC(FS_TransNo) FS_CYDW,
                                                   FS_WeightType,
                                                   LX_BHTOMC(FS_WeightType) FS_LX,
                                                   FS_PoundType,
                                                   to_char(FS_OverTime, 'yyyy-MM-dd HH24:mi:ss') as FS_OverTime,
                                                   FN_SendGrossWeight,
                                                   FN_SendTareWeight,
                                                   FN_SendNetWeight,
                                                   FS_StoveNo,
                                                   FN_BilletCount,
                                                   FS_Person,
                                                   FS_Point,
                                                   to_char(FS_Datetime, 'yyyy-MM-dd HH24:mi:ss') as FS_Datetime,
                                                   FS_Shift,
                                                   FS_Term,
                                                   FS_Status,
                                                   FN_Times,
                                                   FN_WeightTimes,
                                                   FS_PlanUser,
                                                   FS_PlanTel,
                                                   FS_Level,
                                                   FS_IFSAMPLING,
                                                   FS_IFACCEPT,
                                                   FS_DRIVERNAME,
                                                   FS_DRIVERIDCARD
                                              from DT_WeightPlan T
                                             where FS_Datetime =
                                                   (select max(B.FS_Datetime) from DT_WeightPlan B where nvl(FS_STATUS,'0') = '0'
                                                    {0}
                                                    ) {1}
                                             order by FS_Datetime
                                            ";
                string condition1 = "";
                string condition2 = "";
                if (!string.IsNullOrEmpty(cardNo))
                {
                    condition1 += " and B.FS_CARDNUMBER ='" + cardNo + "'";
                    condition2 += " and T.FS_CARDNUMBER ='" + cardNo + "'";
                }
                if (!string.IsNullOrEmpty(carNo))
                {
                    condition1 += " and B.FS_CARNO ='" + carNo + "'";
                    condition2 += " and T.FS_CARNO ='" + carNo + "'";
                }

                //无预报查询一次计量记录
                if (!CommQuery(dt, string.Format(sql, condition1, condition2), null) || dt.Rows.Count == 0)
                {
                    sql = @"SELECT T.FS_PLANCODE,
                                   T.FS_CARDNUMBER,
                                   T.FS_CARNO,
                                   T.FS_CONTRACTNO,
                                   T.FS_CONTRACTITEM,
                                   T.FS_SENDER,
                                   FHDW_BHTOMC(T.FS_SENDER) FS_FHDW,
                                   PROVIDER_BHTOMC(T.FS_PROVIDER) FS_PROVIDER,
                                   T.FS_SENDERSTORE,
                                   T.FS_MATERIAL,
                                   WL_BHTOMC(T.FS_MATERIAL) FS_MATERIALNAME,
                                   T.FS_RECEIVER FS_RECEIVERFACTORY,
                                   SHDW_BHTOMC(T.FS_RECEIVER) FS_SHDW,
                                   T.FS_RECEIVERSTORE,
                                   T.FS_TRANSNO,
                                   CYDW_BHTOMC(T.FS_TRANSNO) FS_CYDW,
                                   T.FS_WEIGHTTYPE,
                                   LX_BHTOMC(T.FS_WEIGHTTYPE) FS_LX,
                                   T.FS_POUNDTYPE,
                                   T.FN_SENDGROSSWEIGHT,
                                   T.FN_SENDTAREWEIGHT,
                                   T.FN_SENDNETWEIGHT,
                                   T.FS_STOVENO,
                                   TO_CHAR(T.FD_WEIGHTTIME, 'YYYY-MM-DD HH24:MI:SS') AS FS_DATETIME,
                                   P.FS_LEVEL,
                                   T.FS_IFSAMPLING,
                                   T.FS_IFACCEPT,
                                   T.FS_DRIVERNAME,
                                   T.FS_DRIVERIDCARD,
                                   T.FS_STOVENO1 FS_STOVENO,
                                   T.FN_COUNT1 FN_BILLETCOUNT,
                                   T.FS_STOVENO2,
                                   T.FN_COUNT2,
                                   T.FS_STOVENO3,
                                   T.FN_COUNT3,
                                   T.FN_SENDGROSSWEIGHT FS_DFMZ,
                                   T.FN_SENDTAREWEIGHT FS_DFPZ,
                                   T.FN_SENDNETWEIGHT FS_DFJZ
                              FROM DT_FIRSTCARWEIGHT T, DT_WEIGHTPLAN P
                             WHERE T.FS_PLANCODE = P.FS_PLANCODE(+)
                               AND FD_WEIGHTTIME = (SELECT MAX(B.FD_WEIGHTTIME)
                                                      FROM DT_FIRSTCARWEIGHT B
                                                     WHERE NVL(B.FS_FALG, '0') = '0'
                                                       {0})
                               {1}
                             ORDER BY FS_DATETIME";
                    return CommQuery(dt, string.Format(sql, condition1, condition2), null);
                }
                else
                {
                    return true;
                }
            }
            else
            {

                string sql = @"SELECT T.FS_PLANCODE,
                                   T.FS_CARDNUMBER,
                                   T.FS_CARNO,
                                   T.FS_CONTRACTNO,
                                   T.FS_CONTRACTITEM,
                                   T.FS_SENDER,
                                   FHDW_BHTOMC(T.FS_SENDER) FS_FHDW,
                                   PROVIDER_BHTOMC(T.FS_PROVIDER) FS_PROVIDER,
                                   T.FS_SENDERSTORE,
                                   T.FS_MATERIAL,
                                   WL_BHTOMC(T.FS_MATERIAL) FS_MATERIALNAME,
                                   T.FS_RECEIVER FS_RECEIVERFACTORY,
                                   SHDW_BHTOMC(T.FS_RECEIVER) FS_SHDW,
                                   T.FS_RECEIVERSTORE,
                                   T.FS_TRANSNO,
                                   CYDW_BHTOMC(T.FS_TRANSNO) FS_CYDW,
                                   T.FS_WEIGHTTYPE,
                                   LX_BHTOMC(T.FS_WEIGHTTYPE) FS_LX,
                                   T.FS_POUNDTYPE,
                                   T.FN_SENDGROSSWEIGHT,
                                   T.FN_SENDTAREWEIGHT,
                                   T.FN_SENDNETWEIGHT,
                                   T.FS_STOVENO,
                                   TO_CHAR(T.FD_WEIGHTTIME, 'YYYY-MM-DD HH24:MI:SS') AS FS_DATETIME,
                                   P.FS_LEVEL,
                                   T.FS_IFSAMPLING,
                                   T.FS_IFACCEPT,
                                   T.FS_DRIVERNAME,
                                   T.FS_DRIVERIDCARD,
                                   T.FS_STOVENO1 FS_STOVENO,
                                   T.FN_COUNT1 FN_BILLETCOUNT,
                                   T.FS_STOVENO2,
                                   T.FN_COUNT2,
                                   T.FS_STOVENO3,
                                   T.FN_COUNT3,
                                   T.FN_SENDGROSSWEIGHT FS_DFMZ,
                                   T.FN_SENDTAREWEIGHT FS_DFPZ,
                                   T.FN_SENDNETWEIGHT FS_DFJZ
                              FROM DT_FIRSTCARWEIGHT T, DT_WEIGHTPLAN P
                             WHERE T.FS_PLANCODE = P.FS_PLANCODE(+)
                               AND FD_WEIGHTTIME = (SELECT MAX(B.FD_WEIGHTTIME)
                                                      FROM DT_FIRSTCARWEIGHT B
                                                     WHERE NVL(B.FS_FALG, '0') = '0'
                                                       {0})
                               {1}
                             ORDER BY FS_DATETIME";
                string condition1 = "";
                string condition2 = "";
                if (!string.IsNullOrEmpty(cardNo))
                {
                    condition1 += " and B.FS_CARDNUMBER ='" + cardNo + "'";
                    condition2 += " and T.FS_CARDNUMBER ='" + cardNo + "'";
                }
                if (!string.IsNullOrEmpty(carNo))
                {
                    condition1 += " and B.FS_CARNO ='" + carNo + "'";
                    condition2 += " and T.FS_CARNO ='" + carNo + "'";
                }

                //无一次计量记录查询预报
                if (!CommQuery(dt, string.Format(sql, condition1, condition2), null) || dt.Rows.Count == 0)
                {
                    sql = @"select FS_PlanCode,
                                   FS_CARDNUMBER,
                                   FS_CARNO,
                                   FS_ContractNo,
                                   FS_ContractItem,
                                   FS_Sender,
                                   FHDW_BHTOMC(FS_Sender) FS_FHDW,
                                   Provider_BHTOMC(FS_Provider) FS_Provider,
                                   FS_DRIVERREMARK,
                                   FS_SenderStore,
                                   FS_Material,
                                   WL_BHTOMC(FS_Material) FS_MaterialName,
                                   FS_ReceiverFactory,
                                   SHDW_BHTOMC(FS_ReceiverFactory) FS_SHDW,
                                   FS_ReceiverStore,
                                   FS_TransNo,
                                   CYDW_BHTOMC(FS_TransNo) FS_CYDW,
                                   FS_WeightType,
                                   LX_BHTOMC(FS_WeightType) FS_LX,
                                   FS_PoundType,
                                   to_char(FS_OverTime, 'yyyy-MM-dd HH24:mi:ss') as FS_OverTime,
                                   FN_SendGrossWeight,
                                   FN_SendTareWeight,
                                   FN_SendNetWeight,
                                   FS_StoveNo,
                                   FN_BilletCount,
                                   FS_Person,
                                   FS_Point,
                                   to_char(FS_Datetime, 'yyyy-MM-dd HH24:mi:ss') as FS_Datetime,
                                   FS_Shift,
                                   FS_Term,
                                   FS_Status,
                                   FN_Times,
                                   FN_WeightTimes,
                                   FS_PlanUser,
                                   FS_PlanTel,
                                   FS_Level,
                                   FS_IFSAMPLING,
                                   FS_IFACCEPT,
                                   FS_DRIVERNAME,
                                   FS_DRIVERIDCARD
                              from DT_WeightPlan T
                             where FS_PLANCODE =
                                   (select max(B.FS_PLANCODE) from DT_WeightPlan B where nvl(FS_STATUS,'0') <> '1'
                                    {0}
                                    ) {1}
                             order by FS_Datetime";
                    return CommQuery(dt, string.Format(sql, condition1, condition2), null);
                }
                else
                {
                    return true;
                }
            }
            
        }

        /// <summary>
        /// 查询快速预报
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="quickNo"></param>
        /// <returns></returns>
        public bool GetQuickPlan(DataTable dt, string quickNo)
        {
            string sql = @"SELECT FS_PlanCode,
                                   FS_CARDNUMBER,
                                   FS_CARNO,
                                   FS_ContractNo,
                                   FS_ContractItem,
                                   FS_Sender,
                                   FHDW_BHTOMC(FS_Sender) FS_FHDW,
                                   Provider_BHTOMC(FS_Provider) FS_Provider,
                                   FS_DRIVERREMARK,
                                   FS_SenderStore,
                                   FS_Material,
                                   WL_BHTOMC(FS_Material) FS_MaterialName,
                                   FS_ReceiverFactory,
                                   SHDW_BHTOMC(FS_ReceiverFactory) FS_SHDW,
                                   FS_ReceiverStore,
                                   FS_TransNo,
                                   CYDW_BHTOMC(FS_TransNo) FS_CYDW,
                                   FS_WeightType,
                                   LX_BHTOMC(FS_WeightType) FS_LX,
                                   FS_PoundType,
                                   to_char(FS_OverTime, 'yyyy-MM-dd HH24:mi:ss') as FS_OverTime,
                                   FN_SendGrossWeight,
                                   FN_SendTareWeight,
                                   FN_SendNetWeight,
                                   FS_StoveNo,
                                   FN_BilletCount,
                                   FS_Person,
                                   FS_Point,
                                   to_char(FS_Datetime, 'yyyy-MM-dd HH24:mi:ss') as FS_Datetime,
                                   FS_Shift,
                                   FS_Term,
                                   FS_Status,
                                   FN_Times,
                                   FN_WeightTimes,
                                   FS_PlanUser,
                                   FS_PlanTel,
                                   FS_Level,
                                   FS_IFSAMPLING,
                                   FS_IFACCEPT,
                                   FS_DRIVERNAME,
                                   FS_DRIVERIDCARD
                                       FROM DT_WEIGHTPLAN A WHERE 1=1 And FS_STATUS='3'";
            sql += " and FS_PLANCODE like '%-'||'" + quickNo + "'||'#'";

            return CommQuery(dt, sql, null);
        }

        /// <summary>
        /// 获取车辆对应的计量信息
        /// </summary>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public string GetWeightStatus(string carNo,bool isDischarge)
        {
            DataTable dt = new DataTable();
            string status = "";

            string sql = @"SELECT T.FS_CARNO, T.FS_WEIGHTNO, T.FN_TAREWEIGHT
                              FROM DT_TERMTARE T
                             WHERE T.FS_CARNO = '{0}'
                               AND T.FS_ISVAILD = '1'
                               AND T.FD_ENDTIME > SYSDATE ORDER BY T.FD_ENDTIME DESC
                            ";
            this.CommQuery(dt, string.Format(sql, carNo), null);
            if (dt.Rows.Count > 0)
            {
                status = "车牌号:" + dt.Rows[0]["FS_CARNO"].ToString() + ",期限皮重:" + dt.Rows[0]["FN_TAREWEIGHT"].ToString() + "吨。";
            }
            else
            {
                sql = @"SELECT T.FS_WEIGHTNO, T.FS_CARNO, T.FN_WEIGHT
                          FROM DT_FIRSTCARWEIGHT T
                         WHERE T.FS_CARNO = '{0}'
                           AND T.FS_FALG = '0'";
                this.CommQuery(dt, string.Format(sql, carNo), null);
                if (dt.Rows.Count > 0)
                {
                    status = "车牌号:" + dt.Rows[0]["FS_CARNO"].ToString() + ",一次计量重量:" + dt.Rows[0]["FN_WEIGHT"].ToString() + "吨。";
                    status += isDischarge ? "已卸货。" : "";
                    if (!isDischarge)
                    {
                        DataTable dtFlag = new DataTable();
                        status += (this.GetFirsetchargeFlag(dtFlag, carNo) && dtFlag.Rows.Count > 0) ? "已卸货。" : "未卸货。";
                    }
                }
                else
                {
                    status = "车牌号:"+carNo+",无计量记录。";
                }
            }

            return status;
        }

        /// <summary>
        /// 获取一次计量或期限皮重图片
        /// </summary>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public Bitmap[] GetFirstImage(string carNo)
        {
            Bitmap[] bm = null;
            string sql = @"select t.fs_weightno, t.fb_image1, t.fb_image2, t.fb_image3, t.fb_image4
                                  from dt_images t
                                 where t.fs_weightno = nvl((select max(fs_weightno)
                                                             from dt_firstcarweight
                                                            where fs_falg = '0'
                                                              and fs_carno = '{0}'),
                                                           (select max(fs_weightno)
                                                              from dt_termtare
                                                             where fs_isvaild = '1'
                                                               and fs_carno = '{1}'))";
            DataTable dt = new DataTable();
            if (CommQuery(dt, string.Format(sql, carNo, carNo), null) && dt.Rows.Count > 0)
            {
                bm = new Bitmap[4];
                bm[0] = BytesToBitmap((byte[])(dt.Rows[0]["FB_IMAGE1"]));
                bm[1] = BytesToBitmap((byte[])(dt.Rows[0]["FB_IMAGE2"]));
                bm[2] = BytesToBitmap((byte[])(dt.Rows[0]["FB_IMAGE3"]));
                bm[3] = BytesToBitmap((byte[])(dt.Rows[0]["FB_IMAGE4"]));
            }

            return bm;
        }
		
		/// <summary>
        /// 获取纸张总量
        /// </summary>
        /// <param name="pointCode"></param>
        /// <returns></returns>
        public int GetPrinterPaperCount(string pointCode)
        {
            int count = 0;
            DataTable dt = new DataTable();
            string sql = "SELECT T.FN_PRINTERPAPERCOUNT FROM BT_POINT T WHERE T.FS_POINTCODE = '{0}'";
            if (CommQuery(dt, string.Format(sql, pointCode), null) && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                int.TryParse(dr["FN_PRINTERPAPERCOUNT"].ToString(), out count);
            }
            return count;
        }

        /// <summary>
        /// 计量点重装打印纸
        /// </summary>
        /// <param name="pointCode"></param>
        /// <returns></returns>
        public bool ReloadPrinterPaper(string pointCode)
        {
            string sql = "UPDATE BT_POINT T SET T.FN_USEDPRINTPAPER = T.FN_PRINTERPAPERCOUNT WHERE T.FS_POINTCODE = '{0}'";
            Save(string.Format(sql, pointCode), null);
            return true;
        }

        /// <summary>
        /// 计量点打印纸减少
        /// </summary>
        /// <param name="pointCode"></param>
        /// <returns></returns>
        public bool ReducePrinterPaper(string pointCode)
        {
            string sql = "UPDATE BT_POINT T SET T.FN_USEDPRINTPAPER = NVL(T.FN_USEDPRINTPAPER,0) - 1 WHERE T.FS_POINTCODE = '{0}'";
            Save(string.Format(sql, pointCode), null);
            return true;
        }

        #region 二进制转换成图片
        /// <summary>
        /// 二进制转换成图片
        /// </summary>
        private Bitmap BytesToBitmap(byte[] Bytes)
        {

            if (Bytes.Length == 0 || Bytes.Length == 1)
            {
                return null;
            }
            try
            {
                MemoryStream ImageMem = new MemoryStream(Bytes, true);
                ImageMem.Write(Bytes, 0, Bytes.Length);
                Bitmap _Image = new Bitmap(ImageMem);
                return _Image;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        /// <summary>
        /// 查询打印数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="weightNo"></param>
        /// <returns></returns>
        public HgLable GetPrintData(string weightNo)
        {
            if(string.IsNullOrEmpty(weightNo))
            {
                return null;
            }
            string sql = "select * from {0} where fs_weightno = '{1}'";
            string tableName = "";
            switch (weightNo.Substring(weightNo.Length - 1).ToUpper())
            {
                case "A":
                    tableName = "DT_ASSISTWEIGHT";
                    break;
                case "T":
                    tableName = "DT_TERMTARE";
                    sql = @"SELECT T.FS_WEIGHTNO,
                                   T.FS_CARNO,
                                   T.FS_MATERIALNAME,
                                   T.FS_CARDNUMBER, 
                                   FHDW_BHTOMC(T.FS_SENDER) FS_SENDER,
                                   SHDW_BHTOMC(T.FS_RECEIVER) FS_RECEIVER,
                                   CYDW_BHTOMC(T.FS_TRANSNO) FS_TRANSNO,
                                   LX_BHTOMC(T.FS_WEIGHTTYPE) FS_WEIGHTTYPE,
                                   T.FD_DATETIME,
                                   T.FN_TAREWEIGHT
                              FROM {0} T
                             WHERE T.FS_WEIGHTNO = '{1}'";
                    break;
                case "F":
                    tableName = "DT_FIRSTCARWEIGHT";
                    sql = @"SELECT T.FS_WEIGHTNO,
                                   T.FS_CONTRACTNO,
                                   T.FS_CARNO,
                                   T.FS_CARDNUMBER, 
                                   T.FS_MATERIALNAME,
                                   FHDW_BHTOMC(T.FS_SENDER) FS_SENDER,
                                   SHDW_BHTOMC(T.FS_RECEIVER) FS_RECEIVER,
                                   CYDW_BHTOMC(T.FS_TRANSNO) FS_TRANSNO,
                                   LX_BHTOMC(T.FS_WEIGHTTYPE) FS_WEIGHTTYPE,
                                   T.FD_WEIGHTTIME,
                                   T.FS_FIRSTLABELID,
                                   T.FS_STOVENO1,
                                   T.FS_STOVENO2,
                                   T.FS_STOVENO3,
                                   T.FN_COUNT1,
                                   T.FN_COUNT2,
                                   T.FN_COUNT3,
                                   T.FN_WEIGHT,
                                   T.FS_YKL,
                                   T.FS_BZ,
                                   H.FS_ZZJY,
                                   H.FS_ADVISESPEC
                              FROM {0} T, IT_FP_TECHCARD H
                             WHERE T.FS_STOVENO1 = H.FS_GP_STOVENO(+)
                               AND T.FS_WEIGHTNO = '{1}'
                            ";
                    break;
                case "H":
                    tableName = "DT_CARWEIGHT_WEIGHT";
                    sql = @"SELECT T.FS_WEIGHTNO,
                                   T.FS_CONTRACTNO,
                                   T.FS_CARNO,
                                   T.FS_CARDNUMBER, 
                                   T.FS_MATERIALNAME,
                                   FHDW_BHTOMC(T.FS_SENDER) FS_SENDER,
                                   SHDW_BHTOMC(T.FS_RECEIVER) FS_RECEIVER,
                                   CYDW_BHTOMC(T.FS_TRANSNO) FS_TRANSNO,
                                   LX_BHTOMC(T.FS_WEIGHTTYPE) FS_WEIGHTTYPE,
                                   T.FD_GROSSDATETIME,
                                   T.FD_TAREDATETIME,
                                   T.FS_FULLLABELID,
                                   T.FS_STOVENO1,
                                   T.FS_STOVENO2,
                                   T.FS_STOVENO3,
                                   T.FN_COUNT1,
                                   T.FN_COUNT2,
                                   T.FN_COUNT3,
                                   T.FN_GROSSWEIGHT,
                                   to_char(T.FD_GROSSDATETIME,'yyyy-mm-dd hh24:mi:ss') FS_GROSSDATETIME,
                                   T.FN_TAREWEIGHT,
                                   to_char(T.FD_TAREDATETIME,'yyyy-mm-dd hh24:mi:ss') FS_TAREDATETIME,
                                   T.FN_NETWEIGHT,
                                   decode(T.FS_YKL,0,null,FS_YKL) FS_YKL,
                                   T.FS_KHJZ,
                                   T.FS_BZ,
                                   H.FS_ZZJY,
                                   H.FS_ADVISESPEC
                              FROM {0} T, IT_FP_TECHCARD H
                             WHERE T.FS_STOVENO1 = H.FS_GP_STOVENO(+)
                               AND T.FS_WEIGHTNO = '{1}'";
                    break;
                default:
                    return null;
                    break;
            }
            DataTable dt = new DataTable();
            CommQuery(dt, string.Format(sql, tableName, weightNo), null);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                switch (tableName)
                {
                    case "DT_ASSISTWEIGHT":
                        return BandAssistWeight(dr);
                        break;
                    case "DT_TERMTARE":
                        return BandTermTareWeight(dr);
                        break;
                    case "DT_FIRSTCARWEIGHT":
                        return BandFirstWeight(dr);
                        break;
                    case "DT_CARWEIGHT_WEIGHT":
                        return BandHistoryWeight(dr);
                        break;
                }
            }

            return null;
        }

        private HgLable BandAssistWeight(DataRow dr)
        {
            HgLable data = new HgLable();
            data.CarNo = dr["FS_CARNO"].ToString();
            data.Weight = dr["FN_WEIGHT"].ToString();
            data.Date = (DateTime)dr["FS_WEIGHTTIME"];
            data.Charge = !string.IsNullOrEmpty(dr["FN_CHARGE"].ToString()) ? dr["FN_CHARGE"].ToString() : "0";
            return data;
        }

        private HgLable BandTermTareWeight(DataRow dr)
        {
            HgLable data = new HgLable();
            data.Date = (DateTime)dr["FD_DATETIME"];//时间
            data.CarNo = dr["FS_CARNO"].ToString();//车号
            data.CardNumber = dr["FS_CARDNUMBER"].ToString();//卡号
            data.SupplierName = dr["FS_SENDER"].ToString();//发货单位
            data.Receiver = dr["FS_RECEIVER"].ToString();//收货单位
            data.TransName = dr["FS_TRANSNO"].ToString();//承运单位
            data.Weight = dr["FN_TAREWEIGHT"].ToString();
            data.CarComment = "期限皮重";//备注
            return data;
        }

        private HgLable BandFirstWeight(DataRow dr)
        {
            HgLable data = new HgLable();
            data.Date = (DateTime)dr["FD_WEIGHTTIME"];//时间
            data.ContractNo = dr["FS_CONTRACTNO"].ToString();//合同号
            data.CarNo = dr["FS_CARNO"].ToString();//车号
            data.CardNumber = dr["FS_CARDNUMBER"].ToString();//卡号
            data.SupplierName = dr["FS_SENDER"].ToString();//发货单位
            data.Receiver = dr["FS_RECEIVER"].ToString();//收货单位
            data.TransName = dr["FS_TRANSNO"].ToString();//承运单位
            data.MaterialName = dr["FS_MATERIALNAME"].ToString();//物资名称
            data.StoveNo = dr["FS_STOVENO1"].ToString() + (!string.IsNullOrEmpty(dr["FS_STOVENO2"].ToString()) ? ("," + dr["FS_STOVENO2"].ToString()) : "") + (!string.IsNullOrEmpty(dr["FS_STOVENO3"].ToString()) ? ("," + dr["FS_STOVENO3"].ToString()) : "");//炉号
            int count1 = 0, count2 = 0, count3 = 0;
            int.TryParse(dr["FN_COUNT1"].ToString(),out count1);
            int.TryParse(dr["FN_COUNT2"].ToString(), out count2);
            int.TryParse(dr["FN_COUNT3"].ToString(), out count3);
            data.Count = (count1+count2+count3).ToString(); //支数
            data.MillComment = dr["FS_ZZJY"].ToString();//轧制建议
            data.PlanSpec = dr["FS_ADVISESPEC"].ToString();//建议轧制规格
            data.Weight = dr["FN_WEIGHT"].ToString();
            //data.DeductWeight = dr["FS_YKL"].ToString();//扣杂
            data.BarCode = dr["FS_FIRSTLABELID"].ToString();//一次条码号
            data.CarComment = dr["FS_BZ"].ToString();//备注
            return data;
        }

        private HgLable BandHistoryWeight(DataRow dr)
        {
            HgLable data = new HgLable();

            DateTime grossTime = (DateTime)dr["FD_GROSSDATETIME"];
            DateTime tareTime = (DateTime)dr["FD_TAREDATETIME"];

            data.WeightNum = "1";
            data.Date = grossTime > tareTime ? grossTime : tareTime;//时间
            data.ContractNo = dr["FS_CONTRACTNO"].ToString();//合同号
            data.CarNo = dr["FS_CARNO"].ToString();//车号
            data.CardNumber = dr["FS_CARDNUMBER"].ToString();//卡号
            data.SupplierName = dr["FS_SENDER"].ToString();//发货单位
            data.Receiver = dr["FS_RECEIVER"].ToString();//收货单位
            data.TransName = dr["FS_TRANSNO"].ToString();//承运单位
            data.MaterialName = dr["FS_MATERIALNAME"].ToString();//物资名称
            data.StoveNo = dr["FS_STOVENO1"].ToString() + (!string.IsNullOrEmpty(dr["FS_STOVENO2"].ToString()) ? ("," + dr["FS_STOVENO2"].ToString()) : "") + (!string.IsNullOrEmpty(dr["FS_STOVENO3"].ToString()) ? ("," + dr["FS_STOVENO3"].ToString()) : "");//炉号
            int count1 = 0, count2 = 0, count3 = 0;
            int.TryParse(dr["FN_COUNT1"].ToString(), out count1);
            int.TryParse(dr["FN_COUNT2"].ToString(), out count2);
            int.TryParse(dr["FN_COUNT3"].ToString(), out count3);
            data.Count = (count1 + count2 + count3).ToString(); //支数
            data.MillComment = dr["FS_ZZJY"].ToString();//轧制建议
            data.PlanSpec = dr["FS_ADVISESPEC"].ToString();//建议轧制规格
            data.GrossWeight = dr["FN_GROSSWEIGHT"].ToString();//毛重
            data.TareWeight = dr["FN_TAREWEIGHT"].ToString();//皮重
            data.NetWeight = dr["FN_NETWEIGHT"].ToString();//净重
            data.DeductWeight = dr["FS_YKL"].ToString();//扣杂
            data.DeductAfterWeight = dr["FS_KHJZ"].ToString();//扣后净重
            data.CarComment = dr["FS_BZ"].ToString();//备注
            data.Flow = dr["FS_WEIGHTTYPE"].ToString();//流向
            data.GrossWeightTime = dr["FS_GROSSDATETIME"].ToString();//毛重时间
            data.TareWeightTime = dr["FS_TAREDATETIME"].ToString();//皮重时间
            data.BarCode = dr["FS_FULLLABELID"].ToString();//完整条码号

            return data;
        }

        /// <summary>
        /// 计量前流程控制，如是否重空匹配、是否卸货、是否存在进厂记录、历史皮重偏差是否太大等
        /// </summary>
        /// <param name="carNo"></param>
        /// <param name="weight"></param>
        /// <param name="message"></param>
        /// <param name="isAffirm">是否可选择</param>
        /// <returns></returns>
        public bool CheckWeighHistory(string carNo,double weight,string flow,bool isNeedDischarge,string dischargeFlag,bool isAllowTareWeight,out string message,out bool isAffirm)
        {
            bool flag = true;

            isAffirm = true;
            double firstWeight = 0;
            DataTable dt = new DataTable();
            GetFirstWeighData(dt, "", carNo);
            if (dt.Rows.Count > 0)
            {
                //二次计量卸货验证
                if (isNeedDischarge && string.IsNullOrEmpty(dischargeFlag))
                {
                    DataTable dtchargeFlag = new DataTable();
                    GetFirsetchargeFlag(dtchargeFlag, carNo);
                    if (dtchargeFlag.Rows.Count == 0)
                    {
                        flag = false;
                        if (isAllowTareWeight)
                        {
                            message = "车号：" + carNo + "没有卸货，是否确认计量？";
                        }
                        else
                        {
                            message = "车号：" + carNo + "没有卸货，不能进行计量！";
                            isAffirm = false;
                        }
                        //message = "车号：" + carNo + "没有卸货!请重新卸货";
                        return flag;
                    }
                }

                firstWeight = double.Parse(dt.Rows[0]["FN_WEIGHT"].ToString());
                if (Math.Abs(weight - firstWeight) < 0.5)
                {
                    flag = false;
                    message = "车号：" + carNo + "两次计量重量差值为" + Math.Abs(weight - firstWeight).ToString() + "吨，是否确认计量？";
                    return flag;
                }
            }
            else
            {
                //一次进厂验证
                if (flow.Equals("001") || flow.Equals("004"))
                {
                    DataTable dtEntry = new DataTable();
                    GetEntryHistory(dtEntry,carNo);
                    if (dtEntry.Rows.Count == 0)
                    {
                        flag = false;
                        message = "车号：" + carNo + "没有进厂记录，是否确认计量？";
                        return flag;
                    }
                }
            }

            dt.Clear();
            GetHistoryTareWeight(dt, "", carNo);
            firstWeight = 0;
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                for (; i < dt.Rows.Count && i < 5; i++)
                {
                    firstWeight += double.Parse(dt.Rows[i]["FN_TAREWEIGHT"].ToString());
                }

                if (Math.Abs(weight - firstWeight / (i + 1)) > 0.5 && Math.Abs(weight - firstWeight / (i + 1)) < 3)
                {
                    flag = false;
                    message = "车号：" + carNo + "重量与历史皮重相差" + Math.Abs(weight - firstWeight / (i + 1)).ToString() + "吨，是否确认计量？";
                    return flag;
                }
            }

            message = "";
            return flag;
        }

        /// <summary>
        /// 查询进厂记录
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public bool GetEntryHistory(DataTable dt, string carNo)
        {
            string sql = @"SELECT T.FS_ENTERFACNO,
                                   T.FS_CARDNUMBER,
                                   T.FS_CARNO,
                                   T.FD_ENTERFACTIME,
                                   T.FS_ENTERFACPLACE,
                                   T.FS_ENTERFACCHECKER,
                                   T.FS_ENTERFACREMARK
                              FROM DT_ENTERFACRECORD T
                             WHERE T.FN_ENTERFACFLAG = 1
                                   {0}
                             ORDER BY T.FD_ENTERFACTIME DESC";
            string condition = "";
            if (!string.IsNullOrEmpty(carNo))
            {
                condition += " AND T.FS_CARNO = '" + carNo + "'";
            }

            return CommQuery(dt, string.Format(sql, condition), null);
        }
		
		public bool GetFirsetchargeFlag(DataTable dt, string carNo)
        {
            string sql = @"SELECT T.FS_WEIGHTNO,
                                   T.FS_CARDNUMBER,
                                   T.FS_CARNO,
                                   T.FS_UNLOADPERSON,
                                   T.FD_UNLOADTIME
                              FROM DT_FIRSTCARWEIGHT T
                             WHERE T.FS_UNLOADFLAG = '1' and FS_FALG <>'1'
                                   {0}
                             ORDER BY T.FD_WEIGHTTIME DESC";
            string condition = "";
            if (!string.IsNullOrEmpty(carNo))
            {
                condition += " AND T.FS_CARNO = '" + carNo + "'";
            }

            return CommQuery(dt, string.Format(sql, condition), null);
        }

        /// <summary>
        /// 获取历史皮重
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardNo"></param>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public bool GetHistoryTareWeight(DataTable dt, string cardNo, string carNo)
        {
            string sql = @"SELECT T.FS_WEIGHTNO, T.FN_TAREWEIGHT
                                  FROM DT_CARWEIGHT_WEIGHT T
                                 WHERE 1 = 1
                                 {0}
                                 ORDER BY T.FD_TAREDATETIME DESC";
            string condition = "";
            if (!string.IsNullOrEmpty(cardNo))
            {
                condition += " AND T.FS_CARDNUMBER = '" + cardNo + "'";
            }
            if (!string.IsNullOrEmpty(carNo))
            {
                condition += " AND T.FS_CARNO = '" + carNo + "'";
            }

            return CommQuery(dt, string.Format(sql, condition), null);
        }

        /// <summary>
        /// 查询一次计量数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardNo"></param>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public bool GetFirstWeighData(DataTable dt,string cardNo,string carNo)
        {
            string sql = @"select A.FS_WEIGHTNO,A.FS_PLANCODE,A.FS_CONTRACTNO,A.FS_CONTRACTITEM,A.FS_STOVENO,Provider_BHTOMC(A.FS_Provider) FS_Provider,A.FS_BZ,
				                    A.FN_COUNT,A.FS_CARDNUMBER,A.FS_CARNO,A.FS_MATERIAL,A.FS_MATERIALNAME,A.FS_Sender,FHDW_BHTOMC(A.FS_Sender) FS_FHDW,
				                    A.FS_SENDERSTORE,A.FS_TRANSNO,CYDW_BHTOMC(A.FS_TRANSNO) FS_CYDW,A.FS_RECEIVER,SHDW_BHTOMC(A.FS_RECEIVER) FS_SHDW,
				                    A.FS_UNLOADPLACE,A.FS_WEIGHTTYPE,LX_BHTOMC(A.FS_WEIGHTTYPE) FS_LX,A.FS_POUNDTYPE,A.FN_SENDGROSSWEIGHT,
				                    A.FN_SENDTAREWEIGHT,A.FN_SENDNETWEIGHT,A.FN_WEIGHT,A.FS_POUND,A.FS_WEIGHTER,
				                    to_char(A.FD_WEIGHTTIME,'yyyy-MM-dd HH24:mi:ss')as FD_WEIGHTTIME,A.FS_SHIFT,A.FS_TERM,FS_FIRSTLABELID,
		                            to_char(FD_UNLOADINSTORETIME,'yyyy-MM-dd HH24:mi:ss')as FD_UNLOADINSTORETIME,
				                    to_char(FD_UNLOADOUTSTORETIME,'yyyy-MM-dd HH24:mi:ss')as FD_UNLOADOUTSTORETIME,
				                    FS_UNLOADFLAG,FS_UNLOADSTOREPERSON,FS_ReceiverFactory,
				                    to_char(FD_LOADINSTORETIME,'yyyy-MM-dd HH24:mi:ss')as FD_LOADINSTORETIME,
				                    to_char(FD_LOADOUTSTORETIME,'yyyy-MM-dd HH24:mi:ss')as FD_LOADOUTSTORETIME,
				                    FS_LOADFLAG,FS_LOADSTOREPERSON,FS_SAMPLEPERSON,FS_YCSFYC,
				                    to_char(FD_SAMPLETIME,'yyyy-MM-dd HH24:mi:ss')as FD_SAMPLETIME,FS_SAMPLEPLACE,
				                    FS_SAMPLEFLAG,FS_UNLOADPERSON,to_char(FD_UNLOADTIME,'yyyy-MM-dd HH24:mi:ss')as FD_UNLOADTIME,
				                    FS_UNLOADPLACE,FS_CHECKPERSON,to_char(FD_CHECKTIME,'yyyy-MM-dd HH24:mi:ss')as FD_CHECKTIME,
				                    FS_CHECKPLACE,FS_CHECKFLAG,FS_IFSAMPLING,FS_IFACCEPT,FS_DRIVERNAME,FS_DRIVERIDCARD,FS_YKL,
				                    FS_REWEIGHTFLAG,to_char(FD_REWEIGHTTIME,'yyyy-MM-dd HH24:mi:ss')as FD_REWEIGHTTIME,
				                    FS_REWEIGHTPLACE,FS_REWEIGHTPERSON,FS_BILLNUMBER,FS_DFJZ,FS_YKBL,FS_MEMO
				                     from DT_FIRSTCARWEIGHT A where 1=1 AND nvl(A.FS_FALG,'0') = '0'
				                    {0}
				                     order by FD_WEIGHTTIME DESC";
            string condition = "";
            if (!string.IsNullOrEmpty(cardNo))
            {
                condition += " AND A.FS_CARDNUMBER = '"+cardNo+"'";
            }
            if (!string.IsNullOrEmpty(carNo))
            {
                condition += " AND A.FS_CARNO = '"+carNo+"'";
            }

            return CommQuery(dt,string.Format(sql,condition),null);
        }

        /// <summary>
        /// 保存卡上次卸货记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string[] SaveCardDischarge(Hashtable param)
        {
            CoreClientParam ccp = this.excuteProcedure2("{call YG_MCMS_CARWEIGHT.SAVE_CARDDISCHARGE(?,?,?,?,?,?,?,?,?)}", param);

            string result = "";
            string message = "";
            if (ccp.ReturnObject != null && !string.IsNullOrEmpty(ccp.ReturnObject.ToString()))
            {
                object[] results = (object[])ccp.ReturnObject;
                for (int i = 0; i < results.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            result = results[i] != null ? results[i].ToString() : "";
                            break;
                        case 1:
                            message = results[i] != null ? results[i].ToString() : "";
                            break;
                    }
                }
            }
            return new string[] { result, message };
        }

        /// <summary>
        /// 保存计量数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string[] SaveWeightData(Hashtable param)
        {
            CoreClientParam ccp = this.excuteProcedure2("{call YG_MCMS_CARWEIGHT.SAVE_WEIGHT(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}", param);

            string result = "-1";
            string message = "";
            string weightNo = "";
            if (ccp.ReturnObject != null && !string.IsNullOrEmpty(ccp.ReturnObject.ToString()))
            {
                object[] results = (object[])ccp.ReturnObject;
                for (int i = 0; i < results.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            result = results[i] != null ? results[i].ToString() : "";
                            break;
                        case 1:
                            weightNo = results[i] != null ? results[i].ToString() : "";
                            break;
                        case 2:
                            message = results[i] != null ? results[i].ToString() : "";
                            break;
                    }
                }
            }
            return new string[] { result, weightNo,message };
        }

        /// <summary>
        /// 保存计量图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public void SavePicData(string weightNo,ArrayList pics,string curve,string reweightFlag)
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SavePicData";
            ccp.ServerParams = new object[] { weightNo,pics,curve,reweightFlag };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 校称
        /// </summary>
        /// <param name="pointCode"></param>
        /// <param name="carNo"></param>
        /// <param name="weight"></param>
        /// <param name="correnter"></param>
        /// <param name="shift"></param>
        /// <param name="group"></param>
        /// <param name="remark"></param>
        /// <returns>校称记录编号</returns>
        public string Corrention(string pointCode, string carNo, double weight, string correnter, string shift, string group, string remark)
        {
            Hashtable param = new Hashtable();
            param.Add("I1", pointCode);
            param.Add("I2", carNo);
            param.Add("I3", weight);
            param.Add("I4", correnter);
            param.Add("I5", shift);
            param.Add("I6", group);
            param.Add("I7", "");
            param.Add("I8", remark);
            param.Add("O9", "");
            CoreClientParam ccp = this.excuteProcedure2("{call HG_MCMS_CARWEIGHT.CORRENTION(?,?,?,?,?,?,?,?,?)}", param);

            string result = "";
            string message = "";
            string weightNo = "";
            if (ccp.ReturnObject != null && !string.IsNullOrEmpty(ccp.ReturnObject.ToString()))
            {
                object[] results = (object[])ccp.ReturnObject;
                for (int i = 0; i < results.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            result = results[i] != null ? results[i].ToString() : "";
                            break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 获取补写卡内容
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="carNo"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetCardData(string cardNo, string carNo, DataTable dt)
        {
            string sql = @"SELECT T.FS_PLANCODE,
                           T.FS_CARDNUMBER,
                           T.FS_CARNO,
                           T.FS_CONTRACTNO,
                           T.FS_CONTRACTITEM,
                           T.FS_SENDER,
                           FHDW_BHTOMC(T.FS_SENDER) FS_FHDW,
                           PROVIDER_BHTOMC(T.FS_PROVIDER) FS_PROVIDER,
                           T.FS_SENDERSTORE,
                           T.FS_MATERIAL,
                           WL_BHTOMC(T.FS_MATERIAL) FS_MATERIALNAME,
                           T.FS_RECEIVER FS_RECEIVERFACTORY,
                           SHDW_BHTOMC(T.FS_RECEIVER) FS_SHDW,
                           T.FS_RECEIVERSTORE,
                           T.FS_TRANSNO,
                           CYDW_BHTOMC(T.FS_TRANSNO) FS_CYDW,
                           T.FS_WEIGHTTYPE,
                           LX_BHTOMC(T.FS_WEIGHTTYPE) FS_LX,
                           T.FS_POUNDTYPE,
                           T.FS_POUND,
                           T.FN_SENDGROSSWEIGHT,
                           T.FN_SENDTAREWEIGHT,
                           T.FN_SENDNETWEIGHT,
                           T.FS_STOVENO,
                           TO_CHAR(T.FD_WEIGHTTIME, 'YYYY-MM-DD HH24:MI:SS') AS FS_DATETIME,
                           T.FN_WEIGHT,T.FS_WEIGHTNO
                      FROM DT_FIRSTCARWEIGHT T, DT_WEIGHTPLAN P
                     WHERE T.FS_PLANCODE = P.FS_PLANCODE(+)
                       AND FD_WEIGHTTIME = (SELECT MAX(B.FD_WEIGHTTIME)
                                              FROM DT_FIRSTCARWEIGHT B
                                             WHERE NVL(B.FS_FALG, '0') = '0'
                                               {0})
                       {1}
                     ORDER BY FS_DATETIME";
            string condition1 = "";
            string condition2 = "";
            if (!string.IsNullOrEmpty(cardNo))
            {
                condition1 += " and B.FS_CARDNUMBER ='" + cardNo + "'";
                condition2 += " and T.FS_CARDNUMBER ='" + cardNo + "'";
            }
            if (!string.IsNullOrEmpty(carNo))
            {
                condition1 += " and B.FS_CARNO ='" + carNo + "'";
                condition2 += " and T.FS_CARNO ='" + carNo + "'";
            }
            return CommQuery(dt, string.Format(sql, condition1, condition2), null);
        }
        #region 班次班组
        public enum OperationInfo : byte
        {
            order, group
        }

        public string GetOrderGroupName(OperationInfo opInfo, string value)
        {
            string str = "";

            if (opInfo == OperationInfo.order)
            {
                switch (value)
                {
                    case "0":
                        str = "常白班";
                        break;
                    case "1":
                        str = "早班";
                        break;
                    case "2":
                        str = "中班";
                        break;
                    case "3":
                        str = "晚班";
                        break;
                }
            }
            else if (opInfo == OperationInfo.group)
            {
                switch (value)
                {
                    case "0":
                        str = "常白班";
                        break;
                    case "1":
                        str = "甲班";
                        break;
                    case "2":
                        str = "乙班";
                        break;
                    case "3":
                        str = "丙班";
                        break;
                    case "4":
                        str = "丁班";
                        break;
                }
            }

            return str;
        }
        #endregion
    }
}
