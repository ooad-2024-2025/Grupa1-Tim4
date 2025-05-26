using System.ComponentModel.DataAnnotations;

namespace DnevnaDoza.Models
{
    public class Apoteka
    {
        [Key]
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Adresa { get; set; }

        public string Telefon { get; set; }

        public string EMail { get; set; }

        public string RadnoVrijeme { get; set; }
    }
} // komenatr 1 
