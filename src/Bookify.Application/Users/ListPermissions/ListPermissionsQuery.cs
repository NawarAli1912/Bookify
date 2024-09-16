using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Users.ListPermissions;

public sealed class ListPermissionsQuery() : IQuery<IReadOnlyCollection<PermissionResponse>>;
