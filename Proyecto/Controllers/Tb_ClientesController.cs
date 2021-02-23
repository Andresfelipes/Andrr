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
using Proyecto.Permisos;

namespace Proyecto.Controllers
{
    [Autenticado]
    [AuthPermisosAtribute]
    public class Tb_ClientesController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        public ActionResult buscar() {
            
            return View();
        }


        // GET: Tb_Clientes


           
        public ActionResult Index(string txtBusca, string id)
        {
            ViewBag.Tipo_Documento_Codigo = new SelectList(db.tipo_doc(), "Codigo", "Nombre");
            if ((txtBusca == "" || txtBusca == null) && (id == null))
            {
                ViewBag.dato = db.Activos();
                return View();
            }
            else
            {
                if (id == "1")
                {
                    ViewBag.dato = db.ConsultarClientes(null, null, "Inactivo").ToList();
                    return View();
                }
            }
            return RedirectToAction("Index", "Tb_Clientes");
        }

        [Autenticado]
        // GET: Tb_Clientes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Clientes tb_Clientes = db.Tb_Clientes.Find(id);
            if (tb_Clientes == null)
            {
                return HttpNotFound();
            }
            return View(tb_Clientes);
        }

        // GET: Tb_Clientes/Create
        [Autenticado]
        public ActionResult Create()
        {
            var Cupo = db.CargarCUpo().ToList();
            ViewBag.Cupo_activo = Cupo[0];
            var list = db.tipo_doc().ToList();
            list.Add(new tipo_doc_Result { Codigo = 0, Nombre = "{Seleccione Tipo de Documento..}" });
            list = list.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Documento_Codigo = new SelectList(list, "Codigo", "Nombre");
            return View();
        }

        public JsonResult Campos(string id)
        {
            var ids = Convert.ToInt32(id);
            var resultado = db.Editar(ids).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        // POST: Tb_Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Registro"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autenticado]
        public ActionResult Create(Tb_Clientes Registro)
        {
            try {

                if (Registro.Nombre1 != null)
                {
                    var usuariodb = db.Tb_Clientes.Where(item => item.Identificacion == Registro.Identificacion).FirstOrDefault();

                    if (usuariodb == null)
                    {

                        if (Registro.Tipo_Documento_Codigo == 0)
                        {
                            ModelState.AddModelError("", "DEBE SELECCIONAR UN TIPO DE DOCUMENTO");

                        }
                        else
                        {
                            db.InsertarClientes(Registro.Identificacion, Registro.Nombre1, Registro.Nombre2, Registro.Apellido1, Registro.Apellido2, Registro.Telefono, Registro.Celular, Registro.Email, Registro.Estado, Registro.TipoCliente, Registro.Direccion, Registro.Ciudad, Registro.Cupo_activo, Registro.Solicitud, Registro.Tipo_Documento_Codigo);

                            ViewBag.correctoo = "Se registro Correctamnete";
                        }
                    } else {
                        var Cupo = db.CargarCUpo().ToList();
                        ViewBag.Cupo_activo = Cupo[0];
                        ViewBag.correcto = "Este cliente ya se encuentra registrado";
                    }
                }
               
            }
            catch
            {
                throw;
            }



            var list = db.tipo_doc().ToList();
            list.Add(new tipo_doc_Result { Codigo = 0, Nombre = "{Seleccione Tipo de Documento..}" });
            list = list.OrderBy(c => c.Nombre).ToList();
            ViewBag.Tipo_Documento_Codigo = new SelectList(list, "Codigo", "Nombre");
            return View(Registro);
        }

        [Autenticado]
        public ActionResult Crear(Tb_Clientes Registro)
        {
            try
            {
                

                if (ModelState.IsValid)
                {
                    db.InsertarClientes(Registro.Identificacion, Registro.Nombre1, Registro.Nombre2, Registro.Apellido1, Registro.Apellido2, Registro.Telefono, Registro.Celular, Registro.Email, Registro.Estado, Registro.TipoCliente, Registro.Direccion, Registro.Ciudad, Registro.Cupo_activo, Registro.Solicitud, Registro.Tipo_Documento_Codigo);
                    ModelState.AddModelError("", "Se registro correctamente");
                    return RedirectToAction("Index", "Tb_Clientes");
                }
            }
            catch {

                throw;
            }
           
            ViewBag.Tipo_Documento_Codigo = new SelectList(db.tipo_doc(), "Codigo", "Nombre");
            return View(Registro);
        }
        [Autenticado]

        // GET: Tb_Clientes/Edit/5
        public ActionResult Edit(long? id)
        {

            try
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Tb_Clientes tb_Clientes = db.Tb_Clientes.Find(id);
                if (tb_Clientes == null)
                {
                    return HttpNotFound();

                }


                ViewBag.Tipo_Documento_Codigo = new SelectList(db.tipo_doc(), "Codigo", "Nombre");
                return View(tb_Clientes);

               
            }
            catch
            {

                throw;
            }
            
         
           
        }

        // POST: Tb_Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autenticado]
        public ActionResult Edit(Tb_Clientes tb_Clientes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ActualizarCliente(tb_Clientes.Identificacion, tb_Clientes.Nombre1, tb_Clientes.Nombre2, tb_Clientes.Apellido1, tb_Clientes.Apellido2, tb_Clientes.Telefono, tb_Clientes.Celular, tb_Clientes.Email, tb_Clientes.Estado, tb_Clientes.TipoCliente, tb_Clientes.Direccion, tb_Clientes.Ciudad, tb_Clientes.Cupo_activo, tb_Clientes.Solicitud, tb_Clientes.Tipo_Documento_Codigo);
                    ViewBag.Tipo_Documento_Codigo = new SelectList(db.tipo_doc(), "Codigo", "Nombre");
                    ViewBag.Registro = "Correcto";
                }



                return View(tb_Clientes);

            }
            catch
            {
                throw;
            }
            
        }

        public JsonResult inactivo(long? Id)
        {
            var detals = db.Clientes_estado(Id).ToList();
            return Json(detals[0], JsonRequestBehavior.AllowGet);
        }

        [Autenticado]

        public JsonResult Delete(long? id)
        {
            if (ModelState.IsValid)
            {
                db.cambiar_estado_cliente(id);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        // POST: Tb_Clientes/Delete/5

        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.cambiar_estado_cliente(id);
        //        db.SaveChanges();
        //    }
        //        return RedirectToAction("Index");
            
        //}
        //////public ActionResult CargarCampos( int id) {
       
        //public ActionResult Estado(Tb_Clientes tb_cliente)
        //{
            
        //    db.cambiar_estado_cliente(tb_cliente.Identificacion,Convert.ToString(tb_cliente.Estado));
        //    db.SaveChanges();
        //    return RedirectToAction("Create");
        //}


        //}

        //public ActionResult Buscar(int id) {


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
