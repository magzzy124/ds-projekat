using System.Data;

namespace TuristickaAgencija.DataAccess
{
    /// <summary>
    /// Interface za database connection - Bridge pattern
    /// Omogućava korišćenje različitih tipova baza podataka
    /// </summary>
    public interface IDatabaseConnection : IDisposable
    {
        string ConnectionString { get; }
        DatabaseType DatabaseType { get; }
        
        Task<IDbConnection> GetConnectionAsync();
        Task<bool> TestConnectionAsync();
        Task<bool> CreateDatabaseIfNotExistsAsync();
        Task<bool> ExecuteSchemaScriptAsync(string script);
    }

    /// <summary>
    /// Enum za tipove baza podataka
    /// </summary>
    public enum DatabaseType
    {
        SQLite,
        MySQL,
        SqlServer
    }
}
