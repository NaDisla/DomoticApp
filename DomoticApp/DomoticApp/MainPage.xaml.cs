using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DomoticApp.Views.Popups;
using System.Net.Http;
using Rg.Plugins.Popup.Services;
using DomoticApp.Views.Dormitorio;
using DomoticApp.Views.Sala;
using DomoticApp.Views.Cocina;
using DomoticApp.Views.Lavado;
using Xamarin.Forms.PlatformConfiguration;

namespace DomoticApp
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private const string urlTarjeta = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        private string content;
        LoadingNetworkPage loadingRed = new LoadingNetworkPage();
        CorrectNetworkPage redCorrecta = new CorrectNetworkPage();
        IncorrectNetworkPage redIncorrecta = new IncorrectNetworkPage();
        ControlDormitorioPage dormitorio = new ControlDormitorioPage();
        ControlCocinaPage cocina = new ControlCocinaPage();
        ControlLavadoPage lavado = new ControlLavadoPage();
        ControlSalaPage sala = new ControlSalaPage();

        [Obsolete]
        public MainPage()
        {
            InitializeComponent();
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
                RedIncorrecta();
            }
        }

        [Obsolete]
        async void RedCorrecta()
        {
            await PopupNavigation.PushAsync(loadingRed);
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingRed);
            await PopupNavigation.PushAsync(redCorrecta);
            await Task.Delay(2000);
        }

        [Obsolete]
        async void RedIncorrecta()
        {
            await PopupNavigation.PushAsync(loadingRed);
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingRed);
            await PopupNavigation.PushAsync(redIncorrecta);
        }

        private void btnDormitorio_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(dormitorio));
        }

        private void btnCocina_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(cocina));
        }

        private void btnAreaLavado_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(lavado));
        }

        private void btnSala_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(sala));
        }

        private void btnBano_Clicked(object sender, EventArgs e)
        {

        }

        private void btnRecibidor_Clicked(object sender, EventArgs e)
        {

        }

        private void btnPiscina_Clicked(object sender, EventArgs e)
        {

        }

        private void btnTinaco_Clicked(object sender, EventArgs e)
        {

        }

        private void btnExteriores_Clicked(object sender, EventArgs e)
        {

        }
    }
}
