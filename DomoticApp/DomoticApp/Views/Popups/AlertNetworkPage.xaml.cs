using DomoticApp.SettingsAccess;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertNetworkPage : PopupPage
    {
        public static int res;

        public AlertNetworkPage()
        {
            InitializeComponent();
        }

        private void btnAjustesVPN_Clicked(object sender, EventArgs e)
        {
           res = DependencyService.Get<IServiceSettings>().OpenSettings();
        }
    }
}