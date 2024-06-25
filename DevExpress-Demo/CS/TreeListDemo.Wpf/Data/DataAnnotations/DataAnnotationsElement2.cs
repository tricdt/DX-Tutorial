using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TreeListDemo {
    public class DataAnnotationsElement2 {

        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [Display(AutoGenerateField = false)]
        public int ParentID { get; set; }

        [Editable(false)]
        public string Employer { get; set; }

        [Display(Name = "First name"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name"), Required]
        public string LastName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        [ReadOnlyAttribute(true)]
        public string SSN { get; set; }

        public override string ToString() {
            return "Attribute support";
        }
    }
}
