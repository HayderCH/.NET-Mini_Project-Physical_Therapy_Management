using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionSeances.Data;
using Microsoft.AspNetCore.Authorization;
using GestionSeances.Models;

namespace GestionSeances.Controllers
{
    [Authorize] // All actions require authentication (admin or user)
    public class SeancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seances
        public async Task<IActionResult> Index(string searchString)
        {
            var seances = _context.Seances.Include(s => s.Kine).Include(s => s.Patient).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                seances = seances.Where(s =>
                    s.TypeSoin.Contains(searchString) ||
                    s.Kine.NomK.Contains(searchString) ||
                    s.Patient.Nomp.Contains(searchString)
                );
            }

            return View(await seances.ToListAsync());
        }

        // GET: Seances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seance = await _context.Seances
                .Include(s => s.Kine)
                .Include(s => s.Patient)
                .FirstOrDefaultAsync(m => m.SeanceId == id);
            if (seance == null)
            {
                return NotFound();
            }

            return View(seance);
        }

        // GET: Seances/Create
        public IActionResult Create()
        {
            ViewData["IdK"] = new SelectList(_context.Kines, "IdK", "NomK");
            ViewData["IdP"] = new SelectList(_context.Patients, "IdP", "Nomp");
            return View();
        }

        // POST: Seances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdK,IdP,DateS,HeureS,TypeSoin")] Seance seance)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in {key}: {error.ErrorMessage}");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                seance.ReservedBy = User.Identity?.Name; // Fill with current user's name, safe null usage
                _context.Add(seance);
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdK"] = new SelectList(_context.Kines, "IdK", "NomK", seance.IdK);
            ViewData["IdP"] = new SelectList(_context.Patients, "IdP", "Nomp", seance.IdP);
            return View(seance);
        }

        // GET: Seances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seance = await _context.Seances.FindAsync(id);
            if (seance == null)
            {
                return NotFound();
            }
            ViewData["IdK"] = new SelectList(_context.Kines, "IdK", "NomK", seance.IdK);
            ViewData["IdP"] = new SelectList(_context.Patients, "IdP", "Nomp", seance.IdP);
            return View(seance);
        }

        // POST: Seances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeanceId,IdK,IdP,DateS,HeureS,TypeSoin")] Seance seance)
        {
            if (id != seance.SeanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    seance.ReservedBy = User.Identity?.Name; // Always set current user
                    _context.Update(seance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeanceExists(seance.SeanceId))
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
            ViewData["IdK"] = new SelectList(_context.Kines, "IdK", "NomK", seance.IdK);
            ViewData["IdP"] = new SelectList(_context.Patients, "IdP", "Nomp", seance.IdP);
            return View(seance);
        }

        // Only admins can delete
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seance = await _context.Seances
                .Include(s => s.Kine)
                .Include(s => s.Patient)
                .FirstOrDefaultAsync(m => m.SeanceId == id);
            if (seance == null)
            {
                return NotFound();
            }

            return View(seance);
        }

        // POST: Seances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seance = await _context.Seances.FindAsync(id);
            if (seance != null)
            {
                _context.Seances.Remove(seance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeanceExists(int id)
        {
            return _context.Seances.Any(e => e.SeanceId == id);
        }
    }
}