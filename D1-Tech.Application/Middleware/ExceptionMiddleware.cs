using D1_Tech.Core.Dtos.Constants;
using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Models.CommonEntity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;

namespace D1_Tech.Application.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        public ExceptionMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }

        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = exception.Message;
            if (exception.GetType() == typeof(ValidationException))
            {
                message = exception.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception.GetType() == typeof(ApplicationException))
            {
                message = exception.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception.GetType() == typeof(UnauthorizedAccessException))
            {
                message = exception.Message;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else if (exception.GetType() == typeof(SecurityException))
            {
                message = exception.Message;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            
            else if (exception.GetType() == typeof(InvalidOperationException))
            {
                if (exception.Message.Contains("Unable to resolve service for type"))
                    message = $"Dependency Injection Reference Hatası = {exception.Message}";
                else if (exception.Message.StartsWith("Cannot create a DbSet for") &&
                         exception.Message.EndsWith("because this type is not included in the model for the context.")) ;
            }

            else
            {
                while (exception.InnerException != null)
                {
                    message += exception.InnerException;
                    exception = exception.InnerException;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            var response = new GenericResponseDto<NoContent>()
            {
                Data = new NoContent(),
                Error = $" {ErrorMessages.CreateError}  {message}",
                StatusCode = (int)ErrorEnums.Fail
            };
            httpContext.Response.ContentType = "application/json; charset=utf-8";
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
