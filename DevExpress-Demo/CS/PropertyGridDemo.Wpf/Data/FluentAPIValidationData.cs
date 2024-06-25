using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PropertyGridDemo {
    [MetadataType(typeof(FluentAPIValidationMetadata))]
    public class FluentAPIValidationData {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Group { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string CreditCard { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }

        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public static class FluentAPIValidationMetadata {
        public static void BuildMetadata(MetadataBuilder<FluentAPIValidationData> builder) {
            #region validation
            builder.Property(x => x.Email)
                .EmailAddressDataType();
            builder.Property(x => x.Phone)
                .PhoneNumberDataType();
            builder.Property(x => x.Facebook)
                .UrlDataType();
            builder.Property(x => x.Title)
                .MaxLength(20);
            builder.Property(x => x.HireDate)
                .MatchesRule(x => x <= DateTime.Today, () => "The {0} field cannot be in the future.");
            builder.Property(x => x.Salary)
                .InRange(0, 1000000);
            builder.Property(x => x.CreditCard)
                .CreditCardDataType();
            builder.Property(x => x.AddressLine1)
                .Required();
            builder.Property(x => x.ZipCode)
                .MatchesRegularExpression(@"^\d{5}$", () => "The {0} field is not a valid zip code.");
            #endregion

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
