namespace TuristickaAgencija.Patterns
{
    /// <summary>
    /// Observer pattern interface
    /// </summary>
    /// <typeparam name="T">Tip podataka za notifikaciju</typeparam>
    public interface INotificationObserver<in T>
    {
        /// <summary>
        /// Metoda koja se poziva kada se desi promena
        /// </summary>
        /// <param name="data">Podaci o promeni</param>
        void Update(T data);
    }

    /// <summary>
    /// Subject interface za Observer pattern
    /// </summary>
    /// <typeparam name="T">Tip podataka za notifikaciju</typeparam>
    public interface INotificationSubject<T>
    {
        /// <summary>
        /// Dodaje observer
        /// </summary>
        void Attach(INotificationObserver<T> observer);

        /// <summary>
        /// Uklanja observer
        /// </summary>
        void Detach(INotificationObserver<T> observer);

        /// <summary>
        /// Notifikuje sve observere
        /// </summary>
        void Notify(T data);
    }

    /// <summary>
    /// Tipovi notifikacija
    /// </summary>
    public enum NotificationType
    {
        ClientAdded,
        ClientUpdated,
        ClientDeleted,
        ReservationCreated,
        ReservationCancelled,
        ReservationCompleted,
        PackageAdded,
        PackageUpdated,
        PackageDeleted,
        DatabaseBackupCompleted,
        ConfigurationChanged
    }

    /// <summary>
    /// Klasa za notifikacije
    /// </summary>
    public class Notification
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public object? Data { get; set; }
        
        public Notification(NotificationType type, string message, object? data = null)
        {
            Type = type;
            Message = message;
            Data = data;
        }
    }
}
