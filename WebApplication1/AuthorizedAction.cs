using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.MainSystem;

namespace WebApplication1
{
    public class AuthorizedAction : ActionFilterAttribute
    {
        
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.Session.GetString("username") == null)
            //if(filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {
                
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            Controller = "Account",
                            Action = "Login",
                            returnUrl = filterContext.HttpContext.Request.Path.ToUriComponent()
                        })) ;
                return;
            }

            var menus = JsonConvert.DeserializeObject<List<Menus>>(filterContext.HttpContext.Session.GetString("menus"));
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];
            string url = "/" + controllerName + "/" + actionName;

            if (!menus.Where(s => s.Url == url).Any())

            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
                return;
            }
        }
    }
}
