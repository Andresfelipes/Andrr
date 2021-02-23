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
    public class Tb_FinanciacionController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        // GET: Tb_Financiacion
        public ActionResult Index(string id)
        {
            var mora = db.mora();
            var ids = Convert.ToInt16(id);
            if (ids == 1 || id == null)
            {
                ViewBag.Dato = db.ConsultarFinanciacion(1);
                ViewBag.Esta = "1";
            }
            else
            {
                if (ids == 2)
                {
                    ViewBag.Dato = db.ConsultarFinanciacion(2);
                    ViewBag.Esta = "2";
                }
                else
                {
                    if (ids == 3)
                    {
                        ViewBag.Dato = db.ConsultarFinanciacion(3);
                        ViewBag.Esta = "3";
                    }
                    else
                    {
                        if (ids == 4)
                        {
                            ViewBag.Dato = db.ConsultarFinanciacion(4);
                            ViewBag.Esta = "1";
                        }
                        else
                        {

                        }
                    }
                }
            }
            return View();
        }

        // GET: Tb_Financiacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Financiacion tb_Financiacion = db.Tb_Financiacion.Find(id);
            if (tb_Financiacion == null)
            {
                return HttpNotFound();
            }
            return View(tb_Financiacion);
        }

        public JsonResult Codigo(string venta)
        {
            var iva = db.Cargar_Iva().ToList();
            ViewBag.Total_Adeudado = iva[0];
            return Json(JsonRequestBehavior.AllowGet);
        }

        // GET: Tb_Financiacion/Create
        public ActionResult Create(int id)
        {
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", selectedValue:1);
            var listC = db.Tb_ventas.ToList();
            listC.Add(new Tb_ventas { Codigo = 0 });
            listC = listC.OrderBy(c => c.Codigo).ToList();
            ViewBag.Venta = new SelectList(listC, "Codigo", "Codigo", selectedValue:id);
            ViewBag.Codigo = id;
            var Au = db.Aumento().ToList();
            ViewBag.Aumento = Au[0];
            var fecha = db.Fecha().ToList();
            ViewBag.Fecha = fecha[0];
            return View();
        }

        // POST: Tb_Financiacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Fecha,Estado,Numero_Pagare,Tiempo,iva,Total,Cuota_Inicial,Total_Adeudado,Venta,Numero_Cuotas")] Tb_Financiacion tb_Financiacion)
        {
            if (ModelState.IsValid)
            {
                var aum = db.Aumento().ToList();
                var aumento = Convert.ToDouble((aum[0] * tb_Financiacion.Total) / 100);
                db.Reg_Financiacion(tb_Financiacion.Codigo, System.DateTime.Now, tb_Financiacion.Estado, tb_Financiacion.Numero_Pagare, tb_Financiacion.Tiempo, aumento, tb_Financiacion.Total, tb_Financiacion.Cuota_Inicial, tb_Financiacion.Total_Adeudado, tb_Financiacion.Venta);
                pdf(tb_Financiacion.Codigo);
                ViewBag.correcto = "Correcto";
            }

            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado");
            var listC = db.Tb_ventas.ToList();
            listC.Add(new Tb_ventas { Codigo = 0 });
            listC = listC.OrderBy(c => c.Codigo).ToList();
            ViewBag.Venta = new SelectList(listC, "Codigo", "Codigo");
            var Au = db.Aumento().ToList();
            ViewBag.Aumento = Au[0];
            var fecha = db.Fecha().ToList();
            ViewBag.Fecha = fecha[0];
            return View(tb_Financiacion);
        }

        // GET: Tb_Financiacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Financiacion tb_Financiacion = db.Tb_Financiacion.Find(id);
            if (tb_Financiacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Financiacion.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Financiacion.Venta);
            return View(tb_Financiacion);
        }

        // POST: Tb_Financiacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Fecha,Estado,Numero_Pagare,Tiempo,iva,Total,Cuota_Inicial,Total_Adeudado,Venta,Numero_Cuotas")] Tb_Financiacion tb_Financiacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Financiacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.Tb_Estados, "Id", "Estado", tb_Financiacion.Estado);
            ViewBag.Venta = new SelectList(db.Tb_ventas, "Codigo", "Codigo", tb_Financiacion.Venta);
            return View(tb_Financiacion);
        }

        // GET: Tb_Financiacion/Delete/5
        public JsonResult Delete(int? id)
        {
            int? result = 0;
            if (ModelState.IsValid)
            {
                var res = db.cambiar_estado_Financiacion(id).ToList();
                result = res[0];
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        // POST: Tb_Financiacion/Delete/5

        public JsonResult Venta(string id)
        {
            var ventas = Convert.ToInt16(id);
            List<Cargar_Venta_Result> dato = db.Cargar_Venta(ventas).ToList();
            return Json(dato, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Aumento()
        {
            var dato = db.Aumento().ToList();
            return Json(dato, JsonRequestBehavior.AllowGet);
        }

        public void pdf(long cod)
        {

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            int codi = Convert.ToInt32(cod);
            var pffcon = db.FacturaFinanciacionPDF(codi).ToList();

            string codigo = pffcon[0].codigoVenta.ToString();
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
            string aumento = pffcon[0].Aumento.ToString();
            string tiempo = pffcon[0].Tiempo.ToString();
            string nunCuotas = pffcon[0].NumeroCuotas.ToString();
            string fechaIniPago = pffcon[0].FechaInicio.ToLongDateString();
            string fechaFinPago = pffcon[0].FechaLimite.ToLongDateString();
            string ProximoPago = pffcon[0].FechaAPagar.ToLongDateString();
            string valorCuotas = pffcon[0].ValorCuotas.ToString();

            int totalFinan = Convert.ToInt32(total) + Convert.ToInt32(aumento);
            string totalFin = totalFinan.ToString();

            var contar = db.contardetalle(Convert.ToInt32(codigo)).ToList();

            var detalles = db.consultar_detalle_pdf(Convert.ToInt32(codigo)).ToList();




            //var calculo = Convert.ToInt32(cantidad) * Convert.ToDouble(vunit) - Convert.ToInt32(des);
            string path1 = @"c:\repors\ventasFinanciacion\Factura" + codigo + ".pdf";
            try
            {
                PdfWriter.GetInstance(pdfDoc, new FileStream(path1, FileMode.Create));

                //Open PDF Document to write data 
                pdfDoc.Open();


                string cadenaFinal = "";
                string path = Server.MapPath("~/assent/img/IMG_5977copia.png");
                cadenaFinal += "<img  ALIGN=center src='" + path + "' Height='120' Width='300' /><br/> <H1 ALIGN=center>  VENTA FINANCIACIÓN</H1><br/>";
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
                    cadenaFinal += "<TR><TD>" + detalles[i].Cantidad + "</TD><TD>" + detalles[i].descripcion + "</TD><TD>$ " + Convert.ToDecimal(detalles[i].Precio).ToString("N2") + "</TD><TD>$ " + Convert.ToDecimal(detalles[i].Descuento).ToString("N2")  + "</TD><TD>" + c + "</TD></TR>";
                }
                cadenaFinal += "<TR><TD colspan='3' rowspan='3' align='justify'>En esta factura se aplican todas las normas relacionadas con la letra de cambio," +
                 "de acuerdo a lo previsto en el código de comercio, reformado por la ley 1231 de 2008. No somos " +
                 "retenedores de IVA.</TD> <TD bgcolor='#878c8e' color='white'>Sub Total </TD><TD>$ " + Convert.ToDecimal(subt).ToString("N2") + "</TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Iva $</TD><TD>$ " + Convert.ToDecimal(iva).ToString("N2") + "</TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Aumento</TD><TD>$ " + Convert.ToDecimal(aumento).ToString("N2") + "</TD></TR>" +                 
                 "<TR><TD colspan='3' rowspan='3' align='justify'> Firma cliente: <br/><br/> ________________________________________________ </br> C.C o NIT </ TD >" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Total </TD><TD>$ " + Convert.ToDecimal(totalFin).ToString("N2") + "</TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Cuota Inicial</TD><TD>$ " + Convert.ToDecimal(cuotaIni).ToString("N2") + "</TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Total Adeudado</TD><TD>$ " + Convert.ToDecimal(totalA).ToString("N2") + "</TD></TR></TABLE><br/>";

                cadenaFinal += "<TABLE border='1'><TR align=center bgcolor='#878c8e'><TD colspan='6'>PLAN DE PAGO</TD></TR>" +
                               "<TR bgcolor='#878c8e'><TD>Tiempo</TD><TD width='12%'>N° Cuotas</TD><TD>Fecha Inicio</TD><TD>Fecha Fin</TD><TD>Próximo Pago</TD><TD>Valor Cuotas</TD></TR>"+
                               "<TR><TD>"+tiempo+"</TD><TD width='12%'>"+nunCuotas+"</TD><TD>"+fechaIniPago+"</TD><TD>"+fechaFinPago+"</TD><TD>"+ProximoPago+"</TD><TD>$ "+ Convert.ToDecimal(valorCuotas).ToString("N2") + "</TD></TR></TABLE>";
                //"<TR bgcolor='#878c8e><TD>Tiempo</TD><TD>N° Cuotas</TD><TD>Fecha Inicio</TD><TD>Fecha Fin</TD><TD>Próximo Pago</TD><TD>Valor Cuota</TD></TR>" +
                //"<TR><TD>" + tiempo + "</TD><TD>" + nunCuotas + "</TD><TD>" + fechaIniPago + "</TD><TD>" + fechaFinPago + "</TD><TD>" + ProximoPago + "</TD><TD>" + valorCuotas + "</TD></TR></TABLE><br/><br/>";
                cadenaFinal += "<H3 ALIGN=CENTER>¡Gracias por su compra!</H3>";

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
                throw;
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
