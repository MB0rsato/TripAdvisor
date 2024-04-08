using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
namespace TripAdvisor.Filters
{
    public class OnlyAdminAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ISession session = context.HttpContext.Session;
            string utente = session.GetString("utente");
            if(utente == "admin")
            {
                return;
            }
            else
            {
                context.Result = new BadRequestObjectResult("Accesso consentito solo all'amministratore");
            }
        }
    }
}
