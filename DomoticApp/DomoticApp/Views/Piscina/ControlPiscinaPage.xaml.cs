using DomoticApp.Views.Monitoreo;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Piscina
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlPiscinaPage : ContentPage
    {
        private const string urlGeneral = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        string content, nivelAgua;
        int aguaNivel;

        public ControlPiscinaPage()
        {
            InitializeComponent();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        private async void btnActualizarNivelAgua_Clicked(object sender, EventArgs e)
        {
            content = await client.GetStringAsync(urlGeneral);
            if(content != null)
            {
                string[] cortando = content.Split(';');
                nivelAgua = cortando[4];
                lblNivelAguaPorcentaje.Text = $"{nivelAgua}%";
                aguaNivel = int.Parse(nivelAgua);
                if(aguaNivel >= 80 && aguaNivel <= 100)
                {
                    lblNivelAguaTexto.Text = "Alto";
                }
                else if(aguaNivel >= 79 && aguaNivel <= 50)
                {
                    lblNivelAguaTexto.Text = "Medio";
                }
                else
                {
                    lblNivelAguaTexto.Text = "Bajo";
                }
            }
        }
    }
}