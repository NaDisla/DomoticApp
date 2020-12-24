using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DomoticApp.DataHelpers
{
    public class CambiarClaveBackend
    {
        Random codigoCambio = new Random();
        GeneralData data = new GeneralData();
        ResultsOperations results = new ResultsOperations();
        string detailLoading, titleError, detailError, titleCorrect, detailCorrect, titleAlert, detailAlert;

        #region EnviarCodigo
        [Obsolete]
        public void EnviarCodigo(Entry txtUsuarioCambioClave, Frame frameUsuarioCambiarClave, Button btnEnviarCodigo,
            Frame frameCodigo, Button btnConfirmarCodigo)
        {
            if (string.IsNullOrEmpty(txtUsuarioCambioClave.Text))
            {
                frameUsuarioCambiarClave.BorderColor = Color.Red;
                txtUsuarioCambioClave.PlaceholderColor = Color.Red;
                txtUsuarioCambioClave.Placeholder = "Usuario requerido.";
            }
            else
            {
                frameUsuarioCambiarClave.BorderColor = Color.Default;
                txtUsuarioCambioClave.PlaceholderColor = Color.Default;
                EnviarCodigoCorreo(txtUsuarioCambioClave, frameUsuarioCambiarClave, btnEnviarCodigo, frameCodigo, btnConfirmarCodigo);
            }
        }
        #endregion

        #region EnviarCorreo
        [Obsolete]
        async void EnviarCodigoCorreo(Entry txtUsuarioCambioClave, Frame frameUsuarioCambiarClave, Button btnEnviarCodigo, 
            Frame frameCodigo, Button btnConfirmarCodigo)
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
                mail.From = new MailAddress("no.reply.domoticapp@gmail.com", "DomoticApp");

                detailLoading = "Validando usuario...";
                LoadingPage loadingUsuario = new LoadingPage(detailLoading);
                await PopupNavigation.PushAsync(loadingUsuario);
                await Task.Delay(2000);
                var getUsers = await data.GetUsuarios();
                var userCorreo = getUsers.Where(x => x.UsuarioNombre == txtUsuarioCambioClave.Text)
                    .Select(y => y.UsuarioCorreo).FirstOrDefault();

                if (userCorreo != null)
                {
                    await PopupNavigation.RemovePageAsync(loadingUsuario);
                    detailLoading = "Enviando código a su correo electrónico...";
                    LoadingPage loadingSend = new LoadingPage(detailLoading);
                    await PopupNavigation.PushAsync(loadingSend);
                    await Task.Delay(2500);

                    var getUserCodigo = await data.GetUsuariosCambioClave();
                    var userCodigo = getUserCodigo.Where(x => x.NombreUsuario == txtUsuarioCambioClave.Text).ToList();

                    if (userCodigo.Count == 0)
                    {
                        ConfigEnviarCorreo(mail, SmtpServer, userCorreo, loadingSend, result);
                        await data.CambiarClave(codigo, txtUsuarioCambioClave.Text);
                        ControlesCodigo(frameUsuarioCambiarClave, btnEnviarCodigo, frameCodigo, btnConfirmarCodigo);
                    }
                    else
                    {
                        ConfigEnviarCorreo(mail, SmtpServer, userCorreo, loadingSend, result);
                        await data.UpdateCodigoUsuario(txtUsuarioCambioClave.Text, codigo);
                        ControlesCodigo(frameUsuarioCambiarClave, btnEnviarCodigo, frameCodigo, btnConfirmarCodigo);
                    }
                }
                else
                {
                    await PopupNavigation.RemovePageAsync(loadingUsuario);
                    titleError = "Error";
                    detailError = "El usuario ingresado no existe. Intente nuevamente.";
                    await results.Unsuccess(titleError, detailError);
                }
            }
            catch (Exception)
            {
                titleError = "Error";
                detailError = "Ha ocurrido un error procesando el envío. Intente nuevamente.";
                await results.Unsuccess(titleError, detailError);
            }
        }
        #endregion

        #region ConfigEnvioCorreo
        [Obsolete]
        async void ConfigEnviarCorreo(MailMessage mail, SmtpClient smtpServer, string correo, LoadingPage loading, string resultMail)
        {
            mail.To.Add(correo);
            mail.Subject = "Recuperación de contraseña";
            mail.IsBodyHtml = true;
            mail.Body = resultMail;

            smtpServer.Port = 587;
            smtpServer.Host = "smtp.gmail.com";
            smtpServer.EnableSsl = true;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new System.Net.NetworkCredential("no.reply.domoticapp@gmail.com", "domotic0131");

            smtpServer.Send(mail);
            await PopupNavigation.RemovePageAsync(loading);
            titleCorrect = "Verificar correo electrónico";
            detailCorrect = "Se ha enviado el código de cambio de contraseña a su correo electrónico.";
            await results.Success(titleCorrect, detailCorrect);
        }
        #endregion

        #region ControlesCodigo
        void ControlesCodigo(Frame frameUsuarioCambiarClave, Button btnEnviarCodigo, Frame frameCodigo, Button btnConfirmarCodigo)
        {
            frameUsuarioCambiarClave.IsVisible = false;
            btnEnviarCodigo.IsVisible = false;
            frameCodigo.IsVisible = true;
            btnConfirmarCodigo.IsVisible = true;
        }
        #endregion

        #region ConfirmarCodigoClicked
        [Obsolete]
        public void ConfirmarCodigo(Entry txtCodigo, Frame frameCodigo, Frame frameUsuarioCambiarClave, Entry txtUsuarioCambioClave, 
            Button btnConfirmarCodigo, Frame frameNuevaClave, Frame frameConfirmarNuevaClave, Button btnCambiarClave)
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                frameCodigo.BorderColor = Color.Red;
                txtCodigo.Placeholder = "Código requerido";
                txtCodigo.PlaceholderColor = Color.Red;
            }
            else
            {
                frameUsuarioCambiarClave.BorderColor = Color.Default;
                txtUsuarioCambioClave.PlaceholderColor = Color.FromHex("#1E619A");
                ValidarCodigo(txtCodigo, txtUsuarioCambioClave, frameCodigo, btnConfirmarCodigo, frameNuevaClave, frameConfirmarNuevaClave, btnCambiarClave);
            }
        }
        #endregion

        #region ConfirmarCodigoLogic
        [Obsolete]
        async void ValidarCodigo(Entry txtCodigo, Entry txtUsuarioCambioClave, Frame frameCodigo, Button btnConfirmarCodigo, 
            Frame frameNuevaClave, Frame frameConfirmarNuevaClave, Button btnCambiarClave)
        {
            detailLoading = "Validando código...";
            LoadingPage loadingCodigo = new LoadingPage(detailLoading);
            await PopupNavigation.PushAsync(loadingCodigo);
            await Task.Delay(2000);
            var getUsersClaveCambio = await data.GetUsuariosCambioClave();
            var userCodigo = getUsersClaveCambio.Where(x => x.NombreUsuario == txtUsuarioCambioClave.Text)
                .Select(y => y.CodigoCambio).FirstOrDefault();

            if (userCodigo == int.Parse(txtCodigo.Text))
            {
                await PopupNavigation.RemovePageAsync(loadingCodigo);
                titleCorrect = "Código correcto";
                detailCorrect = "Validación exitosa. Puede proceder a cambiar su contraseña.";
                await results.Success(titleCorrect, detailCorrect);
                ControlesNuevaClave(frameCodigo, btnConfirmarCodigo, frameNuevaClave, frameConfirmarNuevaClave, btnCambiarClave);
            }
            else
            {
                await PopupNavigation.RemovePageAsync(loadingCodigo);
                titleError = "Error";
                detailError = "El código ingresado no es correcto. Intente nuevamente.";
                await results.Unsuccess(titleError, detailError);
            }
        }
        #endregion

        #region ControlesNuevaClave
        void ControlesNuevaClave(Frame frameCodigo, Button btnConfirmarCodigo, Frame frameNuevaClave, Frame frameConfirmarNuevaClave, Button btnCambiarClave)
        {
            frameCodigo.IsVisible = false;
            btnConfirmarCodigo.IsVisible = false;
            frameNuevaClave.IsVisible = true;
            frameConfirmarNuevaClave.IsVisible = true;
            btnCambiarClave.IsVisible = true;
        }
        #endregion

        #region ReinicioControles
        void ReinicioControles(Frame frameUsuarioCambiarClave, Button btnEnviarCodigo, Frame frameNuevaClave, Frame frameConfirmarNuevaClave, 
            Button btnCambiarClave, Entry txtUsuarioCambioClave)
        {
            frameNuevaClave.IsVisible = false;
            frameConfirmarNuevaClave.IsVisible = false;
            btnCambiarClave.IsVisible = false;
            frameUsuarioCambiarClave.IsVisible = true;
            btnEnviarCodigo.IsVisible = true;
            txtUsuarioCambioClave.Text = null;
            txtUsuarioCambioClave.PlaceholderColor = Color.FromHex("#1E619A");
        }
        #endregion

        #region CambiarClave
        [Obsolete]
        public async void CambiarClave(Frame frameUsuarioCambiarClave, Button btnEnviarCodigo, Entry txtUsuarioCambioClave, Entry txtNuevaClave, 
            Entry txtConfirmarClaveNueva, Frame frameNuevaClave, Frame frameConfirmarNuevaClave, Button btnCambiarClave)
        {
            if (string.IsNullOrEmpty(txtNuevaClave.Text) || string.IsNullOrEmpty(txtConfirmarClaveNueva.Text))
            {
                frameNuevaClave.BorderColor = Color.Red;
                txtNuevaClave.Placeholder = "Contraseña requerida.";
                txtNuevaClave.PlaceholderColor = Color.Red;
                frameConfirmarNuevaClave.BorderColor = Color.Red;
                txtConfirmarClaveNueva.Placeholder = "Confirmación requerida.";
                txtConfirmarClaveNueva.PlaceholderColor = Color.Red;
            }
            else
            {
                if (txtConfirmarClaveNueva.Text != txtNuevaClave.Text)
                {
                    titleAlert = "Contraseñas no coinciden";
                    detailAlert = "Las contraseñas no coinciden. Intente nuevamente.";
                    await results.Alert(titleAlert, detailAlert);
                }
                else
                {
                    detailLoading = "Actualizando contraseña...";
                    LoadingPage loading = new LoadingPage(detailLoading);
                    await PopupNavigation.PushAsync(loading);
                    await Task.Delay(2500);

                    try
                    {
                        var getUsers = await data.GetUsuarios();
                        var idUsuario = getUsers.Where(y => y.UsuarioNombre == txtUsuarioCambioClave.Text).Select(x => x.UsuarioID).FirstOrDefault();
                        var infoUsuario = await data.GetUsuario(idUsuario);
                        int usuarioID = infoUsuario.UsuarioID;
                        string nombreRealUsuario = infoUsuario.UsuarioNombreReal;
                        string nombreUsuario = infoUsuario.UsuarioNombre;
                        string claveEncriptada = DataSecurity.Encrypt(txtNuevaClave.Text, "sblw-3hn8-sqoy19");
                        string rolUsuario = infoUsuario.UsuarioRol;
                        string correoUsuario = infoUsuario.UsuarioCorreo;
                        string accesoUsuario = infoUsuario.Acceso;
                        await data.UpdateUsuario(idUsuario, nombreRealUsuario, correoUsuario, nombreUsuario, claveEncriptada, rolUsuario, accesoUsuario);
                        await PopupNavigation.RemovePageAsync(loading);
                        titleCorrect = "Contraseña actualizada";
                        detailCorrect = "Se ha cambiado la contraseña exitosamente.";
                        await results.Success(titleCorrect, detailCorrect);
                        ReinicioControles(frameUsuarioCambiarClave, btnEnviarCodigo, frameNuevaClave, frameConfirmarNuevaClave, btnCambiarClave,
                            txtUsuarioCambioClave);
                    }
                    catch (Exception)
                    {
                        await PopupNavigation.RemovePageAsync(loading);
                        titleError = "Error";
                        detailError = "Ha ocurrido un error procesando el cambio de contraseña. Intente nuevamente.";
                        await results.Unsuccess(titleError, detailError);
                        ReinicioControles(frameUsuarioCambiarClave, btnEnviarCodigo, frameNuevaClave, frameConfirmarNuevaClave, btnCambiarClave,
                            txtUsuarioCambioClave);
                    }
                }
            }
        }
        #endregion
    }
}