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
using iTextSharp.text.pdf;
using iTextSharp.text;
using itextsharp.pdfa;
using System.IO;




namespace Proyecto.Controllers
{
    [Autenticado]
    public class Tb_UsuariosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Usuarios
        public ActionResult Index(string id)
        {
            if(id == null)
            {
                ViewBag.Dato = db.ConsultarUsuarios("Activo");
            }
            else
            {
                ViewBag.Dato = db.ConsultarUsuarios("Inactivo");
            }
            return View();
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
            ViewBag.igual = "";

            var listr = db.Tb_Rol_Usuarios.ToList();
            listr.Add(new Tb_Rol_Usuarios { Codigo = 0, Nombre = "{Seleccione Rol..}" });
            listr = listr.OrderBy(c => c.Nombre).ToList();
            ViewBag.Rol = new SelectList(listr, "Codigo", "Nombre");

            var lists = db.Tb_Sucursales.ToList();
            lists.Add(new Tb_Sucursales { Codigo = "0", Apodo = "{Seleccione Sucursal..}" });
            lists = lists.OrderBy(c => c.Nombre).ToList();
            ViewBag.Sucursal = new SelectList(lists, "Codigo", "Apodo");

            var list = db.tipo_doc().ToList();
            list.Add(new tipo_doc_Result { Codigo = 0, Nombre = "{Seleccione Tipo de Documento..}" });
            list = list.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Documento = new SelectList(list, "Codigo", "Nombre");

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

        public ActionResult Create([Bind(Include = "Identificacion,Tipo_Documento,Sucursal,Nombre1,Nombre2,Apellido1,Apellido2,Contraseña,Email,Rol,estado")] Tb_Usuarios tb_Usuarios)
        {

            try
            {


                if (tb_Usuarios.Nombre1 != null)
                {
                    var log = HttpContext.User.Identity.Name;

                    var usuarioexiste = db.Tb_Usuarios.Where(item => item.Identificacion == tb_Usuarios.Identificacion).FirstOrDefault();

                    if (usuarioexiste == null)
                    {
                        var usuariobd = db.Tb_Usuarios.Where(item => item.Email == tb_Usuarios.Email).FirstOrDefault();
                        if (usuariobd == null)
                        {

                            //ICryptoService cryptoService = new PBKDF2();
                            //var salt = cryptoService.GenerateSalt();
                            //var contraseniaEncriptada = cryptoService.Compute(tb_Usuarios.Contraseña, salt);
                            //tb_Usuarios.Contraseña = contraseniaEncriptada;
                            db.Tb_Usuarios.Add(tb_Usuarios);
                            db.SaveChanges();
                            ViewBag.correcto = "Correcto";

                        }
                        else
                        {

                            ViewBag.igual = "Ya existe una cuenta asociada a este correo";
                        }
                    }
                    else {

                        ViewBag.existe = "Este usuario ya se encuentra registrado";

                    }

                }



            }

            catch (Exception ex)
            {
                throw;
            }
            var listr = db.Tb_Rol_Usuarios.ToList();
            listr.Add(new Tb_Rol_Usuarios { Codigo = 0, Nombre = "{Seleccione Rol..}" });
            listr = listr.OrderBy(c => c.Nombre).ToList();
            ViewBag.Rol = new SelectList(listr, "Codigo", "Nombre");

            var lists = db.Tb_Sucursales.ToList();
            lists.Add(new Tb_Sucursales { Codigo = "0", Apodo = "{Seleccione Sucursal..}" });
            lists = lists.OrderBy(c => c.Nombre).ToList();
            ViewBag.Sucursal = new SelectList(lists, "Codigo", "Apodo");

            var list = db.tipo_doc().ToList();
            list.Add(new tipo_doc_Result { Codigo = 0, Nombre = "{Seleccione Tipo de Documento..}" });
            list = list.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Documento = new SelectList(list, "Codigo", "Nombre");

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
            var listr = db.Tb_Rol_Usuarios.ToList();
            listr.Add(new Tb_Rol_Usuarios { Codigo = 0, Nombre = "{Seleccione Rol..}" });
            listr = listr.OrderBy(c => c.Nombre).ToList();
            ViewBag.Rol = new SelectList(listr, "Codigo", "Nombre");

            var lists = db.Tb_Sucursales.ToList();
            lists.Add(new Tb_Sucursales { Codigo = "0", Apodo = "{Seleccione Sucursal..}" });
            lists = lists.OrderBy(c => c.Nombre).ToList();
            ViewBag.Sucursal = new SelectList(lists, "Codigo", "Apodo");

            var list = db.tipo_doc().ToList();
            list.Add(new tipo_doc_Result { Codigo = 0, Nombre = "{Seleccione Tipo de Documento..}" });
            list = list.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Documento = new SelectList(list, "Codigo", "Nombre");
            return View(tb_Usuarios);
        }

        // POST: Tb_Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tb_Usuarios tb_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.ActualizarUsuario(tb_Usuarios.Identificacion, tb_Usuarios.Tipo_Documento, tb_Usuarios.Sucursal, tb_Usuarios.Nombre1, tb_Usuarios.Nombre2, tb_Usuarios.Apellido1, tb_Usuarios.Apellido2, tb_Usuarios.Email, tb_Usuarios.Rol, tb_Usuarios.estado);
                ViewBag.Registro = "Correcto";
            }
            var listr = db.Tb_Rol_Usuarios.ToList();
            listr.Add(new Tb_Rol_Usuarios { Codigo = 0, Nombre = "{Seleccione Rol..}" });
            listr = listr.OrderBy(c => c.Nombre).ToList();
            ViewBag.Rol = new SelectList(listr, "Codigo", "Nombre");

            var lists = db.Tb_Sucursales.ToList();
            lists.Add(new Tb_Sucursales { Codigo = "0", Apodo = "{Seleccione Sucursal..}" });
            lists = lists.OrderBy(c => c.Nombre).ToList();
            ViewBag.Sucursal = new SelectList(lists, "Codigo", "Apodo");

            var list = db.tipo_doc().ToList();
            list.Add(new tipo_doc_Result { Codigo = 0, Nombre = "{Seleccione Tipo de Documento..}" });
            list = list.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Documento = new SelectList(list, "Codigo", "Nombre");
            return View();
        }

        public JsonResult Users(string id)
        {
            long ids = Convert.ToInt64(id);
            var list = db.Consul_User(ids).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void pdf( long id) {

            string path = @"c:\repors\MyTest.pdf";
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            Paragraph paragraph = new Paragraph("Este es mi primer pdf hola wey");
            doc.Add(paragraph);
            doc.Close();


        }

        // GET: Tb_Usuarios/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tb_Usuarios tb_Usuarios = db.Tb_Usuarios.Find(id);
        //    if (tb_Usuarios == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tb_Usuarios);
        //}

        public JsonResult Delete(long? id)
        {
            if (ModelState.IsValid)
            {
                db.cambiar_estado_usuario(id);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult inactivo(long? Id)
        {
            var detals = db.Usuario_estado(Id).ToList();
            return Json(detals[0], JsonRequestBehavior.AllowGet);
        }
        // POST: Tb_Usuarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Tb_Usuarios tb_Usuarios = db.Tb_Usuarios.Find(id);
        //    db.Tb_Usuarios.Remove(tb_Usuarios);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
