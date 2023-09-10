﻿using SeatsAeroLibrary;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroTests.Services
{
    public class Messenger : IMessenger
    {

        public SeatsAeroLibrary.Services.DialogResult ShowMessageBox(string message, string title)
        {
            return ShowDevExpressMessageBox(message, title, new SeatsAeroLibrary.Services.DialogResult[] { SeatsAeroLibrary.Services.DialogResult.OK });
        }

        public SeatsAeroLibrary.Services.DialogResult ShowMessageBox(string message, string title, SeatsAeroLibrary.Services.DialogResult[] buttons)
        {
            return ShowDevExpressMessageBox(message, title, buttons);
        }

        //public void ShowNotification(string message, string title)
        //{
        //    ShowGSSUINotification(message, title, null);
        //}


        private SeatsAeroLibrary.Services.DialogResult ShowDevExpressMessageBox(string message, string title, SeatsAeroLibrary.Services.DialogResult[] buttons)
        {
            return SeatsAeroLibrary.Services.DialogResult.OK;
        }

        //private void ShowGSSUINotification(string message, string title, SvgImage icon)
        //{
        //    var titleInfo = new GSSEO.Translations.TranslationInformation(title);
        //    var messageInfo = new GSSEO.Translations.TranslationInformation(message);

        //    if (icon != null)
        //    {
        //        Alerts.ShowAlert(titleInfo, messageInfo, icon);
        //        return;
        //    }

        //    Alerts.ShowAlert(titleInfo, messageInfo);
        //}
    }

}