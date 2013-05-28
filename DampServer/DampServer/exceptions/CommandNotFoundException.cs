using System;

namespace DampServer.exceptions
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string cmd) : base(cmd)
        {
        }
    }
}