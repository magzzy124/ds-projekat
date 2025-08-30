namespace TuristickaAgencija.Patterns
{
    /// <summary>
    /// Singleton service za notifikacije - kombinuje Singleton i Observer pattern
    /// </summary>
    public sealed class NotificationService : INotificationSubject<Notification>
    {
        private static readonly object _lock = new object();
        private static NotificationService? _instance;
        private readonly List<INotificationObserver<Notification>> _observers;

        public static NotificationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new NotificationService();
                    }
                }
                return _instance;
            }
        }

        private NotificationService()
        {
            _observers = new List<INotificationObserver<Notification>>();
        }

        public void Attach(INotificationObserver<Notification> observer)
        {
            if (observer != null && !_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Detach(INotificationObserver<Notification> observer)
        {
            if (observer != null)
            {
                _observers.Remove(observer);
            }
        }

        public void Notify(Notification notification)
        {
            if (notification == null) return;

            // Kreiraj kopiju liste da избегнeš concurrent modification
            var observersCopy = new List<INotificationObserver<Notification>>(_observers);
            
            foreach (var observer in observersCopy)
            {
                try
                {
                    observer.Update(notification);
                }
                catch (Exception ex)
                {
                    // Log greške ali nastavi sa notifikacija ostalih observera
                    Console.WriteLine($"Error notifying observer: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Kreira i šalje notifikaciju
        /// </summary>
        public void SendNotification(NotificationType type, string message, object? data = null)
        {
            var notification = new Notification(type, message, data);
            Notify(notification);
        }

        /// <summary>
        /// Broj registrovanih observera
        /// </summary>
        public int ObserverCount => _observers.Count;

        /// <summary>
        /// Uklanja sve observere
        /// </summary>
        public void ClearObservers()
        {
            _observers.Clear();
        }
    }
}
