using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlTypes;

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
            try
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow, or handle it as appropriate for your application.
                return null;
                
            }
        }
    }
    public class ConnectionStrings
    {
        public string Database { get; set; }
    }
}
