using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.BoardBand.std
{
    class ProdutionWeightStd
    {
        private int seqNo = 0;//序号

        public int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }
        private double spec = 0;//规格

        public double Spec
        {
            get { return spec; }
            set { spec = value; }
        }
        private double lenght = 0;//长度

        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }
        private double minWeight = 0;//单捆重量下限

        public double MinWeight
        {
            get { return minWeight; }
            set { minWeight = value; }
        }
        private double maxWeight = 0;//单捆重量上限

        public double MaxWeight
        {
            get { return maxWeight; }
            set { maxWeight = value; }
        }

        private double singleTheoryWeight = 0;//单支理重

        public double SingleTheoryWeight
        {
            get { return singleTheoryWeight; }
            set { singleTheoryWeight = value; }
        }
        private int singleBundleCount = 0;//单捆支数

        public int SingleBundleCount
        {
            get { return singleBundleCount; }
            set { singleBundleCount = value; }
        }
        private double singleBundleWeight = 0;//单捆理重

        public double SingleBundleWeight
        {
            get { return singleBundleWeight; }
            set { singleBundleWeight = value; }
        }
        private double minDiffrenetRate = 0;//重量负差率下限

        public double MinDiffrenetRate
        {
            get { return minDiffrenetRate; }
            set { minDiffrenetRate = value; }
        }
        private double maxDiffrenetRate = 0;//重量负差率上限

        public double MaxDiffrenetRate
        {
            get { return maxDiffrenetRate; }
            set { maxDiffrenetRate = value; }
        }
    }
}
