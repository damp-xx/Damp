using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ConnectionLibrary;

namespace DampCS
{
    interface IDampServerClient
    {
        bool Login(string username, string password, out string _authToken);

        void Listen(string _authToken, IEventParser HandleParser);

        XmlElement SendRequest(string command, Dictionary<string, string> parameters, string _authToken);
    }
}
