using System;
using System.Collections.Generic;

namespace Zinger.Exceptions
{
    public class ParametersMissingException : Exception
    {
        public ParametersMissingException(IEnumerable<string> paramNames) : base($"Missing required parameter(s): {string.Join(", ", paramNames)}")
        {
        }
    }
}
