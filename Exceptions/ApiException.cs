namespace WebApiExtendedTemplate.Exceptions {
    public class ApiException : Exception {
        public override string Message { get; }
        public int HttpStatusCode { get; set; }
        public ApiException(string message, int httpStatusCode = StatusCodes.Status500InternalServerError) {
            this.Message = message;
            this.HttpStatusCode = httpStatusCode;
        }
    }
}
