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

            if (ResultFromServerXml.Name.Equals("User"))
            {
                return ResultFromServerXml;
            }
            return null;
        }
        /*
        public static void UpdateProfile()
        {
            //TO DO implment update user profile
        }*/

        public static string GetProfileName()
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var profile = client.SendRequest("GetMyUser", new Dictionary<string, string> { { "", "" } }, ComLogin._ComToken);
            if (profile.Name.Equals("User"))
            {
                XmlNode result = profile.GetElementsByTagName("Username").Item(0);

                if (result != null)
                    return result.InnerText;
           
            return "No username found";
            }

        }
        

    }
}
