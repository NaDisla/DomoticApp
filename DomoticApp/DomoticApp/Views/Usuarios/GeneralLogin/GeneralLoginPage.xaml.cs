using DomoticApp.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using DomoticApp.Views.Monitoreo;
using DomoticApp.Views.MasterMenu;
using Xamarin.Essentials;

namespace DomoticApp.Views.Usuarios.GeneralLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralLoginPage : TabbedPage
    {
        GeneralData data = new GeneralData();
        string claveEncriptada, textDetailLoading = "Validando credenciales...", titleCorrect = "¡Bienvenido(a) ",
            detailCorrect = "Ha iniciado sesión correctamente.";
        int idUser = 1000;

        public GeneralLoginPage()
        {
            InitializeComponent();
            txtPassword.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            txtPassword.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            SubscribeTab();
            SendTap();
            HideElements();
        }

        private void SetLayoutPosition(bool onFocus)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplayInfo.Height;

            if (onFocus && height <= 2000)
            {
                EntryLayout.TranslateTo(0, -90, 50);
            }
            else
            {
                EntryLayout.TranslateTo(0, 0, 50);
            }
        }

        async void ConfirmarClave(string clave, string confirmarClave)
        {
            if (confirmarClave != clave)
                await DisplayAlert("Contraseñas no coinciden", "Confirme la contraseña nuevamente.", "OK");
        }
        void CamposNoNulosLogin()
        {
            if (txtUser.Text == null && txtPassword.Text == null)
            {
                EntryUsuarioLogin.BorderColor = Color.Red;
                txtUser.Placeholder = "Usuario requerido.";
                txtUser.PlaceholderColor = Color.Red;
                EntryPasswordLogin.BorderColor = Color.Red;
                txtPassword.Placeholder = "Contraseña requerida.";
                txtPassword.PlaceholderColor = Color.Red;
            }
            else if (txtUser.Text == null)
            {
                EntryUsuarioLogin.BorderColor = Color.Red;
                txtUser.Placeholder = "Usuario requerido.";
                txtUser.PlaceholderColor = Color.Red;
            }
            else if (txtPassword.Text == null)
            {
                EntryPasswordLogin.BorderColor = Color.Red;
                txtPassword.Placeholder = "Contraseña requerida.";
                txtPassword.PlaceholderColor = Color.Red;
            }
        }
        async void CamposNoNulosRegistro(string campo1, string campo2, string campo3, string campo4, string campo5, string campo6)
        {
            if (campo1 == null || campo2 == null || campo3 == null || campo4 == null || campo5 == null || campo6 == null)
                await DisplayAlert("Complete los campos", "No se permiten campos vacíos.", "OK");
        }
        void GenerateUserID()
        {
            if (idUser != 0)
                idUser = idUser++;
        }
        void HideElements()
        {
            entryCodigo.IsVisible = false;
            btnConfirmarCodigo.IsVisible = false;
            entryNuevaClave.IsVisible = false;
            entryConfirmarNuevaClave.IsVisible = false;
        }
        void SubscribeTab()
        {
            MessagingCenter.Subscribe<object, int>(this, "click", (arg, idx) =>
            {
                CurrentPage = Children[idx];
            });
        }
        void SendTap()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                MessagingCenter.Send<object, int>(this, "click", 2);
            };
            lblOlvidarClave.GestureRecognizers.Add(tapGestureRecognizer);
        }

        [Obsolete]
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (txtUser.Text == null || txtPassword.Text == null)
            {
                CamposNoNulosLogin();
            }
            else
            {
                EntryUsuarioLogin.BorderColor = Color.Default;
                EntryPasswordLogin.BorderColor = Color.Default;
                claveEncriptada = DataSecurity.Encrypt(txtPassword.Text, "sblw-3hn8-sqoy19");
                var getUsers = await data.GetUsuarios();
                var userLogin = getUsers.Where(x => x.NombreUsuario == txtUser.Text && x.UsuarioClave == claveEncriptada)
                    .Select(y => y.UsuarioRol).FirstOrDefault();
                if (userLogin == "Administrador")
                {
                    await CredencialesCorrectas();
                    await Navigation.PushAsync(new NavigationPage(new MasterMenuPage()));
                }
                else if (userLogin == "Habitante")
                {
                    await CredencialesCorrectas();
                    await Navigation.PushAsync(new NavigationPage(new MasterMenuHabitantePage()));
                }
                else
                {
                    await DisplayAlert("Credenciales incorrectas", "Verifique usuario y/o contraseña.", "OK");
                }
            }
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            CamposNoNulosRegistro(txtNombre.Text, txtApellidos.Text, txtCorreoRegistro.Text, txtNombreUsuario.Text,
                txtClaveRegistro.Text, txtConfirmarClave.Text);
            ConfirmarClave(txtClaveRegistro.Text, txtConfirmarClave.Text);
            try
            {
                GenerateUserID();
                claveEncriptada = DataSecurity.Encrypt(txtClaveRegistro.Text, "sblw-3hn8-sqoy19");
                await data.AgregarUsuario(idUser, txtNombre.Text, txtApellidos.Text, txtCorreoRegistro.Text, txtNombreUsuario.Text,
                    claveEncriptada);
            }
            catch (Exception)
            {
                await DisplayAlert("Error al registrar", "Ha ocurrido un error en el registro.", "OK");
            }
        }

        private void btnCambiarClave_Clicked(object sender, EventArgs e)
        {

        }

        private void btnConfirmarCodigo_Clicked(object sender, EventArgs e)
        {
            entryNuevaClave.IsVisible = true;
            entryConfirmarNuevaClave.IsVisible = true;
        }

        private void btnEnviarCodigo_Clicked(object sender, EventArgs e)
        {
            entryCodigo.IsVisible = true;
            btnConfirmarCodigo.IsVisible = true;
        }

        [Obsolete]
        async Task CredencialesCorrectas()
        {
            await Loading();
            CorrectValidationPage correctValidation = new CorrectValidationPage($"{titleCorrect}{txtUser.Text}!", detailCorrect);
            await PopupNavigation.PushAsync(correctValidation);
        }

        [Obsolete]
        async Task Loading()
        {
            LoadingPage loadingPage = new LoadingPage(textDetailLoading);
            await PopupNavigation.PushAsync(loadingPage);
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingPage);
        }
    }
}