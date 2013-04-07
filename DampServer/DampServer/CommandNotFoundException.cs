using System;

namespace Damp
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string cmd) :base(cmd)
        {
            
        }
    }
}