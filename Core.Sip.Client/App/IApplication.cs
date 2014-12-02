using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Sip.Client.App
{
    public interface IApplication
    {
        int Init();
        void Run();
        int Finit();
    }
}
