using System;
using System.Runtime.Serialization;

namespace Com.Lepecki.BattleshipGame.Engine.Exceptions
{
    public class InvalidPlayerException : ApplicationException
    {
        public InvalidPlayerException()
        {
        }

        public InvalidPlayerException(string? message) : base(message)
        {
        }

        public InvalidPlayerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidPlayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
