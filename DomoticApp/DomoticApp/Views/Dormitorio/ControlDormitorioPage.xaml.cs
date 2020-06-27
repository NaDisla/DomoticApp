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
        public int contador = 1;
        private const string urlEncenderLed1 = "http://10.0.0.17/H";
        private const string urlApagarLed1 = "http://10.0.0.17/L";
        private readonly HttpClient client = new HttpClient();
        private string content;
        public ControlDormitorioPage()
        {
            InitializeComponent();
        }

        private async void btnLuces_Clicked(object sender, EventArgs e)
        {
            if(contador == 1)
            {
                content = await client.GetStringAsync(urlEncenderLed1);
                if (content != null)
                    await DisplayAlert("Aviso", "Luces encendidas. ", "OK");
                else
                    await DisplayAlert("Error de conexión", "No se ha podido establecer la " +
                        "conexión. ", "OK");
                contador = 0;
            }
            else
            {
                content = await client.GetStringAsync(urlApagarLed1);
                if (content != null)
                    await DisplayAlert("Aviso", "Luces apagadas. ", "OK");
                else
                    await DisplayAlert("Error de conexión", "No se ha podido establecer " +
                        "la conexión. ", "OK");
                contador = 1;
            }
        }

        private void btnAbanico_Clicked(object sender, EventArgs e)
        {

        }

        private void btnTelevision_Clicked(object sender, EventArgs e)
        {

        }
    }
}