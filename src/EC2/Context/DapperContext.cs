using System.Data;
using System.Data.SqlClient;
//using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EC2.Context
{
    /// <summary>
    /// Handle Dapper ORM connection
    /// </summary>
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("northwind");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
