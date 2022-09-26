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
    public class GameRoundController : Controller
    {
        private readonly AppDbContext _context;

        public GameRoundController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/GameRound
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GameRound.Include(g => g.Game).Include(g => g.Match);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/GameRound/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameRound = await _context.GameRound
                .Include(g => g.Game)
                .Include(g => g.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameRound == null)
            {
                return NotFound();
            }

            return View(gameRound);
        }

        // GET: Admin/GameRound/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id");
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken");
            return View();
        }

        // POST: Admin/GameRound/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,MatchId,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] GameRound gameRound)
        {
            if (ModelState.IsValid)
            {
                gameRound.Id = Guid.NewGuid();
                _context.Add(gameRound);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameRound.GameId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", gameRound.MatchId);
            return View(gameRound);
        }

        // GET: Admin/GameRound/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameRound = await _context.GameRound.FindAsync(id);
            if (gameRound == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameRound.GameId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", gameRound.MatchId);
            return View(gameRound);
        }

        // POST: Admin/GameRound/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GameId,MatchId,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] GameRound gameRound)
        {
            if (id != gameRound.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameRound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameRoundExists(gameRound.Id))
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
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameRound.GameId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", gameRound.MatchId);
            return View(gameRound);
        }

        // GET: Admin/GameRound/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameRound = await _context.GameRound
                .Include(g => g.Game)
                .Include(g => g.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameRound == null)
            {
                return NotFound();
            }

            return View(gameRound);
        }

        // POST: Admin/GameRound/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gameRound = await _context.GameRound.FindAsync(id);
            _context.GameRound.Remove(gameRound);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameRoundExists(Guid id)
        {
            return _context.GameRound.Any(e => e.Id == id);
        }
    }
}
