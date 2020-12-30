using DomoticApp.DataHelpers;
using DomoticApp.Views.Accesos;
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
        ResultsOperations results = new ResultsOperations();
        GeneralData data = new GeneralData();
        AccesosPage accesosPage = new AccesosPage();
        string titleAlert, detailAlert, accesoAsignar, titleCorrect, detailCorrect, usuario, titleError, detailError;

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
            var item = (Accesos)e.SelectedItem;
            accesoAsignar = item.Nombre;
        }

        [Obsolete]
        private async void btnAsignarAccesoUsuario_Clicked(object sender, EventArgs e)
        {
            if(listaAccesos.SelectedItem == null)
            {
                titleAlert = "Seleccione un acceso";
                detailAlert = "Debe seleccionar el acceso que desea asignarle al usuario.";
                await results.Alert(titleAlert, detailAlert);
            }
            else
            {
                var accesoExistente = AccesosPage.getUsuarios.Where(y => y.Acceso == accesoAsignar).Select(y => y.Acceso).FirstOrDefault();
                if(accesoAsignar == accesoExistente)
                {
                    titleError = "Usuario con acceso seleccionado";
                    detailError = "Este acceso ya se encuentra asignado a otro usuario.";
                    await results.Unsuccess(titleError, detailError);
                }
                else
                {
                    await data.UpdateUsuario(AccesosPage.selectedUser.UsuarioID, AccesosPage.selectedUser.UsuarioNombreReal,
                    AccesosPage.selectedUser.UsuarioCorreo, AccesosPage.selectedUser.UsuarioNombre, AccesosPage.selectedUser.UsuarioClave,
                    AccesosPage.selectedUser.UsuarioRol, accesoAsignar, AccesosPage.selectedUser.UsuarioEstado);
                    MessagingCenter.Send(this, "RefreshAccesosPage");
                    usuario = AccesosPage.selectedUser.UsuarioNombreReal;
                    await PopupNavigation.PopAsync(true);
                    titleCorrect = "Acceso asignado";
                    detailCorrect = $"Se ha asignado {accesoAsignar} a {usuario}.";
                    await results.Success(titleCorrect, detailCorrect);
                    AccesosPage.selectedUser = null;
                }
            }
        }

        [Obsolete]
        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

        private void celdaListaAcceso_Tapped(object sender, EventArgs e)
        {
            if (ultimaCelda != null)
            {
                ultimaCelda.View.BackgroundColor = Color.Default;
            }
            viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#A5CDDB");
                ultimaCelda = viewCell;
            }
        }
    }
    public class Accesos
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}