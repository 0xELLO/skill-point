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
    public class GameOverallStatisticsController : Controller
    {
        private readonly AppDbContext _context;

        public GameOverallStatisticsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/GameOverallStatistics
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameOverallStatistics.ToListAsync());
        }

        // GET: Admin/GameOverallStatistics/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOverallStatistics = await _context.GameOverallStatistics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameOverallStatistics == null)
            {
                return NotFound();
            }

            return View(gameOverallStatistics);
        }

        // GET: Admin/GameOverallStatistics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GameOverallStatistics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AverageScoreDistribution,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] GameOverallStatistics gameOverallStatistics)
        {
            if (ModelState.IsValid)
            {
                gameOverallStatistics.Id = Guid.NewGuid();
                _context.Add(gameOverallStatistics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameOverallStatistics);
        }

        // GET: Admin/GameOverallStatistics/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOverallStatistics = await _context.GameOverallStatistics.FindAsync(id);
            if (gameOverallStatistics == null)
            {
                return NotFound();
            }
            return View(gameOverallStatistics);
        }

        // POST: Admin/GameOverallStatistics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AverageScoreDistribution,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] GameOverallStatistics gameOverallStatistics)
        {
            if (id != gameOverallStatistics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameOverallStatistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameOverallStatisticsExists(gameOverallStatistics.Id))
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
            return View(gameOverallStatistics);
        }

        // GET: Admin/GameOverallStatistics/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOverallStatistics = await _context.GameOverallStatistics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameOverallStatistics == null)
            {
                return NotFound();
            }

            return View(gameOverallStatistics);
        }

        // POST: Admin/GameOverallStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gameOverallStatistics = await _context.GameOverallStatistics.FindAsync(id);
            _context.GameOverallStatistics.Remove(gameOverallStatistics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameOverallStatisticsExists(Guid id)
        {
            return _context.GameOverallStatistics.Any(e => e.Id == id);
        }
    }
}
