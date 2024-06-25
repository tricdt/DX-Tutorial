using System;
using System.ComponentModel.DataAnnotations;

namespace PropertyGridDemo {
    public class AttributesSupportDataAnnotationsData {
        const string JobGroup = "Job";
        const string ContactGroup = "Contact";
        const string AddressGroup = "Address";
        const string Personal = "Personal";
        const string NameGroup = "Name";

        [Display(GroupName = NameGroup, Name = "First name")]
        public string FirstName { get; set; }
        [Display(GroupName = NameGroup, Name = "Last name")]
        public string LastName { get; set; }

        [Display(GroupName = JobGroup)]
        public string Group { get; set; }
        [DisplayFormat(NullDisplayText = "Title not set"), Display(GroupName = JobGroup)]
        public string Title { get; set; }
        [Display(GroupName = JobGroup, Name = "Hire date")]
        public DateTime HireDate { get; set; }
        [Display(GroupName = JobGroup), DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(GroupName = ContactGroup), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Display(GroupName = ContactGroup), EmailAddress]
        public string Email { get; set; }

        [Display(GroupName = Personal)]
        public Gender Gender { get; set; }
        [Display(GroupName = Personal, Name = "Birth date")]
        [DisplayFormat(DataFormatString = "m", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(GroupName = AddressGroup, Name = "Address line 1")]
        public string AddressLine1 { get; set; }
        [Display(GroupName = AddressGroup, Name = "Address line 2")]
        public string AddressLine2 { get; set; }
    }
}
