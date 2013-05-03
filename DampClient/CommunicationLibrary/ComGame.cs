using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DampCS;

namespace CommunicationLibrary
{
    class ComGame
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
            var ResultFromServerXml = client.SendRequest("RemoveFriend", new Dictionary<string, string>{{"GameId", gameID}}, ComLogin._ComToken);

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

        public static XmlElement GetAllGameList()
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("GetAllGames", new Dictionary<string, string> { { "", "" } }, ComLogin._ComToken);

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
            var ResultFromServerXml = client.SendRequest("GetMyGames", new Dictionary<string, string> { { "Id", GameID } }, ComLogin._ComToken);

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
