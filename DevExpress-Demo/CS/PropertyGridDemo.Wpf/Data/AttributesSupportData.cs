using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel;

namespace PropertyGridDemo {
    public class AttributesSupportData {
        [Browsable(false)]
        public int ID { get; set; }

        [DefaultValueAttribute(18)]
        [NumericMask(Mask = PredefinedMasks.Numeric.Decimal + "2")]
        public int Age { get; set; }

        [DateTimeMask(Mask = PredefinedMasks.DateTime.LongDate)]
        public DateTime HireDate { get; set; }

        const string SSNMask = @"\d{3}-\d{2}-\d{4}";
        [RegExMask(Mask = SSNMask)]
        public string SSN { get; set; }

        [DisplayName("First name"), RefreshProperties(RefreshProperties.All)]
        public string FirstName { get; set; }

        [DisplayName("Last name"), RefreshProperties(RefreshProperties.All)]
        public string LastName { get; set; }

        [DisplayName("Full name")]
        public string FullName {
            get {
                string delimiter = string.IsNullOrEmpty(FirstName) ? string.Empty : " ";
                return FirstName + delimiter + LastName;
            }
        }

        [ReadOnlyAttribute(true)]
        public Gender? Gender { get; set; }
    }
}
