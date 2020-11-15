using System;
using System.Runtime.Serialization;

namespace Com.Lepecki.BattleshipGame.Engine.Exceptions
{
    public class RandomPlacementException : ApplicationException
    {
        public RandomPlacementException()
        {
        }

        public RandomPlacementException(string? message) : base(message)
        {
        }

        public RandomPlacementException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RandomPlacementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
