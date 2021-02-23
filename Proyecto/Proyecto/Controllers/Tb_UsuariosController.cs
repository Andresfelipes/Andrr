using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCrypto;
using Proyecto.Models;
using Proyecto.Tags;

namespace Proyecto.Controllers
{
    [Autenticado]
    public class Tb_UsuariosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Usuarios
        public ActionResult Index()
        {
            var tb_Usuarios = db.Tb_Usuarios.Include(t => t.Tb_Rol_Usuarios).Include(t => t.Tb_Sucursales).Include(t => t.Tb_Tipo_Documento);
            return View(tb_Usuarios.ToList());
        }

        // GET: Tb_Usuarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Usuarios tb_Usuarios = db.Tb_Usuarios.Find(id);
            if (tb_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tb_Usuarios);
        }

        // GET: Tb_Usuarios/Create
        public ActionResult Create()

        {








            ViewBag.Rol = new SelectList(db.Tb_Rol_Usuarios, "Codigo", "Nombre");
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre");

            var list = db.tipo_doc().ToList();
            list.Add(new tipo_doc_Result { Codigo = 0, Nombre = "{Selecccione Tipo de Documento..}" });
            list = list.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Documento_Codigo = new SelectList(list, "Codigo", "Nombre");

            return View();
        }

        // POST: Tb_Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Identificacion,Tipo_Documento,Sucursal,Nombre1,Nombre2,Apellido1,Apellido2,Contraseña,Email,Pregunta1,Pregunta2,Respuesta1,Respuesta2,Rol")] Tb_Usuarios tb_Usuarios)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //        db.Tb_Usuarios.Add(tb_Usuarios);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Rol = new SelectList(db.Tb_Rol_Usuarios, "Codigo", "Nombre", tb_Usuarios.Rol);
        //    ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Usuarios.Sucursal);
        //    ViewBag.Tipo_Documento = new SelectList(db.Tb_Tipo_Documento, "Codigo", "Nombre", tb_Usuarios.Tipo_Documento);
        //    return View(tb_Usuarios);
        //}

        public ActionResult Create([Bind(Include = "Identificacion,Tipo_Documento,Sucursal,Nombre1,Nombre2,Apellido1,Apellido2,Contraseña,Email,Pregunta1,Pregunta2,Respuesta1,Respuesta2,Rol")] Tb_Usuarios tb_Usuarios)
        {

            try
            {

               

                if (ModelState.IsValid) {
                    ICryptoService cryptoService = new PBKDF2();
                    var salt = cryptoService.GenerateSalt();
                    var contraseniaEncriptada = cryptoService.Compute(tb_Usuarios.Contraseña, salt);
                    tb_Usuarios.Contraseña = contraseniaEncriptada;
                    db.Tb_Usuarios.Add(tb_Usuarios);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Tb_Usuarios");
                }
              
            }

            catch (Exception ex)
            {
                throw;
            }
            ViewBag.Rol = new SelectList(db.Tb_Rol_Usuarios, "Codigo", "Nombre", tb_Usuarios.Rol);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Usuarios.Sucursal);

         
            ViewBag.Tipo_Documento_Codigo = new SelectList(db.tipo_doc(), "Codigo", "Nombre", tb_Usuarios.Tipo_Documento);

            return View(tb_Usuarios);
        }

        // GET: Tb_Usuarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Usuarios tb_Usuarios = db.Tb_Usuarios.Find(id);
            if (tb_Usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rol = new SelectList(db.Tb_Rol_Usuarios, "Codigo", "Nombre", tb_Usuarios.Rol);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Usuarios.Sucursal);
            ViewBag.Tipo_Documento = new SelectList(db.Tb_Tipo_Documento, "Codigo", "Nombre", tb_Usuarios.Tipo_Documento);
            return View(tb_Usuarios);
        }

        // POST: Tb_Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identificacion,Tipo_Documento,Sucursal,Nombre1,Nombre2,Apellido1,Apellido2,Contraseña,Email,Pregunta1,Pregunta2,Respuesta1,Respuesta2,Rol")] Tb_Usuarios tb_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rol = new SelectList(db.Tb_Rol_Usuarios, "Codigo", "Nombre", tb_Usuarios.Rol);
            ViewBag.Sucursal = new SelectList(db.Tb_Sucursales, "Codigo", "Nombre", tb_Usuarios.Sucursal);
            ViewBag.Tipo_Documento = new SelectList(db.Tb_Tipo_Documento, "Codigo", "Nombre", tb_Usuarios.Tipo_Documento);
            return View(tb_Usuarios);
        }

        // GET: Tb_Usuarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Usuarios tb_Usuarios = db.Tb_Usuarios.Find(id);
            if (tb_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tb_Usuarios);
        }

        // POST: Tb_Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Tb_Usuarios tb_Usuarios = db.Tb_Usuarios.Find(id);
            db.Tb_Usuarios.Remove(tb_Usuarios);
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
