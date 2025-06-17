using DnevnaDoza.Data;
using DnevnaDoza.Models;
using DnevnaDoza.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace DnevnaDoza.Controllers
{
    public class ChackOutsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailServis;

        public ChackOutsController(ApplicationDbContext context, IEmailSender emailServis)
        {
            _context = context;
            _emailServis = emailServis;
        }
        private string GetKorisnikId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: /ChackOuts
     
        public async Task<IActionResult> Index()
        {
            var korisnikId = GetKorisnikId();
            var stavke = await _context.ChackOut
                .Include(c => c.Proizvod)
                .Where(c => c.KorisnikId == korisnikId)
                .ToListAsync();

            return View(stavke); // obavezno napravi Views/ChackOuts/Index.cshtml
        }

        // POST: /ChackOuts/Dodaj

        [HttpPost]
        public async Task<IActionResult> Dodaj(int proizvodId)
        {
            var korisnikId = GetKorisnikId();
            var proizvod = await _context.Proizvod.FindAsync(proizvodId);
            if (proizvod == null)
                return NotFound();

            var stavka = await _context.ChackOut
                .FirstOrDefaultAsync(c => c.ProizvodId == proizvodId && c.KorisnikId == korisnikId);

            if (stavka != null)
            {
                stavka.Kolicina++;
            }
            else
            {
                stavka = new ChackOut
                {
                    ProizvodId = proizvodId,
                    Naziv = proizvod.Naziv,
                    Cijena = (decimal)proizvod.Cijena,
                    Kolicina = 1,
                    KorisnikId = korisnikId
                };
                _context.ChackOut.Add(stavka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: /ChackOuts/Izbrisi

        [HttpPost]
        public async Task<IActionResult> Izbrisi(int id)
        {
            var korisnikId = GetKorisnikId();
            var stavka = await _context.ChackOut
                .FirstOrDefaultAsync(c => c.Id == id && c.KorisnikId == korisnikId);

            if (stavka != null)
            {
                _context.ChackOut.Remove(stavka);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // POST: /ChackOuts/Umanji
        [HttpPost]
        public async Task<IActionResult> Umanji(int id)
        {
            var korisnikId = GetKorisnikId();
            var stavka = await _context.ChackOut
                .FirstOrDefaultAsync(c => c.Id == id && c.KorisnikId == korisnikId);

            if (stavka != null)
            {
                if (stavka.Kolicina > 1)
                {
                    stavka.Kolicina--;
                }
                else
                {
                    _context.ChackOut.Remove(stavka);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteOrder(string Ime, string Prezime, string BrojTelefona, string Grad, string Adresa, string PostanskiBroj, string BrojKartice, string CVC, DateTime DatumIstekaKartice)
        {
            // Validacija podataka
            if (string.IsNullOrWhiteSpace(Ime) || string.IsNullOrWhiteSpace(Prezime) ||
                string.IsNullOrWhiteSpace(BrojTelefona) || string.IsNullOrWhiteSpace(Grad) ||
                string.IsNullOrWhiteSpace(Adresa) || string.IsNullOrWhiteSpace(PostanskiBroj) ||
                string.IsNullOrWhiteSpace(BrojKartice) || string.IsNullOrWhiteSpace(CVC) ||
                DatumIstekaKartice == default(DateTime))
            {
                TempData["Error"] = "Sva polja su obavezna.";
                return RedirectToAction("Index");
            }

            // Dohvatanje trenutnog korisničkog GUID-a
            var korisnikGuid = GetKorisnikId();
            if (string.IsNullOrEmpty(korisnikGuid))
            {
                TempData["Error"] = "Korisnik nije prijavljen.";
                return RedirectToAction("Index");
            }

            // Pronađi korisnika u bazi
            var korisnik = await _context.Korisnik
                .FirstOrDefaultAsync(k => k.KorisnickoIme == korisnikGuid);

            if (korisnik == null)
            {
                TempData["Error"] = "Korisnik nije pronađen.";
                return RedirectToAction("Index");
            }

            int korisnikId = korisnik.IDKorisnik;

            // Dohvatanje stavki iz korpe
            var stavke = await _context.ChackOut
                .Include(c => c.Proizvod)
                .Where(c => c.KorisnikId == korisnikGuid)
                .ToListAsync();

            if (stavke == null || !stavke.Any())
            {
                TempData["Error"] = "Vaša korpa je prazna.";
                return RedirectToAction("Index");
            }

            // Izračunavanje ukupne cene
            double ukupnaCijena = stavke.Sum(s => (double)s.Cijena * s.Kolicina);

            // Kreiranje nove narudžbe
            var narudzba = new NarudzbaProizvoda
            {
                BrojProizvoda = stavke.Count,
                UkupnaCijena = ukupnaCijena,
                DatumNarudzbe = DateTime.Now,
                IDKorisnika = korisnikId,
                JeObradjenaNarudzba = false,

                // Dodavanje korisničkih podataka
               // Ime = Ime,
               // Prezime = Prezime,
               // BrojTelefona = BrojTelefona,
               // Grad = Grad,
               // Adresa = Adresa,
              //  PostanskiBroj = PostanskiBroj,
              //  BrojKartice = BrojKartice,
              //  CVC = CVC,
              //  DatumIstekaKartice = DatumIstekaKartice
            };

            // Dodavanje narudžbe u bazu
            _context.NarudzbaProizvoda.Add(narudzba);
            await _context.SaveChangesAsync();

            // Generisanje sadržaja računa za e-mail
            var emailBody = "<h1>Vaš račun za narudžbu</h1>";
            emailBody += "<p>Poštovani " + Ime + " " + Prezime + ",</p>";
            emailBody += "<p>Hvala vam što ste izvršili kupovinu. Vaša narudžba sadrži sljedeće stavke:</p>";
            emailBody += "<ul>";

            foreach (var stavka in stavke)
            {
                emailBody += "<li>" + stavka.Proizvod.Naziv + " x" + stavka.Kolicina +
                             " - " + stavka.Cijena.ToString("C") + "</li>";
            }

            emailBody += "</ul>";
            emailBody += "<p>Ukupan iznos: " + ukupnaCijena.ToString("C") + "</p>";
            emailBody += "<p>Adresa za dostavu: " + Adresa + ", " + Grad + "</p>";
            emailBody += "<p>Hvala vam na povjerenju!</p>";

            // Slanje e-maila korisniku
            try
            {
                await _emailServis.SendEmailAsync(korisnik.EMail, "Vaš račun za narudžbu", emailBody);
                TempData["Message"] = "Narudžba je uspješno završena! Račun je poslan na vaš e-mail.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Narudžba je završena, ali slanje e-maila nije uspjelo. " + ex.Message;
            }

            return RedirectToAction("Index");
        }


        }
    }

