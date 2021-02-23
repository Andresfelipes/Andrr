using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Proyecto.Tags;
using Proyecto.Permisos;
namespace Proyecto.Controllers
{
     [Autenticado]
    [AuthPermisosAtribute]
    public class BajasController : Controller
    {
        public ADMISEntities2 db = new ADMISEntities2();
        // GET: Bajas
        public ActionResult Index()
        {
            ViewBag.consultar = db.Consultar_Baja().ToList(); 
            return View();
        }

        [HttpGet]
        public ActionResult Create() {

            var name = HttpContext.User.Identity.Name;
            var listP = db.llenarp(name).ToList();
            var sucur = db.Llamar_Sucursal(name).ToList();
            var listPro = db.llenap_entrada(sucur[0]).ToList();
            ViewBag.dato = listPro;
            var listS = db.Tb_Sucursales.ToList();
            ViewBag.Sucursal = new SelectList(listS, "Codigo", "Apodo", selectedValue: sucur[0]);

            return View();
        }

        [HttpPost]
        public JsonResult Create(List<Tb_Detalle_Bajas> detalle, string baja,string fecha,string sucursal,string motivo) {

            var resp = 0;
            int baja1 = Convert.ToInt32(baja);


            var usuariobd = db.Tb_bajas.Where(item => item.Codigo_baja == baja1).FirstOrDefault();

            if (usuariobd==null) {

                db.Registrar_baja(baja1, sucursal, System.DateTime.Now);
                for (int i = 0; i < detalle.Count; i++)
                {
                    db.Registrar_Detalle_baja(baja1, detalle[i].Motivo, detalle[i].Producto, detalle[i].Cantidad);
                    resp = 1;
                }
            }
            else
            {
                ViewBag.repetido = "Ya Existe una baja con este codigo";
                resp = 2;
            }


            return Json(resp,JsonRequestBehavior.AllowGet);
        }

        public ActionResult consultar() {

           

            return View();

        }

        public JsonResult detalles(string elemento) {

            int id = Convert.ToInt32(elemento);
            db.conusltar_detalle_baja(id);
            List<conusltar_detalle_baja_Result> detals = db.conusltar_detalle_baja(id).ToList();


            return Json(detals,JsonRequestBehavior.AllowGet);
        }

        public JsonResult cantidad(string id, string suc, string cantidad) {

            int idsuc = Convert.ToInt16(suc);
            int cant = Convert.ToInt32(cantidad);
            var Canti = db.Cantidad_Detalle(id, cant, idsuc).ToList();
            var can = Canti[0];


            return Json(can,JsonRequestBehavior.AllowGet);
        }

    }
}