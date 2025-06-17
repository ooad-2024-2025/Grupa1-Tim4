using DnevnaDoza.Data;
using DnevnaDoza.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DnevnaDoza.Controllers
{
    [Authorize]
    public class KorpaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<KorpaController> _logger;

        public KorpaController(ApplicationDbContext context, IEmailSender emailSender, ILogger<KorpaController> logger)
        {
            _context = context;
            _emailSender = emailSender;
            _logger = logger;
        }

        // GET: Korpa
        public async Task<IActionResult> Index()
        {
            var korisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(korisnikId))
            {
                return RedirectToAction("Login", "Korisniks");
            }

            // Pretpostavljam da je KorisnikId tipa string u Korisnik modelu
            var cartItems = await _context.Korpa
                .Where(k => k.IDKorisnik.ToString() == korisnikId)
                .Include(k => k.Proizvod)
                .ToListAsync();

            return View(cartItems);
        }

        // POST: Korpa/RemoveItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var korisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(korisnikId))
            {
                return RedirectToAction("Login", "Korisniks");
            }

            if (string.IsNullOrEmpty(korisnikId) || !int.TryParse(korisnikId, out int korisnikIdInt))
            {
                _logger.LogError("Invalid korisnik ID.");
                return BadRequest("Invalid korisnik ID.");
            }

            var korpaItem = await _context.Korpa
                .FirstOrDefaultAsync(k => k.ID == id && k.IDKorisnik == korisnikId);

            if (korpaItem != null)
            {
                _context.Korpa.Remove(korpaItem);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Stavka je uklonjena iz korpe.";
            }
            else
            {
                TempData["Message"] = "Stavka nije pronađena u vašoj korpi.";
            }

            return RedirectToAction("Index");
        }

        // POST: Korpa/CompleteOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteOrder()
        {
            var korisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(korisnikId))
            {
                return RedirectToAction("Login", "Korisniks");
            }

            if (!int.TryParse(korisnikId, out int korisnikIdInt))
            {
                _logger.LogError("Invalid korisnik ID.");
                return BadRequest("Invalid korisnik ID.");
            }

           // var korisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ovo je string
            var korisnik = await _context.Korisnik
                .FirstOrDefaultAsync(k => k.IDKorisnik == korisnikId); // Sad koristimo string

            if (korisnik == null)
            {
                _logger.LogError($"User with ID {korisnikIdInt} not found.");
                return NotFound("User not found.");
            }

            var cartItems = await _context.Korpa
                .Where(k => k.IDKorisnik == korisnikId)
                .Include(k => k.Proizvod)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Message"] = "Vaša korpa je prazna.";
                return RedirectToAction("Index");
            }

            // Izračunavanje ukupnih troškova i količina
            double totalPrice = 0;
            int totalQuantity = 0;
            string productsList = "<h2>Račun</h2><ul>";

            foreach (var item in cartItems)
            {
                double itemTotal = (double)item.Cijena * item.Kolicina;
                productsList += $"<li>{item.Proizvod.Naziv} - {item.Cijena} KM x {item.Kolicina} = {itemTotal} KM</li>";
                totalPrice += itemTotal;
                totalQuantity += item.Kolicina;
            }

            productsList += "</ul>";
            productsList += $"<h3>Ukupno: {totalPrice} KM</h3>";

            // Kreiranje narudžbe
            var narudzba = new NarudzbaProizvoda
            {
                BrojProizvoda = totalQuantity,
                UkupnaCijena = totalPrice,
                JeObradjenaNarudzba = false,
                DatumNarudzbe = DateTime.Now,
                IDKorisnika = korisnikId,
                // IDObradaNarudzbe može biti 0 ili neki poseban proces
            };

            _context.NarudzbaProizvoda.Add(narudzba);
            await _context.SaveChangesAsync(); // Čuvanje narudžbe i dobijanje IDNarudzbe

            // Dodavanje detalja narudžbe
            foreach (var item in cartItems)
            {
                var detalj = new StavkeNarudzbe
                {
                    IDNarudzbe = narudzba.IDNarudzbe,
                    ProizvodId = item.ProizvodId,
                    Kolicina = item.Kolicina,
                    Cijena = item.Cijena,
                    Ukupno = item.Cijena * item.Kolicina
                };
                _context.StavkeNarudzbe.Add(detalj);
            }

            await _context.SaveChangesAsync();

            // Priprema emaila
            string subject = "Vaš račun za kupovinu - Dnevna Doza";
            string body = $"Poštovani {korisnik.Ime} {korisnik.Prezime},<br/><br/>" +
                          $"Hvala na kupovini u našoj trgovini! Evo detalja vaše narudžbe:<br/>" +
                          $"{productsList}" +
                          $"<p>Vaša narudžba je primljena i obrađena.</p>";

            // Slanje emaila
            try
            {
                await _emailSender.SendEmailAsync(korisnik.EMail, subject, body);
                _logger.LogInformation($"Račun poslat na {korisnik.EMail}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Greška prilikom slanja računa na email {korisnik.EMail}: {ex.Message}");
                TempData["Message"] = "Narudžba je kreirana, ali je došlo do problema prilikom slanja emaila.";
                return RedirectToAction("Index");
            }

            // Čišćenje korpe
            _context.Korpa.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Narudžba je uspješno završena! Račun je poslan na vaš email.";
            return RedirectToAction("OrderConfirmation");
        }

        // GET: Korpa/OrderConfirmation
        public IActionResult OrderConfirmation()
        {
            return View();
        }

        // Ostale akcije (Create, Edit, Delete, Details...) ostaju nepromenjene

        private bool KorpaExists(int id)
        {
            return _context.Korpa.Any(e => e.ID == id);
        }
    }
}