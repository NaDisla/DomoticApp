using DomoticApp.MenuItems;
using DomoticApp.Views.Bath;
using DomoticApp.Views.Cocina;
using DomoticApp.Views.Dormitorio;
using DomoticApp.Views.Exteriores;
using DomoticApp.Views.Lavado;
using DomoticApp.Views.Monitoreo;
using DomoticApp.Views.Opciones;
using DomoticApp.Views.Piscina;
using DomoticApp.Views.Recibidor;
using DomoticApp.Views.Sala;
using DomoticApp.Views.Tinaco;
using DomoticApp.Views.Usuarios;
using DomoticApp.Views.Usuarios.GeneralLogin;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.MasterMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuPage : MasterDetailPage
    {
        public List<MasterMenuItems> elementosMenu { get; set; }
        ViewCell ultimaCelda, viewCell;
        MasterMenuItems pagina;
        public string usuario { get; set; }
        public static string usuarioRefresh;

        [Obsolete]
        public MasterMenuPage(string _usuario)
        {
            InitializeComponent();

            if (_usuario != PerfilPage.refreshUsuario)
            {
                usuario = _usuario;
            }
            else
            {
                usuario = usuarioRefresh;
            }
            var hora = DateTime.Now.Hour;
            var horario = DateTime.Now.ToShortTimeString();

            if (hora <= 12 && horario.Contains("a. m."))
            {
                btnPerfilUsuario.Text = $"Buenos días, {usuario}";
            }
            else if (hora >= 12 && hora <= 18 && horario.Contains("p. m."))
            {
                btnPerfilUsuario.Text = $"Buenas tardes, {usuario}";
            }
            else if (hora > 18 && hora <= 23 && horario.Contains("p. m."))
            {
                btnPerfilUsuario.Text = $"Buenas noches, {usuario}";
            }

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

            MasterMenuItems pagOpciones = new MasterMenuItems()
            {
                Icon = "iconoOpciones.png",
                TargetType = typeof(OpcionesAdminPage),
                Title = "Más opciones"
            };
            elementosMenu.Add(pagOpciones);

            MasterMenuItems pagSalir = new MasterMenuItems()
            {
                Icon = "salir.png",
                TargetType = typeof(GeneralLoginPage),
                Title = "Cerrar sesión"
            };
            elementosMenu.Add(pagSalir);

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

        [Obsolete]
        private void listaMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            pagina = e.SelectedItem as MasterMenuItems;
            if(pagina.Title == "Monitorear")
            {
                Detail = new MainPage(SolicitudMenu);
                IsPresented = false;
            }
            else if(pagina.TargetType.Equals(typeof(GeneralLoginPage)))
            {
                Application.Current.MainPage = new NavigationPage(new GeneralLoginPage());
            }
            else
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(pagina.TargetType));
                IsPresented = false;
            }
        }

        private void btnPerfilUsuario_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PerfilPage(usuario));
            IsPresented = false;
        }

        private void CeldaMenu_Tapped(object sender, EventArgs e)
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
}