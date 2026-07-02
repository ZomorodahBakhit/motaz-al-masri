using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using University.Core.Exceptions;

namespace University.Api.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = Response(context.Exception.Message, "Item not found", StatusCodes.Status404NotFound);
                return;
            }

            if (context.Exception is BusinessException businessException)
            {
                if (businessException.Errors != null && businessException.Errors.Any())
                {
                    context.Result = Response(businessException.Errors, "Validation Errors", StatusCodes.Status400BadRequest);
                }
                else
                {
                    context.Result = Response(businessException.Message, "Business Error", StatusCodes.Status400BadRequest);
                }
                return;
            }

            _logger.LogError(context.Exception, "Unhandled exception occurred");
            context.Result = Response(context.Exception.Message, "Internal Server Error", StatusCodes.Status500InternalServerError, context.Exception.StackTrace);
        }

        private ObjectResult Response(string message, string title, int status, string? stackTrace = null)
        {
            var result = new ApiResponse
            {
                StatusCode = status,
                Message = message,
                ResponseException = title,
                IsError = true,
                Version = "1.0",
                Result = stackTrace
            };
            return new ObjectResult(result) { StatusCode = status };
        }

        private ObjectResult Response(Dictionary<string, List<string>> errors, string title, int status)
        {
            var result = new ApiResponse
            {
                StatusCode = status,
                Message = title,
                ResponseException = title,
                IsError = true,
                Version = "1.0",
                Result = errors
            };
            return new ObjectResult(result) { StatusCode = status };
        }
    }
}