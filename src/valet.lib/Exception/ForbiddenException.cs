using System.Net;

namespace valet.lib.Exception
{
    public class ForbiddenException : AppBaseException
    {
        public ForbiddenException(string message) : base(message) { }

        public override IList<string> GetErrorMessages()
        {
            throw new NotImplementedException();
        }

        public override HttpStatusCode GetStatusCode()
        {
            throw new NotImplementedException();
        }
    }
}
