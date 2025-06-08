﻿using System;
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
                // Hashovanje lozinke
                korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(korisnik.Lozinka);

                // čuvanje korisnika u bazi
                _context.Add(korisnik);
                await _context.SaveChangesAsync();

                // slanje e-mail obavještenje
                string subject = "Dobrodošli!";
                string body = $"Poštovani {korisnik.Ime},<br/><br/>Hvala što ste se registrovali na našu platformu!";
                await _emailServis.SendEmailAsync(korisnik.EMail, subject, body);

                TempData["Message"] = "Registracija uspješna! Provjerite svoj e-mail.";
                return RedirectToAction(nameof(Index));
            }

            return View(korisnik);
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
    }
}
