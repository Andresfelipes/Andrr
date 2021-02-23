using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class ViewModelsPermisos
    {
        public Modulos modulos {get;set;}
        public Tb_Permiso_Denegado_Roles denegar { get; set; }
        public Tb_Rol_Usuarios roles { get; set;}
        public Detalle_permiso detalle { get; set; }
        public Acciones accion { get; set; }
    }
}