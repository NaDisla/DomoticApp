using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DomoticApp.Views.Cocina
{
    public partial class ControlCocinaPage : ContentPage
    {
        public static int stateLuz1 = 0, stateLuz2 = 0;
        const string urlLuz1 = "http://10.0.0.17/luz-cocina-1", urlLuz2 = "http://10.0.0.17/luz-cocina-2";
        const string urlServer = "https://realtimeserver.conveyor.cloud/actionHub";
        private readonly HttpClient client = new HttpClient();
        private string content;
        static int estadoLogicaLuz1 = 0, estadoLogicaLuz2 = 0;
        HubConnection connectHub;
        ValidarCambioRed cambioRed = new ValidarCambioRed();
        CambiarColorBotones colorButton = new CambiarColorBotones();
        GeneralData data = new GeneralData();

        public ControlCocinaPage()
        {
            InitializeComponent();
            InitializeAction();
            EstadoBotonLuz1();
            EstadoBotonLuz2();
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
            connectHub.On<int>("ReceiveStateLuz1Cocina", (stateReceived) =>
            {
                CambiaColorLuz1(btnLuz1, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz2Cocina", (stateReceived) =>
            {
                CambiaColorLuz2(btnLuz2, stateReceived);
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

        public async Task SignalRSendStateLuz1Cocina(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz1Cocina", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz2Cocina(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz2Cocina", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async void EstadoBotonLuz1()
        {
            var getEstado = await data.GetEstadoCocina();
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
            var getEstado = await data.GetEstadoCocina();
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

        [Obsolete]
        private async void btnLuz1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz1, stateLuz1);
            await SignalRSendStateLuz1Cocina(estadoLogicaLuz1);
            await data.UpdateEstadoCocina("C01", estadoLogicaLuz1, estadoLogicaLuz2);
        }

        [Obsolete]
        private async void btnLuz2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz2, stateLuz2);
            await SignalRSendStateLuz2Cocina(estadoLogicaLuz2);
            await data.UpdateEstadoCocina("C01", estadoLogicaLuz1, estadoLogicaLuz2);
        }

        private void btnNevera_Clicked(object sender, EventArgs e)
        {
            if(LayoutNevera.IsVisible == false)
            {
                btnNevera.BackgroundColor = Color.FromHex("#aec5d4");
                btnNevera.TextColor = Color.FromHex("#166498");
                LayoutNevera.IsVisible = true;
            }
            else
            {
                btnNevera.BackgroundColor = Color.FromHex("#b9d9f0");
                btnNevera.TextColor = Color.FromHex("#166498");
                LayoutNevera.IsVisible = false;
            }
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
                else if(url == urlLuz2 && state == 0)
                {
                    state = 1;
                    stateLuz2 = state;
                }
                else if(url == urlLuz2 && state == 1)
                {
                    state = 0;
                    stateLuz2 = state;
                }
            }
        }
    }
}