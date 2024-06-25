using DevExpress.DemoData;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.Grid;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GridDemo {
    
    
    
    public partial class NewItemRow : GridDemoModule {
        int newRowID = 10000;
		public NewItemRow() {
			InitializeComponent();

            NWindDataLoader dataLoader = ((NWindDataLoader)Resources["NWindDataLoader"]);
            grid.ItemsSource = new ObservableCollection<OrderDetail>((IEnumerable<OrderDetail>)dataLoader.OrderDetails);
        }

        protected override void Show() {
            base.Show();
            view.ScrollIntoView(view.FocusedRowHandle);
        }
		void view_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e) {
			grid.SetCellValue(e.RowHandle, colQuantity, 1);
            grid.SetCellValue(e.RowHandle, colUnitPrice, 100);
            grid.SetCellValue(e.RowHandle, colDiscount, 0);
            grid.SetCellValue(e.RowHandle, colOrderID, newRowID++);
		}
        void newItemRowPositionChanged(object sender, System.Windows.RoutedEventArgs e) {
            if(view.NewItemRowPosition != NewItemRowPosition.None) {
                view.FocusedRowHandle = GridControl.NewItemRowHandle;
                view.ScrollIntoView(view.FocusedRowHandle);
            }
        }
	}
}
