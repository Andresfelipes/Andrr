using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Proyecto.Tags
{
 
        public class AutenticadoAttribute : ActionFilterAttribute{

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            bool estaAutenticado = HttpContext.Current.User.Identity.IsAuthenticated;

            if (!estaAutenticado) {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    Controller = "Home",

                    Action = "Login"
                }));
            }
        }



    }

    public class NoLoginAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);


            var estaAutenticado = HttpContext.Current.User.Identity.IsAuthenticated;

            if (estaAutenticado ==true) {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    Controller = "Home",
                    Action = "Index"
                }));
            }
            
        }


    }
    
}