using System;

namespace Translation.Common.Models.Base
{
    public abstract class BaseRequest
    {
        public void ThrowArgumentException(string argumentName, object value)
        {
            throw new ArgumentException(argumentName + " => " + value);
        }
    }
}