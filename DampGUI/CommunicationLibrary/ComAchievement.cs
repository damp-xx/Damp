using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DampCS;
using System.Xml;

namespace CommunicationLibrary
{
    public class ComAchievement
    {
        public static ObservableCollection<string> GetAchievement(string GameId)
        {
            ObservableCollection<string> TempAchievement = new ObservableCollection<string>();
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetAchievementsForGame", new Dictionary<string, string>{{"GameId", GameId}}, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("Achievement"))
            {
                foreach (XmlElement a in ResultFromServerXml.GetElementsByTagName("Archivement"))
                {
                    TempAchievement.Add(a.GetElementsByTagName("Title").Item(0).InnerText);
                }
                return TempAchievement;
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
