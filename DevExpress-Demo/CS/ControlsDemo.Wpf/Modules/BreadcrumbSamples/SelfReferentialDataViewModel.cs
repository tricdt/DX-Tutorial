using System.Collections.Generic;
using DevExpress.Mvvm;

namespace ControlsDemo.BreadcrumbSamples {
    public class SelfReferentialDataViewModel : ViewModelBase {
        public List<SelfRefDataItem> Items { get; set; }

        public SelfReferentialDataViewModel() {
            Items = new List<SelfRefDataItem> {
                new SelfRefDataItem() { Name = "Root item 1", Id = -1 },
                    new SelfRefDataItem() { Name = "Item 1.1", Id = 1, ParentId = -1 },
                        new SelfRefDataItem() { Name = "Item 1.1.1", Id = 3, ParentId = 1 },
                            new SelfRefDataItem() { Name = "Item 1.1.1.1", Id = 5, ParentId = 3 },
                        new SelfRefDataItem() { Name = "Item 1.1.2", Id = 4, ParentId = 1 },
                    new SelfRefDataItem() { Name = "Item 1.2", Id = 2, ParentId = -1 },
                new SelfRefDataItem() { Name = "Root item 2", Id = -2 },
                    new SelfRefDataItem() { Name = "Item 2.1", Id = 6, ParentId = -2 },
            };
        }

    }
    public class SelfRefDataItem {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public override string ToString() {
            return Name;
        }
    }
}
