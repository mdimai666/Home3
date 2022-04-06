using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.Services
{
    public class DToast : IToastPopUp
    {
        public DToast()
        {

        }

        Plugin.Toast.Abstractions.IToastPopUp Toast => Plugin.Toast.CrossToastPopUp.Current;

        public void ShowCustomToast(string message, string bgColor, string txtColor, ToastLength toastLength = ToastLength.Short)
        {
            if (DeviceInfo.Platform != DevicePlatform.WinUI)
                Toast.ShowCustomToast(message, bgColor, txtColor, toastLength);
        }

        public void ShowToastError(string message, ToastLength toastLength = ToastLength.Short)
        {
            if (DeviceInfo.Platform != DevicePlatform.WinUI)
                Toast.ShowToastError(message, toastLength);
        }

        public void ShowToastMessage(string message, ToastLength toastLength = ToastLength.Short)
        {
            if (DeviceInfo.Platform != DevicePlatform.WinUI)
                Toast.ShowToastMessage(message, toastLength);
        }

        public void ShowToastSuccess(string message, ToastLength toastLength = ToastLength.Short)
        {
            if (DeviceInfo.Platform != DevicePlatform.WinUI)
                Toast.ShowToastSuccess(message, toastLength);
        }

        public void ShowToastWarning(string message, ToastLength toastLength = ToastLength.Short)
        {
            if (DeviceInfo.Platform != DevicePlatform.WinUI)
                Toast.ShowToastWarning(message, toastLength);
        }
    }

    public class Toast
    {
        static DToast _toast = null;

        static DToast t
        {
            get
            {
                if (_toast == null) _toast = DependencyService.Get<DToast>();
                return _toast;
            }
        }

        public static void ShowCustomToast(string message, string bgColor, string txtColor, ToastLength toastLength = ToastLength.Short)
        {
            t.ShowCustomToast(message, bgColor, txtColor, toastLength);
        }

        public static void ShowToastError(string message, ToastLength toastLength = ToastLength.Short)
        {
            t.ShowToastError(message, toastLength);
        }

        public static void ShowToastMessage(string message, ToastLength toastLength = ToastLength.Short)
        {
            t.ShowToastMessage(message, toastLength);
        }

        public static void ShowToastSuccess(string message, ToastLength toastLength = ToastLength.Short)
        {
            t.ShowToastSuccess(message, toastLength);
        }

        public static void ShowToastWarning(string message, ToastLength toastLength = ToastLength.Short)
        {
            t.ShowToastWarning(message, toastLength);
        }
    }
}
