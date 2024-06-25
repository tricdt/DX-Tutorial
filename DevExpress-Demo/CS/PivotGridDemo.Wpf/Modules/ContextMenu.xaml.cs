using System;
using System.Windows;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Mvvm;
using System.Linq;
using DevExpress.Mvvm.Native;

namespace PivotGridDemo {
    public partial class ContextMenu: PivotGridDemoModule {
        public ContextMenu() {
            InitializeComponent();
        }

        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(FieldArea.ColumnArea, true, false);
        }

        void OnPivotGridShowMenu(object sender, PopupMenuShowingEventArgs e) {
            if(e.MenuType.Equals(PivotGridMenuType.Header) && e.GetFieldInfo().Field.Area == FieldArea.DataArea) {
                e.Customizations.Add(new BarItemLinkSeparator());
                BarSubItem item = new BarSubItem() { Content = "Summary Type" };
                e.Customizations.Add(item);
                foreach(FieldSummaryType type in EnumExtensions.GetValues(typeof(FieldSummaryType))
                    .Cast<FieldSummaryType>()
                    .Except(FieldSummaryType.Custom.Yield())) {
                    item.ItemLinks.Add(new BarButtonItem() {
                        Name = "item" + type,
                        Content = type.ToString(),
                        CommandParameter = new FieldSummaryItem() { Type = type, Field = e.GetFieldInfo().Field },
                        Command = new DelegateCommand<FieldSummaryItem>(SetFieldSummaryType, CanSetFieldSummaryType)
                    });
                }
            }
        }

        void SetFieldSummaryType(FieldSummaryItem item) {
            item.Field.SummaryType = item.Type;
            item.Field.CellFormat = GetFormat(item.Type, item.Field);
            pivotGrid.BestFit(FieldArea.ColumnArea, true, false);
        }

        string GetFormat(FieldSummaryType fieldSummaryType, PivotGridField field) {
            if(field == fieldQuantity)
                return "";
            switch(fieldSummaryType){
                case FieldSummaryType.Sum:
                case FieldSummaryType.Median:
                case FieldSummaryType.Mode:
                    return "N";
                case FieldSummaryType.Custom:
                case FieldSummaryType.Max:
                case FieldSummaryType.Min:
                    return "c";
                case FieldSummaryType.Count:
                    return "";
                case FieldSummaryType.Average:
                    return field == fieldDiscount ? "p" : "c";
                case FieldSummaryType.StdDev:
                case FieldSummaryType.StdDevp:
                case FieldSummaryType.Var:
                case FieldSummaryType.Varp:
                    return "p";
            }
            return string.Empty;
        }

        bool CanSetFieldSummaryType(FieldSummaryItem item) { return !object.Equals(item.Field.SummaryType, item.Type); }
    }

    public class FieldSummaryItem {
        public PivotGridField Field { get; set; }
        public FieldSummaryType Type { get; set; }
    }
}
