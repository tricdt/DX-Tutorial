using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using EditorsDemo.Utils;
using System.Collections;
using DevExpress.DemoData.Models;

namespace EditorsDemo {
    public partial class ListBoxEditModule : EditorsDemoModule {
        bool isLoadedCompleted = false;
        public ListBoxEditModule() {
            InitializeComponent();
            edit.Focus();
            Loaded += new RoutedEventHandler(ListBoxEditModule_Loaded);
            InitSources();
        }
        void InitSources() {
            edit.ItemsSource = NWindContext.Create().CountriesArray;

            List<SelectionMode> modes = new List<SelectionMode>();
            modes.Add(SelectionMode.Single);
            modes.Add(SelectionMode.Multiple);
            modes.Add(SelectionMode.Extended);
            selectionModeSelector.ItemsSource = modes;
        }
        void ListBoxEditModule_Loaded(object sender, RoutedEventArgs e) {
            isLoadedCompleted = true;
        }
        void styleSelector_SelectionChanged(object sender, RoutedEventArgs e) {
            if(!isLoadedCompleted)
                return;
            string selectedStyleName = (string)styleSelector.EditValue;
            switch(selectedStyleName) {
                case "Checked":
                    edit.StyleSettings = new CheckedListBoxEditStyleSettings();
                    selectionModeSelector.IsEnabled = false;
                    allowHighlightingCheck.IsEnabled = false;
                    break;
                case "Radio":
                    edit.StyleSettings = new RadioListBoxEditStyleSettings();
                    selectionModeSelector.IsEnabled = false;
                    allowHighlightingCheck.IsEnabled = false;
                    break;
                default:
                    edit.StyleSettings = new ListBoxEditStyleSettings();
                    selectionModeSelector.IsEnabled = true;
                    allowHighlightingCheck.IsEnabled = true;
                    break;
            }
        }
        private void selectionModeSelector_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            selectedItemList.IsEnabled = edit.SelectionMode != SelectionMode.Single;
        }
    }
    public class DisplayTextConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            IList values = value as IList;
            if(values != null) {
                StringBuilder builder = new StringBuilder();
                builder.Append("{");
                bool isFirst = true;
                foreach(object obj in values) {
                    if(isFirst) {
                        builder.Append(obj);
                        isFirst = false;
                        continue;
                    }
                    builder.AppendFormat(", {0}", obj);
                }
                builder.Append("}");
                return builder.ToString();
            }
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
