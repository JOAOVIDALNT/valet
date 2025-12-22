using System.Net;

namespace valet.lib.Core.Exception
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(message) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
