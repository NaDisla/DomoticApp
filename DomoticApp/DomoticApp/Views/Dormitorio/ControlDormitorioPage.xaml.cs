using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
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
        public int stateLuz1 = 0, stateLuz2 = 0, stateAbanico = 0;
        private const string urlGeneral= "http://10.0.0.17";
        private const string urlLuz1 = "http://10.0.0.17/luz-dormitorio-1";
        private const string urlLuz2 = "http://10.0.0.17/luz-dormitorio-2";
        private const string urlAbanico = "http://10.0.0.17/abanico-dormitorio";
        private readonly HttpClient client = new HttpClient();
        private string content;
        
        SignalRClient serverClient;
        
        public ControlDormitorioPage()
        {
            InitializeComponent();

            if (btnLuz1.IsPressed == false)
                serverClient = new SignalRClient(btnLuz1);
            else if (btnLuz2.IsPressed == false)
                serverClient = new SignalRClient(btnLuz2);
            else if (btnAbanico.IsPressed == false)
                serverClient = new SignalRClient(btnAbanico);

            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }
        /*private async void InitializeAction()
        {
            SetupAction();
            await SignalRConnect();
        }*/
        /*private void SetupAction()
        {
            connectHub = new HubConnectionBuilder().WithUrl("http://10.0.0.5:45455/actionHub").Build();
            connectHub.On<int>("ReceiveState", (stateReceived) =>
            {
                CambiaColor(btnLuces, stateReceived);
            });
        }*/
        /*async Task SignalRConnect()
        {
            try
            {
                await connectHub.StartAsync();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error estableciendo conexión", e.Message.ToString(), "OK");
            }
        }*/
        protected async override void OnAppearing()
        {
            content = await client.GetStringAsync(urlGeneral);
            var cortando = content.Split(';');
            string temperatura = cortando[0];
            string humedad = cortando[1];
            if (content != null)
            {
                lblTemp.Text = temperatura + "°C";
                lblHum.Text = humedad + "%";
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
            base.OnAppearing();
        }
        /*async Task SignalRSendState(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendState", state);
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message.ToString(), "OK");
            }
        }*/
        private void btnAbanico_Clicked(object sender, EventArgs e)
        {
            Control(urlAbanico, stateAbanico);
        }
        private void btnLuz1_Clicked(object sender, EventArgs e)
        {
            Control(urlLuz1, stateLuz1);
        }
        private void btnLuz2_Clicked(object sender, EventArgs e)
        {
            Control(urlLuz2, stateLuz2);
        }
        async void Control(string contentUrl, int state)
        {
            content = await client.GetStringAsync(contentUrl);
            if (content != null)
            {
                if (state == 0)
                {
                    await serverClient.SignalRSendState(state);
                    state = 1;
                    MessagingCenter.Send<object, int>(this, "State", state);
                }
                else
                {
                    await serverClient.SignalRSendState(state);
                    state = 0;
                }
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
        }
        /*void CambiaColor(Button btn, int stateButton)
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
        }*/
    }
}