using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Grid;
using System.Windows.Data;
using DevExpress.Xpf.Editors;
using System.Xml.Serialization;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using DevExpress.Xpf.DemoBase.DataClasses;
using System.Linq;
using DevExpress.Xpf.Core;

namespace TreeListDemo {
    
    
    
    public partial class EndUserCustomization : TreeListDemoModule {
        int maxId;
        public EndUserCustomization() {
            InitializeComponent();
            maxId = EmployeesWithPhotoData.DataSource.Max(x => x.Id);
        }

        void lbSummary_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(lbSummary.SelectedIndex == 0) {
                TreeListControl.View.TotalSummaryPosition = TotalSummaryPosition.Bottom;
                TreeListControl.View.ShowFixedTotalSummary = false;
            }
            else {
                TreeListControl.View.TotalSummaryPosition = TotalSummaryPosition.None;
                TreeListControl.View.ShowFixedTotalSummary = true;
            }
        }
        void OnInitNewNode(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs e) {
            maxId += 1;
            view.SetNodeValue(e.Node, "Id",  maxId);
        }
    }
    public class EmployeeCategoryImageSelector : TreeListNodeImageSelector, IValueConverter {
        static EmployeeCategoryImageSelector() {
            ImageCache = new Dictionary<string, System.Windows.Media.ImageSource>();
        }
        static Dictionary<string, System.Windows.Media.ImageSource> ImageCache;
        public override System.Windows.Media.ImageSource Select(DevExpress.Xpf.Grid.TreeList.TreeListRowData rowData) {
            Employee empl = (rowData.Row as Employee);
            if(empl == null) return null;
            return GetImageByGroupName(empl.GroupName);
        }

        public static ImageSource GetImageByGroupName(string groupName) {
            if(groupName == null) return null;
            if(ImageCache.ContainsKey(groupName))
                return ImageCache[groupName];
            var extension = new SvgImageSourceExtension() { Uri = new Uri(GetImagePathByGroupName(groupName)), Size = new Size(16, 16) };
            var image = (ImageSource)extension.ProvideValue(null);
            ImageCache.Add(groupName, image);
            return image;
        }

        public static List<string> images = new List<string> { "administration", "inventory", "manufacturing", "quality", "research", "sales" };
        public static string GetImagePathByGroupName(string groupName) {
            groupName = groupName.ToLowerInvariant();
            foreach(string item in images) {
                if(groupName.Contains(item)) {
                    return "pack://application:,,,/TreeListDemo;component/Images/Categories/" + item + ".svg";
                }
            }
            return groupName;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return GetImagePathByGroupName((string)value);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    #region Converters
    public class NavigationStyleToIsEnabledConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return true;
            return ((GridViewNavigationStyle)value) != GridViewNavigationStyle.Row;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class HeaderToImageConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return null;
            return "/TreeListDemo;component/Images/HeaderIcons/" + ((string)value).Replace(" ", String.Empty) + ".svg";
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    
    #endregion
}
