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
    public class MszesController : Controller
    {
        private readonly ParafiaContext _context;

        public MszesController(ParafiaContext context)
        {
            _context = context;
        }

        // GET: Mszes
        public async Task<IActionResult> Index()
        {
            var parafiaContext = _context.Mszes;
            var mszes = from m in parafiaContext
                        join k in _context.Ksiezas on m.Ksiadz equals k.Id
                        join min in _context.Ministrancis on m.Ministrant equals min.Id
                        select new Msze
                        {
                            Id = m.Id,
                            DataMszy = m.DataMszy,
                            KsiadzNavigation = new Ksieza
                            {
                                Imie = k.FullName,
                                Nazwisko = k.Nazwisko,
                                StanowiskoNavigation = k.StanowiskoNavigation
                            },
                            MinistrantNavigation = new Ministranci
                            {
                                Imie = min.FullName,
                                Nazwisko = min.Nazwisko,
                                DataUrodzenia = min.DataUrodzenia
                            }
                        };
            return View(await mszes.ToListAsync());
    }

        // GET: Mszes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mszes == null)
            {
                return NotFound();
            }

            var msze = await _context.Mszes
                .Include(m => m.KsiadzNavigation)
                .Include(m => m.MinistrantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (msze == null)
            {
                return NotFound();
            }

            return View(msze);
        }

        // GET: Mszes/Create
        public IActionResult Create()
        {
            ViewData["Ksiadz"] = new SelectList(_context.Ksiezas, "Id", "FullName");
            ViewData["Ministrant"] = new SelectList(_context.Ministrancis, "Id", "FullName");
            return View();
        }

        // POST: Mszes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataMszy,Ksiadz,Ministrant")] Msze msze)
        {
            if (ModelState.IsValid)
            {
                _context.Add(msze);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ksiadz"] = new SelectList(_context.Ksiezas, "Id", "FullName", msze.Ksiadz);
            ViewData["Ministrant"] = new SelectList(_context.Ministrancis, "Id", "FullName", msze.Ministrant);
            return View(msze);
        }

        // GET: Mszes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mszes == null)
            {
                return NotFound();
            }

            var msze = await _context.Mszes.FindAsync(id);
            if (msze == null)
            {
                return NotFound();
            }
            ViewData["Ksiadz"] = new SelectList(_context.Ksiezas, "Id", "FullName", msze.Ksiadz);
            ViewData["Ministrant"] = new SelectList(_context.Ministrancis, "Id", "FullName", msze.Ministrant);
            return View(msze);
        }

        // POST: Mszes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataMszy,Ksiadz,Ministrant")] Msze msze)
        {
            if (id != msze.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(msze);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MszeExists(msze.Id))
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
            ViewData["Ksiadz"] = new SelectList(_context.Ksiezas, "Id", "FullName", msze.Ksiadz);
            ViewData["Ministrant"] = new SelectList(_context.Ministrancis, "Id", "FullName", msze.Ministrant);
            return View(msze);
        }

        // GET: Mszes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mszes == null)
            {
                return NotFound();
            }

            var msze = await _context.Mszes
                .Include(m => m.KsiadzNavigation)
                .Include(m => m.MinistrantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (msze == null)
            {
                return NotFound();
            }

            return View(msze);
        }

        // POST: Mszes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mszes == null)
            {
                return Problem("Entity set 'ParafiaContext.Mszes'  is null.");
            }
            var msze = await _context.Mszes.FindAsync(id);
            if (msze != null)
            {
                _context.Mszes.Remove(msze);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MszeExists(int id)
        {
          return (_context.Mszes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
