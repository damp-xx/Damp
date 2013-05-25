using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampGUI
{
    public interface IGames
    {
        void Add(IGame aGame);
        IGame Get(int i);
        IGame CurrentGame { get; set; }
        int CurrentIndex { get; set; }
        int TotalGames { get; }
    }
}
