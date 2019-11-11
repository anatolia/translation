using System;

namespace Translation.Common.Exceptions
{
    public class JournalException : Exception
    {
        public JournalException(string message) : base(message)
        {
        }
    }
}