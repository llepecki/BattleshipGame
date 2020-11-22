using System;
using System.Runtime.Serialization;

namespace Com.Lepecki.BattleshipGame.Engine.Exceptions
{
    public class RuleViolationException : ApplicationException
    {
        public RuleViolationException()
        {
        }

        public RuleViolationException(string? message) : base(message)
        {
        }

        public RuleViolationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RuleViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
