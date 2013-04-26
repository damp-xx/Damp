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
        public static void Listen()
        {
            var client = new DampServerClient(ComLogin._ComIp);

            client.Listen(ComLogin._ComToken);

        }
    }
}
