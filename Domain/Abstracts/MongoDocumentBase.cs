using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiExtendedTemplate.Domain.Abstracts {
    public abstract class MongoDocumentBase {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
