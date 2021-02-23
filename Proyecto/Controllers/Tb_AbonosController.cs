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
    public class Tb_AbonosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Abonos
        public ActionResult Index()
        {
            var tb_Abonos = db.Tb_Abonos.Include(t => t.Tb_Creditos);
            return View(tb_Abonos.ToList());
        }

        // GET: Tb_Abonos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Abonos tb_Abonos = db.Tb_Abonos.Find(id);
            if (tb_Abonos == null)
            {
                return HttpNotFound();
            }
            return View(tb_Abonos);
        }

        // GET: Tb_Abonos/Create
        public ActionResult Create(string id)
        {
            if (id != null)
            {
                ViewBag.Credito = new SelectList(db.Tb_Creditos, "Codigo", "Codigo", selectedValue:id);
                var fecha = db.Fecha().ToList();
                ViewBag.Fecha = fecha[0];
                return View();
            }else
            {
                return RedirectToAction("Index", "Tb_Creditos");
            }
        }

        // POST: Tb_Abonos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Fecha,Credito,Valor")] Tb_Abonos tb_Abonos)
        {
            if (ModelState.IsValid)
            {
                db.registrarAbono(tb_Abonos.Codigo, System.DateTime.Now, tb_Abonos.Credito, tb_Abonos.Valor, tb_Abonos.Estado);
                pdf(tb_Abonos.Codigo);
                return RedirectToAction("Index");
            }

            ViewBag.Credito = new SelectList(db.Tb_Creditos, "Codigo", "Codigo", tb_Abonos.Credito);
            var fecha = db.Fecha().ToList();
            ViewBag.Fecha = fecha[0];
            return View(tb_Abonos);
        }

        public void pdf(long cod)
        {

            Document pdfDoc = new Document(PageSize.A5, 10, 10, 10, 10);
            int codi = Convert.ToInt32(cod);
            var pffcon = db.ComprobanteAbonoCredito(codi).ToList();

            string codigo = pffcon[0].Codigo.ToString();
            string credito = pffcon[0].Credito.ToString();
            string fecha = pffcon[0].Fecha.ToLongDateString();
            string valor = pffcon[0].Valor.ToString();
            string cliente = pffcon[0].Cliente.ToString();
            string ident = pffcon[0].Identificacion.ToString();
            string totalAd = pffcon[0].Total_Adeudado.ToString();
            string telefono = pffcon[0].Telefono.ToString();
            string direccion = pffcon[0].Direccion.ToString();

            valor = Convert.ToDecimal(valor).ToString("N2");
            totalAd = Convert.ToDecimal(totalAd).ToString("N2");

            //var calculo = Convert.ToInt32(cantidad) * Convert.ToDouble(vunit) - Convert.ToInt32(des);
            string path1 = @"c:\repors\AbonoCredito\comprobante" +  codigo + "_" + ident + ".pdf";
            try
            {
                PdfWriter.GetInstance(pdfDoc, new FileStream(path1, FileMode.Create));

                //Open PDF Document to write data 
                pdfDoc.Open();


                string cadenaFinal = "";
                string path = Server.MapPath("~/assent/dh.png");
                cadenaFinal += "<img src='" + path + "' Height='120' Width='120'/><br/> <H1>ALMACÉN DULCE HOGAR</H1><br/><H2>COMPROBANTE DE ABONO</H2>";
                cadenaFinal += "<br/><TABLE BORDER='0'><TR><TD colspan='2'> CLIENTE </TD></TR>" +
                               "<TR><TD width='20%' font-weight:bold >Nombre: </TD><TD ALIGN=left>" + cliente + "</TD></TR>" +
                               "<TR><TD width='20%' font-weight:bold >Identificación: </TD><TD ALIGN=left>" + ident + "</TD></TR></TABLE><BR/></BR>";
                cadenaFinal += "<TABLE BORDER='0'><TR><TD colspan='2'>DATOS DEL ABONO</TD></TR>" +
                               "<TR><TD width='30' >Crédito: </TD><TD ALIGN=left>" + credito + "</TD></TR>" +
                               "<TR><TD width='30%' >Fecha:</TD><TD ALIGN=left>" + fecha + "</TD></TR>" +
                               "<TR><TD width='30%' >Valor del abono: </TD><TD ALIGN=left>$ " + valor + "</TD></TR>" +
                               "<TR><TD width='30%' >Total adeudado</TD><TD ALIGN=left>$ " + totalAd + "</TD></TR></TABLE>";
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

        // GET: Tb_Abonos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Abonos tb_Abonos = db.Tb_Abonos.Find(id);
            if (tb_Abonos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Credito = new SelectList(db.Tb_Creditos, "Codigo", "Codigo", tb_Abonos.Credito);
            return View(tb_Abonos);
        }

        // POST: Tb_Abonos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Fecha,Credito,Valor")] Tb_Abonos tb_Abonos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Abonos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Credito = new SelectList(db.Tb_Creditos, "Codigo", "Codigo", tb_Abonos.Credito);
            return View(tb_Abonos);
        }

        // GET: Tb_Abonos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Abonos tb_Abonos = db.Tb_Abonos.Find(id);
            if (tb_Abonos == null)
            {
                return HttpNotFound();
            }
            return View(tb_Abonos);
        }

        // POST: Tb_Abonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Abonos tb_Abonos = db.Tb_Abonos.Find(id);
            db.Tb_Abonos.Remove(tb_Abonos);
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
