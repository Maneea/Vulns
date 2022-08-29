using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vulns.App;
using Vulns.Core;
using Vulns.Infrastructure;

namespace Vulns.Web;

[ApiController]
[Route("weaknesses")]
[Authorize]
public class WeaknessesController : ApiControllerBase
{
    private readonly ILogger<WeaknessesController> _logger;
    private readonly SearchService<Weakness> _search;
    private readonly IMapper _mapper;

    public WeaknessesController(
        ILogger<WeaknessesController> logger,
        SearchService<Weakness> search,
        IMapper mapper)
            => (_logger, _search, _mapper) = (logger, search, mapper);

    /// <summary>Weakness details</summary>
    /// <param name="cweId">Common Weakness Enumeration Identifier (CWE-ID)</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">A detailed view of a Weakness</response>
    /// <response code="400">The format of the provided CWE-ID is invalid.</response>
    /// <response code="401">The user is not authorized to view the requested resources</response>
    /// <response code="404">No Weakness with the given ID was found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeaknessDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet(@"{cweId:regex(^(cwe-)?\d{{1,4}}$)}")]
    public async Task<object> GetDetailsAsync([FromRoute] string cweId, CancellationToken token)
    {
        var weakness = await _search.GetById(cweId, token);
        return weakness != null ? _mapper.Map<WeaknessDetails>(weakness) : NotFoundResponse();
    }

    /// <summary>Typeahead search</summary>
    /// <param name="phrase">The phrase to search for</param>
    /// <param name="size">The maximum number of items to put in the response</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">Returns a list with at least one item that matches the criteria in the result set</response>
    /// <response code="400">The input in the request parameters is not valid.</response>
    /// <response code="401">The user is not authorized to view the requested resources.</response>
    /// <response code="404">No items matching the given criteria were found.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationSet<WeaknessFragment>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorSet<string>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet("typeahead")]
    public async Task<object> TypeaheadAsync(
        [FromQuery][Required] string phrase,
        [FromQuery][Range(1, 100)] int size = 10,
        CancellationToken token = default)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var searchResult = await _search.TypeaheadAsync(phrase, size, token);
        var weaknessesDto = _mapper.Map<IEnumerable<WeaknessFragment>>(searchResult.Results);
        var results = new PaginationSet<WeaknessFragment>(weaknessesDto, 1, searchResult.Total);
        return results.Size > 0 ? results : NotFoundResponse();
    }
}
