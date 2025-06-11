using DnevnaDoza.Data;
using DnevnaDoza.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace DnevnaDoza.Controllers
{

    public class ApotekaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApotekaController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        

        [Authorize(Roles = "Administrator, Korisnik")]
        // GET: Apotekas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apoteka.ToListAsync());
        }

        //GET: INformacijeOApoteci 
        public async Task<IActionResult> InformacijeOApoteci()
        {
            return View();

        }
        // GET: Apotekas/Details/5
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apoteka = await _context.Apoteka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apoteka == null)
            {
                return NotFound();
            }

            return View(apoteka);
        }

        // GET: Apotekas/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apotekas/Create
        [Authorize(Roles = "Administrator")]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Adresa,Telefon,EMail,RadnoVrijeme")] Apoteka apoteka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apoteka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apoteka);
        }

        // GET: Apotekas/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apoteka = await _context.Apoteka.FindAsync(id);
            if (apoteka == null)
            {
                return NotFound();
            }
            return View(apoteka);
        }

        // POST: Apotekas/Edit/5
        [Authorize(Roles = "Administrator")]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Adresa,Telefon,EMail,RadnoVrijeme")] Apoteka apoteka)
        {
            if (id != apoteka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apoteka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApotekaExists(apoteka.Id))
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
            return View(apoteka);
        }

        // GET: Apotekas/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apoteka = await _context.Apoteka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apoteka == null)
            {
                return NotFound();
            }

            return View(apoteka);
        }

        // POST: Apotekas/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apoteka = await _context.Apoteka.FindAsync(id);
            if (apoteka != null)
            {
                _context.Apoteka.Remove(apoteka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApotekaExists(int id)
        {
            return _context.Apoteka.Any(e => e.Id == id);
        }
    }
}
