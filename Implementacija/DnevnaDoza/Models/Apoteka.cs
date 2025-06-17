using System.ComponentModel.DataAnnotations;

namespace DnevnaDoza.Models
{
    public class Apoteka
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan.")]
        [RegularExpression(@"^[A-Z][a-zA-ZčćžšđČĆŽŠĐ]*$", ErrorMessage = "Naziv mora početi velikim slovom i sadržavati samo slova.")]
        public string Naziv { get; set; }

        public string Adresa { get; set; }
        [RegularExpression(@"^\+?\d{6,15}$", ErrorMessage = "Broj telefona mora sadržavati samo cifre")]
        public string Telefon { get; set; }

        public string EMail { get; set; }

        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d-([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "Radno vrijeme mora biti u formatu HH:mm-HH:mm.")]
        public string RadnoVrijeme { get; set; }

    }
} // komenatr 1 
