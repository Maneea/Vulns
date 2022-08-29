using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vulns.App;
using Vulns.Core;

namespace Vulns.Web;

[ApiController]
[Route("issuers")]
[Authorize]
public class IssuersController : ApiControllerBase
{
    private readonly ILogger<IssuersController> _logger;
    private readonly IMapper _mapper;
    private readonly SearchService<Issuer> _search;

    public IssuersController(
        ILogger<IssuersController> logger,
        SearchService<Issuer> search,
        IMapper mapper)
            => (_logger, _search, _mapper) = (logger, search, mapper);

    /// <summary>CVE Numbering Authorities (aka Publishers/Assigner/Issuer) details</summary>
    /// <param name="cnaId">CVE Numbering Authority Identifier (CNA-ID)</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">A detailed view of a CVE Numbering Authority</response>
    /// <response code="400">The format of the provided CNA-ID is invalid</response>
    /// <response code="401">The user is not authorized to view the requested resources</response>
    /// <response code="404">No CVE Numbering Authority with the given ID was found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IssuerDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet(@"{cnaId:regex(cna-\d{{4}}-\d{{4}})}")]
    public async Task<object> GetDetailsAsync([FromRoute] string cnaId, CancellationToken token)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var cna = await _search.GetById(cnaId, token);
        return cna != null ? _mapper.Map<IssuerDetails>(cna) : NotFoundResponse();
    }

    /// <summary>Typeahead search</summary>
    /// <param name="phrase">The phrase to search for</param>
    /// <param name="size">The maximum number of items to put in the response</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">Returns a list with at least one item that matches the criteria in the result set</response>
    /// <response code="400">The input in the request parameters is not valid.</response>
    /// <response code="401">The user is not authorized to view the requested resources.</response>
    /// <response code="404">No items matching the given criteria were found.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationSet<IssuerFragment>))]
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
        var cnaListDto = _mapper.Map<IEnumerable<IssuerFragment>>(searchResult.Results);
        var results = new PaginationSet<IssuerFragment>(cnaListDto, 1, searchResult.Total);
        return results.Size > 0 ? results : NotFoundResponse();
    }
}
