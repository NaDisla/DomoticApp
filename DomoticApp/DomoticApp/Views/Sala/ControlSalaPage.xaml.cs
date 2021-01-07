using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DomoticApp.Views.Sala
{
    public partial class ControlSalaPage : ContentPage
    {
        public static int stateLuz1 = 0, stateLuz2 = 0, stateAbanico = 0;
        private const string urlLuz1 = "http://10.0.0.17/luz-sala-1";
        private const string urlLuz2 = "http://10.0.0.17/luz-sala-2";
        private const string urlAbanico = "http://10.0.0.17/abanico-sala";
        private const string urlGeneral = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        private string content;
        const string urlServer = "https://realtimeserver.conveyor.cloud/actionHub";
        static int estadoLogicaLuz1 = 0, estadoLogicaLuz2 = 0, estadoLogicaAbanico = 0;
        HubConnection connectHub;
        CambiarColorBotones colorButton = new CambiarColorBotones();
        ValidarCambioRed cambioRed = new ValidarCambioRed();
        GeneralData data = new GeneralData();

        public ControlSalaPage()
        {
            InitializeComponent();
            InitializeAction();
            DatosTermicos();
            EstadoBotonLuz1();
            EstadoBotonLuz2();
            EstadoBotonAbanico();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        private async void InitializeAction()
        {
            SetupAction();
            await SignalRConnect();
        }

        private void SetupAction()
        {
            connectHub = new HubConnectionBuilder().WithUrl(urlServer).Build();
            connectHub.On<int>("ReceiveStateLuz1Sala", (stateReceived) =>
            {
                CambiaColorLuz1(btnLuz1, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz2Sala", (stateReceived) =>
            {
                CambiaColorLuz2(btnLuz2, stateReceived);
            });
            connectHub.On<int>("ReceiveStateAbanicoSala", (stateReceived) =>
            {
                CambiaColorAbanico(btnAbanico, stateReceived);
            });
        }

        private void CambiaColorLuz1(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz1 = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz1 = 0;
            }
        }

        private void CambiaColorLuz2(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz2 = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz2 = 0;
            }
        }

        private void CambiaColorAbanico(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorOtrosON(button);
                estadoLogicaAbanico = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaAbanico = 0;
            }
        }

        public async Task SignalRConnect()
        {
            try
            {
                await connectHub.StartAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz1Sala(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz1Sala", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz2Sala(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz2Sala", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateAbanicoSala(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateAbanicoSala", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async void EstadoBotonLuz1()
        {
            var getEstado = await data.GetEstadoSala();
            var luz1 = getEstado.Where(x => x.Luz1 == 0 || x.Luz1 == 1).Select(y => y.Luz1).FirstOrDefault();

            if (luz1 == 0)
            {
                colorButton.CambiarColorOFF(btnLuz1);
                estadoLogicaLuz1 = 0;
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuz1);
                estadoLogicaLuz1 = 1;
            }
        }

        async void EstadoBotonLuz2()
        {
            var getEstado = await data.GetEstadoSala();
            var luz2 = getEstado.Where(x => x.Luz2 == 0 || x.Luz2 == 1).Select(y => y.Luz2).FirstOrDefault();

            if (luz2 == 0)
            {
                colorButton.CambiarColorOFF(btnLuz2);
                estadoLogicaLuz2 = 0;
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuz2);
                estadoLogicaLuz2 = 1;
            }
        }

        async void EstadoBotonAbanico()
        {
            var getEstado = await data.GetEstadoSala();
            var abanico = getEstado.Where(x => x.Abanico == 0 || x.Abanico == 1).Select(y => y.Abanico).FirstOrDefault();

            if (abanico == 0)
            {
                colorButton.CambiarColorOFF(btnAbanico);
                estadoLogicaAbanico = 0;
            }
            else
            {
                colorButton.CambiarColorLucesON(btnAbanico);
                estadoLogicaAbanico = 1;
            }
        }

        [Obsolete]
#pragma warning disable CS0809
        protected override void OnAppearing()
#pragma warning restore CS0809
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        [Obsolete]
#pragma warning disable CS0809
        protected override void OnDisappearing()
#pragma warning restore CS0809
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        [Obsolete]
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            cambioRed.NetworkChanged(e);
        }

        async void DatosTermicos()
        {
            content = await client.GetStringAsync(urlGeneral);
            var cortando = content.Split(';');
            string temperatura = cortando[2];
            string humedad = cortando[3];
            if (content != null)
            {
                lblTemp.Text = temperatura + "°C";
                lblHum.Text = humedad + "%";
            }
        }

        [Obsolete]
        private async void btnAbanico_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlAbanico, stateAbanico);
            await SignalRSendStateAbanicoSala(estadoLogicaAbanico);
            await data.UpdateEstadoSala("S01", estadoLogicaLuz1, estadoLogicaLuz2, estadoLogicaAbanico);
        }

        [Obsolete]
        private async void btnLuz1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz1, stateLuz1);
            await SignalRSendStateLuz1Sala(estadoLogicaLuz1);
            await data.UpdateEstadoSala("S01", estadoLogicaLuz1, estadoLogicaLuz2, estadoLogicaAbanico);
        }

        [Obsolete]
        private async void btnLuz2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz2, stateLuz2);
            await SignalRSendStateLuz2Sala(estadoLogicaLuz2);
            await data.UpdateEstadoSala("S01", estadoLogicaLuz1, estadoLogicaLuz2, estadoLogicaAbanico);
        }

        [Obsolete]
        async void SendArduinoRequest(string url, int state)
        {
            content = await client.GetStringAsync(url);
            if (content != null)
            {
                if (url == urlLuz1 && state == 0)
                {
                    state = 1;
                    stateLuz1 = state;
                }
                else if (url == urlLuz1 && state == 1)
                {
                    state = 0;
                    stateLuz1 = state;
                }
                else if (url == urlLuz2 && state == 0)
                {
                    state = 1;
                    stateLuz2 = state;
                }
                else if (url == urlLuz2 && state == 1)
                {
                    state = 0;
                    stateLuz2 = state;
                }
                else if (url == urlAbanico && state == 0)
                {
                    state = 1;
                    stateAbanico = state;
                }
                else if (url == urlAbanico && state == 1)
                {
                    state = 0;
                    stateAbanico = state;
                }
            }
        }
    }
}
