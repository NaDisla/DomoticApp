using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Usuarios.GeneralLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralLoginPage : TabbedPage
    {
        public GeneralLoginPage()
        {
            InitializeComponent();
            SubscribeTab();
            SendTap();
        }
        void SubscribeTab()
        {
            MessagingCenter.Subscribe<object, int>(this, "click", (arg, idx) =>
            {
                CurrentPage = Children[idx];
            });
        }
        void SendTap()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                MessagingCenter.Send<object, int>(this, "click", 2);
            };
            lblOlvidarClave.GestureRecognizers.Add(tapGestureRecognizer);
        }
        private void btnLogin_Clicked(object sender, EventArgs e)
        {

        }

        private void btnRegistro_Clicked(object sender, EventArgs e)
        {

        }
    }
}