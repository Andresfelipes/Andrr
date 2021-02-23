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
    public class Tb_EstadosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Estados
        public ActionResult Index()
        {
            return View(db.Tb_Estados.ToList());
        }

        // GET: Tb_Estados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Estados tb_Estados = db.Tb_Estados.Find(id);
            if (tb_Estados == null)
            {
                return HttpNotFound();
            }
            return View(tb_Estados);
        }

        // GET: Tb_Estados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Estados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Estado")] Tb_Estados tb_Estados)
        {
            if (ModelState.IsValid)
            {

                var usuariobd = db.Tb_Estados.Where(item => item.Id == tb_Estados.Id).FirstOrDefault();
                if (usuariobd ==null)
                {

                
                db.Tb_Estados.Add(tb_Estados);
                db.SaveChanges();
                ViewBag.correcto = "correcto";
                }else
                {

                    ViewBag.esta = "esta";
                }
            }

            return View(tb_Estados);
        }

        // GET: Tb_Estados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Estados tb_Estados = db.Tb_Estados.Find(id);
            if (tb_Estados == null)
            {
                return HttpNotFound();
            }
            return View(tb_Estados);
        }

        // POST: Tb_Estados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Estado")] Tb_Estados tb_Estados)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tb_Estados).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.edito = "edito";
                
            }
            return View(tb_Estados);
        }

        // GET: Tb_Estados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Estados tb_Estados = db.Tb_Estados.Find(id);
            if (tb_Estados == null)
            {
                return HttpNotFound();
            }
            return View(tb_Estados);
        }

        // POST: Tb_Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Estados tb_Estados = db.Tb_Estados.Find(id);
            db.Tb_Estados.Remove(tb_Estados);
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
