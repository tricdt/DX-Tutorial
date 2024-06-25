using System.Collections.Generic;

namespace DevExpress.WindowsMailClient.Wpf.Data {
    public interface IDataProvider<T> {
        IEnumerable<T> GetItems();
    }
}
