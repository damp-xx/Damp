using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DampCS;
using DampGUI;


namespace CommunicationLibrary
{
    public class ComGame
    {
        public static XmlElement GetMyGameList()
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetMyGames", new Dictionary<string, string> {{"", ""}},
                                                         ComLogin._ComToken);

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

        public static XmlElement GetGameInfo(string gameID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GameInfo", new Dictionary<string, string> {{"GameId", gameID}},
                                                         ComLogin._ComToken);

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

        public static XmlElement SearchGame(string searchString)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GameSearch",
                                                         new Dictionary<string, string> {{"Query", searchString}},
                                                         ComLogin._ComToken);

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

        public Games _games { get; set; }

        public void GetAllGameList()
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetAllGames", new Dictionary<string, string> {{"", ""}},
                                                         ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("GameList"))
            {
                foreach (XmlElement x in ResultFromServerXml.GetElementsByTagName("Game"))
                {
                    List<string> urls = new List<string>();

                    Game k = new Game
                        {
                            Title = x.GetElementsByTagName("Title").Item(0).InnerText,
                            Description = x.GetElementsByTagName("Description").Item(0).InnerText,
                            Genre = x.GetElementsByTagName("Genre").Item(0).InnerText,
                            Developer = x.GetElementsByTagName("Developer").Item(0).InnerText,
                            Mode = x.GetElementsByTagName("Mode").Item(0).InnerText,
                            Language = x.GetElementsByTagName("Language").Item(0).InnerText,
                            AchivementsGame = ComAchievement.GetAllAchievementForGame(x.GetElementsByTagName("Id").Item(0).InnerText),
                        };
                    List<string> PhotoAdd = new List<string>();
                    foreach (XmlElement xxx in x.GetElementsByTagName("Picture"))
                    {
                        PhotoAdd.Add("Https://" + ComLogin._ComIp + ":1337/Download?AuthToken=" +
                                     ComLogin._ComToken + "&Object=" + xxx.InnerText);

                    }
                    k.PhotoCollection = new PhotoCollection(PhotoAdd);
                    _games.Add(k);
                }
            }
        }

        public static XmlElement BuyGame(string GameID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("BuyGame", new Dictionary<string, string> {{"Id", GameID}},
                                                         ComLogin._ComToken);

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

        public static XmlElement GetGame(string GameID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetGames", new Dictionary<string, string> {{"Id", GameID}},
                                                         ComLogin._ComToken);

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
    }
}
