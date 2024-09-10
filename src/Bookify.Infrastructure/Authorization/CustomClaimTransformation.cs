using Bookify.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bookify.Infrastructure.Authorization;
internal sealed class CustomClaimTransformation : IClaimsTransformation
{
    private readonly IServiceProvider _serviceProvider;

    public CustomClaimTransformation(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(claim => claim.Type == ClaimTypes.Role) &&
            principal.HasClaim(claim => claim.Type == JwtRegisteredClaimNames.Sub))
        {
            return principal;
        }
        using var serviceScope = _serviceProvider.CreateScope();
        var authorizationService = serviceScope.ServiceProvider.GetRequiredService<AuthorizationService>();

        var userResponse = await authorizationService.GetRolesForUserAsync(principal.GetIdentityId());

        var claimsIdneity = new ClaimsIdentity();

        claimsIdneity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userResponse.UserId.ToString()));

        foreach (var role in userResponse.Roles)
        {
            claimsIdneity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
        }

        principal.AddIdentity(claimsIdneity);

        return principal;



    }
}
