using TuristickaAgencija.Models;
using TuristickaAgencija.Patterns;
using TuristickaAgencija.DataAccess;

namespace TuristickaAgencija.Forms
{
    /// <summary>
    /// Form za upravljanje klijentima
    /// </summary>
    public partial class ClientForm : Form, INotificationObserver<Notification>
    {
        private readonly NotificationService _notificationService;
        private readonly ClientRepository _clientRepository;
        private List<Client> _clients;

        public ClientForm()
        {
            InitializeComponent();
            _notificationService = NotificationService.Instance;
            _notificationService.Attach(this);
            _clients = new List<Client>();
            
            // Initialize database connection and repository
            var config = ConfigurationManager.Instance;
            var dbConnection = DatabaseConnectionFactory.CreateConnection(config.ConnectionString);
            _clientRepository = new ClientRepository(dbConnection);
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Upravljanje klijentima";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            
            LoadClients();
        }

        private async void LoadClients()
        {
            try
            {
                // Load clients from database
                var clients = await _clientRepository.GetAllAsync();
                _clients = clients.ToList();

                RefreshClientList();
                
                UpdateStatusMessage($"Učitano je {_clients.Count} klijenata iz baze podataka.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju klijenata iz baze: {ex.Message}", 
                              "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Fallback - prikaži praznu listu
                _clients = new List<Client>();
                RefreshClientList();
            }
        }

        private void RefreshClientList()
        {
            dataGridViewClients.DataSource = null;
            dataGridViewClients.DataSource = _clients;
            
            // Configure columns
            if (dataGridViewClients.Columns.Count > 0)
            {
                dataGridViewClients.Columns["Id"].HeaderText = "ID";
                dataGridViewClients.Columns["Ime"].HeaderText = "Ime";
                dataGridViewClients.Columns["Prezime"].HeaderText = "Prezime";
                dataGridViewClients.Columns["BrojPasosa"].HeaderText = "Broj pasoša";
                dataGridViewClients.Columns["DatumRodjenja"].HeaderText = "Datum rođenja";
                dataGridViewClients.Columns["Email"].HeaderText = "Email";
                dataGridViewClients.Columns["BrojTelefona"].HeaderText = "Telefon";
                
                // Hide navigation properties
                if (dataGridViewClients.Columns["Rezervacije"] != null)
                    dataGridViewClients.Columns["Rezervacije"].Visible = false;
                if (dataGridViewClients.Columns["DatumRegistracije"] != null)
                    dataGridViewClients.Columns["DatumRegistracije"].Visible = false;
            }
        }

        private async void btnAddClient_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddClientForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    var newClient = addForm.Client;
                    if (newClient != null)
                    {
                        try
                        {
                            // Save to database
                            var savedClient = await _clientRepository.AddAsync(newClient);
                            _clients.Add(savedClient);
                            RefreshClientList();
                            
                            _notificationService.SendNotification(
                                NotificationType.ClientAdded, 
                                $"Novi klijent {savedClient.Ime} {savedClient.Prezime} je dodat u bazu.",
                                savedClient);
                                
                            UpdateStatusMessage($"Klijent {savedClient.Ime} {savedClient.Prezime} je uspešno dodat.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Greška pri čuvanju klijenta: {ex.Message}", 
                                          "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async void btnEditClient_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow?.DataBoundItem is Client selectedClient)
            {
                using (var editForm = new AddClientForm(selectedClient))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Update in database
                            await _clientRepository.UpdateAsync(selectedClient);
                            
                            // Refresh local list
                            LoadClients();
                            
                            _notificationService.SendNotification(
                                NotificationType.ClientUpdated, 
                                $"Klijent {selectedClient.Ime} {selectedClient.Prezime} je ažuriran u bazi.",
                                selectedClient);
                                
                            UpdateStatusMessage($"Klijent {selectedClient.Ime} {selectedClient.Prezime} je uspešno ažuriran.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Greška pri ažuriranju klijenta: {ex.Message}", 
                                          "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Molimo vas da odaberete klijenta za izmenu.", 
                              "Napomena", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnDeleteClient_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow?.DataBoundItem is Client selectedClient)
            {
                var result = MessageBox.Show(
                    $"Da li ste sigurni da želite da obrišete klijenta {selectedClient.Ime} {selectedClient.Prezime}?\n\nOva akcija je nepovratna!",
                    "Potvrda brisanja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Delete from database
                        var success = await _clientRepository.DeleteAsync(selectedClient.Id);
                        
                        if (success)
                        {
                            _clients.Remove(selectedClient);
                            RefreshClientList();
                            
                            _notificationService.SendNotification(
                                NotificationType.ClientDeleted, 
                                $"Klijent {selectedClient.Ime} {selectedClient.Prezime} je obrisan iz baze.",
                                selectedClient);
                                
                            UpdateStatusMessage($"Klijent {selectedClient.Ime} {selectedClient.Prezime} je uspešno obrisan.");
                        }
                        else
                        {
                            MessageBox.Show("Klijent nije mogao biti obrisan iz baze podataka.", 
                                          "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Greška pri brisanju klijenta: {ex.Message}", 
                                      "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Molimo vas da odaberete klijenta za brisanje.", 
                              "Napomena", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                RefreshClientList();
            }
            else
            {
                try
                {
                    // Koristi database search ako je search text dovoljno dugačak
                    if (searchText.Length >= 2)
                    {
                        var searchResults = await _clientRepository.SearchAsync(searchText);
                        dataGridViewClients.DataSource = searchResults.ToList();
                        UpdateStatusMessage($"Pronađeno {searchResults.Count()} klijenata za pretragu '{searchText}'.");
                    }
                    else
                    {
                        // Za kratke search termine koristi lokalnu pretragu
                        var filteredClients = _clients.Where(c =>
                            c.Ime.ToLower().Contains(searchText.ToLower()) ||
                            c.Prezime.ToLower().Contains(searchText.ToLower()) ||
                            c.BrojPasosa.ToLower().Contains(searchText.ToLower()) ||
                            c.Email.ToLower().Contains(searchText.ToLower())
                        ).ToList();

                        dataGridViewClients.DataSource = filteredClients;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during search: {ex.Message}");
                    // Fallback na lokalnu pretragu
                    RefreshClientList();
                }
            }
        }

        public void Update(Notification notification)
        {
            // Handle notifications from Observer pattern
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Notification>(Update), notification);
                return;
            }

            // Process notifications related to clients
            switch (notification.Type)
            {
                case NotificationType.ClientAdded:
                case NotificationType.ClientUpdated:
                case NotificationType.ClientDeleted:
                    // Refresh if needed
                    break;
            }
        }

        private void UpdateStatusMessage(string message)
        {
            // Pošalji poruku kroz notification sistem
            Console.WriteLine($"ClientForm: {message}");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _notificationService.Detach(this);
            base.OnFormClosed(e);
        }
    }
}
