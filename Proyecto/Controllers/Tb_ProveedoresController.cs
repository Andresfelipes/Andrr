using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Proyecto.Tags;

namespace Proyecto.Controllers
{
    [Autenticado]
    public class Tb_ProveedoresController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Proveedores
        public ActionResult Index(string id)
        {
            if (id == "1")
            {
                ViewBag.Dato = db.ConsultarProveedor("Inactivo");
            }
            else
            {
                ViewBag.Dato = db.ConsultarProveedor("Activo");
            }
            return View();
        }

        // GET: Tb_Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Proveedores tb_Proveedores = db.Tb_Proveedores.Find(id);
            if (tb_Proveedores == null)
            {
                return HttpNotFound();
            }
            return View(tb_Proveedores);
        }

        // GET: Tb_Proveedores/Create
        public ActionResult Create()
        {

            var list = db.tipo_doc().ToList();
            list.Add(new tipo_doc_Result { Codigo = 0, Nombre = "{Seleccione Tipo de Documento..}" });
            list = list.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Documento = new SelectList(list, "Codigo", "Nombre");
            return View();
         
        }

        // POST: Tb_Proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Identificacion,Celular,Telefono,Tipo_Documento,Direccion,Nit,Nombre1,Nombre2,Apellido1,Apellido2,Empresa,Estado,Email")] Tb_Proveedores tb_Proveedores)
        {
            var usuariodb = db.Tb_Proveedores.Where(item => item.Identificacion == tb_Proveedores.Identificacion).FirstOrDefault();
            if (usuariodb == null)
            {
                if (ModelState.IsValid)
                {
                    db.Tb_Proveedores.Add(tb_Proveedores);
                    db.SaveChanges();
                    ViewBag.Respuesta = "Correcto";
                }
            }else
            {
                ViewBag.correcto = "Este proveedor ya se encuentra registrado";
            }

            ViewBag.Tipo_Documento = new SelectList(db.Tb_Tipo_Documento, "Codigo", "Nombre", tb_Proveedores.Tipo_Documento);
            return View(tb_Proveedores);
        }

        public JsonResult inactivo(long? Id)
        {
            var detals = db.Usuario_estado(Id).ToList();
            return Json(detals[0], JsonRequestBehavior.AllowGet);
        }

        // GET: Tb_Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Proveedores tb_Proveedores = db.Tb_Proveedores.Find(id);
            if (tb_Proveedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo_Documento = new SelectList(db.Tb_Tipo_Documento, "Codigo", "Nombre", tb_Proveedores.Tipo_Documento);
            ViewBag.acti = tb_Proveedores.Estado;
            return View(tb_Proveedores);
        }
        public JsonResult Campos(string id)
        {
            var ids = Convert.ToInt32(id);
            var resultado = db.Campos_prov(ids).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        // POST: Tb_Proveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identificacion,Celular,Telefono,Tipo_Documento,Direccion,Nit,Nombre1,Nombre2,Apellido1,Apellido2,Empresa,Estado,Email")] Tb_Proveedores tb_Proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Proveedores).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Registro = "Correcto";
            }
            ViewBag.Tipo_Documento = new SelectList(db.Tb_Tipo_Documento, "Codigo", "Nombre", tb_Proveedores.Tipo_Documento);
            return View(tb_Proveedores);
        }

        // GET: Tb_Proveedores/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tb_Proveedores tb_Proveedores = db.Tb_Proveedores.Find(id);
        //    if (tb_Proveedores == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tb_Proveedores);
        //}

        //// POST: Tb_Proveedores/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Tb_Proveedores tb_Proveedores = db.Tb_Proveedores.Find(id);
        //    db.Tb_Proveedores.Remove(tb_Proveedores);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public JsonResult Delete(long? id)
        {
            if (ModelState.IsValid)
            {
                db.cambiar_estado_proveedor(id);
            }
            return Json(JsonRequestBehavior.AllowGet);
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
