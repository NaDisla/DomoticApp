using DomoticApp.Views.MasterMenu;
using Plugin.SharedTransitions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DomoticApp;
using DomoticApp.Views.Bienvenida;

namespace DomoticApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new MasterMenuPage());
        }
        /*public App(BienvenidaPage pagina)
        {
            InitializeComponent();
            MainPage = new NavigationPage(pagina);
        }*/

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
