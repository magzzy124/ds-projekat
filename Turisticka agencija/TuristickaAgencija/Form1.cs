using TuristickaAgencija.Forms;
using TuristickaAgencija.Patterns;

namespace TuristickaAgencija;

public partial class MainForm : Form, INotificationObserver<Notification>
{
    private readonly ConfigurationManager _config;
    private readonly NotificationService _notificationService;

    public MainForm()
    {
        InitializeComponent();
        
        _config = ConfigurationManager.Instance;
        _notificationService = NotificationService.Instance;
        
        // Subscribe to notifications
        _notificationService.Attach(this);
        
        InitializeForm();
    }

    private void InitializeForm()
    {
        // Set form title from config
        this.Text = $"{_config.AgencyName} - Glavna forma";
        
        // Center the form
        this.StartPosition = FormStartPosition.CenterScreen;
        this.WindowState = FormWindowState.Maximized;
        
        // Setup event handlers
        SetupEventHandlers();
        
        // Load initial data
        LoadInitialData();
    }

    private void SetupEventHandlers()
    {
        // Menu event handlers
        klijentiToolStripMenuItem.Click += (s, e) => OpenClientForm();
        paketiToolStripMenuItem.Click += (s, e) => OpenPackageForm();
        rezervacijeToolStripMenuItem.Click += (s, e) => OpenReservationForm();
    }

    private void OpenClientForm()
    {
        try
        {
            var clientForm = new ClientForm();
            clientForm.MdiParent = this;
            clientForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri otvaranju forme za klijente: {ex.Message}", 
                          "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OpenPackageForm()
    {
        MessageBox.Show("Forma za pakete će biti implementirana u sledećoj verziji.", 
                      "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void OpenReservationForm()
    {
        MessageBox.Show("Forma za rezervacije će biti implementirana u sledećoj verziji.", 
                      "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async void LoadInitialData()
    {
        try
        {
            // TODO: Load clients, packages, reservations
            UpdateStatusBar("Aplikacija je pokrenuta uspešno");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri učitavanju podataka: {ex.Message}", 
                          "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateStatusBar(string message)
    {
        if (toolStripStatusLabel1 != null)
        {
            toolStripStatusLabel1.Text = message;
        }
        Console.WriteLine($"Status: {message}");
    }

    public void Update(Notification notification)
    {
        // Handle notifications from Observer pattern
        BeginInvoke(new Action(() =>
        {
            UpdateStatusBar($"{notification.Type}: {notification.Message}");
        }));
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        // Unsubscribe from notifications
        _notificationService.Detach(this);
        base.OnFormClosed(e);
    }
}
