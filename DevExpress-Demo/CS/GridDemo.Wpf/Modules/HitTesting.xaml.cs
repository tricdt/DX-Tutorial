using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GridDemo {
    
    
    
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/HitTestTemplates.xaml")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/HitTestClasses.(cs)")]
    public partial class HitTesting : GridDemoModule {
        DevExpress.Xpf.Grid.TableView TableView { get { return (DevExpress.Xpf.Grid.TableView)grid.View; } }
        ObservableCollection<HitTestInfo> hitInfoList = new ObservableCollection<HitTestInfo>();
        Point startPosition;
        int floatingContainerIsOpenCount;


        public bool AllowShowHitInfo {
            get { return (bool)GetValue(AllowShowHitInfoProperty); }
            set { SetValue(AllowShowHitInfoProperty, value); }
        }

        public static readonly DependencyProperty AllowShowHitInfoProperty =
            DependencyProperty.Register("AllowShowHitInfo", typeof(bool), typeof(HitTesting), new UIPropertyMetadata(true));


        public HitTesting() {
            InitializeComponent();

            viewsListBox.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(viewsListBox_SelectionChanged);

            FloatingContainer.AddFloatingContainerIsOpenChangedHandler(this, OnFloatingContainerIsOpenChanged);
            grid.Loaded += new RoutedEventHandler(grid_Loaded);

            hitIfoItemsControl.ItemsSource = hitInfoList;

        }
        void grid_Loaded(object sender, RoutedEventArgs e) {
            MultiBinding mBinding = new MultiBinding();
            mBinding.Mode = BindingMode.OneWay;
            Binding bIsMouseOver = new Binding() { Mode = BindingMode.OneWay, ElementName = "grid", Path = new PropertyPath("IsMouseOver", null) };
            mBinding.Bindings.Add(bIsMouseOver);
            Binding bIsMouseCaptureWithin = new Binding() { Mode = BindingMode.OneWay, ElementName = "grid", Path = new PropertyPath("IsMouseCaptureWithin", null), Converter = new NegationConverterExtension() };
            mBinding.Bindings.Add(bIsMouseCaptureWithin);
            Binding bIsChecked = new Binding() { Mode = BindingMode.OneWay, ElementName = "showHitInfoCheckEdit", Path = new PropertyPath("IsChecked", null) };
            mBinding.Bindings.Add(bIsChecked);
            Binding bAllowShowHitInfo = new Binding() { Mode = BindingMode.OneWay, RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, this.GetType(), 1), Path = new PropertyPath("AllowShowHitInfo", null) };
            mBinding.Bindings.Add(bAllowShowHitInfo);
            mBinding.Converter = new AndConverter();
            hitInfoPopup.SetBinding(PopupBase.IsOpenProperty, mBinding);
        }
        void viewsListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            grid.View = (GridViewBase)FindResource(viewsListBox.SelectedIndex == 0 ? "tableView" : "cardView");
        }

        private void grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            Point location = e.GetPosition(grid);
            double hOffset = location.X - startPosition.X;
            if(FlowDirection == System.Windows.FlowDirection.RightToLeft)
                hOffset = -hOffset;

            hitInfoPopup.HorizontalOffset = hOffset;
            hitInfoPopup.VerticalOffset = location.Y - startPosition.Y;

            GridViewHitInfoBase info = GetHitInfo(e);

            hitInfoList.Clear();

            AddHitInfo("HitTest", TypeDescriptor.GetProperties(info)["HitTest"].GetValue(info).ToString());

            AddHitInfo("Column", info.Column != null ? info.Column.HeaderCaption as string : "No column");
            AddHitInfo("RowHandle", GetRowHandleDescription(info.RowHandle));
            AddHitInfo("CellValue", info.Column != null ? grid.GetCellDisplayText(info.RowHandle, info.Column) : null);
            info.Accept(CreateDemoHitTestVisitor());
        }
        void OnFloatingContainerIsOpenChanged(object sender, FloatingContainerEventArgs e) {
            if(e.Container.IsOpen)
                floatingContainerIsOpenCount++;
            else
                floatingContainerIsOpenCount--;
            AllowShowHitInfo = floatingContainerIsOpenCount == 0;
        }
        GridViewHitTestVisitorBase CreateDemoHitTestVisitor() {
            if(grid.View is DevExpress.Xpf.Grid.TableView)
                return new DemoTableViewHitTestVisitor(this);
            return new DemoCardViewHitTestVisitor(this);
        }
        GridViewHitInfoBase GetHitInfo(RoutedEventArgs e) {
            if(grid.View is DevExpress.Xpf.Grid.TableView)
                return (GridViewHitInfoBase)TableView.CalcHitInfo(e.OriginalSource as DependencyObject);
            return ((DevExpress.Xpf.Grid.CardView)grid.View).CalcHitInfo(e.OriginalSource as DependencyObject);

        }
        string GetRowHandleDescription(int rowHanle) {
            if(rowHanle == GridControl.InvalidRowHandle)
                return "No row";
            if(rowHanle == GridControl.NewItemRowHandle)
                return "New Item Row";
            if(rowHanle == GridControl.AutoFilterRowHandle)
                return "Auto Filter Row";
            return string.Format("{0} ({1})", rowHanle, grid.IsGroupRowHandle(rowHanle) ? "group row" : "data row");
        }
        internal void AddHitInfo(string name, string text) {
            hitInfoList.Add(new HitTestInfo(name, text));
        }
        internal void RemoveHitInfo(string name) {
            HitTestInfo infoToRemove = hitInfoList.Where(info => info.Name == name).FirstOrDefault();
            if(infoToRemove != null)
                hitInfoList.Remove(infoToRemove);
        }
        internal void AddTotalSummaryInfo(ColumnBase column) {
            AddHitInfo("TotalSummary", column.TotalSummaryText);
        }
        internal void AddFixedTotalSummaryInfo(string summaryText) {
            RemoveHitInfo("CellValue");
            AddHitInfo("FixedTotalSummary", summaryText);
        }
        internal void AddGroupValueInfo(GridColumnData columnData) {
            AddHitInfo("GroupValue", string.Format("{0}: {1}", columnData.Column.FieldName, columnData.Value));
        }
        internal void AddGroupSummaryInfo(GridGroupSummaryData summaryData) {
            AddHitInfo("GroupSummary", summaryData.Text);
        }
        void hitInfoPopup_Opened(object sender, EventArgs e) {
            startPosition = Mouse.GetPosition(grid);
        }

    }
}
