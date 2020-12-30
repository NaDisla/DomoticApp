using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaUsuariosPage : ContentPage
    {
        GeneralData data = new GeneralData();
        public ObservableCollection<Models.Usuarios> ListUsuarios { get; set; }
        private const string urlGeneral = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        private string content, titleAlert, detailAlert, titleError, detailError, titleCorrect, detailCorrect,
            detailLoading;
        public static Models.Usuarios selectedUser;
        public static List<Models.Usuarios> getUsuarios;
        ResultsOperations results = new ResultsOperations();

        public ListaUsuariosPage()
        {
            InitializeComponent();
            CustomFont();
            GetUsersData();
            dataGrid.Columns[3].IsHidden = true;
            dataGrid.Columns[4].IsHidden = true;
            dataGrid.Columns[5].IsHidden = true;
            btnMenu.Clicked += (s, e) => MainPage.inicio();
            MessagingCenter.Subscribe<ListaUsuariosPage>(this, "RefreshUsuariosPage", (sender) => {
                GetUsersData();
            });
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            if (selectedUser == null)
            {
                selectedUser = (e.AddedItems[0] as Models.Usuarios);
            }
            else
            {
                selectedUser = null;
            }
        }

        public async void GetUsersData()
        {
            getUsuarios = await data.GetUsuarios();
            ListUsuarios = new ObservableCollection<Models.Usuarios>(getUsuarios);
            dataGrid.ItemsSource = ListUsuarios;
        }

        void CustomFont()
        {
            GridTextColumn primeraColumna = new GridTextColumn()
            {
                MappingName = "UsuarioID",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "ID Usuario",
                LoadUIView = true
            };
            dataGrid.Columns.Add(primeraColumna);

            GridTextColumn segundaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioNombre",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Usuario",
                LoadUIView = true
            };
            dataGrid.Columns.Add(segundaColumna);

            GridTextColumn terceraColumna = new GridTextColumn()
            {
                MappingName = "UsuarioNombreReal",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Nombre y apellido",
                LoadUIView = true
            };
            dataGrid.Columns.Add(terceraColumna);

            GridTextColumn cuartaColumna = new GridTextColumn()
            {
                MappingName = "Acceso",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Acceso",
                LoadUIView = true
            };
            dataGrid.Columns.Add(cuartaColumna);

            GridTextColumn quintaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioRol",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Rol",
                LoadUIView = true
            };
            dataGrid.Columns.Add(quintaColumna);

            GridTextColumn sextaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioClave",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Clave",
                LoadUIView = true
            };
            dataGrid.Columns.Add(sextaColumna);

            GridTextColumn septimaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioCorreo",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Correo",
                LoadUIView = true
            };
            dataGrid.Columns.Add(septimaColumna);

            GridTextColumn octavaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioEstado",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Estado",
                LoadUIView = true
            };
            dataGrid.Columns.Add(octavaColumna);
        }

        [Obsolete]
        private async void btnDesactivarUsuario_Clicked(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                titleAlert = "Seleccione un usuario";
                detailAlert = "Debe seleccionar el usuario que desea desactivar o activar.";
                await results.Alert(titleAlert, detailAlert);
            }
            else if (selectedUser.UsuarioEstado == "Activo")
            {
                detailLoading = "Desactivando usuario...";
                LoadingPage loading = new LoadingPage(detailLoading);
                await PopupNavigation.PushAsync(loading);
                await Task.Delay(1000);

                await data.UpdateUsuario(selectedUser.UsuarioID, selectedUser.UsuarioNombreReal, selectedUser.UsuarioCorreo, selectedUser.UsuarioNombre,
                    selectedUser.UsuarioClave, selectedUser.UsuarioRol, selectedUser.Acceso, "Inactivo");
                MessagingCenter.Send(this, "RefreshUsuariosPage");

                await PopupNavigation.RemovePageAsync(loading);
                titleCorrect = "Usuario desactivado";
                detailCorrect = "Se ha desactivado el usuario correctamente.";
                await results.Success(titleCorrect, detailCorrect);
                selectedUser = null;
            }
            else if (selectedUser.UsuarioEstado == "Inactivo")
            {
                detailLoading = "Activando usuario...";
                LoadingPage loading = new LoadingPage(detailLoading);
                await PopupNavigation.PushAsync(loading);
                await Task.Delay(1000);
                
                await data.UpdateUsuario(selectedUser.UsuarioID, selectedUser.UsuarioNombreReal, selectedUser.UsuarioCorreo, selectedUser.UsuarioNombre,
                    selectedUser.UsuarioClave, selectedUser.UsuarioRol, selectedUser.Acceso, "Activo");
                MessagingCenter.Send(this, "RefreshUsuariosPage");

                await PopupNavigation.RemovePageAsync(loading);
                titleCorrect = "Usuario activado";
                detailCorrect = "Se ha activado el usuario correctamente.";
                await results.Success(titleCorrect, detailCorrect);
                selectedUser = null;
            }
        }
    }
}