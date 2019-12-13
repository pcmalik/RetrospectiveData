using System;

namespace RetrospectiveDataApi.Exceptions
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message)
            : base(message)
        {
        }
    }
}
