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
    public class UserGameStatisticsConroller : Controller
    {
        private readonly AppDbContext _context;

        public UserGameStatisticsConroller(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserGameStatisticsConroller
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserGameStatistics.Include(u => u.AppUser).Include(u => u.Game);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/UserGameStatisticsConroller/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGameStatistics = await _context.UserGameStatistics
                .Include(u => u.AppUser)
                .Include(u => u.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGameStatistics == null)
            {
                return NotFound();
            }

            return View(userGameStatistics);
        }

        // GET: Admin/UserGameStatisticsConroller/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id");
            return View();
        }

        // POST: Admin/UserGameStatisticsConroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,GameId,AverageScore,BestScore,Rating,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] UserGameStatistics userGameStatistics)
        {
            if (ModelState.IsValid)
            {
                userGameStatistics.Id = Guid.NewGuid();
                _context.Add(userGameStatistics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userGameStatistics.AppUserId);
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", userGameStatistics.GameId);
            return View(userGameStatistics);
        }

        // GET: Admin/UserGameStatisticsConroller/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGameStatistics = await _context.UserGameStatistics.FindAsync(id);
            if (userGameStatistics == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userGameStatistics.AppUserId);
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", userGameStatistics.GameId);
            return View(userGameStatistics);
        }

        // POST: Admin/UserGameStatisticsConroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,GameId,AverageScore,BestScore,Rating,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] UserGameStatistics userGameStatistics)
        {
            if (id != userGameStatistics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userGameStatistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGameStatisticsExists(userGameStatistics.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userGameStatistics.AppUserId);
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", userGameStatistics.GameId);
            return View(userGameStatistics);
        }

        // GET: Admin/UserGameStatisticsConroller/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGameStatistics = await _context.UserGameStatistics
                .Include(u => u.AppUser)
                .Include(u => u.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGameStatistics == null)
            {
                return NotFound();
            }

            return View(userGameStatistics);
        }

        // POST: Admin/UserGameStatisticsConroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userGameStatistics = await _context.UserGameStatistics.FindAsync(id);
            _context.UserGameStatistics.Remove(userGameStatistics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserGameStatisticsExists(Guid id)
        {
            return _context.UserGameStatistics.Any(e => e.Id == id);
        }
    }
}
