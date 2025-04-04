using System.Net;

namespace valet.lib.Exception
{
    public abstract class AppBaseException : SystemException
    {
        public AppBaseException(string message) : base(message) { }

        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
