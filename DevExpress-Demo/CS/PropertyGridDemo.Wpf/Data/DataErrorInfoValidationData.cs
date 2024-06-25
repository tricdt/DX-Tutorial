using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PropertyGridDemo {
    public class DataErrorInfoValidationData : BindableBase, IDataErrorInfo {
        public string this[string columnName] {
            get {
                switch(columnName) {
                    case "Login":
                        return ValidateLogin(Login) ? string.Empty : Error;
                    case "Mail":
                        return ValidateMail(Mail, ConfirmMail) ? string.Empty : Error;
                    case "ConfirmMail":
                        return ValidateMail(Mail, ConfirmMail) ? string.Empty : Error;
                    case "Age":
                        return ValidateAge(Age) ? string.Empty : Error;
                    case "CardType":
                        return ValidateCardType(CardType) ? string.Empty : Error;
                    case "CardExpDate":
                        return ValidateCardExpDate(CardExpDate) ? string.Empty : Error;
                    case "CardNumber":
                        return ValidateCardNumber(CardType, CardNumber) ? string.Empty : Error;
                }
                return string.Empty;
            }
        }
        public static readonly List<CardInfo> Cards = new List<CardInfo>();
        static DataErrorInfoValidationData() {
            Cards.Add(new CardInfo() { Name = "VISA", DisplayName = "VISA (13 digits)" });
            Cards.Add(new CardInfo() { Name = "Master Card", DisplayName = "Master Card (16 digits)" });
            Cards.Add(new CardInfo() { Name = "American Express", DisplayName = "American Express (15 digits)" });
            Cards.Add(new CardInfo() { Name = "Diners Club", DisplayName = "Diners Club (13 digits)" });
        }

        public DataErrorInfoValidationData() {
            Login = "TestUser";
            Mail = "testmail@devexpress.com";
            FirstName = "John";
            LastName = "Joe";
            CardType = "VISA";
        }
        
        string _error;
        DateTime _cardExpDate = DateTime.Today.AddMonths(-3);

        [Display(AutoGenerateField = false)]
        public string Error { get { return _error; } }

        public string Login {
            get { return GetProperty(() => Login); }
            set { SetProperty(() => Login, value); }
        }
        public string Mail {
            get { return GetProperty(() => Mail); }
            set {
                if(SetProperty(() => Mail, value)) {
                    RaisePropertiesChanged(GetPropertyName(() => ConfirmMail));
                }
            }
        }
        public string ConfirmMail {
            get { return GetProperty(() => ConfirmMail); }
            set {
                if(SetProperty(() => ConfirmMail, value)) {
                    RaisePropertiesChanged(GetPropertyName(() => Mail));
                }
            }
        }
        public string FirstName {
            get { return GetProperty(() => FirstName); }
            set { SetProperty(() => FirstName, value); }
        }
        public string LastName {
            get { return GetProperty(() => LastName); }
            set { SetProperty(() => LastName, value); }
        }
        public decimal Age {
            get { return GetProperty(() => Age); }
            set { SetProperty(() => Age, value); }
        }
        public string CardType {
            get { return GetProperty(() => CardType); }
            set { SetProperty(() => CardType, value); }
        }
        public string CardNumber {
            get { return GetProperty(() => CardNumber); }
            set { SetProperty(() => CardNumber, value); }
        }
        public DateTime CardExpDate {
            get { return GetProperty(() => CardExpDate); }
            set { SetProperty(() => CardExpDate, value); }
        }

        void SetError(bool isValid, string errorString) {
            if(isValid)
                _error = string.Empty;
            else
                _error = errorString;
        }

        public bool ValidateName(string name) {
            bool isValid = !string.IsNullOrEmpty(name);
            SetError(isValid, "Name can't be empty");
            return isValid;
        }
        public bool ValidateMail(string mail, string confirmMail) {
            bool isValid = object.Equals(mail, confirmMail);
            SetError(isValid, "Two specified e-mail addresses are different");
            return isValid;
        }
        public bool ValidateCardNumber(string type, string number) {
            bool isValid = false;
            switch(type) {
                case "VISA":
                    isValid = ValidateVISA(number);
                    break;
                case "Master Card":
                    isValid = ValidateMasterCard(number);
                    break;
                case "American Express":
                    isValid = ValidateAmericanExpress(number);
                    break;
                case "Diners Club":
                    isValid = ValidateDinersClub(number);
                    break;
                default:
                    isValid = false;
                    break;
            }
            SetError(isValid, "Invalid number");
            return isValid;
        }
        public bool ValidateLogin(string login) {
            bool isValid = login != "TestUser";
            SetError(isValid, "A user with this name is already registered");
            return isValid;
        }
        public bool ValidateAge(decimal age) {
            bool isValid = age > 21 && age < 200;
            if(age < 21) {
                SetError(isValid, "Sorry, you're too young to visit our site!");
                return false;
            } else if(age > 200) {
                SetError(isValid, "Congratulations! You're the oldest man on Earth!");
                return false;
            }
            return true;
        }
        public bool ValidateCardType(string type) {
            bool isValid = type == "American Express" || type == "VISA";
            SetError(isValid, "Sorry, cards of this type are not accepted");
            return isValid;
        }
        bool ValidateVISA(string num) {
            return !string.IsNullOrEmpty(num) && num.Length == 13;
        }
        bool ValidateMasterCard(string num) {
            return !string.IsNullOrEmpty(num) && num.Length == 16;
        }
        bool ValidateAmericanExpress(string num) {
            return !string.IsNullOrEmpty(num) && num.Length == 15;
        }
        bool ValidateDinersClub(string num) {
            return !string.IsNullOrEmpty(num) && num.Length == 14;
        }
        public bool ValidateCardExpDate(DateTime expDate) {
            bool isValid = expDate.CompareTo(DateTime.Today) > 0;
            SetError(isValid, "Sorry, your card has expired");
            return isValid;
        }
    }

    public class CardInfo {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
