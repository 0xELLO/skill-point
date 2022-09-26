using App.Bll.DTO;
using App.Contracts.BLL;
using App.DAL.EF;
using App.Domain.Identity;
using App.Public.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class HelpersController : ControllerBase
{
    private AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAppBll _bll;

    public HelpersController(AppDbContext context, UserManager<AppUser> userManager, IAppBll bll)
    {
        _context = context;
        _userManager = userManager;
        _bll = bll;
    }
    
    [HttpGet("GetUsersByUserInMatch/{id}")]
    public async Task<ActionResult<UsersDTO>> GetUsersByUserInMatch(Guid id, [FromHeader] string authorization)
    {
        var appUser = await Helpers.VerifyUser(authorization, _userManager);
        if (appUser == null)
        {
            return BadRequest("token problem");
        }
        
        var match = await _context.Match.FindAsync(id);
        if (match == null)
        {
            return BadRequest("id problem");
        }
        if (!_context.UserInMatch.Any(x => x.AppUserId == appUser.Id && x.MatchId == match.Id))
        {
            return BadRequest("can't access");
        }

        //var list = await _context.UserInMatch.Where(x => x.MatchId == match.Id)
        //    .Select(x => x.AppUser!.Email).ToListAsync();
        var list = await _bll.UserInMatchService.GetJoinedUserEmail(id);
        return new UsersDTO
        {
            Emails = new List<string>(list)
        };
    }

    [HttpGet("JoinMatch/{token}")]
    public async Task<ActionResult<MatchDTO>> JoinMatch(string token, [FromHeader] string authorization)
    {
        var appUser = await Helpers.VerifyUser(authorization, _userManager);
        if (appUser == null)
        {
            return BadRequest("token problem");
        }

        var match = await _bll.MatchService.GetMatchByToken(token);
        if (match == null)
        {
            return BadRequest("match token problem");
        }

        if (!match.OpenedToJoin)
        {
            return BadRequest("match closed");
        }

        
        _bll.UserInMatchService.Add(new UserInMatch
            {
                Id = Guid.NewGuid(),
                AppUserId = appUser.Id,
                MatchId = match.Id,
            }
        );
        
        await _bll.SaveChangesAsync();
        
        return new MatchDTO
        {
            Id = match.Id,
            MatchTypeId = match.MatchTypeId,
            MatchToken = match.MatchToken,
            StartTime = match.StartTime,
            MaxPlayers = match.MaxPlayers,
            OpenedToJoin = match.OpenedToJoin
        };
    }

    [HttpGet("GetOpenedRound/{id}")]
    public async Task<ActionResult<GameRoundDTO>> GetOpenedRound(string id, [FromHeader] string authorization)
    {
        var appUser = await Helpers.VerifyUser(authorization, _userManager);
        if (appUser == null)
        {
            return BadRequest("token problem");
        }

        var match =await _bll.MatchService.FirstOrDefaultAsync(Guid.Parse(id));
        if (match == null)
        {
            return BadRequest("id problem");
        }

        if (!(await _bll.UserInMatchService.GetAllAsync())
            .Any(inMatch => inMatch.AppUserId == appUser.Id && inMatch.MatchId == match.Id))
        {
            return BadRequest("can't access, join first");
        }

        var gameRound  = (await _bll.GameRoundService.GetAllAsync()).FirstOrDefault(round => round.MatchId == match.Id && round.Opened == true);
        if (gameRound == null)
        {
            return BadRequest("could not find opened round");
        }

        return new GameRoundDTO
        {
            Id = gameRound.Id,
            GameId = gameRound.GameId,
            MatchId = gameRound.MatchId,
            GameContentId = gameRound.GameContentId,
            Opened = gameRound.Opened
        };
    }

    [HttpGet("GetUserEmailByRoundResultId/{id}")]
    public async Task<ActionResult<UsersDTO>> GetUserEmailByRoundResultId(string id, [FromHeader] string authorization)
    {
        var userRoundResult = _bll.UserRoundResultService.FirstOrDefault(Guid.Parse(id));
        var appUser = await _userManager.FindByIdAsync(userRoundResult!.AppUserId.ToString());
        return new UsersDTO
        {
            Emails = new List<string>
            {
                appUser.Email
            }
        };
    }
    
    [HttpGet("GetUserRoundResultByRound/{roundId}")]
    public async Task<ActionResult<IEnumerable<UserRoundResultDTO>>> GetUserRoundResultByRound(string roundId, [FromHeader] string authorization)
    {
        var appUser = await Helpers.VerifyUser(authorization, _userManager);
        if (appUser == null)
        {
            return BadRequest($"Token problem");
        }
        
        var res = await Task.WhenAll((await _bll.UserRoundResultService.GetAllAsync()).Where(round => round.GameRoundId == Guid.Parse(roundId))
            .Select(async x => new UserRoundResultDTO
            {
                Id = x.Id,
                AppUserId = x.AppUserId,
                GameRoundId = x.GameRoundId,
                Result = x.Result,
                Email = (await _userManager.FindByIdAsync(  x.AppUserId!.ToString()!)).Email
            })
            .ToList());
        return res;
    }

}