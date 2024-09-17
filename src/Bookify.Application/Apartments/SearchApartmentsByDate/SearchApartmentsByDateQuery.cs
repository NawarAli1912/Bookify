using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Apartments.SearchApartmentsByDate;
public sealed record SearchApartmentsByDateQuery(
    DateOnly StartDate,
    DateOnly EndDate,
    int PageNumber,
    int PageSize) : IPagedQuery<ApartmentResponse>;
