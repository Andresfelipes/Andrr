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
    using System.Collections.Generic;
    
    public partial class Tb_DetalleVenta
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public Nullable<double> Descuento { get; set; }
        public double Sub_total { get; set; }
        public int Venta { get; set; }
        public string ProductoSucursal { get; set; }
        public double Precio { get; set; }
    
        public virtual Tb_Detalle_Producto_Sucursal Tb_Detalle_Producto_Sucursal { get; set; }
        public virtual Tb_ventas Tb_ventas { get; set; }
    }
}
