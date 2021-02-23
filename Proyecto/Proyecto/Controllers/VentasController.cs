using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Proyecto.Tags;
using System.Web.Security;

namespace Proyecto.Controllers
{
    [Autenticado]
    
    public class VentasController : Controller
    {        
        private ADMISEntities2 db = new ADMISEntities2();

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Tb_ventas_iva = db.Cargar_Iva();
            var name = HttpContext.User.Identity.Name;
            var sucursal = db.Llamar_Sucursal(name);
            var listP = db.llenarp(name).ToList();
            listP.Add(new llenarp_Result { Codigo_producto = "0", Descripcion = "{Selecccione Producto...}" });
            listP = listP.OrderBy(c => c.Descripcion).ToList();
            ViewBag.Producto = new SelectList(listP, "Codigo_producto", "Descripcion");
            var listC = db.Tb_Clientes.ToList();
            listC.Add(new Tb_Clientes { Identificacion = 0, Nombre1 = "{Selecccione Cliente...}" });
            listC = listC.OrderBy(c => c.Nombre1).ToList();
            ViewBag.Cliente = new SelectList(listC, "Identificacion", "Nombre1");
            var listS = db.Tb_Sucursales.ToList();
            listS.Add(new Tb_Sucursales { Codigo = "0", Nombre = "{Selecccione Sucursal...}" });
            listS = listS.OrderBy(c => c.Nombre).ToList();
            ViewBag.Sucursal = new SelectList(listS, "Codigo", "Nombre");
            return View();
        }
        [HttpPost]
        public JsonResult Create(List<Tb_DetalleVenta> detalle, string fecha, string sucursal, string cliente, string iva, string total)
        {

            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult Precio(string id, string suc)
        {
            int idsuc = Convert.ToInt16(suc);
            var precio = db.Precio(id, idsuc);
            return Json(precio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Dian(string id)
        {
            int ids = Convert.ToInt16(id);
            var dian = db.Cargar_Dian(ids);
            return Json(dian, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Productos()
        {
            
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}