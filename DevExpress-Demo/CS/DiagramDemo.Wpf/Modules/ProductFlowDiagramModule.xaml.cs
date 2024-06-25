using DevExpress.Data.Filtering;
using DevExpress.Diagram.Core;
using DevExpress.Xpf.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using DevExpress.Diagram.Demos;

namespace DiagramDemo {
    public partial class ProductFlowDiagramModule : DiagramDemoModule {
        readonly DiagramItemStyleId[] styles = DiagramShapeStyleId.Styles.ToArray();
        readonly ProductFlowInfo info;
        public ProductFlowDiagramModule() {
            InitializeComponent();
            diagramControl.Commands.RegisterHotKeys(ClearHotKeys);
            info = OrderDataGenerator.GenerateProductFlowInfo();
            DataContext = info;
        }
        void ClearHotKeys(IHotKeysRegistrator registrator) {
            registrator.ClearHotKeys(DiagramCommandsBase.SaveFileAsCommand);
            registrator.ClearHotKeys(DiagramCommandsBase.SaveFileCommand);
        }
        void SelectionChanged(object sender, EventArgs e) {
            var selectedDiagramItem = this.diagramControl.PrimarySelection;
            gridControl.ClearGrouping();
            gridControl.FilterCriteria = null;

            if(!this.diagramControl.SelectedItems.Any())
                return;
            var customers = GetDataItems<CustomerData>();
            var categories = GetDataItems<CategoryData>();
            var connectors = GetDataItems<ProductFlowData>();

            if(connectors.Any()) {
                var connectorsCriteria = connectors.Select(c => new GroupOperator(GroupOperatorType.And, 
                    GetCategoryOperator(c.Category.Name), GetCustomerOperator(c.Customer.Name)));
                gridControl.FilterCriteria = new GroupOperator(GroupOperatorType.Or, connectorsCriteria);
                GroupGridControl("Category.Name");
                return;
            }
            if(customers.Any() && !categories.Any()) {
                var customersCriteria = customers.Select(c => GetCustomerOperator(c.Name));
                gridControl.FilterCriteria = new GroupOperator(GroupOperatorType.Or, customersCriteria);
                GroupGridControl("Category.Name");
            }
            if(categories.Any()) {
                var productCriteria = new GroupOperator(GroupOperatorType.Or, categories.Select(c => GetCategoryOperator(c.Name)));
                GroupOperator customersCriteria = null;
                if(customers.Any())
                    customersCriteria = new GroupOperator(GroupOperatorType.Or, customers.Select(c => GetCustomerOperator(c.Name)));
                gridControl.FilterCriteria = customers.Any() ? new GroupOperator(GroupOperatorType.And, customersCriteria, productCriteria) : productCriteria;
                GroupGridControl("Customer.Name");
            }
        }
        void GroupGridControl(string fieldName) {
            gridControl.GroupBy(fieldName);
            gridControl.ExpandAllGroups();
        }
        void dataBindingBehavior_GenerateItem(object sender, DiagramGenerateItemEventArgs e) {
            var templateName = (e.DataObject is CustomerData) ? "CustomerTemplate" : "CategoryTemplate";
            e.Item = e.CreateItemFromTemplate(templateName);
        }
        void dataBindingBehavior_CustomLayoutItems(object sender, DiagramCustomLayoutItemsEventArgs e) {
            ArrangeItemsInLine<CategoryData>(e.Items, new Point(600, 50), new Size(150, 105), 20);
            ArrangeItemsInLine<CustomerData>(e.Items, new Point(50, 100), new Size(150, 105), 20);
            foreach(var item in e.Items) {
                var customer = item.DataContext as CustomerData;
                if(customer != null) {
                    item.ThemeStyleId = styles[Array.IndexOf(info.Customers, customer)];
                }
            }
            foreach(var connector in e.DiagramConnectors) {
                var connectorData = (ProductFlowData)connector.DataContext;
                connector.ThemeStyleId = styles[Array.IndexOf(info.Customers, connectorData.Customer)];
            }
            e.Handled = true;
        }
        void ArrangeItemsInLine<TDataContext>(IEnumerable<DiagramItem> items, Point startPosition, Size itemSize, int margin) {
            Point position = startPosition;
            foreach(var diagramItem in items.Where(x => x.DataContext is TDataContext)) {
                diagramItem.Position = position;
                position.Offset(0, itemSize.Height + margin);
            }
        }
        private void dataBindingBehavior_UpdateConnector(object sender, DiagramUpdateConnectorEventArgs e) {
            var connectorData = (ProductFlowData)e.DataObject;
            e.Connector.StrokeThickness = connectorData.Weight;
        }
        void DiagramControl_ItemsChanged(object sender, DiagramItemsChangedEventArgs e) {
            if(diagramControl.Items.Count == 1)
                diagramControl.SelectItem(diagramControl.Items.First());
        }
        void dataBindingBehavior_ItemsGenerated(object sender, DiagramItemsGeneratedEventArgs e) {
            diagramControl.IsReadOnly = true;
        }
        IEnumerable<T> GetDataItems<T>() {
            return this.diagramControl.SelectedItems.Select(x => x.DataContext).Where(x => x is T).Cast<T>();
        }
        BinaryOperator GetCategoryOperator(string value) {
            return GetEqualOperator("Category.Name", value);
        }
        BinaryOperator GetCustomerOperator(string value) {
            return GetEqualOperator("Customer.Name", value);
        }
        BinaryOperator GetEqualOperator(string propertyName, string value) {
            return new BinaryOperator(propertyName, value, BinaryOperatorType.Equal);
        }
    }
}
