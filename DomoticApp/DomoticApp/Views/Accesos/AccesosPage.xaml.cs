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
        int selectedUser, rowIndex;
        public ObservableCollection<Models.Usuarios> ListUsuarios { get; set; }
        private const string urlGeneral = "http://10.0.0.17", urlToken1 = "http://10.0.0.17/desactivar-token-1", 
            urlToken2 = "http://10.0.0.17/desactivar-token-2", urlTarjeta1 = "http://10.0.0.17/desactivar-tarjeta-1",
            urlTarjeta2 = "http://10.0.0.17/desactivar-tarjeta-2";
        private readonly HttpClient client = new HttpClient();
        private string content, titleAlert, detailAlert;
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
            dataGrid.GridTapped += dataGrid_GridTapped;
        }

        private void dataGrid_GridTapped(object sender, GridTappedEventArgs e)
        {
            rowIndex = e.RowColumnIndex.RowIndex;
        }

        async void GetUsersData()
        {
            List<Models.Usuarios> getUsuarios = await data.GetUsuarios();
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

        private void btnDesactivarTokenTarjeta_Clicked(object sender, EventArgs e)
        {

        }

        private void dataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            e.Style.Font = "Raleway-Regular.ttf#Raleway-Regular";
        }

        [Obsolete]
        private async void btnAsignarAcceso_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(rowIndex.ToString()))
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