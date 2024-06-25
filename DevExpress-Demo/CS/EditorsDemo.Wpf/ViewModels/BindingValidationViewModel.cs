using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace EditorsDemo {
    public class BindingValidationViewModel : ValidationViewModelBase, IDataErrorInfo {
        string error;
        bool SetError(bool isValid, string errorString) {
            if(isValid)
                error = string.Empty;
            else
                error = errorString;
            return isValid;
        }

        #region IDataErrorInfo Members
        string IDataErrorInfo.Error { get { return error; } }
        string IDataErrorInfo.this[string columnName] {
            get {
                switch(columnName) {
                    case "Login":
                        return ValidateLogin(Login) ? string.Empty : error;
                    case "Mail":
                        return ValidateMail(Mail, ConfirmMail) ? string.Empty : error;
                    case "ConfirmMail":
                        return ValidateMail(Mail, ConfirmMail) ? string.Empty : error;
                    case "Age":
                        return ValidateAge(Age) ? string.Empty : error;
                    case "CardType":
                        return ValidateCardType(CardType) ? string.Empty : error;
                    case "CardExpDate":
                        return ValidateCardExpDate(CardExpDate) ? string.Empty : error;
                    case "CardNumber":
                        return ValidateCardNumber(CardType, CardNumber) ? string.Empty : error;
                }
                return string.Empty;
            }
        }
        #endregion

        #region Validation methods
        public bool ValidateLogin(string login) {
            bool isValid = login != "TestUser";
            return SetError(isValid, "A user with this name is already registered");
        }
        public bool ValidateMail(string mail, string confirmMail) {
            bool isValid = Equals(mail, confirmMail);
            return SetError(isValid, "Two specified e-mail addresses are different");
        }
        public bool ValidateAge(decimal age) {
            if(age < 21)
                return SetError(false, "Sorry, you're too young to visit our site!");
            if(age > 200)
                return SetError(false, "Congratulations! You're the oldest man on Earth!");
            return true;
        }
        public bool ValidateCardType(string type) {
            bool isValid = type == "American Express" || type == "VISA";
            SetError(isValid, "Sorry, cards of this type are not accepted");
            return isValid;
        }
        public bool ValidateCardNumber(string type, string number) {
            bool isValid;
            switch(type) {
                case "VISA":
                    isValid = !string.IsNullOrEmpty(number) && number.Length == 13;
                    break;
                case "Master Card":
                    isValid = !string.IsNullOrEmpty(number) && number.Length == 16;
                    break;
                case "American Express":
                    isValid = !string.IsNullOrEmpty(number) && number.Length == 15;
                    break;
                case "Diners Club":
                    isValid = !string.IsNullOrEmpty(number) && number.Length == 14;
                    break;
                default:
                    isValid = false;
                    break;
            }
            return SetError(isValid, "Invalid number");
        }
        public bool ValidateCardExpDate(DateTime expDate) {
            bool isValid = expDate.CompareTo(DateTime.Today) > 0;
            return SetError(isValid, "Sorry, your card has expired");
        }
        #endregion
    }

    public class EmptyStringValidationRule : ValidationRule {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
            return new ValidationResult(!string.IsNullOrEmpty((string)value), "Empty");
        }
    }
}
