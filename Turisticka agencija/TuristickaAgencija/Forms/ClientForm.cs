using TuristickaAgencija.Models;
using TuristickaAgencija.Patterns;

namespace TuristickaAgencija.Forms
{
    /// <summary>
    /// Form za upravljanje klijentima
    /// </summary>
    public partial class ClientForm : Form, INotificationObserver<Notification>
    {
        private readonly NotificationService _notificationService;
        private List<Client> _clients;

        public ClientForm()
        {
            InitializeComponent();
            _notificationService = NotificationService.Instance;
            _notificationService.Attach(this);
            _clients = new List<Client>();
            
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
                // TODO: Load clients from database
                _clients = new List<Client>
                {
                    new Client 
                    { 
                        Id = 1, 
                        Ime = "Marko", 
                        Prezime = "Petrović", 
                        BrojPasosa = "123456789",
                        DatumRodjenja = new DateTime(1990, 5, 15),
                        Email = "marko.petrovic@email.com",
                        BrojTelefona = "+381601234567"
                    },
                    new Client 
                    { 
                        Id = 2, 
                        Ime = "Ana", 
                        Prezime = "Nikolić", 
                        BrojPasosa = "987654321",
                        DatumRodjenja = new DateTime(1985, 8, 20),
                        Email = "ana.nikolic@email.com",
                        BrojTelefona = "+381609876543"
                    }
                };

                RefreshClientList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju klijenata: {ex.Message}", 
                              "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddClientForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    var newClient = addForm.Client;
                    if (newClient != null)
                    {
                        // TODO: Save to database
                        newClient.Id = _clients.Count > 0 ? _clients.Max(c => c.Id) + 1 : 1;
                        _clients.Add(newClient);
                        RefreshClientList();
                        
                        _notificationService.SendNotification(
                            NotificationType.ClientAdded, 
                            $"Novi klijent {newClient.Ime} {newClient.Prezime} je dodat.",
                            newClient);
                    }
                }
            }
        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow?.DataBoundItem is Client selectedClient)
            {
                using (var editForm = new AddClientForm(selectedClient))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // TODO: Update in database
                        RefreshClientList();
                        
                        _notificationService.SendNotification(
                            NotificationType.ClientUpdated, 
                            $"Klijent {selectedClient.Ime} {selectedClient.Prezime} je ažuriran.",
                            selectedClient);
                    }
                }
            }
            else
            {
                MessageBox.Show("Molimo vas da odaberete klijenta za izmenu.", 
                              "Napomena", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteClient_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow?.DataBoundItem is Client selectedClient)
            {
                var result = MessageBox.Show(
                    $"Da li ste sigurni da želite da obrišete klijenta {selectedClient.Ime} {selectedClient.Prezime}?",
                    "Potvrda brisanja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // TODO: Delete from database
                    _clients.Remove(selectedClient);
                    RefreshClientList();
                    
                    _notificationService.SendNotification(
                        NotificationType.ClientDeleted, 
                        $"Klijent {selectedClient.Ime} {selectedClient.Prezime} je obrisan.",
                        selectedClient);
                }
            }
            else
            {
                MessageBox.Show("Molimo vas da odaberete klijenta za brisanje.", 
                              "Napomena", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                RefreshClientList();
            }
            else
            {
                var filteredClients = _clients.Where(c =>
                    c.Ime.ToLower().Contains(searchText) ||
                    c.Prezime.ToLower().Contains(searchText) ||
                    c.BrojPasosa.ToLower().Contains(searchText) ||
                    c.Email.ToLower().Contains(searchText)
                ).ToList();

                dataGridViewClients.DataSource = filteredClients;
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _notificationService.Detach(this);
            base.OnFormClosed(e);
        }
    }
}
