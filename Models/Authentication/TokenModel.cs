using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public record TokenModel(string AccessToken, string RefreshToken);
}
