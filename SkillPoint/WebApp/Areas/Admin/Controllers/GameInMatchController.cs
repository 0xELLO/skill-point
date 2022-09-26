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
    public class GameInMatchController : Controller
    {
        private readonly AppDbContext _context;

        public GameInMatchController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/GameInMatch
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GameInMatch.Include(g => g.Game).Include(g => g.Match);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/GameInMatch/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameInMatch = await _context.GameInMatch
                .Include(g => g.Game)
                .Include(g => g.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameInMatch == null)
            {
                return NotFound();
            }

            return View(gameInMatch);
        }

        // GET: Admin/GameInMatch/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id");
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken");
            return View();
        }

        // POST: Admin/GameInMatch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,GameId,RoundAmount,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] GameInMatch gameInMatch)
        {
            if (ModelState.IsValid)
            {
                gameInMatch.Id = Guid.NewGuid();
                _context.Add(gameInMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameInMatch.GameId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", gameInMatch.MatchId);
            return View(gameInMatch);
        }

        // GET: Admin/GameInMatch/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameInMatch = await _context.GameInMatch.FindAsync(id);
            if (gameInMatch == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameInMatch.GameId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", gameInMatch.MatchId);
            return View(gameInMatch);
        }

        // POST: Admin/GameInMatch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MatchId,GameId,RoundAmount,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] GameInMatch gameInMatch)
        {
            if (id != gameInMatch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameInMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameInMatchExists(gameInMatch.Id))
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
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameInMatch.GameId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", gameInMatch.MatchId);
            return View(gameInMatch);
        }

        // GET: Admin/GameInMatch/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameInMatch = await _context.GameInMatch
                .Include(g => g.Game)
                .Include(g => g.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameInMatch == null)
            {
                return NotFound();
            }

            return View(gameInMatch);
        }

        // POST: Admin/GameInMatch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gameInMatch = await _context.GameInMatch.FindAsync(id);
            _context.GameInMatch.Remove(gameInMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameInMatchExists(Guid id)
        {
            return _context.GameInMatch.Any(e => e.Id == id);
        }
    }
}
