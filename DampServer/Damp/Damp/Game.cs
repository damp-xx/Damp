using System.Collections.Generic;

namespace Damp
{
    public class Game : XmlResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }
        public string Picture { get; set; }
        public string Genre { get; set; }
        public string Resume { get; set; }
        public string RecommendedAge { get; set; }
        public string Developer { get; set; }
        public List<Archivement> Archivements { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}