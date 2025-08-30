using System.ComponentModel;
using TuristickaAgencija.Models;

namespace TuristickaAgencija.Forms
{
    /// <summary>
    /// Form za dodavanje/izmenu klijenta
    /// </summary>
    public partial class AddClientForm : Form
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Client? Client { get; private set; }
        private readonly bool _isEditMode;

        public AddClientForm(Client? client = null)
        {
            InitializeComponent();
            
            _isEditMode = client != null;
            Client = client;
            
            InitializeForm();
            
            if (_isEditMode && Client != null)
            {
                LoadClientData();
            }
        }

        private void InitializeForm()
        {
            this.Text = _isEditMode ? "Izmeni klijenta" : "Dodaj novog klijenta";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void LoadClientData()
        {
            if (Client == null) return;

            txtIme.Text = Client.Ime;
            txtPrezime.Text = Client.Prezime;
            txtBrojPasosa.Text = Client.BrojPasosa;
            dateTimePicker.Value = Client.DatumRodjenja;
            txtEmail.Text = Client.Email;
            txtBrojTelefona.Text = Client.BrojTelefona;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    if (_isEditMode && Client != null)
                    {
                        // Update existing client
                        Client.Ime = txtIme.Text.Trim();
                        Client.Prezime = txtPrezime.Text.Trim();
                        Client.BrojPasosa = txtBrojPasosa.Text.Trim();
                        Client.DatumRodjenja = dateTimePicker.Value.Date;
                        Client.Email = txtEmail.Text.Trim();
                        Client.BrojTelefona = txtBrojTelefona.Text.Trim();
                    }
                    else
                    {
                        // Create new client
                        Client = new Client
                        {
                            Ime = txtIme.Text.Trim(),
                            Prezime = txtPrezime.Text.Trim(),
                            BrojPasosa = txtBrojPasosa.Text.Trim(),
                            DatumRodjenja = dateTimePicker.Value.Date,
                            Email = txtEmail.Text.Trim(),
                            BrojTelefona = txtBrojTelefona.Text.Trim()
                        };
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Greška pri čuvanju klijenta: {ex.Message}", 
                                  "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(txtIme.Text))
                errors.Add("Ime je obavezno polje.");

            if (string.IsNullOrWhiteSpace(txtPrezime.Text))
                errors.Add("Prezime je obavezno polje.");

            if (string.IsNullOrWhiteSpace(txtBrojPasosa.Text))
                errors.Add("Broj pasoša je obavezno polje.");

            if (dateTimePicker.Value.Date >= DateTime.Now.Date)
                errors.Add("Datum rođenja mora biti u prošlosti.");

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                errors.Add("Email je obavezno polje.");
            else if (!IsValidEmail(txtEmail.Text))
                errors.Add("Email adresa nije u validnom formatu.");

            if (string.IsNullOrWhiteSpace(txtBrojTelefona.Text))
                errors.Add("Broj telefona je obavezno polje.");

            if (errors.Any())
            {
                var errorMessage = "Molimo vas da ispravite sledeće greške:\n\n" + 
                                 string.Join("\n", errors);
                MessageBox.Show(errorMessage, "Validacija", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email adresa nije u validnom formatu.", 
                              "Validacija", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
        }

        private void txtBrojPasosa_Leave(object sender, EventArgs e)
        {
            // Basic passport number validation
            var passportNumber = txtBrojPasosa.Text.Trim();
            if (!string.IsNullOrWhiteSpace(passportNumber))
            {
                if (passportNumber.Length < 6 || passportNumber.Length > 20)
                {
                    MessageBox.Show("Broj pasoša mora imati između 6 i 20 karaktera.", 
                                  "Validacija", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBrojPasosa.Focus();
                }
            }
        }
    }
}
