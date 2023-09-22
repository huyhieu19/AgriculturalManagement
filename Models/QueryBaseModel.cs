using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class QueryBaseModel
    {
        public string? SearchTerm { get; set; }
        public TypeOrderBy? typeOrderBy { get; set; }
    }
}
