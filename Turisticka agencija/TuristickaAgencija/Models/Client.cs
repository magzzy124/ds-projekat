using System;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Models
{
    /// <summary>
    /// Model klasa za klijenta turističke agencije
    /// </summary>
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string BrojPasosa { get; set; } = string.Empty;

        [Required]
        public DateTime DatumRodjenja { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string BrojTelefona { get; set; } = string.Empty;

        public DateTime DatumRegistracije { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<Reservation> Rezervacije { get; set; } = new List<Reservation>();

        public override string ToString()
        {
            return $"{Ime} {Prezime} ({BrojPasosa})";
        }
    }
}
