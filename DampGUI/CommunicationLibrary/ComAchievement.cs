using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DampCS;
using System.Xml;

namespace CommunicationLibrary
{
    class ComAchievement
    {
        public static XmlElement GetAchievement(string achievementID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("RemoveFriend", new Dictionary<string, string>{{"Achievement", achievementID}}, ComLogin._ComToken);

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

        public static bool SetAchievement(string achievementID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("RemoveFriend", new Dictionary<string, string> { { "Achievement", achievementID } }, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("Status"))
            {
                var xmlNode = ResultFromServerXml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
