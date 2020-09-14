using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Dormitorio
{
    public partial class ControlDormitorioPage : ContentPage
    {
        HubConnection connectHub;
        public int stateButtonClicked = 0;
        public string Temperatura { get; set; }
        public string Humedad { get; set; }
        private const string urlTarjeta = "http://10.0.0.17";
        private const string urlLuz1 = "http://10.0.0.17/dormitorio-izquierda";
        //private const string urlLuz2 = "http://10.0.0.17/D";
        private const string urlEncenderAbanico = "http://10.0.0.17/A";
        private readonly HttpClient client = new HttpClient();
        private string content;
        public string estadoDormitorio;
        
        public ControlDormitorioPage()
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
            connectHub = new HubConnectionBuilder().WithUrl("http://10.0.0.5:45457/actionHub").Build();
            connectHub.On<int>("ReceiveState", (stateReceived) =>
            {
                CambiaColor(btnLuces, stateReceived);
            });
        }
        async Task SignalRConnect()
        {
            try
            {
                await connectHub.StartAsync();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error estableciendo conexión", e.Message.ToString(), "OK");
            }
        }
        protected async override void OnAppearing()
        {
            content = await client.GetStringAsync(urlTarjeta);
            var cortando = content.Split(';');
            string temperatura = cortando[0];
            string humedad = cortando[1];
            estadoDormitorio = cortando[2];
            if (content != null)
            {
                Temperatura = temperatura + "°C";
                Humedad = humedad + "%";
                lblTemp.Text = Temperatura;
                lblHum.Text = Humedad;
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
            base.OnAppearing();
        }
        public async void btnLuces_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlLuz1);
            if (content != null)
            {
                await SignalRSendState(stateButtonClicked);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
        }
        async Task SignalRSendState(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendState", state);
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message.ToString(), "OK");
            }
        }
        private async void btnAbanico_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlEncenderAbanico);
            if (content != null)
            {
                await SignalRSendState(stateButtonClicked);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
            
        }

        private void btnTelevision_Clicked(object sender, EventArgs e)
        {

        }

        void CambiaColor(Button btn, int stateButton)
        {
            if(stateButton == 0)
            {
                btn.BackgroundColor = Color.FromHex("#739DB8");
                btn.TextColor = Color.White;
                stateButtonClicked = 1;
            }
            else
            {
                btn.BackgroundColor = Color.AliceBlue;
                btn.TextColor = Color.FromHex("#166498");
                stateButtonClicked = 0;
            }
        }
    }
}