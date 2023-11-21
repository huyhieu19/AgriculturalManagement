using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class LogProcessModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("ValueDate")]
        public DateTime? ValueDate { get; set; } = DateTime.Now;
        [BsonElement("ServiceName")]
        public string? ServiceName { get; set; }
        [BsonElement("LogMessage")]
        public string? LogMessage { get; set; }
        [BsonElement("LogMessageDetail")]
        public string? LogMessageDetail { get; set; }
        [BsonElement("LoggerType")]
        public string? LoggerType { get; set; }
        [BsonElement("LoggerProcessType")]
        public string? LoggerProcessType { get; set; }
        [BsonElement("User")]
        public string? User { get; set; }
    }
}
