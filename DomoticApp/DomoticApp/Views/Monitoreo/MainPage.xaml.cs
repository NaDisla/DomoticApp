using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DomoticApp.Views.Popups;
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
using DomoticApp.DataHelpers;

namespace DomoticApp.Views.Monitoreo
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public static Action inicio { get; set; }
        public int _stateReceived { get; set; }
        ValidarCambioRed cambioRed = new ValidarCambioRed();
        GeneralData data = new GeneralData();

        [Obsolete]
        public MainPage(Action solicitudMenu)
        {
            InitializeComponent();
            inicio = solicitudMenu;
            btnMenu.Clicked += (s, e) => inicio();
        }

        [Obsolete]
#pragma warning disable CS0809
        protected override void OnAppearing()
#pragma warning restore CS0809
        {
            GetStateModules();
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

        void GetStateModules()
        {
            var dormitorio = GetStateDormitorio();
            var cocina = GetStateCocina();
            var bath = GetStateBath();
            var exteriores = GetStateExteriores();
            var lavadero = GetStateLavadero();
            var recibidor = GetStateRecibidor();
            var sala = GetStateSala();

            if (dormitorio || cocina || bath || exteriores || lavadero || recibidor || sala)
            {
                lblTextoActivos.IsVisible = false;
            }
            else
            {
                lblTextoActivos.IsVisible = true;
            }
        }

        bool GetStateDormitorio()
        {
            var luzDormitorio1 = ControlDormitorioPage.stateLuz1;
            var luzDormitorio2 = ControlDormitorioPage.stateLuz2;
            var dormitorioAbanico = ControlDormitorioPage.stateAbanico;

            if (luzDormitorio1 == 1 || luzDormitorio2 == 1 || dormitorioAbanico == 1)
            {
                btnDormitorio.IsVisible = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool GetStateCocina()
        {
            var luzCocina1 = ControlCocinaPage.stateLuz1;
            var luzCocina2 = ControlCocinaPage.stateLuz2;

            if (luzCocina1 == 1 || luzCocina2 == 1)
            {
                btnCocina.IsVisible = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool GetStateBath()
        {
            var luzBath = ControlBathPage.stateLuz;

            if (luzBath == 1 || luzBath == 1)
            {
                btnBath.IsVisible = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool GetStateExteriores()
        {
            var luzEntrada1 = ControlExterioresPage.stateLuzEntrada1;
            var luzEntrada2 = ControlExterioresPage.stateLuzEntrada2;
            var luzEntrada3 = ControlExterioresPage.stateLuzEntrada3;
            var luzJardin1 = ControlExterioresPage.stateLuzJardin1;
            var luzJardin2 = ControlExterioresPage.stateLuzJardin2;
            var luzTerraza = ControlExterioresPage.stateLuzTerraza;

            if (luzEntrada1 == 1 || luzEntrada2 == 1 || luzEntrada3 == 1 || luzJardin1 == 1 || luzJardin2 == 1 || luzTerraza == 1)
            {
                btnExteriores.IsVisible = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool GetStateLavadero()
        {
            var luzLavadero = ControlLavadoPage.stateLuz;

            if (luzLavadero == 1 || luzLavadero == 1)
            {
                btnLavado.IsVisible = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool GetStateRecibidor()
        {
            var luzRecibidor1 = ControlRecibidorPage.stateLuz1;
            var luzRecibidor2 = ControlRecibidorPage.stateLuz2;
            var luzRecibidor3 = ControlRecibidorPage.stateLuz3;

            if (luzRecibidor1 == 1 || luzRecibidor2 == 1 || luzRecibidor3 == 1)
            {
                btnRecibidor.IsVisible = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool GetStateSala()
        {
            var luzSala1 = ControlSalaPage.stateLuz1;
            var luzSala2 = ControlSalaPage.stateLuz2;
            var abanicoSala = ControlSalaPage.stateAbanico;

            if (luzSala1 == 1 || luzSala2 == 1 || abanicoSala == 1)
            {
                btnSala.IsVisible = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
