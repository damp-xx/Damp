using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DampServer.responses
{
    [XmlRoot(ElementName = "Achievement")]
    public class AchievementListResponse : XmlResponse
    {
        public List<Archivement> Achievement { get; set; }
    }
}
