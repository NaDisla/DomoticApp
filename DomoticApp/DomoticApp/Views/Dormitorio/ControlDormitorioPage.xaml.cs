using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Dormitorio
{
    public partial class ControlDormitorioPage : ContentPage
    {
        public static int stateLuz1 = 0, stateLuz2 = 0, stateAbanico = 0, stateGeneral = 0;
        private const string urlGeneral= "http://10.0.0.17";
        private const string urlLuz1 = "http://10.0.0.17/luz-dormitorio-1";
        private const string urlLuz2 = "http://10.0.0.17/luz-dormitorio-2";
        private const string urlAbanico = "http://10.0.0.17/abanico-dormitorio";
        private readonly HttpClient client = new HttpClient();
        private string content, titleError, detailError;
        
        SignalRClient serverClient;
        ResultsOperations results = new ResultsOperations();

        [Obsolete]
        public ControlDormitorioPage()
        {
            InitializeComponent();
            //TempHum();

            /*if (btnLuz1.IsPressed == false)
                serverClient = new SignalRClient(btnLuz1);
            else if (btnLuz2.IsPressed == false)
                serverClient = new SignalRClient(btnLuz2);
            else if (btnAbanico.IsPressed == false)
                serverClient = new SignalRClient(btnAbanico);*/

            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        [Obsolete]
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
                titleError = "Error de conexión";
                detailError = "No se ha podido establecer la conexión con la vivienda.";
                await results.Unsuccess(titleError, detailError);
            }
            base.OnAppearing();
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
        async void SendArduinoRequest(string contentUrl, int state)
        {
            content = await client.GetStringAsync(contentUrl);
            if (content != null)
            {
                if (contentUrl == urlAbanico && state == 0)
                {
                    //await serverClient.SignalRSendState(state);
                    state = 1;
                    stateAbanico = state;
                }
                else if (contentUrl == urlAbanico && state == 1)
                {
                    //await serverClient.SignalRSendState(state);
                    state = 0;
                    stateAbanico = state;
                }
                if (contentUrl == urlLuz1 && state == 0)
                {
                    //await serverClient.SignalRSendState(state);
                    state = 1;
                    stateLuz1 = state;
                }
                else if (contentUrl == urlLuz1 && state == 1)
                {
                    //await serverClient.SignalRSendState(state);
                    state = 0;
                    stateLuz1 = state;
                }
                if (contentUrl == urlLuz2 && state == 0)
                {
                    //await serverClient.SignalRSendState(state);
                    state = 1;
                    stateLuz2 = state;
                }
                else if (contentUrl == urlLuz2 && state == 1)
                {
                    //await serverClient.SignalRSendState(state);
                    state = 0;
                    stateLuz2 = state;
                }
            }
            else
            {
                titleError = "Error de conexión";
                detailError = "No se ha podido establecer la conexión con la vivienda.";
                await results.Unsuccess(titleError, detailError);
            }
        }
    }
}