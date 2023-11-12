using System.ComponentModel.DataAnnotations;

namespace Entities.Module
{
    public class TopicSystemEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid SystemTopic { get; set; }
    }
}
