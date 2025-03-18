namespace WebApiExtendedTemplate.Services {
    public class UriService {
        private readonly string _baseUri;
        public UriService(string baseUri) {
            _baseUri = baseUri;
        }

        public Uri GetUri(string path) {
            return new Uri(_baseUri + path);
        }
    }
}
