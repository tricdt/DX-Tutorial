using DevExpress.Data;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;
using System.Windows.Input;

namespace GridDemo {
    
    
    
    public partial class BuiltInContextMenus : GridDemoModule {
        public static readonly DependencyProperty CellMenuInfoProperty = DependencyPropertyManager.Register("CellMenuInfo", typeof(GridCellMenuInfo), typeof(BuiltInContextMenus), new FrameworkPropertyMetadata(null));

        public ICommand DeleteRow { get; private set; }
        public ICommand CopyCellInfo { get; private set; }
        public ICommand CopyRowInfo { get; private set; }

        public GridCellMenuInfo CellMenuInfo {
            get { return (GridCellMenuInfo)GetValue(CellMenuInfoProperty); }
            set { SetValue(CellMenuInfoProperty, value); }
        }

        public BuiltInContextMenus() {
            DeleteRow = new DelegateCommand<object>(OnDeleteRow);
            CopyCellInfo = new DelegateCommand<object>(OnCopyCellInfo);
            CopyRowInfo = new DelegateCommand<object>(OnCopyRowInfo);
            InitializeComponent();
            grid.GroupBy(colCountry);
        }
        private void TableView_ShowGridMenu(object sender, DevExpress.Xpf.Grid.GridMenuEventArgs e) {
            switch(e.MenuType) {
                case GridMenuType.Column:
                    if(columnMenuRemoveItemCheck.IsChecked.Value)
                        e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.GroupBox });
                    if(!columnMenuAddItemCheck.IsChecked.Value) {
                        e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = "allowSortingItem" });
                        e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = "allowGroupingItem" });
                    }
                    break;
                case GridMenuType.GroupPanel:
                    if(groupPanelMenuRemoveItemCheck.IsChecked.Value)
                        e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.ClearGrouping });
                    if(!groupPanelMenuAddItemCheck.IsChecked.Value) {
                        e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = "allowAllSortingItem" });
                        e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = "allowAllGroupingItem" });
                    }
                    break;
                case GridMenuType.TotalSummary:
                    if(totalMenuRemoveItemCheck.IsChecked.Value)
                        e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultSummaryMenuItemNames.Customize });
                    if(!object.ReferenceEquals(e.MenuInfo.Column, colUnitPrice))
                        e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = "customSummaryMenuItem" });

                    break;
            }
            CellMenuInfo = e.MenuType == GridMenuType.RowCell ? (GridCellMenuInfo)e.MenuInfo : null;
        }
        void OnDeleteRow(object parameter) {
            if(parameter is int)
                view.DeleteRow(Convert.ToInt32(parameter));
        }
        void OnCopyCellInfo(object parameter) {
            GridCellMenuInfo menuInfo = parameter as GridCellMenuInfo;
            if(menuInfo != null && menuInfo.Row != null) {
                string text = GetCellText(menuInfo.Row.RowHandle.Value, menuInfo.Column);
                SetClibboardText(text);
            }
        }
        void OnCopyRowInfo(object parameter) {
            if(parameter is int) {
                grid.ClipboardCopyMode = ClipboardCopyMode.ExcludeHeader; 
                grid.CopyRowsToClipboard(new int[] { Convert.ToInt32(parameter) });
                grid.ClipboardCopyMode = ClipboardCopyMode.IncludeHeader; 
            }
        }
        decimal max, min;
        private void grid_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e) {
            if(object.Equals(e.SummaryProcess, CustomSummaryProcess.Start)) {
                min = decimal.MinValue;
            }
            if(e.SummaryProcess == CustomSummaryProcess.Calculate) {
                decimal value = (decimal)e.FieldValue;
                if(min == decimal.MinValue) {
                    min = max = value;
                }
                else {
                    max = Math.Max(max, value);
                    min = Math.Min(min, value);
                }
            }
            if(e.SummaryProcess == CustomSummaryProcess.Finalize)
                e.TotalValue = (min + max) / 2;
        }

        GridSummaryItem customItemCore;
        GridSummaryItem CustomItem {
            get {
                if(customItemCore == null)
                    customItemCore = GetCustomItem();
                return customItemCore;
            }
        }
        private GridSummaryItem GetCustomItem() {
            foreach(GridSummaryItem item in grid.TotalSummary) {
                if(item.Tag != null && item.Tag.ToString() == "customItem")
                    return item;
            }
            return null;
        }

        private void grid_CustomSummaryExists(object sender, DevExpress.Data.CustomSummaryExistEventArgs e) {
            if((grid != null) && (object.Equals(e.Item, CustomItem)))
                e.Exists = (view.TotalSummaryMenuCustomizations[0] as BarCheckItem).IsChecked.Value;
        }
        private void customSummaryMenuItem_CheckedChanged(object sender, ItemClickEventArgs e) {
            if(grid != null)
                CustomItem.SummaryType = (sender as BarCheckItem).IsChecked.Value ? SummaryItemType.Custom : SummaryItemType.None;
        }
        void SetClibboardText(string text) {
            try {
                Clipboard.SetText(text);
            } catch { }
        }

        string GetCellText(int rowHandle, ColumnBase column) {
            return Convert.ToString(grid.GetCellValue(rowHandle, (GridColumn)column));
        }
    }
}
