﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface IMessenger
    {
        MessengerDialogResults ShowMessageBox(string message, string title);
        MessengerDialogResults ShowMessageBox(string message, string title, MessengerDialogResults[] buttons);
        //void ShowNotification(string message, string title);       // void ShowNotification(string message, string title, SvgImage icon);
    }
    //
    // Summary:
    //     Specifies identifiers to indicate the return value of a dialog box.
    [ComVisible(true)]
    public enum MessengerDialogResults
    {
        //
        // Summary:
        //     Nothing is returned from the dialog box. This means that the modal dialog continues
        //     running.
        None,
        //
        // Summary:
        //     The dialog box return value is OK (usually sent from a button labeled OK).
        OK,
        //
        // Summary:
        //     The dialog box return value is Cancel (usually sent from a button labeled Cancel).
        Cancel,
        //
        // Summary:
        //     The dialog box return value is Abort (usually sent from a button labeled Abort).
        Abort,
        //
        // Summary:
        //     The dialog box return value is Retry (usually sent from a button labeled Retry).
        Retry,
        //
        // Summary:
        //     The dialog box return value is Ignore (usually sent from a button labeled Ignore).
        Ignore,
        //
        // Summary:
        //     The dialog box return value is Yes (usually sent from a button labeled Yes).
        Yes,
        //
        // Summary:
        //     The dialog box return value is No (usually sent from a button labeled No).
        No
    }
}
