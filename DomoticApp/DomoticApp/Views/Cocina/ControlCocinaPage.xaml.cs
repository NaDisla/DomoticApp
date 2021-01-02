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
        private string content;
        SignalRClient serverClient;
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlCocinaPage()
        {
            InitializeComponent();
            
            //if (btnLuz1.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuz1);
            //}
            //else if(btnLuz2.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuz2);
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
            if(LayoutNevera.IsVisible == false)
            {
                btnNevera.BackgroundColor = Color.FromHex("#739DB8");
                btnNevera.TextColor = Color.White;
                LayoutNevera.IsVisible = true;
            }
            else
            {
                btnNevera.BackgroundColor = Color.AliceBlue;
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
                    await serverClient.SignalRSendState(state);
                    stateLuz1 = state;
                }
                else if (url == urlLuz1 && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuz1 = state;
                }
                else if(url == urlLuz2 && state == 0)
                {
                    state = 1;
                    await serverClient.SignalRSendState(state);
                    stateLuz2 = state;
                }
                else if(url == urlLuz2 && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuz2 = state;
                }
            }
        }
    }
}