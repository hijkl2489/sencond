using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.App
{
    public interface IApplication
    {
        int Init(int m_iSelectedPound);
        void Run();
        int Finit();
    }
}
