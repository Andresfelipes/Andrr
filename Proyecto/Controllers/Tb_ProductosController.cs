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
    public class Tb_ProductosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Productos
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                ViewBag.dato = db.Consultar_Productos("Activo");
            }
            else
            {
                ViewBag.dato = db.Consultar_Productos(id);
            }
            return View();
        }

        // GET: Tb_Productos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Productos tb_Productos = db.Tb_Productos.Find(id);
            if (tb_Productos == null)
            {
                return HttpNotFound();
            }
            return View(tb_Productos);
        }

        // GET: Tb_Productos/Create
        public ActionResult Create()
        {
            var listtp = db.Tb_Tipo_Producto.ToList();
            listtp.Add(new Tb_Tipo_Producto{ Codigo = "0", Nombre = "{Seleccione Tipo de Producto...}" });
            listtp = listtp.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Producto = new SelectList(listtp, "Codigo", "Nombre");

            var listR = db.Tb_Tipo_Producto.ToList();
            listR.Add(new Tb_Tipo_Producto { Codigo = "0", Nombre = "{Seleccione Tipo Producto...}" });
            listR = listR.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Producto = new SelectList(listR, "Codigo", "Nombre");
            return View();
        }

        // POST: Tb_Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tb_Productos productos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Registrar_Producto(productos.Referencia, productos.Descripcion, productos.Estado, productos.Tipo_Producto, productos.cantidad, productos.Precio);
                    ViewBag.res = "Correcto";
                }
                catch {
                    throw;

                }
            }

            ViewBag.Tipo_Producto = new SelectList(db.Tb_Tipo_Producto, "Codigo", "Nombre", productos.Tipo_Producto);
            return View(productos);
        }

        // GET: Tb_Productos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Productos tb_Productos = db.Tb_Productos.Find(id);
            if (tb_Productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo_Producto = new SelectList(db.Tb_Tipo_Producto, "Codigo", "Nombre", tb_Productos.Tipo_Producto);
            return View(tb_Productos);
        }

        // POST: Tb_Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Codigo_producto,Referencia,Descripcion,Estado,Tipo_Producto,cantidad,Precio")] Tb_Productos tb_Productos)
        {
            if (true)
            {
                db.Entry(tb_Productos).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.res = "Correcto";
            }
            ViewBag.Tipo_Producto = new SelectList(db.Tb_Tipo_Producto, "Codigo", "Nombre", tb_Productos.Tipo_Producto);
            return View();
        }

        public JsonResult Consul(string Id)
        {
            var select2 = "";
            if (ModelState.IsValid)
            {
                select2 = (from e in db.Tb_Productos
                           where e.Codigo_producto == Id
                           select e.Estado).Single();
            }
            return Json(select2, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string Id)
        {
            if (ModelState.IsValid)
            {
                db.cambiar_estado_producto(Id);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        //// GET: Tb_Productos/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStat busCode.BadRequest);
        //    }
        //    Tb_Productos tb_Productos = db.Tb_Productos.Find(id);
        //    if (tb_Productos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tb_Productos);
        //}

        //// POST: Tb_Productos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Tb_Productos tb_Productos = db.Tb_Productos.Find(id);
        //    db.Tb_Productos.Remove(tb_Productos);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
