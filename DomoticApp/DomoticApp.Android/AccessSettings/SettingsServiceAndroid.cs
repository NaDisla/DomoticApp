using System;
using Android.Content;
using Android.OS;
using DomoticApp.Droid.AccessSettings;
using DomoticApp.SettingsAccess;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsServiceAndroid))]
namespace DomoticApp.Droid.AccessSettings
{
    public class SettingsServiceAndroid : IServiceSettings
    {
        [Obsolete]
        public void OpenSettings()
        {
            int sdkVersion = int.Parse(Build.VERSION.Sdk);
            if(sdkVersion >= 20 && sdkVersion <= 25 )
            {
                Forms.Context.StartActivity(new Intent(Android.Provider.Settings.ActionWirelessSettings));
            }
            else if(sdkVersion >= 26 && sdkVersion <= 30)
            {
                Forms.Context.StartActivity(new Intent(Android.Provider.Settings.ActionVpnSettings));
            }
        }
    }
}