using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstraction;
using Dapper;

namespace Bookify.Application.Users.ListPermissions;
internal sealed class ListPermissionsQueryHandler : IQueryHandler<ListPermissionsQuery, IReadOnlyCollection<PermissionResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ListPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyCollection<PermissionResponse>>> Handle(ListPermissionsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var permissions = await connection.QueryAsync<PermissionResponse>(
            """
                SELECT 
                    *
                FROM 
                    permission
            """);

        return permissions.ToList();
    }
}
