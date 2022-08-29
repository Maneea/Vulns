using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vulns.App;

namespace Vulns.Web;

[ApiController]
[Route("vulnerabilities")]
[Authorize]
public class VulnerabilitiesController : ApiControllerBase
{
    private readonly ILogger<VulnerabilitiesController> _logger;
    private readonly IMapper _mapper;
    private readonly VulnerabilityAggregationService _aggregation;
    private readonly VulnerabilitySearchService _search;

    public VulnerabilitiesController(
        ILogger<VulnerabilitiesController> logger,
        IMapper mapper,
        VulnerabilitySearchService search,
        VulnerabilityAggregationService aggregation)
        => (_logger, _mapper, _search, _aggregation) = (logger, mapper, search, aggregation);

    /// <summary>Retrieves Vulnerability details</summary>
    /// <param name="cveId">Vulnerability ID</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">A detailed view of a Vulnerability</response>
    /// <response code="400">The format of the provided CVE-ID is invalid.</response>
    /// <response code="401">The user is not authorized to view the requested resources</response>
    /// <response code="404">No Vulnerability with the given ID was found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VulnerabilityDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet(@"{cveId:regex(^cve-\d{{4}}-\d{{4,20}}$)}")]
    public async Task<object> GetAsync([FromRoute] string cveId, CancellationToken token)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var vulnerability = await _search.GetById(cveId, token);
        return vulnerability != null ? _mapper.Map<VulnerabilityDetails>(vulnerability) : NotFoundResponse();
    }

    /// <summary>Latest vulnerabilities</summary>
    /// <param name="offset">Numbers of records to skip</param>
    /// <param name="size">Number of records to return</param>
    /// <param name="field">Return latest based on this parameer</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">Returns a list with at least one item</response>
    /// <response code="400">The input in the request parameters is not valid.</response>
    /// <response code="401">The user is not authorized to view the requested resources.</response>
    /// <response code="404">No vulnerabilities matching the given criteria were found.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VulnerabilityFragment>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorSet<string>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet("latest")]
    public async Task<object> LatestAsync(
        int offset = 0,
        int size = 10,
        VulnerabilitySortField field = VulnerabilitySortField.ModificationDate,
        CancellationToken token = default)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var vulnerabilities = await _search.GetLatestAsync(offset, size, (SortFields)field, token);
        var cveListDto = _mapper.Map<IEnumerable<VulnerabilityFragment>>(vulnerabilities);
        return cveListDto.Count() > 0 ? cveListDto : NotFoundResponse();
    }

    /// <summary>Typeahead search</summary>
    /// <param name="phrase">The phrase to search for</param>
    /// <param name="size">The maximum number of items to put in the response</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">Returns a list with at least one item that matches the criteria in the result set</response>
    /// <response code="400">The input in the request parameters is not valid.</response>
    /// <response code="401">The user is not authorized to view the requested resources.</response>
    /// <response code="404">No items matching the given criteria were found.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationSet<VulnerabilityFragment>))]
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
        var cveListDto = _mapper.Map<IEnumerable<VulnerabilityFragment>>(searchResult.Results);
        var results = new PaginationSet<VulnerabilityFragment>(cveListDto, 1, searchResult.Total);
        return results.Size > 0 ? results : NotFoundResponse();
    }

    /// <summary>Search for vulnerabilities</summary>
    /// <param name="searchRequest">Pagination request that includes the page number and the size of the result set</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">Returns a list with at least one item that matches the criteria in the result set</response>
    /// <response code="400">The input in the request parameters is not valid.</response>
    /// <response code="401">The user is not authorized to view the requested resources.</response>
    /// <response code="404">No vulnerabilities matching the given criteria were found.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationSet<VulnerabilityFragment>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorSet<string>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet("search")]
    public async Task<object> SearchAsync([FromQuery] VulnerabilitySearchFilters searchRequest, CancellationToken token)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var searchCriteria = _mapper.Map<VulnerabilitySearchCriteria>(searchRequest);
        var searchResponse = await _search.SearchAsync(searchCriteria, token);
        var vulnerabilitiesDto = _mapper.Map<IEnumerable<VulnerabilityFragment>>(searchResponse.Results);
        var results = new PaginationSet<VulnerabilityFragment>(vulnerabilitiesDto, searchRequest.Page, searchResponse.Total);
        return results.Size > 0 ? results : NotFoundResponse();
    }

    /// <summary>Valid values for search parameters</summary>
    /// <response code="200">Returns a list of valid values for search parameters</response>
    /// <response code="401">The user is not authorized to view the requested resources.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VulnerabilitySearchParametersValues))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorSet<string>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("search/parameters")]
    public dynamic SearchParameterValues(CancellationToken token) => _search.GetSearchParametersValuesAsync(token);

    /// <summary>Vulnerabilities statistics</summary>
    /// <response code="200">Returns a dynamically-generated list of results based on the aggregation criteria fields and types</response>
    /// <response code="400">The input in the request parameters is not valid</response>
    /// <response code="401">The user is not authorized to view the requested resources</response>
    /// <response code="406">The way the aggregation request is stated does not make sense. A specific error is returned</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorSet<string>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(Error))]
    [HttpGet("statistics")]
    public async Task<object> StatisticsAsync(
        [FromQuery] VulnerabilityAggregationFilters filters,
        [FromQuery] VulnerabilitySearchFilters searchRequest,
        CancellationToken token)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var aggregationCriteria = _mapper.Map<VulnerabilityAggregationCriteria>(filters);
        var searchCriteria = _mapper.Map<VulnerabilitySearchCriteria>(searchRequest);
        try
        {
            return await _aggregation.AggregateAsync(aggregationCriteria, searchCriteria, token);
        }
        catch (ArgumentException e)
        {
            return NotAcceptableResponse(e.Message);
        }
    }
}
