namespace PawTrack.WebApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMaintenanceModel(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintanenceMiddleware>();
        }

        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
