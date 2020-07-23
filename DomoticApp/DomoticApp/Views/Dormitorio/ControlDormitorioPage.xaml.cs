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
        public int estado = 0;
        private const string urlEncenderLed1 = "http://10.0.0.17/D";
        private const string urlApagarLed1 = "http://10.0.0.17/O";
        private const string urlEncenderAbanico = "http://10.0.0.17/V";
        private const string urlApagarAbanico = "http://10.0.0.17/E";
        private readonly HttpClient client = new HttpClient();
        private string content;

        public ControlDormitorioPage()
        {
            InitializeComponent();
        }
        private async void btnLuces_Clicked(object sender, EventArgs e)
        {
            if (estado == 0)
            {
                content = await client.GetStringAsync(urlEncenderLed1);
                if (content != null)
                {
                    btnLuces.BackgroundColor = Color.FromHex("#739DB8");
                    btnLuces.TextColor = Color.White;
                }
                else
                    await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
                estado = 1; 
            }
            else
            {
                content = await client.GetStringAsync(urlApagarLed1);
                if (content != null)
                {
                    await DisplayAlert("PRUEBA", "Probando ", "OK");
                    btnLuces.BackgroundColor = Color.AliceBlue;
                    btnLuces.TextColor = Color.FromHex("#166498");
                }
                else
                    await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
                estado = 0;
            }        
        }

        private void btnAbanico_Clicked(object sender, EventArgs e)
        {
            /*if (contador == 1)
            {
                content = await client.GetStringAsync(urlEncenderAbanico);
                if (content != null)
                    await DisplayAlert("Aviso", "Abanico encendido. ", "OK");
                else
                    await DisplayAlert("Error de conexión", "No se ha podido establecer la " +
                        "conexión. ", "OK");
                contador2 = 0;
            }
            else
            {
                content = await client.GetStringAsync(urlApagarAbanico);
                if (content != null)
                    await DisplayAlert("Aviso", "Abanico apagado. ", "OK");
                else
                    await DisplayAlert("Error de conexión", "No se ha podido establecer " +
                        "la conexión. ", "OK");
                contador2 = 1;
            }*/
        }

        private void btnTelevision_Clicked(object sender, EventArgs e)
        {

        }
    }
}