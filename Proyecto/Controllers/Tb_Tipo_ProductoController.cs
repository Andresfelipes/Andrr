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
    public class Tb_Tipo_ProductoController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Tipo_Producto
        public ActionResult Index()
        {
            return View(db.Tb_Tipo_Producto.ToList());
        }

        // GET: Tb_Tipo_Producto/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Tipo_Producto tb_Tipo_Producto = db.Tb_Tipo_Producto.Find(id);
            if (tb_Tipo_Producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_Tipo_Producto);
        }

        // GET: Tb_Tipo_Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Tipo_Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Tb_Tipo_Producto tb_Tipo_Producto)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Tipo_Producto.Add(tb_Tipo_Producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_Tipo_Producto);
        }

        // GET: Tb_Tipo_Producto/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Tipo_Producto tb_Tipo_Producto = db.Tb_Tipo_Producto.Find(id);
            if (tb_Tipo_Producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_Tipo_Producto);
        }

        // POST: Tb_Tipo_Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre")] Tb_Tipo_Producto tb_Tipo_Producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Tipo_Producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Tipo_Producto);
        }

        // GET: Tb_Tipo_Producto/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Tipo_Producto tb_Tipo_Producto = db.Tb_Tipo_Producto.Find(id);
            if (tb_Tipo_Producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_Tipo_Producto);
        }

        // POST: Tb_Tipo_Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tb_Tipo_Producto tb_Tipo_Producto = db.Tb_Tipo_Producto.Find(id);
            db.Tb_Tipo_Producto.Remove(tb_Tipo_Producto);
            db.SaveChanges();
            return RedirectToAction("Index");
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
