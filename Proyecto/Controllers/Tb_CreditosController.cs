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
using itextsharp.pdfa;
using System.IO;
using iTextSharp.text.html.simpleparser;
using Proyecto.Tags;

namespace Proyecto.Controllers
{
    [Autenticado]
    public class Tb_CreditosController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Creditos
        public ActionResult Index(string id)
        {
            if (id == "1" || id == null)
            {
                ViewBag.dato = db.Consultar_Credito(1);
            }
            else
            {
                if (id == "2")
                {
                    ViewBag.dato = db.Consultar_Credito(2);
                }
                else
                {
                    if (id == "3")
                    {
                        ViewBag.dato = db.Consultar_Credito(3);
                    }
                }
            }
            return View();
        }

        // GET: Tb_Creditos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Creditos tb_Creditos = db.Tb_Creditos.Find(id);
            if (tb_Creditos == null)
            {
                return HttpNotFound();
            }
            return View(tb_Creditos);
        }

        // GET: Tb_Creditos/Create
        public ActionResult Create(string id)
        {
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", selectedValue: 1);
            var listC = db.Tb_ventas.ToList();
            listC.Add(new Tb_ventas { Codigo = 0 });
            listC = listC.OrderBy(c => c.Codigo).ToList();
            ViewBag.Venta = new SelectList(listC, "Codigo", "Codigo", selectedValue: id);
            var fecha = db.Fecha().ToList();
            ViewBag.Fecha = fecha[0];
            ViewBag.Codigo = id;
            return View();
        }

        // POST: Tb_Creditos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Fecha,Estado,Numero_Pagare,Cuota_inicial,Total_Adeudado,Venta")] Tb_Creditos tb_Creditos)
        {
            if (ModelState.IsValid)
            {
                var cuota = Convert.ToInt32(tb_Creditos.Cuota_inicial);
                db.Reg_Credito(tb_Creditos.Codigo, System.DateTime.Now, tb_Creditos.Estado, tb_Creditos.Numero_Pagare, cuota, tb_Creditos.Total_Adeudado, tb_Creditos.Venta);
                pdf(tb_Creditos.Codigo);
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", selectedValue: 1);
            var listC = db.Tb_ventas.ToList();
            listC.Add(new Tb_ventas { Codigo = 0 });
            listC = listC.OrderBy(c => c.Codigo).ToList();
            ViewBag.Venta = new SelectList(listC, "Codigo", "Codigo");
            return View(tb_Creditos);
        }

        // GET: Tb_Creditos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Creditos tb_Creditos = db.Tb_Creditos.Find(id);
            if (tb_Creditos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Creditos.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Creditos.Venta);
            return View(tb_Creditos);
        }

        // POST: Tb_Creditos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Fecha,Estado,Numero_Pagare,Cuota_inicial,Total_Adeudado,Venta")] Tb_Creditos tb_Creditos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Creditos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Creditos.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Creditos.Venta);
            return View(tb_Creditos);
        }

        // GET: Tb_Creditos/Delete/5
        public JsonResult Delete(int? id)
        {
            int? result = 0;
            if (ModelState.IsValid)
            {
                var res = db.cambiar_estado_credito(id).ToList();
                result = res[0];
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Venta(string id)
        {
            var ventas = Convert.ToInt16(id);
            List<Cargar_Venta_Result> dato = db.Cargar_Venta(ventas).ToList();
            return Json(dato, JsonRequestBehavior.AllowGet);
        }

        public void pdf(long cod)
        {

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            int codi = Convert.ToInt32(cod);
            var pffcon = db.facturaCrédito(codi).ToList();

            string codigo = pffcon[0].Codigo.ToString();
            string cliente = pffcon[0].Nombre.ToString();
            string resolucion = pffcon[0].Resolucion.ToString();
            string prefijo = pffcon[0].Prefijo.ToString();
            string fechavigencia = pffcon[0].Fecha_inicio_vigencia.ToLongDateString();
            string direccion = pffcon[0].Direccion.ToString();
            string telefono = pffcon[0].Telefono_Cliente.ToString();
            string subt = pffcon[0].Sub_Total.ToString();
            string iva = pffcon[0].iva.ToString();
            string total = pffcon[0].Total.ToString();
            string totalA = pffcon[0].Total_Adeudado.ToString();
            string cuotaIni = pffcon[0].Cuota_inicial.ToString();
            string pagare = pffcon[0].Numero_Pagare.ToString();

            var contar = db.contardetalle(Convert.ToInt32(codigo)).ToList();

            var detalles = db.consultar_detalle_pdf(Convert.ToInt32(codigo)).ToList();




            //var calculo = Convert.ToInt32(cantidad) * Convert.ToDouble(vunit) - Convert.ToInt32(des);
            string path1 = @"c:\repors\ventasCredito\Factura" + codigo + ".pdf";
            try
            {
                PdfWriter.GetInstance(pdfDoc, new FileStream(path1, FileMode.Create));

                //Open PDF Document to write data 
                pdfDoc.Open();


                string cadenaFinal = "";
                string path = Server.MapPath("~/assent/img/IMG_5977copia.png");
                cadenaFinal += "<img  ALIGN=center src='" + path + "' Height='120' Width='300' /><br/> <H1 ALIGN=center>  VENTA CRÉDITO</H1><br/>";
                cadenaFinal += "ADRIAN FERNEY GIRALDO HOYOS<br/>" + "Nit. 71.374.723 - 1 * IVA Régimen Común <br/>" + " Autoriza Resolución DIAN N°" + resolucion + " <br/> Fecha:  " + fechavigencia + " Rango del " + prefijo + "   " + codigo + " <br/> " + " " + "Factura de venta: " + prefijo + "   " + codigo + "<br/> Número de pagaré: " + pagare;
                cadenaFinal += "<br/><br/><TABLE BORDER='0'><TR><TD colspan='4' align=center bgcolor='#878c8e'>INFORMACIÓN BÁSICA DEL CLIENTE </TD></TR>" +
                               "<TR><TD width='12%' font-weight:bold>Nombre: </TD><TD>" + cliente + "</TD><TD width='12%' font-weight:bold>NIT/ID: </TD><TD></TR>" +
                               "<TR></TD><TD font-weight:bold>Teléfono: </TD><TD>" + telefono + "</TD><TD font-weight:bold>Dirección: </TD><TD>" + direccion + "</TD></TR></TABLE> <br/><br/>";

                //cadenaFinal += "ADRIAN FERNEY GIRALDO HOYOS" + "<br/>" + "Nit. 71.374.723-1  * IVA Régimen Común " + "                  "+" Autoriza Resolución DIAN N°" + resolucion + "<br/> Fecha:  " + fechavigencia + "Rango del " + prefijo + "   " + codigo + "<br/>" + "                      " + "Factura de venta:  " + prefijo + "   " + codigo;
                //cadenaFinal += "<br/>Nombre: " + "  " + cliente + "         "+ "Ciudad: Medellín <br/>";
                //cadenaFinal += "Dirección:  " + direccion + "<br/>"+ "Teléfono:  " + telefono + "<br/><br/>";

                cadenaFinal += "<TABLE border='1'><TR bgcolor='#878c8e' color='white'><TD width='8%'>CANT</TD><TD width='40%'> DESCRIPCIÓN</TD><TD width='11%'>VR UNIDAD</TD><TD width='11%'>DESC. </TD><TD width='11%'>VR TOTAL</TD></TR>";
                for (int i = 0; i < contar[0]; i++)
                {
                    double descu = Convert.ToDouble(detalles[i].Descuento);
                    int cant = Convert.ToInt32(detalles[i].Cantidad);
                    double precio = Convert.ToDouble(detalles[i].Precio);
                    int calculo = Convert.ToInt32((cant * precio) - descu);
                    string c = calculo.ToString();
                    cadenaFinal += "<TR><TD>" + detalles[i].Cantidad + "</TD><TD>" + detalles[i].descripcion + "</TD><TD>$ " + Convert.ToDecimal(detalles[i].Precio).ToString("N2") + "</TD><TD>$ " + Convert.ToDecimal(detalles[i].Descuento).ToString("N2") + "</TD><TD>" + c + "</TD></TR>";
                }
                cadenaFinal += "<TR><TD colspan='3' rowspan='2' align='justify'>En esta factura se aplican todas las normas relacionadas con la letra de cambio," +
                 "de acuerdo a lo previsto en el código de comercio, reformado por la ley 1231 de 2008. No somos " +
                 "retenedores de IVA.</TD> <TD bgcolor='#878c8e' color='white'>Sub Total </TD><TD>$ " + Convert.ToDecimal(subt).ToString("N2") + "</TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Iva $</TD><TD>$ " + Convert.ToDecimal(iva).ToString("N2") + "</TD></TR>" +
                 "<TR><TD colspan='3' rowspan='3' align='justify'> Firma cliente: <br/><br/> ________________________________________________ </br> C.C o NIT </ TD >" +
                 "<TD bgcolor='#878c8e' color='white'>Total </TD><TD>$ " + Convert.ToDecimal(total).ToString("N2") + "</TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Cuota Inicial</TD><TD>$ " + Convert.ToDecimal(cuotaIni).ToString("N2") + "</TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Total Adeudado</TD><TD>$ " + Convert.ToDecimal(totalA).ToString("N2") + "</TD></TR></TABLE>";
                cadenaFinal += "<br/><br/><H2 ALIGN=CENTER>¡Gracias por su compra!</H2>";

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
