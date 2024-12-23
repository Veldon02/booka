using System.Net;

namespace Booka.Core.Exceptions;

public class UnauthorizedException(string message)
    : ApplicationException(message, HttpStatusCode.Unauthorized);