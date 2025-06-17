using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace DnevnaDoza.Models
{
    public class ChackOut
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProizvodId { get; set; }

        [ForeignKey("ProizvodId")]
        [Required(ErrorMessage = "Naziv je obavezan.")]
        [RegularExpression(@"^[A-Z][a-zA-ZčćžšđČĆŽŠĐ]*$", ErrorMessage = "Naziv mora početi velikim slovom i sadržavati samo slova.")]
        public Proizvod Proizvod { get; set; }

        public string Naziv { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Cijena { get; set; }

        public int Kolicina { get; set; } = 1;

        // 🔽 Dodano: veza s korisnikom
        [Required]
        public string KorisnikId { get; set; }

    }
}
