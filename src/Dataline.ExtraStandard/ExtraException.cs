using System;

namespace ExtraStandard
{
    public class ExtraException : Exception
    {
        public ExtraException()
        {
        }

        public ExtraException(string message) : base(message)
        {
        }

        public ExtraException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
