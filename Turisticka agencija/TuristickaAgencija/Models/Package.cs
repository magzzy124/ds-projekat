using System;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Models
{
    /// <summary>
    /// Bazna klasa za turistički paket
    /// </summary>
    public abstract class Package
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena mora biti pozitivna")]
        public decimal Cena { get; set; }

        [Required]
        [StringLength(50)]
        public string VrstaPaketa { get; set; } = string.Empty;

        [StringLength(500)]
        public string Opis { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Destinacija { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string TipPrevoza { get; set; } = string.Empty;

        [Range(1, 365)]
        public int BrojDana { get; set; }

        public DateTime DatumKreiranja { get; set; } = DateTime.Now;

        public bool Aktivan { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Reservation> Rezervacije { get; set; } = new List<Reservation>();

        public override string ToString()
        {
            return $"{Naziv} - {VrstaPaketa} ({Cena:C})";
        }
    }

    /// <summary>
    /// Aranžman za more
    /// </summary>
    public class AranzmanZaMore : Package
    {
        [Required]
        [StringLength(50)]
        public string VrstaSmestaja { get; set; } = string.Empty;

        [Range(1, 5)]
        public int ZvezdiceHotela { get; set; }

        public bool UkljucenDorucak { get; set; }
        public bool UkljucenRucak { get; set; }
        public bool UkljucenVecera { get; set; }

        public AranzmanZaMore()
        {
            VrstaPaketa = "More";
        }
    }

    /// <summary>
    /// Aranžman za planine
    /// </summary>
    public class AranzmanZaPlanine : Package
    {
        [Required]
        [StringLength(50)]
        public string VrstaSmestaja { get; set; } = string.Empty;

        [StringLength(200)]
        public string DodatneAktivnosti { get; set; } = string.Empty;

        public bool VodicUsluge { get; set; }
        public bool SkijaskaOprema { get; set; }

        public AranzmanZaPlanine()
        {
            VrstaPaketa = "Planine";
        }
    }

    /// <summary>
    /// Ekskurzija
    /// </summary>
    public class Ekskurzija : Package
    {
        [Required]
        [StringLength(100)]
        public string Vodic { get; set; } = string.Empty;

        [Range(1, 30)]
        public int Trajanje { get; set; } // u danima

        [StringLength(200)]
        public string ObilasciMesta { get; set; } = string.Empty;

        public bool UkljucenUlaznice { get; set; }

        public Ekskurzija()
        {
            VrstaPaketa = "Ekskurzija";
        }
    }

    /// <summary>
    /// Krstarenje
    /// </summary>
    public class Krstarenje : Package
    {
        [Required]
        [StringLength(100)]
        public string Brod { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Ruta { get; set; } = string.Empty;

        [Required]
        public DateTime DatumPolaska { get; set; }

        [Required]
        [StringLength(50)]
        public string TipKabine { get; set; } = string.Empty;

        public bool AllInclusive { get; set; }

        public Krstarenje()
        {
            VrstaPaketa = "Krstarenje";
        }
    }
}
