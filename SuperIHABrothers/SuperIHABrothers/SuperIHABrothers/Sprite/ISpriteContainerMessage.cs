using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprites;

namespace SuperIHABrothers.Sprite
{
    public interface ISpriteContainerMessage
    {
        void AddSprite(ISprite sprite);
    }
}
