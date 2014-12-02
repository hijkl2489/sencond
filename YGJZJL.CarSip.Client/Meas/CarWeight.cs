using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.Meas
{
    // 汽车衡
    public class CarWeight : CoreWeight
    {
        #region <事件>
        public event WeightChangedEventHandler WeightChanged;
        public event WeightCompletedEventHandler WeightCompleted;
        #endregion

        #region <成员变量>
        //private bool _stable = false;
        #endregion
        #region <公共的方法>
        private void OnWeightChange(double weight)
        {
            if (WeightChanged == null) return;
            WeightEventArgs arg = new WeightEventArgs(weight);
            WeightChanged(this, arg);
        }

        private void OnWeightCompleted(double weight)
        {
            if (WeightCompleted == null) return;
            WeightEventArgs arg = new WeightEventArgs(weight);
            _count++;
            if (_count > _maxCount) // 稳定值计算判断
            {
                arg.Value = weight;
                WeightCompleted(this, arg);// 重量稳定事件
                isWeightComplete = true;                
                _count = 0;
            }
        }

        protected override void HandleWeight(object weightObj)
        {
            double weight = (double)weightObj;
            // 保存前一次重量
            double preWeight = _preWeight;
            //// 清零操作
            //if ((weight < _precision && preWeight < _precision) || weight < _minWeight)
            //{
            //    _count = 0;
            //    isWeightComplete = false;                
            //    //return;
            //}       

            //// 称重完成且为稳定值
            //if (Math.Abs(weight - preWeight) <= _precision
            //    && weight < MaxWeight && weight > MinWeight)
            //{
            //    OnWeightCompleted(weight);
            //}
            //else  // 重量变化且不稳定
            //{
            //    isWeightComplete = false;
            //    _preWeight = weight;
            //    OnWeightChange(weight);
            //}
            // 清零操作


            // 称重完成且为稳定值
            if (Math.Abs(weight - preWeight) <= _precision
                && weight < MaxWeight && weight > MinWeight)
            {
                _count++;
                if (_count > MaxCount)
                {
                    OnWeightCompleted(weight);
                }
              
            }
            else // 重量变化且不稳定
            {
                _count = 0;
                isWeightComplete = false;
                _preWeight = weight;
                OnWeightChange(weight);
            }

        }
        #endregion

    }
       
}
