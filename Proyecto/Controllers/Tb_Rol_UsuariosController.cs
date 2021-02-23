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
    public class Tb_Rol_UsuariosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Rol_Usuarios
        public ActionResult Index()
        {
            return View(db.Tb_Rol_Usuarios.ToList());
        }

        // GET: Tb_Rol_Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Rol_Usuarios tb_Rol_Usuarios = db.Tb_Rol_Usuarios.Find(id);
            if (tb_Rol_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tb_Rol_Usuarios);
        }

        // GET: Tb_Rol_Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Rol_Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Tb_Rol_Usuarios tb_Rol_Usuarios)
        {
            if (ModelState.IsValid)
            {
                var usuariobd = db.Tb_Rol_Usuarios.Where(item => item.Codigo == tb_Rol_Usuarios.Codigo).FirstOrDefault();
                if (usuariobd == null)
                {

                    db.Tb_Rol_Usuarios.Add(tb_Rol_Usuarios);
                    db.SaveChanges();
                    ViewBag.correcto = "SeRegistroCorrectamente";
                }
                else
                {

                    ViewBag.esta = "Ya se encuentra Registrado";

                }
            }

            return View(tb_Rol_Usuarios);
        }

        // GET: Tb_Rol_Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Rol_Usuarios tb_Rol_Usuarios = db.Tb_Rol_Usuarios.Find(id);
            if (tb_Rol_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tb_Rol_Usuarios);
        }

        // POST: Tb_Rol_Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre")] Tb_Rol_Usuarios tb_Rol_Usuarios)
        {
            if (ModelState.IsValid)
            {

                db.editar_rol(Convert.ToInt32( tb_Rol_Usuarios.Codigo), tb_Rol_Usuarios.Nombre);
                ViewBag.edito = "edito";
            }
            return View(tb_Rol_Usuarios);
        }

        // GET: Tb_Rol_Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Rol_Usuarios tb_Rol_Usuarios = db.Tb_Rol_Usuarios.Find(id);
            if (tb_Rol_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tb_Rol_Usuarios);
        }

        // POST: Tb_Rol_Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Rol_Usuarios tb_Rol_Usuarios = db.Tb_Rol_Usuarios.Find(id);
            db.Tb_Rol_Usuarios.Remove(tb_Rol_Usuarios);
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
