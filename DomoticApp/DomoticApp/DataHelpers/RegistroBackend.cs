using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DomoticApp.DataHelpers
{
    public class RegistroBackend
    {
        ResultsOperations results = new ResultsOperations();
        GeneralData data = new GeneralData();
        string detailLoading, claveEncriptada, titleCorrect, detailCorrect, titleError, detailError;
        int idUser;
        bool result;

        #region ValidarNoNulos
        void CamposNoNulos(Entry txtNombreUsuario, Entry txtNombreCompleto, Entry txtCorreoRegistro, Entry txtClaveRegistro,
            Entry txtConfirmarClaveRegistro, Frame frameNombreRealRegistro, Frame frameUsuarioRegistro, Frame frameCorreoRegistro, Frame frameClaveRegistro,
            Frame frameConfirmarClaveRegistro)
        {
            if (string.IsNullOrEmpty(txtNombreUsuario.Text) && string.IsNullOrEmpty(txtNombreCompleto.Text)
                && string.IsNullOrEmpty(txtCorreoRegistro.Text) && string.IsNullOrEmpty(txtClaveRegistro.Text) && string.IsNullOrEmpty(txtConfirmarClaveRegistro.Text))
            {
                frameNombreRealRegistro.BorderColor = Color.Red;
                txtNombreCompleto.Placeholder = "Nombre requerido.";
                txtNombreCompleto.PlaceholderColor = Color.Red;
                frameUsuarioRegistro.BorderColor = Color.Red;
                txtNombreUsuario.Placeholder = "Usuario requerido.";
                txtNombreUsuario.PlaceholderColor = Color.Red;
                frameCorreoRegistro.BorderColor = Color.Red;
                txtCorreoRegistro.Placeholder = "Correo requerido.";
                txtCorreoRegistro.PlaceholderColor = Color.Red;
                frameClaveRegistro.BorderColor = Color.Red;
                txtClaveRegistro.Placeholder = "Contraseña requerida.";
                txtClaveRegistro.PlaceholderColor = Color.Red;
                frameConfirmarClaveRegistro.BorderColor = Color.Red;
                txtConfirmarClaveRegistro.Placeholder = "Confirmación requerida.";
                txtConfirmarClaveRegistro.PlaceholderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtNombreUsuario.Text))
            {
                frameUsuarioRegistro.BorderColor = Color.Red;
                txtNombreUsuario.Placeholder = "Usuario requerido.";
                txtNombreUsuario.PlaceholderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtNombreCompleto.Text))
            {
                frameNombreRealRegistro.BorderColor = Color.Red;
                txtNombreCompleto.Placeholder = "Nombre requerido.";
                txtNombreCompleto.PlaceholderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtCorreoRegistro.Text))
            {
                frameCorreoRegistro.BorderColor = Color.Red;
                txtCorreoRegistro.Placeholder = "Correo requerido.";
                txtCorreoRegistro.PlaceholderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtClaveRegistro.Text))
            {
                frameClaveRegistro.BorderColor = Color.Red;
                txtClaveRegistro.Placeholder = "Contraseña requerida.";
                txtClaveRegistro.PlaceholderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtConfirmarClaveRegistro.Text))
            {
                frameConfirmarClaveRegistro.BorderColor = Color.Red;
                txtConfirmarClaveRegistro.Placeholder = "Confirmación requerida.";
                txtConfirmarClaveRegistro.PlaceholderColor = Color.Red;
            }
        }
        #endregion

        #region RegistroClicked
        [Obsolete]
        public async Task<bool> Registro(Entry txtNombreUsuario, Entry txtNombreCompleto, Entry txtCorreoRegistro, Entry txtClaveRegistro,
            Entry txtConfirmarClaveRegistro, Frame frameNombreRealRegistro, Frame frameUsuarioRegistro, Frame frameCorreoRegistro, Frame frameClaveRegistro,
            Frame frameConfirmarClaveRegistro)
        {
            var correo = txtCorreoRegistro.Text;
            var emailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

            if (string.IsNullOrEmpty(txtNombreCompleto.Text) || string.IsNullOrEmpty(txtNombreUsuario.Text) || string.IsNullOrEmpty(txtCorreoRegistro.Text)
                || string.IsNullOrEmpty(txtClaveRegistro.Text) || string.IsNullOrEmpty(txtConfirmarClaveRegistro.Text))
            {
                CamposNoNulos(txtNombreUsuario, txtNombreCompleto, txtCorreoRegistro, txtClaveRegistro, txtConfirmarClaveRegistro, frameNombreRealRegistro,
                    frameUsuarioRegistro, frameCorreoRegistro, frameClaveRegistro, frameConfirmarClaveRegistro);
            }
            else if (!Regex.IsMatch(correo, emailPattern))
            {
                titleError = "Correo no válido";
                detailError = "El correo ingresado no es correcto. Intente nuevamente.";
                await results.Unsuccess(titleError, detailError);
            }
            else if(!txtNombreCompleto.Text.ToCharArray().All(Char.IsLetter))
            {
                titleError = "Nombre incorrecto";
                detailError = "El nombre no puede contener números. Intente nuevamente.";
                await results.Unsuccess(titleError, detailError);
            }
            else
            {
                frameNombreRealRegistro.BorderColor = Color.Default;
                frameClaveRegistro.BorderColor = Color.Default;
                frameConfirmarClaveRegistro.BorderColor = Color.Default;
                frameCorreoRegistro.BorderColor = Color.Default;
                frameUsuarioRegistro.BorderColor = Color.Default;

                txtNombreCompleto.Placeholder = "Nombre completo";
                txtNombreCompleto.PlaceholderColor = Color.FromHex("#1E619A");

                txtNombreUsuario.Placeholder = "Nombre de usuario";
                txtNombreUsuario.PlaceholderColor = Color.FromHex("#1E619A");

                txtCorreoRegistro.Placeholder = "Correo electrónico";
                txtCorreoRegistro.PlaceholderColor = Color.FromHex("#1E619A");

                txtClaveRegistro.Placeholder = "Contraseña";
                txtClaveRegistro.PlaceholderColor = Color.FromHex("#1E619A");

                txtConfirmarClaveRegistro.Placeholder = "Confirmar contraseña";
                txtConfirmarClaveRegistro.PlaceholderColor = Color.FromHex("#1E619A");

                if (txtConfirmarClaveRegistro.Text != txtClaveRegistro.Text)
                {
                    await results.Alert();
                }
                else
                {
                    detailLoading = "Procesando registro...";
                    LoadingPage loading = new LoadingPage(detailLoading);
                    await PopupNavigation.PushAsync(loading);
                    await Task.Delay(2000);
                    try
                    {
                        var getUsuarios = await data.GetUsuarios();
                        var usuarioExiste = getUsuarios.Where(x => x.UsuarioNombre == txtNombreUsuario.Text).Select(y => y.UsuarioNombre).FirstOrDefault();

                        if (getUsuarios.Count == 0)
                        {
                            idUser = 1000;
                            if (usuarioExiste != null)
                            {
                                await PopupNavigation.RemovePageAsync(loading);
                                titleError = "Usuario incorrecto";
                                detailError = "El nombre de usuario es incorrecto. Intente nuevamente.";
                                await results.Unsuccess(titleError, detailError);
                                result = false;
                            }
                            else
                            {
                                RegistroUsuario(idUser, loading, txtClaveRegistro, txtNombreCompleto, txtCorreoRegistro, txtNombreUsuario, txtConfirmarClaveRegistro);
                                result = true;
                            }
                        }
                        else
                        {
                            var getUserID = getUsuarios.Select(x => x.UsuarioID).LastOrDefault();
                            getUserID++;
                            if (usuarioExiste != null)
                            {
                                await PopupNavigation.RemovePageAsync(loading);
                                titleError = "Usuario existente";
                                detailError = "Este usuario ya está registrado. Intente nuevamente.";
                                await results.Unsuccess(titleError, detailError);
                                result = false;
                            }
                            else
                            {
                                RegistroUsuario(getUserID, loading, txtClaveRegistro, txtNombreCompleto, txtCorreoRegistro, txtNombreUsuario, txtConfirmarClaveRegistro);
                                result = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        await PopupNavigation.RemovePageAsync(loading);
                        titleError = "Error";
                        detailError = "Ha ocurrido un error procesando el registro. Intente nuevamente.";
                        await results.Unsuccess(titleError, detailError);
                        result = false;
                    }
                }
            }
            return result;
        }
        #endregion

        #region RegistroLogic
        [Obsolete]
        async void RegistroUsuario(int idUsuario, LoadingPage pageLoading, Entry txtClaveRegistro, Entry txtNombreCompleto, Entry txtCorreoRegistro,
            Entry txtNombreUsuario, Entry txtConfirmarClaveRegistro)
        {
            claveEncriptada = DataSecurity.Encrypt(txtClaveRegistro.Text, "sblw-3hn8-sqoy19");
            await data.AgregarUsuario(idUsuario, txtNombreCompleto.Text, txtCorreoRegistro.Text, txtNombreUsuario.Text, claveEncriptada);
            await PopupNavigation.RemovePageAsync(pageLoading);

            titleCorrect = "Registro exitoso";
            detailCorrect = "Se ha registrado correctamente. Puede iniciar sesión.";
            await results.Success(titleCorrect, detailCorrect);

            txtNombreCompleto.Text = null;
            txtNombreUsuario.Text = null;
            txtCorreoRegistro.Text = null;
            txtClaveRegistro.Text = null;
            txtConfirmarClaveRegistro.Text = null;
        }
        #endregion
    }
}
