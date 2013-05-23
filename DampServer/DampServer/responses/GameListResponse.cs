using System.Collections.Generic;
using System.Xml.Serialization;

namespace DampServer.responses
{
    [XmlRoot(ElementName = "GameList")]
    public class GameListResponse : XmlResponse
    {
        public List<Game> Games { get; set; }
    }
}