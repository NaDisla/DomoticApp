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

namespace DomoticApp.Views.Dormitorio
{
    public partial class ControlDormitorioPage : ContentPage
    {
        public static int stateLuz1 = 0, stateLuz2 = 0, stateAbanico = 0, stateGeneral = 0;
        private const string urlGeneral = "http://10.0.0.17";
        private const string urlLuz1 = "http://10.0.0.17/luz-dormitorio-1";
        private const string urlLuz2 = "http://10.0.0.17/luz-dormitorio-2";
        private const string urlAbanico = "http://10.0.0.17/abanico-dormitorio";
        private readonly HttpClient client = new HttpClient();
        private string content, titleError, detailError;
        ValidarCambioRed cambioRed = new ValidarCambioRed();
        ResultsOperations results = new ResultsOperations();
        HubConnection connectHub;
        const string urlServer = "https://realtimeserver.conveyor.cloud/actionHub";
        int estadoLogicaLuz1 = 0, estadoLogicaLuz2 = 0, estadoLogicaAbanico = 0;

        [Obsolete]
        public ControlDormitorioPage()
        {
            InitializeComponent();
            InitializeAction();
            DatosTermicos();

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
            connectHub.On<int>("ReceiveStateLuz1Dormitorio", (stateReceived) =>
            {
                CambiaColorLuz1(btnLuz1, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz2Dormitorio", (stateReceived) =>
            {
                CambiaColorLuz2(btnLuz2, stateReceived);
            });
            connectHub.On<int>("ReceiveStateAbanicoDormitorio", (stateReceived) =>
            {
                CambiaColorAbanico(btnAbanico, stateReceived);
            });
        }

        void CambiarColorLucesON(Button button)
        {
            button.BackgroundColor = Color.FromHex("#F8F8D8");
            button.TextColor = Color.FromHex("#166498");
            button.BorderColor = Color.FromHex("#e6e620");
        }

        void CambiarColorOFF(Button button)
        {
            button.BackgroundColor = Color.FromHex("#b9d9f0");
            button.TextColor = Color.FromHex("#166498");
            button.BorderColor = Color.FromHex("#166498");
        }

        private void CambiaColorLuz1(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                CambiarColorLucesON(button);
                estadoLogicaLuz1 = 1;
            }
            else
            {
                CambiarColorOFF(button);
                estadoLogicaLuz1 = 0;
            }
        }

        private void CambiaColorLuz2(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                CambiarColorLucesON(button);
                estadoLogicaLuz2 = 1;
            }
            else
            {
                CambiarColorOFF(button);
                estadoLogicaLuz2 = 0;
            }
        }

        private void CambiaColorAbanico(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                button.BackgroundColor = Color.FromHex("#aec5d4");
                button.TextColor = Color.FromHex("#166498");
                estadoLogicaAbanico = 1;
            }
            else
            {
                CambiarColorOFF(button);
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

        public async Task SignalRSendStateLuz1Dormitorio(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz1Dormitorio", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz2Dormitorio(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz2Dormitorio", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateAbanicoDormitorio(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateAbanicoDormitorio", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnAppearing()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnDisappearing()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
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
            string temperatura = cortando[0];
            string humedad = cortando[1];

            lblTemp.Text = temperatura + "°C";
            lblHum.Text = humedad + "%";
        }

        [Obsolete]
        private async void btnAbanico_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlAbanico, stateAbanico);
            await SignalRSendStateAbanicoDormitorio(estadoLogicaAbanico);
        }

        [Obsolete]
        private async void btnLuz1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz1, stateLuz1);
            await SignalRSendStateLuz1Dormitorio(estadoLogicaLuz1);
        }

        [Obsolete]
        private async void btnLuz2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz2, stateLuz2);
            await SignalRSendStateLuz2Dormitorio(estadoLogicaLuz2);
        }

        [Obsolete]
        async void SendArduinoRequest(string contentUrl, int state)
        {
            content = await client.GetStringAsync(contentUrl);
            if (content != null)
            {
                if (contentUrl == urlAbanico && state == 0)
                {
                    state = 1;
                    stateAbanico = state;
                }
                else if (contentUrl == urlAbanico && state == 1)
                {
                    state = 0;
                    stateAbanico = state;
                }
                if (contentUrl == urlLuz1 && state == 0)
                {
                    state = 1;
                    stateLuz1 = state;
                }
                else if (contentUrl == urlLuz1 && state == 1)
                {
                    state = 0;
                    stateLuz1 = state;
                }
                if (contentUrl == urlLuz2 && state == 0)
                {
                    state = 1;
                    stateLuz2 = state;
                }
                else if (contentUrl == urlLuz2 && state == 1)
                {
                    state = 0;
                    stateLuz2 = state;
                }
            }
        }
    }
}