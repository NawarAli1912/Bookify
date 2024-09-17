using Bookify.Application.Apartments.SearchApartmentsByDate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Apartments;

[ApiController]
[Route("api/apartments")]
// [Authorize]
public class ApartmentsController : ControllerBase
{
    private readonly ISender _sender;

    public ApartmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchApartments(
        DateOnly startDate,
        DateOnly endDate,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken)
    {
        var query = new SearchApartmentsByDateQuery(startDate, endDate, pageNumber, pageSize);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }
}
