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
using System.ComponentModel;
using DevExpress.Xpf.Editors.Validation;
using EditorsDemo.Utils;

namespace EditorsDemo {
    
    
    
    public partial class TextEditModule : EditorsDemoModule {
        public TextEditModule() {
            InitializeComponent();
            InitSources();
            dDate.DateTime = DateTime.Now;
        }
        void InitSources() {
            numericValue.ItemsSource = new decimal[] { 0.5M, 1M, 123.45M, -12.34M, 100M };

            List<FormatWrapper> numericFormats = new List<FormatWrapper>();
            numericFormats.Add(new FormatWrapper("No formatting", string.Empty));
            numericFormats.Add(new FormatWrapper("Number", "n"));
            numericFormats.Add(new FormatWrapper("Currency", "c"));
            numericFormats.Add(new FormatWrapper("Scientific", "e"));
            numericFormats.Add(new FormatWrapper("Percent", "p"));
            numericFormat.ItemsSource = numericFormats;

            List<FormatWrapper> dateTimeFormats = new List<FormatWrapper>();
            dateTimeFormats.Add(new FormatWrapper("No formatting", string.Empty));
            dateTimeFormats.Add(new FormatWrapper("Short date", "d"));
            dateTimeFormats.Add(new FormatWrapper("Long date", "D"));
            dateTimeFormats.Add(new FormatWrapper("Short time", "t"));
            dateTimeFormats.Add(new FormatWrapper("Long time", "T"));
            dateTimeFormats.Add(new FormatWrapper("Full date/time", "f"));
            dateTimeFormats.Add(new FormatWrapper("General date/time", "g"));
            dateTimeFormats.Add(new FormatWrapper("Sortable date/time", "s"));
            dateTimeFormat.ItemsSource = dateTimeFormats;

        }
        private void ButtonInfo_Click(object sender, RoutedEventArgs e) {
            txtEditValue.ClearValue(BaseEdit.EditValueProperty);
        }
    }
}
