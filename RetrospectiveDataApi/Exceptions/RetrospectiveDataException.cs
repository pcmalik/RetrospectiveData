using System;

namespace RetrospectiveDataApi.Exceptions
{
    /// <summary>
    /// Instance of RetrospectiveDataException will be returned to client in case of bad request - i.e 400 error code
    /// whereas instance of Exception will be returned to client in case of internal server error - i.e 500 erro code
    /// </summary>
    public class RetrospectiveDataException : Exception
    {
        public RetrospectiveDataException(string message)
            : base(message)
        {
        }
    }
}
