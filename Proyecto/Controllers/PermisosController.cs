using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using System.Diagnostics;

namespace Proyecto.Controllers
{
    public class PermisosController : Controller
    {
        ADMISEntities2 bd = new ADMISEntities2();
        private int elem;

        // GET: Permisos


        public ActionResult Index()
        {

            ViewBag.dato = bd.Consultar_permiso().ToList();
            return View();
        }


        public ActionResult Create()
        {
            ViewBag.dato = bd.sacarRoles().ToList();
            ViewBag.dato1 = bd.sacarModulos().ToList();
            ViewBag.dato2 = bd.SacarAcciones().ToList();

            return View();
        }
        [HttpPost]
        public JsonResult Create(List<Detalle_permiso> detalle, string id)
        {

            List<string> res = new List<string> { "Mensaje", "valor" };


            var iden = Convert.ToInt32(id);
            var log = HttpContext.User.Identity.Name;

            var identificacion = bd.sacarid(log).ToList();
            var usuario = Convert.ToInt32(identificacion[0]);

            var usuariobd = bd.Tb_Permiso_Denegado_Roles.Where(item => item.id == iden).FirstOrDefault();

            if (usuariobd == null)
            {

                bd.registrarPermisoDEnegado(iden);



                for (int i = 0; i < detalle.Count; i++)
                {

                    var mod = detalle[i].Modulo;
                    var mudul = Convert.ToInt32(mod);
                    var accions = Convert.ToInt32(detalle[i].Accion);
                    var rol = Convert.ToInt32(detalle[i].Rol);
                    var selec = (from e in bd.Modulos
                                 where e.id == mudul
                                 select e.Modulo).Single();

                    var select1 = (from e in bd.Acciones
                                   where e.id == accions
                                   select e.id).Single();

                    var select2 = (from e in bd.Tb_Rol_Usuarios
                                   where e.Codigo == rol
                                   select e.Codigo).Single();


                    var mostrar1 = (from e in bd.Tb_Rol_Usuarios
                                    where e.Codigo == select2
                                    select e.Nombre).Single();

                    var mostrar = (from e in bd.Tb_Rol_Usuarios
                                   where e.Codigo == select2
                                   select e.Nombre).Single();

                    var selectacc = (from e in bd.Acciones
                                     where e.id == accions
                                     select e.Accion).Single();


                    var selecmodu = (from e in bd.Modulos
                                     where e.id == mudul
                                     select e.Modulo).Single();

                    var accionexist = bd.Detalle_permiso.Where(item => item.Accion == accions & item.Modulo == selecmodu & item.Rol == select2).FirstOrDefault();



                    if (accionexist == null)
                    {

                        bd.RegistrarPermiso(detalle[i].Rol, Convert.ToInt32(detalle[i].Modulo), detalle[i].Accion, iden);
                        res[0] = "1";
                    }
                    else
                    {

                        res[1] = "EL Permiso" + "  " + mostrar.ToString() + "-  " + (selec).ToString() + "  -  " + (selectacc).ToString() + "  " + "Ya se encuentra Registrado";
                        bd.estaRegistrado_delete(iden);

                        res[0] = "2";
                    }
                }

            }
            else
            {
                res[0] = "3";
                ViewBag.repetido = "Ya existe un Permiso Denegado con este codigo";

            }


            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CONSULTAR(string elemento)
        {


            int id = Convert.ToInt32(elemento);
            var consulto = bd.consultar_detalle_permiso1(id).ToList();

            List<consultar_detalle_permiso1_Result> detals = bd.consultar_detalle_permiso1(id).ToList();

            return Json(detals, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string elemento1)
        {
            List<string> elem = new List<string> { "" };


            try
            {



                var iden = Convert.ToInt32(elemento1);

                var esta = bd.elminar_permisos(iden).ToList();

                elem[0] = esta.ToString();
                ViewBag.corrrecto = "Se registro correctamente";


            }
            catch
            {

                elem[0] = "1";
            }
            return Json(elem, JsonRequestBehavior.AllowGet);
        }
    }


}
