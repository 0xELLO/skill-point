using System.Data;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using App.Public.DTO;
using App.Public.DTO.Identity;
using Base.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApp.ApiControllers.Identity;
/// <summary>
/// Represents logging RESTful service.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Random _rnd = new Random();
    private AppDbContext _context;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
        ILogger<AccountController> logger, IConfiguration configuration, AppDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _configuration = configuration;
        _context = context;
    }
    /// <summary>
    /// Verifies user exists in the system
    /// </summary>
    /// <param name="loginData">Login data: username, password</param>
    /// <returns>User JWT tokens</returns>
    /// <responce code="404">User not found</responce>
    /// <responce code="200">User verification successful</responce>
    [HttpPost]
    public async Task<ActionResult<JwtResponse>> LogIn([FromBody]Login loginData)
    {
        //verify username
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed email {} not found", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }
        
        // verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed password for email {} ", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        } 
        
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get claimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }
        
        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
            );

        appUser.RefreshTokens = await _context
            .Entry(appUser)
            .Collection(a => a.RefreshTokens!)
            .Query()
            .Where(t => t.AppUserId == appUser.Id)
            .ToListAsync();

        foreach (var appUserRefreshToken in appUser.RefreshTokens)
        {
            if (appUserRefreshToken.ExpirationTime < DateTime.UtcNow
                && appUserRefreshToken.PreviousExpirationTime < DateTime.UtcNow)
            {
                _context.RefreshTokens.Remove(appUserRefreshToken);
            }
        }

        var refreshToken = new RefreshToken();
        refreshToken.AppUserId = appUser.Id;
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            Email = appUser.Email
        };
        return Ok(res);
    }
    
    /// <summary>
    /// Registers user into the system.
    /// </summary>
    /// <param name="registrationData">Registration data: login, password</param>
    /// <returns>User JWT tokens</returns>
    /// <responce code="400">User validation problems</responce>
    /// <responce code="200">User registration successful</responce>
    [HttpPost]
    public async Task<ActionResult<JwtResponse>> Register(Register registrationData)
    {
        // verify user
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("WebApi login failed password for email {} ", registrationData.Email);
            var error = new RestApiErrorResponse()
            {
                Type = "bad request",
                Title = "App error",
                Status = (int) HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };
            error.Errors["email"] = new List<string>()
            {
                "Email already registered"
            };
            return BadRequest(error);
        }

        var refreshToken = new RefreshToken();
        appUser = new AppUser()
        {
            UserName = registrationData.Email,
            Email = registrationData.Email,
            RefreshTokens = new List<RefreshToken>()
            {
                refreshToken
            }
        };
        
        // validate
        var passwordValidator = new PasswordValidator<AppUser>();
        var passwordValidationRes = await passwordValidator.ValidateAsync(_userManager, appUser, registrationData.Password);
        if (!passwordValidationRes.Succeeded)
        {
            return BadRequest(passwordValidationRes.Errors.First().Description);
        }
        
        var trimmedEmail = appUser.Email.Trim();

        if (trimmedEmail.EndsWith(".")) {
            return BadRequest("Enter valid email address");
        }
        try {
            var addr = new System.Net.Mail.MailAddress(appUser.Email);
            if (addr.Address != trimmedEmail)
            {
                return BadRequest("Enter valid email address");
            }
        }
        catch {
            return BadRequest("Enter valid email address");
        }
        
        // create user (system will do it)
        var result = await _userManager.CreateAsync(appUser, registrationData.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.FirstOrDefault());
        }
        
        // get full user from system with fixed ddata
        appUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (appUser == null)
        {
            _logger.LogWarning("User with email {} is not found after the registration", registrationData.Email);
            return BadRequest($"User with email { registrationData.Email } is not found after the registration");
        }

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get claimsPrincipal for user {}", registrationData.Email);
            return NotFound("User/Password problem");
        }
        
        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );
        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            Email = appUser.Email
        };
        return Ok(res);
    }
    /// <summary>
    /// Refreshes JWT token
    /// </summary>
    /// <param name="refreshTokenModel">JWT token</param>
    /// <returns>New JWT token</returns>
    [HttpPost]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel)
    {
        // get info from JWT
        JwtSecurityToken jwtToken;
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
            if (jwtToken == null)
            {
                return BadRequest("No token");
            }
        }
        catch (Exception e)
        {
            return BadRequest($"Can't parse the token {e.Message}");
        }
        
        // validate token siganture
        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest("No email");
        }
        
        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return BadRequest($"User with email {userEmail} not found");
        }
        
        // load and compare refresh tokens
        await _context.Entry(appUser).Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x =>
                (x.Token == refreshTokenModel.RefreshToken && x.ExpirationTime > DateTime.UtcNow) ||
                (x.PreviousToken == refreshTokenModel.RefreshToken && x.PreviousExpirationTime > DateTime.UtcNow))
            .ToListAsync();
        
        if (appUser.RefreshTokens == null)
        {
            return Problem("RefreshTokens collection is null");
        }
        
        if (appUser.RefreshTokens.Count == 0)
        {
            return Problem("No valid refresh tokens found, collection in empty");
        }
        
        if (appUser.RefreshTokens.Count != 1)
        {
            return Problem("Mote then one valid refresh token found");
        }
        
        // genrate new jwt
        
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get claimsPrincipal for user {}", userEmail);
            return NotFound("User/Password problem");
        }
        
        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );
        
        // make new refresh token, onsolate old ones
        var refreshToken = appUser.RefreshTokens.First();
        if (refreshToken.Token == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousExpirationTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.ExpirationTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
        }
  
        
        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            Email = appUser.Email
        };
        return Ok(res);
    }
}