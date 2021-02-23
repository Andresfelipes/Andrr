using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Proyecto.Models;
using System.Text.RegularExpressions;
using Proyecto.Tags;

namespace Proyecto.Permisos
{
    [Autenticado]
    public class AuthPermisosAtribute : ActionFilterAttribute
    {
        ADMISEntities2 bd = new ADMISEntities2();


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var log = HttpContext.Current.User.Identity.Name;

                var id = bd.sacarid(log).ToList();

                string url = HttpContext.Current.Request.CurrentExecutionFilePath;

                string parent = @"/";
                string[] elementos = Regex.Split(url, parent);
                string Accion = "";
                string controller = elementos[1];

                if (elementos.Length == 3)
                {

                    Accion = elementos[2];
                }
                else if (elementos.Length == 2)
                {

                    Accion = "Index";
                }



                int iden = Convert.ToInt32(id[0]);


                var permiso = bd.tienepermiso(controller, Accion, iden).ToList();

                if ((permiso[0] == 1))
                {

                    filterContext.Result = new RedirectToRouteResult(

                     new RouteValueDictionary(new
                     {
                         controller = "Home",
                         action = "About"
                     }
                     ));

                }
            }

        }

    }
}