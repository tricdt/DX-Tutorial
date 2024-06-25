using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GridDemo {
    [POCOViewModel]
    public class LargeDataSourceViewModel {
        protected LargeDataSourceViewModel() { }

        public virtual bool IsLoading { get; set; }
        public virtual IList ItemsSource { get; set; }

        public void InitializeSource(int rowCount, string columnCountType) {
            if(ItemsSource == null)
                AssignSource(rowCount, columnCountType);
        }
        public void AssignSource(int rowCount, string columnCountType) {
            IsLoading = true;
            ItemsSource = CreateSource(rowCount, columnCountType);
            IsLoading = false;
        }

        static IList CreateSource(int rowCount, string columnCountType) {
            switch(columnCountType) {
            case "Medium":
                return CreateSource(rowCount, i => new LargeDataSourceObjectMedium(i));
            case "Large":
                return CreateSource(rowCount, i => new LargeDataSourceObjectLarge(i));
            case "Immense":
                return CreateSource(rowCount, i => new LargeDataSourceObjectImmense(i));
            default:
                throw new InvalidOperationException();
            }
        }
        static List<T> CreateSource<T>(int rowCount, Func<int, T> createItem) where T : LargeDataSourceObjectBase {
            return Enumerable.Range(0, rowCount)
                .Select(x => createItem(x))
                .ToList();
        }
    }
}
