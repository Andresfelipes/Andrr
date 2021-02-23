using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class viewModelEntradas
    {

        public Tb_Entradas Tb_Entradas { get; set; }
        public Tb_Detalle_Entrada Tb_Detalle { get; set; }
        public Tb_Configuraciones config { get; set; }
    }
}