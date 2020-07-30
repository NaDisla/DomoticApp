using DomoticApp.Views.Dormitorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Sala
{
    public partial class ControlSalaPage : ContentPage
    {
        public int estado = 0;
        private const string urlEncenderLuz = "http://10.0.0.17/S";
        private readonly HttpClient client = new HttpClient();
        private string content;

        public ControlSalaPage()
        {
            InitializeComponent();
        }

        private async void btnLuces_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlEncenderLuz);
            if (content != null)
            {
                CambiaColor(btnLuces);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
        }

        private void btnAbanico_Clicked(object sender, EventArgs e)
        {

        }

        private void btnTelevision_Clicked(object sender, EventArgs e)
        {

        }

        void CambiaColor(Button btn)
        {
            if (estado == 0)
            {
                btn.BackgroundColor = Color.FromHex("#739DB8");
                btn.TextColor = Color.White;
                estado = 1;
            }
            else
            {
                btn.BackgroundColor = Color.AliceBlue;
                btn.TextColor = Color.FromHex("#166498");
                estado = 0;
            }
        }
    }
}
