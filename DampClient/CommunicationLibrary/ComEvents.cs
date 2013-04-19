using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DampCS;

namespace CommunicationLibrary
{
    class ComEvents
    {
        public static string _ComToken { get; set; }
        public static string _ComIp { get; private set; }
        public static void Listen()
        {
            var client = new DampServerClient(_ComIp);

            client.Listen(_ComToken);

        }
    }
}
