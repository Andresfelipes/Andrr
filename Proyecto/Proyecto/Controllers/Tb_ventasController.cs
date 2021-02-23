using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class Tb_ventasController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_ventas
        public ActionResult Index()
        {
            var tb_ventas = db.Tb_ventas.Include(t => t.Tb_Clientes).Include(t => t.Tb_Sucursales);
            return View(tb_ventas.ToList());
        }

        // GET: Tb_ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_ventas tb_ventas = db.Tb_ventas.Find(id);
            if (tb_ventas == null)
            {
                return HttpNotFound();
            }
            return View(tb_ventas);
        }

        // GET: Tb_ventas/Create
        public ActionResult Create(Tb_Usuarios Persona)
        {
           
            ViewBag.Cliente = new SelectList(db.Tb_Clientes, "Identificacion", "Nombre1");
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre");
            return View();
        }

        // POST: Tb_ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Fecha,Total,iva,Cliente,Sucursal")] Tb_ventas tb_ventas)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ventas.Add(tb_ventas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente = new SelectList(db.Tb_Clientes, "Identificacion", "Nombre1", tb_ventas.Cliente);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_ventas.Sucursal);
            return View(tb_ventas);
        }

        // GET: Tb_ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_ventas tb_ventas = db.Tb_ventas.Find(id);
            if (tb_ventas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente = new SelectList(db.Tb_Clientes, "Identificacion", "Nombre1", tb_ventas.Cliente);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_ventas.Sucursal);
            return View(tb_ventas);
        }

        // POST: Tb_ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Fecha,Total,iva,Cliente,Sucursal")] Tb_ventas tb_ventas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_ventas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente = new SelectList(db.Tb_Clientes, "Identificacion", "Nombre1", tb_ventas.Cliente);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_ventas.Sucursal);
            return View(tb_ventas);
        }

        // GET: Tb_ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_ventas tb_ventas = db.Tb_ventas.Find(id);
            if (tb_ventas == null)
            {
                return HttpNotFound();
            }
            return View(tb_ventas);
        }

        // POST: Tb_ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_ventas tb_ventas = db.Tb_ventas.Find(id);
            db.Tb_ventas.Remove(tb_ventas);
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
