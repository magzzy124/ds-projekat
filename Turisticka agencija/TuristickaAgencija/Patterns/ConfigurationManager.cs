using System.Configuration;

namespace TuristickaAgencija.Patterns
{
    /// <summary>
    /// Singleton pattern za upravljanje konfigurацијом aplikacije
    /// </summary>
    public sealed class ConfigurationManager
    {
        private static readonly object _lock = new object();
        private static ConfigurationManager? _instance;
        private readonly Dictionary<string, string> _configValues;

        public static ConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new ConfigurationManager();
                    }
                }
                return _instance;
            }
        }

        private ConfigurationManager()
        {
            _configValues = new Dictionary<string, string>();
            LoadConfiguration();
        }

        /// <summary>
        /// Naziv turističke agencije
        /// </summary>
        public string AgencyName { get; private set; } = "Turistička Agencija";

        /// <summary>
        /// Connection string za bazu podataka
        /// </summary>
        public string ConnectionString { get; private set; } = "Data Source=turisticka_agencija.db;Version=3;";

        /// <summary>
        /// Putanja do config.txt fajla
        /// </summary>
        public string ConfigFilePath { get; set; } = "config.txt";

        /// <summary>
        /// Učitava konfiguraciju iz config.txt fajla
        /// </summary>
        public void LoadConfiguration()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var lines = File.ReadAllLines(ConfigFilePath);
                    
                    if (lines.Length >= 1)
                        AgencyName = lines[0].Trim();
                    
                    if (lines.Length >= 2)
                        ConnectionString = lines[1].Trim();

                    // Učitaj dodatne konfiguracije ako postoje
                    for (int i = 2; i < lines.Length; i++)
                    {
                        var line = lines[i].Trim();
                        if (line.Contains('='))
                        {
                            var parts = line.Split('=', 2);
                            if (parts.Length == 2)
                            {
                                _configValues[parts[0].Trim()] = parts[1].Trim();
                            }
                        }
                    }
                }
                else
                {
                    // Kreiraj default config fajl
                    CreateDefaultConfigFile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                // Koristi default vrednosti
            }
        }

        /// <summary>
        /// Kreira default config.txt fajl
        /// </summary>
        private void CreateDefaultConfigFile()
        {
            try
            {
                var defaultConfig = new[]
                {
                    AgencyName,
                    ConnectionString,
                    "BackupInterval=24",
                    "MaxReservations=1000",
                    "Currency=RSD"
                };

                File.WriteAllLines(ConfigFilePath, defaultConfig);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating default config file: {ex.Message}");
            }
        }

        /// <summary>
        /// Vraća vrednost konfiguracije po ključu
        /// </summary>
        /// <param name="key">Ključ</param>
        /// <param name="defaultValue">Default vrednost</param>
        /// <returns>Vrednost konfiguracije</returns>
        public string GetConfigValue(string key, string defaultValue = "")
        {
            return _configValues.TryGetValue(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// Postavlja vrednost konfiguracije
        /// </summary>
        /// <param name="key">Ključ</param>
        /// <param name="value">Vrednost</param>
        public void SetConfigValue(string key, string value)
        {
            _configValues[key] = value;
        }

        /// <summary>
        /// Čuva konfiguraciju u fajl
        /// </summary>
        public void SaveConfiguration()
        {
            try
            {
                var lines = new List<string>
                {
                    AgencyName,
                    ConnectionString
                };

                foreach (var kvp in _configValues)
                {
                    lines.Add($"{kvp.Key}={kvp.Value}");
                }

                File.WriteAllLines(ConfigFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving configuration: {ex.Message}");
            }
        }

        /// <summary>
        /// Reload konfiguracije iz fajla
        /// </summary>
        public void ReloadConfiguration()
        {
            _configValues.Clear();
            LoadConfiguration();
        }
    }
}
