using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vulns.App;
using Vulns.Core;

namespace Vulns.Web;

[ApiController]
[Route("products")]
[Authorize]
public class ProductsController : ApiControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IMapper _mapper;
    private readonly ProductSearchService _search;

    public ProductsController(ILogger<ProductsController> logger, IMapper mapper, ProductSearchService search)
        => (_logger, _mapper, _search) = (logger, mapper, search);

    /// <summary>Retrieves product details</summary>
    /// <param name="cpeId">Product ID</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">A detailed view of a Product</response>
    /// <response code="400">The format of the provided CPE-ID is invalid.</response>
    /// <response code="401">The user is not authorized to view the requested resources</response>
    /// <response code="404">No Product with the given ID was found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet(@"{cpeId:regex(^cpe:2.3:[[aho]](:(\\:|[[^:]])+){{10}}$)}")]
    public async Task<object> GetAsync(
        [FromRoute] string cpeId,
        CancellationToken token)
    {
        var product = await _search.GetById(cpeId, token);
        return product != null ? _mapper.Map<ProductDetails>(product) : NotFoundResponse();
    }

    /// <summary>Typeahead search</summary>
    /// <param name="phrase">The phrase to search for</param>
    /// <param name="size">The maximum number of items to put in the response</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">Returns a list with at least one item that matches the criteria in the result set</response>
    /// <response code="400">The input in the request parameters is not valid.</response>
    /// <response code="401">The user is not authorized to view the requested resources.</response>
    /// <response code="404">No items matching the given criteria were found.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationSet<ProductFragment>))]
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
        var productsDto = _mapper.Map<IEnumerable<ProductFragment>>(searchResult.Results);
        var results = new PaginationSet<ProductFragment>(productsDto, 1, searchResult.Total);
        return results.Size > 0 ? results : NotFoundResponse();
    }

    /// <summary>Typeahead search for vendors</summary>
    /// <param name="phrase">Vendor phrase to search for</param>
    /// <param name="size">The number of results to get</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">A list of results that match the request parameters</response>
    /// <response code="400">Request parameters do not respect the constraints set by the backend</response>
    /// <response code="401">The user is not authorized to view the requested resources</response>
    /// <response code="404">No matches the given criteria</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationSet<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet("typeahead/vendors/{phrase}")]
    public async Task<object> TypeaheadVendorsAsync(
        [FromRoute] string phrase,
        [FromQuery][Range(1, 100)] int size = 10,
        CancellationToken token = default)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var searchResult = await _search.TypeaheadVendorsAsync(phrase, size, token);
        var results = new PaginationSet<string>(searchResult.Results, 1, 314159265358979323);
        return results.Size > 0 ? results : NotFoundResponse();
    }

    /// <summary>Typeahead search for products by a given vendor</summary>
    /// <param name="vendor">Vendor name</param>
    /// <param name="phrase">Product phrase to search for</param>
    /// <param name="size">The number of results to get</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">A list of results that match the request parameters</response>
    /// <response code="400">Request parameters do not respect the constraints set by the backend</response>
    /// <response code="401">The user is not authorized to view the requested resources</response>
    /// <response code="404">No matches the given criteria</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationSet<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet("typeahead/vendors/{vendor}/products/{phrase}")]
    public async Task<object> TypeaheadVendorProductsAsync(
        [FromRoute] string vendor,
        [FromRoute] string phrase,
        [FromQuery][Range(1, 100)] int size = 10,
        CancellationToken token = default)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var searchResult = await _search.TypeaheadVendorProductsAsync(vendor, phrase, size, token);
        var results = new PaginationSet<string>(searchResult.Results, 1, 314159265358979323);
        return results.Size > 0 ? results : NotFoundResponse();
    }

    /// <summary>Typeahead search for versions of a product by a given vendor</summary>
    /// <param name="vendor">Vendor name</param>
    /// <param name="product">Product name</param>
    /// <param name="phrase">Version phrase to search for</param>
    /// <param name="size">The number of results to get</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">A list of results that match the request parameters</response>
    /// <response code="400">Request parameters do not respect the constraints set by the backend</response>
    /// <response code="401">The user is not authorized to view the requested resources</response>
    /// <response code="404">No matches the given criteria</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationSet<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet("typeahead/vendors/{vendor}/products/{product}/versions/{phrase}")]
    public async Task<object> TypeaheadVendorProductVersionsAsync(
        [FromRoute] string vendor,
        [FromRoute] string product,
        [FromRoute] string phrase,
        [FromQuery][Range(1, 100)] int size = 10,
        CancellationToken token = default)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var searchResult = await _search.TypeaheadVendorProductVersionsAsync(vendor, product, phrase, size, token);
        var results = new PaginationSet<string>(searchResult.Results, 1, searchResult.Total);
        return results.Size > 0 ? results : NotFoundResponse();
    }
}
