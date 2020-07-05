using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Lavado
{
    public partial class ControlLavadoPage : ContentPage
    {
        public int estado = 1;
        private const string urlEncenderLed4 = "http://10.0.0.17/R";
        private const string urlApagarLed4 = "http://10.0.0.17/V";
        private readonly HttpClient client = new HttpClient();
        private string content;

        public ControlLavadoPage()
        {
            InitializeComponent();
        }

        private void btnLavadora_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnLuces_Clicked(object sender, EventArgs e)
        {
            if (estado == 1)
            {
                content = await client.GetStringAsync(urlEncenderLed4);
                if (content != null)
                    await DisplayAlert("Aviso", "Luces encendidas. ", "OK");
                else
                    await DisplayAlert("Error de conexión", "No se ha podido establecer la " +
                        "conexión. ", "OK");
                estado = 0;
            }
            else
            {
                content = await client.GetStringAsync(urlApagarLed4);
                if (content != null)
                    await DisplayAlert("Aviso", "Luces apagadas. ", "OK");
                else
                    await DisplayAlert("Error de conexión", "No se ha podido establecer " +
                        "la conexión. ", "OK");
                estado = 1;
            }
        }
    }
}