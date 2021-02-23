using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public partial class Tb_Usuarios
    {
        [NotMapped]
        public bool Recordarme { get; set;}
        public string nuevacontrasenia { get; set; }

        public string confirmarcontrasenia { get; set; }
    }
}