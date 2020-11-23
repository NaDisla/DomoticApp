using DomoticApp.Views.MasterMenu;
using DomoticApp.Views.Monitoreo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Bath
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlBathPage : ContentPage
    {
        public ControlBathPage()
        {
            InitializeComponent();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        private void btnLuz_Clicked(object sender, EventArgs e)
        {

        }
    }
}