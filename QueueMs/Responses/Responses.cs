using Microsoft.AspNetCore.Mvc;

namespace QueueMs.Responses;

public static class ApiResponses
{
    public static IActionResult Ok(string message, object? data = null)
        => new ApiResponse(200, message, data);
    public static IActionResult BadRequest(string message, object? data = null)
        => new ApiResponse(400, message, data);
    public static IActionResult NotFound(string message, object? data = null)
        => new ApiResponse(404, message, data);
}
