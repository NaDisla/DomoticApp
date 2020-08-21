using DomoticApp.MenuItems;
using DomoticApp.Views.Bath;
using DomoticApp.Views.Cocina;
using DomoticApp.Views.Dormitorio;
using DomoticApp.Views.Exteriores;
using DomoticApp.Views.Lavado;
using DomoticApp.Views.Monitoreo;
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
        public MasterMenuPage()
        {
            InitializeComponent();
            elementosMenu = new List<MasterMenuItems>();
            
            MasterMenuItems pagMonitoreo = new MasterMenuItems()
            {
                Icon = "iconoOjo.png",
                TargetType = typeof(PanelMonitoreoPage),
                Title = "Monitorear"
            };
            elementosMenu.Add(pagMonitoreo);

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

            listaMenu.ItemsSource = elementosMenu;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)));
            listaMenu.ItemSelected += listaMenu_ItemSelected;
        }

        private void listaMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MasterMenuItems pagina = e.SelectedItem as MasterMenuItems;
            Detail = new NavigationPage((Page)Activator.CreateInstance(pagina.TargetType));
            IsPresented = false;
        }
    }
}