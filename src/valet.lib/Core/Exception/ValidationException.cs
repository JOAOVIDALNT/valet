using System.Net;

namespace valet.lib.Core.Exception;

public class ValidationException(IList<string> errorMessages) : BaseException(errorMessages)
{
    public override HttpStatusCode GetStatusCode()  => HttpStatusCode.BadRequest;
}