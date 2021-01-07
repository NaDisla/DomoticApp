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
            EstadoAreas();
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

        async void EstadoAreas()
        {
            var dormitorio = await EstadoDormitorio();
            var sala = await EstadoSala();
            var recibidor = await EstadoRecibidor();
            var cocina = await EstadoCocina();
            var exteriores = await EstadoExteriores();
            var lavado = await EstadoLavado();
            var bath = await EstadoBath();

            if (dormitorio || sala || recibidor || cocina || exteriores || lavado || bath)
            {
                lblTextoActivos.IsVisible = false;
            }
            else
            {
                lblTextoActivos.IsVisible = true;
            }

        }

        async Task<bool> EstadoDormitorio()
        {
            var getEstado = await data.GetEstadoDormitorio();
            var luz1 = getEstado.Where(x => x.Luz1 == 0 || x.Luz1 == 1).Select(y => y.Luz1).FirstOrDefault();
            var luz2 = getEstado.Where(x => x.Luz2 == 0 || x.Luz2 == 1).Select(y => y.Luz2).FirstOrDefault();
            var abanico = getEstado.Where(x => x.Abanico == 0 || x.Abanico == 1).Select(y => y.Abanico).FirstOrDefault();

            if (getEstado.Count == 0)
            {
                await data.AgregarEstadoDormitorio();
                return false;
            }
            else
            {
                if (luz1 == 1 || luz2 == 1 || abanico == 1)
                {
                    btnDormitorio.IsVisible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        async Task<bool> EstadoSala()
        {
            var getEstado = await data.GetEstadoSala();
            var luz1 = getEstado.Where(x => x.Luz1 == 0 || x.Luz1 == 1).Select(y => y.Luz1).FirstOrDefault();
            var luz2 = getEstado.Where(x => x.Luz2 == 0 || x.Luz2 == 1).Select(y => y.Luz2).FirstOrDefault();
            var abanico = getEstado.Where(x => x.Abanico == 0 || x.Abanico == 1).Select(y => y.Abanico).FirstOrDefault();

            if (getEstado.Count == 0)
            {
                await data.AgregarEstadoSala();
                return false;
            }
            else
            {
                if (luz1 == 1 || luz2 == 1 || abanico == 1)
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

        async Task<bool> EstadoExteriores()
        {
            var getEstado = await data.GetEstadoExteriores();
            var luzEntrada1 = getEstado.Where(x => x.LuzEntrada1 == 0 || x.LuzEntrada1 == 1).
                Select(y => y.LuzEntrada1).FirstOrDefault();
            var luzEntrada2 = getEstado.Where(x => x.LuzEntrada2 == 0 || x.LuzEntrada2 == 1).
                Select(y => y.LuzEntrada2).FirstOrDefault();
            var luzEntrada3 = getEstado.Where(x => x.LuzEntrada3 == 0 || x.LuzEntrada3 == 1).
                Select(y => y.LuzEntrada3).FirstOrDefault();
            var luzJardin1 = getEstado.Where(x => x.LuzJardin1 == 0 || x.LuzJardin1 == 1).
                Select(y => y.LuzJardin1).FirstOrDefault();
            var luzJardin2 = getEstado.Where(x => x.LuzJardin2 == 0 || x.LuzJardin2 == 1).
                Select(y => y.LuzJardin2).FirstOrDefault();
            var luzTerraza = getEstado.Where(x => x.LuzTerraza == 0 || x.LuzTerraza == 1).
                Select(y => y.LuzTerraza).FirstOrDefault();

            if (getEstado.Count == 0)
            {
                await data.AgregarEstadoExteriores();
                return false;
            }
            else
            {
                if (luzEntrada1 == 1 || luzEntrada2 == 1 || luzEntrada3 == 1 || luzJardin1 == 1 || luzJardin2 == 1
                    || luzTerraza == 1)
                {
                    btnExteriores.IsVisible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        async Task<bool> EstadoBath()
        {
            var getEstado = await data.GetEstadoBath();
            var luz = getEstado.Where(x => x.Luz == 0 || x.Luz == 1).Select(y => y.Luz).FirstOrDefault();

            if (getEstado.Count == 0)
            {
                await data.AgregarEstadoBath();
                return false;
            }
            else
            {
                if (luz == 1)
                {
                    btnBath.IsVisible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        async Task<bool> EstadoLavado()
        {
            var getEstado = await data.GetEstadoLavadero();
            var luz = getEstado.Where(x => x.Luz == 0 || x.Luz == 1).Select(y => y.Luz).FirstOrDefault();

            if (getEstado.Count == 0)
            {
                await data.AgregarEstadoLavadero();
                return false;
            }
            else
            {
                if (luz == 1)
                {
                    btnLavado.IsVisible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        async Task<bool> EstadoCocina()
        {
            var getEstado = await data.GetEstadoCocina();
            var luz1 = getEstado.Where(x => x.Luz1 == 0 || x.Luz1 == 1).Select(y => y.Luz1).FirstOrDefault();
            var luz2 = getEstado.Where(x => x.Luz2 == 0 || x.Luz2 == 1).Select(y => y.Luz2).FirstOrDefault();

            if (getEstado.Count == 0)
            {
                await data.AgregarEstadoCocina();
                return false;
            }
            else
            {
                if (luz1 == 1 || luz2 == 1)
                {
                    btnCocina.IsVisible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        async Task<bool> EstadoRecibidor()
        {
            var getEstado = await data.GetEstadoRecibidor();
            var luz1 = getEstado.Where(x => x.Luz1 == 0 || x.Luz1 == 1).Select(y => y.Luz1).FirstOrDefault();
            var luz2 = getEstado.Where(x => x.Luz2 == 0 || x.Luz2 == 1).Select(y => y.Luz2).FirstOrDefault();
            var luz3 = getEstado.Where(x => x.Luz2 == 0 || x.Luz2 == 1).Select(y => y.Luz2).FirstOrDefault();

            if (getEstado.Count == 0)
            {
                await data.AgregarEstadoRecibidor();
                return false;
            }
            else
            {
                if (luz1 == 1 || luz2 == 1 || luz3 == 1)
                {
                    btnRecibidor.IsVisible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
