using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLibrary;

namespace DampGUI
{
    class EventSubscriber : IEventSubscribe
    {
        private Dictionary<string, ChatView> ChatIdentyfier = new Dictionary<string, ChatView>();
        public void HandleNewChatMessage(string from, string message)
        {
            Console.WriteLine("HandleNewChatMessage");
            Console.WriteLine(from+message);
            //if (ChatIdentyfier.ContainsKey(from))
            //{
            //    Console.WriteLine("from known");
            //    var d = ChatIdentyfier[from];
            //    d.SendData(message);
            //}
            //else
            //{
            //    Console.WriteLine("from unknown");
            //  //  var d = new ChatView(from);
            // //   d.SendData(message);
            //}
        }

        public void NewChat(string from, ChatView Instance)
        {
            if (!ChatIdentyfier.ContainsKey(from))
            {
                ChatIdentyfier.Add(from, Instance);
            }
        }
    }
}
