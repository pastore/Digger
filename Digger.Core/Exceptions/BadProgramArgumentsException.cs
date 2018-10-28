using System;

namespace Digger.Core.Exceptions
{
    public class BadProgramArgumentsException : Exception
    {
        public BadProgramArgumentsException(string message): base(message)
        { }
    }
}
