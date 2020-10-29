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
    public partial class LoadingPage : PopupPage
    {
        public LoadingPage(string textLoadingDetail)
        {
            InitializeComponent();
            lblLoadingDetail.Text = textLoadingDetail;
        }
    }
}