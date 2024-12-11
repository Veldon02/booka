using System.Net;

namespace Booka.Application.Exceptions;

public class ApplicationException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    protected ApplicationException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}