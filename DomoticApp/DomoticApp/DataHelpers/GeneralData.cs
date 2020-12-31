using DomoticApp.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomoticApp.DataHelpers
{
    public class GeneralData
    {
        public const string clientUri = "https://domoticapp-database.firebaseio.com/";
        FirebaseClient client = new FirebaseClient(clientUri);
        public int controlRol;
        
        public async Task<List<Usuarios>> GetUsuarios()
        {
            return (await client.Child("Usuarios").OnceAsync<Usuarios>()).Select(
                user => new Usuarios 
                {
                    UsuarioID = user.Object.UsuarioID,
                    Acceso = user.Object.Acceso,
                    UsuarioNombreReal = user.Object.UsuarioNombreReal,
                    UsuarioCorreo = user.Object.UsuarioCorreo,
                    UsuarioNombre = user.Object.UsuarioNombre,
                    UsuarioClave = user.Object.UsuarioClave,
                    UsuarioRol = user.Object.UsuarioRol,
                    UsuarioEstado = user.Object.UsuarioEstado
                }).ToList();
        }

        public async Task<List<ControlesAlexa>> GetControlesAlexa()
        {
            return (await client.Child("ControlesAlexa").OnceAsync<ControlesAlexa>()).Select(
                alexa => new ControlesAlexa
                {
                    AbanicoDormitorio = alexa.Object.AbanicoDormitorio,
                    AbanicoSala = alexa.Object.AbanicoSala,
                    LuzDormitorio1 = alexa.Object.LuzDormitorio1,
                    LuzDormitorio2 = alexa.Object.LuzDormitorio1,
                    LuzSala1 = alexa.Object.LuzSala1,
                    LuzSala2 = alexa.Object.LuzSala2
                }).ToList();
        }

        public async Task<Usuarios> GetUsuario(int idUsuario)
        {
            var users = await GetUsuarios();
            await client.Child("Usuarios").OnceAsync<Usuarios>();
            return users.Where(a => a.UsuarioID == idUsuario).FirstOrDefault();
        }

        public async Task AgregarUsuario(int id, string nombreReal, string correo, string nombreUsuario, string clave)
        {
            var obtenerUsuarios = await GetUsuarios();

            if (obtenerUsuarios.Count == 0)
            {
                await client.Child("Usuarios").PostAsync(new Usuarios() 
                {
                    UsuarioID = id,
                    UsuarioNombreReal = nombreReal,
                    UsuarioCorreo = correo,
                    UsuarioNombre = nombreUsuario,
                    UsuarioClave = clave,
                    UsuarioRol = "Administrador",
                    Acceso = "null",
                    UsuarioEstado = "Activo"
                 });
            }
            else
            {
                await client.Child("Usuarios").PostAsync(new Usuarios() 
                {
                    UsuarioID = id,
                    UsuarioNombreReal = nombreReal,
                    UsuarioCorreo = correo,
                    UsuarioNombre = nombreUsuario,
                    UsuarioClave = clave,
                    UsuarioRol = "Habitante",
                    Acceso = "null",
                    UsuarioEstado = "Activo"
                });
            }
        }
        
        public async Task CambiarClave(int codigo, string usuario, string fecha)
        {
            await client.Child("CambiosClaveUsuarios").PostAsync(new CambiarClaveUsuario()
            {
                CodigoCambio = codigo,
                NombreUsuario = usuario,
                FechaCambio = fecha
            });
        }
        
        public async Task<List<CambiarClaveUsuario>> GetUsuariosCambioClave()
        {
            return (await client.Child("CambiosClaveUsuarios").OnceAsync<CambiarClaveUsuario>()).Select(
                userCodigo => new CambiarClaveUsuario
                {
                    CodigoCambio = userCodigo.Object.CodigoCambio,
                    NombreUsuario = userCodigo.Object.NombreUsuario,
                    FechaCambio = userCodigo.Object.FechaCambio
                }).ToList();
        }
        
        public async Task UpdateCodigoUsuario(string usuario, int codigo)
        {
            var updateCodigo = (await client.Child("CambiosClaveUsuarios").OnceAsync<CambiarClaveUsuario>()).
                Where(a => a.Object.NombreUsuario == usuario).FirstOrDefault();

            await client.Child("CambiosClaveUsuarios").Child(updateCodigo.Key)
                .PutAsync(new CambiarClaveUsuario() { NombreUsuario = usuario, CodigoCambio = codigo});
        }
        
        public async Task UpdateUsuario(int idUsuario, string nombreRealUsuario, string correoUsuario, string nombreUsuario, string nuevaClave, 
            string rolUsuario, string acceso, string estado)
        {
            var claveNueva = (await client.Child("Usuarios").OnceAsync<Usuarios>()).
                Where(x => x.Object.UsuarioID == idUsuario).FirstOrDefault();

            await client.Child("Usuarios").Child(claveNueva.Key).PutAsync(new Usuarios() 
            {
                UsuarioID = idUsuario,
                UsuarioNombreReal = nombreRealUsuario,
                UsuarioCorreo = correoUsuario,
                UsuarioNombre = nombreUsuario,
                UsuarioClave = nuevaClave,
                UsuarioRol = rolUsuario,
                Acceso = acceso,
                UsuarioEstado = estado
            });
        }
    }
}
