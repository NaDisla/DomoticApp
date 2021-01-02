using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using System;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Exteriores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlExterioresPage : ContentPage
    {
        public static int stateLuzEntrada1 = 0, stateLuzEntrada2 = 0, stateLuzEntrada3 = 0, stateLuzJardin1 = 0, stateLuzJardin2 = 0,
            stateLuzTerraza = 0;
        private const string urlLuzEntrada1 = "http://10.0.0.17/luz-entrada", urlLuzEntrada2 = "http://10.0.0.17/luz-frente-1",
            urlLuzEntrada3 = "http://10.0.0.17/luz-frente-2", urlLuzJardin1 = "http://10.0.0.17/luz-jardin-1", 
            urlLuzJardin2 = "http://10.0.0.17/luz-jardin-2", urlLuzTerraza = "http://10.0.0.17/luz-terraza-sala";
        private readonly HttpClient client = new HttpClient();
        private string content;

        SignalRClient serverClient;
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlExterioresPage()
        {
            InitializeComponent();

            //if (btnLuzEntrada1.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuzEntrada1);
            //}
            //else if (btnLuzEntrada2.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuzEntrada2);
            //}
            //else if (btnLuzEntrada3.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuzEntrada3);
            //}
            //else if (btnLuzJardin1.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuzJardin1);
            //}
            //else if (btnLuzJardin2.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuzJardin2);
            //}
            //else if (btnLuzTerraza.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuzTerraza);
            //}

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
        private void btnLuzEntrada1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada1, stateLuzEntrada1);
        }

        [Obsolete]
        private void btnLuzEntrada2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada2, stateLuzEntrada2);
        }

        [Obsolete]
        private void btnLuzEntrada3_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada3, stateLuzEntrada3);
        }

        [Obsolete]
        private void btnLuzJardin1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzJardin1, stateLuzJardin1);
        }

        [Obsolete]
        private void btnLuzJardin2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzJardin2, stateLuzJardin2);
        }

        [Obsolete]
        private void btnLuzTerraza_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzTerraza, stateLuzTerraza);
        }

        [Obsolete]
        async void SendArduinoRequest(string url, int state)
        {
            content = await client.GetStringAsync(url);
            if (content != null)
            {
                if (url == urlLuzEntrada1 && state == 0)
                {
                    state = 1;
                    await serverClient.SignalRSendState(state);
                    stateLuzEntrada1 = state;
                }
                else if (url == urlLuzEntrada1 && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuzEntrada1 = state;
                }
                else if (url == urlLuzEntrada2 && state == 0)
                {
                    state = 1;
                    await serverClient.SignalRSendState(state);
                    stateLuzEntrada2 = state;
                }
                else if (url == urlLuzEntrada2 && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuzEntrada2 = state;
                }
                else if (url == urlLuzEntrada3 && state == 0)
                {
                    state = 1;
                    await serverClient.SignalRSendState(state);
                    stateLuzEntrada3 = state;
                }
                else if (url == urlLuzEntrada3 && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuzEntrada3 = state;
                }
                else if (url == urlLuzJardin1 && state == 0)
                {
                    state = 1;
                    await serverClient.SignalRSendState(state);
                    stateLuzJardin1 = state;
                }
                else if (url == urlLuzJardin1 && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuzJardin1 = state;
                }
                else if (url == urlLuzJardin2 && state == 0)
                {
                    state = 1;
                    await serverClient.SignalRSendState(state);
                    stateLuzJardin2 = state;
                }
                else if (url == urlLuzJardin2 && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuzJardin2 = state;
                }
                else if (url == urlLuzTerraza && state == 0)
                {
                    state = 1;
                    await serverClient.SignalRSendState(state);
                    stateLuzTerraza = state;
                }
                else if (url == urlLuzTerraza && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuzTerraza = state;
                }
            }
        }
    }
}