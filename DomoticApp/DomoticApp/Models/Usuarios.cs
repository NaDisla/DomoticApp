using System;
using System.Collections.Generic;
using System.Text;

namespace DomoticApp.Models
{
    public class Usuarios
    {
        public int UsuarioID { get; set; }
        public string UsuarioNombres { get; set; }
        public string UsuarioApellido { get; set; }
        public string UsuarioCorreo { get; set; }
        public string UsuarioClave { get; set; }
        public Accesos AccesoID { get; set; }
    }
}
