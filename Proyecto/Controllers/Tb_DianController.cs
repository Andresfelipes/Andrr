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
    public class Tb_DianController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Dian
        public ActionResult Index(string id)
        {
            var ids = Convert.ToInt16(id);
            ViewBag.dato = db.Consultar_Dian(ids).ToList();
            return View();
        }

        // GET: Tb_Dian/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Dian tb_Dian = db.Tb_Dian.Find(id);
            if (tb_Dian == null)
            {
                return HttpNotFound();
            }
            return View(tb_Dian);
        }

        // GET: Tb_Dian/Create
        public ActionResult Create()
        {
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Apodo");
            return View();
        }

        // POST: Tb_Dian/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Resolucion,Fecha_inicio_vigencia,Fecha_fin_vigencia,Num_inicio_rango,Num_fin_rango,Num_actual,Sucursal,Estado")] Tb_Dian tb_Dian)
        {
            //if (ModelState.IsValid)
            //{
                db.Tb_Dian.Add(tb_Dian);
                db.SaveChanges();
            ViewBag.correcto = "Se Registro Correctamente";
               
            //}

            //ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Dian.Sucursal);
            return View();
        }

        // GET: Tb_Dian/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Dian tb_Dian = db.Tb_Dian.Find(id);
            if (tb_Dian == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Dian.Sucursal);
            return View(tb_Dian);
        }

        // POST: Tb_Dian/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Resolucion,Fecha_inicio_vigencia,Fecha_fin_vigencia,Num_inicio_rango,Num_fin_rango,Num_actual,Sucursal,Estado")] Tb_Dian tb_Dian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Dian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Dian.Sucursal);
            return View(tb_Dian);
        }

        // GET: Tb_Dian/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Dian tb_Dian = db.Tb_Dian.Find(id);
            if (tb_Dian == null)
            {
                return HttpNotFound();
            }
            return View(tb_Dian);
        }

        // POST: Tb_Dian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Dian tb_Dian = db.Tb_Dian.Find(id);
            db.Tb_Dian.Remove(tb_Dian);
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
