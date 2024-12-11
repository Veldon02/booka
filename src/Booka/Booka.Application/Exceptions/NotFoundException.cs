using System.Net;

namespace Booka.Application.Exceptions;

public class NotFoundException(string message)
    : ApplicationException(message, HttpStatusCode.NotFound);