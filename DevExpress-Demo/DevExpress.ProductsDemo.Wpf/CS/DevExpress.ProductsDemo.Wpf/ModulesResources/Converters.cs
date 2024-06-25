using DevExpress.Data.Helpers;
using DevExpress.Utils;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using System.Globalization;
using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace ProductsDemo.Modules {
    public class EmptyPhotoConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value != null) return value;
            return new BitmapImage(new Uri(@"pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Contacts/Unknown-user.png", UriKind.RelativeOrAbsolute));
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }
    }
    public class ViewToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || !(value is ViewType)) return Visibility.Collapsed;
            ViewType viewType = (ViewType)value;
            return viewType == ViewType.TableView ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class ViewToMarginConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value is TableView ? new Thickness(1, 1, 4, 1) : new Thickness(1);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class ObjectToOpacityConverter : IValueConverter {
        public bool Invert { get; set; }
        public double Opacity { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (value != null ^ Invert) ? Opacity : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ColumnIconConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || !(value is int)) return null;
            int intValue = (int)value;
            var extension = new SvgImageSourceExtension() { Uri = new Uri(string.Format(@"pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Status_{0}.svg", intValue), UriKind.RelativeOrAbsolute), Size = new Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class ColumnPriorityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || !(value is TaskPriority)) return null;
            TaskPriority priority = (TaskPriority)value;
            if(priority == TaskPriority.Medium) return null;
            var extension = new SvgImageSourceExtension() { Uri = new Uri(String.Format(@"pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Priority_{0}.svg", priority), UriKind.RelativeOrAbsolute), Size = new Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class ColumnCategoryImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || !(value is TaskCategory)) return null;
            TaskCategory category = (TaskCategory)value;
            return String.Format(@"pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_{0}.svg", category);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class ColumnFlagStatusImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || !(value is FlagStatus)) return null;
            FlagStatus status = (FlagStatus)value;
            var extension = new SvgImageSourceExtension() { Uri = new Uri(String.Format(@"pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/{0}.svg", status), UriKind.RelativeOrAbsolute), Size = new Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class TaskStatusCompletedConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null || !(value is TaskStatus)) return false;
            TaskStatus status = (TaskStatus)value;
            return status == TaskStatus.Completed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class BoolToDecorationsConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null || !(value is bool)) return null;
            bool b = (bool)value;
            return !b ? null : TextDecorations.Strikethrough;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class SplitStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value == null ? null : SplitStringHelper.SplitPascalCaseString(value.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }

    public class FieldNameToTemplateConverter : BindableBase, IValueConverter {
        static Dictionary<string, object> cache = new Dictionary<string, object>();

        string targetTemplateName = null;
        public string TargetTemplateName {
            get { return targetTemplateName; }
            set {
                targetTemplateName = value;
                RaisePropertiesChanged("TargetTemplateName");
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            GridColumnBase column = value as GridColumnBase;
            if(column == null || String.IsNullOrWhiteSpace(column.FieldName))
                return null;
            string fullTemplateName = String.Format("{0}_{1}", column.FieldName, TargetTemplateName);
            if(!cache.ContainsKey(fullTemplateName))
                cache.Add(fullTemplateName, column.TryFindResource(fullTemplateName));
            return cache[fullTemplateName];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class PhoneFormatConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return string.Empty;
            string phoneNumber = (string)value;
            if(phoneNumber.Length == 0)
                return phoneNumber;
            return string.Format("{0} {1}", phoneNumber.Substring(0, 5), phoneNumber.Substring(5));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class MaskToTextEditSettingsConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string mask = value as string;
            if(value == null)
                return null;
            return new TextEditSettings { Mask = mask, MaskUseAsDisplayFormat = true, MaskType = DevExpress.Xpf.Editors.MaskType.RegEx };
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class AddressDetailPanelConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Address address = value as Address;
            if(address == null)
                return null;
            return address.GetAddressFullString(" | ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class FocusedRowHandleToActiveRecordConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            GridViewBase view = parameter as GridViewBase;
            if (value == null || value.GetType() != typeof(int) || view == null)
                return -1;
            int focusedRowHandle = (int)value;
            return view.Grid.GetListIndexByRowHandle(focusedRowHandle);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            GridViewBase view = parameter as GridViewBase;
            if (value == null || value.GetType() != typeof(int) || view == null)
                return -1;
            int activeRecord = (int)value;
            return view.Grid.GetRowHandleByListIndex(activeRecord);
        }
    }
}
