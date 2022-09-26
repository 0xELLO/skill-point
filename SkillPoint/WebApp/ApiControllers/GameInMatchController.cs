#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Public.DTO;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GameInMatchController : ControllerBase
    {
        private readonly IAppBll _bll;

        public GameInMatchController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: api/GameInMatch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameInMatchDTO>>> GetGameInMatch()
        {
            var res = (await _bll.GameInMatchService.GetAllAsync())
                .Select(x => new GameInMatchDTO
                {
                    Id = x.Id,
                    MatchId = x.MatchId,
                    GameId = x.GameId,
                    RoundAmount = x.RoundAmount
                })
                .ToList();
            return res;
        }

        // GET: api/GameInMatch/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameInMatchDTO>> GetGameInMatch(Guid id)
        {
            var gameInMatch = await _bll.GameInMatchService.FirstOrDefaultAsync(id);

            if (gameInMatch == null)
            {
                return NotFound();
            }

            return new GameInMatchDTO
            {
                Id = gameInMatch.Id,
                MatchId = gameInMatch.MatchId,
                GameId = gameInMatch.GameId,
                RoundAmount = gameInMatch.RoundAmount
            };
        }

        // PUT: api/GameInMatch/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameInMatch(Guid id, App.Bll.DTO.GameInMatch gameInMatch)
        {
            if (id != gameInMatch.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _bll.GameInMatchService.Update(gameInMatch);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameInMatchExists(id))
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

        // POST: api/GameInMatch
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameInMatchDTO>> PostGameInMatch(App.Bll.DTO.GameInMatch gameInMatch)
        {
            gameInMatch.Id = Guid.NewGuid();
            _bll.GameInMatchService.Add(gameInMatch);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetGameInMatch", new
            {
                id = gameInMatch.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, gameInMatch);
        }

        // DELETE: api/GameInMatch/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameInMatch(Guid id)
        {
            var gameInMatch = await _bll.GameInMatchService.FirstOrDefaultAsync(id);
            if (gameInMatch == null)
            {
                return NotFound();
            }

            _bll.GameInMatchService.Remove(gameInMatch);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool GameInMatchExists(Guid id)
        {
            return _bll.GameInMatchService.Exists(id);
        }
    }
}
