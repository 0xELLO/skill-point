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
    public class UserInMatchController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserManager<App.Domain.Identity.AppUser> _userManager;


        public UserInMatchController(IAppBll bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: api/UserInMatch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInMatchDTO>>> GetUserInMatch()
        {
            var res = (await _bll.UserInMatchService.GetAllAsync())
                .Select(x => new UserInMatchDTO
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    MatchId = x.MatchId
                })
                .ToList();
            return res;
        }

        // GET: api/UserInMatch/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInMatchDTO>> GetUserInMatch(Guid id)
        {
            var userInMatch = await _bll.UserInMatchService.FirstOrDefaultAsync(id);

            if (userInMatch == null)
            {
                return NotFound();
            }

            return new UserInMatchDTO
            {
                Id = userInMatch.Id,
                AppUserId = userInMatch.AppUserId,
                MatchId = userInMatch.MatchId
            };
        }

        // PUT: api/UserInMatch/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInMatch(Guid id, App.Bll.DTO.UserInMatch userInMatch)
        {
            if (id != userInMatch.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _bll.UserInMatchService.Update(userInMatch);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInMatchExists(id))
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

        // POST: api/UserInMatch
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserInMatch>> PostUserInMatch(App.Bll.DTO.UserInMatch userInMatch, [FromHeader] string authorization)
        {
            userInMatch.Id = Guid.NewGuid();
            var appUser = await Helpers.VerifyUser(authorization, _userManager);
            if (appUser == null)
            {
                return BadRequest("invalid token");
            }
            
            userInMatch.AppUserId = appUser.Id;
            _bll.UserInMatchService.Add(userInMatch);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserInMatch", new
            {
                id = userInMatch.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, userInMatch);
        }

        // DELETE: api/UserInMatch/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInMatch(Guid id)
        {
            var userInMatch = await _bll.UserInMatchService.FirstOrDefaultAsync(id);
            if (userInMatch == null)
            {
                return NotFound();
            }

            _bll.UserInMatchService.Remove(userInMatch);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool UserInMatchExists(Guid id)
        {
            return _bll.UserInMatchService.Exists(id);
        }
    }
}
