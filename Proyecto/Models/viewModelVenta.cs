using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class viewModelVenta
    {
        public Tb_ventas Tb_ventas { get; set; }
        public Tb_DetalleVenta Tb_Detalle { get; set; }
    }
}