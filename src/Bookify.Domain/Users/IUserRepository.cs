﻿namespace Bookify.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email);

    void Add(User user);
}
