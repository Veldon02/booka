using System.Net;

namespace Booka.Core.Exceptions;

public class InvalidTokenException(string message)
    : ApplicationException(message, HttpStatusCode.Unauthorized);