using static Domain.Exceptions.CpfDuplicate;
using static Domain.Exceptions.EmailDuplicate;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EmailDuplicateException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            Console.WriteLine(ex.Message);
            await context.Response.WriteAsJsonAsync(new { erro = ex.Message });
        }
        catch (CpfDuplicateException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro interno");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { erro = "Erro interno no servidor." });
        }
    }
}
