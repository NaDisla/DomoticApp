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

        public async Task<Usuarios> GetUsuario(int idUsuario)
        {
            var users = await GetUsuarios();
            await client.Child("Usuarios").OnceAsync<Usuarios>();
            return users.Where(a => a.UsuarioID == idUsuario).FirstOrDefault();
        }

        #region EstadoDormitorio
        public async Task<List<Dormitorio>> GetEstadoDormitorio()
        {
            return (await client.Child("Dormitorio").OnceAsync<Dormitorio>()).Select(
                dormitorio => new Dormitorio
                {
                    Luz1 = dormitorio.Object.Luz1,
                    Luz2 = dormitorio.Object.Luz2,
                    Abanico = dormitorio.Object.Abanico
                }).ToList();
        }

        public async Task AgregarEstadoDormitorio()
        {
            await client.Child("Dormitorio").PostAsync(new Dormitorio()
            {
                Luz1 = 0,
                Luz2 = 0,
                Abanico = 0,
                IDArea = "D01"
            });
        }

        public async Task UpdateEstadoDormitorio(string id, int estadoLuz1, int estadoLuz2, int estadoAbanico)
        {
            var updateEstadoDormitorio = (await client.Child("Dormitorio").OnceAsync<Dormitorio>()).
                Where(a => a.Object.IDArea == id).FirstOrDefault();

            await client.Child("Dormitorio").Child(updateEstadoDormitorio.Key)
                .PutAsync(new Dormitorio()
                {
                    IDArea = id,
                    Luz1 = estadoLuz1,
                    Luz2 = estadoLuz2,
                    Abanico = estadoAbanico
                });
        }
        #endregion

        #region EstadoBath
        public async Task<List<Bathroom>> GetEstadoBath()
        {
            return (await client.Child("Baño").OnceAsync<Bathroom>()).Select(
                bath => new Bathroom
                {
                    Luz = bath.Object.Luz
                }).ToList();
        }

        public async Task AgregarEstadoBath()
        {
            await client.Child("Baño").PostAsync(new Bathroom()
            {
                Luz = 0,
                IDArea = "B01"
            });
        }

        public async Task UpdateEstadoBath(string id, int estadoLuz)
        {
            var updateEstadoBath = (await client.Child("Baño").OnceAsync<Bathroom>()).
                Where(a => a.Object.IDArea == id).FirstOrDefault();

            await client.Child("Baño").Child(updateEstadoBath.Key)
                .PutAsync(new Bathroom()
                {
                    IDArea = id,
                    Luz = estadoLuz
                });
        }
        #endregion

        #region EstadoCocina
        public async Task<List<Cocina>> GetEstadoCocina()
        {
            return (await client.Child("Cocina").OnceAsync<Cocina>()).Select(
                cocina => new Cocina
                {
                    Luz1 = cocina.Object.Luz1,
                    Luz2 = cocina.Object.Luz2
                }).ToList();
        }

        public async Task AgregarEstadoCocina()
        {
            await client.Child("Cocina").PostAsync(new Cocina()
            {
                Luz1 = 0,
                Luz2 = 0,
                IDArea = "C01"
            });
        }

        public async Task UpdateEstadoCocina(string id, int estadoLuz1, int estadoLuz2)
        {
            var updateEstadoCocina = (await client.Child("Cocina").OnceAsync<Cocina>()).
                Where(a => a.Object.IDArea == id).FirstOrDefault();

            await client.Child("Cocina").Child(updateEstadoCocina.Key)
                .PutAsync(new Cocina()
                {
                    IDArea = id,
                    Luz1 = estadoLuz1,
                    Luz2 = estadoLuz2
                });
        }
        #endregion

        #region EstadoExteriores
        public async Task<List<Exteriores>> GetEstadoExteriores()
        {
            return (await client.Child("Exteriores").OnceAsync<Exteriores>()).Select(
                exteriores => new Exteriores
                {
                    LuzEntrada1 = exteriores.Object.LuzEntrada1,
                    LuzEntrada2 = exteriores.Object.LuzEntrada2,
                    LuzEntrada3 = exteriores.Object.LuzEntrada3,
                    LuzJardin1 = exteriores.Object.LuzJardin1,
                    LuzJardin2 = exteriores.Object.LuzJardin2,
                    LuzTerraza = exteriores.Object.LuzTerraza
                }).ToList();
        }

        public async Task AgregarEstadoExteriores()
        {
            await client.Child("Exteriores").PostAsync(new Exteriores()
            {
                LuzEntrada1 = 0,
                LuzEntrada2 = 0,
                LuzEntrada3 = 0,
                LuzJardin1 = 0,
                LuzJardin2 = 0,
                LuzTerraza = 0,
                IDArea = "E01"
            });
        }

        public async Task UpdateEstadoExteriores(string id, int estadoLuzEntrada1, int estadoLuzEntrada2, int estadoLuzEntrada3,
            int estadoLuzJardin1, int estadoLuzJardin2, int estadoLuzTerraza)
        {
            var updateEstadoExteriores = (await client.Child("Exteriores").OnceAsync<Exteriores>()).
                Where(a => a.Object.IDArea == id).FirstOrDefault();

            await client.Child("Exteriores").Child(updateEstadoExteriores.Key)
                .PutAsync(new Exteriores()
                {
                    IDArea = id,
                    LuzEntrada1 = estadoLuzEntrada1,
                    LuzEntrada2 = estadoLuzEntrada2,
                    LuzEntrada3 = estadoLuzEntrada3,
                    LuzJardin1 = estadoLuzJardin1,
                    LuzJardin2 = estadoLuzJardin2,
                    LuzTerraza = estadoLuzTerraza
                });
        }
        #endregion

        #region EstadoLavado
        public async Task<List<Lavadero>> GetEstadoLavadero()
        {
            return (await client.Child("ÁreaLavado").OnceAsync<Lavadero>()).Select(
                lavado => new Lavadero
                {
                    Luz = lavado.Object.Luz
                }).ToList();
        }

        public async Task AgregarEstadoLavadero()
        {
            await client.Child("ÁreaLavado").PostAsync(new Lavadero()
            {
                Luz = 0,
                IDArea = "L01"
            });
        }

        public async Task UpdateEstadoLavado(string id, int estadoLuz)
        {
            var updateEstadoLavado = (await client.Child("ÁreaLavado").OnceAsync<Lavadero>()).
                Where(a => a.Object.IDArea == id).FirstOrDefault();

            await client.Child("ÁreaLavado").Child(updateEstadoLavado.Key)
                .PutAsync(new Lavadero()
                {
                    IDArea = id,
                    Luz = estadoLuz
                });
        }
        #endregion

        #region EstadoSala
        public async Task<List<Sala>> GetEstadoSala()
        {
            return (await client.Child("Sala").OnceAsync<Sala>()).Select(
                sala => new Sala
                {
                    Luz1 = sala.Object.Luz1,
                    Luz2 = sala.Object.Luz2,
                    Abanico = sala.Object.Abanico,
                }).ToList();
        }

        public async Task AgregarEstadoSala()
        {
            await client.Child("Sala").PostAsync(new Sala()
            {
                Luz1 = 0,
                Luz2 = 0,
                Abanico = 0,
                IDArea = "S01"
            });
        }

        public async Task UpdateEstadoSala(string id, int estadoLuz1, int estadoLuz2, int estadoAbanico)
        {
            var updateEstadoSala = (await client.Child("Sala").OnceAsync<Sala>()).
                Where(a => a.Object.IDArea == id).FirstOrDefault();

            await client.Child("Sala").Child(updateEstadoSala.Key)
                .PutAsync(new Sala()
                {
                    IDArea = id,
                    Luz1 = estadoLuz1,
                    Luz2 = estadoLuz2,
                    Abanico = estadoAbanico
                });
        }
        #endregion

        #region EstadoRecibidor
        public async Task<List<Recibidor>> GetEstadoRecibidor()
        {
            return (await client.Child("Recibidor").OnceAsync<Recibidor>()).Select(
                recibidor => new Recibidor
                {
                    Luz1 = recibidor.Object.Luz1,
                    Luz2 = recibidor.Object.Luz2,
                    Luz3 = recibidor.Object.Luz3
                }).ToList();
        }

        public async Task AgregarEstadoRecibidor()
        {
            await client.Child("Recibidor").PostAsync(new Recibidor()
            {
                Luz1 = 0,
                Luz2 = 0,
                Luz3 = 0,
                IDArea = "R01"
            });
        }

        public async Task UpdateEstadoRecibidor(string id, int estadoLuz1, int estadoLuz2, int estadoLuz3)
        {
            var updateEstadoRecibidor = (await client.Child("Recibidor").OnceAsync<Recibidor>()).
                Where(a => a.Object.IDArea == id).FirstOrDefault();

            await client.Child("Recibidor").Child(updateEstadoRecibidor.Key)
                .PutAsync(new Recibidor()
                {
                    IDArea = id,
                    Luz1 = estadoLuz1,
                    Luz2 = estadoLuz2,
                    Luz3 = estadoLuz3
                });
        }
        #endregion

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

        public async Task UpdateCodigoUsuario(string usuario, int codigo, string fechaCambio)
        {
            var updateCodigo = (await client.Child("CambiosClaveUsuarios").OnceAsync<CambiarClaveUsuario>()).
                Where(a => a.Object.NombreUsuario == usuario).FirstOrDefault();

            await client.Child("CambiosClaveUsuarios").Child(updateCodigo.Key)
                .PutAsync(new CambiarClaveUsuario() { NombreUsuario = usuario, 
                    CodigoCambio = codigo, FechaCambio = fechaCambio });
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
