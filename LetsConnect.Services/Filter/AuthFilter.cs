using LetsConnect.Data.Domains.Menu;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace LetsConnect.Services.Filter
{
    public class AuthFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var identity1 = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var name = identity1.Claims.Where(c => c.Type == ClaimTypes.Name)
                               .Select(c => c.Value).SingleOrDefault();
            if (string.IsNullOrEmpty(name))
            {
                filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {{ "Controller", "Account" },
                                      { "Action", "Login" } });
            }

           // var url = filterContext.HttpContext.Request.Url;
           // if (!string.IsNullOrEmpty(Convert.ToString(url)))
           // {
           //     var BaseUrl = Convert.ToString(url).Split('/');
           //     string TargetUrl = BaseUrl[BaseUrl.Length - 2] + "/" + BaseUrl[BaseUrl.Length - 1];
           //     string str_permission = new LetsConnect.Core.Generic.ValueCookies().GetCookies("CkPermission");
           //     List<MenuPermissions> PermissinList = JsonConvert.DeserializeObject<List<MenuPermissions>>(str_permission);
           //     if (!PermissinList.Any(i => i.pageUrl.Contains(TargetUrl)))
           //     {
           //         filterContext.Result = new RedirectToRouteResult(
           //new RouteValueDictionary {{ "Controller", "Account" },
           //                           { "Action", "Login" } });
           //     }
           // }

            base.OnActionExecuting(filterContext);
        }
    }
}
