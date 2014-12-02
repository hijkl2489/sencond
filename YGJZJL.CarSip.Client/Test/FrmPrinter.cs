using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.CarSip.Client.Meas;
using YGJZJL.CarSip.Client.App;

namespace YGJZJL.CarSip.Client.Test
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
            _printer.Data.SteelType = "HRB400G";
            _printer.Data.Spec = "8";
            _printer.Data.Length = "9";

            _printer.Data.Weight = "2743";
            _printer.Data.Count = "160";
            
            //_printer.Data.PrintStandard = "0";
            //_printer.Data.PrintSteelType = "1";
            
            _printer.Data.Term = "1";
            _printer.Data.BarCode = "402P1204125204";
            _printer.Data.PrintAddress = true;
           
            // 汽车
            _printer.Data.ContractNo = "B0100280";
            _printer.Data.SupplierName = "淡水河谷";
            _printer.Data.Receiver = "昆明钢铁";
            _printer.Data.MaterialName = "铁矿石";
            _printer.Data.TransName = "";
            _printer.Data.CarNo = "云G-TA1512";
            _printer.Data.StoveNo = "";//G1-12008
            _printer.Data.MillComment = "M01";
            _printer.Data.PlanSpec = "12×7";
            _printer.Data.GrossWeight = "12";
            _printer.Data.TareWeight = "10";
            _printer.Data.NetWeight = "10";
            _printer.Data.WeightPoint = "1#汽车衡";            
            _printer.Data.CarComment = "备注信息";
            _printer.Data.Rate = "10%";
            _printer.Data.DeductWeight = "1";
            _printer.Data.DeductAfterWeight = "9";
            _printer.Data.WeightNum = "";
            _printer.Data.Charge = "100";
            // 
            _printer.Copies = 1;
            _printer.Data.Type = LableType.CAR;
            _printer.PrinterName = "EPSON BA-T500 Receipt";//"Zebra 105SL"; //"Microsoft XPS Document Writer"; //
             

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
