using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;


namespace TreeListDemo {
    
    
    
    public partial class NodeTemplate : TreeListDemoModule {
        public NodeTemplate() {
            InitializeComponent();
            view.ExpandAllNodes();
        }
        private void NodeTemplateListBox_SelectionChanged(object sender, EditValueChangedEventArgs e) {
            if (treeList == null)
                return;
            switch (((ListBoxEdit)sender).SelectedIndex) {
                case 0:
                    view.DataRowTemplate = (DataTemplate)Resources["nodeDetailTemplate"];
                    break;
                case 1:
                    view.DataRowTemplate = (DataTemplate)Resources["expandableNodeDetailTemplate"];
                    break;
                case 2:
                    view.DataRowTemplate = (DataTemplate)Resources["nodeToolTipTemplate"];
                    break;
                case 3:
                    view.ClearValue(TreeListView.DataRowTemplateProperty);
                    break;
            }
        }
    }
   
    public class SpaceObjectsViewModel {
        public IList<SpaceObjects> SpaceObjects { get { return SpaceObjectData.DataSource; } }
    }
    
    #region Converters
    public class ImageDataToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || ((byte[])value).Length == 0)
                return Visibility.Collapsed;
            else return Visibility.Visible;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
