using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class InstrumentValueByFiveSecondEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("DeviceId")]
        public string? DeviceId { get; set; }
        [BsonElement("DeviceType")]
        public string? DeviceType { get; set; }
        [BsonElement("DeviceNumber")]
        public string? DeviceNameType { get; set; }
        [BsonElement("PayLoad")]
        public string? PayLoad { get; set; }
        [BsonElement("ValueDate")]
        public DateTime? ValueDate { get; set; } = DateTime.UtcNow;
    }
}