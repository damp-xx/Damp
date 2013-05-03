using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DampCS;

namespace CommunicationLibrary
{
    class ComProfile
    {
        public static XmlElement GetProfile()
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetMyUser", new Dictionary<string, string>{{"", ""}}, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("Status"))
            {
                var xmlNode = ResultFromServerXml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    return ResultFromServerXml;
                }
            }
            return null;
        }
        /*
        public static void UpdateProfile()
        {
            
        }*/
        

    }
}
