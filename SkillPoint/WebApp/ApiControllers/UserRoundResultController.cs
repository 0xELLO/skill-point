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
    public class UserRoundResultController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserManager<AppUser> _userManager;


        public UserRoundResultController(IAppBll bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: api/UserRoundResult
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoundResultDTO>>> GetUserRoundResult([FromHeader] string authorization)
        {
            var appUser = await Helpers.VerifyUser(authorization, _userManager);
            if (appUser == null)
            {
                return BadRequest($"Token problem");
            }
            
            var res = (await _bll.UserRoundResultService.GetAllByUserId(appUser.Id))
                .Select(x => new UserRoundResultDTO
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    GameRoundId = x.GameRoundId,
                    Result = x.Result,
                    Email = appUser.Email
                })
                .ToList();
            return res;
        }

        // GET: api/UserRoundResult/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoundResultDTO>> GetUserRoundResult(Guid id)
        {
            var userRoundResult = await _bll.UserRoundResultService.FirstOrDefaultAsync(id);

            if (userRoundResult == null)
            {
                return NotFound();
            }

            return new UserRoundResultDTO
            {
                Id = userRoundResult.Id,
                AppUserId = userRoundResult.AppUserId,
                GameRoundId = userRoundResult.GameRoundId,
                Result = userRoundResult.Result
            };
        }

        // PUT: api/UserRoundResult/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRoundResult(Guid id, App.Bll.DTO.UserRoundResult userRoundResult)
        {
            if (id != userRoundResult.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _bll.UserRoundResultService.Update(userRoundResult);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoundResultExists(id))
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

        // POST: api/UserRoundResult
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRoundResultDTO>> PostUserRoundResult([FromHeader] string authorization, App.Bll.DTO.UserRoundResult userRoundResult)
        {
            userRoundResult.Id = Guid.NewGuid();
            var appUser = await Helpers.VerifyUser(authorization, _userManager);
            if (appUser == null)
            {
                return BadRequest($"Token problem");
            }

            userRoundResult.AppUserId = appUser.Id;
            
            _bll.UserRoundResultService.Add(userRoundResult);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserRoundResult", new
            {
                id = userRoundResult.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, userRoundResult);
        }

        // DELETE: api/UserRoundResult/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRoundResult(Guid id)
        {
            var userRoundResult = await _bll.UserRoundResultService.FirstOrDefaultAsync(id);
            if (userRoundResult == null)
            {
                return NotFound();
            }

            _bll.UserRoundResultService.Remove(userRoundResult);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRoundResultExists(Guid id)
        {
            return _bll.UserRoundResultService.Exists(id);
        }
    }
}
