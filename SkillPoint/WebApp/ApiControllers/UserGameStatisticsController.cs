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
using App.Domain.Identity;
using App.Public.DTO;
using Microsoft.AspNetCore.Identity;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserGameStatisticsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserManager<App.Domain.Identity.AppUser> _userManager;

        public UserGameStatisticsController(IAppBll bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: api/UserGameStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGameStatisticsDTO>>> GetUserGameStatistics([FromHeader] string authorization)
        {
            var appUser = await Helpers.VerifyUser(authorization, _userManager);
            if (appUser == null)
            {
                return BadRequest($"Token problem");
            }
            
            var res = (await _bll.UserGameStatisticsService.GetByUserId(appUser.Id))
                .Select(x => new UserGameStatisticsDTO
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    GameId = x.GameId,
                    AverageScore = x.AverageScore,
                    BestScore = x.BestScore,
                    Rating = x.Rating
                })
                .ToList();
            return res;
        }

        // GET: api/UserGameStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGameStatisticsDTO>> GetUserGameStatistics(Guid id)
        {
            var userGameStatistics = await _bll.UserGameStatisticsService.FirstOrDefaultAsync(id);

            if (userGameStatistics == null)
            {
                return NotFound();
            }
            
            return new UserGameStatisticsDTO
            {
                Id = userGameStatistics.Id,
                AppUserId = userGameStatistics.AppUserId,
                GameId = userGameStatistics.GameId,
                AverageScore = userGameStatistics.AverageScore,
                BestScore = userGameStatistics.BestScore,
                Rating = userGameStatistics.Rating
            };
        }

        // PUT: api/UserGameStatistics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserGameStatistics(Guid id, App.Bll.DTO.UserGameStatistics userGameStatistics)
        {
            if (id != userGameStatistics.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _bll.UserGameStatisticsService.Update(userGameStatistics);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGameStatisticsExists(id))
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

        // POST: api/UserGameStatistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserGameStatisticsDTO>> PostUserGameStatistics(App.Bll.DTO.UserGameStatistics userGameStatistics)
        {
            userGameStatistics.Id = Guid.NewGuid();
            var res = await _bll.UserGameStatisticsService.FirstOrDefaultAsync(userGameStatistics.GameId);
            if (res == null)
            {
                return BadRequest();
            }

            _bll.UserGameStatisticsService.Add(userGameStatistics);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserGameStatistics", new
            {
                id = userGameStatistics.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, userGameStatistics);
        }

        // DELETE: api/UserGameStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGameStatistics(Guid id)
        {
            var userGameStatistics = await _bll.UserGameStatisticsService.FirstOrDefaultAsync(id);
            if (userGameStatistics == null)
            {
                return NotFound();
            }

            _bll.UserGameStatisticsService.Remove(userGameStatistics);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool UserGameStatisticsExists(Guid id)
        {
            return _bll.UserGameStatisticsService.Exists(id);
        }
    }
}
