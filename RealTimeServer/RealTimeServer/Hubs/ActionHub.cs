using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RealTimeServer.Hubs
{
    public class ActionHub : Hub
    {
        #region Dormitorio
        public async Task SendStateLuz1Dormitorio(int stateLuz1Dormitorio)
        {
            await Clients.All.SendAsync("ReceiveStateLuz1Dormitorio", stateLuz1Dormitorio);
        }
        public async Task SendStateLuz2Dormitorio(int stateLuz2Dormitorio)
        {
            await Clients.All.SendAsync("ReceiveStateLuz2Dormitorio", stateLuz2Dormitorio);
        }
        public async Task SendStateAbanicoDormitorio(int stateAbanicoDormitorio)
        {
            await Clients.All.SendAsync("ReceiveStateAbanicoDormitorio", stateAbanicoDormitorio);
        }
        #endregion

        #region Cocina
        public async Task SendStateLuz1Cocina(int stateLuz1Cocina)
        {
            await Clients.All.SendAsync("ReceiveStateLuz1Cocina", stateLuz1Cocina);
        }
        public async Task SendStateLuz2Cocina(int stateLuz2Cocina)
        {
            await Clients.All.SendAsync("ReceiveStateLuz2Cocina", stateLuz2Cocina);
        }
        #endregion

        #region Baño
        public async Task SendStateLuzBath(int stateLuzBath)
        {
            await Clients.All.SendAsync("ReceiveStateLuzBath", stateLuzBath);
        }
        #endregion

        #region Lavadero
        public async Task SendStateLuzLavadero(int stateLuzLavadero)
        {
            await Clients.All.SendAsync("ReceiveStateLuzLavadero", stateLuzLavadero);
        }
        #endregion

        #region Sala
        public async Task SendStateLuz1Sala(int stateLuz1Sala)
        {
            await Clients.All.SendAsync("ReceiveStateLuz1Sala", stateLuz1Sala);
        }
        public async Task SendStateLuz2Sala(int stateLuz2Sala)
        {
            await Clients.All.SendAsync("ReceiveStateLuz2Sala", stateLuz2Sala);
        }
        public async Task SendStateAbanicoSala(int stateAbanicoSala)
        {
            await Clients.All.SendAsync("ReceiveStateAbanicoSala", stateAbanicoSala);
        }
        #endregion

        #region Recibidor
        public async Task SendStateLuz1Recibidor(int stateLuz1Recibidor)
        {
            await Clients.All.SendAsync("ReceiveStateLuz1Recibidor", stateLuz1Recibidor);
        }
        public async Task SendStateLuz2Recibidor(int stateLuz2Recibidor)
        {
            await Clients.All.SendAsync("ReceiveStateLuz2Recibidor", stateLuz2Recibidor);
        }
        public async Task SendStateLuz3Recibidor(int stateLuz3Recibidor)
        {
            await Clients.All.SendAsync("ReceiveStateLuz3Recibidor", stateLuz3Recibidor);
        }
        #endregion

        #region Exteriores
        public async Task SendStateLuz1Entrada(int stateLuz1Entrada)
        {
            await Clients.All.SendAsync("ReceiveStateLuz1Entrada", stateLuz1Entrada);
        }
        public async Task SendStateLuz2Entrada(int stateLuz2Entrada)
        {
            await Clients.All.SendAsync("ReceiveStateLuz2Entrada", stateLuz2Entrada);
        }
        public async Task SendStateLuz3Entrada(int stateLuz3Entrada)
        {
            await Clients.All.SendAsync("ReceiveStateLuz3Entrada", stateLuz3Entrada);
        }
        public async Task SendStateLuz1Jardin(int stateLuz1Jardin)
        {
            await Clients.All.SendAsync("ReceiveStateLuz1Jardin", stateLuz1Jardin);
        }
        public async Task SendStateLuz2Jardin(int stateLuz2Jardin)
        {
            await Clients.All.SendAsync("ReceiveStateLuz2Jardin", stateLuz2Jardin);
        }
        public async Task SendStateLuzTerraza(int stateLuzTerraza)
        {
            await Clients.All.SendAsync("ReceiveStateLuzTerraza", stateLuzTerraza);
        }
        #endregion
    }
}
