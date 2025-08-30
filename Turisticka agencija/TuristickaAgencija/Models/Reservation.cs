using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuristickaAgencija.Models
{
    /// <summary>
    /// Model klasa za rezervaciju paketa
    /// </summary>
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        [ForeignKey("Package")]
        public int PackageId { get; set; }

        [Required]
        public DateTime DatumRezervacije { get; set; } = DateTime.Now;

        [Required]
        public DateTime DatumPutovanja { get; set; }

        [Required]
        [Range(1, 50)]
        public int BrojPutnika { get; set; } = 1;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal UkupnaCena { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Aktivna"; // Aktivna, Otkazana, Završena

        [StringLength(500)]
        public string Napomene { get; set; } = string.Empty;

        public DateTime? DatumOtkazivanja { get; set; }

        [StringLength(200)]
        public string RazlogOtkazivanja { get; set; } = string.Empty;

        // Navigation properties
        public virtual Client Client { get; set; } = null!;
        public virtual Package Package { get; set; } = null!;

        public bool IsActive => Status == "Aktivna";
        public bool IsCancelled => Status == "Otkazana";
        public bool IsCompleted => Status == "Završena";

        public void CancelReservation(string razlog = "")
        {
            Status = "Otkazana";
            DatumOtkazivanja = DateTime.Now;
            RazlogOtkazivanja = razlog;
        }

        public void CompleteReservation()
        {
            Status = "Završena";
        }

        public override string ToString()
        {
            return $"Rezervacija #{Id} - {Package?.Naziv} ({Status})";
        }
    }

    /// <summary>
    /// Enumeracija za status rezervacije
    /// </summary>
    public enum ReservationStatus
    {
        Aktivna,
        Otkazana,
        Završena,
        Čeka_Potvrdu
    }
}
