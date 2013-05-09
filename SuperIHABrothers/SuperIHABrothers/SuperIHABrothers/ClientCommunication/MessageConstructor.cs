using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ClientCommunication
{
    public class MessageConstructor : IMessageConstructor
    {
        private IClientCommunication_Game _communication;

        public MessageConstructor(IClientCommunication_Game communication)
        {
            _communication = communication;
        }

        public void StartUp()
        {
            _communication.Send("GHS");
            _communication.Send("GPN");
        }

        public void Achievement(string message)
        {
            _communication.Send("ACH:" + message);
        }

        public void NewHighscore(string highscore)
        {
            _communication.Send("NHS" + highscore);
        }
    }
}
