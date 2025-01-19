using PawTrack.Business.Operations.Setting;

namespace PawTrack.WebApi.Middlewares
{
    public class MaintanenceMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
       

        public MaintanenceMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
            
        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenanceMode = settingService.GetMaintenanceState();

            if (context.Request.Path.StartsWithSegments("/api/auth/login") || context.Request.Path.StartsWithSegments("/api/settings"))
            {
                await _requestDelegate(context);
                return;
            }

            if (maintenanceMode)
            {
                await context.Response.WriteAsync("Su anda bakim durumunda hizmet veremiyoruz");
            }
            else
            {
                await _requestDelegate(context);
            }
        }
    }
}
