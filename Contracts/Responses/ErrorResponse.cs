namespace WebApiExtendedTemplate.Contracts.Responses {
    public class ErrorResponse {
        public int StatusCode { get; set; }
        public string Message { get; set; } = "An error occured.";
        public List<string> Errors { get; set; } = [];
        public string? TraceId { get; set; }
        public string? StackTrace { get; set; }
    }
}
