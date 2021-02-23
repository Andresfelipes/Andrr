using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto.Models;
using Proyecto.Tags;
using System.Text.RegularExpressions;

namespace Proyecto.Permisos
{
    
    public class PErmisos
    {

        public static int Permiso(string mmod) {
            ADMISEntities2 bd = new ADMISEntities2();
            int deegar = 0;
        
               
                var log = HttpContext.Current.User.Identity.Name;
                var id = bd.sacarid(log).ToList();
                string parent = @"/";
                string[] elementos = Regex.Split(mmod, parent);
                string controller = elementos[0];
                string Accion = elementos[1];
                int iden = Convert.ToInt32(id[0]);
                var permiso = bd.tienepermiso(controller, Accion, iden).ToList();
                if ((permiso[0] == 1))
                {

                    deegar = 1;

                }
            
                return deegar;
            
        }
    }
}