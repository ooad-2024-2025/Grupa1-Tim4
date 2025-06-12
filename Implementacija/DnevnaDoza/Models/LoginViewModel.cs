using System.ComponentModel.DataAnnotations;

namespace DnevnaDoza.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }
    }
}