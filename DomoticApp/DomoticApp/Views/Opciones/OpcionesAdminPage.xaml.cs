using DomoticApp.Views.Accesos;
using DomoticApp.Views.Monitoreo;
using DomoticApp.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            ResizeTitlePage();
            btnMenu.Clicked += (s,e) => MainPage.inicio();
        }
        void ResizeTitlePage()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplayInfo.Height;
            if(height <= 2000)
            {
                lblTitlePage.FontSize = 17;
                lblTitleFrameMantenimiento.FontSize = 17;
                lblTitleFrameLog.FontSize = 17;
            }
        }

        private async void btnUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaUsuariosPage());
        }

        private async void btnAccesos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccesosPage());
        }

        private void btnEntradas_Clicked(object sender, EventArgs e)
        {

        }
    }
}