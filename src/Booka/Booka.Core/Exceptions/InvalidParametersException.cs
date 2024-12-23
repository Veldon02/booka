using System.Net;

namespace Booka.Core.Exceptions;

public class InvalidParametersException(string message) :
    ApplicationException(message, HttpStatusCode.BadRequest);