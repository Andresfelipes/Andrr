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

namespace Proyecto.Controllers
{
    [Autenticado]
    public class Tb_Detalle_Producto_SucursalController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Detalle_Producto_Sucursal
        public ActionResult Index()
        {
            var tb_Detalle_Producto_Sucursal = db.Tb_Detalle_Producto_Sucursal.Include(t => t.Tb_Productos).Include(t => t.Tb_Sucursales);
            return View(tb_Detalle_Producto_Sucursal.ToList());
        }

        // GET: Tb_Detalle_Producto_Sucursal/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Detalle_Producto_Sucursal tb_Detalle_Producto_Sucursal = db.Tb_Detalle_Producto_Sucursal.Find(id);
            if (tb_Detalle_Producto_Sucursal == null)
            {
                return HttpNotFound();
            }
            return View(tb_Detalle_Producto_Sucursal);
        }

        // GET: Tb_Detalle_Producto_Sucursal/Create
        public ActionResult Create()
        {
            var listp = db.Tb_Productos.ToList();
            
            listp.Add(new Tb_Productos{ Codigo_producto = "0", Descripcion = "{Seleccione Producto...}" });
            listp = listp.OrderBy(c => c.Referencia).ToList();
            ViewBag.Producto = new SelectList(listp, "Codigo_producto", "Descripcion");


            var listS = db.Tb_Sucursales.ToList();
            
            listS.Add(new Tb_Sucursales { Codigo = "0", Apodo = "{Seleccione Sucursal...}" });
            listS = listS.OrderBy(c => c.Apodo).ToList();
            ViewBag.Sucursal = new SelectList(listS, "Codigo", "Apodo");

            return View();
        }

        // POST: Tb_Detalle_Producto_Sucursal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Tb_Detalle_Producto_Sucursal detalle)
        {
            if (ModelState.IsValid)
            {
                db.Registrar_detalle_producto_sucursal(detalle.Stock_Minimo,detalle.Stock_Maximo,detalle.Valor_Venta,detalle.Valor_Mayor,detalle.Valor_Especial,detalle.Cantidad,detalle.Producto,detalle.Sucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Producto = new SelectList(db.Tb_Productos, "Codigo_producto", "Referencia",detalle.Producto);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", detalle.Sucursal);
            return View(detalle);
        }

        // GET: Tb_Detalle_Producto_Sucursal/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Detalle_Producto_Sucursal tb_Detalle_Producto_Sucursal = db.Tb_Detalle_Producto_Sucursal.Find(id);
            if (tb_Detalle_Producto_Sucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.Producto = new SelectList(db.Tb_Productos, "Codigo_producto", "Referencia", tb_Detalle_Producto_Sucursal.Producto);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Detalle_Producto_Sucursal.Sucursal);
            return View(tb_Detalle_Producto_Sucursal);
        }

        // POST: Tb_Detalle_Producto_Sucursal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo_detalle,Stock_Minimo,Stock_Maximo,Valor_Venta,Valor_Mayor,Valor_Especial,Cantidad,Producto,Sucursal")] Tb_Detalle_Producto_Sucursal tb_Detalle)
        {
            if (ModelState.IsValid)
            {
                db.Actualiza_producto_Sucursal(tb_Detalle.Sucursal,tb_Detalle.Stock_Minimo, tb_Detalle.Stock_Minimo, tb_Detalle.Valor_Venta, tb_Detalle.Valor_Mayor, tb_Detalle.Valor_Especial, tb_Detalle.Cantidad, tb_Detalle.Producto);
                return RedirectToAction("Index");
            }
            ViewBag.Producto = new SelectList(db.Tb_Productos, "Codigo_producto", "Referencia", tb_Detalle);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Detalle);
            return View(tb_Detalle);
        }

        // GET: Tb_Detalle_Producto_Sucursal/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Detalle_Producto_Sucursal tb_Detalle_Producto_Sucursal = db.Tb_Detalle_Producto_Sucursal.Find(id);
            if (tb_Detalle_Producto_Sucursal == null)
            {
                return HttpNotFound();
            }
            return View(tb_Detalle_Producto_Sucursal);
        }

        // POST: Tb_Detalle_Producto_Sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tb_Detalle_Producto_Sucursal tb_Detalle_Producto_Sucursal = db.Tb_Detalle_Producto_Sucursal.Find(id);
            db.Tb_Detalle_Producto_Sucursal.Remove(tb_Detalle_Producto_Sucursal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult Consul(string Id)
        {
            int? select2 = 0;
            if (ModelState.IsValid)
            {
                select2 = (from e in db.Tb_Productos
                           where e.Codigo_producto == Id
                           select e.Precio).Single();
            }
            return Json(select2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cantidad(string cant)
        {
            int? select2 = 0;
            if (ModelState.IsValid)
            {
                select2 = (from e in db.Tb_Productos
                           where e.Codigo_producto == cant
                           select e.cantidad).Single();
            }
            return Json(select2, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
