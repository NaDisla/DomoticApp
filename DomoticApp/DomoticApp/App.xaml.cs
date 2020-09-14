using DomoticApp.Views.MasterMenu;
using Plugin.SharedTransitions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DomoticApp;
using Rg.Plugins.Popup.Services;
using DomoticApp.Views.Popups;
using Plugin.Connectivity;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.InteropServices;
using DomoticApp.Views.SmartFridge;
using DomoticApp.Views.Dormitorio;

namespace DomoticApp
{
    public partial class App : Application
    {
        private const string urlTarjeta = "http://10.0.0.17";
        LoadingNetworkPage loadingRed;
        CorrectNetworkPage redCorrecta; 
        IncorrectNetworkPage redIncorrecta;
        AlertNetworkPage alertaVPN;
        private string content;
        private readonly HttpClient client = new HttpClient();

        [Obsolete]
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MasterMenuPage());
            if (CrossConnectivity.Current.IsConnected)
                ValidandoRedes();
            else
                RedIncorrecta();
        }
        [Obsolete]
        async void ValidandoRedes()
        {
            try
            {
                content = await client.GetStringAsync(urlTarjeta);
                if (content != null)
                {
                    RedCorrecta();
                }
            }
            catch (Exception)
            {
                AlertaVPN();
            }
        }
        [Obsolete]
        async void RedCorrecta()
        {
            await PopupNavigation.PushAsync(loadingRed = new LoadingNetworkPage());
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingRed);
            await PopupNavigation.PushAsync(redCorrecta = new CorrectNetworkPage());
        }

        [Obsolete]
        async void RedIncorrecta()
        {
            await PopupNavigation.PushAsync(loadingRed = new LoadingNetworkPage());
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingRed);
            await PopupNavigation.PushAsync(redIncorrecta = new IncorrectNetworkPage());
        }

        [Obsolete]
        async void AlertaVPN()
        {
            await PopupNavigation.PushAsync(loadingRed = new LoadingNetworkPage());
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingRed);
            await PopupNavigation.PushAsync(alertaVPN = new AlertNetworkPage());
        }
        protected override  void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
