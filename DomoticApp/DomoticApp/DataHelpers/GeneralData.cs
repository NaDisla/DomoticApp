﻿using DomoticApp.Models;
using DomoticApp.Views.Usuarios.GeneralLogin;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
                    UsuarioNombres = user.Object.UsuarioNombres,
                    UsuarioApellidos = user.Object.UsuarioApellidos,
                    UsuarioCorreo = user.Object.UsuarioCorreo,
                    NombreUsuario = user.Object.NombreUsuario,
                    UsuarioClave = user.Object.UsuarioClave,
                    UsuarioRol = user.Object.UsuarioRol
                }).ToList();
        }
        public async Task AgregarUsuario(int id, string nombres, string apellidos, string correo, string nombreUsuario, string clave)
        {
            var obtenerUsuarios = await GetUsuarios();

            if (obtenerUsuarios.Count == 0)
            {
                await client.Child("Usuarios").PostAsync(new Usuarios() 
                {
                    UsuarioID = id,
                    UsuarioNombres = nombres,
                    UsuarioApellidos = apellidos,
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
                    UsuarioNombres = nombres,
                    UsuarioApellidos = apellidos,
                    UsuarioCorreo = correo,
                    NombreUsuario = nombreUsuario,
                    UsuarioClave = clave,
                    UsuarioRol = "Habitante"
                });
            }
        }
    }
}
