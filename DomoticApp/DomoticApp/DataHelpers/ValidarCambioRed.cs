using System;
using System.Net.Http;
using Rg.Plugins.Popup.Services;
using DomoticApp.Views.Popups;
using System.Net;
using Xamarin.Essentials;
using System.Linq;

namespace DomoticApp.DataHelpers
{
    public class ValidarCambioRed
    {
        const string titleError = "No hay conexión de red", detailError = "Se ha perdido la conexión a internet.",
            titleCorrect = "¡Bienvenido(a)! - Red conectada",
            detailCorrect = "Se encuentra conectado a la red de su vivienda.";
        string ipDevice, parteInicialCasa, parteInicialDevice, content;
        string[] numIPCasa, numIPDevice;
        const string urlTarjeta = "http://10.0.0.17";
        const string ipCasa = "10.0.0.17";
        private HttpClient client = new HttpClient();
        public CorrectValidationPage redCorrecta;
        public IncorrectNetworkPage redIncorrecta;
        public AlertNetworkPage alertaVPN;
        int estadoRedCorrecta, estadoRedIncorrecta;

        [Obsolete]
        public async void NetworkChanged(ConnectivityChangedEventArgs e)
        {
            var deviceIP = Dns.GetHostAddresses(Dns.GetHostName()).LastOrDefault();
            if (e.NetworkAccess.ToString() == "Internet")
            {
                if (deviceIP != null)
                {
                    ipDevice = deviceIP.ToString();
                    numIPCasa = ipCasa.Split('.');
                    numIPDevice = ipDevice.Split('.');
                    parteInicialCasa = numIPCasa[0] + numIPCasa[1] + numIPCasa[2];
                    parteInicialDevice = numIPDevice[0] + numIPDevice[1] + numIPDevice[2];

                    if (parteInicialCasa == parteInicialDevice)
                    {
                        content = await client.GetStringAsync(urlTarjeta);
                        if (content != null)
                        {
                            RedCorrecta();
                        }
                        else
                        {
                            RedIncorrecta();
                        }

                    }
                    else if (parteInicialCasa != parteInicialDevice)
                    {
                        AlertaVPN();
                    }
                }
            }
            else
            {
                RedIncorrecta();
            }
        }

        [Obsolete]
        public async void RedCorrecta()
        {
            if(estadoRedIncorrecta == 1)
            {
                await PopupNavigation.RemovePageAsync(redIncorrecta);
                estadoRedIncorrecta = 0;
            }
            else
            {
                await PopupNavigation.PushAsync(redCorrecta = new CorrectValidationPage(titleCorrect, detailCorrect));
                estadoRedCorrecta = 1;
            }
        }

        [Obsolete]
        public async void RedIncorrecta()
        {
            if(estadoRedCorrecta == 1)
            {
                await PopupNavigation.RemovePageAsync(redCorrecta);
                estadoRedCorrecta = 0;
            }
            else
            {
                await PopupNavigation.PushAsync(redIncorrecta = new IncorrectNetworkPage(titleError, detailError));
                estadoRedIncorrecta = 1;
            }
        }

        [Obsolete]
        public async void AlertaVPN()
        {
            await PopupNavigation.PushAsync(alertaVPN = new AlertNetworkPage());
        }
    }
}
