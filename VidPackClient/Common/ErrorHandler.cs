using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups; 

namespace VidPackClient.Common
{
    public static class ErrorHandler
    {
        private static List<Exception> _errorMessage;
        
        static ErrorHandler()
        {
            _errorMessage = new List<Exception>();
            RethrowError = false; 
        }

        public static bool RethrowError { get; set; }
        public static bool ErrorOccured
        {
            get {return _errorMessage.Count > 0; }
        }

        public static void AddError(Exception exception)
        {
            _errorMessage.Add(exception);

            if (!RethrowError)
                return;

            throw (exception); 
        }

        public static Exception GetAndRemoveLastError()
        {
            if (_errorMessage.Count == 0)
                return null;

            Exception exception = _errorMessage[_errorMessage.Count - 1];
            _errorMessage.Remove(exception);

            return exception; 
        }

        public static Exception ReadLastError()
        {
            if (_errorMessage.Count == 0)
                return null;

            return _errorMessage[_errorMessage.Count - 1];
        }

        public static async void ShowCustomError(Exception exception)
        {
            var dialog = new MessageDialog(exception.Message);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
        }

        public static void ShowCustomError(string exceptionDescription)
        {
            ShowCustomError(new Exception(exceptionDescription)); 
        }

        internal static void ShowLastError()
        {
            ShowCustomError(GetAndRemoveLastError()); 
        }

        internal static void ShowLastError(string exceptionAddOn)
        {
            Exception exception = GetAndRemoveLastError();
            Exception exceptionToShow = new Exception(String.Concat(exceptionAddOn, '\n', exception.Message), exception);
            ShowCustomError(exceptionToShow); 
        }
    }
}
