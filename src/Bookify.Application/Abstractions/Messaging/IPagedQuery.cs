using Bookify.Domain.Abstraction;
using MediatR;

namespace Bookify.Application.Abstractions.Messaging;

public interface IPagedQuery<TResponse> : IRequest<PagedResult<TResponse>>
{
    int PageSize { get; init; }

    int PageNumber { get; init; }
}
