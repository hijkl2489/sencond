using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Sip.Client.Meas;
using Core.Sip.Client.App;

namespace Core.Sip.Client.Test
{
    public partial class FrmPrinter : Form
    {
        //BTDevice[] _devs = null;
        //CoreCabinet _cab = null;
        CorePrinter _printer = null;
        //HighWireLable _lable = null;
        public FrmPrinter()
        {
            InitializeComponent();
            _printer = new CorePrinter();
            _printer.Data = new HgLable();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            //_cab.Printer.PrintData();
            //_cab.Printer.TestPrintChinese();
            _printer.Print();
        }

        private void FrmPrinter_Load(object sender, EventArgs e)
        {
            _printer.Data.BatchNo = "P2032774";
            _printer.Data.BandNo = "12";
            _printer.Data.Date = DateTime.Now;
            
            
            _printer.Data.Standard = "GB 1499.2-2007";
            _printer.Data.SteelType = "HRB400E";
            _printer.Data.Spec = "18";
            _printer.Data.Length = "9";

            _printer.Data.Weight = "2743";
            _printer.Data.Count = "160";
            
            //_printer.Data.PrintStandard = "0";
            //_printer.Data.PrintSteelType = "1";
            
            _printer.Data.Term = "1";
            _printer.Data.BarCode = "402111"+_printer.Data.BatchNo;
            _printer.Data.PrintAddress = true;
            _printer.Data.Type = LableType.BIG;
        

            //_devs = new BTDevice[1];
            //_devs[0] = new BTDevice();
            //_devs[0].FS_CODE = "P001";
            //_devs[0].FS_PARAM = "10.25.3.16";
            //_devs[0].FS_TYPE = "ZPL";
            //_cab = new CoreCabinet();
            //_cab.Params = _devs;
            //_cab.Init();
            //_cab.Run();
            
        }
    }
}
