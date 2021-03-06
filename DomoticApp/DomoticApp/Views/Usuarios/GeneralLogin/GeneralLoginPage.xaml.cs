﻿using DomoticApp.DataHelpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using DomoticApp.Views.MasterMenu;

namespace DomoticApp.Views.Usuarios.GeneralLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralLoginPage : TabbedPage
    {
        ConfigLayoutLogin configLayout = new ConfigLayoutLogin();
        LoginBackend loginBackend = new LoginBackend();
        RegistroBackend registroBackend = new RegistroBackend();
        CambiarClaveBackend cambioClave = new CambiarClaveBackend();
        public string usuario = "";
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public GeneralLoginPage()
        {
            InitializeComponent();
            configLayout.DetectarFocoEntry(txtUser, txtPassword, ContainerLogin);
            configLayout.ContainerLoginInitialPosition(ContainerLogin, imgLogin, FrameLogin, EntryUsuarioLogin, EntryPasswordLogin, BaseImageUser, 
                baseImageUser3, BaseImagePassword, btnHidePassword, btnHidePasswordReg, btnHideConfirmPasswordReg, btnHidePasswordChange,
                btnHideConfirmPasswordChange, LayoutOlvidoClave);
            SubscribeTab();
            SendTap();
            HideElements();
            SecureStorage.RemoveAll();
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnAppearing()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnDisappearing()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        [Obsolete]
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            cambioRed.NetworkChanged(e);
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
            var login = await loginBackend.Login(txtUser, txtPassword, EntryUsuarioLogin, EntryPasswordLogin);
            usuario = loginBackend.nombreRealUsuario;

            if (login == 1)
            {
                await Navigation.PushAsync(new MasterMenuPage(usuario));
            }
            else if (login == 2)
            {
                await Navigation.PushAsync(new MasterMenuHabitantePage(usuario));
            }
        }

        [Obsolete]
        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            var registro = await registroBackend.Registro(txtNombreUsuario, txtNombreCompleto, txtCorreoRegistro, txtClaveRegistro, txtConfirmarClaveRegistro, entryNombreRealRegistro,
                entryUsuarioRegistro, entryCorreoRegistro, entryClaveRegistro, entryConfirmarClaveRegistro);
            
            if(registro)
            {
                CurrentPage = Children[0];
            }
            else
            {
                CurrentPage = Children[1];
            }
        }

        [Obsolete]
        private void btnEnviarCodigo_Clicked(object sender, EventArgs e)
        {
            cambioClave.EnviarCodigo(txtUsuarioCambioClave, entryUsuarioCambiarClave, btnEnviarCodigo, entryCodigo, btnConfirmarCodigo);
        }

        [Obsolete]
        private void btnConfirmarCodigo_Clicked(object sender, EventArgs e)
        {
            cambioClave.ConfirmarCodigo(txtCodigo, entryCodigo, entryUsuarioCambiarClave, txtUsuarioCambioClave, btnConfirmarCodigo, entryNuevaClave,
                entryConfirmarNuevaClave, btnCambiarClave);
        }

        [Obsolete]
        private void btnCambiarClave_Clicked(object sender, EventArgs e)
        {
            cambioClave.CambiarClave(entryUsuarioCambiarClave, btnEnviarCodigo, txtUsuarioCambioClave, txtNuevaClave, txtConfirmarClaveNueva, 
                entryNuevaClave, entryConfirmarNuevaClave, btnCambiarClave);
        }
    }
}