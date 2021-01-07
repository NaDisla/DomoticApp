using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Bath
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlBathPage : ContentPage
    {
        public static int stateLuz = 0;
        private const string urlLuz = "http://10.0.0.17/luz-bath";
        const string urlServer = "https://realtimeserver.conveyor.cloud/actionHub";
        static int estadoLogicaLuz = 0;
        HubConnection connectHub;
        private readonly HttpClient client = new HttpClient();
        private string content;

        CambiarColorBotones colorButton = new CambiarColorBotones();
        ValidarCambioRed cambioRed = new ValidarCambioRed();
        GeneralData data = new GeneralData();

        public ControlBathPage()
        {
            InitializeComponent();
            InitializeAction();
            EstadoBotonLuz();
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
            connectHub.On<int>("ReceiveStateLuzBath", (stateReceived) =>
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

        public async Task SignalRSendStateLuzBath(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuzBath", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async void EstadoBotonLuz()
        {
            var getEstado = await data.GetEstadoBath();
            var luz = getEstado.Where(x => x.Luz == 0 || x.Luz == 1).Select(y => y.Luz).FirstOrDefault();

            if (luz == 0)
            {
                colorButton.CambiarColorOFF(btnLuz);
                estadoLogicaLuz = 0;
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuz);
                estadoLogicaLuz = 1;
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
        private async void btnLuz_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz, stateLuz);
            await SignalRSendStateLuzBath(estadoLogicaLuz);
            await data.UpdateEstadoBath("B01", estadoLogicaLuz);
        }

        [Obsolete]
        async void SendArduinoRequest(string url, int state)
        {
            content = await client.GetStringAsync(url);
            if(content != null)
            {
                if(url == urlLuz && state == 0)
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
    }
}