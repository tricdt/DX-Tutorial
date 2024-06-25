using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.PropertyGrid;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm;
using System.Windows.Input;

namespace PropertyGridDemo {
    public partial class CollectionEditingModule : PropertyGridDemoModule {
        public CollectionEditingModule() {
            InitializeComponent();
            var itemInitializer = (VisualizerItemInitializer)Resources["itemInitializer"];                   
        }
        void OnVisualizerPanelLoaded(object sender, RoutedEventArgs e) {
            grid.ItemsSource = new List<object> {root};
        }

        private void OnVisualizerPanelChanged(object sender, EventArgs e) {
            if (grid != null)
                grid.RefreshData();
        }

        private void pGrid_CustomExpand(object sender, CustomExpandEventArgs args) {
            if (args.IsInitializing) {
                args.Row.Children.FirstOrDefault();
            }
        }

        private void pGrid_CollectionButtonsVisibility(object sender, CollectionButtonsVisibilityEventArgs e) {
            if(e.ButtonKind == CollectionButtonKind.Add) {
                var collection = (UIElementCollection)e.Value;
                e.IsVisible = collection.Count <= 5;
            }
            if(e.ButtonKind == CollectionButtonKind.Remove) {
                var button = e.Value as Button;
                if(button != null && string.Equals(button.Content as string, "Button", StringComparison.Ordinal))
                    e.IsVisible = false;
            }
        }
        private void pGrid_CollectionButtonClick(object sender, CollectionButtonClickEventArgs e) {
            if(e.ButtonKind == CollectionButtonKind.Add) {
                var collection = (UIElementCollection)e.Value;
                e.DefaultAction.Invoke(new Button { Content = "Button " + collection.OfType<Button>().Count() });
                e.Handled = true;
            }
        }
    }

    public class VisualItemsSelector : IChildNodesSelector {
        readonly VisualizerItemInitializer initializer = new VisualizerItemInitializer();
        IEnumerable IChildNodesSelector.SelectChildren(object item) {
            List<FrameworkElement> list = new List<FrameworkElement>();
            FrameworkElement fe = (FrameworkElement)item;
            if (((IInstanceInitializer)initializer).Types.Any(typeInfo => item.GetType() == typeInfo.Type)) 
                return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(fe); i++)
                list.Add((FrameworkElement)VisualTreeHelper.GetChild(fe, i));
            return list;
        }
    }
    public class ItemsSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new List<object> {value};
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class GetNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null ? value.GetType().Name : string.Empty;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class VisualizerItemInitializer : IInstanceInitializer {        
        public VisualizerItemInitializer() {
        }  
        object IInstanceInitializer.CreateInstance(TypeInfo type) {
            FrameworkElement element = null;
            if (type.Type == typeof(Button))
                element = new Button() { Content = type.Name };
            if (type.Type == typeof(CheckBox))
                element = new CheckBox() { Content = type.Name };
            if (type.Type == typeof(TextBox))
                element = new TextBox() { Text = type.Name };
            element.HorizontalAlignment = HorizontalAlignment.Center;
            element.Margin = new Thickness(5);
            return element;
        }        
        IEnumerable<TypeInfo> IInstanceInitializer.Types {
            get {
                var result = new List<TypeInfo>();
                result.Add(new TypeInfo(typeof(Button), "New Button"));
                result.Add(new TypeInfo(typeof(TextBox), "New Text Editor"));
                result.Add(new TypeInfo(typeof(CheckBox), "New CheckBox"));
                return result;
            }
        }        
    }
}
