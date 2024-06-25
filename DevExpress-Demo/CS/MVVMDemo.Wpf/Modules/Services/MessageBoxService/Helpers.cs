using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace MVVMDemo.Services {
    public class MessageBoxButtonToMessageBoxResultsConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is MessageButton))
                return null;
            var buttons = (MessageButton)value;
            return buttons.ToMessageResults();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class TimeSpanToStringConverter : IValueConverter {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            int seconds;
            if (value is string && int.TryParse((string)value, out seconds))
                return TimeSpan.FromSeconds(seconds);
            return null;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is TimeSpan)
                return $"{value:%s}";
            return null;
        }
    }
    public static class Helpers {
        public static List<MessageResult> ToMessageResults(this MessageButton buttons) {
            var resultButtons = new List<MessageResult>();
            switch (buttons) {
                case MessageButton.OK:
                    resultButtons.Add(MessageResult.OK);
                    break;
                case MessageButton.OKCancel:
                    resultButtons.Add(MessageResult.OK);
                    resultButtons.Add(MessageResult.Cancel);
                    break;
                case MessageButton.YesNoCancel:
                    resultButtons.Add(MessageResult.Yes);
                    resultButtons.Add(MessageResult.No);
                    resultButtons.Add(MessageResult.Cancel);
                    break;
                case MessageButton.YesNo:
                    resultButtons.Add(MessageResult.Yes);
                    resultButtons.Add(MessageResult.No);
                    break;
            }
            return resultButtons;
        }
        public static List<PredefinedFormat> GeneratePredefinedFormat(MessageResult defaultButton, TimeSpan timeout) {
            var formats = new List<PredefinedFormat>();
            formats.Add(new PredefinedFormat($"{defaultButton} ({timeout:%s} sec.)", "{0} ({1:%s} sec.)"));
            formats.Add(new PredefinedFormat($"{defaultButton} {timeout:%s} sec.", "{0} {1:%s} sec."));
            formats.Add(new PredefinedFormat($"{defaultButton} {timeout:%s} secons to close", "{0} {1:%s} secons to close"));
            formats.Add(new PredefinedFormat($"{defaultButton} close after {timeout:%s} sec.", "{0} close after {1:%s} sec."));
            return formats;
        }
    }
}
