using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using parafia2.Models.DataLayer;

namespace parafia2.Controllers
{
    public class KsiezasController : Controller
    {
        private readonly ParafiaContext _context;

        public KsiezasController(ParafiaContext context)
        {
            _context = context;
        }

        // GET: Ksiezas
        public async Task<IActionResult> Index()
        {
            var parafiaContext = _context.Ksiezas.Include(k => k.StanowiskoNavigation);
            var ksiezas = from k in parafiaContext
                          join s in _context.Stanowiskas on k.Stanowisko equals s.Id
                          select new Ksieza
                          {
                              Id = k.Id,
                              Imie = k.Imie,
                              Nazwisko = k.Nazwisko,
                              StanowiskoNavigation = new Stanowiska
                              {
                                  NazwaStanowiska = s.NazwaStanowiska
                              }
                          };
            return View(await ksiezas.ToListAsync());
        }

        // GET: Ksiezas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ksiezas == null)
            {
                return NotFound();
            }

            var ksieza = await _context.Ksiezas
                .Include(k => k.StanowiskoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ksieza == null)
            {
                return NotFound();
            }

            return View(ksieza);
        }

        // GET: Ksiezas/Create
        public IActionResult Create()
        {
            ViewData["Stanowisko"] = new SelectList(_context.Stanowiskas, "Id", "NazwaStanowiska");
            return View();
        }

        // POST: Ksiezas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Stanowisko")] Ksieza ksieza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ksieza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Stanowisko"] = new SelectList(_context.Stanowiskas, "Id", "NazwaStanowiska", ksieza.Stanowisko);
            return View(ksieza);
        }

        // GET: Ksiezas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ksiezas == null)
            {
                return NotFound();
            }

            var ksieza = await _context.Ksiezas.FindAsync(id);
            if (ksieza == null)
            {
                return NotFound();
            }
            ViewData["Stanowisko"] = new SelectList(_context.Stanowiskas, "Id", "NazwaStanowiska", ksieza.Stanowisko);
            return View(ksieza);
        }

        // POST: Ksiezas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Stanowisko")] Ksieza ksieza)
        {
            if (id != ksieza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ksieza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KsiezaExists(ksieza.Id))
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
            ViewData["Stanowisko"] = new SelectList(_context.Stanowiskas, "Id", "NazwaStanowiska", ksieza.Stanowisko);
            return View(ksieza);
        }

        // GET: Ksiezas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ksiezas == null)
            {
                return NotFound();
            }

            var ksieza = await _context.Ksiezas
                .Include(k => k.StanowiskoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ksieza == null)
            {
                return NotFound();
            }

            return View(ksieza);
        }

        // POST: Ksiezas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ksiezas == null)
            {
                return Problem("Entity set 'ParafiaContext.Ksiezas'  is null.");
            }
            var ksieza = await _context.Ksiezas.FindAsync(id);
            if (ksieza != null)
            {
                _context.Ksiezas.Remove(ksieza);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KsiezaExists(int id)
        {
          return (_context.Ksiezas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
