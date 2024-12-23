using System.Net;

namespace Booka.Core.Exceptions;

public class NotFoundException(string message)
    : ApplicationException(message, HttpStatusCode.NotFound);