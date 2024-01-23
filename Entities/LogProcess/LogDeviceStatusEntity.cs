using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.LogProcess
{
    public class LogDeviceStatusEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("ValueDate")]
        public DateTime? ValueDate { get; set; } = DateTime.UtcNow;
        [BsonElement("DeviceName")]
        public string? DeviceName { get; set; }
        [BsonElement("RequestOn")]
        public bool? RequestOn { get; set; }
        [BsonElement("IsSuccess")]
        public bool? IsSuccess { get; set; }
        [BsonElement("ValueSensor")]
        public string? ValueSensor { get; set; }
        [BsonElement("TypeOnOff")]
        public int? TypeOnOff { get; set; }
        [BsonElement("TimerId")]
        public int? TimerId { get; set; }
        [BsonElement("ThresholdId")]
        public int? ThresholdId { get; set; }
    }
}