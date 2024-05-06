using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TripAdvisor.Models;
using TripAdvisor.Controllers;

namespace TripAdvisor.Filters
{
    public class OnlyLoggedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ISession session = context.HttpContext.Session;
            string uid = session.GetString("uid");
            if (uid != null)
            {
                return;
            }
            else
            {
                context.Result = new BadRequestObjectResult("Devi aver fatto il login per farlo!");
            }
        }
    }
}
