#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using MatchType = App.Domain.MatchType;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MatchTypeConroller : Controller
    {
        private readonly AppDbContext _context;

        public MatchTypeConroller(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MatchTypeConroller
        public async Task<IActionResult> Index()
        {
            return View(await _context.MatchType.ToListAsync());
        }

        // GET: Admin/MatchTypeConroller/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchType = await _context.MatchType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchType == null)
            {
                return NotFound();
            }

            return View(matchType);
        }

        // GET: Admin/MatchTypeConroller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MatchTypeConroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] MatchType matchType)
        {
            if (ModelState.IsValid)
            {
                matchType.Id = Guid.NewGuid();
                _context.Add(matchType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matchType);
        }

        // GET: Admin/MatchTypeConroller/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchType = await _context.MatchType.FindAsync(id);
            if (matchType == null)
            {
                return NotFound();
            }
            return View(matchType);
        }

        // POST: Admin/MatchTypeConroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] MatchType matchType)
        {
            if (id != matchType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchTypeExists(matchType.Id))
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
            return View(matchType);
        }

        // GET: Admin/MatchTypeConroller/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchType = await _context.MatchType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchType == null)
            {
                return NotFound();
            }

            return View(matchType);
        }

        // POST: Admin/MatchTypeConroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var matchType = await _context.MatchType.FindAsync(id);
            _context.MatchType.Remove(matchType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchTypeExists(Guid id)
        {
            return _context.MatchType.Any(e => e.Id == id);
        }
    }
}
