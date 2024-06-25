using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DialogsDemo.Helpers;

namespace DialogsDemo
{
    [CodeFile("Helpers/PredefinedFormat.cs")]
    public partial class ThemedMessageBoxModule : DemoModule
    {
        public ThemedMessageBoxModule()
        {
            InitializeComponent();
            UpdateDefaultButtonSource();
        }
        private void ShowThemedMessageBox(object sender, RoutedEventArgs e)
        {
            var commonParameters = new ThemedMessageBoxParameters((MessageBoxImage)icons.EditValue)
            {
                AllowTextSelection = allowTextSelectionEdit.IsChecked.Value
            };
            int seconds;
            if (int.TryParse((string)timerTimeout.SelectedItem, out seconds))
            {
                commonParameters.TimerTimeout = TimeSpan.FromSeconds(seconds);
                if (predefinedFormats.EditValue != null)
                    commonParameters.TimerFormat = predefinedFormats.EditValue?.ToString();
            }

            dialogResult.Content = ThemedMessageBox.Show(
                captionEdit.DisplayText,
                contentEdit.DisplayText,
                (MessageBoxButton)buttons.EditValue,
                (MessageBoxResult)defaultButton.EditValue,
                commonParameters
                );
        }

        void OnButtonsChanged(object sender, RoutedEventArgs e)
        {
            UpdateDefaultButtonSource();
        }

        void UpdateDefaultButtonSource()
        {
            defaultButton.ItemsSource = DialogsDemoHelper.GetMessageBoxResults((MessageBoxButton)buttons.EditValue);
            defaultButton.SelectedIndex = 0;
            UpdatePredefinedFormats();
        }

        void GenerateFormats(MessageBoxResult defaultButton, int timeout)
        {
            var formats = new List<PredefinedFormat>();
            formats.Add(new PredefinedFormat($"{defaultButton} ({timeout} sec.)", "{0} ({1:%s} sec.)"));
            formats.Add(new PredefinedFormat($"{defaultButton} {timeout} sec.", "{0} {1:%s} sec."));
            formats.Add(new PredefinedFormat($"{defaultButton} {timeout} secons to close", "{0} {1:%s} secons to close"));
            formats.Add(new PredefinedFormat($"{defaultButton} close after {timeout} sec.", "{0} close after {1:%s} sec."));
            predefinedFormats.ItemsSource = formats;
            predefinedFormats.SelectedIndex = 0;
        }

        void UpdatePredefinedFormats()
        {
            int seconds;
            if (int.TryParse((string)timerTimeout.SelectedItem, out seconds))
            {
                GenerateFormats((MessageBoxResult)defaultButton.EditValue, seconds);
            }
        }

        void OnDefaultButtonChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            UpdatePredefinedFormats();
        }

        void OnTimerTimeoutChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            UpdatePredefinedFormats();
        }
    }
}
