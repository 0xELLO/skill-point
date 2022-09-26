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
    public class UserPlayingGameRoundConroller : Controller
    {
        private readonly AppDbContext _context;

        public UserPlayingGameRoundConroller(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserPlayingGameRoundConroller
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserPlayingGameRound.Include(u => u.AppUser).Include(u => u.GameRound);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/UserPlayingGameRoundConroller/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlayingGameRound = await _context.UserPlayingGameRound
                .Include(u => u.AppUser)
                .Include(u => u.GameRound)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPlayingGameRound == null)
            {
                return NotFound();
            }

            return View(userPlayingGameRound);
        }

        // GET: Admin/UserPlayingGameRoundConroller/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["GameRoundId"] = new SelectList(_context.GameRound, "Id", "Id");
            return View();
        }

        // POST: Admin/UserPlayingGameRoundConroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,GameRoundId,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] UserPlayingGameRound userPlayingGameRound)
        {
            if (ModelState.IsValid)
            {
                userPlayingGameRound.Id = Guid.NewGuid();
                _context.Add(userPlayingGameRound);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userPlayingGameRound.AppUserId);
            ViewData["GameRoundId"] = new SelectList(_context.GameRound, "Id", "Id", userPlayingGameRound.GameRoundId);
            return View(userPlayingGameRound);
        }

        // GET: Admin/UserPlayingGameRoundConroller/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlayingGameRound = await _context.UserPlayingGameRound.FindAsync(id);
            if (userPlayingGameRound == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userPlayingGameRound.AppUserId);
            ViewData["GameRoundId"] = new SelectList(_context.GameRound, "Id", "Id", userPlayingGameRound.GameRoundId);
            return View(userPlayingGameRound);
        }

        // POST: Admin/UserPlayingGameRoundConroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,GameRoundId,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] UserPlayingGameRound userPlayingGameRound)
        {
            if (id != userPlayingGameRound.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPlayingGameRound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPlayingGameRoundExists(userPlayingGameRound.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userPlayingGameRound.AppUserId);
            ViewData["GameRoundId"] = new SelectList(_context.GameRound, "Id", "Id", userPlayingGameRound.GameRoundId);
            return View(userPlayingGameRound);
        }

        // GET: Admin/UserPlayingGameRoundConroller/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlayingGameRound = await _context.UserPlayingGameRound
                .Include(u => u.AppUser)
                .Include(u => u.GameRound)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPlayingGameRound == null)
            {
                return NotFound();
            }

            return View(userPlayingGameRound);
        }

        // POST: Admin/UserPlayingGameRoundConroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userPlayingGameRound = await _context.UserPlayingGameRound.FindAsync(id);
            _context.UserPlayingGameRound.Remove(userPlayingGameRound);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPlayingGameRoundExists(Guid id)
        {
            return _context.UserPlayingGameRound.Any(e => e.Id == id);
        }
    }
}
