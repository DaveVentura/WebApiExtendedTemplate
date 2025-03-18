using MongoDB.Bson;

namespace WebApiExtendedTemplate.Contracts.Responses {
    public class PostResponse {
        public ObjectId Id;
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
