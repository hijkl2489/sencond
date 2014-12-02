using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.Meas
{
    interface IDevice
    {
         bool Open();
         bool Close();
         
         //string GetType();
         
         //string Read();
         //int Write(string data);
    }
}
