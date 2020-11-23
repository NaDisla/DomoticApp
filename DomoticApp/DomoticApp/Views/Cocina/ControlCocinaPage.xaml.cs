using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using DomoticApp.Views.SmartFridge;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Cocina
{
    public partial class ControlCocinaPage : ContentPage
    {
        public int estado = 0;
        private const string urlLuz1 = "http://10.0.0.17/cocina-recibidor", urlLuz2 = "http://10.0.0.17/sala-cocina";
        private readonly HttpClient client = new HttpClient();
        private string content;
        SignalRClient serverClient;
        public int stateButtonClicked = 0;

        public ControlCocinaPage()
        {
            InitializeComponent();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        async void SendArduinoRequest(string url)
        {
            content = await client.GetStringAsync(url);
            if (content != null)
            {
                if (stateButtonClicked == 0)
                {
                    await serverClient.SignalRSendState(stateButtonClicked);
                    stateButtonClicked = 1;
                    MessagingCenter.Send<object, int>(this, "State", stateButtonClicked);
                }
                else
                {
                    await serverClient.SignalRSendState(stateButtonClicked);
                    stateButtonClicked = 0;
                }
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
        }

        private void btnLuz1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz1);
        }

        private void btnLuz2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz2);
        }

        private async void btnNevera_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FridgeSimulatorPage());
        }
    }
}