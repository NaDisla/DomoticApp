using DomoticApp.Views.Accesos;
using DomoticApp.Views.Monitoreo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Opciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpcionesAdminPage : ContentPage
    {
        public OpcionesAdminPage()
        {
            InitializeComponent();
            btnMenu.Clicked += (s,e) => MainPage.inicio();
        }

        private void btnUsuarios_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnAccesos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new TestAccesosPage()));
        }

        private void btnEntradas_Clicked(object sender, EventArgs e)
        {

        }
    }
}