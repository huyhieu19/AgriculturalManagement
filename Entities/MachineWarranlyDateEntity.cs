using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class MachineWarranlyDateEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime WarrantyDate { get; set; }
        [ForeignKey("Machine")]
        public int MachineId { get; set; }
        public MachineEntity? Machine { get; set; }
        [ForeignKey("Farm")]
        public int FarmId { get; set; }
        public FarmEntity? Farm { get; set; }
        [ForeignKey("Instrumentation")]
        public int InstrumentationId { get; set; }
        public InstrumentationEntity? Instrumentation { get; set; }
        [ForeignKey("DeviceDriver")]
        public int DeviceDriversId { get; set; }
        public DeviceDriverEntity? DeviceDriver { get; set; }
        public string? Note { get; set; }

    }
}
