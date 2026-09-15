
using Microsoft.AspNetCore.Mvc;

namespace QueueMs.Responses;

public class ApiResponse : IActionResult
{
    private readonly int _statusCode;
    private readonly string _message;
    private readonly object? _data;

    public ApiResponse(int statusCode, string message, object? data = null)
    {
        _statusCode = statusCode;
        _message = message;
        _data = data;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = new
        {
            success = _statusCode >= 200 && _statusCode < 300,
            message = _message,
            data = _data
        };
        context.HttpContext.Response.StatusCode = _statusCode;
        await context.HttpContext.Response.WriteAsJsonAsync(response);
    }
}
