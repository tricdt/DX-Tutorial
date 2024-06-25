using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Themes;
using DevExpress.Data.TreeList;

namespace TreeListDemo {    
    public partial class ScrollBarAnnotations : TreeListDemoModule {
        public ScrollBarAnnotations() {
            InitializeComponent();            
        }    

        private void view_ValidateCell(object sender, DevExpress.Xpf.Grid.TreeList.TreeListCellValidationEventArgs e) {
            if(e.CellValue != null) {
                switch(e.Column.FieldName) {
                    case "SeptemberFromPriorYear":
                    case "MarchFromPriorYear":
                        double numb = double.Parse(e.CellValue.ToString());
                        if(numb <= -0.3) {
                            e.IsValid = false;
                            e.SetError("'Change from Prior Year' is less than or equals to -30%");
                        }
                        break;
                    case "MarketShare":
                        double val = double.Parse(e.CellValue.ToString());
                        if(val < 0.12) {
                            e.IsValid = false;
                            e.SetError("Market Share is less than 12%");
                        }
                        break;
                    default:
                        return;
                }
            }
        }

        private HashSet<int> changedRows = new HashSet<int>();

        private void view_ScrollBarAnnotationsCreating(object sender, ScrollBarAnnotationsCreatingEventArgs e) {

            if(changedRows != null) {
                ScrollBarAnnotationInfo info = new ScrollBarAnnotationInfo(Brushes.Green, ScrollBarAnnotationAlignment.Left, 4, 3);
                e.CustomScrollBarAnnotations = new List<ScrollBarAnnotationRowInfo>(changedRows.Select(x => new ScrollBarAnnotationRowInfo(view.GetNodeByKeyValue(x).RowHandle, info)));
            }
        }

        private void view_NodeChanged(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeChangedEventArgs e) {            
            if(e.ChangeType == NodeChangeType.Content && changedRows != null)
                changedRows.Add(((SalesData)e.Node.Content).ID);          
        }

        private void listAnnotaions_Loaded(object sender, RoutedEventArgs e){
            listAnnotaions.SelectAll();    
        }
    }

    public class ToScrollBarAnnotationsModeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var list = value as IEnumerable;
            if(list == null)
                return ScrollBarAnnotationMode.None;
            ScrollBarAnnotationMode result = ScrollBarAnnotationMode.None;
            foreach(var item in list)
                result |= ((AnnotationsDataContext.AnnotationsItem)item).Mode;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class ColorAnnotationConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var key = values[0] as DevExpress.Xpf.Grid.Themes.TableViewThemeKeyExtension;
            if(key == null)
                return values[3];
            var treeWalker = values[1] as ThemeTreeWalker;
            var frameworkElement = values[2] as FrameworkElement;
            ThemeResourceConverter converter = new ThemeResourceConverter();
            return converter.Convert(new object[] { treeWalker, frameworkElement }, targetType, key, culture);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class AnnotationsDataContext {
        public class AnnotationsItem {
            public string Name { get; set; }
            public ScrollBarAnnotationMode Mode { get; set; }
            public object ResourceKey { get; set; }
            public Brush CustomColor { get; set; }
        }

        public List<AnnotationsItem> AnnotationsItems { get; private set; }

        public AnnotationsDataContext() {
            AnnotationsItems = new List<AnnotationsItem>(){
                new AnnotationsItem(){
                    Name = "Focused Row", 
                    Mode = ScrollBarAnnotationMode.FocusedRow, 
                    ResourceKey = new TableViewThemeKeyExtension() {ResourceKey = TableViewThemeKeys.AnnotationFocusedRowBrush}
                },
                 new AnnotationsItem(){
                    Name = "Selected Rows", 
                    Mode = ScrollBarAnnotationMode.Selected, 
                    ResourceKey = new TableViewThemeKeyExtension() {ResourceKey = TableViewThemeKeys.AnnotationSelectionBrush}
                },
                 new AnnotationsItem(){
                    Name = "Search Results", 
                    Mode = ScrollBarAnnotationMode.SearchResult, 
                    ResourceKey = new TableViewThemeKeyExtension() {ResourceKey = TableViewThemeKeys.AnnotationSearchBrush}
                },
                 new AnnotationsItem(){
                    Name = "Invalid Cells", 
                    Mode = ScrollBarAnnotationMode.InvalidCells, 
                    ResourceKey = new TableViewThemeKeyExtension() {ResourceKey = TableViewThemeKeys.AnnotationErrorBrush}
                },
                  new AnnotationsItem(){
                    Name = "Modified Rows", 
                    Mode = ScrollBarAnnotationMode.Custom, 
                    ResourceKey = null,
                    CustomColor = Brushes.Green
                },
            };
        }
    }
}
