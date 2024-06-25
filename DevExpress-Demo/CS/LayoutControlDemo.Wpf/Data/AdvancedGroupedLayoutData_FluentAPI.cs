using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace LayoutControlDemo {
    [MetadataType(typeof(AdvancedGroupedLayoutMetadata))]
    public class AdvancedGroupedLayoutData_FluentAPI {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public Gender Gender { get; set; }
        public string Group { get; set; }
        public DateTime HireDate { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public string Title { get; set; }
    }
    public static class AdvancedGroupedLayoutMetadata {
        public static void BuildMetadata(MetadataBuilder<AdvancedGroupedLayoutData_FluentAPI> builder) {
            builder.DataFormLayout()
               .Group("Name", Orientation.Horizontal)
                   .ContainsProperty(x => x.FirstName)
                   .ContainsProperty(x => x.LastName)
               .EndGroup()
               .TabbedGroup("Tabs")
                   .Group("Job")
                       .ContainsProperty(x => x.Group)
                       .ContainsProperty(x => x.Title)
                       .ContainsProperty(x => x.HireDate)
                       .ContainsProperty(x => x.Salary)
                   .EndGroup()
                   .Group("Contact")
                       .ContainsProperty(x => x.Phone)
                       .ContainsProperty(x => x.Email)
                       .GroupBox("Address")
                           .ContainsProperty(x => x.AddressLine1)
                           .ContainsProperty(x => x.AddressLine2)
                       .EndGroup()
                   .EndGroup()
               .EndGroup()
               .GroupBox("Personal", Orientation.Horizontal)
                   .ContainsProperty(x => x.Gender)
                   .ContainsProperty(x => x.BirthDate)
               .EndGroup();


            builder.Property(x => x.AddressLine1)
                .DisplayShortName(string.Empty);
            builder.Property(x => x.AddressLine2)
                .DisplayShortName(string.Empty);
            builder.Property(x => x.BirthDate)
                .DisplayName("Birth date");
            builder.Property(x => x.FirstName)
                .DisplayName("First name");
            builder.Property(x => x.LastName)
                .DisplayName("Last name");
            builder.Property(x => x.HireDate)
                .DisplayName("Hire date");
            builder.Property(x => x.Phone)
                .PhoneNumberDataType();
            builder.Property(x => x.Salary)
                .CurrencyDataType();
        }
    }
}
