using System.Net;

namespace Booka.Core.Exceptions;

public class ApplicationException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    protected ApplicationException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}