using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;






namespace DevExpress.DataAnnotations {
    public abstract class DemoRegexAttributeBase : DataTypeAttribute {
        protected const RegexOptions DefaultRegexOptions = RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase;

        readonly Regex regex;

        public DemoRegexAttributeBase(string regex, string defaultErrorMessage, DataType dataType)
            : this(new Regex(regex, DefaultRegexOptions), defaultErrorMessage, dataType) {
        }
        public DemoRegexAttributeBase(Regex regex, string defaultErrorMessage, DataType dataType)
            : base(dataType) {
            this.regex = (Regex)regex;
            this.ErrorMessage = defaultErrorMessage;
        }
        public sealed override bool IsValid(object value) {
            if(value == null)
                return true;
            string input = value as string;
            return input != null && regex.Match(input).Length > 0;
        }
    }
    public sealed class DemoPhoneAttribute : DemoRegexAttributeBase {
        static readonly Regex regex = new Regex(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", DefaultRegexOptions);
        const string Message = "The {0} field is not a valid phone number.";
        public DemoPhoneAttribute()
            : base(regex, Message, DataType.PhoneNumber) {
        }
    }
    public sealed class DemoEmailAddressAttribute : DemoRegexAttributeBase {
        static readonly Regex regex = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", DefaultRegexOptions);
        const string Message = "The {0} field is not a valid e-mail address.";
        public DemoEmailAddressAttribute()
            : base(regex, Message, DataType.EmailAddress) {
        }
    }
    public sealed class DemoUrlAttribute : DemoRegexAttributeBase {
        static Regex regex = new Regex(@"^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$", DefaultRegexOptions);
        const string Message = "The {0} field is not a valid fully-qualified http, https, or ftp URL.";
        public DemoUrlAttribute()
            : base(regex, Message, DataType.Url) {
        }
    }
    public sealed class DemoZipCodeAttribute : DemoRegexAttributeBase {
        static Regex regex = new Regex(@"^[0-9][0-9][0-9][0-9][0-9]$", DefaultRegexOptions);
        const string Message = "The {0} field is not a valid ZIP code.";
        public DemoZipCodeAttribute()
            : base(regex, Message, DataType.Url) {
        }
    }
    public sealed class DemoCreditCardAttribute : DataTypeAttribute {
        const string Message = "The {0} field is not a valid credit card number.";
        public DemoCreditCardAttribute()
            : base(DataType.Custom) {
            this.ErrorMessage = Message;
        }
        public override bool IsValid(object value) {
            if(value == null)
                return true;
            string stringValue = value as string;
            if(stringValue == null)
                return false;
            stringValue = stringValue.Replace("-", "").Replace(" ", "");
            int number = 0;
            bool oddEvenFlag = false;
            foreach(char ch in stringValue.Reverse()) {
                if(ch < '0' || ch > '9')
                    return false;
                int digitValue = ((int)ch - (int)'0') * (oddEvenFlag ? 2 : 1);
                oddEvenFlag = !oddEvenFlag;
                while(digitValue > 0) {
                    number += digitValue % 10;
                    digitValue = digitValue / 10;
                }
            }
            return (number % 10) == 0;
        }
    }
}
