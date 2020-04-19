using System;
using System.Runtime.Serialization;

namespace AOP.Sorting.Models
{
    public class TypeNotAllowedException : Exception
    {
        public TypeNotAllowedException()
        {
        }

        public TypeNotAllowedException(string message) : base(message)
        {
        }

        public TypeNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
