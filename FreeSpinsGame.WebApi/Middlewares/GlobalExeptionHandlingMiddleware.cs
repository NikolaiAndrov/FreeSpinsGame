using System.Net;

namespace FreeSpinsGame.WebApi.Middlewares
{
    public class GlobalExeptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExeptionHandlingMiddleware> logger;

        public GlobalExeptionHandlingMiddleware(ILogger<GlobalExeptionHandlingMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
