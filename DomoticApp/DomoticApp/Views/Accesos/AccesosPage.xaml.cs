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
        public ObservableCollection<Models.Usuarios> ListUsuarios { get; set; }
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
            List<Models.Usuarios> getUsuarios = await data.GetUsuarios();
            List<Models.Usuarios> final = new List<Models.Usuarios>();
            final = (from usuario in getUsuarios.Where(p => p.UsuarioID != 0).ToList()
                       select usuario).Select(p => new Models.Usuarios()
                       {
                           UsuarioID = p.UsuarioID,
                           NombreUsuario = p.NombreUsuario,
                           UsuarioNombreCompleto = p.UsuarioNombreCompleto
                       }).ToList();
            ListUsuarios = new ObservableCollection<Models.Usuarios>(final);
            
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
                MappingName = "NombreUsuario",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Usuario",
                LoadUIView = true
            };
            dataGrid.Columns.Add(segundaColumna);

            GridTextColumn terceraColumna = new GridTextColumn()
            {
                MappingName = "UsuarioNombreCompleto",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Nombre",
                LoadUIView = true
            };
            dataGrid.Columns.Add(terceraColumna);
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            selectedUser = (e.AddedItems[0] as Models.Usuarios);
        }

        private async void btnClave_Clicked(object sender, EventArgs e)
        {
            try
            {
                await data.AgregarClaveTeclado(selectedUser.NombreUsuario, txtClaveTeclado.Text);
                await DisplayAlert("Registro exitoso", "Se ha registrado su clave de acceso", "OK");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnToken_Clicked(object sender, EventArgs e)
        {

        }

        private void btnTarjeta_Clicked(object sender, EventArgs e)
        {

        }

        
    }
}