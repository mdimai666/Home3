using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Toast.Abstractions;

namespace Home3.Services
{
    public class DToast : IToastPopUp
    {
        public DToast()
        {

        }

        Plugin.Toast.Abstractions.IToastPopUp Toast => Plugin.Toast.CrossToastPopUp.Current;

        public void ShowCustomToast(string message, string bgColor, string txtColor, ToastLength toastLength = ToastLength.Short)
        {
            Toast.ShowCustomToast(message, bgColor, txtColor, toastLength);
        }

        public void ShowToastError(string message, ToastLength toastLength = ToastLength.Short)
        {
            Toast.ShowToastError(message, toastLength);
        }

        public void ShowToastMessage(string message, ToastLength toastLength = ToastLength.Short)
        {
            Toast.ShowToastMessage(message, toastLength);
        }

        public void ShowToastSuccess(string message, ToastLength toastLength = ToastLength.Short)
        {
            Toast.ShowToastSuccess(message, toastLength);
        }

        public void ShowToastWarning(string message, ToastLength toastLength = ToastLength.Short)
        {
            Toast.ShowToastWarning(message, toastLength);
        }
    }
}
