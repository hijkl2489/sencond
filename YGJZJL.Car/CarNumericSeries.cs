using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.UltraChart.Resources;
using Infragistics.UltraChart.Resources.Appearance;

namespace YGJZJL.Car
{
    public class CarNumericSeries : NumericSeries
    {
        public string PointsString
        {
            get { return this.ConvertPointToString(); }
            set { this.SetPoints(value); }
        }

        public CarNumericSeries() : base()
        {
        }

        public CarNumericSeries(IChartComponent chartComponent) : base(chartComponent)
        {

        }

        private string ConvertPointToString()
        {
            string result = "";

            if (this.Points != null)
            {
                for (int i = 0; i < this.Points.Count; i++)
                {
                    if (i == 0)
                    {
                        result += this.Points[i].Value.ToString();
                    }
                    else
                    {
                        result += "," + this.Points[i].Value.ToString();
                    }
                }
            }

            return result;
        }

        private void SetPoints(string param)
        {
            try
            {
                this.Points.Clear();
                NumericDataPoint np = null;
                string[] args = param.Split(new char[] { ',' });
                foreach (string str in args)
                {
                    np = new NumericDataPoint();
                    np.Value = double.Parse(str.Trim());
                    this.Points.Add(np);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
