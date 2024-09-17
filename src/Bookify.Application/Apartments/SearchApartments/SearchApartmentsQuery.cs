using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.SearchApartmentsByDate;

namespace Bookify.Application.Apartments.SearchApartments;
public record SearchApartmentsQuery(int PageNumber, int PageSize) : IPagedQuery<ApartmentResponse>;
