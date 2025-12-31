using System.Net;

namespace valet.lib.Core.Exception
{
    public class ForbiddenException(string message) : BaseException(message)
    {
        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
