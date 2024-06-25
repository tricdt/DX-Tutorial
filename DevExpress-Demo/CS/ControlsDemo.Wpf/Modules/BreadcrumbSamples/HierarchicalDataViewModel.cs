using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;

namespace ControlsDemo.BreadcrumbSamples {
    public class HierarchicalDataViewModel : ViewModelBase {
        public List<DataItem> Items { get; set; }

        public HierarchicalDataViewModel() {
            Items = new List<DataItem>() {
                new DataItem("Root Item 1", new[] {
                    new DataItem("Item 1.1", new[] {
                        new DataItem("Item 1.1.1"),
                        new DataItem("Item 1.1.2"),
                    }),
                    new DataItem("Item 1.2")
                }),
                new DataItem("Root Item 2", new[] {
                    new DataItem("Item 2.1")
                })
            };
        }
    }

    public class DataItem {
        public DataItem(string name) {
            Name = name;
        }
        public DataItem(string name, DataItem[] children) : this(name) {
            Children = children.ToList();
        }

        public List<DataItem> Children { get; set; }
        public string Name { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}
