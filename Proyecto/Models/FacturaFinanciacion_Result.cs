//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    
    public partial class FacturaFinanciacion_Result
    {
        public string Resolucion { get; set; }
        public System.DateTime Fecha_inicio_vigencia { get; set; }
        public int Num_inicio_rango { get; set; }
        public int Num_fin_rango { get; set; }
        public string Prefijo { get; set; }
        public string Telefono { get; set; }
        public string Direccion_sucur { get; set; }
        public int codigoVenta { get; set; }
        public System.DateTime Fecha { get; set; }
        public double Total { get; set; }
        public double iva { get; set; }
        public double Sub_Total { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono_Cliente { get; set; }
        public double Total_Adeudado { get; set; }
        public Nullable<int> Cuota_inicial { get; set; }
        public int Numero_Pagare { get; set; }
        public double Aumento { get; set; }
        public string Tiempo { get; set; }
        public int NumeroCuotas { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaLimite { get; set; }
        public System.DateTime FechaAPagar { get; set; }
        public double ValorCuotas { get; set; }
    }
}
