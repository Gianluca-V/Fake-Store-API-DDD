using Domain.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Api.Common.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext Context, RequestDelegate next)
        {
            try
            {
                await next(Context);
            }
            catch (NotFoundException e)
            {
                string title = "Resource not found";
                await HandleException(Context, (int)HttpStatusCode.NotFound, title, e);
            }
            catch (BadRequestException e)
            {
                string title = "Invalid request field";
                await HandleException(Context, (int)HttpStatusCode.BadRequest, title, e);
            }
            catch (ValidationException e)
            {
                string title = "Invalid data format";
                await HandleException(Context, (int)HttpStatusCode.BadRequest, title, e);
            }
            catch (AlreadyExistException e)
            {
                string title = "Resource already exists";
                await HandleException(Context, (int)HttpStatusCode.Conflict, title, e);
            }
            catch (LoginException e)
            {
                string title = "Login error";
                await HandleException(Context, (int)HttpStatusCode.Unauthorized, title, e);
            }
        }

        private static Task HandleException(HttpContext Context, int httpStatusCode, string title, Exception e)
        {
            var problemDetails = new ProblemDetails
            {
                Status = httpStatusCode,
                Title = title,
                Detail = e.Message,
            };
            Context.Response.ContentType = "application/problem+json";
            Context.Response.StatusCode = (int)problemDetails.Status;
            return Context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}
