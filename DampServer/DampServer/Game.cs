using System.Collections.Generic;
using System.Xml.Serialization;

namespace DampServer
{
    public class Game : XmlResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }

        [XmlElement("Picture")]
        public List<string> Pictures { get; set; }

        public string Genre { get; set; }
        public int RecommendedAge { get; set; }
        public string Developer { get; set; }

        public List<Archivement> Archivements { get; set; }
        public string Path { get; set; }

        public string Language { get; set; }
        public string Mode { get; set; }


        public override string ToString()
        {
            return Title;
        }
    }
}