#region

using System;

#endregion

namespace DampServer
{
    public class InvalidHttpRequestException : Exception
    {
        public InvalidHttpRequestException(string noGetLine) : base(noGetLine)
        {
        }
    }
}