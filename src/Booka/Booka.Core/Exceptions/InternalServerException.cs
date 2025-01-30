using System.Net;

namespace Booka.Core.Exceptions;

public class InternalServerException(string message)
    : ApplicationException(message, HttpStatusCode.InternalServerError);