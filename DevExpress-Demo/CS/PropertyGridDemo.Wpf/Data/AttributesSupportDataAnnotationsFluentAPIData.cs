using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;

namespace PropertyGridDemo {
    [MetadataType(typeof(AttributesSupportDataAnnotationsFluentAPIMetadata))]
    public class AttributesSupportDataAnnotationsFluentAPIData {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Group { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string Phone { get; set; }
        public string Title { get; set; }

        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
    public class AttributesSupportDataAnnotationsFluentAPIMetadata {
        public static void BuildMetadata(MetadataBuilder<AttributesSupportDataAnnotationsFluentAPIData> builder) {
            #region display options
            builder.Property(x => x.AddressLine1)
                .DisplayShortName("Address line 1");
            builder.Property(x => x.AddressLine2)
                .DisplayShortName("Address line 2");
            builder.Property(x => x.BirthDate)
                .DisplayShortName("Birth date")
                .DisplayFormatString("m", applyDisplayFormatInEditMode: true);
            builder.Property(x => x.FirstName)
                .DisplayShortName("First name");
            builder.Property(x => x.LastName)
                .DisplayShortName("Last name");
            builder.Property(x => x.Title)
                .NullDisplayText("Title not set");
            builder.Property(x => x.HireDate)
                .DisplayName("Hire date");
            builder.Property(x => x.Phone)
                .PhoneNumberDataType();
            builder.Property(x => x.Salary)
                .CurrencyDataType();
            builder.Property(x => x.Email)
                .EmailAddressDataType();
            #endregion

            #region layout
            builder.Group("Name")
                .ContainsProperty(x => x.FirstName)
                .ContainsProperty(x => x.LastName);
            builder.Group("Job")
                .ContainsProperty(x => x.Group)
                .ContainsProperty(x => x.Title)
                .ContainsProperty(x => x.HireDate)
                .ContainsProperty(x => x.Salary);
            builder.Group("Address")
                .ContainsProperty(x => x.AddressLine1)
                .ContainsProperty(x => x.AddressLine2);
            builder.Group("Contact")
                .ContainsProperty(x => x.Email)
                .ContainsProperty(x => x.Phone);
            builder.Group("Personal")
                .ContainsProperty(x => x.Gender)
                .ContainsProperty(x => x.BirthDate);
            #endregion
        }
    }
}
