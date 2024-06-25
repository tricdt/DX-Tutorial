using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LayoutControlDemo {
    public class AttributeSupportData {
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "d2", ApplyFormatInEditMode = true)]
        public int Age { get; set; }

        [Editable(false)]
        public string Employer { get; set; }

        [Display(Name = "First name", Order = 0), Required]
        public string FirstName { get; set; }

        [Display(Name = "Full name", Order = 2)]
        public string FullName { get { return FirstName + " " + LastName; } }

        [Display(ShortName = "Sex", Order = 3)]
        public Gender Gender { get; set; }

        [Display(Name = "Last name", Order = 1), Required]
        public string LastName { get; set; }

        [ReadOnlyAttribute(true)]
        public string SSN { get; set; }

        [DisplayFormat(NullDisplayText = "Department not set")]
        public string Department { get; set; }

    }
}
