using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommunicationLibrary
{
    interface IEvents
    {
        void Action(XmlElement Event);
    }
}
