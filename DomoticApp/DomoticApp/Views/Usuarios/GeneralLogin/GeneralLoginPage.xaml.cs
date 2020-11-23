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
        string claveEncriptada; 
        const string textDetailLoading = "Validando credenciales...", titleCorrect = "¡Bienvenido(a) ",
            detailCorrect = "Ha iniciado sesión correctamente.", 
            titleError = "Credenciales incorrectas", 
            detailError = "Verifique usuario y/o contraseña.";
        int idUser = 1000;

        public GeneralLoginPage()
        {
            InitializeComponent();
            txtUser.Focused += (s,e) => { SetContainerLoginPosition(onFocus: true); };
            txtUser.Unfocused += (s, e) => { SetContainerLoginPosition(onFocus: false); };
            txtPassword.Focused += (s, e) => { SetContainerLoginPosition(onFocus: true); };
            txtPassword.Unfocused += (s, e) => { SetContainerLoginPosition(onFocus: false); };
            ContainerLoginInitialPosition();
            SubscribeTab();
            SendTap();
            HideElements();
            SecureStorage.RemoveAll();
        }
        private void ContainerLoginInitialPosition()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplayInfo.Height;

            if(height > 2000 || height <= 2000)
            {
                ContainerLogin.VerticalOptions = LayoutOptions.Center;
                if(height <= 2000)
                {
                    imgLogin.HeightRequest = 100;
                    imgLogin.WidthRequest = 100;
                    FrameLogin.HeightRequest = 330;
                    EntryUsuarioLogin.HeightRequest = 40;
                    EntryPasswordLogin.HeightRequest = 40;
                    BaseImageUser.HeightRequest = 20;
                    BaseImageUser.WidthRequest = 20;
                    BaseImagePassword.HeightRequest = 20;
                    BaseImagePassword.WidthRequest = 20;
                    btnHidePassword.HeightRequest = 25;
                    btnHidePassword.WidthRequest = 25;
                    LayoutOlvidoClave.Margin = new Thickness(0, 15, 0, 0);
                }
            }
        }

        private void SetContainerLoginPosition(bool onFocus)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplayInfo.Height;

            if (onFocus && height <= 2000)
            {
                ContainerLogin.TranslateTo(0, -75, 50);
            }
            else if(onFocus && height > 2000)
            {
                ContainerLogin.TranslateTo(0, -110, 50);
            }
            else
            {
                ContainerLogin.TranslateTo(0, 0, 50);
            }
        }

        async void ConfirmarClave(string clave, string confirmarClave)
        {
            if (confirmarClave != clave)
                await DisplayAlert("Contraseñas no coinciden", "Confirme la contraseña nuevamente.", "OK");
        }
        void CamposNoNulosLogin()
        {
            if ((txtUser.Text == null && txtPassword.Text == null) ||(txtUser.Text == "" && txtPassword.Text == ""))
            {
                EntryUsuarioLogin.BorderColor = Color.Red;
                txtUser.Placeholder = "Usuario requerido.";
                txtUser.PlaceholderColor = Color.Red;
                EntryPasswordLogin.BorderColor = Color.Red;
                txtPassword.Placeholder = "Contraseña requerida.";
                txtPassword.PlaceholderColor = Color.Red;
            }
            else if (txtUser.Text == null || txtUser.Text == "")
            {
                EntryUsuarioLogin.BorderColor = Color.Red;
                txtUser.Placeholder = "Usuario requerido.";
                txtUser.PlaceholderColor = Color.Red;
            }
            else if (txtPassword.Text == null || txtPassword.Text == "")
            {
                EntryPasswordLogin.BorderColor = Color.Red;
                txtPassword.Placeholder = "Contraseña requerida.";
                txtPassword.PlaceholderColor = Color.Red;
            }
        }
        async void CamposNoNulosRegistro(string campo1, string campo2, string campo3, string campo4, string campo5)
        {
            if (campo1 == null || campo2 == null || campo3 == null || campo4 == null || campo5 == null)
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
            if (txtUser.Text == null || txtPassword.Text == null || txtUser.Text == "" || txtPassword.Text == "")
            {
                CamposNoNulosLogin();
            }
            else
            {
                EntryUsuarioLogin.BorderColor = Color.Default;
                EntryPasswordLogin.BorderColor = Color.Default;
                claveEncriptada = DataSecurity.Encrypt(txtPassword.Text, "sblw-3hn8-sqoy19");
                await Loading();
                var getUsers = await data.GetUsuarios();
                var userLogin = getUsers.Where(x => x.NombreUsuario == txtUser.Text && x.UsuarioClave == claveEncriptada)
                    .Select(y => y.UsuarioRol).FirstOrDefault();
                
                if (userLogin == "Administrador")
                {
                    await CredencialesCorrectas();
                    await SecureStorage.SetAsync("isLogged", "1");
                    await Navigation.PushAsync(new MasterMenuPage());
                }
                else if (userLogin == "Habitante")
                {
                    await CredencialesCorrectas();
                    await SecureStorage.SetAsync("isLogged", "2");
                    await Navigation.PushAsync(new MasterMenuHabitantePage());
                }
                else
                {
                    await CredencialesIncorrectas();
                }
            }
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            CamposNoNulosRegistro(txtNombreCompleto.Text, txtCorreoRegistro.Text, txtNombreUsuario.Text,
                txtClaveRegistro.Text, txtConfirmarClave.Text);
            ConfirmarClave(txtClaveRegistro.Text, txtConfirmarClave.Text);
            try
            {
                GenerateUserID();
                claveEncriptada = DataSecurity.Encrypt(txtClaveRegistro.Text, "sblw-3hn8-sqoy19");
                await data.AgregarUsuario(idUser, txtNombreCompleto.Text, txtCorreoRegistro.Text, txtNombreUsuario.Text,
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
            CorrectValidationPage correctValidation = new CorrectValidationPage($"{titleCorrect}{txtUser.Text}!", detailCorrect);
            await PopupNavigation.PushAsync(correctValidation);
        }

        [Obsolete]
        async Task CredencialesIncorrectas()
        {
            IncorrectValidationPage incorrectValidation = new IncorrectValidationPage($"{titleError}", detailError);
            await PopupNavigation.PushAsync(incorrectValidation);
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