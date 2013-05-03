using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DampServer.responses
{
    [XmlRoot(ElementName = "FriendSearchResponse")]
    public class FriendSearchResponse : XmlResponse
    {
        public int Code { get; set; }
        public List<User> Users { get; set; }
    }
}
