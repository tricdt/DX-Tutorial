using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    [MetadataType(typeof(ValidationFluentAPIMetadata))]
    public class ValidationData_FluentAPI {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string CreditCard { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }

    }
    public static class ValidationFluentAPIMetadata {
        public static void BuildMetadata(MetadataBuilder<ValidationData_FluentAPI> builder) {
            #region validation
            builder.Property(x => x.FirstName)
                .Required();
            builder.Property(x => x.LastName)
                .Required();
            builder.Property(x => x.Email)
                .EmailAddressDataType();
            builder.Property(x => x.Phone)
                .PhoneNumberDataType();
            builder.Property(x => x.Facebook)
                .UrlDataType();
            builder.Property(x => x.Title)
                .MaxLength(30);
            builder.Property(x => x.HireDate)
                .MatchesRule(x => x <= DateTime.Today, () => "The {0} field cannot be in the future.");
            builder.Property(x => x.Salary)
                .InRange(0, 1000000);
            builder.Property(x => x.CreditCard)
                .CreditCardDataType();
            builder.Property(x => x.Address)
                .Required();
            builder.Property(x => x.ZipCode)
                .MatchesRegularExpression(@"^\d{5}$", () => "The {0} field is not a valid zip code.");
            #endregion

            #region display options
            builder.Property(x => x.Salary)
                .CurrencyDataType();
            #endregion
        }
    }
}
