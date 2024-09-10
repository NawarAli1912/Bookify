using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Users.GetLoggedinUser;
public sealed record GetLoggedinUserQuery : IQuery<UserResponse>;
