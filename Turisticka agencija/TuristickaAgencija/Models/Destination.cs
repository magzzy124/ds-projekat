using System;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Models
{
    /// <summary>
    /// Model klasa za destinacije
    /// </summary>
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Zemlja { get; set; } = string.Empty;

        [StringLength(100)]
        public string Region { get; set; } = string.Empty;

        [StringLength(500)]
        public string Opis { get; set; } = string.Empty;

        [StringLength(50)]
        public string Tip { get; set; } = string.Empty; // More, Planine, Grad, Priroda

        public bool Popularna { get; set; } = false;

        public decimal? ProsecnaCena { get; set; }

        [StringLength(10)]
        public string Valuta { get; set; } = "RSD";

        public DateTime DatumDodavanja { get; set; } = DateTime.Now;

        public bool Aktivna { get; set; } = true;

        public override string ToString()
        {
            return $"{Naziv}, {Zemlja}";
        }
    }
}
