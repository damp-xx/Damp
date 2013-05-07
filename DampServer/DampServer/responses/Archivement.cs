
namespace DampServer
{
    public class Archivement : XmlResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public long ArcheivementId { get; set; }
        public long GameId { get; set; }
    }
}