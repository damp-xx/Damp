﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientCommunication
{
    public interface IMessageConstructor
    {
        void StartUp();
        void Achievement(string message);
        void NewHighscore(string highscore);      
    }
}
