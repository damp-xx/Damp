using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientCommunication;
using Microsoft.Xna.Framework.Content;

namespace GameControle
{
    public interface IGameFactory
    {
        Game GetGame(ContentManager content, IPlayerDataGame mDataGame, IMessageConstructor messageConstructor);
    }
}
