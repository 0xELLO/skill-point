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
    public class GameContentController : Controller
    {
        private readonly AppDbContext _context;

        public GameContentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/GameContent
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GameContent.Include(g => g.Game);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/GameContent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameContent = await _context.GameContent
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameContent == null)
            {
                return NotFound();
            }

            return View(gameContent);
        }

        // GET: Admin/GameContent/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id");
            return View();
        }

        // POST: Admin/GameContent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,Content,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] GameContent gameContent)
        {
            if (ModelState.IsValid)
            {
                gameContent.Id = Guid.NewGuid();
                _context.Add(gameContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameContent.GameId);
            return View(gameContent);
        }

        // GET: Admin/GameContent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameContent = await _context.GameContent.FindAsync(id);
            if (gameContent == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameContent.GameId);
            return View(gameContent);
        }

        // POST: Admin/GameContent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GameId,Content,CratedBy,CratedAt,UpdatedBy,UpdatedAt,Id")] GameContent gameContent)
        {
            if (id != gameContent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameContentExists(gameContent.Id))
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
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameContent.GameId);
            return View(gameContent);
        }

        // GET: Admin/GameContent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameContent = await _context.GameContent
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameContent == null)
            {
                return NotFound();
            }

            return View(gameContent);
        }

        // POST: Admin/GameContent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gameContent = await _context.GameContent.FindAsync(id);
            _context.GameContent.Remove(gameContent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameContentExists(Guid id)
        {
            return _context.GameContent.Any(e => e.Id == id);
        }
    }
}
