using DomoticApp.Views.Monitoreo;
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
        public int estado = 0;
        private const string urlEncenderLuz = "http://10.0.0.17/L";
        private readonly HttpClient client = new HttpClient();
        private string content;

        public ControlLavadoPage()
        {
            InitializeComponent();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        private void btnLuz_Clicked(object sender, EventArgs e)
        {

        }
    }
}