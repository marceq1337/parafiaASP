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
    public class MinistrancisController : Controller
    {
        private readonly ParafiaContext _context;

        public MinistrancisController(ParafiaContext context)
        {
            _context = context;
        }

        // GET: Ministrancis
        public async Task<IActionResult> Index()
        {
              return _context.Ministrancis != null ? 
                          View(await _context.Ministrancis.ToListAsync()) :
                          Problem("Entity set 'ParafiaContext.Ministrancis'  is null.");
        }

        // GET: Ministrancis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ministrancis == null)
            {
                return NotFound();
            }

            var ministranci = await _context.Ministrancis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ministranci == null)
            {
                return NotFound();
            }

            return View(ministranci);
        }

        // GET: Ministrancis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ministrancis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,DataUrodzenia")] Ministranci ministranci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ministranci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ministranci);
        }

        // GET: Ministrancis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ministrancis == null)
            {
                return NotFound();
            }

            var ministranci = await _context.Ministrancis.FindAsync(id);
            if (ministranci == null)
            {
                return NotFound();
            }
            return View(ministranci);
        }

        // POST: Ministrancis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,DataUrodzenia")] Ministranci ministranci)
        {
            if (id != ministranci.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ministranci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MinistranciExists(ministranci.Id))
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
            return View(ministranci);
        }

        // GET: Ministrancis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ministrancis == null)
            {
                return NotFound();
            }

            var ministranci = await _context.Ministrancis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ministranci == null)
            {
                return NotFound();
            }

            return View(ministranci);
        }

        // POST: Ministrancis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ministrancis == null)
            {
                return Problem("Entity set 'ParafiaContext.Ministrancis'  is null.");
            }
            var ministranci = await _context.Ministrancis.FindAsync(id);
            if (ministranci != null)
            {
                _context.Ministrancis.Remove(ministranci);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MinistranciExists(int id)
        {
          return (_context.Ministrancis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
