﻿using DomoticApp.Models;
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
                    UsuarioNombreCompleto = user.Object.UsuarioNombreCompleto,
                    UsuarioCorreo = user.Object.UsuarioCorreo,
                    NombreUsuario = user.Object.NombreUsuario,
                    UsuarioClave = user.Object.UsuarioClave,
                    UsuarioRol = user.Object.UsuarioRol
                }).ToList();
        }

        public async Task<Usuarios> GetUsuario(int idUsuario)
        {
            var users = await GetUsuarios();
            await client.Child("Usuarios").OnceAsync<Usuarios>();
            return users.Where(a => a.UsuarioID == idUsuario).FirstOrDefault();
        }

        public async Task AgregarUsuario(int id, string nombreCompleto, string correo, string nombreUsuario, string clave)
        {
            var obtenerUsuarios = await GetUsuarios();

            if (obtenerUsuarios.Count == 0)
            {
                await client.Child("Usuarios").PostAsync(new Usuarios() 
                {
                    UsuarioID = id,
                    UsuarioNombreCompleto = nombreCompleto,
                    UsuarioCorreo = correo,
                    NombreUsuario = nombreUsuario,
                    UsuarioClave = clave,
                    UsuarioRol = "Administrador"
                 });
            }
            else
            {
                await client.Child("Usuarios").PostAsync(new Usuarios() 
                {
                    UsuarioID = id,
                    UsuarioNombreCompleto = nombreCompleto,
                    UsuarioCorreo = correo,
                    NombreUsuario = nombreUsuario,
                    UsuarioClave = clave,
                    UsuarioRol = "Habitante"
                });
            }
        }
        
        public async Task AgregarClave(int id, string nombre, string valor)
        {
            await client.Child("Accesos").PostAsync(new Accesos()
            {
                AccesoID = id,
                AccesoTipo = nombre,
                AccesoValor = valor
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
                UsuarioNombreCompleto = nombreRealUsuario,
                UsuarioCorreo = correoUsuario,
                NombreUsuario = nombreUsuario,
                UsuarioClave = nuevaClave,
                UsuarioRol = rolUsuario
            });
        }
    }
}
