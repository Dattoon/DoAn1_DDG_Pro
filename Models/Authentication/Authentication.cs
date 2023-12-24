using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DoAn1_DDG_Pro.Models.Authentication
{
    public class Authentication:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
           if(context.HttpContext.Session.GetString("Username")==null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller","Access"},
                        {"Action","Login" }
                    });
            }
        }
    }
}
