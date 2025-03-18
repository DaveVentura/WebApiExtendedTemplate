namespace WebApiExtendedTemplate.Contracts.Responses {
    public class PublicationResponse {
        public required string Id { get; set; }
        public required string PublicationType { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required int YearPublished { get; set; }
    }
}
