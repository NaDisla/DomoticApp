using Android.Graphics;
using DomoticApp.DataHelpers;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaUsuariosPage : ContentPage
    {
        GeneralData data = new GeneralData();
        public ObservableCollection<Models.Usuarios> listUsuarios { get; set; }
        
        public ListaUsuariosPage()
        {
            InitializeComponent();
            
            CustomFont();
            GetUsersData();
        }

        async void GetUsersData()
        {
            List<Models.Usuarios> getUsuarios = await data.GetUsuarios();
            listUsuarios = new ObservableCollection<Models.Usuarios>(getUsuarios);
            dataGrid.ItemsSource = listUsuarios;
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
                MappingName = "UsuarioNombreCompleto",
                HeaderFont = "Raleway-Bold.ttf#Raleway-Bold",
                HeaderText = "Nombre",
                LoadUIView = true
            };
            dataGrid.Columns.Add(segundaColumna);
        }
    }
}