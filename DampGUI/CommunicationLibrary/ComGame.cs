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
            var ResultFromServerXml = client.SendRequest("GetMyGames", new Dictionary<string, string>{{"", ""}}, ComLogin._ComToken);

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
            var ResultFromServerXml = client.SendRequest("GameInfo", new Dictionary<string, string>{{"GameId", gameID}}, ComLogin._ComToken);

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
            var ResultFromServerXml = client.SendRequest("GameSearch", new Dictionary<string, string> { { "Query", searchString } }, ComLogin._ComToken);

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

        public static void GetAllGameList(Games games)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetAllGames", new Dictionary<string, string> { { "", "" } }, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("GameList"))
            {
                foreach (XmlElement x in ResultFromServerXml.GetElementsByTagName("Game"))
                {

                    List<string> urls = new List<string>();
                    urls.Add("http://www.securelist.com/en/images/vlpub/0807_online_games_pict01.png");
                    //urls.Add("http://www.hdwallpapers.in/walls/angry_birds_space_game-HD.jpg");
                    //urls.Add("http://www.shortfacts.com/wp-content/uploads/2013/01/halo-3-video-game-1031.jpg");
                    //urls.Add("http://files.all-free-download.com/downloadfiles/wallpapers/1920_1200_widescreen/super_mario_galaxy_4_wallpaper_super_mario_games_wallpaper_1920_1200_widescreen_3159.jpg");
                    //urls.Add("http://images4.fanpop.com/image/photos/22700000/Video-Game-Collages-video-games-22728041-1024-768.jpg");
                    Game k = new Game
                        {
                            Title = x.GetElementsByTagName("Title").Item(0).InnerText,
                            Description = x.GetElementsByTagName("Description").Item(0).InnerText,
                            Genre = x.GetElementsByTagName("Genre").Item(0).InnerText,
                            Developer = x.GetElementsByTagName("Developer").Item(0).InnerText,
                            Mode = x.GetElementsByTagName("Mode").Item(0).InnerText,
                            Language = x.GetElementsByTagName("Language").Item(0).InnerText,
                            AchivementsGame = ComAchievement.GetAchievement(x.GetElementsByTagName("Id").Item(0).InnerText),
                       
                            PhotoCollection = new PhotoCollection(urls)
                        };
                    Console.WriteLine(k.Title);
                    games.Add(k);

                }
            }
         
        }

        public static XmlElement BuyGame(string GameID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("BuyGame", new Dictionary<string, string> { { "Id", GameID } }, ComLogin._ComToken);

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
            var ResultFromServerXml = client.SendRequest("GetGames", new Dictionary<string, string> { { "Id", GameID } }, ComLogin._ComToken);

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
