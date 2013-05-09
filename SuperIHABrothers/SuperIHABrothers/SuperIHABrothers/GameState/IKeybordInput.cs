using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameState
{
    

    public interface IKeybordInput
    {
        bool IsLeftPressed { get; }
        bool WasLeftPressed { get; }
        bool IsRightPressed { get; }
        bool WasRightPressed { get; }
        bool IsJumpPressed { get; }
        bool WasJumpPressed { get; }
    }
}
