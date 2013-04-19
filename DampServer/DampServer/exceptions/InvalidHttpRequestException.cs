#region

using System;

#endregion

namespace DampServer.exceptions
{
    public class InvalidHttpRequestException : Exception
    {
        public InvalidHttpRequestException(string noGetLine) : base(noGetLine)
        {
        }
    }
}