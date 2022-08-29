using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Vulns.Infrastructure;

namespace Vulns.Web;

[ApiController]
[Route("account")]
[Produces("application/json")]
public class AccountController : ApiControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AccountController(
        ILogger<AccountController> logger,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _logger = logger;
        _userManager = userManager;
        _mapper = mapper;
    }

    /// <summary>Update dashboard widgets</summary>
    /// <param name="widgets">JSON object containing the widgets</param>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">User widgets have been updated</response>
    /// <response code="400">The user could not be update and the reasons are returned</response>
    /// <response code="401">The user is not authorized to update the resources</response>
    /// <response code="404">No User with the given ID in the JWT claims was found</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpPut("widgets")]
    public async Task<object> UpdateWidgetsAsync([FromBody] string widgets, CancellationToken token)
    {
        var userId = _userManager.GetUserId(HttpContext.User);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFoundResponse();

        user.DashboardWidgets = widgets;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return ErrorResponse(result.Errors.Select(e => e.Description));
        return Ok();
    }

    /// <summary>Get dashboard widgets</summary>
    /// <param name="token">Cancellation token</param>
    /// <response code="200">User widgets have been updated</response>
    /// <response code="400">The user could not be update and the reasons are returned</response>
    /// <response code="401">The user is not authorized to update the resources</response>
    /// <response code="404">No User with the given ID in the JWT claims was found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet("widgets")]
    public async Task<object?> GetWidgetsAsync(CancellationToken token)
    {
        var userId = _userManager.GetUserId(HttpContext.User);
        var user = await _userManager.FindByIdAsync(userId);
        return user == null ? NotFoundResponse() : user.DashboardWidgets;
    }
}
