using System.Net;

namespace valet.lib.Core.Exception
{
    public class UnauthorizedException(string message) : BaseException(message)
    {
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
