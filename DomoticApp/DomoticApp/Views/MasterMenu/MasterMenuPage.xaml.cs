using DomoticApp.MenuItems;
using DomoticApp.Views.Bath;
using DomoticApp.Views.Cocina;
using DomoticApp.Views.Dormitorio;
using DomoticApp.Views.Exteriores;
using DomoticApp.Views.Lavado;
using DomoticApp.Views.Piscina;
using DomoticApp.Views.Recibidor;
using DomoticApp.Views.Sala;
using DomoticApp.Views.Tinaco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.MasterMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuPage : MasterDetailPage
    {
        public List<MasterMenuItems> elementosMenu {get; set;}
        ViewCell ultimaCelda, viewCell;
        MasterMenuItems pagina;
        public MasterMenuPage()
        {
            InitializeComponent();
            Detail = new MainPage(SolicitudMenu);
            elementosMenu = new List<MasterMenuItems>();
            
            MasterMenuItems pagInicio = new MasterMenuItems()
            {
                Icon = "iconoOjo.png",
                TargetType = typeof(MainPage),
                Title = "Monitorear"
            };
            elementosMenu.Add(pagInicio);

            MasterMenuItems pagDormitorio = new MasterMenuItems()
            {
                Icon = "iconoDormitorio.png",
                TargetType = typeof(ControlDormitorioPage),
                Title = "Dormitorio"
            };
            elementosMenu.Add(pagDormitorio);

            MasterMenuItems pagCocina = new MasterMenuItems()
            {
                Icon = "iconoCocina.png",
                TargetType = typeof(ControlCocinaPage),
                Title = "Cocina"
            };
            elementosMenu.Add(pagCocina);

            MasterMenuItems pagBath = new MasterMenuItems()
            {
                Icon = "iconoBano.png",
                TargetType = typeof(ControlBathPage),
                Title = "Baño"
            };
            elementosMenu.Add(pagBath);

            MasterMenuItems pagLavado = new MasterMenuItems()
            {
                Icon = "iconoLavado.png",
                TargetType = typeof(ControlLavadoPage),
                Title = "Área de Lavado"
            };
            elementosMenu.Add(pagLavado);

            MasterMenuItems pagSala = new MasterMenuItems()
            {
                Icon = "iconoSala.png",
                TargetType = typeof(ControlSalaPage),
                Title = "Sala"
            };
            elementosMenu.Add(pagSala);

            MasterMenuItems pagRecibidor = new MasterMenuItems()
            {
                Icon = "iconoRecibidor.png",
                TargetType = typeof(ControlRecibidorPage),
                Title = "Recibidor"
            };
            elementosMenu.Add(pagRecibidor);

            MasterMenuItems pagPiscina = new MasterMenuItems()
            {
                Icon = "iconoPiscina.png",
                TargetType = typeof(ControlPiscinaPage),
                Title = "Piscina"
            };
            elementosMenu.Add(pagPiscina);

            MasterMenuItems pagTinaco = new MasterMenuItems()
            {
                Icon = "iconoTinaco.png",
                TargetType = typeof(ControlTinacoPage),
                Title = "Tinaco"
            };
            elementosMenu.Add(pagTinaco);

            MasterMenuItems pagExteriores = new MasterMenuItems()
            {
                Icon = "iconoExterior.png",
                TargetType = typeof(ControlExterioresPage),
                Title = "Exteriores"
            };
            elementosMenu.Add(pagExteriores);

            MasterMenuItems pagTest = new MasterMenuItems()
            {
                Icon = "iconoExterior.png",
                TargetType = typeof(ControlExterioresPage),
                Title = "Test"
            };
            elementosMenu.Add(pagTest);

            MasterMenuItems pagTest2 = new MasterMenuItems()
            {
                Icon = "iconoExterior.png",
                TargetType = typeof(ControlExterioresPage),
                Title = "Otro Test"
            };
            elementosMenu.Add(pagTest2);

            MasterMenuItems pagTest3 = new MasterMenuItems()
            {
                Icon = "iconoExterior.png",
                TargetType = typeof(ControlExterioresPage),
                Title = "Otro Test 3"
            };
            elementosMenu.Add(pagTest3);

            listaMenu.ItemsSource = elementosMenu;
            listaMenu.ItemSelected += listaMenu_ItemSelected;
            
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void SolicitudMenu()
        {
            IsPresented = !IsPresented;
        }

        private void listaMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            pagina = e.SelectedItem as MasterMenuItems;
            if(pagina.Title == "Monitorear")
            {
                Detail = new MainPage(SolicitudMenu);
                IsPresented = false;
            }
            else
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(pagina.TargetType));
                IsPresented = false;
            }
        }

        private void CeldaMenu_Tapped(object sender, EventArgs e)
        {
            if (ultimaCelda != null)
                ultimaCelda.View.BackgroundColor = Color.Default;
            viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#A5CDDB");
                ultimaCelda = viewCell;
            }
        }
    }
}