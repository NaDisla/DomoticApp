using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
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

        const string urlServer = "https://realtimeserver.conveyor.cloud/actionHub";
        static int estadoLogicaLuz1Entrada = 0, estadoLogicaLuz2Entrada = 0, estadoLogicaLuz3Entrada = 0,
            estadoLogicaLuz1Jardin = 0, estadoLogicaLuz2Jardin = 0, estadoLogicaLuzTerraza = 0;
        HubConnection connectHub;
        CambiarColorBotones colorButton = new CambiarColorBotones();
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlExterioresPage()
        {
            InitializeComponent();
            InitializeAction();
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
            connectHub.On<int>("ReceiveStateLuz1Entrada", (stateReceived) =>
            {
                CambiaColorLuz1Entrada(btnLuzEntrada1, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz2Entrada", (stateReceived) =>
            {
                CambiaColorLuz2Entrada(btnLuzEntrada2, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz3Entrada", (stateReceived) =>
            {
                CambiaColorLuz3Entrada(btnLuzEntrada3, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz1Jardin", (stateReceived) =>
            {
                CambiaColorLuz1Jardin(btnLuzJardin1, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz2Jardin", (stateReceived) =>
            {
                CambiaColorLuz2Jardin(btnLuzJardin2, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuzTerraza", (stateReceived) =>
            {
                CambiaColorLuzTerraza(btnLuzTerraza, stateReceived);
            });
        }

        private void CambiaColorLuz1Entrada(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz1Entrada = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz1Entrada = 0;
            }
        }

        private void CambiaColorLuz2Entrada(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz2Entrada = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz2Entrada = 0;
            }
        }

        private void CambiaColorLuz3Entrada(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz3Entrada = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz3Entrada = 0;
            }
        }

        private void CambiaColorLuz1Jardin(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz1Jardin = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz1Jardin = 0;
            }
        }

        private void CambiaColorLuz2Jardin(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz2Jardin = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz2Jardin = 0;
            }
        }

        private void CambiaColorLuzTerraza(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuzTerraza = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuzTerraza = 0;
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

        public async Task SignalRSendStateLuz1Entrada(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz1Entrada", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz2Entrada(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz2Entrada", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz3Entrada(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz3Entrada", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz1Jardin(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz1Jardin", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz2Jardin(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz2Jardin", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuzTerraza(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuzTerraza", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        void EstadoBotonLuzEntrada1()
        {
            if (estadoLogicaLuz1Entrada == 0)
            {
                colorButton.CambiarColorOFF(btnLuzEntrada1);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuzEntrada1);
            }
        }

        void EstadoBotonLuzEntrada2()
        {
            if (estadoLogicaLuz2Entrada == 0)
            {
                colorButton.CambiarColorOFF(btnLuzEntrada2);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuzEntrada2);
            }
        }

        void EstadoBotonLuzEntrada3()
        {
            if (estadoLogicaLuz3Entrada == 0)
            {
                colorButton.CambiarColorOFF(btnLuzEntrada3);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuzEntrada3);
            }
        }

        void EstadoBotonLuzJardin1()
        {
            if (estadoLogicaLuz1Jardin == 0)
            {
                colorButton.CambiarColorOFF(btnLuzJardin1);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuzJardin1);
            }
        }

        void EstadoBotonLuzJardin2()
        {
            if (estadoLogicaLuz2Jardin == 0)
            {
                colorButton.CambiarColorOFF(btnLuzJardin2);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuzJardin2);
            }
        }

        void EstadoBotonLuzTerraza()
        {
            if (estadoLogicaLuzTerraza == 0)
            {
                colorButton.CambiarColorOFF(btnLuzTerraza);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuzTerraza);
            }
        }

        [Obsolete]
#pragma warning disable CS0809
        protected override void OnAppearing()
#pragma warning restore CS0809
        {
            base.OnAppearing();
            EstadoBotonLuzEntrada1();
            EstadoBotonLuzEntrada2();
            EstadoBotonLuzEntrada3();
            EstadoBotonLuzJardin1();
            EstadoBotonLuzJardin2();
            EstadoBotonLuzTerraza();
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
        private async void btnLuzEntrada1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada1, stateLuzEntrada1);
            await SignalRSendStateLuz1Entrada(estadoLogicaLuz1Entrada);
        }

        [Obsolete]
        private async void btnLuzEntrada2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada2, stateLuzEntrada2);
            await SignalRSendStateLuz2Entrada(estadoLogicaLuz2Entrada);
        }

        [Obsolete]
        private async void btnLuzEntrada3_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada3, stateLuzEntrada3);
            await SignalRSendStateLuz3Entrada(estadoLogicaLuz3Entrada);
        }

        [Obsolete]
        private async void btnLuzJardin1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzJardin1, stateLuzJardin1);
            await SignalRSendStateLuz1Jardin(estadoLogicaLuz1Jardin);
        }

        [Obsolete]
        private async void btnLuzJardin2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzJardin2, stateLuzJardin2);
            await SignalRSendStateLuz2Jardin(estadoLogicaLuz2Jardin);
        }

        [Obsolete]
        private async void btnLuzTerraza_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzTerraza, stateLuzTerraza);
            await SignalRSendStateLuzTerraza(estadoLogicaLuzTerraza);
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
                    stateLuzEntrada1 = state;
                }
                else if (url == urlLuzEntrada1 && state == 1)
                {
                    state = 0;
                    stateLuzEntrada1 = state;
                }
                else if (url == urlLuzEntrada2 && state == 0)
                {
                    state = 1;
                    stateLuzEntrada2 = state;
                }
                else if (url == urlLuzEntrada2 && state == 1)
                {
                    state = 0;
                    stateLuzEntrada2 = state;
                }
                else if (url == urlLuzEntrada3 && state == 0)
                {
                    state = 1;
                    stateLuzEntrada3 = state;
                }
                else if (url == urlLuzEntrada3 && state == 1)
                {
                    state = 0;
                    stateLuzEntrada3 = state;
                }
                else if (url == urlLuzJardin1 && state == 0)
                {
                    state = 1;
                    stateLuzJardin1 = state;
                }
                else if (url == urlLuzJardin1 && state == 1)
                {
                    state = 0;
                    stateLuzJardin1 = state;
                }
                else if (url == urlLuzJardin2 && state == 0)
                {
                    state = 1;
                    stateLuzJardin2 = state;
                }
                else if (url == urlLuzJardin2 && state == 1)
                {
                    state = 0;
                    stateLuzJardin2 = state;
                }
                else if (url == urlLuzTerraza && state == 0)
                {
                    state = 1;
                    stateLuzTerraza = state;
                }
                else if (url == urlLuzTerraza && state == 1)
                {
                    state = 0;
                    stateLuzTerraza = state;
                }
            }
        }
    }
}