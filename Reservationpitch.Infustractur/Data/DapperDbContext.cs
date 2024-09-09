using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Infustractur.Data
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
