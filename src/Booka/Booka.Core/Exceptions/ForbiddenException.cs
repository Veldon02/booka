using System.Net;

namespace Booka.Core.Exceptions;

public class ForbiddenException(string message) :
    ApplicationException(message, HttpStatusCode.Forbidden);