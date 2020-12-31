using DomoticApp.DataHelpers;
using DomoticApp.Views.Accesos;
using DomoticApp.Views.Monitoreo;
using DomoticApp.Views.Popups;
using DomoticApp.Views.Usuarios;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Opciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpcionesAdminPage : ContentPage
    {
        GeneralData data = new GeneralData();
        public ObservableCollection<Models.Usuarios> ListUsuarios { get; set; }
        private const string urlGeneral = "http://10.0.0.17", urlToken1 = "http://10.0.0.17/desactivar-token-1",
            urlToken2 = "http://10.0.0.17/desactivar-token-2", urlTarjeta1 = "http://10.0.0.17/desactivar-tarjeta-1",
            urlTarjeta2 = "http://10.0.0.17/desactivar-tarjeta-2";
        private readonly HttpClient client = new HttpClient();
        private string content, titleAlert, detailAlert, titleError, detailError, titleCorrect, detailCorrect,
            detailLoading, acceso;
        public static Models.Usuarios selectedUser;
        public static List<Models.Usuarios> getUsuarios;
        ResultsOperations results = new ResultsOperations();
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public OpcionesAdminPage()
        {
            InitializeComponent();
            ResizeTitlePage();
            CustomFont();
            GetUsersData();
            
            usuariosGrid.Columns[4].IsHidden = true;
            usuariosGrid.Columns[5].IsHidden = true;
            usuariosGrid.Columns[6].IsHidden = true;

            btnMenu.Clicked += (s, e) => MainPage.inicio();
            MessagingCenter.Subscribe<OpcionesAdminPage>(this, "RefreshUsuariosPage", (sender) => {
                GetUsersData();
            });
            MessagingCenter.Subscribe<ListaAccesosPage>(this, "RefreshAccesosPage", (sender) => {
                GetUsersData();
            });
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnAppearing()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnDisappearing()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        [Obsolete]
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            cambioRed.NetworkChanged(e);
        }

        public async void GetUsersData()
        {
            getUsuarios = await data.GetUsuarios();
            ListUsuarios = new ObservableCollection<Models.Usuarios>(getUsuarios);
            usuariosGrid.ItemsSource = ListUsuarios;
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
            usuariosGrid.Columns.Add(primeraColumna);

            GridTextColumn segundaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioNombre",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Usuario",
                LoadUIView = true
            };
            usuariosGrid.Columns.Add(segundaColumna);

            GridTextColumn terceraColumna = new GridTextColumn()
            {
                MappingName = "UsuarioNombreReal",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Nombre y apellido",
                LoadUIView = true
            };
            usuariosGrid.Columns.Add(terceraColumna);

            GridTextColumn cuartaColumna = new GridTextColumn()
            {
                MappingName = "Acceso",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Acceso",
                LoadUIView = true
            };
            usuariosGrid.Columns.Add(cuartaColumna);

            GridTextColumn quintaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioRol",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Rol",
                LoadUIView = true
            };
            usuariosGrid.Columns.Add(quintaColumna);

            GridTextColumn sextaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioClave",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Clave",
                LoadUIView = true
            };
            usuariosGrid.Columns.Add(sextaColumna);

            GridTextColumn septimaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioCorreo",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Correo",
                LoadUIView = true
            };
            usuariosGrid.Columns.Add(septimaColumna);

            GridTextColumn octavaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioEstado",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Estado",
                LoadUIView = true
            };
            usuariosGrid.Columns.Add(octavaColumna);
        }

        void ResizeTitlePage()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplayInfo.Height;
            if(height <= 2000)
            {
                lblTitlePage.FontSize = 17;
                lblTitleFrameMantenimiento.FontSize = 17;
                lblTitleFrameUsuariosAccesos.FontSize = 17;
            }
        }

        private void btnUsuarios_Clicked(object sender, EventArgs e)
        {
            if(FrameUsuariosAccesos.IsVisible == false)
            {
                FrameUsuariosAccesos.IsVisible = true;
                usuariosGrid.IsVisible = true;
                btnDesactivarUsuario.IsVisible = true;
            }
            else if(btnAsignarAcceso.IsVisible && btnDesactivarTokenTarjeta.IsVisible)
            {
                usuariosGrid.IsVisible = true;
                btnDesactivarUsuario.IsVisible = true;
                btnAsignarAcceso.IsVisible = false;
                btnDesactivarTokenTarjeta.IsVisible = false;
            }
        }

        private void btnAccesos_Clicked(object sender, EventArgs e)
        {
            if (FrameUsuariosAccesos.IsVisible == false)
            {
                FrameUsuariosAccesos.IsVisible = true;
                usuariosGrid.IsVisible = true;
                btnAsignarAcceso.IsVisible = true;
                btnDesactivarTokenTarjeta.IsVisible = true;
            }
            else if(btnDesactivarUsuario.IsVisible)
            {
                usuariosGrid.IsVisible = true;
                btnAsignarAcceso.IsVisible = true;
                btnDesactivarTokenTarjeta.IsVisible = true;
                btnDesactivarUsuario.IsVisible = false;
            }
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
                await SecureStorage.SetAsync("estadoUsuario", "Inactivo");

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

                await SecureStorage.SetAsync("estadoUsuario", "Inactivo");
                await PopupNavigation.RemovePageAsync(loading);
                titleCorrect = "Usuario activado";
                detailCorrect = "Se ha activado el usuario correctamente.";
                await results.Success(titleCorrect, detailCorrect);
                selectedUser = null;
            }
        }

        [Obsolete]
        private async void btnAsignarAcceso_Clicked(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                titleAlert = "Seleccione un usuario";
                detailAlert = "Debe seleccionar el usuario al que desea asignarle el acceso.";
                await results.Alert(titleAlert, detailAlert);
            }
            else
            {
                ListaAccesosPage listaAccesos = new ListaAccesosPage();
                await PopupNavigation.PushAsync(listaAccesos);
            }
        }

        [Obsolete]
        private async void btnDesactivarTokenTarjeta_Clicked(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                titleAlert = "Seleccione un usuario";
                detailAlert = "Debe seleccionar el usuario al que desea desactivar el acceso de tarjeta o token.";
                await results.Alert(titleAlert, detailAlert);
            }
            else if (selectedUser.Acceso == "Clave teclado")
            {
                titleAlert = "Usuario incorrecto";
                detailAlert = "Este usuario no tiene una tarjeta o token como acceso.";
                await results.Alert(titleAlert, detailAlert);
            }
            else
            {
                acceso = selectedUser.Acceso;
                switch (acceso)
                {
                    case "Tarjeta 1":
                        DesactivarTarjetaToken(urlTarjeta1);
                        break;

                    case "Tarjeta 2":
                        DesactivarTarjetaToken(urlTarjeta2);
                        break;

                    case "Token 1":
                        DesactivarTarjetaToken(urlToken1);
                        break;

                    case "Token 2":
                        DesactivarTarjetaToken(urlToken2);
                        break;
                }
            }
        }

        [Obsolete]
        async void DesactivarTarjetaToken(string url)
        {
            content = await client.GetStringAsync(url);
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

        private void usuariosGrid_SelectionChanged(object sender, Syncfusion.SfDataGrid.XForms.GridSelectionChangedEventArgs e)
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
    }
}