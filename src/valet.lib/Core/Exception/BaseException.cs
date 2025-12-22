using System.Net;

namespace valet.lib.Core.Exception
{
    /// <summary>
    /// Represents the base class for custom exceptions in the application.
    /// </summary>
    /// <remarks>
    /// This abstract class defines a consistent structure for all domain-specific exceptions,
    /// enforcing the implementation of error message retrieval and corresponding HTTP status code.
    /// </remarks>
    public abstract class BaseException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BaseException(string message) : base(message) { }

        /// <summary>
        /// Retrieves a list of error messages associated with the exception.
        /// </summary>
        /// <returns>A list of strings containing error messages.</returns>
        public abstract IList<string> GetErrorMessages();

        /// <summary>
        /// Gets the HTTP status code that should be returned for this exception.
        /// </summary>
        /// <returns>The corresponding <see cref="HttpStatusCode"/>.</returns>
        public abstract HttpStatusCode GetStatusCode();
    }
}
