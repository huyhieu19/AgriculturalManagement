using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Database
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;

        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection CreateConnection() => new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
}
