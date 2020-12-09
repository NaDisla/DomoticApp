using DomoticApp.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Accesos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestAccesosPage : ContentPage
    {
        GeneralData data = new GeneralData();
        int id = 1000;
        private const string urlGeneral = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        private string content;
        
        public TestAccesosPage()
        {
            InitializeComponent();
        }

        private async void btnRegistroClaveAcceso_Clicked(object sender, EventArgs e)
        {
            try
            {
                await data.AgregarClave(id++, "Clave", txtNuevaClave.Text);
                HttpContent httpContent = new StringContent(txtNuevaClave.Text);
                await client.PostAsync(urlGeneral, httpContent);
                await DisplayAlert("Registro exitoso", "Se ha registrado su clave de acceso", "OK");
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}