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
        public string Temperatura { get; set; }
        public string Humedad { get; set; }
        private const string urlTarjeta = "http://10.0.0.17";
        private const string urlEncenderLuz = "http://10.0.0.17/D";
        private const string urlEncenderAbanico = "http://10.0.0.17/A";
        private readonly HttpClient client = new HttpClient();
        private string content;
        public string estadoDormitorio;
        
        public ControlDormitorioPage()
        {
            InitializeComponent();  
        }
        protected async override void OnAppearing()
        {
            content = await client.GetStringAsync(urlTarjeta);
            var cortando = content.Split(';');
            string temperatura = cortando[0];
            string humedad = cortando[1];
            if (content != null)
            {
                Temperatura = temperatura + "°C";
                Humedad = humedad + "%";
                lblTemp.Text = Temperatura;
                lblHum.Text = Humedad;
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
            base.OnAppearing();
        }
        public async void btnLuces_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlEncenderLuz);
            /*var cortando = content.Split(';');
            estadoDormitorio = cortando[2];*/
            if (content != null)
            {
                CambiaColor(btnLuces);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
            
        }

        private async void btnAbanico_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlEncenderAbanico);
            if (content != null)
            {
                CambiaColor(btnAbanico);
            }
            else
            {
                await DisplayAlert("Error de conexión", "No se ha podido establecer la conexión. ", "OK");
            }
            
        }

        private void btnTelevision_Clicked(object sender, EventArgs e)
        {

        }

        void CambiaColor(Button btn)
        {
            if(estado == 0)
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