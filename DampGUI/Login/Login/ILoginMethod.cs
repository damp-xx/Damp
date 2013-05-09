using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    interface ILoginMethod
    {
        bool Login(string username, string password);
    }
}
