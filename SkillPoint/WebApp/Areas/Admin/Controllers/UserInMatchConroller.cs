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
    public class UserInMatchConroller : Controller
    {
        private readonly AppDbContext _context;

        public UserInMatchConroller(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserInMatchConroller
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserInMatch.Include(u => u.AppUser).Include(u => u.Match);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/UserInMatchConroller/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInMatch = await _context.UserInMatch
                .Include(u => u.AppUser)
                .Include(u => u.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInMatch == null)
            {
                return NotFound();
            }

            return View(userInMatch);
        }

        // GET: Admin/UserInMatchConroller/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken");
            return View();
        }

        // POST: Admin/UserInMatchConroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,MatchId,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] UserInMatch userInMatch)
        {
            if (ModelState.IsValid)
            {
                userInMatch.Id = Guid.NewGuid();
                _context.Add(userInMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userInMatch.AppUserId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", userInMatch.MatchId);
            return View(userInMatch);
        }

        // GET: Admin/UserInMatchConroller/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInMatch = await _context.UserInMatch.FindAsync(id);
            if (userInMatch == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userInMatch.AppUserId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", userInMatch.MatchId);
            return View(userInMatch);
        }

        // POST: Admin/UserInMatchConroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,MatchId,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] UserInMatch userInMatch)
        {
            if (id != userInMatch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInMatchExists(userInMatch.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userInMatch.AppUserId);
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "MatchToken", userInMatch.MatchId);
            return View(userInMatch);
        }

        // GET: Admin/UserInMatchConroller/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInMatch = await _context.UserInMatch
                .Include(u => u.AppUser)
                .Include(u => u.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInMatch == null)
            {
                return NotFound();
            }

            return View(userInMatch);
        }

        // POST: Admin/UserInMatchConroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userInMatch = await _context.UserInMatch.FindAsync(id);
            _context.UserInMatch.Remove(userInMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInMatchExists(Guid id)
        {
            return _context.UserInMatch.Any(e => e.Id == id);
        }
    }
}
