using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DomoticApp.DataHelpers
{
    public class SignalRClient
    {
        HubConnection connectHub;
        const string urlServer = "http://10.0.0.5:45455/actionHub";
        Button buttonReceived;

        public SignalRClient(Button btnReceived)
        {
            buttonReceived = btnReceived;
            InitializeAction();
        }

        private async void InitializeAction()
        {
            SetupAction();
            await SignalRConnect();
        }

        private void SetupAction()
        {
            connectHub = new HubConnectionBuilder().WithUrl(urlServer).Build();
            connectHub.On<int>("ReceiveState", (stateReceived) =>
             {
                 CambiaColor(buttonReceived, stateReceived);   
             });
        }
       
        private void CambiaColor(Button button, int stateButton)
        {
            if (stateButton == 0)
            {
                button.BackgroundColor = Color.FromHex("#739DB8");
                button.TextColor = Color.White;
            }
            else
            {
                button.BackgroundColor = Color.AliceBlue;
                button.TextColor = Color.FromHex("#166498");
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

        public async Task SignalRSendState(int state)
        {
            try
            {
                await connectHub.InvokeAsync("SendState", state);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
