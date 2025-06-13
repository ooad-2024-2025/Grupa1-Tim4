using DnevnaDoza.Data;
using DnevnaDoza.Models;
using DnevnaDoza.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DnevnaDoza.Controllers
{
    public class ChackOutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly EmailServis _emailService;

        public ChackOutsController(ApplicationDbContext context, EmailServis emailService)
        {
            _context = context;
            _emailService = emailService;
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

        [HttpGet]
      //  [Authorize]
        public IActionResult ZavrsiNarudzbu()
        {
            return View(new NarudzbaViewModel());
        }

        [HttpPost]
       // [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZavrsiNarudzbu(NarudzbaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var korisnikId = GetKorisnikId();
            var korisnik = await _context.Korisnik.FirstOrDefaultAsync(k => k.IDKorisnik.ToString() == korisnikId);

            if (korisnik == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            // Preuzmi proizvode iz ChackOut
            var proizvodi = await _context.ChackOut
                .Where(c => c.KorisnikId == korisnikId)
                .ToListAsync();

            if (!proizvodi.Any())
            {
                ModelState.AddModelError("", "Vaša korpa je prazna.");
                return View(model);
            }

            // Kreiraj narudžbu i račun
            var narudzba = new NarudzbaProizvoda
            {
                IDKorisnika = korisnik.IDKorisnik,
                BrojProizvoda = proizvodi.Sum(p => p.Kolicina),
             //   UkupnaCijena = proizvodi.Sum(p => p.Cijena * p.Kolicina),
                DatumNarudzbe = DateTime.Now
            };

            _context.NarudzbaProizvoda.Add(narudzba);
            await _context.SaveChangesAsync();

            var eRacun = new ERacun
            {
                IDNarudzbe = narudzba.IDNarudzbe,
             //   IznosNarudzbe = narudzba.UkupnaCijena,
                Datum = DateOnly.FromDateTime(DateTime.Now),
                NazivZaposlenika = korisnik.Ime + " " + korisnik.Prezime,
                PonistenRacun = false
            };

            _context.ERacun.Add(eRacun);
            await _context.SaveChangesAsync();

            // Generisanje i slanje računa putem e-maila
            var emailBody = $"<h3>Račun za vašu narudžbu</h3>" +
                            $"<p>Ukupna cijena: {narudzba.UkupnaCijena} KM</p>" +
                            $"<h4>Proizvodi:</h4><ul>";

            foreach (var proizvod in proizvodi)
            {
                emailBody += $"<li>{proizvod.Naziv} x {proizvod.Kolicina} = {proizvod.Cijena * proizvod.Kolicina} KM</li>";
            }

            emailBody += "</ul>";

            await _emailService.SendEmailAsync(korisnik.EMail, "Vaš račun", emailBody);

            // Očisti korpu (ChackOut)
            _context.ChackOut.RemoveRange(proizvodi);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Narudžba uspješno završena! Račun je poslan na e-mail.";
            return RedirectToAction("Index", "Home");
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
    }
}
