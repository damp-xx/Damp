using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperIHABrothers.GameState
{
    

    public interface IKeybordInput
    {
        bool IsLeftPressed { get; }
        bool WasLeftPressed { get; }
        bool IsRightPressed { get; }
        bool WasRightPressed { get; }
        bool IsSpacePressed { get; }
        bool WasSpacePressed { get; }
    }
}
