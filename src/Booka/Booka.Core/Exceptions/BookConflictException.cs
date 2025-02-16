using System.Net;

namespace Booka.Core.Exceptions;

public class BookConflictException(string message)
    : ApplicationException(message, HttpStatusCode.Conflict)
{
}