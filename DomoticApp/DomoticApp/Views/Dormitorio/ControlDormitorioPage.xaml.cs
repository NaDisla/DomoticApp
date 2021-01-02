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
        Button receiveButton;

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
            connectHub.On<int>("ReceiveState", (stateReceived) =>
            {
                CambiaColor(receiveButton, stateReceived);
            });
        }

        private void CambiaColor(Button button, int stateButton)
        {
            receiveButton = button;
            if (stateButton == 1)
            {
                button.BackgroundColor = Color.FromHex("#739DB8");
                button.TextColor = Color.White;
            }
            /*else
            {
                button.BackgroundColor = Color.FromHex("#b9d9f0");
                button.TextColor = Color.FromHex("#166498");
            }*/
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

        public async Task SignalRSendState(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendState", state);
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
            await SignalRSendState(stateAbanico);
        }

        [Obsolete]
        private async void btnLuz1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz1, stateLuz1);
            await SignalRSendState(stateLuz1);
        }

        [Obsolete]
        private async void btnLuz2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz2, stateLuz2);
            await SignalRSendState(stateLuz2);
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
                    CambiaColor(btnAbanico, stateAbanico);
                }
                else if (contentUrl == urlAbanico && state == 1)
                {
                    state = 0;
                    stateAbanico = state;
                    btnAbanico.BackgroundColor = Color.FromHex("#b9d9f0");
                    btnAbanico.TextColor = Color.FromHex("#166498");
                    //CambiaColor(btnAbanico, stateAbanico);
                }
                if (contentUrl == urlLuz1 && state == 0)
                {
                    state = 1;
                    stateLuz1 = state;
                    CambiaColor(btnLuz1, stateLuz1);
                }
                else if (contentUrl == urlLuz1 && state == 1)
                {
                    state = 0;
                    stateLuz1 = state;
                    btnLuz1.BackgroundColor = Color.FromHex("#b9d9f0");
                    btnLuz1.TextColor = Color.FromHex("#166498");
                    //CambiaColor(btnLuz1, stateLuz1);
                }
                if (contentUrl == urlLuz2 && state == 0)
                {
                    state = 1;
                    stateLuz2 = state;
                    CambiaColor(btnLuz2, stateLuz2);
                }
                else if (contentUrl == urlLuz2 && state == 1)
                {
                    state = 0;
                    stateLuz2 = state;
                    //CambiaColor(btnLuz2, stateLuz2);
                }
            }
        }
    }
}