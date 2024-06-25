using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Bars;
using DevExpress.XtraRichEdit;
using System.Windows.Data;
using System;
using System.Globalization;
using System.Collections;
using System.Windows.Markup;
using DevExpress.Xpf.RichEdit;
using DevExpress.Images;

namespace DevExpress.DevAV.Views {
    public partial class EmployeeMailMergeView : UserControl {
        public EmployeeMailMergeView() {
            InitializeComponent();
        }
        void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
            ((IRichEditControl)richEdit).Print();
        }
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Views.PrintLayoutView.AllowDisplayLineNumbers = false;
        }
        void richEdit_PopupMenuShowing(object sender, Xpf.RichEdit.PopupMenuShowingEventArgs e) {
            radialMenu.Items.Clear();
            var items = Validate(e.Menu.Items);
            foreach (var item in items) {
                radialMenu.Items.Add(item);
            }
            radialMenu.IsOpen = true;
            e.Menu.Items.Clear();
        }
        const int maxItemsInRadialmenu = 8;
        List<BarItem> Validate(CommonBarItemCollection items) {
            var filteredItems = items.Where(x => x is BarItem).Select(x => x as BarItem).Where(x => x.IsEnabled && x.IsVisible && !(x is BarItemSeparator)).ToList();
            if (filteredItems.Count <= maxItemsInRadialmenu)
                return filteredItems;
            var firstLevelItems = filteredItems.Where(i => i is BarSubItem).ToList();
            var anotherItems = filteredItems.Where(i => !(i is BarSubItem)).ToList();
            int additionCount = maxItemsInRadialmenu - 1 - firstLevelItems.Count;
            var firstLevelAnotherItems = anotherItems.Take(additionCount).ToList();
            anotherItems.RemoveRange(0, additionCount);
            var secondLevelItems = anotherItems;
            firstLevelItems.AddRange(firstLevelAnotherItems);
            var popupMenu = new PopupMenu();
            foreach (var item in secondLevelItems) {
                popupMenu.Items.Add(item);
            }
            firstLevelItems.Add(new BarSplitButtonItem() { PopupControl = popupMenu, Content = "Actions" });
            return firstLevelItems.ToList();
        }
    }
}
