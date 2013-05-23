using System.Collections.Generic;
using System.Xml.Serialization;

namespace DampServer.responses
{
    [XmlRoot(ElementName = "HighScoreCommand")]
    public class HighScoreResponse : XmlResponse
    {
        public int GameId { get; set; }
        public List<User> UserScores { get; set; }
    }
}