using System;

namespace PropertyGridDemo {
    public class ValidationFluentAPIViewModel {
        public virtual FluentAPIValidationData Data { get; protected set; }
        public ValidationFluentAPIViewModel() {
            Data = new FluentAPIValidationData() {
                FirstName = "Anita",
                LastName = "Benson",
                Group = "Inventory Management",
                Title = "Purchasing Manager Purchasing Manager",
                HireDate = DateTime.Today.AddDays(1),
                Salary = -10000m,
                CreditCard = "1234 5678 1234 5678",
                Phone = "(713) 863 813",
                Email = "@gmail.com",
                Facebook = "ttps://www.facebook.com/anitabenson",
                AddressLine1 = "",
                AddressLine2 = "Seattle, 77025, USA",
                ZipCode = "1234",
                Gender = Gender.Female,
                BirthDate = new DateTime(1985, 3, 18)
            };
        }
    }
}
