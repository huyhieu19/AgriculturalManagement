using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class InstrumentValueByFiveSecondEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Topic")]
        public string? Topic { get; set; }
        [BsonElement("PayLoad")]
        public string? PayLoad { get; set; }
        [BsonElement("ValueDate")]
        public DateTime? ValueDate { get; set; } = DateTime.Now;
    }
}