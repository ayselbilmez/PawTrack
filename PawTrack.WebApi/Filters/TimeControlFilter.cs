using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PawTrack.WebApi.Filters
{
    public class TimeControlFilter : ActionFilterAttribute
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var now = DateTime.Now.TimeOfDay;

            StartTime = "18:00";
            EndTime = "20:00";

            if(now >= TimeSpan.Parse(StartTime) &&  now <= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Bu saatler arasinda calismamaktadir",
                    StatusCode = 403
                };
            }
        }
    }
}
