using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class MachineWarranlyDateEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string? Note { get; set; }


        [ForeignKey("Machine")]
        public int MachineId { get; set; }
        [ForeignKey("Instrumentation")]
        public int InstrumentationId { get; set; }
        [ForeignKey("DeviceDriver")]
        public int DeviceDriversId { get; set; }


        public MachineEntity? Machine { get; set; }
        public InstrumentationEntity? Instrumentation { get; set; }
        public DeviceDriverEntity? DeviceDriver { get; set; }


    }
}
