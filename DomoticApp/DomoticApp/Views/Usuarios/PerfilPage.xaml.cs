﻿using DomoticApp.DataHelpers;
using DomoticApp.Views.MasterMenu;
using DomoticApp.Views.Monitoreo;
using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilPage : ContentPage
    {
        GeneralData data = new GeneralData();
        public string _usuario { get; set; }
        string titleError, detailError, titleCorrect, detailCorrect, detailLoading, claveEncriptada;
        int userID;
        public static string refreshUsuario;
        Models.Usuarios perfilUsuario;
        ResultsOperations results = new ResultsOperations();
        List<Models.Usuarios> usuarios;
        ValidarCambioRed cambioRed = new ValidarCambioRed();

        public PerfilPage(string usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            DatosUsuario();
            btnMenu.Clicked += (s, e) => MainPage.inicio();
            
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplayInfo.Height;

            if(height > 2000)
            {
                rowPerfil.Height = 490;
            }
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

        async void DatosUsuario()
        {
            usuarios = await data.GetUsuarios();
            userID = usuarios.Where(x => x.UsuarioNombreReal == _usuario).Select(y => y.UsuarioID).FirstOrDefault();
            perfilUsuario = await data.GetUsuario(userID);

            txtNombreRealPerfil.Text = perfilUsuario.UsuarioNombreReal;
            txtCorreoPerfil.Text = perfilUsuario.UsuarioCorreo;
            txtUsuarioPerfil.Text = perfilUsuario.UsuarioNombre;
            txtClavePerfil.Text = DataSecurity.Decrypt(perfilUsuario.UsuarioClave, "sblw-3hn8-sqoy19");
        }

        [Obsolete]
        private async void btnActualizarPerfil_Clicked(object sender, EventArgs e)
        {
            detailLoading = "Actualizando perfil...";
            LoadingPage loading = new LoadingPage(detailLoading);
            await PopupNavigation.PushAsync(loading);
            await Task.Delay(1000);

            try
            {
                var usuarioExiste = usuarios.Where(x => x.UsuarioNombre == txtUsuarioPerfil.Text && x.UsuarioID != userID)
                    .Select(y => y.UsuarioNombre).FirstOrDefault();

                if(usuarioExiste != null)
                {
                    await PopupNavigation.RemovePageAsync(loading);
                    titleError = "Usuario incorrecto";
                    detailError = "El nombre de usuario es incorrecto. Intente nuevamente.";
                    await results.Unsuccess(titleError, detailError);
                }
                else
                {
                    claveEncriptada = DataSecurity.Encrypt(txtClavePerfil.Text, "sblw-3hn8-sqoy19");
                    await data.UpdateUsuario(userID, txtNombreRealPerfil.Text, txtCorreoPerfil.Text, txtUsuarioPerfil.Text, claveEncriptada,
                    perfilUsuario.UsuarioRol, perfilUsuario.Acceso, perfilUsuario.UsuarioEstado);
                    refreshUsuario = txtNombreRealPerfil.Text;
                    await SecureStorage.SetAsync("nombreUsuario", txtNombreRealPerfil.Text);
                    MasterMenuHabitantePage.usuarioRefresh = txtNombreRealPerfil.Text;
                    MasterMenuPage.usuarioRefresh = txtNombreRealPerfil.Text;
                    await PopupNavigation.RemovePageAsync(loading);
                    titleCorrect = "Perfil actualizado";
                    detailCorrect = "Se ha actualizado su perfl correctamente.";
                    await results.Success(titleCorrect, detailCorrect);
                }
            }
            catch (Exception)
            {
                await PopupNavigation.RemovePageAsync(loading);
                titleError = "Error";
                detailError = "Ha ocurrido un error procesando la actualización de datos. Intente nuevamente.";
                await results.Unsuccess(titleError, detailError);
            }
        }
    }
}