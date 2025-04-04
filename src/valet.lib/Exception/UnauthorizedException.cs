using System.Net;

namespace valet.lib.Exception
{
    public class UnauthorizedException : AppBaseException
    {
        public UnauthorizedException(string message) : base(message) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
