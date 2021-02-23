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
    public class Tb_AbonosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Abonos
        public ActionResult Index()
        {
            var tb_Abonos = db.Tb_Abonos.Include(t => t.Tb_Creditos);
            return View(tb_Abonos.ToList());
        }

        // GET: Tb_Abonos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Abonos tb_Abonos = db.Tb_Abonos.Find(id);
            if (tb_Abonos == null)
            {
                return HttpNotFound();
            }
            return View(tb_Abonos);
        }

        // GET: Tb_Abonos/Create
        public ActionResult Create()
        {
            ViewBag.Credito = new SelectList(db.Tb_Creditos, "Codigo", "Codigo");
            return View();
        }

        // POST: Tb_Abonos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Fecha,Credito,Valor")] Tb_Abonos tb_Abonos)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Abonos.Add(tb_Abonos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Credito = new SelectList(db.Tb_Creditos, "Codigo", "Codigo", tb_Abonos.Credito);
            return View(tb_Abonos);
        }

        // GET: Tb_Abonos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Abonos tb_Abonos = db.Tb_Abonos.Find(id);
            if (tb_Abonos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Credito = new SelectList(db.Tb_Creditos, "Codigo", "Codigo", tb_Abonos.Credito);
            return View(tb_Abonos);
        }

        // POST: Tb_Abonos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Fecha,Credito,Valor")] Tb_Abonos tb_Abonos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Abonos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Credito = new SelectList(db.Tb_Creditos, "Codigo", "Codigo", tb_Abonos.Credito);
            return View(tb_Abonos);
        }

        // GET: Tb_Abonos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Abonos tb_Abonos = db.Tb_Abonos.Find(id);
            if (tb_Abonos == null)
            {
                return HttpNotFound();
            }
            return View(tb_Abonos);
        }

        // POST: Tb_Abonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Abonos tb_Abonos = db.Tb_Abonos.Find(id);
            db.Tb_Abonos.Remove(tb_Abonos);
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
