using DomoticApp.Views.Dormitorio;
using DomoticApp.Views.Monitoreo;
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
        private const string urlLuz1 = "http://10.0.0.17/luz-sala-1";
        private const string urlLuz2 = "http://10.0.0.17/luz-sala-2";
        private const string urlLuz3 = "http://10.0.0.17/luz-sala-3";
        private const string urlAbanico = "http://10.0.0.17/abanico-sala";
        private const string urlGeneral = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        private string content;

        public ControlSalaPage()
        {
            InitializeComponent();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        protected async override void OnAppearing()
        {
            content = await client.GetStringAsync(urlGeneral);
            var cortando = content.Split(';');
            string temperatura = cortando[3];
            string humedad = cortando[4];
            if (content != null)
            {
                lblTemp.Text = temperatura + "°C";
                lblHum.Text = humedad + "%";
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
            base.OnAppearing();
        }

        private async void btnAbanico_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlAbanico);
            if (content != null)
            {
                //CambiaColor(btnAbanico);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
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

        private async void btnLuz1_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlLuz1);
            if (content != null)
            {
                CambiaColor(btnLuz1);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
        }

        private async void btnLuz2_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlLuz2);
            if (content != null)
            {
                CambiaColor(btnLuz1);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
        }

        private async void btnLuz3_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlLuz3);
            if (content != null)
            {
                CambiaColor(btnLuz1);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
        }
    }
}
