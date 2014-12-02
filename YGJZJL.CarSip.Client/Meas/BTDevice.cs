using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.Meas
{
   public class BTDevice
    {
        public string FS_CODE { get; set; }
        public string FS_NAME { get; set; }
        public string FS_TYPE { get; set; }
        public string FS_PARAM { get; set; }
        public string FS_DESC { get; set; }
        public BTDevice()
        {
            FS_CODE = "";
            FS_NAME = "";
            FS_TYPE = "";
            FS_PARAM = "";
            FS_DESC = "";
        }
    }
}
