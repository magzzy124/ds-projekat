using System.Data;
using System.Linq.Expressions;
using TuristickaAgencija.Patterns;

namespace TuristickaAgencija.DataAccess
{
    /// <summary>
    /// Bazna implementacija Repository paterna
    /// </summary>
    /// <typeparam name="T">Entity tip</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly IDatabaseConnection _dbConnection;
        protected readonly string _tableName;

        protected BaseRepository(IDatabaseConnection dbConnection, string tableName)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
        }

        public abstract Task<T?> GetByIdAsync(int id);
        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        public abstract Task<T> AddAsync(T entity);
        public abstract Task<T> UpdateAsync(T entity);
        public abstract Task<bool> DeleteAsync(int id);
        public abstract Task<bool> DeleteAsync(T entity);

        public virtual async Task<int> CountAsync()
        {
            try
            {
                using var connection = await _dbConnection.GetConnectionAsync();
                using var command = connection.CreateCommand();
                command.CommandText = $"SELECT COUNT(*) FROM {_tableName}";
                
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            var items = await FindAsync(predicate);
            return items.Count();
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            var item = await GetByIdAsync(id);
            return item != null;
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var items = await FindAsync(predicate);
            return items.Any();
        }

        /// <summary>
        /// Kreira tabelu ako ne postoji
        /// </summary>
        protected abstract Task CreateTableIfNotExistsAsync();

        /// <summary>
        /// Izvršava SQL komandu
        /// </summary>
        protected async Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object>? parameters = null)
        {
            using var connection = await _dbConnection.GetConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value ?? DBNull.Value;
                    command.Parameters.Add(parameter);
                }
            }

            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Izvršava SQL query i vraća rezultate
        /// </summary>
        protected async Task<IEnumerable<Dictionary<string, object>>> ExecuteQueryAsync(string sql, Dictionary<string, object>? parameters = null)
        {
            var results = new List<Dictionary<string, object>>();

            using var connection = await _dbConnection.GetConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value ?? DBNull.Value;
                    command.Parameters.Add(parameter);
                }
            }

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                results.Add(row);
            }

            return results;
        }

        /// <summary>
        /// Vraća SQL parameter prefix za trenutnu bazu
        /// </summary>
        protected string GetParameterPrefix()
        {
            return _dbConnection.DatabaseType switch
            {
                DatabaseType.SQLite => "@",
                DatabaseType.MySQL => "@",
                _ => "@"
            };
        }
    }
}
