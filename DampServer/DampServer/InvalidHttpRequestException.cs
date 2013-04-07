#region

using System;

#endregion

namespace Damp
{
    public class InvalidHttpRequestException : Exception
    {
        public InvalidHttpRequestException(string noGetLine) : base(noGetLine)
        {
        }
    }
}