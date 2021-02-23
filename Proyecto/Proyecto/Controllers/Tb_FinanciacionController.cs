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
    public class Tb_FinanciacionController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Financiacion
        public ActionResult Index()
        {
            var tb_Financiacion = db.Tb_Financiacion.Include(t => t.Tb_Estados).Include(t => t.Tb_ventas);
            return View(tb_Financiacion.ToList());
        }

        // GET: Tb_Financiacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Financiacion tb_Financiacion = db.Tb_Financiacion.Find(id);
            if (tb_Financiacion == null)
            {
                return HttpNotFound();
            }
            return View(tb_Financiacion);
        }

        // GET: Tb_Financiacion/Create
        public ActionResult Create()
        {
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado");
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo");
            return View();
        }

        // POST: Tb_Financiacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Fecha,Estado,Numero_Pagare,Tiempo,iva,Total,Cuota_Inicial,Total_Adeudado,Venta,Numero_Cuotas")] Tb_Financiacion tb_Financiacion)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Financiacion.Add(tb_Financiacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Financiacion.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Financiacion.Venta);
            return View(tb_Financiacion);
        }

        // GET: Tb_Financiacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Financiacion tb_Financiacion = db.Tb_Financiacion.Find(id);
            if (tb_Financiacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Financiacion.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Financiacion.Venta);
            return View(tb_Financiacion);
        }

        // POST: Tb_Financiacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Fecha,Estado,Numero_Pagare,Tiempo,iva,Total,Cuota_Inicial,Total_Adeudado,Venta,Numero_Cuotas")] Tb_Financiacion tb_Financiacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Financiacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Financiacion.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Financiacion.Venta);
            return View(tb_Financiacion);
        }

        // GET: Tb_Financiacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Financiacion tb_Financiacion = db.Tb_Financiacion.Find(id);
            if (tb_Financiacion == null)
            {
                return HttpNotFound();
            }
            return View(tb_Financiacion);
        }

        // POST: Tb_Financiacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Financiacion tb_Financiacion = db.Tb_Financiacion.Find(id);
            db.Tb_Financiacion.Remove(tb_Financiacion);
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
