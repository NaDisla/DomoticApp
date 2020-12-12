﻿using DomoticApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

namespace DomoticApp.DataHelpers
{
    public class ResultsOperations
    {
        string titleAlert, detailAlert;

        [Obsolete]
        public async Task Alert()
        {
            titleAlert = "Contraseñas no coinciden";
            detailAlert = "Las contraseñas no coinciden. Verifique nuevamente.";
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
    }
}