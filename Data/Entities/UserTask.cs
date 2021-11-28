using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entities
{
    public class UserTask
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
