using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DomoticApp.DataHelpers
{
    public class CambiarColorBotones
    {
        public void CambiarColorLucesON(Button button)
        {
            button.BackgroundColor = Color.FromHex("#F8F8D8");
            button.TextColor = Color.FromHex("#166498");
            button.BorderColor = Color.FromHex("#e6e620");
        }

        public void CambiarColorOFF(Button button)
        {
            button.BackgroundColor = Color.FromHex("#b9d9f0");
            button.TextColor = Color.FromHex("#166498");
            button.BorderColor = Color.FromHex("#166498");
        }

        public void CambiarColorOtrosON(Button button)
        {
            button.BackgroundColor = Color.FromHex("#aec5d4");
            button.TextColor = Color.FromHex("#166498");
        }
    }
}
