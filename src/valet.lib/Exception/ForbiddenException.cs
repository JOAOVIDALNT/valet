using System.Net;

namespace valet.lib.Exception
{
    public class ForbiddenException : AppBaseException
    {
        public ForbiddenException(string message) : base(message) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
