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
    
    public partial class Tb_Recuperar_contraseña
    {
        public int Codigo { get; set; }
        public string Respuesta { get; set; }
        public long Usuario { get; set; }
    
        public virtual Tb_Usuarios Tb_Usuarios { get; set; }
    }
}
