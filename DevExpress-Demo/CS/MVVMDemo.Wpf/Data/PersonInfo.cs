using System.ComponentModel.DataAnnotations;

namespace MVVMDemo {
    public class PersonInfo {
        public static PersonInfo[] Persons {
            get {
                return new[] {
                    new PersonInfo() { FirstName = "Gregory", LastName = "Price" },
                    new PersonInfo() { FirstName = "Irma", LastName = "Marshal" },
                    new PersonInfo() { FirstName = "John", LastName = "Powell" },
                };
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(AutoGenerateField = false)]
        public string FullName { get { return FirstName + ' ' + LastName; } }
    }
}
