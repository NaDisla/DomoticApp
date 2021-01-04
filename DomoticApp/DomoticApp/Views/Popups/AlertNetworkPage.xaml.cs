using DomoticApp.SettingsAccess;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertNetworkPage : PopupPage
    {
        public AlertNetworkPage()
        {
            InitializeComponent();
        }

        private void btnAjustesVPN_Clicked(object sender, EventArgs e)
        {
           DependencyService.Get<IServiceSettings>().OpenSettings();
        }
    }
}