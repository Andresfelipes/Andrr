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
    public class Tb_CreditosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Creditos
        public ActionResult Index()
        {
            
            var tb_Creditos = db.Tb_Creditos.Include(t => t.Tb_Estados).Include(t => t.Tb_ventas);
            return View(tb_Creditos.ToList());
        }

        // GET: Tb_Creditos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Creditos tb_Creditos = db.Tb_Creditos.Find(id);
            if (tb_Creditos == null)
            {
                return HttpNotFound();
            }
            return View(tb_Creditos);
        }

        // GET: Tb_Creditos/Create
        public ActionResult Create()
        {
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado");
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo");
            return View();
        }

        // POST: Tb_Creditos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Fecha,Estado,Numero_Pagare,Cuota_inicial,Total_Adeudado,Venta")] Tb_Creditos tb_Creditos)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Creditos.Add(tb_Creditos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Creditos.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Creditos.Venta);
            return View(tb_Creditos);
        }

        // GET: Tb_Creditos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Creditos tb_Creditos = db.Tb_Creditos.Find(id);
            if (tb_Creditos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Creditos.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Creditos.Venta);
            return View(tb_Creditos);
        }

        // POST: Tb_Creditos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Fecha,Estado,Numero_Pagare,Cuota_inicial,Total_Adeudado,Venta")] Tb_Creditos tb_Creditos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Creditos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Creditos.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Creditos.Venta);
            return View(tb_Creditos);
        }

        // GET: Tb_Creditos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Creditos tb_Creditos = db.Tb_Creditos.Find(id);
            if (tb_Creditos == null)
            {
                return HttpNotFound();
            }
            return View(tb_Creditos);
        }

        // POST: Tb_Creditos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Creditos tb_Creditos = db.Tb_Creditos.Find(id);
            db.Tb_Creditos.Remove(tb_Creditos);
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
