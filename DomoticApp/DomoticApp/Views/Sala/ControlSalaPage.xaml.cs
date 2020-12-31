using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using System;
using System.Net.Http;
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

        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlSalaPage()
        {
            InitializeComponent();
            DatosTermicos();
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

        async void DatosTermicos()
        {
            content = await client.GetStringAsync(urlGeneral);
            var cortando = content.Split(';');
            string temperatura = cortando[3];
            string humedad = cortando[4];
            if (content != null)
            {
                lblTemp.Text = temperatura + "°C";
                lblHum.Text = humedad + "%";
            }
        }

        [Obsolete]
        private void btnAbanico_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlAbanico, stateAbanico);
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
