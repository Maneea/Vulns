using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vulns.Core;
using Vulns.Services;
using Vulns.Infrastructure;

namespace Vulns.Web;

[ApiController]
[Route("auth")]
[Produces("application/json")]
public class AuthenticationController : ApiControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly JwtService _jwtService;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper,
        JwtService jwtService)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    /// <summary>Create a new user</summary>
    /// <param name="registerDto">Username, password, and email are required. The phone number is optional</param>
    /// <response code="200">Creates the user and returns a JWT token along with its expiration date</response>
    /// <response code="400">The input in the request body is not valid.</response>
    /// <response code="406">The user has not been created due to the returned list of errors.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtToken))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorSet<string>))]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ErrorSet<AuthenticationError>))]
    public async Task<object> Register([FromBody] RegisterRequest registerDto)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var user = new User(registerDto.Username)
        {
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber
        };
        var creationResult = await _userManager.CreateAsync(user, registerDto.Password);
        if (!creationResult.Succeeded)
            return ErrorResponse<AuthenticationError>(_mapper.Map<IEnumerable<AuthenticationError>>(creationResult.Errors), StatusCodes.Status406NotAcceptable);

        var roleResult = await _userManager.AddToRoleAsync(user, Role.User.ToString());
        if (!roleResult.Succeeded)
            return ErrorResponse<AuthenticationError>(_mapper.Map<IEnumerable<AuthenticationError>>(creationResult.Errors), StatusCodes.Status406NotAcceptable);

        var claims = _jwtService.GenerateUserClaims(user, new System.Collections.Generic.List<string>() { Role.User.ToString() });
        var token = _jwtService.GetToken(claims);
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new JwtToken(encodedToken, token.ValidTo);
    }

    /// <summary>Authorize a user</summary>
    /// <param name="loginDto">The username-password pair are required</param>
    /// <response code="200">The user has been authorized and an access token has been issued.</response>
    /// <response code="400">The input in the request body is not valid.</response>
    /// <response code="401">The user has failed the authorization procdure.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtToken))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorSet<string>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorSet<string>))]
    [HttpPost("login")]
    public async Task<object> Login([FromBody] LoginRequest loginDto)
    {
        if (!ModelState.IsValid) return ValidationErrorResponse();
        var user = await _userManager.FindByNameAsync(loginDto.Username);

        if (user == null || !(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
            return ErrorResponse<string>("The provided username and/or password are incorrect", StatusCodes.Status401Unauthorized);

        var roles = await _userManager.GetRolesAsync(user);
        var claims = _jwtService.GenerateUserClaims(user, roles);
        var token = _jwtService.GetToken(claims);
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new JwtToken(encodedToken, token.ValidTo);
    }
}
