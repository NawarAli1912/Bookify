using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;
internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public override void Add(User user)
    {
        foreach (var role in user.Roles)
        {
            DbContext.Attach(role);
        }

        DbContext.Add(user);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        var e = new Domain.Users.Email(email);

        return DbContext.Set<User>()
            .Where(u => u.Email == e)
            .FirstOrDefaultAsync();
    }
}
