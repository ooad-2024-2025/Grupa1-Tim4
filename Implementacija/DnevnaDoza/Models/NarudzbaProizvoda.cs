namespace DnevnaDoza.Models
{
    public class NarudzbaProizvoda
    {
        public int IDNarudzbe {  get; set; }
        public int brojProizvoda {  get; set; }
        public double ukupnaCijena {  get; set; }
        bool jeObradjenaNarudzba {  get; set; }
        public DateTime datumNarudzbe { get; set; } 
        public int IDKorisnika {  get; set; }
        public int IDObradaNarudzbe { get; set; }



    }
}
