using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Sip.Client.Printer
{
    public class CheckExcelInstalled
    {
        public static bool isExcelInstalled()
        {
            Type type = Type.GetTypeFromProgID("Excel.Application");
            return type != null;
        }
    }
}
