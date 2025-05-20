namespace DnevnaDoza.Models
{
    public class Korisnik
    {
        int IDKorisnik { get; set; }
        String ime { get; set; }
        String prezime { get; set; }
        String korisnickoIme { get; set; }
        TipKorisnika tipKorisnika { get; set; }
        DateOnly datumZaposlenja { get; set; }
        String lozinka {  get; set; }
        String CVC { get; set; }
        DateOnly datumIstekaKartice { get; set; }
        String brojKartice { get; set; }
        String brojTelefona { get; set; }
        String mjestoStanovanja { get; set; }
        String postanskiBroj { get; set; }
        String adresa { get; set; }

        String eMail { get; set; }

        int IDApoteke { get; set; }



    }
}
