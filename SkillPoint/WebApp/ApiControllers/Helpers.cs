using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp.ApiControllers;

public static class Helpers 
{
    public  static async Task<AppUser?> VerifyUser(string authorization, UserManager<AppUser> userManager)
    {
        var token = authorization.Substring("Bearer ".Length);
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return null;
        }
            
        var appUser = await userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return null;
        }

        return appUser;
    }
}