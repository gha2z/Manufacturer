using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace IntrManApp.Api.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateOpenConnection();
    }

    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.Database;
        }

        public IDbConnection CreateOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch 
            {
                // Log the exception and rethrow, or handle it as appropriate for your application.   
            }
            return connection;
        }
    }
    public class ConnectionStrings
    {
        public string Database { get; set; } = string.Empty;
    }
}
