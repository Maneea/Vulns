using Microsoft.AspNetCore.Mvc;
namespace Vulns.Web;

public class ApiControllerBase : ControllerBase
{
    [NonAction]
    protected ErrorSet<T> ErrorResponse<T>(IEnumerable<T> errors, int errorStatus = 400) where T : class
    {
        Response.StatusCode = errorStatus;
        return new ErrorSet<T>(errors);
    }

    [NonAction]
    protected ErrorSet<T> ErrorResponse<T>(T errorDetails, int errorStatus = 400) where T : class
    {
        Response.StatusCode = errorStatus;
        return new ErrorSet<T>(errorDetails);
    }

    [NonAction]
    protected ErrorSet<string> ValidationErrorResponse()
        => ErrorResponse<string>(ModelState.Values.SelectMany(v => v.Errors.Select(err => err.ErrorMessage)), StatusCodes.Status400BadRequest);

    [NonAction]
    protected Error NotFoundResponse(string? message = null) 
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return new(string.IsNullOrEmpty(message) ? DtoConstants.NotFound : message);
    }

    [NonAction]
    protected Error NotAcceptableResponse(string? message = null) 
    {
        Response.StatusCode = StatusCodes.Status406NotAcceptable;
        return new(string.IsNullOrEmpty(message) ? DtoConstants.NotAcceptable : message);
    }
}