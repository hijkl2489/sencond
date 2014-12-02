using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.App
{
    public enum LableType
    {
        LITTLE = 0,   //   小标签 线材用
        BIG = 1,       //  棒材大标签
        BIG1 = 2,       // 高线大标签
        CAR = 3,       // 汽车榜单
        BIG2 = 4,      // 高线大标签(新)
        LITTLE2 = 5     // 高线小标签(新)
    }
    public class CarBilletData
    {
        public string StoveNo { get; set; } //炉号
        public string Count   {get;set;}    //支数
    }

    public class HgLable
    {
        public string BatchNo{get;set;}        //轧制编号
        public string BandNo { get; set; }     //分卷号        
        public string Spec { get; set; }       //规格
        public string Length { get; set; }     //长度
        public string Weight { get; set; }     //重量
        public string Count { get; set; }      //支数
        public DateTime Date { get; set; }     //日期
        public string Term { get; set; }       //班别
        public string BarCode { get; set; }    //条码
        public string Standard { get; set; }   //标准
        public string SteelType { get; set; }  //牌号
        public bool PrintAddress { get; set; } //是否打印地址
        public LableType Type { get; set; }    //标牌打印类型
        // 添加汽车标牌内容
        public string ContractNo { get; set; }     //合同号
        public string SupplierName { get; set; }  //发货单位
        public string Receiver { get; set; }      //收货单位
        public string MaterialName { get; set; }  //物资名称
        public string TransName { get; set; }     //承运单位
        public string Flow { get; set; }          //流向
        public string CarNo { get; set; }         //车号
        public string CardNumber { get; set; }     //车号
        public string StoveNo { get; set; }       //炉号
        public string MillComment { get; set; }   //轧制建议
        public string PlanSpec { get; set; }      //建议轧制规格
        public string GrossWeight { get; set; }   //毛重
        public string TareWeight{get;set;}        //皮重
        public string NetWeight {get;set;}        //净重
        public string GrossWeightTime { get; set; }//毛重时间
        public string TareWeightTime { get; set; }//皮重时间
        public string WeightPoint { get; set; }   //计量点
        public string MeasTech { get; set; }      //计量员
        public string CarComment { get; set; }    //备注        
        public string Rate { get; set; }          // 比例
        public string DeductWeight { get; set; }  // 扣渣重量
        public string DeductAfterWeight { get; set; } // 扣渣后重量
        public string Charge { get; set; }            // 外协费用
        public string WeightNum { get; set; }         // 计量次数
        public CarBilletData[] BilletInfo {get; set;} // 一车多炉次钢坯信息
        public HgLable()
        {
            BatchNo = "";
            BandNo = "";
            Spec = "";
            Length = "";
            Weight = "";
            Count = "";
            Term = "";
            BarCode = "";
            Standard = "";
            SteelType = "";
            PrintAddress = true;
            ContractNo = "";
            SupplierName = "";
            Receiver = "";
            MaterialName = "";
            TransName = "";
            Flow = "";
            CarNo = "";
            CardNumber = "";
            StoveNo = "";
            MillComment = "";
            PlanSpec = "";
            GrossWeight = "";
            TareWeight = "";
            NetWeight = "";
            WeightPoint = "";
            MeasTech = "";
            CarComment = "";
            Rate = "";
            DeductWeight = "";
            DeductAfterWeight = "";
            Charge = "";
            WeightNum = "";
            Type = LableType.BIG;
        }
    }
 
}
