using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class viewModelTraslados
    {

        public Tb_DetalleTranslados tb_traslados_detalle {get ;set ;}
        public Translados tb_traslados { get; set; }  
        public Tb_Configuraciones config { get; set; }   
    }
}