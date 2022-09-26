#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Bll.DTO;
using App.DAL.EF;
using App.Domain;
using Game = App.Resources.App.Domain.Game;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private readonly IAppBll _bll;

        public GameController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Admin/Game
        public async Task<IActionResult> Index()
        {
            var res = await _bll.Games.GetAllAsync();
            return View(res);
        }

        // GET: Admin/Game/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value)!;

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Admin/Game/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Game/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.Bll.DTO.Game game)
        {
            if (ModelState.IsValid)
            {
                game.Id = Guid.NewGuid();
                _bll.Games.Add(game);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Admin/Game/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Admin/Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, App.Bll.DTO.Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Games.Update(game);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            return View(game);
        }

        // GET: Admin/Game/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Admin/Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Games.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(Guid id)
        {
            return _bll.Games.Exists(id);
        }
    }
}
