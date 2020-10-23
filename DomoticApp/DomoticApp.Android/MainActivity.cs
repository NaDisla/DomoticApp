using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using static Android.Content.PM.PackageManager;
using DomoticApp.Views.Bienvenida;

namespace DomoticApp.Droid
{
    [Activity(Label = "DomoticApp", Icon = "@mipmap/logoapp", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //BienvenidaPage bienvenida = new BienvenidaPage();

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnCreate(Bundle savedInstanceState)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //ValidarInstalacion();
            //string nombrePaquete = "com.companyname.domoticapp";
            //Intent intent = PackageManager.GetLaunchIntentForPackage(nombrePaquete);

            //if (intent == null)
            //{
            //    //No se puede abrir aplicación.
            //}
            //else
            //{
            Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
            LoadApplication(new App());
            
            //}
            //StartActivity(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private bool estaInstaladaAplicacion(string nombrePaquete, Context context)
        {
            PackageManager pm = context.PackageManager;
            try
            {
                pm.GetPackageInfo(nombrePaquete, PackageInfoFlags.Activities);
                return true;
            }
            catch (NameNotFoundException)
            {
                return false;
            }
        }

        [Obsolete]
        public void ValidarInstalacion()
        {
            bool isInstalled = estaInstaladaAplicacion("com.companyname.domoticapp", Application.Context);
            if(isInstalled)
            {
                LoadApplication(new App());
            }
            else
            {
                //LoadApplication(new App(new BienvenidaPage()));
            }
        }
    }
}