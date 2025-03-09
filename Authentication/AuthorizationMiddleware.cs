using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

public class AuthorizationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            // Call next middleware
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        // ErrorDetails error = null;
        // if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
        // {
        //     context.Response.StatusCode = StatusCodes.Status404NotFound;
        //     error = _localizer.FilesOrFoldersNotFound();
        // }
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("3245");

            // await context.Response.WriteAsync(JsonConvert.SerializeObject(
            // new CustomResponse(false, error ?? _localizer.DefaultError()),
            // _serializerSettings));
        }
}

