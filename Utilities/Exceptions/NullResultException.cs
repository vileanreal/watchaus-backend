using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Exceptions
{

    [Serializable]
    public class NullResultException : Exception
    {
        public NullResultException()
            : base("The result is null.")
        {
        }

        public NullResultException(string message)
            : base(message)
        {
        }

        public NullResultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NullResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
