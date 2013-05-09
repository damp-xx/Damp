using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameControle;
using GameState;
using Microsoft.Xna.Framework.Content;

namespace GameControle
{
    public interface IFactoryLevel
    {
        ILevel GetLevelOne(IKeybordInput _input, ContentManager manager);

    }
}
