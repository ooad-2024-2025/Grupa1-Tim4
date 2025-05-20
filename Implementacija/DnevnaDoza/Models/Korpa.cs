namespace DnevnaDoza.Models
{
    public class Korpa
    {
        int ID { get; set; }
        bool stanjeKorpe {  get; set; }
        int brojProizvoda {  get; set; }
        bool potvrdjenaNarudzba {  get; set; }
        float ukupanIznos {  get; set; }
        int IDKorisnik {  get; set; }
        int IDNarudzbe {  get; set; }
    }
}
