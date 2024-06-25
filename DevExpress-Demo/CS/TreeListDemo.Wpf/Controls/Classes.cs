using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Grid;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid.TreeList;
using System.Windows.Media;
using DevExpress.Xpf.Editors;
using System.Collections.ObjectModel;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Core;
using System.Collections;
using DevExpress.Mvvm.UI.Interactivity;

namespace TreeListDemo {
    public class NavigationStyleList : List<GridViewNavigationStyle> {
    }
    #region Converters
    public class ShowTreeListLinesConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return !object.Equals(value, TreeListLineStyle.None);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? TreeListLineStyle.Solid : TreeListLineStyle.None;
        }
        #endregion
    }

    public class AlertVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class PriorityTemplateSelector : IValueConverter {
        public DataTemplate HighTemplate { get; set; }
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate LowTemplate { get; set; }

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            TreeListRowData rowData = value as TreeListRowData;
            if(rowData != null) {
                Task task = rowData.Row as Task;
                if(task != null) {
                    switch(task.Priority) {
                        case Priority.Low:
                            return LowTemplate;
                        case Priority.Normal:
                            return NormalTemplate;
                        case Priority.High:
                            return HighTemplate;
                    }
                }
            }
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class ObjectIsTaskConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value is Task;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class SummaryIconVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return null;
            if(value.GetType() == typeof(TaskObject))
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class CountryToFlagImageConverter : BytesToImageSourceConverter {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            foreach(Country item in CountriesData.DataSource) {
                if(item.Name == (string)value)
                    return base.Convert(item.Flag, targetType, parameter, culture);
            }
            return null;
        }
    }
    #endregion

    public static class DragDropSourceGenerator {
        public static IEnumerable<Employee> GetActiveEmployees() {
            return GetEmployees(false);
        }
        public static IEnumerable<Employee> GetNewEmployees() {
            return GetEmployees(true);
        }
        static IEnumerable<Employee> GetEmployees(bool isNew) {
            HashSet<int> newIds = new HashSet<int>() {
                106, 002, 176, 128, 278, 231, 198, 201, 272
            };
            return EmployeesData.DataSource.Where(x => (!(newIds.Contains(x.Id) ^ isNew)));
        }
    }

    public class TaskContentControl : ContentControl {
        public static readonly DependencyProperty ShowBorderProperty =
            DependencyPropertyManager.Register("ShowBorder", typeof(bool), typeof(TaskContentControl), new PropertyMetadata(false));
        public TaskContentControl() {
            this.SetDefaultStyleKey(typeof(TaskContentControl));
        }
        public bool ShowBorder {
            get { return (bool)GetValue(ShowBorderProperty); }
            set { SetValue(ShowBorderProperty, value); }
        }
    }
    public class IEnumerableToFirstItemConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return value;
            IEnumerator enumerator = ((IEnumerable)value).GetEnumerator();
            enumerator.MoveNext();
            object result = enumerator.Current;
            enumerator.Reset();
            return result;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class AutoSuggestEditInplaceEditingBehavior : Behavior<AutoSuggestEdit> {
        readonly DevExpress.DemoData.Models.NWindContext context = DevExpress.DemoData.Models.NWindContext.Create();
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.QuerySubmitted += AssociatedObjectOnQuerySubmitted;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.QuerySubmitted -= AssociatedObjectOnQuerySubmitted;
        }
        void AssociatedObjectOnQuerySubmitted(object sender, AutoSuggestEditQuerySubmittedEventArgs e) {
            AssociatedObject.ItemsSource = string.IsNullOrEmpty(e.Text)
                ? this.context.Products.ToArray()
                : this.context.Products.Where(x => x.ProductName.StartsWith(e.Text)).ToArray();
        }
    }
}
