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

namespace Proyecto.Tags
{

    //public class TienePermisoAttribute : ActionFilterAttribute
    //{
       
    //    public override void OnResultExecuted(ResultExecutedContext filterContext)
    //    {
    //        ADMISEntities2 bd = new ADMISEntities2();

    //        base.OnResultExecuted(filterContext);


    //        bool estaAutenticado = HttpContext.Current.User.Identity.IsAuthenticated;

    //        if (estaAutenticado)
    //        {

    //            var log = HttpContext.Current.User.Identity.Name;

    //            var id = bd.sacarid(log).ToList();

    //            string url = HttpContext.Current.Request.CurrentExecutionFilePath;

    //            int iden = Convert.ToInt32(id[0]);


    //            var permiso = bd.tienepermiso(url, iden).ToList();



    //            if ((permiso[0] == 1))
    //            {
    //                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
    //                {
    //                    Controller = "Tb_Usuarios",

    //                    Action = "Index"
    //                }));

    //            }
    //        }
    //    }

    //}

    public class NoLoginAttribute : ActionFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);


            var estaAutenticado = HttpContext.Current.User.Identity.IsAuthenticated;

            if (estaAutenticado == true)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    Controller = "Tb_Usuarios",

                    Action = "Index"
                }));
            }

        }


    }

    
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        ADMISEntities2 bd = new ADMISEntities2();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            bool estaAutenticado = HttpContext.Current.User.Identity.IsAuthenticated;

            if (!estaAutenticado)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    Controller = "Home",

                    Action = "Login"
                }));
            }
        }

     
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
           
            base.OnResultExecuted(filterContext);

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var log = HttpContext.Current.User.Identity.Name;

                var id = bd.sacarid(log).ToList();

                string ur = HttpContext.Current.Request.Url.AbsoluteUri;

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
                            Controller = "Home",

                            Action = "About"
                        }));
                }

            }


        }
    }
}