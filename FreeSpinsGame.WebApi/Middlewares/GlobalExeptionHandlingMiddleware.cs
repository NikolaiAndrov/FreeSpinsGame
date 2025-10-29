using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using static FreeSpinsGame.Common.ExceptionMessages;
using static FreeSpinsGame.Common.GeneralApplicationMessages;

namespace FreeSpinsGame.WebApi.Middlewares
{
    public class GlobalExeptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExeptionHandlingMiddleware> logger;
        private readonly IHostEnvironment environment;

        public GlobalExeptionHandlingMiddleware(ILogger<GlobalExeptionHandlingMiddleware> logger, IHostEnvironment environment)
        {
            this.logger = logger;
            this.environment = environment;
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

                int statusCode = this.GetStatusCode(ex);

                context.Response.StatusCode = statusCode;

                ProblemDetails problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Type = this.GetMessage(ex),
                    Title = this.GetMessage(ex),
                    Detail = this.environment.IsDevelopment() ? ex.Message : UnexpectedErrorMessage
                };

                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }

        private int GetStatusCode(Exception ex)
        {
            return ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                DbUpdateConcurrencyException => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }

        private string GetMessage(Exception ex)
        {
            return ex switch
            {
                ArgumentException => BadRequestMessage,
                KeyNotFoundException => NotFoundMessage,
                UnauthorizedAccessException => UnauthorizedMessage,
                DbUpdateConcurrencyException => DbUpdateConcurrencyMessage,
                _ => InternalServerErrorMessage
            };
        }
    }
}
