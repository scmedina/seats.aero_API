using SeatsAeroLibrary;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroTool.Services
{
    public class Messenger : IMessenger
    {

        public SeatsAeroLibrary.Services.MessengerDialogResult ShowMessageBox(string message, string title)
        {
            return ShowDevExpressMessageBox(message, title, new SeatsAeroLibrary.Services.MessengerDialogResult[] { SeatsAeroLibrary.Services.MessengerDialogResult.OK });
        }

        public SeatsAeroLibrary.Services.MessengerDialogResult ShowMessageBox(string message, string title, SeatsAeroLibrary.Services.MessengerDialogResult[] buttons)
        {
            return ShowDevExpressMessageBox(message, title, buttons);
        }

        //public void ShowNotification(string message, string title)
        //{
        //    ShowGSSUINotification(message, title, null);
        //}


        private SeatsAeroLibrary.Services.MessengerDialogResult ShowDevExpressMessageBox(string message, string title, SeatsAeroLibrary.Services.MessengerDialogResult[] buttons)
        {
            return (SeatsAeroLibrary.Services.MessengerDialogResult)MessageBox.Show(message, title, GetMessageBoxButtonsFromDialogButton(buttons));
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

        private MessageBoxButtons GetMessageBoxButtonsFromDialogButton(SeatsAeroLibrary.Services.MessengerDialogResult[] buttons)
        {
            if (buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.OK) && buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.Cancel))
            {
                return MessageBoxButtons.OKCancel;
            }
            else if(buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.Yes) && buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.No))
            {
                if (buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.Cancel))
                {
                    return MessageBoxButtons.YesNoCancel;
                }
                else
                {
                    return MessageBoxButtons.YesNo;
                }
            }
            else if (buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.Abort) && buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.Retry) && buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.Ignore))
            {
                return MessageBoxButtons.AbortRetryIgnore;
            }
            else if (buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.Cancel) && buttons.Contains(SeatsAeroLibrary.Services.MessengerDialogResult.Retry))
            {
                return MessageBoxButtons.RetryCancel;
            }
            else
            {
                return MessageBoxButtons.OK;
            }
        }

    }

}
