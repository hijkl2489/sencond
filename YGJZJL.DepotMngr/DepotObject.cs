using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.DepotMngr
{
    public class DepotObject
    {
        public string BatchNo{get;set;}//轧批号
        private string batchIndex = "";

        public string BatchIndex
        {
            get { return batchIndex.Length < 2 ? "0"+batchIndex : batchIndex; }
            set { batchIndex = value; }
        }
        public string weighTime { get; set; }
        public string Material { get; set; }
        public string Weight { get; set; }
        public string Plant { get; set; }
        public string Barcode { get; set; }
        public string TheoryWeight { get; set; }
        public string IndepotFlag { get; set; }
        public string QsFlag { get; set; }
    }
}
