using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InstrumentationTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
    }
}
