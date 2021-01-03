using DomoticApp.DataHelpers;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationPage : PopupPage
    {
        string content, detailCorrect, titleCorrect, detailError, titleError, urlReceived;
        ResultsOperations results = new ResultsOperations();
        HttpClient client = new HttpClient();

        public ConfirmationPage(string title, string detail, string url)
        {
            InitializeComponent();
            lblTitleConfirmation.Text = title;
            lblDetailConfirmation.Text = detail;
            urlReceived = url;
        }

        [Obsolete]
        private async void btnYes_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
            content = await client.GetStringAsync(urlReceived);
            if (content != null)
            {
                titleCorrect = "Acceso desactivado";
                detailCorrect = "Se ha desactivado el acceso correctamente.";
                await results.Success(titleCorrect, detailCorrect);
            }
            else
            {
                titleError = "Error de conexión";
                detailError = "No se ha podido establecer la conexión con la vivienda.";
                await results.Unsuccess(titleError, detailError);
            }
        }

        [Obsolete]
        private async void btnNo_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}