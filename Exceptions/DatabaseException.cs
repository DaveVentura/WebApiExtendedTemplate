namespace DaveVentura.WebApiExtendedTemplate.Exceptions {
    public class DatabaseException : ApiException {
        public DatabaseException(string message, int httpStatusCode = 500) : base(message, httpStatusCode) {
        }
    }
}
