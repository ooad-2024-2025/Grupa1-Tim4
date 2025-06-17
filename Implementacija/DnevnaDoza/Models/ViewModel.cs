// Models/OrderViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace DnevnaDoza.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Ime je obavezno.")]
        [RegularExpression(@"^[A-Z][a-zA-ZčćžšđČĆŽŠĐ]*$", ErrorMessage = "Ime mora početi velikim slovom i sadržavati samo slova.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [RegularExpression(@"^[A-Z][a-zA-ZčćžšđČĆŽŠĐ]*$", ErrorMessage = "Prezime mora početi velikim slovom i sadržavati samo slova.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        [RegularExpression(@"^\+?\d{6,15}$", ErrorMessage = "Broj telefona mora sadržavati samo cifre, uz opcionalni '+' na početku.")]
        public string BrojTelefona { get; set; }

        [Required(ErrorMessage = "Grad je obavezan.")]
        public string Grad { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna.")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Poštanski broj je obavezan.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Poštanski broj mora imati tačno 5 cifara.")]
        public string PostanskiBroj { get; set; }

        [Required(ErrorMessage = "Broj kartice je obavezan.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Broj kartice mora imati tačno 16 cifara.")]
        public string BrojKartice { get; set; }

        [Required(ErrorMessage = "CVC broj je obavezan.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC mora imati tačno 3 cifre.")]
        public string CVC { get; set; }

        [Required(ErrorMessage = "Datum isteka kartice je obavezan.")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum isteka kartice")]
        public DateTime DatumIstekaKartice { get; set; }
    }
}