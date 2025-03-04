using DaveVentura.WebApiExtendedTemplate.Domain.Abstracts;

namespace DaveVentura.WebApiExtendedTemplate.Domain.Documents {
    public class Post : MongoDocumentBase {

        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
