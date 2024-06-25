using System;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    public class DataAnnotationsElement3 {

        [Display(Name = "First name", GroupName = "Personal")]
        public string FirstName { get; set; }

        [Display(Name = "Last name", GroupName = "Personal")]
        public string LastName { get; set; }

        [Display(GroupName = "Address", ShortName = "Address1")]
        public string Address { get; set; }

        [Display(GroupName = "Address", ShortName = "Address2")]
        public string City { get; set; }

        [Display(GroupName = "Personal", Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Display(GroupName = "Personal")]
        public Gender Gender { get; set; }

        [Display(GroupName = "Job")]
        public string Group { get; set; }

        [Display(GroupName = "Job", Name = "Hire date")]
        public DateTime HireDate { get; set; }

        [Display(GroupName = "Contact"), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(GroupName = "Contact")]
        public string Email { get; set; }

        [Display(GroupName = "Job"), DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(GroupName = "Job")]
        public string Title { get; set; }
    }
}
