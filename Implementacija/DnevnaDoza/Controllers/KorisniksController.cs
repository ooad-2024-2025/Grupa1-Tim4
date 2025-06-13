using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnevnaDoza.Data;
using DnevnaDoza.Models;
using Org.BouncyCastle.Crypto.Generators;
using DnevnaDoza.Services;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using DnevnaDoza.Models; // Za LoginViewModel


namespace DnevnaDoza.Controllers
{
    [Authorize]
    public class KorisniksController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly EmailServis _emailServis;

        public KorisniksController(ApplicationDbContext context, EmailServis emailServis)
        {
            _context = context;
            _emailServis = emailServis;
        }

        // GET: Korisniks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Korisnik.ToListAsync());
        }

        // GET: Korisniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnik
                .FirstOrDefaultAsync(m => m.IDKorisnik == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // GET: Korisniks/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Korisniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ime,Prezime,KorisnickoIme,Lozinka,EMail")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                // Provjera postoji li već korisnik s istim korisničkim imenom ili emailom
                var existingUser = await _context.Korisnik
                    .FirstOrDefaultAsync(k => k.KorisnickoIme == korisnik.KorisnickoIme || k.EMail == korisnik.EMail);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Korisničko ime ili email već postoji.");
                    return View(korisnik);
                }

                // Hashovanje lozinke
                korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(korisnik.Lozinka);

                // Generiranje jedinstvenog potvrdivnog tokena
                korisnik.ConfirmationToken = Guid.NewGuid().ToString();

                // Čuvanje korisnika u bazi podataka
                _context.Add(korisnik);
                await _context.SaveChangesAsync();

                // Generiranje URL-a za potvrdu emaila
                var confirmationLink = Url.Action("ConfirmEmail", "Korisniks", new { token = korisnik.ConfirmationToken }, protocol: HttpContext.Request.Scheme);

                // Sastavljanje emaila
                string subject = "Potvrda registracije";
                string body = $"Poštovani {korisnik.Ime},<br/><br/>" +
                              $"Hvala što ste se registrovali na našu platformu.<br/>" +
                              $"Molimo vas da potvrdite vašu email adresu klikom na link ispod:<br/>" +
                              $"<a href='{confirmationLink}'>Potvrdi email</a>";

                // Slanje emaila
                await _emailServis.SendEmailAsync(korisnik.EMail, subject, body);

                // Prikazivanje poruke korisniku
                TempData["Message"] = "Registracija uspješna! Molimo provjerite vaš email na link za potvrdu.";
                return RedirectToAction("RegistrationConfirmation");
            }

            return View(korisnik);
        }

        public IActionResult RegistrationConfirmation()
        {
            return View();
        }


        public async Task<IActionResult> ConfirmEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid token.");
            }

            var korisnik = await _context.Korisnik
                .FirstOrDefaultAsync(k => k.ConfirmationToken == token);

            if (korisnik == null)
            {
                return NotFound("Invalid token.");
            }

            // Označavanje emaila kao potvrđenog
            korisnik.EmailConfirmed = true;
            korisnik.ConfirmationToken = null; // Uklanjanje tokena nakon potvrde

            _context.Update(korisnik);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Email adresa uspješno potvrđena! Sada se možete prijaviti.";
            return RedirectToAction("ConfirmEmailSuccess");
        }

        public IActionResult ConfirmEmailSuccess()
        {
            return View();
        }



        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // DEBUG: Dodajmo ovu liniju da vidite da li se metoda poziva
            Console.WriteLine($"=== LOGIN POST POZVAN ===");
            Console.WriteLine($"Username: {model?.KorisnickoIme}");
            Console.WriteLine($"Password length: {model?.Lozinka?.Length}");

            if (ModelState.IsValid)
            {
                Console.WriteLine("✅ ModelState je valjan");

                var korisnik = await _context.Korisnik
                    .FirstOrDefaultAsync(k => k.KorisnickoIme == model.KorisnickoIme);

                Console.WriteLine($"Korisnik pronađen u bazi: {korisnik != null}");

                if (korisnik != null)
                {
                    Console.WriteLine($"Korisnik ID: {korisnik.IDKorisnik}");
                    Console.WriteLine($"EmailConfirmed: {korisnik.EmailConfirmed}");

                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(model.Lozinka, korisnik.Lozinka);
                    Console.WriteLine($"Password match: {passwordMatch}");

                    if (passwordMatch)
                    {
                        if (!korisnik.EmailConfirmed)
                        {
                            Console.WriteLine("❌ Email nije potvrđen");
                            ModelState.AddModelError("", "Molimo vas da potvrdite vašu email adresu prije prijave.");
                            return View(model);
                        }

                        Console.WriteLine("✅ Kreiranje claims i signin...");

                        // Kreiranje korisničkih tvrdnji (claims)
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, korisnik.KorisnickoIme),
                    new Claim(ClaimTypes.NameIdentifier, korisnik.IDKorisnik.ToString()),
                    new Claim(ClaimTypes.Role, korisnik.TipKorisnika.ToString())
                };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        Console.WriteLine("✅ SignIn uspješan - preusmjeravanje na Home");
                        return RedirectToAction("Index", "Home");
                    }
                }

                Console.WriteLine("❌ Pogrešno korisničko ime ili lozinka");
                ModelState.AddModelError("", "Neispravno korisničko ime ili lozinka.");
            }
            else
            {
                Console.WriteLine("❌ ModelState nije valjan:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"   - {error.ErrorMessage}");
                }
            }

            Console.WriteLine("Vraćam View sa modelom");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }





        // GET: Korisniks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnik.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }
            return View(korisnik);
        }

        // POST: Korisniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDKorisnik,Ime,Prezime,KorisnickoIme,TipKorisnika,DatumZaposlenja,Lozinka,CVC,DatumIstekaKartice,BrojKartice,BrojTelefona,MjestoStanovanja,PostanskiBroj,Adresa,EMail,IDApoteke")] Korisnik korisnik)
        {
            if (id != korisnik.IDKorisnik)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikExists(korisnik.IDKorisnik))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(korisnik);
        }

        // GET: Korisniks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnik
                .FirstOrDefaultAsync(m => m.IDKorisnik == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);
            if (korisnik != null)
            {
                _context.Korisnik.Remove(korisnik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(int id)
        {
            return _context.Korisnik.Any(e => e.IDKorisnik == id);
        }


        [AllowAnonymous]
        public async Task<IActionResult> CreateTestUser()
        {
            // Provjeri da li već postoji test korisnik
            var existing = await _context.Korisnik.FirstOrDefaultAsync(k => k.KorisnickoIme == "test");
            if (existing != null)
            {
                return Content($"Test korisnik već postoji! ID: {existing.IDKorisnik}, EmailConfirmed: {existing.EmailConfirmed}");
            }

            var testUser = new Korisnik
            {
                Ime = "Test",
                Prezime = "User",
                KorisnickoIme = "test",
                Lozinka = BCrypt.Net.BCrypt.HashPassword("123456"),
                EMail = "test@test.com",
                EmailConfirmed = true, // VAŽNO!
                TipKorisnika = TipKorisnika.RegistrovaniKorisnik,
                DatumZaposlenja = DateOnly.FromDateTime(DateTime.Now),
                IDApoteke = 1,
                BrojTelefona = "123456789",
                MjestoStanovanja = "Test Grad",
                PostanskiBroj = "12345",
                Adresa = "Test Adresa 1",
                CVC = "123",
                DatumIstekaKartice = DateOnly.FromDateTime(DateTime.Now.AddYears(2)),
                BrojKartice = "1234567890123456"
            };

            _context.Korisnik.Add(testUser);
            await _context.SaveChangesAsync();

            return Content("✅ Test korisnik kreiran uspješno!<br/>Username: test<br/>Password: 123456<br/>EmailConfirmed: true");
        }
    }


}