using DomoticApp.Views.MasterMenu;
using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DomoticApp.DataHelpers
{
    public partial class LoginBackend
    {
        //public INavigation Navigation;
        string claveEncriptada, titleCorrect, detailCorrect, titleError, detailError, detailLoading;
        int result;
        GeneralData data = new GeneralData();
        ResultsOperations results = new ResultsOperations();

        /*public LoginBackend(INavigation navigation)
        {
            Navigation = navigation;
        }*/

        #region ValidarNoNulos
        public void CamposNoNulos(Entry txtUser, Entry txtPassword, Frame frameUser, Frame framePassword)
        {
            if (string.IsNullOrEmpty(txtUser.Text) && string.IsNullOrEmpty(txtPassword.Text))
            {
                frameUser.BorderColor = Color.Red;
                txtUser.Placeholder = "Usuario requerido.";
                txtUser.PlaceholderColor = Color.Red;
                framePassword.BorderColor = Color.Red;
                txtPassword.Placeholder = "Contraseña requerida.";
                txtPassword.PlaceholderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtUser.Text))
            {
                frameUser.BorderColor = Color.Red;
                txtUser.Placeholder = "Usuario requerido.";
                txtUser.PlaceholderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                framePassword.BorderColor = Color.Red;
                txtPassword.Placeholder = "Contraseña requerida.";
                txtPassword.PlaceholderColor = Color.Red;
            }
        }
        #endregion

        #region Login
        [Obsolete]
        public async Task<int> Login(Entry txtUser, Entry txtPassword, Frame frameUser, Frame framePassword)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                CamposNoNulos(txtUser, txtPassword, frameUser, framePassword);
            }
            else
            {
                frameUser.BorderColor = Color.Default;
                framePassword.BorderColor = Color.Default;
                claveEncriptada = DataSecurity.Encrypt(txtPassword.Text, "sblw-3hn8-sqoy19");
                titleCorrect = $"¡Bienvenido(a) {txtUser.Text}!";
                detailCorrect = "Ha iniciado sesión correctamente.";
                detailLoading = "Validando credenciales...";

                LoadingPage loading = new LoadingPage(detailLoading);
                await PopupNavigation.PushAsync(loading);
                await Task.Delay(2000);
                var getUsers = await data.GetUsuarios();
                var userLogin = getUsers.Where(x => x.NombreUsuario == txtUser.Text && x.UsuarioClave == claveEncriptada)
                    .Select(y => y.UsuarioRol).FirstOrDefault();

                if (userLogin == "Administrador")
                {
                    await PopupNavigation.RemovePageAsync(loading);
                    await results.Success(titleCorrect, detailCorrect);
                    await SecureStorage.SetAsync("isLogged", "1");
                    result = 1;
                }
                else if (userLogin == "Habitante")
                {
                    await PopupNavigation.RemovePageAsync(loading);
                    await results.Success(titleCorrect, detailCorrect);
                    await SecureStorage.SetAsync("isLogged", "2");
                    result = 2;
                }
                else
                {
                    await PopupNavigation.RemovePageAsync(loading);
                    titleError = "Credenciales incorrectas";
                    detailError = "Verifique usuario y/o contraseña.";
                    await results.Unsuccess(titleError, detailError);
                }
            }
            return result;
        }
        #endregion
    }
}