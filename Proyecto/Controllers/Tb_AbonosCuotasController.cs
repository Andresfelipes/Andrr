using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using Proyecto.Tags;

namespace Proyecto.Controllers
{
    [Autenticado]
    public class Tb_AbonosCuotasController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_AbonosCuotas
        public ActionResult Index()
        {
            var tb_AbonosCuotas = db.Tb_AbonosCuotas.Include(t => t.Tb_Cuotas);
            return View(tb_AbonosCuotas.ToList());
        }

        // GET: Tb_AbonosCuotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_AbonosCuotas tb_AbonosCuotas = db.Tb_AbonosCuotas.Find(id);
            if (tb_AbonosCuotas == null)
            {
                return HttpNotFound();
            }
            return View(tb_AbonosCuotas);
        }

        // GET: Tb_AbonosCuotas/Create
        public ActionResult Create(string id)
        {
            if (id != null)
            {
                var ids = db.Cargar_Cuota(id).ToList();
                ViewBag.Cuotas = new SelectList(db.Tb_Cuotas, "Codigo", "Codigo", selectedValue: ids[0]);
                var fecha = db.Fecha().ToList();
                ViewBag.Fecha = fecha[0];
                var recargo = db.Recargos(ids[0]).ToString();
                if (recargo != "System.Data.Entity.Core.Objects.ObjectResult`1[System.Nullable`1[System.Double]]")
                {
                    ViewBag.Recargo = recargo;
                }
                else
                {
                    ViewBag.Recargo = 0;
                }
                 
                return View();
            }else
            {
                return RedirectToAction("Index", "Tb_Financiacion");
            }
        }
        public JsonResult Consultar(string elemento)
        {
            int id = Convert.ToInt16(elemento);
            List<Consultar_Abonos_Cuotas_Result> detals = db.Consultar_Abonos_Cuotas(id).ToList();
            return Json(detals, JsonRequestBehavior.AllowGet);
        }


        // POST: Tb_AbonosCuotas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cuotas,FechaAbono,ValorAbono,Recargo")] Tb_AbonosCuotas tb_AbonosCuotas)
        {
            if (ModelState.IsValid)
            {
                db.RegistrarAbonoCuota(tb_AbonosCuotas.Cuotas, tb_AbonosCuotas.FechaAbono, tb_AbonosCuotas.ValorAbono, tb_AbonosCuotas.Recargo);
                var id = db.Cuota().ToList();
                pdf(id[0]);
                return RedirectToAction("Index");
            }

            ViewBag.Cuotas = new SelectList(db.Tb_Cuotas, "Codigo", "Codigo", tb_AbonosCuotas.Cuotas);
            return View(tb_AbonosCuotas);
        }

        public void pdf(int? cod)
       {

            Document pdfDoc = new Document(PageSize.A5, 10, 10, 10, 10);
            int codi = Convert.ToInt32(cod);
            var pffcon = db.ComprobanteAbonoCuotas(codi).ToList();

            string id = pffcon[0].Id.ToString();
            string fechaAbono = pffcon[0].FechaAbono.ToLongDateString();
            string recargo = pffcon[0].Recargo.ToString();
            string valorAbono = pffcon[0].ValorAbono.ToString();
            string CaPagar = pffcon[0].CuotasAPagar.ToString();
            string ProximoPago = pffcon[0].FechaAPagar.ToLongDateString();
            string valorCuota = pffcon[0].ValorCuotas.ToString();
            string fechaini = pffcon[0].FechaInicio.ToLongDateString();
            string fechafin = pffcon[0].FechaLimite.ToLongDateString();
            string totalAd = pffcon[0].Total_Adeudado.ToString();
            string nomCliente = pffcon[0].cliente.ToString();
            string ident = pffcon[0].Identificacion.ToString();
            string telefono = pffcon[0].Telefono.ToString();
            string direccion = pffcon[0].Direccion.ToString();
            string financiacion = pffcon[0].Codigo.ToString();

            valorAbono = Convert.ToDecimal(valorAbono).ToString("N2");
            totalAd = Convert.ToDecimal(totalAd).ToString("N2");


            //var calculo = Convert.ToInt32(cantidad) * Convert.ToDouble(vunit) - Convert.ToInt32(des);
            string path1 = @"c:\repors\AbonoFinanciacion\comprobante" + id +"_"+ ident+ ".pdf";
            try
            {
                PdfWriter.GetInstance(pdfDoc, new FileStream(path1, FileMode.Create));

                //Open PDF Document to write data 
                pdfDoc.Open();


                string cadenaFinal = "";
                string path = Server.MapPath("~/assent/dh.png");
                cadenaFinal += "<img src='" + path + "' Height='120' Width='120'/><br/> <H1>ALMACÉN DULCE HOGAR</H1><br/><H2>COMPROBANTE DE ABONO</H2>";
                cadenaFinal += "<br/><TABLE BORDER='0'><TR><TD colspan='2'> CLIENTE </TD></TR>" +
                               "<TR><TD width='20%' font-weight:bold >Nombre: </TD><TD ALIGN=left>" + nomCliente + "</TD></TR>" +
                               "<TR><TD width='20%' font-weight:bold >Identificación: </TD><TD ALIGN=left>" + ident + "</TD></TR></TABLE><BR/></BR>";              
                cadenaFinal += "<TABLE BORDER='0'><TR><TD colspan='2'>DATOS DEL ABONO</TD></TR>" +
                               "<TR><TD width='30' >Financiación: </TD><TD ALIGN=left>" + financiacion + "</TD></TR>" +
                               "<TR><TD width='30%' >Fecha:</TD><TD ALIGN=left>" + fechaAbono + "</TD></TR>" +
                               "<TR><TD width='30%' >Valor del abono: </TD><TD ALIGN=left>$ " + valorAbono + "</TD></TR>" +
                               "<TR><TD width='30%' >Recargo por Mora: </TD><TD ALIGN=left>$ " + recargo + "</TD></TR>" +
                               "<TR><TD width='30%' >Total adeudado</TD><TD ALIGN=left>$ " + totalAd + "</TD></TR>"+
                               "<b><TR><TD width='30%' >Próximo pago:</TD><TD ALIGN=left>" + ProximoPago + "</TD></TR>"+
                               "<TR><TD width='30%' >Valor de la cuota:</TD><TD ALIGN=left>$ " + valorCuota + "</TD></TR></b></TABLE>";
                cadenaFinal += "<p size=1>Financiación vigente desde el " + fechaini + " hasta el " + fechafin + "</p>";
                cadenaFinal += "<b>CONTACTENOS<BR/><p size=1>Dirección: " + direccion + "<BR/> Teléfono: " + telefono + "</p></b>";


                //Assign Html content in a string to write in PDF 
                string strContent = cadenaFinal;
                /*HttpUtility.HtmlEncode(cadenaFinal);*/
                

                //Read string contents using stream reader and convert html to parsed conent 
                var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(strContent), null);

                //Get each array values from parsed elements and add to the PDF document 
                foreach (var htmlElement in parsedHtmlElements)
                    pdfDoc.Add(htmlElement as IElement);

                //Close your PDF 
                pdfDoc.Close();

                Response.ContentType = "application/pdf";

                //Set default file Name as current datetime 
                Response.AddHeader("content-disposition", "attachment; filename=DEMO.pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);

                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

        }

        // GET: Tb_AbonosCuotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_AbonosCuotas tb_AbonosCuotas = db.Tb_AbonosCuotas.Find(id);
            if (tb_AbonosCuotas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cuotas = new SelectList(db.Tb_Cuotas, "Codigo", "Codigo", tb_AbonosCuotas.Cuotas);
            return View(tb_AbonosCuotas);
        }

        // POST: Tb_AbonosCuotas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cuotas,FechaAbono,ValorAbono,Recargo")] Tb_AbonosCuotas tb_AbonosCuotas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_AbonosCuotas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cuotas = new SelectList(db.Tb_Cuotas, "Codigo", "Codigo", tb_AbonosCuotas.Cuotas);
            return View(tb_AbonosCuotas);
        }

        // GET: Tb_AbonosCuotas/Delete/5
        public JsonResult Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                db.Anular_Abono(id);
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
