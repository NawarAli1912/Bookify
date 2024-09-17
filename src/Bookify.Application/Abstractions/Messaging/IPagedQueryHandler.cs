using Bookify.Domain.Abstraction;
using MediatR;

namespace Bookify.Application.Abstractions.Messaging;
public interface IPagedQueryHandler<TQuery, TRespone> : IRequestHandler<TQuery, PagedResult<TRespone>>
    where TQuery : IPagedQuery<TRespone>;