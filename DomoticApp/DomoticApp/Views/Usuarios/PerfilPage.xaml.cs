using DomoticApp.DataHelpers;
using DomoticApp.Views.Monitoreo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilPage : ContentPage
    {
        GeneralData data = new GeneralData();
        public string _usuario { get; set; }

        public PerfilPage(string usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            DatosUsuario();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
        }

        async void DatosUsuario()
        {
            var usuarios = await data.GetUsuarios();
            var userID = usuarios.Where(x => x.UsuarioNombreCompleto == _usuario).Select(y => y.UsuarioID).FirstOrDefault();
            var perfilUsuario = await data.GetUsuario(userID);

            txtNombreRealPerfil.Text = perfilUsuario.UsuarioNombreCompleto;
            txtCorreoPerfil.Text = perfilUsuario.UsuarioCorreo;
            txtUsuarioPerfil.Text = perfilUsuario.NombreUsuario;
            txtClavePerfil.Text = DataSecurity.Decrypt(perfilUsuario.UsuarioClave, "sblw-3hn8-sqoy19");
        }

        private void btnActualizarPerfil_Clicked(object sender, EventArgs e)
        {

        }
    }
}