using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Plugin.LocalNotifications;
using System;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Tinaco
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlTinacoPage : ContentPage
    {
        private const string urlGeneral = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        string content, nivelAgua;
        int aguaNivel;
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public ControlTinacoPage()
        {
            InitializeComponent();
            GetNivel();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        [Obsolete]
#pragma warning disable CS0809
        protected override void OnAppearing()
#pragma warning restore CS0809
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        [Obsolete]
#pragma warning disable CS0809
        protected override void OnDisappearing()
#pragma warning restore CS0809
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        [Obsolete]
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            cambioRed.NetworkChanged(e);
        }

        async void GetNivel()
        {
            content = await client.GetStringAsync(urlGeneral);
            if (content != null)
            {
                string[] cortando = content.Split(';');
                nivelAgua = cortando[5];
                aguaNivel = int.Parse(nivelAgua);

                if (aguaNivel == 06)
                {
                    lblNivelAguaPorcentaje.Text = "0%";
                    lblNivelAguaTexto.Text = "Vacío";
                    CrossLocalNotifications.Current.Show("Tinaco vacío", "El tinaco necesita llenarse.", 0);
                }
                else 
                {
                    lblNivelAguaPorcentaje.Text = $"{nivelAgua}%";
                    if (aguaNivel >= 80 && aguaNivel <= 100)
                    {
                        lblNivelAguaTexto.Text = "Alto";
                    }
                    else if (aguaNivel >= 79 && aguaNivel <= 50)
                    {
                        lblNivelAguaTexto.Text = "Medio";
                    }
                    else
                    {
                        lblNivelAguaTexto.Text = "Bajo";
                        CrossLocalNotifications.Current.Show("Tinaco disminuyendo", "El tinaco está a punto de quedar vacío.", 0);
                    }

                }
            }
        }
        private void btnActualizarNivelAgua_Clicked(object sender, EventArgs e)
        {
            GetNivel();
        }
    }
}