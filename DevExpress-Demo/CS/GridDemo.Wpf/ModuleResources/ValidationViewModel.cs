using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.DemoBase.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GridDemo {
    [POCOViewModel]
    public class ValidationViewModel {
        const int RowCount = 8;
        static IEnumerable<ValidationData> GetValidationData() {
            var result = new List<ValidationData>();
            foreach(Employee employee in EmployeesData.DataSource) {
                var data = new ValidationData() {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.EmailAddress,
                    Address = employee.AddressLine1,
                    Phone = employee.Phone,
                    Title = employee.JobTitle,
                    ZipCode = employee.PostalCode,
                    HireDate = employee.HireDate,
                    Salary = DataGenerator.GetSalary(),
                    Facebook = DataGenerator.GetFacebookAddress(employee.FirstName, employee.LastName),
                    CreditCard = DataGenerator.GetCreditCardNumber(),
                };
                result.Add(data);
                if(result.Count > RowCount)
                    break;
            }
            result[GetRandomRowIndex()].FirstName = null;
            result[GetRandomRowIndex()].FirstName = null;
            result[GetRandomRowIndex()].LastName = null;
            result[GetRandomRowIndex()].LastName = null;
            result[GetRandomRowIndex()].HireDate = DateTime.Today.AddDays(2);
            result[GetRandomRowIndex()].HireDate = DateTime.Today.AddDays(3);
            result[GetRandomRowIndex()].Salary = -10000;
            result[GetRandomRowIndex()].Salary = 1000001;
            result[GetRandomRowIndex()].CreditCard = "0000 0000 0000 0000";
            result[GetRandomRowIndex()].CreditCard = "1234 1234 1234 1234";
            result[GetRandomRowIndex()].Address = null;
            result[GetRandomRowIndex()].Address = null;
            result[GetRandomRowIndex()].ZipCode = "000";
            result[GetRandomRowIndex()].ZipCode = "123";
            result[GetRandomRowIndex()].Phone = "555 1234";
            result[GetRandomRowIndex()].Phone = "(00o)555 1234";
            var dataItem = result[GetRandomRowIndex()];
            dataItem.Email = dataItem.Email.Replace("@", " ");
            dataItem = result[GetRandomRowIndex()];
            dataItem.Email = dataItem.Email.Replace("com", "");
            dataItem = result[GetRandomRowIndex()];
            dataItem.Facebook = dataItem.Email;
            dataItem = result[GetRandomRowIndex()];
            dataItem.Facebook = dataItem.Facebook.Replace("com", "");
            return result;
        }
        static IEnumerable<ValidationData_FluentAPI> GetValidationData_FluentAPI() {
            var result = new List<ValidationData_FluentAPI>();
            foreach(Employee employee in EmployeesData.DataSource) {
                var data = new ValidationData_FluentAPI() {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.EmailAddress,
                    Address = employee.AddressLine1,
                    Phone = employee.Phone,
                    Title = employee.JobTitle,
                    ZipCode = employee.PostalCode,
                    HireDate = employee.HireDate,
                    Salary = DataGenerator.GetSalary(),
                    Facebook = DataGenerator.GetFacebookAddress(employee.FirstName, employee.LastName),
                    CreditCard = DataGenerator.GetCreditCardNumber(),

                };
                result.Add(data);
                if(result.Count > RowCount)
                    break;
            }
            result[GetRandomRowIndex()].FirstName = null;
            result[GetRandomRowIndex()].FirstName = null;
            result[GetRandomRowIndex()].LastName = null;
            result[GetRandomRowIndex()].LastName = null;
            result[GetRandomRowIndex()].HireDate = DateTime.Today.AddDays(2);
            result[GetRandomRowIndex()].HireDate = DateTime.Today.AddDays(3);
            result[GetRandomRowIndex()].Salary = -10000;
            result[GetRandomRowIndex()].Salary = 1000001;
            result[GetRandomRowIndex()].CreditCard = "0000 0000 0000 0000";
            result[GetRandomRowIndex()].CreditCard = "1234 1234 1234 1234";
            result[GetRandomRowIndex()].Address = null;
            result[GetRandomRowIndex()].Address = null;
            result[GetRandomRowIndex()].ZipCode = "000";
            result[GetRandomRowIndex()].ZipCode = "123";
            result[GetRandomRowIndex()].Phone = "555 1234";
            result[GetRandomRowIndex()].Phone = "(00o)555 1234";
            var dataItem = result[GetRandomRowIndex()];
            dataItem.Email = dataItem.Email.Replace("@", " ");
            dataItem = result[GetRandomRowIndex()];
            dataItem.Email = dataItem.Email.Replace("com", "");
            dataItem = result[GetRandomRowIndex()];
            dataItem.Facebook = dataItem.Email;
            dataItem = result[GetRandomRowIndex()];
            dataItem.Facebook = dataItem.Facebook.Replace("com", "");
            return result;
        }
        static readonly Random random = new Random();
        static int GetRandomRowIndex() {
            return random.Next(RowCount);
        }

        protected ValidationViewModel() {
            Items = new List<CollectionInfo> { 
                new CollectionInfo(GetValidationData(), "Validation via Data Annotation attributes"),
                new CollectionInfo(GetValidationData_FluentAPI(), "Validation via Fluent API"), 
            };
            SelectedCollectionInfo = Items.First();
        }

        public List<CollectionInfo> Items { get; private set; }
        public virtual CollectionInfo SelectedCollectionInfo { get; set; }
    }
}
