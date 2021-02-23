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
    public class Tb_EntradasController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Entradas
        public ActionResult Index()
        {
            var tb_Entradas = db.Tb_Entradas.Include(t => t.Tb_Proveedores).Include(t => t.Tb_Sucursales);
            return View(tb_Entradas.ToList());
        }

        // GET: Tb_Entradas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Entradas tb_Entradas = db.Tb_Entradas.Find(id);
            if (tb_Entradas == null)
            {
                return HttpNotFound();
            }
            return View(tb_Entradas);
        }

        // GET: Tb_Entradas/Create
        public ActionResult Create()
        {
            var listP = db.Tb_Proveedores.ToList();
            ViewBag.Proveedor = new SelectList(listP, "Identificacion", "Nombre1");
            var name = HttpContext.User.Identity.Name;
            var listf = db.llenarp(name).ToList();
            var sucur = db.Llamar_Sucursal(name).ToList();
            var listPro = db.llenap_entrada(sucur[0]).ToList();
            ViewBag.dato = listPro;
            var listS = db.Tb_Sucursales.ToList();
            listS.Add(new Tb_Sucursales { Codigo = "0", Apodo = "{Selecccione Sucursal...}" });
            listS = listS.OrderBy(c => c.Apodo).ToList();
            ViewBag.Sucursal = new SelectList(listS, "Codigo", "Apodo");

            return View();

        }

        // POST: Tb_Entradas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Fecha,Total,Estado,Sucursal,Proveedor")] Tb_Entradas tb_Entradas)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Entradas.Add(tb_Entradas);
                db.SaveChanges();
                ViewBag.res = "Correcto";
            }

            ViewBag.Proveedor = new SelectList(db.Tb_Proveedores, "Identificacion", "Celular", tb_Entradas.Proveedor);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Entradas.Sucursal);
            return View(tb_Entradas);
        }

        // GET: Tb_Entradas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Entradas tb_Entradas = db.Tb_Entradas.Find(id);
            if (tb_Entradas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Proveedor = new SelectList(db.Tb_Proveedores, "Identificacion", "Celular", tb_Entradas.Proveedor);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Entradas.Sucursal);
            return View(tb_Entradas);
        }

        // POST: Tb_Entradas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Fecha,Total,Estado,Sucursal,Proveedor")] Tb_Entradas tb_Entradas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Entradas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Proveedor = new SelectList(db.Tb_Proveedores, "Identificacion", "Celular", tb_Entradas.Proveedor);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Entradas.Sucursal);
            return View(tb_Entradas);
        }

        // GET: Tb_Entradas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Entradas tb_Entradas = db.Tb_Entradas.Find(id);
            if (tb_Entradas == null)
            {
                return HttpNotFound();
            }
            return View(tb_Entradas);
        }

        // POST: Tb_Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Entradas tb_Entradas = db.Tb_Entradas.Find(id);
            db.Tb_Entradas.Remove(tb_Entradas);
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
