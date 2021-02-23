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
    public class Tb_SucursalesController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Sucursales
        public ActionResult Index()
        {
            return View(db.Tb_Sucursales.ToList());
        }

        // GET: Tb_Sucursales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Sucursales tb_Sucursales = db.Tb_Sucursales.Find(id);
            if (tb_Sucursales == null)
            {
                return HttpNotFound();
            }
            return View(tb_Sucursales);
        }

        // GET: Tb_Sucursales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Sucursales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,Apodo,Prefijo,Telefono,Direccion,NIT")] Tb_Sucursales tb_Sucursales)
        {
            if (ModelState.IsValid)
            {
                var usuariobd = db.Tb_Sucursales.Where(item => item.Codigo == tb_Sucursales.Codigo).FirstOrDefault();

                if (usuariobd == null)
                {


                    db.registrar_sucursal(tb_Sucursales.Codigo, tb_Sucursales.Nombre, tb_Sucursales.Apodo, tb_Sucursales.Prefijo, tb_Sucursales.Telefono, tb_Sucursales.Direccion, tb_Sucursales.NIT);

                    ViewBag.correct = "SeRegistroCorrectamente";

                    
                }else
                {
                    ViewBag.esta = "Esta sucursal ya se Encuentra Registrada";

                }
            }

            return View(tb_Sucursales);
        }

        // GET: Tb_Sucursales/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Sucursales tb_Sucursales = db.Tb_Sucursales.Find(id);
            if (tb_Sucursales == null)
            {
                return HttpNotFound();
            }
            return View(tb_Sucursales);
        }

        // POST: Tb_Sucursales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,Apodo,Prefijo,Telefono,Direccion,NIT")] Tb_Sucursales Sucursales)
        {
            if (ModelState.IsValid)
            {
                db.ActSucursal(Convert.ToInt32( Sucursales.Codigo), Sucursales.Nombre, Sucursales.Apodo, Sucursales.Prefijo, Sucursales.Telefono, Sucursales.Direccion, Sucursales.NIT);
                ViewBag.correcto = "edito";
            }
            return View(Sucursales);
        }

        // GET: Tb_Sucursales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Sucursales tb_Sucursales = db.Tb_Sucursales.Find(id);
            if (tb_Sucursales == null)
            {
                return HttpNotFound();
            }
            return View(tb_Sucursales);
        }

        // POST: Tb_Sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Sucursales tb_Sucursales = db.Tb_Sucursales.Find(id);
            db.Tb_Sucursales.Remove(tb_Sucursales);
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
