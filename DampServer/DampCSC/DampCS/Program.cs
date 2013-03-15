using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new DampServerClient("localhost");
            Console.WriteLine(client.Login("Bardyr", "mormor") ? "User logged in" : "User not logged in");

          //  client.SendRequest("Chat", new Dictionary<string, string>{{"To", "1"}, {"Message", "1337"}});

          //  client.Listen();

          //  client.SendRequest("GetUser", new Dictionary<string, string>{ {"userId", "1337"}});
              client.SendRequest("GetMyUser", new Dictionary<string, string>());


            Console.Read();
        }
    }
}
