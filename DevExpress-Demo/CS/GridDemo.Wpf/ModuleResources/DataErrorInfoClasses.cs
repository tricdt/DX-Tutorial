using DevExpress.XtraEditors.DXErrorProvider;

namespace GridDemo {
    public class PersonInfo : object, IDXDataErrorInfo {
        public PersonInfo(string firstName, string lastName, string address, string phoneNumber, string email) {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        #region IDXDataErrorInfo Members
        void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info) {
            switch(propertyName) {
                case "FirstName":
                case "LastName":
                    if(IsStringEmpty(propertyName == "FirstName" ? FirstName : LastName)) {
                        SetErrorInfo(info, propertyName + " field can't be empty", ErrorType.Critical);
                    }
                    break;
                case "Address":
                    if(IsStringEmpty(Address)) {
                        SetErrorInfo(info, "Address hasn't been entered", ErrorType.Information);
                    }
                    break;
                case "Email":
                    if(IsStringEmpty(Email)) {
                        SetErrorInfo(info, "Email hasn't been entered", ErrorType.Information);
                    } else if(Email != "none" && !IsEmailCorrect(Email)) {
                        SetErrorInfo(info, "Wrong email address", ErrorType.Warning);
                    }
                    break;
            }
        }
        void IDXDataErrorInfo.GetError(ErrorInfo info) {
            if(IsStringEmpty(PhoneNumber) && (Email == "none" || !IsEmailCorrect(Email)))
                SetErrorInfo(info, "Either Phone Number or Email should be specified", ErrorType.Information);
        }
        #endregion
        bool IsStringEmpty(string str) {
            return str == null || str.Trim().Length == 0;
        }
        bool IsEmailCorrect(string email) {
            return email == null || (email.IndexOf("@") >= 1 && email.Length > email.IndexOf("@") + 1);
        }
        void SetErrorInfo(ErrorInfo info, string errorText, ErrorType errorType) {
            info.ErrorText = errorText;
            info.ErrorType = errorType;
        }
    }
}
