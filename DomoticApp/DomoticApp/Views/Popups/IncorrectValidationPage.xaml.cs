using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncorrectValidationPage : PopupPage
    {
        public IncorrectValidationPage(string title, string detail)
        {
            InitializeComponent();
            lblTitleError.Text = title;
            lblDetailError.Text = detail;
        }

        [Obsolete]
        private async void btnCerrarVentana_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}