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
    public class UserRoundResultConroller : Controller
    {
        private readonly AppDbContext _context;

        public UserRoundResultConroller(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserRoundResultConroller
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserRoundResult.Include(u => u.AppUser).Include(u => u.GameRound);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/UserRoundResultConroller/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoundResult = await _context.UserRoundResult
                .Include(u => u.AppUser)
                .Include(u => u.GameRound)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRoundResult == null)
            {
                return NotFound();
            }

            return View(userRoundResult);
        }

        // GET: Admin/UserRoundResultConroller/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["GameRoundId"] = new SelectList(_context.GameRound, "Id", "Id");
            return View();
        }

        // POST: Admin/UserRoundResultConroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,GameRoundId,Result,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] UserRoundResult userRoundResult)
        {
            if (ModelState.IsValid)
            {
                userRoundResult.Id = Guid.NewGuid();
                _context.Add(userRoundResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userRoundResult.AppUserId);
            ViewData["GameRoundId"] = new SelectList(_context.GameRound, "Id", "Id", userRoundResult.GameRoundId);
            return View(userRoundResult);
        }

        // GET: Admin/UserRoundResultConroller/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoundResult = await _context.UserRoundResult.FindAsync(id);
            if (userRoundResult == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userRoundResult.AppUserId);
            ViewData["GameRoundId"] = new SelectList(_context.GameRound, "Id", "Id", userRoundResult.GameRoundId);
            return View(userRoundResult);
        }

        // POST: Admin/UserRoundResultConroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,GameRoundId,Result,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] UserRoundResult userRoundResult)
        {
            if (id != userRoundResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRoundResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoundResultExists(userRoundResult.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userRoundResult.AppUserId);
            ViewData["GameRoundId"] = new SelectList(_context.GameRound, "Id", "Id", userRoundResult.GameRoundId);
            return View(userRoundResult);
        }

        // GET: Admin/UserRoundResultConroller/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoundResult = await _context.UserRoundResult
                .Include(u => u.AppUser)
                .Include(u => u.GameRound)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRoundResult == null)
            {
                return NotFound();
            }

            return View(userRoundResult);
        }

        // POST: Admin/UserRoundResultConroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userRoundResult = await _context.UserRoundResult.FindAsync(id);
            _context.UserRoundResult.Remove(userRoundResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoundResultExists(Guid id)
        {
            return _context.UserRoundResult.Any(e => e.Id == id);
        }
    }
}
