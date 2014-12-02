using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YGJZJL.CarSip.Client.HLA;
using System.Windows.Forms;
using YGJZJL.CarSip.Client.Test;
namespace YGJZJL.CarSip.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmPrinter());
            //Application.Run(new FrmIoLogic());
            Application.Run(new FrmIcCard());
            //Application.Run(new FrmRtu());
            //Application.Run(new FrmDvr());
            //Application.Run(new FrmLCD());
        }
    }
}
