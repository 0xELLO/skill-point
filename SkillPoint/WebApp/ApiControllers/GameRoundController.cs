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
using System.Security.Claims;
using App.Bll.DTO;
using App.Public.DTO;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GameRoundController : ControllerBase
    {
        private readonly IAppBll _bll;

        public GameRoundController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: api/GameRound
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameRoundDTO>>> GetGameRound()
        {
            var res = (await _bll.GameRoundService.GetAllAsync())
                .Select(x => new GameRoundDTO
                {
                    Id = x.Id,
                    Opened = x.Opened,
                    GameId = x.GameId,
                    MatchId = x.MatchId,
                    GameContentId = x.GameContentId
                })
                .ToList();
            return res;
        }

        // GET: api/GameRound/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameRoundDTO>> GetGameRound(Guid id)
        {
            var gameRound = await _bll.GameRoundService.FirstOrDefaultAsync(id);

            if (gameRound == null)
            {
                return NotFound();
            }
            
            return new GameRoundDTO
            {
                Id = id,
                GameId = gameRound.GameId,
                Opened = gameRound.Opened,
                MatchId = gameRound.MatchId,
                GameContentId = gameRound.GameContentId
            };
        }

        // PUT: api/GameRound/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameRound(Guid id, App.Bll.DTO.GameRound gameRound)
        {
            if (id != gameRound.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _bll.GameRoundService.Update(gameRound);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameRoundExists(id))
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

        // POST: api/GameRound
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameRoundDTO>> PostGameRound(App.Bll.DTO.GameRound gameRound)
        {
            gameRound.Id = Guid.NewGuid();
            var game = await _bll.Games.FirstOrDefaultAsync(gameRound.GameId);
            if (game == null)
            {
                return BadRequest();
            }
            var content = game.LogoUrl == "keyboard" ? await _bll.GameContentServices.GetRandomTypingGame() : null;
            gameRound.GameContentId = content?.Id;

            _bll.GameRoundService.Add(gameRound);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetGameRound", new
            {
                id = gameRound.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, gameRound);
        }

        // DELETE: api/GameRound/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameRound(Guid id)
        {
            var gameRound = await _bll.GameRoundService.FirstOrDefaultAsync(id);
            if (gameRound == null)
            {
                return NotFound();
            }

            _bll.GameRoundService.Remove(gameRound);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool GameRoundExists(Guid id)
        {
            return _bll.GameRoundService.Exists(id);
        }
    }
}
