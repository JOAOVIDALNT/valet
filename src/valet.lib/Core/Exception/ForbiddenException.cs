using System.Net;

namespace valet.lib.Core.Exception
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message) : base(message) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
