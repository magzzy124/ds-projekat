using System.Linq.Expressions;
using TuristickaAgencija.Models;

namespace TuristickaAgencija.DataAccess
{
    /// <summary>
    /// Repository implementacija za Client model
    /// </summary>
    public class ClientRepository : BaseRepository<Client>
    {
        public ClientRepository(IDatabaseConnection dbConnection) 
            : base(dbConnection, "Clients")
        {
        }

        public override async Task<Client?> GetByIdAsync(int id)
        {
            try
            {
                var sql = $"SELECT * FROM {_tableName} WHERE Id = {GetParameterPrefix()}id";
                var parameters = new Dictionary<string, object> { { "id", id } };
                
                var results = await ExecuteQueryAsync(sql, parameters);
                var result = results.FirstOrDefault();
                
                return result != null ? MapToClient(result) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting client by id: {ex.Message}");
                return null;
            }
        }

        public override async Task<IEnumerable<Client>> GetAllAsync()
        {
            try
            {
                await CreateTableIfNotExistsAsync();
                
                var sql = $"SELECT * FROM {_tableName} ORDER BY Ime, Prezime";
                var results = await ExecuteQueryAsync(sql);
                
                return results.Select(MapToClient).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all clients: {ex.Message}");
                return new List<Client>();
            }
        }

        public override async Task<IEnumerable<Client>> FindAsync(Expression<Func<Client, bool>> predicate)
        {
            // Za sada vraćamo sve pa filtriramo u memoriji
            // U realnoj aplikaciji bi trebalo konvertovati Expression u SQL
            var allClients = await GetAllAsync();
            return allClients.Where(predicate.Compile());
        }

        public override async Task<Client> AddAsync(Client client)
        {
            try
            {
                await CreateTableIfNotExistsAsync();
                
                var sql = $@"INSERT INTO {_tableName} 
                    (Ime, Prezime, BrojPasosa, DatumRodjenja, Email, BrojTelefona, DatumRegistracije) 
                    VALUES ({GetParameterPrefix()}ime, {GetParameterPrefix()}prezime, {GetParameterPrefix()}passos, 
                           {GetParameterPrefix()}datum, {GetParameterPrefix()}email, {GetParameterPrefix()}telefon, 
                           {GetParameterPrefix()}registracija)";

                var parameters = new Dictionary<string, object>
                {
                    { "ime", client.Ime },
                    { "prezime", client.Prezime },
                    { "passos", client.BrojPasosa },
                    { "datum", client.DatumRodjenja.ToString("yyyy-MM-dd") },
                    { "email", client.Email },
                    { "telefon", client.BrojTelefona },
                    { "registracija", client.DatumRegistracije.ToString("yyyy-MM-dd HH:mm:ss") }
                };

                await ExecuteNonQueryAsync(sql, parameters);

                // Dobij ID novog klijenta
                var lastIdSql = _dbConnection.DatabaseType == DatabaseType.SQLite 
                    ? "SELECT last_insert_rowid()" 
                    : "SELECT LAST_INSERT_ID()";
                
                var idResults = await ExecuteQueryAsync(lastIdSql);
                if (idResults.Any())
                {
                    client.Id = Convert.ToInt32(idResults.First().Values.First());
                }

                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding client: {ex.Message}");
                throw;
            }
        }

        public override async Task<Client> UpdateAsync(Client client)
        {
            try
            {
                var sql = $@"UPDATE {_tableName} 
                    SET Ime = {GetParameterPrefix()}ime, 
                        Prezime = {GetParameterPrefix()}prezime, 
                        BrojPasosa = {GetParameterPrefix()}passos,
                        DatumRodjenja = {GetParameterPrefix()}datum, 
                        Email = {GetParameterPrefix()}email, 
                        BrojTelefona = {GetParameterPrefix()}telefon
                    WHERE Id = {GetParameterPrefix()}id";

                var parameters = new Dictionary<string, object>
                {
                    { "id", client.Id },
                    { "ime", client.Ime },
                    { "prezime", client.Prezime },
                    { "passos", client.BrojPasosa },
                    { "datum", client.DatumRodjenja.ToString("yyyy-MM-dd") },
                    { "email", client.Email },
                    { "telefon", client.BrojTelefona }
                };

                await ExecuteNonQueryAsync(sql, parameters);
                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating client: {ex.Message}");
                throw;
            }
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var sql = $"DELETE FROM {_tableName} WHERE Id = {GetParameterPrefix()}id";
                var parameters = new Dictionary<string, object> { { "id", id } };
                
                var rowsAffected = await ExecuteNonQueryAsync(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting client: {ex.Message}");
                return false;
            }
        }

        public override async Task<bool> DeleteAsync(Client client)
        {
            return await DeleteAsync(client.Id);
        }

        protected override async Task CreateTableIfNotExistsAsync()
        {
            try
            {
                var sql = _dbConnection.DatabaseType == DatabaseType.SQLite
                    ? @"CREATE TABLE IF NOT EXISTS Clients (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Ime TEXT NOT NULL,
                        Prezime TEXT NOT NULL,
                        BrojPasosa TEXT NOT NULL UNIQUE,
                        DatumRodjenja TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        BrojTelefona TEXT NOT NULL,
                        DatumRegistracije TEXT DEFAULT CURRENT_TIMESTAMP
                      )"
                    : @"CREATE TABLE IF NOT EXISTS Clients (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Ime VARCHAR(50) NOT NULL,
                        Prezime VARCHAR(50) NOT NULL,
                        BrojPasosa VARCHAR(20) NOT NULL UNIQUE,
                        DatumRodjenja DATE NOT NULL,
                        Email VARCHAR(100) NOT NULL,
                        BrojTelefona VARCHAR(20) NOT NULL,
                        DatumRegistracije TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                      )";

                await ExecuteNonQueryAsync(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Clients table: {ex.Message}");
            }
        }

        private Client MapToClient(Dictionary<string, object> row)
        {
            var client = new Client
            {
                Id = Convert.ToInt32(row["Id"]),
                Ime = row["Ime"].ToString() ?? "",
                Prezime = row["Prezime"].ToString() ?? "",
                BrojPasosa = row["BrojPasosa"].ToString() ?? "",
                Email = row["Email"].ToString() ?? "",
                BrojTelefona = row["BrojTelefona"].ToString() ?? ""
            };

            // Parse datum rođenja
            if (row["DatumRodjenja"] != null)
            {
                if (DateTime.TryParse(row["DatumRodjenja"].ToString(), out var datum))
                {
                    client.DatumRodjenja = datum;
                }
            }

            // Parse datum registracije
            if (row["DatumRegistracije"] != null)
            {
                if (DateTime.TryParse(row["DatumRegistracije"].ToString(), out var datumReg))
                {
                    client.DatumRegistracije = datumReg;
                }
            }

            return client;
        }

        /// <summary>
        /// Traži klijente po imenu, prezimenu ili broju pasoša
        /// </summary>
        public async Task<IEnumerable<Client>> SearchAsync(string searchTerm)
        {
            try
            {
                var sql = $@"SELECT * FROM {_tableName} 
                    WHERE Ime LIKE {GetParameterPrefix()}search 
                       OR Prezime LIKE {GetParameterPrefix()}search 
                       OR BrojPasosa LIKE {GetParameterPrefix()}search 
                       OR Email LIKE {GetParameterPrefix()}search
                    ORDER BY Ime, Prezime";

                var parameters = new Dictionary<string, object> 
                { 
                    { "search", $"%{searchTerm}%" } 
                };

                var results = await ExecuteQueryAsync(sql, parameters);
                return results.Select(MapToClient).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching clients: {ex.Message}");
                return new List<Client>();
            }
        }
    }
}
