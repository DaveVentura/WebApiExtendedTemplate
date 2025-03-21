namespace WebApiExtendedTemplate.Contracts.Requests {
    public class PublicationRequest {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required int YearPublished { get; set; }
    }

    public enum PublicationType {
        Book,
        Article,
        Magazine,
        ResearchPaper,
        Thesis,
        ConferencePaper
    }

}
