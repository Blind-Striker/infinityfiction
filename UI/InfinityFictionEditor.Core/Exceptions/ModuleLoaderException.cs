using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using CodeFiction.DarkMatterFramework.CoreExceptions;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Exceptions
{
    [Serializable]
    public class ModuleLoaderException : CriticalUserLevelException
    {
        public ModuleLoaderException(string exceptionMessage, Exception exp)
            : base(exceptionMessage, exp)
        {
        }

        public ModuleLoaderException(Exception exp)
            : base(exp)
        {
        }

        public ModuleLoaderException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }

        public ModuleLoaderException()
        {
        }

        protected ModuleLoaderException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
