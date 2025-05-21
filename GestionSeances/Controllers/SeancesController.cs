using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionSeances.Data;

namespace GestionSeances.Controllers
{
    public class SeancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Seances.Include(s => s.Kine).Include(s => s.Patient);
            return View(await applicationDbContext.ToListAsync());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeanceId,IdK,IdP,DateS,HeureS,TypeSoin")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seance);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Seances/Delete/5
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
