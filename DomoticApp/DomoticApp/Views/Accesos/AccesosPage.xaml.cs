using DomoticApp.Customs;
using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Accesos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccesosPage : ContentPage
    {
        GeneralData data = new GeneralData();
        public ObservableCollection<Models.Usuarios> ListUsuarios { get; set; }
        private const string urlGeneral = "http://10.0.0.17", urlToken1 = "http://10.0.0.17/desactivar-token-1", 
            urlToken2 = "http://10.0.0.17/desactivar-token-2", urlTarjeta1 = "http://10.0.0.17/desactivar-tarjeta-1",
            urlTarjeta2 = "http://10.0.0.17/desactivar-tarjeta-2";
        private readonly HttpClient client = new HttpClient();
        private string content, titleAlert, detailAlert, acceso, titleError, detailError, titleCorrect, detailCorrect;
        public static Models.Usuarios selectedUser;
        public static List<Models.Usuarios> getUsuarios;
        ResultsOperations results = new ResultsOperations();

        public AccesosPage()
        {
            InitializeComponent();
            CustomFont();
            GetUsersData();
            dataGrid.Columns[4].IsHidden = true;
            dataGrid.Columns[5].IsHidden = true;
            dataGrid.Columns[6].IsHidden = true;
            btnMenu.Clicked += (s, e) => MainPage.inicio();
            MessagingCenter.Subscribe<ListaAccesosPage>(this, "RefreshAccesosPage", (sender) => {
                GetUsersData();
            });
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            if(selectedUser == null)
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
            else if(selectedUser.Acceso == "Clave teclado")
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
            if(content != null)
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
        private async void btnAsignarAcceso_Clicked(object sender, EventArgs e)
        {
            if(selectedUser == null)
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
    }
}