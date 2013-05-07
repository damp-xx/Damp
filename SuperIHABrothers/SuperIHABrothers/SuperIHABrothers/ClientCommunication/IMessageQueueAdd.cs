using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperIHABrothers.ClientCommunication
{
    interface IMessageQueueAdd
    {
        void InsertMessage(string message);
    }
}
