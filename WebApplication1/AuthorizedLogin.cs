using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.MainSystem;

namespace WebApplication1
{
    public class AuthorizedLogin : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filtercontext)
        {
            
        }
        public override void OnActionExecuting(ActionExecutingContext filtercontext)
        {
            base.OnActionExecuting(filtercontext);
            if(filtercontext.HttpContext.Session.GetString("username")== null)
            {
                filtercontext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            Controller = "Account",
                            Action = "Login",
                            returnUrl = filtercontext.HttpContext.Request.Path.ToUriComponent()
                        }));
                return;
            }
        }
        
    }
}
