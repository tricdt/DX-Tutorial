using DevExpress.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    public class ValidationData {
        const string ZipCodeRegExp = @"^\d{5}$";
        private const string ZipCodeErrorMessage = "The {0} field is not a valid zip code.";
        public static ValidationResult IsHireDateValid(DateTime date) {
            return date <= DateTime.Today ? ValidationResult.Success : new ValidationResult(string.Format("The {0} field cannot be in the future.", "HireDate"));
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }
        [CustomValidation(typeof(ValidationData), "IsHireDateValid")]
        public DateTime HireDate { get; set; }
        [Range(typeof(Decimal), "0", "1000000"), DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DemoCreditCard]
        public string CreditCard { get; set; }

        [Required]
        public string Address { get; set; }
        [RegularExpression(ZipCodeRegExp, ErrorMessage = ZipCodeErrorMessage)]
        public string ZipCode { get; set; }

        [DemoPhone]
        public string Phone { get; set; }
        [DemoEmailAddress]
        public string Email { get; set; }
        [DemoUrl]
        public string Facebook { get; set; }
    }
}
