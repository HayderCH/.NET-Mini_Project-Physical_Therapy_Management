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
    public class KinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kines.ToListAsync());
        }

        // GET: Kines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kine = await _context.Kines
                .FirstOrDefaultAsync(m => m.IdK == id);
            if (kine == null)
            {
                return NotFound();
            }

            return View(kine);
        }

        // GET: Kines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdK,NomK,PrenomK")] Kine kine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kine);
        }

        // GET: Kines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kine = await _context.Kines.FindAsync(id);
            if (kine == null)
            {
                return NotFound();
            }
            return View(kine);
        }

        // POST: Kines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdK,NomK,PrenomK")] Kine kine)
        {
            if (id != kine.IdK)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KineExists(kine.IdK))
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
            return View(kine);
        }

        // GET: Kines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kine = await _context.Kines
                .FirstOrDefaultAsync(m => m.IdK == id);
            if (kine == null)
            {
                return NotFound();
            }

            return View(kine);
        }

        // POST: Kines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kine = await _context.Kines.FindAsync(id);
            if (kine != null)
            {
                _context.Kines.Remove(kine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KineExists(int id)
        {
            return _context.Kines.Any(e => e.IdK == id);
        }
    }
}
