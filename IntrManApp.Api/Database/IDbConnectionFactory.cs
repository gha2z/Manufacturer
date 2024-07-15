using IntrManApp.Shared.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace IntrManApp.Api.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateOpenConnection();
        Task<IDbConnection> CreateOpenConnectionAsync();
        void SetConnectionString(string connectionString);
    }

    public interface IDbConfigConnectionFactory
    {
        IDbConnection CreateOpenConnection();
        Task<IDbConnection> CreateOpenConnectionAsync();
        void SetConnectionString(string connectionString);
    }

    public class SqlConnectionFactory(IOptions<ConnectionStrings> options) : IDbConnectionFactory
    {
        private string _connectionString = options.Value.Database;

        public IDbConnection CreateOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch 
            {
                 
            }
            return connection;
        }

        public async Task<IDbConnection> CreateOpenConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
            }
            catch
            {

            }
            return connection;
        }

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }

    public class SqlConfigConnectionFactory(IOptions<ConnectionStrings> options) : IDbConfigConnectionFactory
    {
        private  string _connectionString = options.Value.DbConfig;

        public IDbConnection CreateOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch
            {
                
            }
            return connection;
        }

        public async Task<IDbConnection> CreateOpenConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
            }
            catch
            {

            }
            return connection;
        }

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
