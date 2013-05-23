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
        public static ObservableCollection<string> GetAllAchievementForGame(string GameId)
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


        public static ObservableCollection<string> GetMyAchievementForGame(string GameId)
        {
            ObservableCollection<string> TempAchievement = new ObservableCollection<string>();
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetGameMyAchievement", new Dictionary<string, string> { { "GameId", GameId } }, ComLogin._ComToken);

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

        public static bool AddAchievement(string achievementTitle, string GameId)
        {
            Dictionary<string, string> AchievementsForGame = new Dictionary<string, string>();

            AchievementsForGame = GetAchievementForGame(GameId);
            string achievementID = "0";
            foreach (var s in AchievementsForGame)
            {
                if (s.Value == achievementTitle) 
                achievementID = s.Key;
            }

            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("AddAchievement", new Dictionary<string, string> { { "AchievementId", achievementID } }, ComLogin._ComToken);

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

        private static Dictionary<string, string> GetAchievementForGame(string GameId)
        {
            Dictionary<string, string> TempAchievement = new Dictionary<string, string>();
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetAchievementsForGame", new Dictionary<string, string> { { "GameId", GameId } }, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("Achievement"))
            {
                foreach (XmlElement a in ResultFromServerXml.GetElementsByTagName("Archivement"))
                {
                    TempAchievement.Add(a.GetElementsByTagName("ArcheivementId").Item(0).InnerText, a.GetElementsByTagName("Title").Item(0).InnerText);
                }
                return TempAchievement;
            }
            return null;
        }

        public static bool UpdateHighscore(string gameId, string score)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("UpdateScore", new Dictionary<string, string> { { "GameId", gameId }, { "Score", score } }, ComLogin._ComToken);

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

        public static bool GetHighscore(string gameId)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetScore", new Dictionary<string, string> { { "GameId", gameId } }, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("Score"))
            {
                return true;
            }
            return false;
        }
    }
}
