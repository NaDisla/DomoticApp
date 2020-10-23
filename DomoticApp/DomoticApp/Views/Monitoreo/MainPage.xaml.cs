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

namespace DomoticApp.Views.Monitoreo
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public static Action inicio { get; set; }

        [Obsolete]
        public MainPage(Action solicitudMenu)
        {
            InitializeComponent();
            inicio = solicitudMenu;
            btnMenu.Clicked += (s, e) => inicio();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
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
    }
}
