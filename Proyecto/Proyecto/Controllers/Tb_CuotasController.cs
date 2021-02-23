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
    public class Tb_CuotasController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Cuotas
        public ActionResult Index()
        {
            var tb_Cuotas = db.Tb_Cuotas.Include(t => t.Tb_Financiacion);
            return View(tb_Cuotas.ToList());
        }

        // GET: Tb_Cuotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Cuotas tb_Cuotas = db.Tb_Cuotas.Find(id);
            if (tb_Cuotas == null)
            {
                return HttpNotFound();
            }
            return View(tb_Cuotas);
        }

        // GET: Tb_Cuotas/Create
        public ActionResult Create()
        {
            ViewBag.Financiacion = new SelectList(db.Tb_Financiacion, "Codigo", "Tiempo");
            return View();
        }

        // POST: Tb_Cuotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Estado,Recargo,Valor,Fecha,Fecha_Limite,Financiacion")] Tb_Cuotas tb_Cuotas)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Cuotas.Add(tb_Cuotas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Financiacion = new SelectList(db.Tb_Financiacion, "Codigo", "Tiempo", tb_Cuotas.Financiacion);
            return View(tb_Cuotas);
        }

        // GET: Tb_Cuotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Cuotas tb_Cuotas = db.Tb_Cuotas.Find(id);
            if (tb_Cuotas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Financiacion = new SelectList(db.Tb_Financiacion, "Codigo", "Tiempo", tb_Cuotas.Financiacion);
            return View(tb_Cuotas);
        }

        // POST: Tb_Cuotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Estado,Recargo,Valor,Fecha,Fecha_Limite,Financiacion")] Tb_Cuotas tb_Cuotas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Cuotas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Financiacion = new SelectList(db.Tb_Financiacion, "Codigo", "Tiempo", tb_Cuotas.Financiacion);
            return View(tb_Cuotas);
        }

        // GET: Tb_Cuotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Cuotas tb_Cuotas = db.Tb_Cuotas.Find(id);
            if (tb_Cuotas == null)
            {
                return HttpNotFound();
            }
            return View(tb_Cuotas);
        }

        // POST: Tb_Cuotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Cuotas tb_Cuotas = db.Tb_Cuotas.Find(id);
            db.Tb_Cuotas.Remove(tb_Cuotas);
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
