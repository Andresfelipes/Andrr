using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleCrypto;
using System.Reflection;
using Proyecto.Models;
using System.Web.Security;
using Proyecto.Tags;

namespace Proyecto.Controllers

{
 
    public class HomeController : Controller
    {
        ADMISEntities2 bd = new ADMISEntities2();

        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      

        [NoLogin]
       
        public ActionResult Login([Bind(Include ="email ,contraseña,recordarme")] Tb_Usuarios Persona)
        {

           


            if (HttpContext.User.Identity.IsAuthenticated == true) {
                
                return RedirectToAction("Index", "Tb_Clientes");
            }
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();

            if ((ModelState.IsValidField("email") && ModelState.IsValidField("contraseña") && ModelState.IsValidField("recordarme")) )

            {
              

                var usuariodb = bd.Tb_Usuarios.Where(item => item.Email == Persona.Email && item.Contraseña == Persona.Contraseña).FirstOrDefault();
                    
                if (usuariodb != null)
                {
                    //ICryptoService cryptoService = new PBKDF2();
                    //var salt = cryptoService.GenerateSalt();
                    //var contraseniaEncriptada = cryptoService.Compute(Persona.Contraseña, salt);
                    //Persona.Contraseña = contraseniaEncriptada;

                    if (Persona.Contraseña == usuariodb.Contraseña  & Persona.Email ==usuariodb.Email)
                    {

                        FormsAuthentication.SetAuthCookie(usuariodb.Email,false);

                        return RedirectToAction("Index","Tb_Clientes");
                    }
                    else {
                        ModelState.AddModelError("", "El usuario o contraseña no coinciden");
                    }

                }
                
            }
            
            return View(Persona);
        }


        [Autenticado]
        public ActionResult LogOut() {


            System.Web.Security.FormsAuthentication.SignOut();
            Session["Email"] = null;
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("Login"); ;
        }
    }
    
}