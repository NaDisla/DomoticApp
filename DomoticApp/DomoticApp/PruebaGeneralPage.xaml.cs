using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xam.Plugin.SimpleAppIntro;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DomoticApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PruebaGeneralPage : ContentPage
    {

        public string Text { get; set; } = "Custom Slide (ContentView)";

        public ICommand ButtonClicked { get; set; }

        public PruebaGeneralPage()
        {
            InitializeComponent();
            ButtonClicked = new Command(() => OnButtonClicked());
        }

        private void Open_Static_Clicked(object sender, EventArgs e)
        {
            var welcomePage = new SimpleAppIntro(GetSlides())
            {
                // Properties
                ShowPositionIndicator = true,
                ShowSkipButton = true,
                ShowNextButton = true,
                DoneText = "Finish",
                NextText = "Next",
                BackText = "Back",
                SkipText = "Skip",

                // Theming
                BarColor = "#607D8B",
                BackButtonBackgroundColor = "#FF9700",
                SkipButtonBackgroundColor = "#FF9700",
                DoneButtonBackgroundColor = "#8AC149",
                NextButtonBackgroundColor = "#8AC149",

                // Callbacks
                OnSkipButtonClicked = OnSkipButtonClicked,
                OnDoneButtonClicked = OnDoneButtonClicked,
                OnPositionChanged = OnPositionChanged,

                // Vibrate
                Vibrate = true,
                VibrateDuration = 1,
            };

            Navigation.PushModalAsync(welcomePage);
        }

        private void Open_Icons_Clicked(object sender, EventArgs e)
        {
            var welcomePage = new SimpleAppIntro(GetSlides())
            {
                ShowBackButton = true,
                DoneButtonImage = "baseline_done_white_24.png",
                NextButtonImage = "baseline_keyboard_arrow_right_white_24.png",
                SkipButtonImage = "baseline_double_arrow_white_24.png",
                BackButtonImage = "baseline_keyboard_arrow_left_white_24.png",
                OnSkipButtonClicked = OnSkipButtonClicked,
                OnDoneButtonClicked = OnDoneButtonClicked,
                OnPositionChanged = OnPositionChanged,
            };
            Navigation.PushModalAsync(welcomePage);
        }

        private void Open_Clicked(object sender, EventArgs e)
        {
            var welcomePage = new AnimatedSimpleAppIntro(GetAnimatedSlides())
            {
                // Properties
                ShowPositionIndicator = true,
                ShowSkipButton = true,
                ShowNextButton = true,
                DoneText = "Finish",
                NextText = "Next",
                SkipText = "Skip",

                // Theming
                BarColor = "#607D8B",
                SkipButtonBackgroundColor = "#FF9700",
                DoneButtonBackgroundColor = "#8AC149",
                NextButtonBackgroundColor = "#8AC149",

                // Use images instead of buttons
                DoneButtonImage = "baseline_done_white_24.png",

                // Callbacks
                OnSkipButtonClicked = OnSkipButtonClicked,
                OnDoneButtonClicked = OnDoneButtonClicked,
                OnPositionChanged = OnPositionChanged,

                // Vibrate
                // NOTE: you will probably need to ask VIBRATE permission in Manifest.
                Vibrate = true,
                VibrateDuration = 0.2,
            };

            Navigation.PushModalAsync(welcomePage);
        }

        private List<object> GetSlides()
        {
            return new List<object>() {
            new Slide(new SlideConfig("Welcome", "This is a sample app showing off the new App Intro", "cup_icon.png",
                null, "#FFFFFF", "#FFFFFF",
                FontAttributes.Bold, FontAttributes.Italic, 24, 16)),
                new ButtonSlide(new ButtonSlideConfig("Slides", "You can add slides and have a clean app intro", "cup_icon.png",
                null, "Click here", null,"#FFFFFF", new Command(() => OnButtonClicked()), "#FFFFFF", "#FFFFFF",
                FontAttributes.Bold, FontAttributes.Italic, 24, 16)),
                new CheckboxSlide(new CheckboxSlideConfig("Checkbox", "Let your user set specific settings via a AppIntro screen.",  "cup_icon.png",
                null, true, new Command<bool>((value) => OnCheckboxClicked(value)), "#FFFFFF", "#FFFFFF",
                FontAttributes.Bold, FontAttributes.Italic, 24, 16)),
                new CustomSlide
                {
                    BindingContext = this
                },
                new SwitchSlide(new SwitchSlideConfig("Other", "Tell your user what they can do with your app",  "cup_icon.png",
                null, true, new Command<bool>((value) => OnSwitchClicked(value)), "#FFFFFF", "#FFFFFF",
                FontAttributes.Bold, FontAttributes.Italic, 24, 16)),
            };
        }

        private List<object> GetAnimatedSlides()
        {
            return new List<object>() {
            new Slide(new SlideConfig("Welcome", "This is a sample app showing off the new App Intro", "world.json",
                null, "#FFFFFF", "#FFFFFF",
                FontAttributes.Bold, FontAttributes.Italic, 24, 16)),
                 new ButtonSlide(new ButtonSlideConfig("Slides", "You can add slides and have a clean app intro", "twitter_heart.json",
                null, "Click here", null, "#FFFFFF", new Command(() => OnButtonClicked()), "#FFFFFF", "#FFFFFF",
                FontAttributes.Bold, FontAttributes.Italic, 24, 16)),
                new CheckboxSlide(new CheckboxSlideConfig("Checkbox", "Let your user set specific settings via a AppIntro screen.",  "twitter_heart.json",
                null, true, new Command<bool>((value) => OnCheckboxClicked(value)), "#FFFFFF", "#FFFFFF",
                FontAttributes.Bold, FontAttributes.Italic, 24, 16)),
                new CustomSlide
                {
                    BindingContext = this
                },
                new SwitchSlide(new SwitchSlideConfig("Other", "Tell your user what they can do with your app", "send_message_done.json",
                null, true, new Command<bool>((value) => OnSwitchClicked(value)), "#FFFFFF", "#FFFFFF",
                FontAttributes.Bold, FontAttributes.Italic, 24, 16)),
             };
        }

        private void OnSwitchClicked(bool isToggled)
        {
            DisplayAlert("Switch Slide", $"Switch toggled {isToggled}", "OK");
        }
        private void OnCheckboxClicked(bool isChecked)
        {
            DisplayAlert("Checkbox Slide", $"Checkbox toggled {isChecked}", "OK");
        }
        private void OnButtonClicked()
        {
            DisplayAlert("Button Slide", "Button clicked", "OK");
        }
        private void OnSkipButtonClicked()
        {
            DisplayAlert("Result", "Skip", "OK");
        }
        private void OnDoneButtonClicked()
        {
            DisplayAlert("Result", "Done", "OK");
        }
        private void OnPositionChanged(int page)
        {
            Console.Write($"Slide changed to page {page}");
        }
    }
}