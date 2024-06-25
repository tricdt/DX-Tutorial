using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    [MetadataType(typeof(DataAnnotationsElement3Metadata))]
    public class DataAnnotationsElement3_FluentAPI {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public static class DataAnnotationsElement3Metadata {
        public static void BuildMetadata(MetadataBuilder<DataAnnotationsElement3_FluentAPI> builder) {
            builder.Property(p => p.FirstName).DisplayName("First name");
            builder.Property(p => p.LastName).DisplayName("Last name");
            builder.Property(p => p.BirthDate).DisplayName("Birth date");
            builder.Group("Personal")
                .ContainsProperty(p => p.FirstName)
                .ContainsProperty(p => p.LastName)
                .ContainsProperty(p => p.Gender)
                .ContainsProperty(p => p.BirthDate);

            builder.Property(p => p.Address).DisplayShortName("Address1");
            builder.Property(p => p.City).DisplayShortName("Address2");
            builder.Group("Address")
                .ContainsProperty(p => p.Address)
                .ContainsProperty(p => p.City);

            builder.Property(p => p.HireDate).DisplayName("Hire date");
            builder.Property(p => p.Salary).CurrencyDataType();
            builder.Group("Job")
                .ContainsProperty(p => p.Group)
                .ContainsProperty(p => p.Title)
                .ContainsProperty(p => p.HireDate)
                .ContainsProperty(p => p.Salary);

            builder.Property(p => p.Phone).PhoneNumberDataType();
            builder.Group("Contact")
                .ContainsProperty(p => p.Phone)
                .ContainsProperty(p => p.Email);
        }
    }
}
