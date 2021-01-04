using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

namespace DomoticApp.DataHelpers
{
    public class ResultsOperations
    {
        [Obsolete]
        public async Task Alert(string titleAlert, string detailAlert)
        {
            AlertPage alert = new AlertPage(titleAlert, detailAlert);
            await PopupNavigation.PushAsync(alert);
        }
        [Obsolete]
        public async Task Success(string titleSuccess, string detailSuccess)
        {
            CorrectValidationPage correctValidation = new CorrectValidationPage(titleSuccess, detailSuccess);
            await PopupNavigation.PushAsync(correctValidation);
        }
        [Obsolete]
        public async Task Unsuccess(string titleUnsuccess, string detailUnsuccess)
        {
            IncorrectValidationPage incorrectValidation = new IncorrectValidationPage(titleUnsuccess, detailUnsuccess);
            await PopupNavigation.PushAsync(incorrectValidation);
        }
        [Obsolete]
        public async Task Confirmation(string titleConfirmation, string detailConfirmation, string url)
        {
            ConfirmationPage confirmation = new ConfirmationPage(titleConfirmation, detailConfirmation, url);
            await PopupNavigation.PushAsync(confirmation);
        }
    }
}
