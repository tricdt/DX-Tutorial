using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.DataAnnotations;
using CreditCardAttribute = System.ComponentModel.DataAnnotations.CreditCardAttribute;

namespace PropertyGridDemo {
    [MetadataType(typeof(DataAnnotationValidationMetadata))]
    public class DataAnnotationValidationData {
        const string ZipCodeRegExp = @"^\d{5}$";
        private const string ZipCodeErrorMessage = "The {0} field is not a valid zip code.";

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Group { get; set; }
        [StringLength(20)]
        public string Title { get; set; }
        [CustomValidation(typeof(DataAnnotationValidationMetadata), "IsHireDateValid")]
        public DateTime HireDate { get; set; }
        [Range(0, 1000000)]
        public decimal Salary { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }

        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [RegularExpression(ZipCodeRegExp, ErrorMessage = ZipCodeErrorMessage)]
        public string ZipCode { get; set; }

        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Url]
        public string Facebook { get; set; }

        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public static class DataAnnotationValidationMetadata {
        public static ValidationResult IsHireDateValid(DateTime date) {
            return date <= DateTime.Today ? ValidationResult.Success : new ValidationResult(string.Format("The {0} field cannot be in the future.", "HireDate"));
        }

        public static void BuildMetadata(MetadataBuilder<DataAnnotationValidationData> builder) {
            #region display options
            builder.Property(x => x.Salary)
                .CurrencyDataType();
            builder.Property(x => x.FirstName)
                .DisplayName("First name");
            builder.Property(x => x.LastName)
                .DisplayName("Last name");
            builder.Property(x => x.BirthDate)
                .DisplayName("Hire date");
            builder.Property(x => x.BirthDate)
                .DisplayName("Birth date");
            builder.Property(x => x.AddressLine1)
                .DisplayName("Address line 1");
            builder.Property(x => x.AddressLine2)
                .DisplayName("Address line 2");
            builder.Property(x => x.ZipCode)
                .DisplayName("Zip code");
            builder.Property(x => x.CreditCard)
                .DisplayName("Credit card");
            #endregion

            #region layout
            builder.Group("Name")
                .ContainsProperty(x => x.FirstName)
                .ContainsProperty(x => x.LastName);
            builder.Group("Job")
                .ContainsProperty(x => x.Group)
                .ContainsProperty(x => x.Title)
                .ContainsProperty(x => x.HireDate)
                .ContainsProperty(x => x.Salary)
                .ContainsProperty(x => x.CreditCard);
            builder.Group("Contact")
                .ContainsProperty(x => x.Phone)
                .ContainsProperty(x => x.Email)
                .ContainsProperty(x => x.Facebook);
            builder.Group("Personal")
                .ContainsProperty(x => x.Gender)
                .ContainsProperty(x => x.BirthDate);
            builder.Group("Address")
                .ContainsProperty(x => x.AddressLine1)
                .ContainsProperty(x => x.AddressLine2)
                .ContainsProperty(x => x.ZipCode);
            #endregion
        }
    }
}
