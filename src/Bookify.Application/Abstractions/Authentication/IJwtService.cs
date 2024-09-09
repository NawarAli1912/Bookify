
using Bookify.Domain.Abstraction;

namespace Bookify.Application.Abstractions.Authentication;
public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken);
}
