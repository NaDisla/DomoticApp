using DomoticApp.SettingsAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Exteriores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlExterioresPage : ContentPage
    {
        public ControlExterioresPage()
        {
            InitializeComponent();
        }

        private void btnAjustes_Clicked(object sender, EventArgs e)
        {
             DependencyService.Get<IServiceSettings>().OpenSettings();
        }
    }
}