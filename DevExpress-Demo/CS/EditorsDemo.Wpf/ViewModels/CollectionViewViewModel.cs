using DevExpress.Xpf.DemoBase.DataClasses;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace EditorsDemo {

    public class CollectionViewViewModel {
        IList employees = new ObservableCollection<object>(EmployeesWithPhotoData.DataSource);
        public IList Employees { get { return employees; } }
        public ICollectionView CollectionView { get; private set; }
        public CollectionViewViewModel() {
            CollectionView = new CollectionViewSource() { Source = Employees }.View;
            CollectionView.GroupDescriptions.Add(new PropertyGroupDescription("JobTitle"));
            CollectionView.SortDescriptions.Add(new SortDescription("JobTitle", ListSortDirection.Ascending));
        }
    }
}
