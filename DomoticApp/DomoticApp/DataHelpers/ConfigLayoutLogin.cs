using Xamarin.Essentials;
using Xamarin.Forms;

namespace DomoticApp.DataHelpers
{
    public class ConfigLayoutLogin
    {
        public void DetectarFocoEntry(Entry entryUser, Entry entryPassword, StackLayout containerLogin)
        {
            entryUser.Focused += (s, e) => { SetContainerLoginPosition(onFocus: true, containerLogin); };
            entryUser.Unfocused += (s, e) => { SetContainerLoginPosition(onFocus: false, containerLogin); };
            entryPassword.Focused += (s, e) => { SetContainerLoginPosition(onFocus: true, containerLogin); };
            entryPassword.Unfocused += (s, e) => { SetContainerLoginPosition(onFocus: false, containerLogin); };
        }

        public void SetContainerLoginPosition(bool onFocus, StackLayout containerLogin)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplayInfo.Height;

            if (onFocus && height <= 2000)
            {
                containerLogin.TranslateTo(0, -75, 50);
            }
            else if (onFocus && height > 2000)
            {
                containerLogin.TranslateTo(0, -110, 50);
            }
            else
            {
                containerLogin.TranslateTo(0, 0, 50);
            }
        }

        public void ContainerLoginInitialPosition(StackLayout containerLogin, Image imgLogin, Frame frameLogin, Frame EntryUsuarioLogin, 
            Frame EntryPasswordLogin, Frame BaseImageUser, Frame baseImageUser3, Frame BaseImagePassword, ImageButton btnHidePassword, 
            ImageButton btnHidePasswordReg, ImageButton btnHideConfirmPasswordReg, ImageButton btnHidePasswordChange, ImageButton btnHideConfirmPasswordChange,
            StackLayout LayoutOlvidoClave)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplayInfo.Height;

            if (height > 2000 || height <= 2000)
            {
                containerLogin.VerticalOptions = LayoutOptions.Center;
                if (height <= 2000)
                {
                    imgLogin.HeightRequest = 100;
                    imgLogin.WidthRequest = 100;
                    frameLogin.HeightRequest = 330;
                    EntryUsuarioLogin.HeightRequest = 40;
                    EntryPasswordLogin.HeightRequest = 40;
                    BaseImageUser.HeightRequest = 20;
                    BaseImageUser.WidthRequest = 20;
                    baseImageUser3.HeightRequest = 20;
                    baseImageUser3.WidthRequest = 20;
                    BaseImagePassword.HeightRequest = 20;
                    BaseImagePassword.WidthRequest = 20;
                    btnHidePassword.HeightRequest = 25;
                    btnHidePassword.WidthRequest = 25;
                    btnHidePasswordReg.HeightRequest = 25;
                    btnHidePasswordReg.WidthRequest = 25;
                    btnHideConfirmPasswordReg.HeightRequest = 25;
                    btnHideConfirmPasswordReg.WidthRequest = 25;
                    btnHidePasswordChange.WidthRequest = 25;
                    btnHidePasswordChange.HeightRequest = 25;
                    btnHideConfirmPasswordChange.HeightRequest = 25;
                    btnHideConfirmPasswordChange.WidthRequest = 25;
                    LayoutOlvidoClave.Margin = new Thickness(0, 15, 0, 0);
                }
            }
        }
    }
}
