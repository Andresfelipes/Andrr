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
    
    public partial class ComprobanteAbonoCuotas_Result
    {
        public int Id { get; set; }
        public System.DateTime FechaAbono { get; set; }
        public Nullable<double> Recargo { get; set; }
        public double ValorAbono { get; set; }
        public int CuotasAPagar { get; set; }
        public System.DateTime FechaAPagar { get; set; }
        public double ValorCuotas { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaLimite { get; set; }
        public double Total_Adeudado { get; set; }
        public string cliente { get; set; }
        public long Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Codigo { get; set; }
    }
}
