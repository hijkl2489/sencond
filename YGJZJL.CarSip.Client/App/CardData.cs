using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.App
{
    public enum UnloadFlagType
    {
        UNLOAD_OK = 1,
        RETURN_WEIGHT = 2,
        RETURN_NOWEIGHT=3,
        REWEIGHT = 4
    }
    public class CardData
    {
        public string ID { get; set; }           //卡芯片号 [0,0]
        public string CardNo { get; set; }       //卡序号   [0,1]
        public string CarNo { get; set; }        //车号     [0,2]
        public string FirLocNo { get; set; }      //一次计量点编号 [1,0]
        public string FirWeight { get; set; }     //一次计量重量   [1,1]
        public string CardType { get; set; }       //预留   [1,2]   0:管理员 1：车证卡 2：临时卡   
        public string MateriaName { get; set; }   //物资名称 [2,]
        public string Sender { get; set; }        //发货单位 [3,]
        public string Receiver { get; set; }      //收货单位 [4,] 
        public string PlanLoc { get; set; }       //预报地点 [5,] 
        public string SecLocNo { get; set; }      //二次计量点编号 [6,0]
        public string SecWeight { get; set; }     //二次计量重量   [6,1]
        public string WgtOpCd { get; set; }      //计量操作编号    [7,]
        public string UnloadName { get; set; }    //卸货人         [8]
        public string UnloadDepart { get; set; }  //卸货点         [9]
        public string UnloadTime { get; set; }    //卸货时间       [10]
        public string UnloadFlag { get; set; }    //卸货标识       [11,0]  1代表：卸货确认 2代表：退货&过磅 3代表：退货&不过磅 4代表：复磅（重新过磅）
        public string UnloadKZ { get; set; }      //卸货扣杂          [11,1]
        public string Reserve6 { get; set; }      //预留6          [11,2]
        public string PassNo { get; set; }        //进出厂操作编号 [12,]
         
        public string  EnterGate{ get; set; }      //进厂门岗      [13,0]
        public string EnterChecker { get; set; }  //进厂核验人     [13,1]
        public string EnterTime { get; set; }     // 进厂时间      [13,2]
        public string LeaveGate { get; set; }      //出厂门岗      [14,0]
        public string LeaveChecker { get; set; }  //出厂核验人     [14,0]
        public string LeaveTime { get; set; }     // 出厂时间      [14,0]
        public string Reserve7 { get; set; }      // 预留7         [15,0]
        public string Reserve8 { get; set; }      // 预留8         [15,1]
        public string Reserve9 { get; set; }      // 预留9         [15,2] 

        public CardData()
        {
            ID = "";
            CardNo = "";
            CarNo = "";
           
            MateriaName = "";
            Sender = "";
            Receiver = "";
            PlanLoc = "";
            FirLocNo = "";
            FirWeight = "";
            SecLocNo = "";
            SecWeight = "";
        
            WgtOpCd = "";
            UnloadName = "";
            UnloadDepart = "";
            UnloadTime = "";
            UnloadFlag = "";
            UnloadKZ = "";
            Reserve6 = "";
            PassNo = "";
            EnterGate = "";
            EnterChecker = "";
            EnterTime = "";
            LeaveGate = "";
            LeaveChecker = "";
            LeaveTime = "";
            Reserve7 = "";
            Reserve8 = "";
            Reserve9 = "";
        }

        public override string ToString()
        {
            string str = "";
            str += "[CardData:ID:" + ID + ",CardNo:" + CardNo + ",CarNo:" + CarNo + ",MateriaName:" + MateriaName + ",Sender:"
                + Sender + ",Receiver:" + Receiver + ",PlanLoc:" + PlanLoc + ",FirLocNo:" + FirLocNo + ",FirWeight:" + FirWeight + ",SecLocNo:" + SecLocNo + ",SecWeight:" + SecWeight + ",WgtOpCd:" + WgtOpCd
                +"]";
            return str;
        }
    }
}
