using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Cocina
{
    public partial class ControlCocinaPage : ContentPage
    {
        public int contador = 1;
        private const string urlEncenderLed3 = "http://10.0.0.17/B";
        private const string urlApagarLed3 = "http://10.0.0.17/N";
        private readonly HttpClient client = new HttpClient();
        private string content;
        private string content2;

        public ControlCocinaPage()
        {
            InitializeComponent();
        }

        private async void btnLuces_Clicked(object sender, EventArgs e)
        {
            content2 = await client.GetStringAsync(urlApagarLed3);
            if (contador == 1)
            {
                content2 = await client.GetStringAsync(urlEncenderLed3);
                if (content2 != null)
                {
                    await DisplayAlert("Aviso", "Luces encendidas. ", "OK");
                }
                else
                {
                    await DisplayAlert("Error de conexión", "No se ha podido establecer " +
                        "la conexión. ", "OK");
                }
                    
                contador = 0;
            }
            //else if (contador == 1 && content2 != null)
            //{
            //    content2 = await client.GetStringAsync(urlApagarLed3);
            //    if (content2 != null)
            //    {
            //        await DisplayAlert("Aviso", "Luces apagadas. ", "OK");
            //    }

            //    else
            //    {
            //        await DisplayAlert("Error de conexión", "No se ha podido establecer " +
            //            "la conexión. ", "OK");
            //    }
            //    contador = 0;
            //}
            else
            {
                content = await client.GetStringAsync(urlApagarLed3);
                if (content != null)
                {
                    await DisplayAlert("Aviso", "Luces apagadas. ", "OK");
                }
                    
                else
                {
                    await DisplayAlert("Error de conexión", "No se ha podido establecer " +
                        "la conexión. ", "OK");
                }
                    
                contador = 1;
            }
        }
    }
}