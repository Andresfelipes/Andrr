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
    public class Tb_ConfiguracionesController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Configuraciones
        public ActionResult Index()
        {
            return View(db.Tb_Configuraciones.ToList());
        }

        // GET: Tb_Configuraciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Configuraciones tb_Configuraciones = db.Tb_Configuraciones.Find(id);
            if (tb_Configuraciones == null)
            {
                return HttpNotFound();
            }
            return View(tb_Configuraciones);
        }

        // GET: Tb_Configuraciones/Create
        [HttpGet]
        public ActionResult Create()
        {
            var contar = db.ContarConfiguracion().ToList();

            if (contar[0] == 1)
            {
                var listr = db.Tb_Sucursales.ToList();
                listr.Add(new Tb_Sucursales { Codigo = "0", Nombre = "{Seleccione Sucursal..}" });
                listr = listr.OrderBy(c => c.Nombre).ToList();
                ViewBag.Rol = new SelectList(listr, "Codigo", "Nombre");
            }
            else
            {
                return RedirectToAction("Index");

            }
            return View();
         }

        // POST: Tb_Configuraciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,AumentoFinanciacion,Tiempo,IVA,Recargo,MontoMinorista,MontoMayorista,CupoCliente,Sucursal_pricipal")] Tb_Configuraciones tb_Configuraciones)
        {

            var contar = db.ContarConfiguracion().ToList();

            if (contar[0] == 1)
            {
                if (ModelState.IsValid)
                {
                    db.Tb_Configuraciones.Add(tb_Configuraciones);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            else {

                return RedirectToAction("Index");
            }

            return View(tb_Configuraciones);
        }

        // GET: Tb_Configuraciones/Edit/5
        public ActionResult Edit(int? id)
        {
            var listr = db.Tb_Sucursales.ToList();
            listr.Add(new Tb_Sucursales { Codigo = "0", Nombre = "{Seleccione Sucursal..}" });
            listr = listr.OrderBy(c => c.Nombre).ToList();
            ViewBag.Rol = new SelectList(listr, "Codigo", "Nombre");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Configuraciones tb_Configuraciones = db.Tb_Configuraciones.Find(id);
            if (tb_Configuraciones == null)
            {
                return HttpNotFound();
            }
            return View(tb_Configuraciones);
        }

        // POST: Tb_Configuraciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,AumentoFinanciacion,Tiempo,IVA,Recargo,MontoMinorista,MontoMayorista,CupoCliente,Sucursal_pricipal")] Tb_Configuraciones tb_Configuraciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Configuraciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Configuraciones);
        }

        // GET: Tb_Configuraciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Configuraciones tb_Configuraciones = db.Tb_Configuraciones.Find(id);
            if (tb_Configuraciones == null)
            {
                return HttpNotFound();
            }
            return View(tb_Configuraciones);
        }

        // POST: Tb_Configuraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Configuraciones tb_Configuraciones = db.Tb_Configuraciones.Find(id);
            db.Tb_Configuraciones.Remove(tb_Configuraciones);
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
