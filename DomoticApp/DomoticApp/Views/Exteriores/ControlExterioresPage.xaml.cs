using DomoticApp.DataHelpers;
using DomoticApp.SettingsAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Exteriores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlExterioresPage : ContentPage
    {

        public static int stateLuzEntrada1 = 0, stateLuzEntrada2 = 0, stateLuzEntrada3 = 0, stateLuzJardin1 = 0, stateLuzJardin2 = 0,
            stateLuzTerraza = 0;
        private const string urlLuzEntrada1 = "http://10.0.0.17/luz-entrada", urlLuzEntrada2 = "http://10.0.0.17/luz-frente-1",
            urlLuzEntrada3 = "http://10.0.0.17/luz-frente-2", urlLuzJardin1 = "http://10.0.0.17/luz-jardin-1", 
            urlLuzJardin2 = "http://10.0.0.17/luz-jardin-2", urlLuzTerraza = "http://10.0.0.17/luz-terraza-sala";
        private readonly HttpClient client = new HttpClient();
        private string content, titleError, detailError;

        SignalRClient serverClient;
        ResultsOperations results = new ResultsOperations();

        public ControlExterioresPage()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void btnLuzEntrada1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada1, stateLuzEntrada1);
        }

        [Obsolete]
        private void btnLuzEntrada2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada2, stateLuzEntrada2);
        }

        [Obsolete]
        private void btnLuzEntrada3_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzEntrada3, stateLuzEntrada3);
        }

        [Obsolete]
        private void btnLuzJardin1_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzJardin1, stateLuzJardin1);
        }

        [Obsolete]
        private void btnLuzJardin2_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzJardin2, stateLuzJardin2);
        }

        [Obsolete]
        private void btnLuzTerraza_Clicked(object sender, EventArgs e)
        {
            SendArduinoRequest(urlLuzTerraza, stateLuzTerraza);
        }

        [Obsolete]
        async void SendArduinoRequest(string url, int state)
        {
            content = await client.GetStringAsync(url);
            if (content != null)
            {
                if (url == urlLuzEntrada1 && state == 0)
                {
                    state = 1;
                    stateLuzEntrada1 = state;
                }
                else if (url == urlLuzEntrada1 && state == 1)
                {
                    state = 0;
                    stateLuzEntrada1 = state;
                }
                else if (url == urlLuzEntrada2 && state == 0)
                {
                    state = 1;
                    stateLuzEntrada2 = state;
                }
                else if (url == urlLuzEntrada2 && state == 1)
                {
                    state = 0;
                    stateLuzEntrada2 = state;
                }
                else if (url == urlLuzEntrada3 && state == 0)
                {
                    state = 1;
                    stateLuzEntrada3 = state;
                }
                else if (url == urlLuzEntrada3 && state == 1)
                {
                    state = 0;
                    stateLuzEntrada3 = state;
                }
                else if (url == urlLuzJardin1 && state == 0)
                {
                    state = 1;
                    stateLuzJardin1 = state;
                }
                else if (url == urlLuzJardin1 && state == 1)
                {
                    state = 0;
                    stateLuzJardin1 = state;
                }
                else if (url == urlLuzJardin2 && state == 0)
                {
                    state = 1;
                    stateLuzJardin2 = state;
                }
                else if (url == urlLuzJardin2 && state == 1)
                {
                    state = 0;
                    stateLuzJardin2 = state;
                }
                else if (url == urlLuzTerraza && state == 0)
                {
                    state = 1;
                    stateLuzTerraza = state;
                }
                else if (url == urlLuzTerraza && state == 1)
                {
                    state = 0;
                    stateLuzTerraza = state;
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