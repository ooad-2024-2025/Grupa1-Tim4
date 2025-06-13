using System.ComponentModel.DataAnnotations;

namespace DnevnaDoza.Models
{
    public class NarudzbaViewModel
    {
        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        public string Adresa { get; set; }

        [Required]
        public string Grad { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Poštanski broj mora imati 5 cifara.")]
        public string PostanskiBroj { get; set; }

        [Required]
        [CreditCard]
        public string BrojKartice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatumIstekaKartice { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC mora imati 3 cifre.")]
        public string CVC { get; set; }
    }
}