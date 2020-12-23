using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class ListaAccesosPage : PopupPage
    {
        ViewCell ultimaCelda, viewCell;
        List<Accesos> accesos = new List<Accesos>();
        public ListaAccesosPage()
        {
            InitializeComponent();
            
            accesos.Add(new Accesos() { Nombre = "Token 1", Valor = "AA932815" });
            accesos.Add(new Accesos() { Nombre = "Token 2", Valor = "9A0FB715" });
            accesos.Add(new Accesos() { Nombre = "Tarjeta 1", Valor = "B916AD8C" });
            accesos.Add(new Accesos() { Nombre = "Tarjeta 2", Valor = "39E2DC8B" });
            accesos.Add(new Accesos() { Nombre = "Clave teclado", Valor = "4A6B" });
            listaAccesos.ItemsSource = accesos;
        }

        private void listaAccesos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void btnAsignarAccesoUsuario_Clicked(object sender, EventArgs e)
        {

        }

        [Obsolete]
        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

        private void listaAccesos_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
    public class Accesos
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }

        public string Acceso
        {
            get
            {
                return string.Format("{0}: {1}", Nombre, Valor);
            }
        }
    }
}