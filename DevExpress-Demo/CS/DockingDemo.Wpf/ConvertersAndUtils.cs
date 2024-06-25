using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DockingDemo {
    
    public class UniversalContainer<T> {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public T Value { get; set; }
        public override bool Equals(object obj) {
            return obj is UniversalContainer<T> ? object.Equals(Value, ((UniversalContainer<T>)obj).Value) : false;
        }
        public override int GetHashCode() {
            return Value.GetHashCode();
        }
    }
    public class AutoHideExpandModeContainer : UniversalContainer<AutoHideExpandMode> { }
    public class AutoHideModeContainer : UniversalContainer<AutoHideMode> { }
    public class TileLayoutContainer : UniversalContainer<TileLayout> { }

    
    public class UniversalContainerConverter<T> : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value is T ? GetContainer((T)value) : null;
        }
        protected virtual object GetContainer(T value) {
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((UniversalContainer<T>)value).Value;
        }
        #endregion
    }
    public class UniversalContainerConverter2<TValue, TContainer> : UniversalContainerConverter<TValue> where TContainer : UniversalContainer<TValue> {
        protected override object GetContainer(TValue value) {
            TContainer container = Activator.CreateInstance<TContainer>();
            container.Value = value;
            return container;
        }
    }
    public class AutoHideExpandModeContainerConverter : UniversalContainerConverter2<AutoHideExpandMode, AutoHideExpandModeContainer> { }
    public class AutoHideModeContainerConverter : UniversalContainerConverter2<AutoHideMode, AutoHideModeContainer> { }
    public class TileLayoutContainerConverter : UniversalContainerConverter<TileLayout> { }
    public class SourceConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public enum TileLayout {
        Default, Layout3x2, Layout3x3, Layout4x2
    }
    static class TileLayoutExtension {
        public static int GetRowCount(TileLayout layout) {
            return (layout == TileLayout.Layout3x3) ? 3 : 2;
        }
        public static int GetColumnsCount(TileLayout layout) {
            return (layout == TileLayout.Layout4x2) ? 4 : 3;
        }
    }
    static class TileImageHelper {
        static IDictionary<int, Image> images = new Dictionary<int, Image>();
        public static Image GetImage(int index) {
            Image result = null;
            if(!images.TryGetValue(index, out result)) {
                Uri uri = new Uri(string.Format("/Images/TileImages/{0:D2}.jpg", index), UriKind.Relative);
                result = new Image();
                result.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
                images.Add(index, result);
            }
            return result;
        }
    }
    public static class FontSizes {
        static double[] itemsCore = new double[] { 
            3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 
            10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0,
            16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
            32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
            80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0};
        public static double[] Items {
            get { return itemsCore; }
        }
    }
    public static class FontFamilies {
        static List<string> fontNames;
        public static List<string> FontNames {
            get {
                if(fontNames == null)
                    fontNames = GetSystemFontNames();
                return fontNames;
            }
        }
        static List<string> GetSystemFontNames() {
            List<string> systemFontNames = new List<string>();
            foreach(FontFamily fam in Fonts.SystemFontFamilies) {
                systemFontNames.Add(fam.Source);
            }
            return systemFontNames;
        }
    }
    
    public class Employee : DependencyObject {
        public string TitleOfCourtesy { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string HomePhone { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string BirthDate { get; set; }
        public string HireDate { get; set; }
        public string Extension { get; set; }
        public ImageSource Photo { get; set; }

        public static List<Employee> CreateSampleData() {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee();
            employee.TitleOfCourtesy = "Dr.";
            employee.Title = "Sales Representative";
            employee.FirstName = "Andrew";
            employee.LastName = "Fuller";
            employee.Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association.";
            employee.Address = "908 W. Capital Way";
            employee.Country = "USA";
            employee.City = "Tacoma";
            employee.HomePhone = "(206) 555-9482";
            employee.Region = "WA";
            employee.PostalCode = "98401";
            employee.Photo = new BitmapImage(new Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person1.png", UriKind.RelativeOrAbsolute));
            employees.Add(employee);

            employee = new Employee();
            employee.FirstName = "Janet";
            employee.LastName = "Leverling";
            employee.Notes = "Janet has a BS degree in chemistry from Boston College (1984).  She has also completed a certificate program in food retailing management.  Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992.";
            employee.City = "Kirkland";
            employee.Photo = new BitmapImage(new Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person2.png", UriKind.RelativeOrAbsolute));
            employees.Add(employee);

            employee = new Employee();
            employee.TitleOfCourtesy = "Dr.";
            employee.LastName = "Evil";
            employee.Photo = new BitmapImage(new Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person3.png", UriKind.RelativeOrAbsolute));
            employees.Add(employee);
            return employees;
        }
    }
    public class CommandsModel {
        public ICommand Close { get; private set; }
        public ICommand MDIStyle { get; private set; }
        public ICommand Cascade { get; private set; }
        public ICommand TileHorizontal { get; private set; }
        public ICommand TileVertical { get; private set; }
        public ICommand ArrangeIcons { get; private set; }

        public CommandsModel() {
            Close = DockControllerCommand.Close;
            MDIStyle = MDIControllerCommand.ChangeMDIStyle;
            Cascade = MDIControllerCommand.Cascade;
            TileHorizontal = MDIControllerCommand.TileHorizontal;
            TileVertical = MDIControllerCommand.TileVertical;
            ArrangeIcons = MDIControllerCommand.ArrangeIcons;
        }
        public object Target { get; set; }
    }
}
