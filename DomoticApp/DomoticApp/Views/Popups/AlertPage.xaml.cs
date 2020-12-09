using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPage : PopupPage
    {
        public AlertPage(string title, string detail)
        {
            InitializeComponent();
            lblTitleAlert.Text = title;
            lblDetailAlert.Text = detail;
        }

        [Obsolete]
        private async void btnCerrarAlerta_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}