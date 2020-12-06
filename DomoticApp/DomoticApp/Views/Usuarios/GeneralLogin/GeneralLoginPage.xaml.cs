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
using System.Reflection;
using System.IO;
using System.Net.Mail;

namespace DomoticApp.Views.Usuarios.GeneralLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralLoginPage : ContentPage
    {
        GeneralData data = new GeneralData();
        Random codigoCambio = new Random();
        string claveEncriptada, detailLoading , titleCorrect,
            detailCorrect, titleError, detailError;
        int idUser = 1000;

        public GeneralLoginPage()
        {
            InitializeComponent();
            //DetectarFocoEntry();
            //ContainerLoginInitialPosition();
            //SubscribeTab();
            //SendTap();
            HideElements();
            SecureStorage.RemoveAll();
        }
        //void DetectarFocoEntry()
        //{
        //    txtUser.Focused += (s, e) => { SetContainerLoginPosition(onFocus: true); };
        //    txtUser.Unfocused += (s, e) => { SetContainerLoginPosition(onFocus: false); };
        //    txtPassword.Focused += (s, e) => { SetContainerLoginPosition(onFocus: true); };
        //    txtPassword.Unfocused += (s, e) => { SetContainerLoginPosition(onFocus: false); };
        //}
        //void ContainerLoginInitialPosition()
        //{
        //    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        //    var height = mainDisplayInfo.Height;

        //    if(height > 2000 || height <= 2000)
        //    {
        //        ContainerLogin.VerticalOptions = LayoutOptions.Center;
        //        if(height <= 2000)
        //        {
        //            imgLogin.HeightRequest = 100;
        //            imgLogin.WidthRequest = 100;
        //            FrameLogin.HeightRequest = 330;
        //            EntryUsuarioLogin.HeightRequest = 40;
        //            EntryPasswordLogin.HeightRequest = 40;
        //            BaseImageUser.HeightRequest = 20;
        //            BaseImageUser.WidthRequest = 20;
        //            BaseImagePassword.HeightRequest = 20;
        //            BaseImagePassword.WidthRequest = 20;
        //            btnHidePassword.HeightRequest = 25;
        //            btnHidePassword.WidthRequest = 25;
        //            LayoutOlvidoClave.Margin = new Thickness(0, 15, 0, 0);
        //        }
        //    }
        //}
        //void SetContainerLoginPosition(bool onFocus)
        //{
        //    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        //    var height = mainDisplayInfo.Height;

        //    if (onFocus && height <= 2000)
        //    {
        //        ContainerLogin.TranslateTo(0, -75, 50);
        //    }
        //    else if(onFocus && height > 2000)
        //    {
        //        ContainerLogin.TranslateTo(0, -110, 50);
        //    }
        //    else
        //    {
        //        ContainerLogin.TranslateTo(0, 0, 50);
        //    }
        //}
        async void ConfirmarClave(string clave, string confirmarClave)
        {
            if (confirmarClave != clave)
                await DisplayAlert("Contraseñas no coinciden", "Confirme la contraseña nuevamente.", "OK");
        }
        //void CamposNoNulosLogin()
        //{
        //    if (string.IsNullOrEmpty(txtUser.Text) && string.IsNullOrEmpty(txtPassword.Text))
        //    {
        //        EntryUsuarioLogin.BorderColor = Color.Red;
        //        txtUser.Placeholder = "Usuario requerido.";
        //        txtUser.PlaceholderColor = Color.Red;
        //        EntryPasswordLogin.BorderColor = Color.Red;
        //        txtPassword.Placeholder = "Contraseña requerida.";
        //        txtPassword.PlaceholderColor = Color.Red;
        //    }
        //    else if (string.IsNullOrEmpty(txtUser.Text))
        //    {
        //        EntryUsuarioLogin.BorderColor = Color.Red;
        //        txtUser.Placeholder = "Usuario requerido.";
        //        txtUser.PlaceholderColor = Color.Red;
        //    }
        //    else if (string.IsNullOrEmpty(txtPassword.Text))
        //    {
        //        EntryPasswordLogin.BorderColor = Color.Red;
        //        txtPassword.Placeholder = "Contraseña requerida.";
        //        txtPassword.PlaceholderColor = Color.Red;
        //    }
        //}
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
        //void SubscribeTab()
        //{
        //    MessagingCenter.Subscribe<object, int>(this, "click", (arg, idx) =>
        //    {
        //        CurrentPage = Children[idx];
        //    });
        //}
        //void SendTap()
        //{
        //    var tapGestureRecognizer = new TapGestureRecognizer();
        //    tapGestureRecognizer.Tapped += (s, e) =>
        //    {
        //        MessagingCenter.Send<object, int>(this, "click", 2);
        //    };
        //    lblOlvidarClave.GestureRecognizers.Add(tapGestureRecognizer);
        //}
        /*[Obsolete]
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                CamposNoNulosLogin();
            }
            else
            {
                EntryUsuarioLogin.BorderColor = Color.Default;
                EntryPasswordLogin.BorderColor = Color.Default;
                claveEncriptada = DataSecurity.Encrypt(txtPassword.Text, "sblw-3hn8-sqoy19");
                titleCorrect = $"¡Bienvenido(a) {txtUser.Text}!";
                detailCorrect = "Ha iniciado sesión correctamente.";
                detailLoading = "Validando credenciales...";
                
                await Loading(detailLoading);

                var getUsers = await data.GetUsuarios();
                var userLogin = getUsers.Where(x => x.NombreUsuario == txtUser.Text && x.UsuarioClave == claveEncriptada)
                    .Select(y => y.UsuarioRol).FirstOrDefault();

                if (userLogin == "Administrador")
                {
                    await Success(titleCorrect, detailCorrect);
                    await SecureStorage.SetAsync("isLogged", "1");
                    await Navigation.PushAsync(new MasterMenuPage());
                }
                else if (userLogin == "Habitante")
                {
                    await Success(titleCorrect, detailCorrect);
                    await SecureStorage.SetAsync("isLogged", "2");
                    await Navigation.PushAsync(new MasterMenuHabitantePage());
                }
                else
                {
                    titleError = "Credenciales incorrectas";
                    detailError = "Verifique usuario y/o contraseña.";
                    await CredencialesIncorrectas(titleError, detailError);
                }
            }
        }*/
        //private async void btnRegistro_Clicked(object sender, EventArgs e)
        //{
        //    CamposNoNulosRegistro(txtNombreCompleto.Text, txtCorreoRegistro.Text, txtNombreUsuario.Text,
        //        txtClaveRegistro.Text, txtConfirmarClave.Text);
        //    ConfirmarClave(txtClaveRegistro.Text, txtConfirmarClave.Text);
        //    try
        //    {
        //        GenerateUserID();
        //        claveEncriptada = DataSecurity.Encrypt(txtClaveRegistro.Text, "sblw-3hn8-sqoy19");
        //        await data.AgregarUsuario(idUser, txtNombreCompleto.Text, txtCorreoRegistro.Text, txtNombreUsuario.Text,
        //            claveEncriptada);

        //    }
        //    catch (Exception)
        //    {
        //        await DisplayAlert("Error al registrar", "Ha ocurrido un error en el registro.", "OK");
        //    }
        //}

        private void btnCambiarClave_Clicked(object sender, EventArgs e)
        {

        }
        [Obsolete]
        private void btnConfirmarCodigo_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCodigo.Text))
            {
                entryCodigo.BorderColor = Color.Red;
                txtCodigo.Placeholder = "Código requerido";
                txtCodigo.PlaceholderColor = Color.Red;
            }
            else
            {
                entryUsuarioCambiarClave.BorderColor = Color.Default;
                txtUsuarioCambioClave.PlaceholderColor = Color.Default;
                ConfirmarCodigo();
            }
        }
        [Obsolete]
        async void ConfirmarCodigo()
        {
            detailLoading = "Validando código...";
            LoadingPage loadingCodigo = new LoadingPage(detailLoading);
            await PopupNavigation.PushAsync(loadingCodigo);
            await Task.Delay(2000);
            var getUsersClaveCambio = await data.GetUsuariosCambioClave();
            var userCodigo = getUsersClaveCambio.Where(x => x.NombreUsuario == txtUsuarioCambioClave.Text)
                .Select(y => y.CodigoCambio).FirstOrDefault();
            if(userCodigo == int.Parse(txtCodigo.Text))
            {
                await PopupNavigation.RemovePageAsync(loadingCodigo);
                titleCorrect = "Código correcto";
                detailCorrect = "Validación exitosa. Puede proceder a cambiar su contraseña.";
                await Success(titleCorrect, detailCorrect);
                entryCodigo.IsEnabled = false;
                txtCodigo.IsEnabled = false;
                btnConfirmarCodigo.IsEnabled = false;
                entryNuevaClave.IsVisible = true;
                entryConfirmarNuevaClave.IsVisible = true;
                btnCambiarClave.IsVisible = true;
            }
            else
            {
                await PopupNavigation.RemovePageAsync(loadingCodigo);
                titleError = "Error";
                detailError = "El código ingresado no es correcto. Intente nuevamente.";
                await Unsuccess(titleError, detailError);
            }
        }
        [Obsolete]
        private void btnEnviarCodigo_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuarioCambioClave.Text))
            {
                entryUsuarioCambiarClave.BorderColor = Color.Red;
                txtUsuarioCambioClave.PlaceholderColor = Color.Red;
                txtUsuarioCambioClave.Placeholder = "Usuario requerido.";
            }
            else
            {
                entryUsuarioCambiarClave.BorderColor = Color.Default;
                txtUsuarioCambioClave.PlaceholderColor = Color.Default;
                EnviarCodigoCorreo();
            }
        }
        [Obsolete]
        async void EnviarCodigoCorreo()
        {
            try
            {
                int codigo = codigoCambio.Next(100000, 999999);
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "DomoticApp.Views.Templates.MailTemplate.html";
                string result = "";
                Stream stream = assembly.GetManifestResourceStream(resourceName);

                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                    result = result.Replace("{codigo}", $"{codigo}");
                }

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("no.reply.domoticapp@gmail.com","DomoticApp");

                detailLoading = "Validando usuario...";
                LoadingPage loadingUsuario = new LoadingPage(detailLoading);
                await PopupNavigation.PushAsync(loadingUsuario);
                await Task.Delay(2000);
                var getUsers = await data.GetUsuarios();
                var userCorreo = getUsers.Where(x => x.NombreUsuario == txtUsuarioCambioClave.Text)
                    .Select(y => y.UsuarioCorreo).FirstOrDefault();

                if(userCorreo != null)
                {
                    await PopupNavigation.RemovePageAsync(loadingUsuario);
                    detailLoading = "Enviando código al correo...";
                    LoadingPage loadingSend = new LoadingPage(detailLoading);
                    await PopupNavigation.PushAsync(loadingSend);
                    await Task.Delay(2500);

                    var getUserCodigo = await data.GetUsuariosCambioClave();
                    var userCodigo = getUserCodigo.Where(x => x.NombreUsuario == txtUsuarioCambioClave.Text).ToList();

                    if (userCodigo.Count == 0)
                    {
                        mail.To.Add(userCorreo);
                        mail.Subject = "Recuperación de contraseña";
                        mail.IsBodyHtml = true;
                        mail.Body = result;

                        SmtpServer.Port = 587;
                        SmtpServer.Host = "smtp.gmail.com";
                        SmtpServer.EnableSsl = true;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("no.reply.domoticapp@gmail.com", "domotic0131");

                        SmtpServer.Send(mail);
                        await PopupNavigation.RemovePageAsync(loadingSend);
                        titleCorrect = "Verificar correo electrónico";
                        detailCorrect = "Se ha enviado el código de cambio de contraseña a su correo electrónico.";
                        await Success(titleCorrect, detailCorrect);
                        await data.CambiarClave(codigo, txtUsuarioCambioClave.Text);
                        entryUsuarioCambiarClave.IsEnabled = false;
                        txtUsuarioCambioClave.IsEnabled = false;
                        btnEnviarCodigo.IsEnabled = false;
                        entryCodigo.IsVisible = true;
                        btnConfirmarCodigo.IsVisible = true;
                    }
                    else
                    {
                        mail.To.Add(userCorreo);
                        mail.Subject = "Recuperación de contraseña";
                        mail.IsBodyHtml = true;
                        mail.Body = result;

                        SmtpServer.Port = 587;
                        SmtpServer.Host = "smtp.gmail.com";
                        SmtpServer.EnableSsl = true;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("no.reply.domoticapp@gmail.com", "domotic0131");

                        SmtpServer.Send(mail);
                        await PopupNavigation.RemovePageAsync(loadingSend);
                        titleCorrect = "Verificar correo electrónico";
                        detailCorrect = "Se ha enviado el código de cambio de contraseña a su correo electrónico.";
                        await Success(titleCorrect, detailCorrect);
                        await data.UpdateCodigoUsuario(txtUsuarioCambioClave.Text, codigo);
                        entryUsuarioCambiarClave.IsEnabled = false;
                        txtUsuarioCambioClave.IsEnabled = false;
                        btnEnviarCodigo.IsEnabled = false;
                        entryCodigo.IsVisible = true;
                        btnConfirmarCodigo.IsVisible = true;
                    }
                }
                else 
                {
                    await PopupNavigation.RemovePageAsync(loadingUsuario);
                    titleError = "Error";
                    detailError = "El usuario ingresado no existe. Intente nuevamente.";
                    await Unsuccess(titleError, detailError);
                }
            }
            catch (Exception)
            {
                titleError = "Error";
                detailError = "Ha ocurrido un error procesando el envío. Intente nuevamente.";
                await Unsuccess(titleError, detailError);
            }
        }
        [Obsolete]
        async Task Success(string titleSuccess, string detailSuccess)
        {
            CorrectValidationPage correctValidation = new CorrectValidationPage(titleSuccess, detailSuccess);
            await PopupNavigation.PushAsync(correctValidation);
        }
        [Obsolete]
        async Task Unsuccess(string titleUnsuccess, string detailUnsuccess)
        {
            IncorrectValidationPage incorrectValidation = new IncorrectValidationPage(titleUnsuccess, detailUnsuccess);
            await PopupNavigation.PushAsync(incorrectValidation);
        }
        [Obsolete]
        async Task Loading(string detail)
        {
            LoadingPage loadingPage = new LoadingPage(detail);
            await PopupNavigation.PushAsync(loadingPage);
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingPage);
        }
    }
}