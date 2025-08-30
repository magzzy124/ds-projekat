using System.Data;
using MySql.Data.MySqlClient;

namespace TuristickaAgencija.DataAccess
{
    /// <summary>
    /// MySQL implementation of database connection - Bridge pattern
    /// </summary>
    public class MySQLDatabaseConnection : IDatabaseConnection
    {
        private MySqlConnection? _connection;
        private bool _disposed = false;

        public string ConnectionString { get; private set; }
        public DatabaseType DatabaseType => DatabaseType.MySQL;

        public MySQLDatabaseConnection(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IDbConnection> GetConnectionAsync()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(ConnectionString);
            }

            if (_connection.State == ConnectionState.Closed)
            {
                await _connection.OpenAsync();
            }

            return _connection;
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                // Extract database name from connection string
                var builder = new MySqlConnectionStringBuilder(ConnectionString);
                var databaseName = builder.Database;
                
                if (string.IsNullOrEmpty(databaseName))
                    return false;

                // Create connection without database specified
                builder.Database = "";
                var connectionStringWithoutDb = builder.ConnectionString;

                using var connection = new MySqlConnection(connectionStringWithoutDb);
                await connection.OpenAsync();
                
                using var command = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS `{databaseName}`", connection);
                await command.ExecuteNonQueryAsync();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExecuteSchemaScriptAsync(string script)
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                
                using var command = new MySqlCommand(script, connection);
                await command.ExecuteNonQueryAsync();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _connection?.Dispose();
                _disposed = true;
            }
        }
    }
}
