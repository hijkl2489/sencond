using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Sip.Client.HLA;
using System.Windows.Forms;
using Core.Sip.Client.Test;
namespace Core.Sip.Client
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
            //Application.Run(new FrmIcCard());
            Application.Run(new FrmPrinter());
        }
    }
}
