using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevExpress.DemoData.Models;
using DevExpress.Office.Services;

namespace RichEditDemo {
    public class DBUriStreamProvider : IUriStreamProvider {
        static readonly string prefix = "dbimg://";

        readonly IList<Employee> employees;

        public DBUriStreamProvider(IList<Employee> employees) {
            this.employees = employees;
        }

        Stream IUriStreamProvider.GetStream(string uri) {
            uri = uri.Trim();
            if (!uri.StartsWith(prefix))
                return null;
            string strId = uri.Substring(prefix.Length).Trim();
            int id;
            if (!int.TryParse(strId, out id))
                return null;
            byte[] bytes = employees.First(employee => employee.EmployeeID == id).Photo;
            if (bytes == null)
                return null;
            return new MemoryStream(bytes);
        }
    }
}
