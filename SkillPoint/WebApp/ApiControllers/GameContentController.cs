#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using App.Public.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Represents a RESTful GameContent service.    
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameContentController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserManager<AppUser> _userManager;


        public GameContentController(IAppBll bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/GameContent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameContentDTO>>> GetGameContent()
        {
            var res = (await _bll.GameContentServices.GetAllAsync())
                .Select(x => new GameContentDTO
                {
                    Id = x.Id,
                    GameId = x.GameId,
                    Content = x.Content
                })
                .ToList();
            return res;
        }

        // GET: api/GameContent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameContentDTO>> GetGameContent(Guid id)
        {
            var gameContent = await _bll.GameContentServices.FirstOrDefaultAsync(id);

            if (gameContent == null)
            {
                return NotFound();
            }

            return new GameContentDTO
            {
                Id = gameContent.Id,
                GameId = gameContent.GameId,
                Content = gameContent.Content
            };
        }
        
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // PUT: api/GameContent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameContent(Guid id, App.Bll.DTO.GameContent gameContent)
        {
            if (id != gameContent.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _bll.GameContentServices.Update(gameContent);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameContentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // POST: api/GameContent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameContentDTO>> PostGameContent(App.Bll.DTO.GameContent gameContent)
        {
            gameContent.Id = Guid.NewGuid();
            _bll.GameContentServices.Add(gameContent);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetGameContent", new
            {
                id = gameContent.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, gameContent);
        }
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // DELETE: api/GameContent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameContent(Guid id)
        {
            var gameContent = await _bll.GameContentServices.FirstOrDefaultAsync(id);
            if (gameContent == null)
            {
                return NotFound();
            }

            _bll.GameContentServices.Remove(gameContent);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool GameContentExists(Guid id)
        {
            return _bll.GameContentServices.Exists(id);
        }
    }
}
