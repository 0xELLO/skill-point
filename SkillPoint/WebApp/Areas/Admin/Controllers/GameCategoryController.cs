#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using GameCategory = App.DAL.DTO.GameCategory;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="admin")]
    public class GameCategoryController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public GameCategoryController(IAppUnitOfWork ouw)
        {
            _uow = ouw;
        }

        // GET: Admin/GameCategory
        public async Task<IActionResult> Index()
        {
            var res = await _uow.GameCategory.GetAllAsync();
            return View(res);
        }

        // GET: Admin/GameCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameCategory = await _uow.GameCategory.FirstOrDefaultAsync(id.Value)!;

            if (gameCategory == null)
            {
                return NotFound();
            }

            return View(gameCategory);
        }

        // GET: Admin/GameCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GameCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( GameCategory gameCategory)
        {
            if (ModelState.IsValid)
            {
                gameCategory.Id = Guid.NewGuid();
                _uow.GameCategory.Add(gameCategory);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameCategory);
        }

        // GET: Admin/GameCategory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                    return NotFound();
            }

            var gameCategory = await _uow.GameCategory.FirstOrDefaultAsync(id.Value);
            if (gameCategory == null)
            {
                return NotFound();
            }
            return View(gameCategory);
        }

        // POST: Admin/GameCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameCategory gameCategory)
        {
            if (id != gameCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.GameCategory.Update(gameCategory);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameCategoryExists(gameCategory.Id))
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
            return View(gameCategory);
        }

        // GET: Admin/GameCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameCategory = await _uow.GameCategory.FirstOrDefaultAsync(id.Value);
            if (gameCategory == null)
            {
                return NotFound();
            }

            return View(gameCategory);
        }

        // POST: Admin/GameCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.GameCategory.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameCategoryExists(Guid id)
        {
            return _uow.GameCategory.Exists(id);
        }
    }
}
