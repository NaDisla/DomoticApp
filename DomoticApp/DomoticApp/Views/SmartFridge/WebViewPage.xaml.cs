using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.SmartFridge
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage()
        {
            WebView webview = new WebView
            {
                Source = "http://publish.samsungsimulator.com/simulator/ac2b0789-5ad3-4932-b286-11e46e8e756c/",
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Content = new StackLayout
            {
                Children = {
                     webview
                }
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}