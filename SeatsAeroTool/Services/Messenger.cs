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
            return (SeatsAeroLibrary.Services.DialogResult)MessageBox.Show(message, title, GetMessageBoxButtonsFromDialogButton(buttons));
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

        private MessageBoxButtons GetMessageBoxButtonsFromDialogButton(SeatsAeroLibrary.Services.DialogResult[] buttons)
        {
            if (buttons.Contains(SeatsAeroLibrary.Services.DialogResult.OK) && buttons.Contains(SeatsAeroLibrary.Services.DialogResult.Cancel))
            {
                return MessageBoxButtons.OKCancel;
            }
            else if(buttons.Contains(SeatsAeroLibrary.Services.DialogResult.Yes) && buttons.Contains(SeatsAeroLibrary.Services.DialogResult.No))
            {
                if (buttons.Contains(SeatsAeroLibrary.Services.DialogResult.Cancel))
                {
                    return MessageBoxButtons.YesNoCancel;
                }
                else
                {
                    return MessageBoxButtons.YesNo;
                }
            }
            else if (buttons.Contains(SeatsAeroLibrary.Services.DialogResult.Abort) && buttons.Contains(SeatsAeroLibrary.Services.DialogResult.Retry) && buttons.Contains(SeatsAeroLibrary.Services.DialogResult.Ignore))
            {
                return MessageBoxButtons.AbortRetryIgnore;
            }
            else if (buttons.Contains(SeatsAeroLibrary.Services.DialogResult.Cancel) && buttons.Contains(SeatsAeroLibrary.Services.DialogResult.Retry))
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
