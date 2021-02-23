using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

using iTextSharp.text.pdf;
using iTextSharp.text;
using itextsharp.pdfa;
using System.IO;
using iTextSharp.text.html.simpleparser;


namespace Proyecto.Controllers
{
    public class TrasladosController : Controller
    {

        public ADMISEntities2 db = new ADMISEntities2();
        // GET: Traslados
        public ActionResult Index()
        {
            ViewBag.dato = db.Consultar_Translado();
            return View();
        }
        [HttpGet]
        public ActionResult Create() {
            var name = HttpContext.User.Identity.Name;
            var listP = db.llenarp(name).ToList();
            var sucur = db.Llamar_Sucursal(name).ToList();
            var listPro = db.llenap_entrada(sucur[0]).ToList();
            ViewBag.dato = listPro;

            var listS = db.Tb_Configuraciones.ToList();

            var lis = db.Tb_Sucursales.ToList();

            var listSU = db.Tb_Sucursales.ToList();

            ViewBag.Sucursal_origen = new SelectList(lis, "Codigo", "Apodo", selectedValue: listS[0].Sucursal_pricipal);

            ViewBag.Sucu = lis;

            return View();
        }
        public JsonResult Consultar(string elemento)
        {
            int id = Convert.ToInt16(elemento);
            List<Consultar_Detalle_Traslado_Result> detals = db.Consultar_Detalle_Traslado(id).ToList();
            return Json(detals, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cantidad(string id, string suc, string cantidad)
        {
            int idsuc = Convert.ToInt16(suc);
            int cant = Convert.ToInt32(cantidad);
            var Canti = db.Cantidad_Detalle(id, cant, idsuc).ToList();
            var can = Canti[0];
            return Json(Canti, JsonRequestBehavior.AllowGet);
        }

        public JsonResult pdfs()
        {
            pdf(824);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(List<Tb_DetalleTranslados> detalle, string traslado ,string fecha, string sucursal_origen ,string sucursal_destino, string total)
        {

            var Tot = Convert.ToDouble(total);
            var tras = Convert.ToInt32(traslado);

            db.registrarTraslado(tras, System.DateTime.Now, sucursal_origen, sucursal_destino, Tot);
            for (int i = 0; i < detalle.Count; i++)
            {
                db.RegistraDetaleTraslado(detalle[i].Cantidad,detalle[i].ProductoSucursal, detalle[i].Aumento,tras);
                pdf(tras);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }



        public void pdf(long cod)
        {

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            int codi = Convert.ToInt32(cod);
            var pffcon = db.ComprobanteTraslados(codi).ToList();

            string codigo = pffcon[0].Codigo.ToString();
            string telefonoOrigen = pffcon[0].telOrigen.ToString();
            string telefoboDestino = pffcon[0].telDestino.ToString();
            string direccionOrigen = pffcon[0].direccionOrigen.ToString();
            string direccionDestino = pffcon[0].direccionDestibo.ToString();
            string nit = pffcon[0].NIT.ToString();
            string prefijoorigen = pffcon[0].Prefijo.ToString();
            string apodoDestino = pffcon[0].Apodo.ToString();
            string fecha = pffcon[0].Fecha.ToLongDateString();
            string total = pffcon[0].Total.ToString();

            total = Convert.ToDecimal(total).ToString("N2");

            var contar = db.contardetalleTraslados(Convert.ToInt32(codigo)).ToList();

            var detalles = db.consultar_Detalle_TrasladoPDF(Convert.ToInt32(codigo)).ToList();

            //var calculo = Convert.ToInt32(cantidad) * Convert.ToDouble(vunit) - Convert.ToInt32(des);
            string path1 = @"c:\repors\Traslados\Comprobante" + codigo + ".pdf";
            try
            {
                PdfWriter.GetInstance(pdfDoc, new FileStream(path1, FileMode.Create));

                //Open PDF Document to write data 
                pdfDoc.Open();


                string cadenaFinal = "";
                string path = Server.MapPath("~/assent/img/IMG_5977copia.png");
                cadenaFinal += "<img  ALIGN=center src='" + path + "' Height='120' Width='300' /><br/> <H1 ALIGN=center> COMPROBANTE TRASLADO</H1><br/>";
                cadenaFinal += "Traslado N°" +codigo;
                cadenaFinal += "<br/><br/><TABLE BORDER='0'><TR><TD colspan='2' align=center bgcolor='#878c8e'>INFORMACIÓN SUCURSAL DESTINO </TD></TR>" +
                               "<TR><TD width='12%' font-weight:bold>Nombre: </TD><TD> Almacén Dulce Hogar - " + apodoDestino + "</TD></TR>" +
                               "<TR><TD font-weight:bold>Teléfono: </TD><TD>" + telefoboDestino + "</TD></TR>"+
                               "<TR><TD font-weight:bold>Dirección: </TD><TD>"+direccionDestino+"</TD></TR> </TABLE> <br/><br/>";

                //cadenaFinal += "ADRIAN FERNEY GIRALDO HOYOS" + "<br/>" + "Nit. 71.374.723-1  * IVA Régimen Común " + "                  "+" Autoriza Resolución DIAN N°" + resolucion + "<br/> Fecha:  " + fechavigencia + "Rango del " + prefijo + "   " + codigo + "<br/>" + "                      " + "Factura de venta:  " + prefijo + "   " + codigo;
                //cadenaFinal += "<br/>Nombre: " + "  " + cliente + "         "+ "Ciudad: Medellín <br/>";
                //cadenaFinal += "Dirección:  " + direccion + "<br/>"+ "Teléfono:  " + telefono + "<br/><br/>";

                cadenaFinal += "<TABLE border='1'><TR bgcolor='#878c8e' color='white'><TD width='8%'>CANT</TD><TD width='40%'> DESCRIPCIÓN</TD><TD width='11%'>PRECIO</TD><TD width='11%'>AUMENTO </TD><TD width='11%'>VR TOTAL </TD></TR>";
                for (int i = 0; i < contar[0]; i++)
                {
                    double aumento = Convert.ToDouble(detalles[i].Aumento);
                    int cant = Convert.ToInt32(detalles[i].Cantidad);
                    double precio = Convert.ToDouble(detalles[i].Aumento);
                    int calculo = Convert.ToInt32((aumento + precio) * cant);
                    string c = calculo.ToString();
                    cadenaFinal += "<TR><TD>" + detalles[i].Cantidad + "</TD><TD>" + detalles[i].descripcion + "</TD><TD>$ " + Convert.ToDecimal(detalles[i].Aumento).ToString("N2") + "</TD><TD>$ " + Convert.ToDecimal(detalles[i].Aumento).ToString("N2") + "</TD><TD>$ " + Convert.ToDecimal(c).ToString("N2") + "</TD></TR>";
                }
                cadenaFinal += "<TR bgcolor='#878c8e' color='white'><TD width='8%'></TD><TD width='40%'></TD><TD width='11%'></TD><TD width='11%'>Total</TD><TD width='11%'>"+total+"</TD></TR></TABLE>";
                cadenaFinal += "<b>CONTACTo<BR/><p size=1>Dirección: " + direccionOrigen + "<BR/> Teléfono: " + telefonoOrigen + "</p></b>";

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



    }
}