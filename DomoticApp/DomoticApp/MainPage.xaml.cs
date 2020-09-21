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

namespace DomoticApp
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
            //btnTest.Clicked += (s, e) => inicio();
            //btnTest.Clicked += (s, e) => 
        }

        private void btnTest_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("P", "H", "OK");
        }

        /*protected async override void OnAppearing()
        {
           
            base.OnAppearing();
        }*/
    }
}
