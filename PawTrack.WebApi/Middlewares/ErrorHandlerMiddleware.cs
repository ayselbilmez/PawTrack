using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PawTrack.WebApi.Middlewares.Responses;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace PawTrack.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
      

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = ApiResponse<string>.Fail(error.Message);

                switch (error)
                {
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Message = "Veri bulunamadi";
                        break;

                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        responseModel.Message = "Yetkisiz islem";
                        break;

                    case ValidationException:
                        response.StatusCode = (int)HttpStatusCode.UnprocessableContent;
                        responseModel.Message = "Beklenmeyen bir hata";
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = "SERVER_ERROR";
                        break ;

                
                }
                
                var result = JsonSerializer.Serialize(responseModel); ;
                await response.WriteAsync(result);
            }
        }


    }
}
