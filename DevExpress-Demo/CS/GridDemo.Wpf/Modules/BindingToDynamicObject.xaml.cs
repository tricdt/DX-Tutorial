using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace GridDemo {
    public partial class BindingToDynamicObject : GridDemoModule {
        ObservableCollection<ExpandoObject> list = new ObservableCollection<ExpandoObject>();
        public BindingToDynamicObject() {
            InitializeComponent();
            for(int i = 0; i < 50; i++) {
                IDictionary<string, object> row = new ExpandoObject(); 
                row["Id"] = i;
                row["FirstName"] = DataGenerator.GetFirstName();
                row["LastName"] = DataGenerator.GetLastName();
                list.Add((ExpandoObject)row);
            }
            grid.ItemsSource = list;
        }
        void OnCreateNewColumnButtonClick(object sender, RoutedEventArgs e) {
            string fieldName = ((ValueSelectorItem)TypeBox.SelectedItem).DisplayName + " Column";
            int index = GetUniqueColumnIndex(fieldName);
            fieldName = fieldName + " " + index.ToString();
            foreach(IDictionary<string, object> item in list) {
                item[fieldName] = GetRandomValue();
            }
            GridColumn column = new GridColumn();
            column.Binding = new Binding(fieldName) { Mode = BindingMode.TwoWay };
            column.Header = fieldName;
            grid.Columns.Add(column);
        }

        int GetUniqueColumnIndex(string fieldName) {
            return Enumerable.Range(1, int.MaxValue)
                            .First(i => !grid.Columns.Any(col => (string)col.Header == (fieldName + " " + i.ToString())));
        }

        object GetRandomValue() {
            var selectedType = (Type)TypeBox.EditValue;
            if(selectedType == typeof(int))
                return DataGenerator.GetInt();
            if(selectedType == typeof(string))
                return DataGenerator.GetString();
            if(selectedType == typeof(DateTime))
                return DataGenerator.GetDateTime();
            if(selectedType == typeof(bool))
                return DataGenerator.GetBool();
            throw new NotSupportedException();
        }
    }
}
