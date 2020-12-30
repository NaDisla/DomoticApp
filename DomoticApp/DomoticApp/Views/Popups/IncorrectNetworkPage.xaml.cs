using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncorrectNetworkPage : PopupPage
    {
        public IncorrectNetworkPage(string titleError, string detailError)
        {
            InitializeComponent();
            lblTitleError.Text = titleError;
            lblDetailError.Text = detailError;
        }

        private void btnCerrarVentana_Clicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}