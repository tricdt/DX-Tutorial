using DevExpress.DemoData;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.LookUp;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee;

namespace CommonDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiColumnLookupEditorTemplates.xaml")]
    public partial class StandaloneMultiColumnLookupEditor : CommonDemoModule {

        public StandaloneMultiColumnLookupEditor() {
            DataContext = ViewModel = new LookUpEditorDemoViewModel();
            Keyboard.AddGotKeyboardFocusHandler(this, new KeyboardFocusChangedEventHandler(OnKeyBoardFocusChanged));
            InitializeComponent();
        }

        LookUpEditorDemoViewModel ViewModel { get; set; }

        void OnKeyBoardFocusChanged(object sender, KeyboardFocusChangedEventArgs e) {
            var focused = e.NewFocus as DependencyObject;
            ViewModel.FocusedEditor = focused is LookUpEdit ? focused as LookUpEdit : LayoutHelper.FindParentObject<LookUpEdit>(focused);
        }
    }

    public class LookUpEditorDemoViewModel : ViewModelBase {

        public LookUpEditorDemoViewModel()  {
            var dataLoader = new NWindDataLoader();
            OrdersSource = new List<OrderDetailsExtended>(GetOrderDetails(dataLoader));
            SelectedOrders = new List<object>() { OrdersSource[0].ProductName, OrdersSource[2].ProductName, OrdersSource[10].ProductName };
            Employees = new List<Employee>(EmployeesWithPhotoData.DataSource);
            Products = new List<Product>((IList<Product>)dataLoader.Products);
        }

        public List<OrderDetailsExtended> OrdersSource { get; private set; }
        public IList<object> SelectedOrders { get; set; }
        public IList<Employee> Employees { get; private set; }
        public IList<Product> Products { get; private set; }
        public LookUpEdit FocusedEditor {
            get { return GetValue<LookUpEdit>(); }
            set { SetValue(value); }
        }

        IEnumerable<OrderDetailsExtended> GetOrderDetails(NWindDataLoader dataLoader) {
            return ((IEnumerable<OrderDetailsExtended>)dataLoader.OrderDetailsExtended).Distinct(new OrderEqualityComparer());
        }
    }

    public class OrderEqualityComparer : IEqualityComparer<OrderDetailsExtended> {
        public bool Equals(OrderDetailsExtended x, OrderDetailsExtended y) {
            if(x == null && y != null)
                return false;
            if(x != null && y == null)
                return false;
            return x.ProductName == y.ProductName;
        }

        public int GetHashCode(OrderDetailsExtended obj) {
            return obj.ProductName.GetHashCode();
        }
    }

    public class LookUpEditOptionsTemplateSelector : DataTemplateSelector {

        public DataTemplate LookUpTemplate { get; set; }
        public DataTemplate SearchLookUpTemplate { get; set; }
        public DataTemplate MultiSelectLookUpTemplate { get; set; }
        public DataTemplate PlaceHolderTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var edit = item as LookUpEditBase;
            if(edit != null) {
                switch(edit.Name) {
                    case "defaultLookUpEdit": return LookUpTemplate;
                    case "searchLookUpEdit": return SearchLookUpTemplate;
                    case "multiSelectLookUpEdit": return MultiSelectLookUpTemplate;
                }
            }
            return PlaceHolderTemplate;
        }
    }
}
