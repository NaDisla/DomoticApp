﻿using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Recibidor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlRecibidorPage : ContentPage
    {
        public static int stateLuz1 = 0, stateLuz2 = 0, stateLuz3 = 0;
        private const string urlLuz1 = "http://10.0.0.17/luz-recibidor-1", urlLuz2 = "http://10.0.0.17/luz-recibidor-2",
            urlLuz3 = "http://10.0.0.17/luz-recibidor-3";
        private readonly HttpClient client = new HttpClient();
        private string content;
        const string urlServer = "https://realtimeserver.conveyor.cloud/actionHub";
        static int estadoLogicaLuz1 = 0, estadoLogicaLuz2 = 0, estadoLogicaLuz3 = 0;
        HubConnection connectHub;
        CambiarColorBotones colorButton = new CambiarColorBotones();
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlRecibidorPage()
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
            connectHub.On<int>("ReceiveStateLuz1Recibidor", (stateReceived) =>
            {
                CambiaColorLuz1(btnLuz1, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz2Recibidor", (stateReceived) =>
            {
                CambiaColorLuz2(btnLuz2, stateReceived);
            });
            connectHub.On<int>("ReceiveStateLuz3Recibidor", (stateReceived) =>
            {
                CambiaColorLuz3(btnLuz3, stateReceived);
            });
        }

        private void CambiaColorLuz1(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz1 = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz1 = 0;
            }
        }

        private void CambiaColorLuz2(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz2 = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz2 = 0;
            }
        }

        private void CambiaColorLuz3(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                colorButton.CambiarColorLucesON(button);
                estadoLogicaLuz3 = 1;
            }
            else
            {
                colorButton.CambiarColorOFF(button);
                estadoLogicaLuz3 = 0;
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

        public async Task SignalRSendStateLuz1Recibidor(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz1Recibidor", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz2Recibidor(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz2Recibidor", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignalRSendStateLuz3Recibidor(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendStateLuz3Recibidor", state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        void EstadoBotonLuz1()
        {
            if (estadoLogicaLuz1 == 0)
            {
                colorButton.CambiarColorOFF(btnLuz1);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuz1);
            }
        }

        void EstadoBotonLuz2()
        {
            if (estadoLogicaLuz2 == 0)
            {
                colorButton.CambiarColorOFF(btnLuz2);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuz2);
            }
        }

        void EstadoBotonLuz3()
        {
            if (estadoLogicaLuz3 == 0)
            {
                colorButton.CambiarColorOFF(btnLuz3);
            }
            else
            {
                colorButton.CambiarColorLucesON(btnLuz3);
            }
        }

        [Obsolete]
#pragma warning disable CS0809
        protected override void OnAppearing()
#pragma warning restore CS0809
        {
            base.OnAppearing();
            EstadoBotonLuz1();
            EstadoBotonLuz2();
            EstadoBotonLuz3();
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
        private async void btnLuz1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz1, stateLuz1);
            await SignalRSendStateLuz1Recibidor(estadoLogicaLuz1);
        }

        [Obsolete]
        private async void btnLuz2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz2, stateLuz2);
            await SignalRSendStateLuz2Recibidor(estadoLogicaLuz2);
        }

        [Obsolete]
        private async void btnLuz3_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz3, stateLuz3);
            await SignalRSendStateLuz3Recibidor(estadoLogicaLuz3);
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
                else if (url == urlLuz3 && state == 0)
                {
                    state = 1;
                    stateLuz3 = state;
                }
                else if (url == urlLuz3 && state == 1)
                {
                    state = 0;
                    stateLuz3 = state;
                }
            }
        }
    }
}