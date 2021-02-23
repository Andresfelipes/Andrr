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
using System.Web.Security;

using iTextSharp.text.pdf;
using iTextSharp.text;
using itextsharp.pdfa;
using System.IO;
using iTextSharp.text.html.simpleparser;

namespace Proyecto.Controllers
{
    [Autenticado]

    public class VentasController : Controller
    {
        private ADMISEntities2 db = new ADMISEntities2();

        [HttpGet]
        public ActionResult Create()
        {
            var name = HttpContext.User.Identity.Name;
            var listP = db.llenarp(name).ToList();
            var sucur = db.Llamar_Sucursal(name).ToList();
            listP.Add(new llenarp_Result { Codigo_producto = "0", Descripcion = "{Seleccione Producto...}" });
            listP = listP.OrderBy(c => c.Descripcion).ToList();
            ViewBag.Producto = new SelectList(listP, "Codigo_producto", "Descripcion");
            ViewBag.dato = db.Tb_Clientes.ToList();
            var listS = db.Tb_Sucursales.ToList();
            listS.Add(new Tb_Sucursales { Codigo = "0", Apodo = "{Seleccione Sucursal...}" });
            listS = listS.OrderBy(c => c.Nombre).ToList();
            ViewBag.Sucursal = new SelectList(listS, "Codigo", "Apodo", selectedValue: sucur[0]);
            var fecha = db.Fecha().ToList();
            ViewBag.fecha = fecha[0];
            try
            {
                var dian = db.Cargar_Dian(sucur[0]).ToList();
                ViewBag.Codigo = dian[0];
            }
            catch (Exception e)
            {
                return RedirectToAction("Create", "Tb_Dian");
            }
            return View();
        }

        public void pdf(long cod)
        {

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            int codi = Convert.ToInt32(cod);
            var pffcon = db.ReporteVenta2(codi).ToList();

            string codigo = pffcon[0].Codigo.ToString();
            string cliente = pffcon[0].Nombre.ToString();
            string resolucion = pffcon[0].Resolucion.ToString();
            string prefijo = pffcon[0].Prefijo.ToString();
            string fechavigencia = pffcon[0].Fecha_inicio_vigencia.ToLongDateString();
            string direccion = pffcon[0].Direccion.ToString();
            string telefono = pffcon[0].Telefono_Cliente.ToString();
            string cantidad = pffcon[0].Cantidad.ToString();
            string des = pffcon[0].Producto.ToString();
            string vunit = pffcon[0].Precio.ToString();
            string descuento = pffcon[0].Descuento.ToString();
            string subt = pffcon[0].Sub_Total.ToString();
            string iva = pffcon[0].iva.ToString();
            string total = pffcon[0].Total.ToString();

            telefono = telefono.Insert(3, "-");
            var contar = db.contardetalle(Convert.ToInt32(codigo)).ToList();

            var detalles = db.consultar_detalle_pdf(Convert.ToInt32(codigo)).ToList();


            //var calculo = Convert.ToInt32(cantidad) * Convert.ToDouble(vunit) - Convert.ToInt32(des);
            string path1 = @"c:\repors\ventasDetal\Factura" + codigo + ".pdf";
            try
            {
                PdfWriter.GetInstance(pdfDoc, new FileStream(path1, FileMode.Create));

                //Open PDF Document to write data 
                pdfDoc.Open();


                string cadenaFinal = "";
                string path = Server.MapPath("~/assent/img/IMG_5977copia.png");
                cadenaFinal += "<img  ALIGN=center src='" + path + "' Height='120' Width='300' /><br/> <H1 ALIGN=center>  VENTA DETAL</H1><br/>";
                cadenaFinal += "ADRIAN FERNEY GIRALDO HOYOS<br/>" + "Nit. 71.374.723 - 1 * IVA Régimen Común <br/>" + " Autoriza Resolución DIAN N°" + resolucion + " <br/> Fecha:  " + fechavigencia + " Rango del " + prefijo + "   " + codigo + " <br/> " + " " + "Factura de venta: " + prefijo + "   " + codigo;
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
                    cadenaFinal += "<TR><TD>" + detalles[i].Cantidad + "</TD><TD>" + detalles[i].descripcion + "</TD><TD>$ " + Convert.ToDecimal(detalles[i].Precio).ToString("N2") + "</TD><TD>$ " + Convert.ToDecimal(detalles[i].Descuento).ToString("N2") + "</TD><TD>$ " + Convert.ToDecimal(c).ToString("N2") + "</TD></TR>";
                }
                cadenaFinal += "<TR><TD colspan='3' rowspan='2' align='justify'>En esta factura se aplican todas las normas relacionadas con la letra de cambio," +
                 "de acuerdo a lo previsto en el código de comercio, reformado por la ley 1231 de 2008. No somos " +
                 "retenedores de IVA.</TD> <TD bgcolor='#878c8e' color='white'>Sub Total </TD><TD>$ " + Convert.ToDecimal(subt).ToString("N2") + "</TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Iva $</TD><TD>$ " + Convert.ToDecimal(iva).ToString("N2") + "</TD></TR>" +
                 "<TR><TD colspan='3' rowspan='2' align='justify'> Firma cliente: <br/><br/> ________________________________________________ </br> C.C o NIT </ TD >" +
                 "<TD bgcolor='#878c8e' color='white'> $</TD><TD></TD></TR>" +
                 "<TR><TD bgcolor='#878c8e' color='white'>Total </TD><TD>$ " + Convert.ToDecimal(total).ToString("N2") + "</TD></TR></TABLE>";
                cadenaFinal += "<br/><br/><H3 ALIGN=CENTER>¡Gracias por su compra!</H3>";

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

        public JsonResult Consultar(string elemento)
        {
            int id = Convert.ToInt16(elemento);
            List<Consultar_Detalle_Result> detals = db.Consultar_Detalle(id).ToList();
            return Json(detals, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cliente(string cliente)
        {
            int id = Convert.ToInt32(cliente);
            var detals = db.Validar_Cliente(id).ToList();
            return Json(detals[0], JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string id)
        {
            if (id == "1" || id == null)
            {
                ViewBag.dato = db.Consultar_Venta("Activo").ToList();
                ViewBag.res = db.Consultar_Venta("Activo").ToList().Count();
                ViewBag.buton = "1";
            }
            else
            {
                ViewBag.dato = db.Consultar_Venta("Anulada").ToList();
                ViewBag.res = db.Consultar_Venta("Anulada").ToList().Count();
                ViewBag.buton = "2";
            }
            return View();
        }

        [HttpPost]
        public JsonResult Create(List<Tb_DetalleVenta> detalle, string venta, string sub_total, string fecha, string sucursal, string cliente, string iva, string total, string Id)
        {
            try
            {
                var Sub = Convert.ToDouble(sub_total);
                var Tot = Convert.ToDouble(total);
                var iv = Convert.ToDouble(iva);
                var ventas = Convert.ToInt16(venta);
                db.RegistrarVenta(ventas, System.DateTime.Now, Tot, iv, Convert.ToInt32(cliente), sucursal, Sub);
                for (int i = 0; i < detalle.Count; i++)
                {
                    db.RegDetalleVenta(detalle[i].Cantidad, detalle[i].Descuento, ventas, detalle[i].Precio, detalle[i].ProductoSucursal);
                }
                if (Id == "1")
                {
                    pdf(ventas);
                }
            }catch(Exception e)
            {

            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult Precio(string id, string suc, string tipo)
        {
            int idsuc = Convert.ToInt16(suc);
            var precio = db.Precio(id, idsuc, tipo);
            return Json(precio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Produc(string id, string suc)
        {
            int idsuc = Convert.ToInt16(suc);
            var precio = db.Producto(id, idsuc).ToList();
            var pre = precio[0];
            return Json(pre, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                db.cambiar_estado_Ventas(id);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }


        public JsonResult Cantidad(string id, string suc, string cantidad)
        {
            int idsuc = Convert.ToInt16(suc);
            int cant = Convert.ToInt32(cantidad);
            var Canti = db.Cantidad_Detalle(id, cant, idsuc).ToList();
            var can = Canti[0];
            return Json(Canti, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Iva()
        {
            var iva = db.Cargar_Iva();
            return Json(iva, JsonRequestBehavior.AllowGet);
        }
    }
}