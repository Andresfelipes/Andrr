using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class EntradasController : Controller
    {

        public ADMISEntities2 db = new ADMISEntities2();

        // GET: Entradas
        public ActionResult Index()
        {
            ViewBag.dato = db.Consultar_Entrada().ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Estado = "Activo";
            var name = HttpContext.User.Identity.Name;
            var sucur = db.Llamar_Sucursal(name).ToList();
            var listPro = db.llenap_entrada(sucur[0]).ToList();
            ViewBag.dato = listPro;

            var listS = db.Tb_Configuraciones.ToList();

            var lis = db.Tb_Sucursales.ToList();


            ViewBag.Sucursal = new SelectList(lis, "Codigo", "Apodo", selectedValue: listS[0].Sucursal_pricipal);

            var listP = db.Tb_Proveedores.ToList();
            ViewBag.Prove = listP;

            return View();
        }

        public JsonResult Consultar(string elemento)
        {
            int id = Convert.ToInt16(elemento);
            List<Consultar_Detalle_Entrada_Result> detals = db.Consultar_Detalle_Entrada(id).ToList();
            return Json(detals, JsonRequestBehavior.AllowGet);
        }

        public JsonResult crear(List<Tb_Detalle_Entrada> detalle, string entrada, string fecha, string total, string estado, string sucursal, string proveedor, string producto, string cantidad, string precio)
        {


            var prov = Convert.ToInt32(proveedor);
            var Tot = Convert.ToDouble(total);
            var entra = Convert.ToInt32(entrada);
            var cant = Convert.ToInt16(cantidad);
            var pre = Convert.ToDouble(precio);

            db.RegistrarEntrada(entra, System.DateTime.Now, Tot, estado, sucursal, prov);
            for (int i = 0; i < detalle.Count; i++)
            {
                db.RegistrarDetalleEntrada(detalle[i].Cantidad, detalle[i].Precio, entra, detalle[i].ProductoSucursal);
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

    }
}