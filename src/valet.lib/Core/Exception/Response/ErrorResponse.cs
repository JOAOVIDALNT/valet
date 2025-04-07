namespace valet.lib.Core.Exception.Response
{
    public class ErrorResponse
    {
        public IReadOnlyList<string> ErrorMessages { get; }
        public bool TokenIsExpired { get; set; }

        public ErrorResponse(IEnumerable<string> errorMessages) =>
            ErrorMessages = errorMessages.ToList().AsReadOnly();

        public ErrorResponse(string errorMessage) =>
            ErrorMessages = new List<string> { errorMessage }.AsReadOnly();

        public ErrorResponse(System.Exception ex) =>
            ErrorMessages = new List<string> { ex.Message }.AsReadOnly();
    }
}
