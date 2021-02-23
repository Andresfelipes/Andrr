using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleCrypto;
using Proyecto.Models;
using System.Web.Security;
using Proyecto.Tags;
using System.Net.Mail;
using System.Net;

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





        public ActionResult Login([Bind(Include = "email ,contraseña,recordarme")] Tb_Usuarios Persona)
        {




            if (HttpContext.User.Identity.IsAuthenticated == true)
            {

                return RedirectToAction("About", "Home");
            }
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();



            var usuarioinactivo = bd.Tb_Usuarios.Where(item => item.Identificacion == Persona.Identificacion & Persona.estado == "Inactivo").FirstOrDefault();

            if (Persona.Email != null)
            {

                var usuariodb = bd.Tb_Usuarios.Where(item => item.Email == Persona.Email).FirstOrDefault();
                if (usuariodb != null)
                {
                   
                    if (usuarioinactivo ==  null)
                    {



                        //ICryptoService cryptoService = new PBKDF2();
                        //var salt = cryptoService.GenerateSalt();
                        //var contraseniaEncriptada = cryptoService.Compute(Persona.Contraseña, salt);
                        //Persona.Contraseña = contraseniaEncriptada;

                        if (Persona.Contraseña == usuariodb.Contraseña)
                        {

                            FormsAuthentication.SetAuthCookie(usuariodb.Email, Persona.Recordarme);

                            return RedirectToAction("About", "Home");
                        }
                        else
                        {
                            ViewBag.logmessaje = "Contraseña incorrecta";

                        }

                    }
                    else
                    {
                        ViewBag.inactivo = "Este usuario no tiene permisos para ingresar al aplicativo";

                    }
                }
                else
                {
                    ViewBag.emailError = "El usuario no existe";

                }
            }




            return View(Persona);
        }


        public ActionResult RecuperarContrasenia(Tb_Usuarios usuario)
        {

            if (usuario.Email != null)
            {

                var usucariobd = bd.Tb_Usuarios.Where(item => item.Email == usuario.Email).FirstOrDefault();
                if (usucariobd != null)
                {

                    string emailSalida = "grupo.admis4@gmail.com";
                    string contraseniaSalida = "builybruja";
                    string emailDestino = usucariobd.Email;

                    string password = RandomPassword.Generate(10, PasswordGroup.Uppercase, PasswordGroup.Lowercase, PasswordGroup.Numeric, PasswordGroup.Special);

                    bd.Entry(usucariobd).State = System.Data.Entity.EntityState.Modified;


                    usucariobd.Contraseña = password;

                    try
                    {
                        bd.SaveChanges();
                        EmailEnriquecido(emailSalida, contraseniaSalida, emailDestino, password);
                        return RedirectToAction("Login");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);


                    }


                }
                ViewBag.messaje = "El usuario no existe";

            }






            return View(usuario);
        }



        public ActionResult LogOut()
        {


            System.Web.Security.FormsAuthentication.SignOut();
            Session["Email"] = null;
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("Login"); ;
        }

        public void EmailEnriquecido(string emailSalida, string contraseniaSalida, string emailDestino, string contraseniaNueva)
        {
            //    string asunto = "Recuperación de contraseña - ADMIS";

            //    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //    msg.To.Add(emailDestino);

            //    msg.From = new MailAddress(emailSalida, "ADMIS", System.Text.Encoding.UTF8);
            //    msg.Subject = asunto;
            //    msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //    msg.Body = string.Format("Por favor ingrese con esta contraseña: <b>{0}</b>", contraseniaNueva);
            //    msg.BodyEncoding = System.Text.Encoding.UTF8;
            //    msg.IsBodyHtml = true;
            //    msg.Priority = System.Net.Mail.MailPriority.High;

            //    SmtpClient client = new SmtpClient();
            //    client.Credentials = new System.Net.NetworkCredential(emailSalida, contraseniaSalida);

            //    //client.Port = 587;
            //    //client.Host = "smtp.gmail.com";
            //    //client.EnableSsl = true;
            //    client.Host = "smtp.gmail.com";
            //    client.Port = 465;//25 //587;
            //    client.EnableSsl = true;
            //    client.UseDefaultCredentials = false;

            //    try
            //    {
            //        client.Send(msg);
            //    }
            //    catch (System.Net.Mail.SmtpException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        Console.ReadLine();
            //    }




            MailMessage msg = new MailMessage();

            string asunto = "Recuperación de contraseña - ADMIS";
            msg.From = new MailAddress(emailSalida);
            msg.To.Add(emailDestino);
            msg.From = new MailAddress(emailSalida, "ADMIS", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = string.Format("Por favor ingrese con esta contraseña: <b>{0}</b>", contraseniaNueva);
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.High;


            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 25;//25 //587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(emailSalida, contraseniaSalida);
            client.Timeout = 20000;

            try
            {
                client.Send(msg);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                msg.Dispose();
            }
        }


        [Autenticado]
        public ActionResult contrasenianueva(Tb_Usuarios usuario)
        {





            if (usuario.Contraseña != null)
            {

                var log = HttpContext.User.Identity.Name;
                var usuariobd = bd.Tb_Usuarios.Where(item => item.Email == log).FirstOrDefault();

                if (usuariobd != null)
                {

                    usuariobd = bd.Tb_Usuarios.Where(item => item.Contraseña == usuario.Contraseña).FirstOrDefault();
                    if (usuariobd != null)
                    {

                        if (usuario.nuevacontrasenia == usuario.confirmarcontrasenia)
                        {


                            if (usuario.Contraseña != usuario.nuevacontrasenia)
                            {

                                bd.Entry(usuariobd).State = System.Data.Entity.EntityState.Modified;


                                usuariobd.Contraseña = usuario.nuevacontrasenia;

                                try
                                {

                                    bd.SaveChanges();
                                    ViewBag.correcto = "Contraseña modificada correctamente";
                                    //ViewData["Nombre"] = "Eduard Tomàs";
                                    System.Web.Security.FormsAuthentication.SignOut();
                                    ViewBag.messaje = RedirectToAction("Login", "Home");

                                }
                                catch (Exception e)
                                {
                                    throw;
                                    Console.Write(e);
                                    Console.ReadKey();
                                }






                            }
                            else
                            {

                                ViewBag.igual = "La contraseña nueva no puede ser igual a la anterior";

                            }







                        }
                        else
                        {


                            ViewBag.nocoinciden = "Las contraseñas no coinciden";
                        }



                    }
                    else
                    {

                        ViewBag.contramala = "La contraseña es incorrecta";

                    }


                }

            }









            return View();
        }




    }
}


