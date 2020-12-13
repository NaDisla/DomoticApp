﻿using DomoticApp.Views.MasterMenu;
using Plugin.SharedTransitions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DomoticApp;
using Rg.Plugins.Popup.Services;
using DomoticApp.Views.Popups;
using Plugin.Connectivity;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.InteropServices;
using Xamarin.Essentials;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using DomoticApp.Views.Recibidor;
using DomoticApp.Views.Usuarios.GeneralLogin;
using DomoticApp.Views.Usuarios;
using DomoticApp.DataHelpers;

namespace DomoticApp
{
    public partial class App : Application
    {
        private const string urlTarjeta = "http://10.0.0.17";
        private const string ipCasa = "10.0.0.17";
        public LoadingPage loadingRed;
        public CorrectValidationPage redCorrecta;
        public IncorrectValidationPage redIncorrecta;
        public AlertNetworkPage alertaVPN;
        private readonly HttpClient client = new HttpClient();
        IEnumerable<ConnectionProfile> connectionProfile = Connectivity.ConnectionProfiles;
        public string ipDevice, parteInicialCasa, parteInicialDevice, admin;
        public string[] numIPCasa, numIPDevice;
        private const string textLoadingDetail = "Comprobando conexión...", textTitleCorrect = "¡Bienvenido(a)! - Red conectada",
            textDetailCorrect = "Se encuentra conectado a la red de su vivienda.", textTitleError = "No hay conexión de red", 
            textDetailError = "No se ha detectado una conexión de internet.";
        
        [Obsolete]
        public App()
        {
            InitializeComponent();
            //DetectarLogin();
            MainPage = new NavigationPage(new MasterMenuPage(""));
            /*if (connectionProfile.Contains(ConnectionProfile.WiFi) || connectionProfile.Contains(ConnectionProfile.Cellular))
                ValidandoRedes();
            else
                RedIncorrecta();*/
        }
        [Obsolete]
        void DetectarLogin()
        {
            var isLoogged = SecureStorage.GetAsync("isLogged").Result;
            var nombreUsuario = SecureStorage.GetAsync("nombreUsuario").Result;

            if (isLoogged == "1")
            {
                MainPage = new NavigationPage(new MasterMenuPage(nombreUsuario));
            }
            else if (isLoogged == "2")
            {
                MainPage = new NavigationPage(new MasterMenuHabitantePage(nombreUsuario));
            }
            else
            {
                MainPage = new NavigationPage(new GeneralLoginPage());
            }
        }
        [Obsolete]
        void ValidandoRedes()
        {
            var deviceIP = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();

            if (deviceIP != null)
            {
                ipDevice = deviceIP.ToString();
                numIPCasa = ipCasa.Split('.');
                numIPDevice = ipDevice.Split('.');
                parteInicialCasa = numIPCasa[0] + numIPCasa[1] + numIPCasa[2];
                parteInicialDevice = numIPDevice[0] + numIPDevice[1] + numIPDevice[2];

                if (parteInicialDevice == parteInicialCasa && client.GetStringAsync(urlTarjeta) != null)
                {
                    RedCorrecta();
                }
                else
                {
                    AlertaVPN();
                }
            }
        }

        [Obsolete]
        public async void RedCorrecta()
        {
            await PopupNavigation.PushAsync(loadingRed = new LoadingPage(textLoadingDetail));
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingRed);
            await PopupNavigation.PushAsync(redCorrecta = new CorrectValidationPage(textTitleCorrect, textDetailCorrect));
        }

        [Obsolete]
        public async void RedIncorrecta()
        {
            await PopupNavigation.PushAsync(loadingRed = new LoadingPage(textLoadingDetail));
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingRed);
            await PopupNavigation.PushAsync(redIncorrecta = new IncorrectValidationPage(textTitleError, textDetailError));
        }

        [Obsolete]
        public async void AlertaVPN()
        {
            await PopupNavigation.PushAsync(loadingRed = new LoadingPage(textLoadingDetail));
            await Task.Delay(2500);
            await PopupNavigation.RemovePageAsync(loadingRed);
            await PopupNavigation.PushAsync(alertaVPN = new AlertNetworkPage());
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
