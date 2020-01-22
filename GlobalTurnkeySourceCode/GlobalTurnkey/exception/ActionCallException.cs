using System;
using System.Runtime.Serialization;

namespace GlobalTurnkey.exception
{
    [Serializable]
    public class ActionCallException : Exception
    {
        public ActionCallException()
        {
        }

        public ActionCallException(string message) : base(message)
        {

        }

        public ActionCallException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ActionCallException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}