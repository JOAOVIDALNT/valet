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
        private readonly IReadOnlyList<string> _errorMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected BaseException(string message)
            : base(message)
        {
            _errorMessages = new List<string> { message };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class with a specified error message and inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        protected BaseException(string message, System.Exception innerException)
            : base(message, innerException)
        {
            _errorMessages = new List<string> { message };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class with a list of error messages.
        /// </summary>
        /// <param name="messages">The list of messages that describe the error.</param>
        protected BaseException(IEnumerable<string> messages)
            : base(messages != null ? string.Join("; ", messages) : string.Empty)
        {
            _errorMessages = messages?.ToList() ?? new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class with a list of error messages and an inner exception.
        /// </summary>
        /// <param name="messages">The list of messages that describe the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        protected BaseException(IEnumerable<string> messages, System.Exception innerException)
            : base(messages != null ? string.Join("; ", messages) : string.Empty, innerException)
        {
            _errorMessages = messages?.ToList() ?? new List<string>();
        }

        /// <summary>
        /// Retrieves a list of error messages associated with the exception.
        /// </summary>
        /// <returns>A read-only list of strings containing error messages.</returns>
        public virtual IReadOnlyList<string> GetErrorMessages() => _errorMessages;

        /// <summary>
        /// Gets the HTTP status code that should be returned for this exception.
        /// </summary>
        /// <returns>The corresponding <see cref="HttpStatusCode"/>.</returns>
        public abstract HttpStatusCode GetStatusCode();
    }
}
