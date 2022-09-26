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
    public class MatchController : ControllerBase
    {
        private readonly IAppBll _bll;

        public MatchController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: api/Match
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDTO>>> GetMatch()
        {
            var res = (await _bll.MatchService.GetAllAsync())
                .Select(x => new MatchDTO
                {
                    Id = x.Id,
                    MatchTypeId = x.MatchTypeId,
                    MatchToken = x.MatchToken,
                    StartTime = x.StartTime,
                    FinishTime = x.FinishTime,
                    MaxPlayers = x.MaxPlayers,
                    OpenedToJoin = x.OpenedToJoin,
                    MatchType = _bll.MatchTypeService.FirstOrDefault(x.MatchTypeId)!.Name
                })
                .ToList();
            return res;
        }

        // GET: api/Match/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDTO>> GetMatch(Guid id)
        {
            var gameMatch = await _bll.MatchService.FirstOrDefaultAsync(id);

            if (gameMatch == null)
            {
                return NotFound();
            }

            return new MatchDTO
            {
                Id = gameMatch.Id,
                MatchTypeId = gameMatch.MatchTypeId,
                MatchToken = gameMatch.MatchToken,
                StartTime = gameMatch.StartTime,
                FinishTime = gameMatch.FinishTime,
                MaxPlayers = gameMatch.MaxPlayers,
                OpenedToJoin = gameMatch.OpenedToJoin
            };
        }

        // PUT: api/Match/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(Guid id, App.Bll.DTO.Match match)
        {
            if (id != match.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _bll.MatchService.Update(match);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Match
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MatchDTO>> PostMatch(MatchDTO matchDTO)
        {
            var match = new App.Bll.DTO.Match
            {
                Id = Guid.NewGuid(),
                MatchToken = Guid.NewGuid().ToString(),
                StartTime = DateTime.UtcNow,
                MaxPlayers = matchDTO.MaxPlayers,
                OpenedToJoin = matchDTO.OpenedToJoin,
                MatchTypeId = _bll.MatchTypeService.GetByName(matchDTO.MatchType ?? "singleplayer").Result.Id,
            };
            
            _bll.MatchService.Add(match);
            await _bll.SaveChangesAsync();
            Console.WriteLine(match.Id + "IDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");

            return CreatedAtAction("GetMatch", new
            {
                id = match.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, match);
        }

        // DELETE: api/Match/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(Guid id)
        {
            var match = await _bll.MatchService.FirstOrDefaultAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _bll.MatchService.Remove(match);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(Guid id)
        {
            return _bll.MatchService.Exists(id);
        }
    }
}
