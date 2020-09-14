using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.SimpleAppIntro.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSlide : ContentView, IValidate, ISave
    {
        public CustomSlide()
        {
            InitializeComponent();
        }

        public void Save()
        {
            Console.Write("Data Saved");
        }

        public bool Validate()
        {
            return true;
        }

    }
}