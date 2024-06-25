using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Editors;
using System;

namespace EditorsDemo {
    public class InputValidationViewModel : ValidationViewModelBase {
        #region Validation Commands
        [Command]
        public void ValidateLogin(ValidationArgs args) {
            string login = args.Value as string;
            bool isValid = login != "TestUser";
            args.SetError(isValid, "A user with this name is already registered");
        }
        [Command]
        public void ValidateMail(ValidationArgs args) {
            string confirmMail = args.Value as string;
            bool isValid = Mail == confirmMail;
            args.SetError(isValid, "Two specified e-mail addresses are different");
        }
        [Command]
        public void ValidateName(ValidationArgs args) {
            string name = args.Value as string;
            bool isValid = !string.IsNullOrEmpty(name);
            args.SetError(isValid, "Name can't be empty");
        }
        [Command]
        public void ValidateAge(ValidationArgs args) {
            decimal age = (decimal)args.Value;
            if(age < 21)
                args.SetError(false, "Sorry, you're too young to visit our site!");
            if(age > 200)
                args.SetError(false, "Congratulations! You're the oldest man on Earth!");
        }
        [Command]
        public void ValidateCardType(ValidationArgs args) {
            string type = args.Value as string;
            bool isValid = type == "American Express" || type == "VISA";
            args.SetError(isValid, "Sorry, cards of this type are not accepted");
        }
        [Command]
        public void ValidateCardNumber(ValidationArgs args) {
            string number = args.Value as string;
            bool isValid;
            switch(CardType) {
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
            args.SetError(isValid, "Invalid number");
        }
        [Command]
        public void ValidateCardExpDate(ValidationArgs args) {
            DateTime expDate = (DateTime)args.Value;
            bool isValid = expDate.CompareTo(DateTime.Today) > 0;
            args.SetError(isValid, "Sorry, your card has expired");
        }
        #endregion

        #region Show Message
        IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } }
        [Command]
        public void ShowMessage() {
            MessageBoxService.ShowMessage("Thank you!", "Joined", MessageButton.OK, MessageIcon.None);
        }
        #endregion
    }

    public class ValidationArgs {
        public string ErrorContent { get; private set; }
        public object Value { get; private set; }

        public ValidationArgs(object value) {
            Value = value;
        }
        public void SetError(bool isValid, string errorContent) {
            ErrorContent = isValid ? null : errorContent;
        }
    }
    public class ValidationEventArgsConverter : EventArgsConverterBase<ValidationEventArgs> {
        protected override object Convert(object sender, ValidationEventArgs e) {
            return new ValidationArgs(e.Value);
        }
        protected override void ConvertBack(object sender, ValidationEventArgs e, object parameter) {
            var args = (ValidationArgs)parameter;
            e.IsValid = args.ErrorContent == null;
            e.ErrorContent = args.ErrorContent;
        }
    }
}
