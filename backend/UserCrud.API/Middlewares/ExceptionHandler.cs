namespace UserCrud.API.Middlewares
{
    using Newtonsoft.Json;
    using System.Net;
    using UserCrud.Domain.Exceptions;
    
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCode(exception);
            var errorMessage = exception.InnerException?.Message ?? exception.Message;
            var jsonMessage = JsonConvert.SerializeObject(errorMessage, Formatting.Indented);
            await context.Response.WriteAsync(jsonMessage);
        }

        public HttpStatusCode GetStatusCode(Exception exception)
        {
            var internalException = exception as BaseException;
            if (internalException == null)
            {
                return HttpStatusCode.InternalServerError;
            }
            return internalException.StatusCode;
        }
    }
}
