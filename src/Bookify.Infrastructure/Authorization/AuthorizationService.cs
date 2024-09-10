﻿using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;
internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _context;

    public AuthorizationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        return _context.Set<User>()
            .Where(user => user.IdentityId == identityId)
            .Select(user => new UserRolesResponse
            {
                UserId = user.Id,
                Roles = user.Roles.ToList()
            }).FirstAsync();
    }
}
