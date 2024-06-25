using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    public class DataAnnotationsElement2 {
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [Editable(false)]
        public string Employer { get; set; }

        [Display(Name = "First name"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name"), Required]
        public string LastName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        [DisplayFormat(DataFormatString = "d2", ApplyFormatInEditMode = true)]
        public int Age { get; set; }

        [ReadOnlyAttribute(true)]
        public string SSN { get; set; }

        [DisplayFormat(NullDisplayText = "Department not set")]
        public string Department { get; set; }
    }
}
