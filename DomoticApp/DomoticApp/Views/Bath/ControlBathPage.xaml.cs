using DomoticApp.Views.MasterMenu;
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
        }

        private void btnRegreso_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MasterMenuPage());
        }
    }
}