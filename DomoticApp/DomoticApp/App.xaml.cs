﻿using DomoticApp.Views.MasterMenu;
using Plugin.SharedTransitions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
using Plugin.LocalNotifications;

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
        public string ipDevice, parteInicialCasa, parteInicialDevice, admin, content, MyIp;
        public string[] numIPCasa, numIPDevice;
        private const string textLoadingDetail = "Comprobando conexión...", textTitleCorrect = "¡Bienvenido(a)! - Red conectada",
            textDetailCorrect = "Se encuentra conectado a la red de su vivienda.", textTitleError = "No hay conexión de red", 
            textDetailError = "No se ha detectado una conexión de internet.";
        
        [Obsolete]
        public App()
        {
            InitializeComponent();
            CheckConnection();
            
            //NotificationTinaco();
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
        void CheckConnection()
        {
            if (connectionProfile.Contains(ConnectionProfile.WiFi) || connectionProfile.Contains(ConnectionProfile.Cellular))
                ValidandoRedes();
            else
                RedIncorrecta();
        }

        [Obsolete]
        async void ValidandoRedes()
        {
            var deviceIP = Dns.GetHostAddresses(Dns.GetHostName()).LastOrDefault();

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
                    DetectarLogin();
                }
                else
                {
                    AlertaVPN();
                    
                }
                var result = AlertNetworkPage.res;
                if (result == 1)
                {
                    await PopupNavigation.RemovePageAsync(alertaVPN);
                    CheckConnection();
                }
                else if(result == 2)
                {
                    await PopupNavigation.RemovePageAsync(alertaVPN);
                    CheckConnection();
                }
            }
        }
        
        async void NotificationTinaco()
        {
            content = await client.GetStringAsync(urlTarjeta);
            var cortando = content.Split(';');
            string nivel = cortando[5];
            int agua = int.Parse(nivel);
            if (nivel == "06")
            {
                CrossLocalNotifications.Current.Show("Tinaco vacío", "El tinaco necesita llenarse.", 0);
            }
            else if(agua >= 10 && agua <= 40)
            {
                CrossLocalNotifications.Current.Show("Tinaco disminuyendo", "El tinaco está a punto de quedar vacío.", 0);
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

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
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
