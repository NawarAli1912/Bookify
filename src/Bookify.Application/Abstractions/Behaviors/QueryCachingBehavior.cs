using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstractions.Behaviors;
internal class QueryCachingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery<TRequest>
    where TResponse : Result
{
    private readonly ICacheService _cahceService;
    private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger;

    public QueryCachingBehavior(
        ICacheService cahceService,
        ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
    {
        _cahceService = cahceService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? cacheResult = await _cahceService.GetAsync<TResponse>(
            request.CacheKey,
            cancellationToken);

        string name = typeof(TRequest).Name;

        if (cacheResult is not null)
        {
            _logger.LogInformation("Cache hit for {Query}", name);

            return cacheResult;
        }

        _logger.LogInformation("Cache miss for {Query}", name);

        var result = await next();

        if (result.IsSuccess)
        {
            await _cahceService.SetAsync(request.CacheKey, result, request.Duration, cancellationToken);
        }

        return result;

    }
}
