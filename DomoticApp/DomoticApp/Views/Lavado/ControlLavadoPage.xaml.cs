using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Lavado
{
    public partial class ControlLavadoPage : ContentPage
    {
        public static int stateLuz = 0;
        private const string urlLuz = "http://10.0.0.17/luz-lavadero";
        private const string urlGeneral = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        private string content;
        string humedad;
        const string urlServer = "https://realtimeserver.conveyor.cloud/actionHub";
        static int estadoLogicaLuz = 0;
        HubConnection connectHub;
        CambiarColorBotones colorButton = new CambiarColorBotones();
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlLavadoPage()
        {
            InitializeComponent();
            InitializeAction();
            GetNivel();
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
            connectHub.On<int>("ReceiveStateLuzLavadero", (stateReceived) =>
            {
                CambiaColorLuz(btnLuz, stateReceived);
            });
        }

        private void CambiaColorLuz(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz = 0;
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

        public async Task SignalRSendStateLuzLavadero(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuzLavadero", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        void EstadoBotonLuz()
        {
            if (estadoLogicaLuz == 0)
            {
                colorButton.CambiarColorOFF(btnLuz);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuz);
            }
        }

        [Obsolete]
#pragma warning disable CS0809
        protected override void OnAppearing()
#pragma warning restore CS0809
        {
            base.OnAppearing();
            EstadoBotonLuz();
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

        async void GetNivel()
        {
            content = await client.GetStringAsync(urlGeneral);
            if (content != null)
            {
                string[] cortando = content.Split(';');
                humedad = cortando[6];
                if (humedad.Contains("�"))
                {
                    lblHum.Text = "8.00%";
                }
                else
                {
                    lblHum.Text = $"{humedad}%";
                }
            }
        }

        [Obsolete]
        private async void btnLuz_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz, stateLuz);
            await SignalRSendStateLuzLavadero(estadoLogicaLuz);
        }

        [Obsolete]
        async void SendArduinoRequest(string url, int state)
        {
            content = await client.GetStringAsync(url);
            if (content != null)
            {
                if (url == urlLuz && state == 0)
                {
                    state = 1;
                    stateLuz = state;
                }
                else if (url == urlLuz && state == 1)
                {
                    state = 0;
                    stateLuz = state;
                }
            }
        }

        private void btnActualizarNivelHumedad_Clicked(object sender, EventArgs e)
        {
            GetNivel();
        }
    }
}