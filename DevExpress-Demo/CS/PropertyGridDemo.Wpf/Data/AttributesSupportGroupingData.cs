using System;
using System.ComponentModel;

namespace PropertyGridDemo {
    public class AttributesSupportGroupingData {
        const string JobGroup = "Job";
        const string ContactGroup = "Contact";
        const string AddressGroup = "Address";
        const string PersonalGroup = "Personal";
        const string InfoGroup = "Info";
        [Category(InfoGroup), DisplayName("First name")]
        public string FirstName { get; set; }
        [Category(InfoGroup), DisplayName("Last name")]
        public string LastName { get; set; }

        [Category(AddressGroup)]
        public string AddressLine1 { get; set; }
        [Category(AddressGroup)]
        public string AddressLine2 { get; set; }

        [Category(PersonalGroup), DisplayName("Birth date")]
        public DateTime BirthDate { get; set; }
        [Category(PersonalGroup)]
        public Gender Gender { get; set; }

        [Category(ContactGroup)]
        public string Email { get; set; }
        [Category(ContactGroup)]
        public string Phone { get; set; }

        [Category(JobGroup)]
        public string Group { get; set; }
        [Category(JobGroup), DisplayName("Hire date")]
        public DateTime HireDate { get; set; }
        [Category(JobGroup)]
        public decimal Salary { get; set; }
        [Category(JobGroup)]
        public string Title { get; set; }
    }
}
