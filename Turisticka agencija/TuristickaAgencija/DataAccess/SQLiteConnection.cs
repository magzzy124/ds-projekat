using System.Data;
using System.Data.SQLite;

namespace TuristickaAgencija.DataAccess
{
    /// <summary>
    /// SQLite implementation of database connection - Bridge pattern
    /// </summary>
    public class SQLiteDatabaseConnection : IDatabaseConnection
    {
        private System.Data.SQLite.SQLiteConnection? _connection;
        private bool _disposed = false;

        public string ConnectionString { get; private set; }
        public DatabaseType DatabaseType => DatabaseType.SQLite;

        public SQLiteDatabaseConnection(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IDbConnection> GetConnectionAsync()
        {
            if (_connection == null)
            {
                _connection = new System.Data.SQLite.SQLiteConnection(ConnectionString);
            }

            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open(); // SQLite doesn't support async open
            }

            return _connection;
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var connection = new System.Data.SQLite.SQLiteConnection(ConnectionString);
                connection.Open();
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
                // SQLite creates database file automatically when connection is opened
                using var connection = new System.Data.SQLite.SQLiteConnection(ConnectionString);
                connection.Open();
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
                using var connection = new System.Data.SQLite.SQLiteConnection(ConnectionString);
                connection.Open();
                
                using var command = new System.Data.SQLite.SQLiteCommand(script, connection);
                command.ExecuteNonQuery();
                
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
