using Bookify.Domain.Abstraction;
using MediatR;

namespace Bookify.Application.Abstractions.Messaging;
internal interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
