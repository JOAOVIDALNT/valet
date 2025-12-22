namespace valet.lib.Core.Exception.Response
{
    /// <summary>
    /// Represents a standardized error response with one or more error messages.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets the list of error messages returned in the response.
        /// </summary>
        public IReadOnlyList<string> ErrorMessages { get; }

        /// <summary>
        /// Gets or sets a flag indicating whether the error was caused by an expired token.
        /// </summary>
        public bool TokenIsExpired { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class with multiple error messages.
        /// </summary>
        /// <param name="errorMessages">A collection of error messages.</param>
        public ErrorResponse(IEnumerable<string> errorMessages) =>
            ErrorMessages = errorMessages.ToList().AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class with a single error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public ErrorResponse(string errorMessage) =>
            ErrorMessages = new List<string> { errorMessage }.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class based on an exception.
        /// </summary>
        /// <param name="ex">The exception that caused the error.</param>
        public ErrorResponse(System.Exception ex) =>
            ErrorMessages = new List<string> { ex.Message }.AsReadOnly();
    }
}
