using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
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
        private string content, titleError, detailError;
        string humedad;
        int aguaNivel;
        SignalRClient serverClient;
        ResultsOperations results = new ResultsOperations();
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlLavadoPage()
        {
            InitializeComponent();
            //if (btnLuz.IsPressed == false)
            //{
            //    serverClient = new SignalRClient(btnLuz);
            //}
            GetNivel();
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
        private void btnLuz_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz, stateLuz);
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
                    await serverClient.SignalRSendState(state);
                    stateLuz = state;
                }
                else if (url == urlLuz && state == 1)
                {
                    state = 0;
                    await serverClient.SignalRSendState(state);
                    stateLuz = state;
                }
            }
        }
    }
}