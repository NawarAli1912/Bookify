using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;
internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _context;
    private readonly ICacheService _cacheService;

    public AuthorizationService(
        ApplicationDbContext context,
        ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        string key = $"auth:roles-{identityId}";

        var cache = await _cacheService.GetAsync<UserRolesResponse>(key);

        if (cache is not null)
            return cache;

        var userRolesResponse = await _context.Set<User>()
            .Where(user => user.IdentityId == identityId)
            .Select(user => new UserRolesResponse
            {
                UserId = user.Id,
                Roles = user.Roles.ToList()
            }).FirstAsync();

        await _cacheService.SetAsync(key, userRolesResponse);

        return userRolesResponse;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        var key = $"auth:permissions-{identityId}";

        var cacheResult = await _cacheService.GetAsync<HashSet<string>>(key);
        if (cacheResult is not null)
        {
            return cacheResult;
        }


        var permissions = await _context.Set<User>()
            .Where(user => user.IdentityId == identityId)
            .SelectMany(user => user.Roles.Select(role => role.Permissions))
            .FirstAsync();

        var result = permissions.Select(p => p.Name).ToHashSet();

        await _cacheService.SetAsync<HashSet<string>>(key, result);

        return result;
    }
}
