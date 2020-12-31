using DomoticApp.DataHelpers;
using DomoticApp.Views.MasterMenu;
using DomoticApp.Views.Monitoreo;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Bath
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlBathPage : ContentPage
    {
        public static int stateLuz = 0;
        private const string urlLuz = "http://10.0.0.17/luz-bath";
        private readonly HttpClient client = new HttpClient();
        private string content, titleError, detailError;

        SignalRClient serverClient;
        ResultsOperations results = new ResultsOperations();
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlBathPage()
        {
            InitializeComponent();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnAppearing()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnDisappearing()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
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
        private void btnLuz_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuz, stateLuz);
        }

        [Obsolete]
        async void SendArduinoRequest(string url, int state)
        {
            content = await client.GetStringAsync(url);
            if(content != null)
            {
                if(url == urlLuz && state == 0)
                {
                    state = 1;
                    stateLuz = state;
                }
                else if (url == urlLuz && state == 1)
                {
                    state = 0;
                    stateLuz = state;
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