using System;

namespace DampServer
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string cmd) :base(cmd)
        {
            
        }
    }
}