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
                    AccesoID = user.Object.AccesoID,
                    UsuarioNombreReal = user.Object.UsuarioNombreReal,
                    UsuarioCorreo = user.Object.UsuarioCorreo,
                    UsuarioNombre = user.Object.UsuarioNombre,
                    UsuarioClave = user.Object.UsuarioClave,
                    UsuarioRol = user.Object.UsuarioRol
                }).ToList();
        }

        public async Task<List<UsuariosAccesos>> GetUsuariosAccesos()
        {
            return (await client.Child("UsuariosAccesos").OnceAsync<UsuariosAccesos>()).Select(
                user => new UsuariosAccesos
                {
                    UsuarioID = user.Object.UsuarioID,
                    Usuario = user.Object.Usuario,
                    UsuarioNombreApellido = user.Object.UsuarioNombreApellido,
                    UsuarioAcceso = user.Object.UsuarioAcceso
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
                    UsuarioRol = "Administrador"
                 });

                await client.Child("UsuariosAccesos").PostAsync(new UsuariosAccesos()
                {
                    UsuarioID = id,
                    Usuario = nombreUsuario,
                    UsuarioNombreApellido = nombreReal,
                    UsuarioAcceso = "null"
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
                    UsuarioRol = "Habitante"
                });

                await client.Child("UsuariosAccesos").PostAsync(new UsuariosAccesos()
                {
                    UsuarioID = id,
                    Usuario = nombreUsuario,
                    UsuarioNombreApellido = nombreReal,
                    UsuarioAcceso = "null"
                });
            }
        }
        
        public async Task AgregarClaveTeclado(string usuario, string clave)
        {
            await client.Child("Accesos").PostAsync(new Accesos()
            {
                Usuario = usuario,
                ClaveTeclado = clave
            });
        }
        
        public async Task CambiarClave(int codigo, string usuario)
        {
            await client.Child("CambiosClaveUsuarios").PostAsync(new CambiarClaveUsuario()
            {
                CodigoCambio = codigo,
                NombreUsuario = usuario
            });
        }
        
        public async Task<List<CambiarClaveUsuario>> GetUsuariosCambioClave()
        {
            return (await client.Child("CambiosClaveUsuarios").OnceAsync<CambiarClaveUsuario>()).Select(
                userCodigo => new CambiarClaveUsuario
                {
                    CodigoCambio = userCodigo.Object.CodigoCambio,
                    NombreUsuario = userCodigo.Object.NombreUsuario
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
            string rolUsuario)
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
                UsuarioRol = rolUsuario
            });
        }
    }
}
