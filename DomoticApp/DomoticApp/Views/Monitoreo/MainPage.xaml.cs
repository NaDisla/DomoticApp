using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DomoticApp.Views.Popups;
using System.Net.Http;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Essentials;
using DomoticApp.Views.Dormitorio;
using DomoticApp.Views.Cocina;
using DomoticApp.Views.Bath;
using DomoticApp.Views.Exteriores;
using DomoticApp.Views.Lavado;
using DomoticApp.Views.Recibidor;
using DomoticApp.Views.Sala;

namespace DomoticApp.Views.Monitoreo
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public static Action inicio { get; set; }
        public int _stateReceived { get; set; }

        [Obsolete]
        public MainPage(Action solicitudMenu)
        {
            InitializeComponent();
            
            inicio = solicitudMenu;
            btnMenu.Clicked += (s, e) => inicio();
        }

        protected override void OnAppearing()
        {
            GetStateDormitorio();
            GetStateCocina();
            GetStateBath();
            GetStateExteriores();
            GetStateLavadero();
            GetStateRecibidor();
            GetStateSala();

            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void ControlActivos(Button btnModule, int state)
        {
            if(state == 1)
            {
                lblTextoActivos.IsVisible = false;
                btnModule.IsVisible = true;
            }
            else
            {
                lblTextoActivos.IsVisible = true;
                btnModule.IsVisible = false;
            }
        }

        void GetStateDormitorio()
        {
            var luzDormitorio1 = ControlDormitorioPage.stateLuz1;
            var luzDormitorio2 = ControlDormitorioPage.stateLuz2;
            var dormitorioAbanico = ControlDormitorioPage.stateAbanico;

            if (luzDormitorio1 == 1 || luzDormitorio2 == 1 || dormitorioAbanico == 1)
            {
                ControlActivos(btnDormitorio, 1);
            }
            else
            {
                ControlActivos(btnDormitorio, 0);
            }
        }

        void GetStateCocina()
        {
            var luzCocina1 = ControlCocinaPage.stateLuz1;
            var luzCocina2 = ControlCocinaPage.stateLuz2;

            if (luzCocina1 == 1 || luzCocina2 == 1)
            {
                ControlActivos(btnCocina, 1);
            }
            else
            {
                ControlActivos(btnCocina, 0);
            }
        }

        void GetStateBath()
        {
            var luzBath = ControlBathPage.stateLuz;

            if (luzBath == 1 || luzBath == 1)
            {
                ControlActivos(btnBath, 1);
            }
            else
            {
                ControlActivos(btnBath, 0);
            }
        }

        void GetStateExteriores()
        {
            var luzEntrada1 = ControlExterioresPage.stateLuzEntrada1;
            var luzEntrada2 = ControlExterioresPage.stateLuzEntrada2;
            var luzEntrada3 = ControlExterioresPage.stateLuzEntrada3;
            var luzJardin1 = ControlExterioresPage.stateLuzJardin1;
            var luzJardin2 = ControlExterioresPage.stateLuzJardin2;
            var luzTerraza = ControlExterioresPage.stateLuzTerraza;

            if (luzEntrada1 == 1 || luzEntrada2 == 1 || luzEntrada3 == 1 || luzJardin1 == 1 || luzJardin2 == 1 || luzTerraza == 1)
            {
                ControlActivos(btnExteriores, 1);
            }
            else
            {
                ControlActivos(btnExteriores, 0);
            }
        }

        void GetStateLavadero()
        {
            var luzLavadero = ControlLavadoPage.stateLuz;

            if (luzLavadero == 1 || luzLavadero == 1)
            {
                ControlActivos(btnLavado, 1);
            }
            else
            {
                ControlActivos(btnLavado, 0);
            }
        }

        void GetStateRecibidor()
        {
            var luzRecibidor1 = ControlRecibidorPage.stateLuz1;
            var luzRecibidor2 = ControlRecibidorPage.stateLuz2;
            var luzRecibidor3 = ControlRecibidorPage.stateLuz3;

            if (luzRecibidor1 == 1 || luzRecibidor2 == 1 || luzRecibidor3 == 1)
            {
                ControlActivos(btnRecibidor, 1);
            }
            else
            {
                ControlActivos(btnRecibidor, 0);
            }
        }

        void GetStateSala()
        {
            var luzSala1 = ControlSalaPage.stateLuz1;
            var luzSala2 = ControlSalaPage.stateLuz2;
            var abanicoSala = ControlSalaPage.stateAbanico;

            if (luzSala1 == 1 || luzSala2 == 1 || abanicoSala == 1)
            {
                ControlActivos(btnSala, 1);
            }
            else
            {
                ControlActivos(btnSala, 0);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }
        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        { 
            await DisplayAlert("Red Cambiada", "Estado: " + e.NetworkAccess, "OK");
        }

        private async void btnDormitorio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ControlDormitorioPage());
        }

        private void btnCocina_Clicked(object sender, EventArgs e)
        {

        }

        private void btnLavado_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSala_Clicked(object sender, EventArgs e)
        {

        }

        private void btnBath_Clicked(object sender, EventArgs e)
        {

        }

        private void btnRecibidor_Clicked(object sender, EventArgs e)
        {

        }

        private void btnExteriores_Clicked(object sender, EventArgs e)
        {

        }
    }
}
