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
        const string urlServer = "https://realtimeserver.conveyor.cloud/actionHub";
        Button receiveButton;

        public SignalRClient(Button btnReceived)
        {
            receiveButton = btnReceived;
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
                 CambiaColor(receiveButton, stateReceived);
             });
        }
       
        private void CambiaColor(Button button, int stateButton)
        {
            if (stateButton == 1)
            {
                button.BackgroundColor = Color.FromHex("#739DB8");
                button.TextColor = Color.White;
            }
            else
            {
                button.BackgroundColor = Color.FromHex("#b9d9f0");
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
