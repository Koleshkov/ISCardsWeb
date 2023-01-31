using FluentValidation;
using ISCardsWeb.Aplication.Common.Exceptions;
using ISCardsWeb.Application.Common.Exceptions;
using ISCardsWeb.Shared.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace ISCardsWeb.Server.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next=next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (ex)
            {
                case Aplication.Common.Exceptions.ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result=JsonSerializer.Serialize(new BaseResponse { Errors = validationException.Errors});
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result=JsonSerializer.Serialize(new BaseResponse
                    {
                        Errors = new List<string> { notFoundException.Message }
                    });
                    break;
                case AlreadyExistException alreadyExistException:
                    code = HttpStatusCode.AlreadyReported;
                    result=JsonSerializer.Serialize(new BaseResponse
                    {
                        Errors = new List<string> { alreadyExistException.Message }
                    });
                    break;
                case Exception exception:
                    code = HttpStatusCode.BadRequest;
                    result=JsonSerializer.Serialize(new BaseResponse
                    {
                        Errors = new List<string> { exception.Message }
                    });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
