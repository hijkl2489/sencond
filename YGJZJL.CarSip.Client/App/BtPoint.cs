using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.App
{
   public class BT_POINT
    {
        public string FS_POINTCODE { get; set; }    //计量点编码
        public string FS_POINTNAME { get; set; } //计量点名称
        public string FS_POINTDEPART { get; set; }//计量点所属工厂代码,只能是内部单位，参考 IT_Factory工厂基础表
        /*
         * 只能是以下类型：汽车衡、静态轨道衡、 动态轨道衡、钢坯转移秤、钢材秤、皮带秤、料斗秤 
         */
        public string FS_POINTTYPE { get; set; }

        public string FS_VIEDOIP   { get; set; }   //硬盘录像机IP
        public string FS_VIEDOPORT { get; set; } //硬盘录像机端口
        public string FS_VIEDOUSER { get; set; }  //硬盘录像机用户名
        public string FS_VIEDOPWD  { get; set; }  //硬盘录像机密码
        public string FS_METERTYPE { get; set; } //仪表类型
        public string FS_METERPARA { get; set; } //仪表参数
        public string FS_MOXAIP    { get; set; }    //MOXA卡IP
        public string FS_MOXAPORT  { get; set; }   //仪表MOXA端口
        public string FS_RTUIP     { get; set; }      //RTUIP
        public string FS_RTUPORT   { get; set; }     //RTU端口
        public string FS_PRINTERIP { get; set; }     //打印服务器IP
        public string FS_PRINTERNAME    { get; set; }   //打印机名称
        public string FS_PRINTTYPECODE  { get; set; }  //打印机类型代码
        public string FN_USEDPRINTPAPER { get; set; }//剩余纸张量
        public string FN_USEDPRINTINK   { get; set; } //剩余碳带量
        public string FS_LEDIP          { get; set; } //LED屏IP
        public string FS_LEDPORT        { get; set; }//LED屏端口
        public string FN_VALUE          { get; set; }//复位值
        public string FS_ALLOWOTHERTARE { get; set; }//允许异地去皮
        public string FS_SIGN           { get; set; }//计量点标志(1:为已被选择,0:没选择)
        public string FS_DISPLAYPORT    { get; set; }//液晶屏MOXA端口
        public string FS_DISPLAYPARA    { get; set; }//液晶屏MOXA参数
        public string FS_READERPORT     { get; set; }//读卡器MOXA端口
        public string FS_READERPARA     { get; set; }//读卡器MOXA参数
        public string FS_READERTYPE     { get; set; }//读卡器类型
        public string FS_DISPLAYTYPE    { get; set; }//液晶屏类型
        public string FS_LEDTYPE        { get; set; } //LED类型
        public string FF_CLEARVALUE     { get; set; } //清零值（差值）
        public string FS_POINTSTATE     { get; set; }//磅房状态 0：试用；1：正式运行
       public BT_POINT()
        {
FS_POINTTYPE = "HGTC";

FS_VIEDOIP = "";//"10.25.3.241";
FS_VIEDOPORT = "8000";
FS_VIEDOUSER = "admin";
FS_VIEDOPWD = "12345";
FS_METERTYPE="XK3210";
FS_METERPARA="COM2,4800,N,8,1,02,0D,17,6,9,0";
FS_MOXAIP  = ""; 
FS_MOXAPORT=""; 
FS_RTUIP   =""; 
FS_RTUPORT =""; 
FS_PRINTERIP="";

FS_PRINTERNAME ="";   
FS_PRINTTYPECODE =""; 
FN_USEDPRINTPAPER ="";
FN_USEDPRINTINK   ="";
FS_LEDIP = "10.25.3.242,0,6666,9990";
FS_LEDPORT        ="";
FN_VALUE          ="";
FS_ALLOWOTHERTARE ="";
FS_SIGN           ="";
FS_DISPLAYPORT    ="";
FS_DISPLAYPARA    ="";
FS_READERPORT     ="";
FS_READERPARA     ="";
FS_READERTYPE     ="";
FS_DISPLAYTYPE    ="";
FS_LEDTYPE        ="";
FF_CLEARVALUE     ="";
FS_POINTSTATE     = ""; 


        }
    }
}
