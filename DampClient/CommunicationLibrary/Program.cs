using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var resultOfLogin = ComLogin.Authenticate("bardyr", "mormor");

            Console.WriteLine("login status is: {0}", resultOfLogin);

            Console.Read();
        }
    }
}