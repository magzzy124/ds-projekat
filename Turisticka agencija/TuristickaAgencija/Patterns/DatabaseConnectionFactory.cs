using TuristickaAgencija.DataAccess;

namespace TuristickaAgencija.Patterns
{
    /// <summary>
    /// Factory pattern za kreiranje database konekcija
    /// </summary>
    public static class DatabaseConnectionFactory
    {
        /// <summary>
        /// Kreira odgovarajuću database konekciju na osnovu connection stringa
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>IDatabaseConnection implementacija</returns>
        public static IDatabaseConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));

            // Detektuj tip baze na osnovu connection stringa
            var lowerConnectionString = connectionString.ToLowerInvariant();

            if (lowerConnectionString.Contains("data source") && 
                (lowerConnectionString.Contains(".db") || lowerConnectionString.Contains(".sqlite")))
            {
                return new SQLiteDatabaseConnection(connectionString);
            }
            else if (lowerConnectionString.Contains("server=") || lowerConnectionString.Contains("host="))
            {
                return new MySQLDatabaseConnection(connectionString);
            }
            
            // Default to SQLite if type cannot be determined
            return new SQLiteDatabaseConnection(connectionString);
        }

        /// <summary>
        /// Kreira database konekciju na osnovu eksplicitno specificiranog tipa
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="databaseType">Tip baze podataka</param>
        /// <returns>IDatabaseConnection implementacija</returns>
        public static IDatabaseConnection CreateConnection(string connectionString, DatabaseType databaseType)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));

            return databaseType switch
            {
                DatabaseType.SQLite => new SQLiteDatabaseConnection(connectionString),
                DatabaseType.MySQL => new MySQLDatabaseConnection(connectionString),
                _ => throw new NotSupportedException($"Database type {databaseType} is not supported")
            };
        }

        /// <summary>
        /// Vraća podržane tipove baza podataka
        /// </summary>
        /// <returns>Lista podržanih tipova</returns>
        public static IEnumerable<DatabaseType> GetSupportedDatabaseTypes()
        {
            return new[] { DatabaseType.SQLite, DatabaseType.MySQL };
        }
    }
}
