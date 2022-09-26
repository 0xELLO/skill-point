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

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MatchController : Controller
    {
        private readonly AppDbContext _context;

        public MatchController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Match
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Match.Include(m => m.MatchType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Match/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.MatchType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Admin/Match/Create
        public IActionResult Create()
        {
            ViewData["MatchTypeId"] = new SelectList(_context.MatchType, "Id", "Id");
            return View();
        }

        // POST: Admin/Match/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchTypeId,MatchToken,StartTime,FinishTime,MaxPlayers,OpenedToJoin,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] Match match)
        {
            if (ModelState.IsValid)
            {
                match.Id = Guid.NewGuid();
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchTypeId"] = new SelectList(_context.MatchType, "Id", "Id", match.MatchTypeId);
            return View(match);
        }

        // GET: Admin/Match/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["MatchTypeId"] = new SelectList(_context.MatchType, "Id", "Id", match.MatchTypeId);
            return View(match);
        }

        // POST: Admin/Match/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MatchTypeId,MatchToken,StartTime,FinishTime,MaxPlayers,OpenedToJoin,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            ViewData["MatchTypeId"] = new SelectList(_context.MatchType, "Id", "Id", match.MatchTypeId);
            return View(match);
        }

        // GET: Admin/Match/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.MatchType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Admin/Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var match = await _context.Match.FindAsync(id);
            _context.Match.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(Guid id)
        {
            return _context.Match.Any(e => e.Id == id);
        }
    }
}
