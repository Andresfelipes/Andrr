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
    public class Tb_Tipo_DocumentoController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Tipo_Documento
        public ActionResult Index()
        {
            return View(db.Tb_Tipo_Documento.ToList());
        }

        // GET: Tb_Tipo_Documento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Tipo_Documento tb_Tipo_Documento = db.Tb_Tipo_Documento.Find(id);
            if (tb_Tipo_Documento == null)
            {
                return HttpNotFound();
            }
            return View(tb_Tipo_Documento);
        }

        // GET: Tb_Tipo_Documento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Tipo_Documento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Tb_Tipo_Documento tb_Tipo_Documento)
        {
            if (ModelState.IsValid)
            {

                var usuariobd = db.Tb_Tipo_Documento.Where(item => item.Codigo == tb_Tipo_Documento.Codigo).FirstOrDefault();
                if (usuariobd == null)
                {
                    db.Tb_Tipo_Documento.Add(tb_Tipo_Documento);
                    db.SaveChanges();
                    ViewBag.correcto = "correcto";
                }
                else {

                    ViewBag.esta = "Yaesta";
                }
               
            }

            return View(tb_Tipo_Documento);
        }

        // GET: Tb_Tipo_Documento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Tipo_Documento tb_Tipo_Documento = db.Tb_Tipo_Documento.Find(id);
            if (tb_Tipo_Documento == null)
            {
                return HttpNotFound();
            }
            return View(tb_Tipo_Documento);
        }

        // POST: Tb_Tipo_Documento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre")] Tb_Tipo_Documento tb_Tipo_Documento)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tb_Tipo_Documento).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.edito = "edito";
            }
            return View(tb_Tipo_Documento);
        }

        // GET: Tb_Tipo_Documento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Tipo_Documento tb_Tipo_Documento = db.Tb_Tipo_Documento.Find(id);
            if (tb_Tipo_Documento == null)
            {
                return HttpNotFound();
            }
            return View(tb_Tipo_Documento);
        }

        // POST: Tb_Tipo_Documento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Tipo_Documento tb_Tipo_Documento = db.Tb_Tipo_Documento.Find(id);
            db.Tb_Tipo_Documento.Remove(tb_Tipo_Documento);
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
