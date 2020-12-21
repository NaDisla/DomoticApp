using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Models.Usuarios selectedUser;
        public ObservableCollection<Models.UsuariosAccesos> ListUsuarios { get; set; }
        private const string urlGeneral = "http://10.0.0.17";
        private readonly HttpClient client = new HttpClient();
        private string content;
        
        public AccesosPage()
        {
            InitializeComponent();
            CustomFont();
            GetUsersData();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        async void GetUsersData()
        {
            List<Models.UsuariosAccesos> getUsuarios = await data.GetUsuariosAccesos();
            ListUsuarios = new ObservableCollection<Models.UsuariosAccesos>(getUsuarios);
            dataGrid.ItemsSource = ListUsuarios;
        }

        void CustomFont()
        {
            GridTextColumn encabezado01 = new GridTextColumn()
            {
                MappingName = "UsuarioID",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "ID Usuario",
                LoadUIView = true
            };
            dataGrid.Columns.Add(encabezado01);

            GridTextColumn segundaColumna = new GridTextColumn()
            {
                MappingName = "Usuario",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Usuario",
                LoadUIView = true
            };
            dataGrid.Columns.Add(segundaColumna);

            GridTextColumn terceraColumna = new GridTextColumn()
            {
                MappingName = "UsuarioNombreApellido",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Nombre y apellido",
                LoadUIView = true
            };
            dataGrid.Columns.Add(terceraColumna);

            GridTextColumn cuartaColumna = new GridTextColumn()
            {
                MappingName = "UsuarioAcceso",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Acceso",
                LoadUIView = true
            };
            dataGrid.Columns.Add(cuartaColumna);
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            selectedUser = (e.AddedItems[0] as Models.Usuarios);
        }

        private void btnToken_Clicked(object sender, EventArgs e)
        {

        }

        private void btnTarjeta_Clicked(object sender, EventArgs e)
        {

        }

        private void btnDesactivarTokenTarjeta_Clicked(object sender, EventArgs e)
        {

        }

        private void dataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            e.Style.Font = "Raleway-Regular.ttf#Raleway-Regular";
        }
    }
}