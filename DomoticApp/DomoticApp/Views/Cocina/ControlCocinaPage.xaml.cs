using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using System;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Cocina
{
    public partial class ControlCocinaPage : ContentPage
    {
        public static int stateLuz1 = 0, stateLuz2 = 0;
        private const string urlLuz1 = "http://10.0.0.17/luz-cocina-1", urlLuz2 = "http://10.0.0.17/luz-cocina-2";
        private readonly HttpClient client = new HttpClient();
        private string content, titleError, detailError;
        SignalRClient serverClient;
        ResultsOperations results = new ResultsOperations();
        public int stateButtonClicked = 0;
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlCocinaPage()
        {
            InitializeComponent();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
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

        [Obsolete]
        private void btnLuz1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz1, stateLuz1);
        }

        [Obsolete]
        private void btnLuz2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz2, stateLuz2);
        }

        private void btnNevera_Clicked(object sender, EventArgs e)
        {
            LayoutNevera.IsVisible = true;
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
                    //await serverClient.SignalRSendState(stateButtonClicked);
                }
                else if (url == urlLuz1 && state == 1)
                {
                    state = 0;
                    stateLuz1 = state;
                    //await serverClient.SignalRSendState(stateButtonClicked);
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
            else
            {
                titleError = "Error de conexión";
                detailError = "No se ha podido establecer la conexión con la vivienda.";
                await results.Unsuccess(titleError, detailError);
            }
        }
    }
}