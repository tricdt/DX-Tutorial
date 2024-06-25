using System;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using System.Windows;
using System.Windows.Data;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EditorsDemo {
    public partial class SearchControlOptions : UserControl {
        public static readonly DependencyProperty FocusedControlProperty = DependencyProperty.Register("FocusedControl", typeof(SearchControl), typeof(SearchControlOptions), null);
        public static readonly DependencyProperty CustomColumnsProperty = DependencyProperty.Register("CustomColumns", typeof(ObservableCollection<string>), typeof(SearchControlOptions), null);
        public SearchControlOptions() {
            InitializeComponent();
            ComboBoxEdit.SetupComboBoxEnumItemSource<FindMode, FindMode>(FindModeComboBox);
            ComboBoxEdit.SetupComboBoxEnumItemSource<FilterCondition, FilterCondition>(FilterConditionComboBox);
            ComboBoxEdit.SetupComboBoxEnumItemSource<CriteriaOperatorType, CriteriaOperatorType>(CriteriaOperatorTypeComboBox);
            ComboBoxEdit.SetupComboBoxEnumItemSource<FilterByColumnsMode, FilterByColumnsMode>(FilterByColumnsModeComboBox);
            CustomColumns = new ObservableCollection<string>();
        }

        public ObservableCollection<string> CustomColumns {
            get { return (ObservableCollection<string>)GetValue(CustomColumnsProperty); }
            private set { SetValue(CustomColumnsProperty, value); }
        }
        public SearchControl FocusedControl {
            get { return (SearchControl)GetValue(FocusedControlProperty); }
            set { SetValue(FocusedControlProperty, value); }
        }

        void FindModeComboBoxEditValueChanged(object sender, EditValueChangedEventArgs e) {
            ShowFindButtonCheckEdit.IsEnabled = object.Equals(FindModeComboBox.EditValue, FindMode.FindClick);
        }
        void FilterByColumnsModeComboBoxEditValueChanged(object sender, EditValueChangedEventArgs e) {
            CustomColumnsComboBox.IsEnabled = object.Equals(FilterByColumnsModeComboBox.EditValue, FilterByColumnsMode.Custom);
        }
        void CustomColumnsComboBoxSelectedIndexChanged(object sender, RoutedEventArgs e) {
            CustomColumns = new ObservableCollection<string>(CustomColumnsComboBox.SelectedItems.Cast<string>());
        }
    }
    public class ToIntConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return null;
            if (value is string)
                return int.Parse((string)value);
            return Convert.ToInt32(value);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }
    }
}
