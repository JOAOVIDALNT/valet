using System.Net;

namespace valet.lib.Core.Exception;

public class ValidationException(IList<string> errorMessages) : BaseException(string.Empty)
{
    private IList<string> ErrorMessages { get; } = errorMessages;
    public override IList<string> GetErrorMessages() => ErrorMessages;
    public override HttpStatusCode GetStatusCode()  => HttpStatusCode.BadRequest;
}