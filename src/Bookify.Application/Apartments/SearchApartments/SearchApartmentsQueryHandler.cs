using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.SearchApartmentsByDate;
using Bookify.Domain.Abstraction;

namespace Bookify.Application.Apartments.SearchApartments;
internal sealed class SearchApartmentsQueryHandler : IPagedQueryHandler<SearchApartmentsQuery, ApartmentResponse>
{
    public Task<PagedResult<ApartmentResponse>> Handle(
        SearchApartmentsQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
