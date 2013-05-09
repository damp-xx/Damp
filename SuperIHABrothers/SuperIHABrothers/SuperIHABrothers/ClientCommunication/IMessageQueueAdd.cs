using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientCommunication
{
    public interface IMessageQueueAdd
    {
        void InsertMessage(string message);
    }
}
